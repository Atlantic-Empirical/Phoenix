#Region "IMPORTS"

'-----------------------------------------------------------
'                   Phoenix DVD Emulator
'                           by
'               Sequoyan Media Technology Inc.
'              All Rights Reserved. 2004 - 2008
'-----------------------------------------------------------

Imports DevExpress.XtraBars.Docking
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Controls
Imports SMT.Applications.Phoenix.Classes
Imports SMT.Applications.Phoenix.Enums
Imports SMT.DotNet.AppConsole
Imports SMT.DotNet.Reflection
Imports SMT.DotNet.Serialization.XML
Imports SMT.DotNet.UserInterface.WindowsForms
Imports SMT.DotNet.Utility
Imports SMT.IPProtection.SafeNet
Imports SMT.Multimedia.DirectShow
Imports SMT.Multimedia.Enums
Imports SMT.Multimedia.Formats.AC3
Imports SMT.Multimedia.Formats.DVD.IFO
Imports SMT.Multimedia.Hardware.Blackmagic.DeviceDetection
Imports SMT.Multimedia.Players
Imports SMT.Multimedia.Players.DVD
Imports SMT.Multimedia.Players.DVD.Classes
Imports SMT.Multimedia.Players.DVD.Enums
Imports SMT.Multimedia.Players.DVD.Structures
Imports SMT.Multimedia.Players.DTSAC3Player
Imports SMT.Multimedia.Players.PCMWAVPlayer
Imports SMT.Multimedia.Players.SDMPEG2
Imports SMT.Multimedia.Utility.Timecode
Imports SMT.Win.Filesystem
Imports SMT.Win.KeyboardHooking
Imports SMT.Win.OSVersion
Imports SMT.Win.ProcessExecution
Imports SMT.Win.Registry
Imports SMT.Win.UserInterface
Imports SMT.Win.WinAPI
Imports SMT.Win.WinAPI.Constants
Imports System.IO
Imports System.Reflection
Imports System.Text
Imports System.Threading
Imports System.Xml.Serialization
Imports SMT.Multimedia.GraphConstruction

#End Region 'IMPORTS

Public Class Phoenix_Form
    Inherits DevExpress.XtraEditors.XtraForm

#Region "PUBLIC PROPERTIES"

    Public WithEvents Player As cDVDPlayer
    Private WithEvents Viewer As SMT.Multimedia.UI.WPF.Viewer.Viewer_WPF

    Public ReadOnly Property MacroStorageDirectory() As String
        Get
            If Not Directory.Exists(Player.DumpDirectory & "Macros\") Then Directory.CreateDirectory(Player.DumpDirectory & "Macros\")
            Return Player.DumpDirectory & "Macros\"
        End Get
    End Property

    Public ReadOnly Property SplashBitmap() As Bitmap
        Get
            Dim str As Stream = Me.GetType.Module.Assembly.GetManifestResourceStream("SMT.Applications.Phoenix.PhoenixSplash_080522.png")
            'Dim str As Stream = Me.GetType.Module.Assembly.GetManifestResourceStream("SMT.Applications.Phoenix.PhoenixSplash_070803.png")
            Return New Bitmap(str)
        End Get
    End Property

#End Region 'PUBLIC PROPERTIES

#Region "PRIVATE PROPERTIES"

    Private ExtendedOptionsForm As ExtendedOptions_Form
    Private NonSeamlessCellsDialog As dlgNonSeamlessCells

    Public ReadOnly Property DecklinkAvailable() As Boolean
        Get
            Return _DecklinkAvailable
        End Get
    End Property
    Private _DecklinkAvailable As Boolean = False
    Public ReadOnly Property IntensityAvailable() As Boolean
        Get
            Return _IntensityAvailable
        End Get
    End Property
    Private _IntensityAvailable As Boolean = False

    Public UserOperationTemplates As cUserOperationTemplates

#End Region 'PRIVATE PROPERTIES

#Region "CONSTRUCTOR"

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        Try
            'UPGRADE SETTINGS IF NEEDED - run only on first run after installation of new version
            If My.Settings.UPGRADE_SETTINGS Then
                My.Settings.Upgrade()
                My.Settings.UPGRADE_SETTINGS = False
                My.Settings.Save()
            End If

            'SETUP DONGLE
            InitializeDongle()

            'SETUP FEATURE MANAGEMENT
            _FeatureManagement = New cPhoenixFeatureManagement(LicensedVersion)

            'RUNNING INSTANCE CHECK
            Dim assemblyName As String = Path.GetFileName(System.Reflection.Assembly.GetExecutingAssembly().Location)
            If IsProcessRunning(assemblyName) Then
                XtraMessageBox.Show(Me.LookAndFeel, Me, "Only one instance of this application may be run at a time.", My.Settings.APPLICATION_NAME, MessageBoxButtons.OK, MessageBoxIcon.Stop)
                Process.GetCurrentProcess.Kill()
            End If

            'FILTER CHECK
            Dim FC As String = Filter_Check()
            If FC <> "True" Then
                XtraMessageBox.Show(Me.LookAndFeel, Me, "Phoenix is not configured correctly. Please either re-run the installer or contact SMT for support." & vbNewLine & vbNewLine & "Message code: " & 327, My.Settings.APPLICATION_NAME, MessageBoxButtons.OK, MessageBoxIcon.Stop)
                Process.GetCurrentProcess.Kill()
            End If

            'CHECK KEYSTONE VERSION
            If Not Keystone_Check() Then
                XtraMessageBox.Show(Me.LookAndFeel, Me, "The wrong version of SMT Keystone is installed. Contact SMT for support.", My.Settings.APPLICATION_NAME, MessageBoxButtons.OK, MessageBoxIcon.Stop)
                Process.GetCurrentProcess.Kill()
            End If

            'CHECK DECKLINK/INTENSITY
            Select Case Blackmagic_Check()
                Case eBMDDetectionResult.NOT_FOUND
                    Me._MaxAVMode = eAVMode.DesktopVMR
                Case eBMDDetectionResult.DECKLINK_INSTALLED
                    _DecklinkAvailable = True
                    If LicensedVersion >= ePhoenixLicense.Ultimate Then Me._MaxAVMode = eAVMode.Decklink
                Case eBMDDetectionResult.INTENSITY_INSTALLED
                    _IntensityAvailable = True
                    If LicensedVersion >= ePhoenixLicense.Pro Then Me._MaxAVMode = eAVMode.Intensity
            End Select

            'CHECK/GET OS VERSION
            If Not OS_Check() Then
                XtraMessageBox.Show(Me.LookAndFeel, Me, "The current operating system is not supported.", My.Settings.APPLICATION_NAME, MessageBoxButtons.OK, MessageBoxIcon.Stop)
                Process.GetCurrentProcess.Kill()
            End If

            'CHECK VERSION OF QDVD
            If Not QDVD_Check() Then
                XtraMessageBox.Show(Me.LookAndFeel, Me, "The version of the Microsoft DVD runtime on this system is not supported." & vbNewLine & vbNewLine & "Contact SMT for an update.", My.Settings.APPLICATION_NAME, MessageBoxButtons.OK, MessageBoxIcon.Stop)
                Process.GetCurrentProcess.Kill()
            End If

            ''GRAPHICS CHECK
            'If Not Graphics_Check() Then
            '    XtraMessageBox.Show(Me.LookAndFeel, Me, "The display adapter in this system is not supported by Phoenix." & vbNewLine & vbNewLine & "An nVidia card is required. Contact SMT support with questions.", My.Settings.APPLICATION_NAME, MessageBoxButtons.OK, MessageBoxIcon.Stop)
            '    Process.GetCurrentProcess.Kill()
            'End If

            'SHOW SPLASH SCREEN
            Dim ss As New Phoenix_SplashScreen(True, SplashBitmap, LicensedVersion, False)
            Dim dr As DialogResult = ss.ShowDialog
            Select Case dr
                Case DialogResult.Yes
                    PreferredAVMode = eAVMode.DesktopVMR
                Case DialogResult.Abort
                    Debug.WriteLine("Problem with splash screen.")
            End Select

            'DESKTOP SIZE CHECK / WARNING
            If Desktop_W(Me) < 1152 Or Desktop_H(Me) < 864 Then
                XtraMessageBox.Show(Me.LookAndFeel, Me, "Phoenix requires minimum desktop size of 1152x864.", My.Settings.APPLICATION_NAME, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If

            'DEBUGGING
            'Me.EnforcedAVMode = eAVMode.DesktopVMR
            'DEBUGGING

            'SET CURSOR
            Me.Cursor = Cursors.WaitCursor

            'SETUP PROFILE
            InitializeUserProfile()

        Catch ex As Exception
            XtraMessageBox.Show(Me.LookAndFeel, Me, "Problem with New(). APPLICATION EXITING. Error: " & ex.Message, My.Settings.APPLICATION_NAME, MessageBoxButtons.OK, MessageBoxIcon.Stop)
            Process.GetCurrentProcess.Kill()
        End Try
    End Sub

#End Region 'CONSTRUCTOR

#Region "ENVIRONMENT"

#Region "ENVIRONMENT:INSTALLATION"

    Private Function OS_Check() As Boolean
        Try
            Select Case Environment.OSVersion.Platform
                Case PlatformID.Unix, PlatformID.Win32S, PlatformID.Win32Windows, PlatformID.WinCE ', PlatformID.Xbox, PlatformID.MacOSX
                    Return False
                Case PlatformID.Win32NT
                    Select Case Environment.OSVersion.Version.Major
                        Case 3, 4 'NT 3.51, NT 4.0
                            Return False
                        Case 5
                            Select Case Environment.OSVersion.Version.Minor
                                Case 0 'Win2000
                                    Return False
                                Case 1 'WinXP
                                    'If InStr(Environment.OSVersion.VersionString.ToLower, "Service Pack 3") Then
                                    '    OS_IsSupported = True
                                    'Else
                                    '    XtraMessageBox.Show(Me.LookAndFeel, Me, "Please upgrage to SP3.", My.Settings.APPLICATION_NAME, MessageBoxButtons.OK, MessageBoxIcon.Stop)
                                    'End If
                                    Return True
                                Case 2 'Win2003
                                    Return False
                            End Select
                        Case 6 'Vista/Win2008
                            'Dim prd As mOSVersion.eWindowsProduct = mOSVersion.GetWindowsProduct()
                            Dim prd As eWindowsProduct
                            GetProductInfo(6, 0, 0, 0, prd)
                            Select Case prd
                                Case eWindowsProduct.ClusterServer, eWindowsProduct.DatacenterServer, eWindowsProduct.DatacenterServerCore, eWindowsProduct.EnterpriseN, eWindowsProduct.EnterpriseServer, eWindowsProduct.EnterpriseServerCore, eWindowsProduct.EnterpriseServerIa64
                                    Return False
                                Case eWindowsProduct.HomeBasic, eWindowsProduct.HomeBasicN, eWindowsProduct.HomePremium, eWindowsProduct.HomeServer
                                    Return False
                                Case eWindowsProduct.ServerForSmallbusiness, eWindowsProduct.SmallbusinessServer, eWindowsProduct.SmallbusinessServerPremium, eWindowsProduct.StandardServer, eWindowsProduct.StandardServerCore, eWindowsProduct.Starter, eWindowsProduct.StorageEnterpriseServer, eWindowsProduct.StorageExpressServer, eWindowsProduct.StorageStandardServer, eWindowsProduct.StorageWorkgroupServer
                                    Return False
                                Case eWindowsProduct.Undefined, eWindowsProduct.WebServer, eWindowsProduct.WebServerCore
                                    Return False
                                Case eWindowsProduct.Business, eWindowsProduct.BusinessN, eWindowsProduct.Ultimate, eWindowsProduct.UltimateN, eWindowsProduct.Enterprise
                                    Return True
                            End Select
                        Case 7 'Win7
                            Debug.WriteLine("Win7")
                        Case Else
                            Return False
                    End Select
            End Select
            Return False
        Catch ex As Exception
            AddConsoleLine(eConsoleItemType.ERROR, "Problem with OS_Check(). Error: " & ex.Message)
            Return False
        End Try
    End Function

    Private Function QDVD_Check() As Boolean
        Try
            Dim FVI As FileVersionInfo
            Dim SystemPath As String = Environment.GetFolderPath(Environment.SpecialFolder.System)
            Dim QDVDPath As String = SystemPath & "\qdvd.dll"
            If File.Exists(QDVDPath) Then
                FVI = FileVersionInfo.GetVersionInfo(QDVDPath)
                Select Case FVI.FileMajorPart
                    Case Is < 6
                        Return False
                    Case 6
                        Select Case FVI.FileMinorPart
                            Case Is < 5
                                Return False
                            Case 5
                                If FVI.FileBuildPart = "2600" Then
                                    If FVI.ProductVersion = "6.05.2600.2831" Or FVI.ProductVersion = "6.05.2600.5512" Then
                                        'XP up-to date with all functionality for XP
                                        Return True
                                    Else
                                        'The nav is not uptodate for full functionality on XP.
                                        'THIS NEEDS TO BE TESTED
                                        Dim res As DialogResult = XtraMessageBox.Show(Me.LookAndFeel, Me, _
                                            "The version of DVD runtime on this system is sufficient to run Phoenix but you'll" & vbNewLine & _
                                            "not have access to some emulation functionality." & vbNewLine & vbNewLine & _
                                            "ABORT = Close Phoenix." & vbNewLine & _
                                            "RETRY = Attempt upgrade of DVD Runtime." & vbNewLine & _
                                            "IGNORE = Run Phoenix with reduced functionality.", _
                                            My.Settings.APPLICATION_NAME, _
                                            MessageBoxButtons.AbortRetryIgnore, _
                                            MessageBoxIcon.Question)
                                        Select Case res
                                            Case DialogResult.Abort
                                                Process.GetCurrentProcess.Kill()
                                            Case DialogResult.Ignore
                                                Return True
                                            Case DialogResult.Retry
                                                mNavUpgrader.InstallNav_XP()
                                                Thread.Sleep("1000")
                                                Process.GetCurrentProcess.Kill()
                                        End Select
                                    End If
                                Else
                                    Return False
                                End If
                            Case 6
                                If FVI.FileBuildPart < "6001" Then
                                    'vista pre update fall 2008
                                    'THIS NEEDS TO BE TESTED
                                    Dim res As DialogResult = XtraMessageBox.Show(Me.LookAndFeel, Me, _
                                        "The version of DVD runtime on this system is sufficient to run Phoenix but you'll" & vbNewLine & _
                                        "not have access to some emulation functionality." & vbNewLine & vbNewLine & _
                                        "ABORT = Close Phoenix." & vbNewLine & _
                                        "RETRY = Attempt upgrade of DVD Runtime." & vbNewLine & _
                                        "IGNORE = Run Phoenix with reduced functionality.", _
                                        My.Settings.APPLICATION_NAME, _
                                        MessageBoxButtons.AbortRetryIgnore, _
                                        MessageBoxIcon.Question)
                                    Select Case res
                                        Case DialogResult.Abort
                                            Process.GetCurrentProcess.Kill()
                                        Case DialogResult.Ignore
                                            Return True
                                        Case DialogResult.Retry
                                            mNavUpgrader.InstallNav_Vista()
                                            Thread.Sleep("1000")
                                            Process.GetCurrentProcess.Kill()
                                    End Select
                                Else
                                    ' This should always only return for the most up-to-date qdvd we have for Vista.
                                    Return True
                                End If

                            Case Is > 6
                                Return False
                        End Select
                    Case Is > 6
                        Return False
                End Select
            End If
            Return False
        Catch ex As Exception
            Me.AddConsoleLine(eConsoleItemType.ERROR, "Problem with QDVD_Check(). Error: " & ex.Message)
            Return False
        End Try
    End Function

    Private Function Keystone_Check() As Boolean
        Try
            Dim FVI As FileVersionInfo
            Dim CommonFilesPath As String = Environment.GetFolderPath(Environment.SpecialFolder.CommonProgramFiles)
            Dim KeystonePath As String = CommonFilesPath & "\SMT Shared\Filters\keystone_omni.ax"
            If Not File.Exists(KeystonePath) Then Throw New Exception("Keystone is not installed correctly.")
            FVI = FileVersionInfo.GetVersionInfo(KeystonePath)
            Return FVI.FileMajorPart = 2 And FVI.FileMinorPart = 0
        Catch ex As Exception
            Me.AddConsoleLine(eConsoleItemType.ERROR, "Problem with Keystone_Check(). Error: " & ex.Message)
            Return False
        End Try
    End Function

    Private Function Blackmagic_Check() As eBMDDetectionResult
        Try
            ' FIRST - check to see if a BMD card is present in the system.
            Dim out As eBMDDetectionResult = BMD_Detection()
            out = eBMDDetectionResult.NOT_FOUND    '090711/3.0.2


            ' SECOND - check to see that the driver version is correct.
            Dim FVI As FileVersionInfo
            Dim ProgramFilesPath As String = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)
            Dim DecklinkPath As String = ProgramFilesPath & "\Blackmagic Design\Blackmagic DeckLink\decklink.dll"
            Dim DecklinkInstalled As Boolean = False
            If File.Exists(DecklinkPath) Then
                out = eBMDDetectionResult.DECKLINK_INSTALLED    '090711/3.0.2

                FVI = FileVersionInfo.GetVersionInfo(DecklinkPath)
                'If FVI.ProductVersion <> "6.8" Then
                If FVI.ProductVersion <> "7.6.4" Then
                    XtraMessageBox.Show(Me.LookAndFeel, Me, "The wrong version of Blackmagic Decklink drivers are installed. 7.6.4 is required for Phoenix.", My.Settings.APPLICATION_NAME, MessageBoxButtons.OK, MessageBoxIcon.Stop)
                    DecklinkInstalled = False
                Else
                    DecklinkInstalled = True
                End If
            End If
            Dim IntensityPath As String = ProgramFilesPath & "\Blackmagic Design\Blackmagic Intensity\decklink.dll"
            Dim IntensityInstalled As Boolean = False
            If File.Exists(IntensityPath) Then

                'start - 090711/3.0.2
                If out = eBMDDetectionResult.DECKLINK_INSTALLED Then
                    out = eBMDDetectionResult.DECKLINK_AND_INTENSITY_INSTALLED
                Else
                    out = eBMDDetectionResult.INTENSITY_INSTALLED
                End If
                'end - 090711/3.0.2

                FVI = FileVersionInfo.GetVersionInfo(IntensityPath)
                If FVI.ProductVersion <> "7.2" Then
                    XtraMessageBox.Show(Me.LookAndFeel, Me, "The wrong version of Blackmagic Intensity drivers are installed. 7.2 is required for Phoenix.", My.Settings.APPLICATION_NAME, MessageBoxButtons.OK, MessageBoxIcon.Stop)
                    IntensityInstalled = False
                Else
                    IntensityInstalled = True
                End If
            End If

            Select Case out
                Case eBMDDetectionResult.NOT_FOUND
                    Return out
                Case eBMDDetectionResult.DECKLINK_INSTALLED
                    If Not DecklinkInstalled Then
                        Return eBMDDetectionResult.NOT_FOUND
                    Else
                        Return out
                    End If
                Case eBMDDetectionResult.INTENSITY_INSTALLED
                    If Not IntensityInstalled Then
                        Return eBMDDetectionResult.NOT_FOUND
                    Else
                        Return out
                    End If

                Case eBMDDetectionResult.DECKLINK_AND_INTENSITY_INSTALLED
                    If DecklinkInstalled And IntensityInstalled Then Return eBMDDetectionResult.DECKLINK_AND_INTENSITY_INSTALLED
                    If DecklinkInstalled Then Return eBMDDetectionResult.DECKLINK_INSTALLED
                    If IntensityInstalled Then Return eBMDDetectionResult.INTENSITY_INSTALLED
                    Return eBMDDetectionResult.NOT_FOUND
            End Select

            Return eBMDDetectionResult.NOT_FOUND
        Catch ex As Exception
            Me.AddConsoleLine(eConsoleItemType.ERROR, "Problem with Blackmagic_Check(). Error: " & ex.Message)
            Return False
        End Try
    End Function

    Private Function Filter_Check() As String
        Return cDVDPlayer.FilterCheck_shr()
    End Function

    Private Function Graphics_Check() As Boolean
        Try
            Dim alc As Microsoft.DirectX.Direct3D.AdapterListCollection = Microsoft.DirectX.Direct3D.Manager.Adapters
            If alc Is Nothing Then Return False
            Dim out As Boolean = False
            For i As Integer = 0 To alc.Count - 1
                If InStr(alc(i).Information.Description.ToLower, "nvidia") Then out = True
            Next
            alc = Nothing
            Return out
        Catch ex As Exception
            Me.AddConsoleLine(eConsoleItemType.ERROR, "Problem with Graphics_Check(). Error: " & ex.Message)
            Return False
        End Try
    End Function

#End Region 'ENVIRONMENT:INSTALLATION

#Region "ENVIRONMENT:MEMORY LEAK DETECTION"

    Private LastMemAvailable As ULong = 0
    Private MemUseTrend() As Boolean 'Down = True
    Private RunMemLeakCheck As Boolean = True

    Public ReadOnly Property MemIsLeaking() As Boolean
        Get
            Return _MemIsLeaking
        End Get
    End Property
    Private _MemIsLeaking As Boolean = False

    Private Sub CheckForMemLeak()
        Try
            If Not RunMemLeakCheck Then Exit Sub
            If LastMemAvailable = 0 Then
                ReDim MemUseTrend(-1)
                GoTo Finish
            End If
            Dim CurrentMemAvailale As ULong = My.Computer.Info.AvailablePhysicalMemory
            ReDim Preserve MemUseTrend(UBound(MemUseTrend) + 1)
            MemUseTrend(UBound(MemUseTrend)) = CurrentMemAvailale < LastMemAvailable
            'Debug.WriteLine("Last Mem: " & LastMemAvailable)
            'Debug.WriteLine("Cur Mem: " & CurrentMemAvailale)
            'Debug.WriteLine("Mem Trend is Down: " & MemUseTrend(UBound(MemUseTrend)))

            'check to see if mem is too low for Phoenix to run
            If CurrentMemAvailale / 1024000 < 75 Then
                If XtraMessageBox.Show(Me.LookAndFeel, Me, "Memory available is dangerously low. Phoenix will now close. Please close unneeded applications and run Phoenix again.", My.Settings.APPLICATION_NAME, MessageBoxButtons.OK, MessageBoxIcon.Stop) = DialogResult.OK Then
                End If
                Application.Exit()
            End If

            'look for leak
            Dim t As ULong = 0
            Dim Tolerance As Byte = 5
            If UBound(MemUseTrend) < Tolerance Then Exit Sub
            For i As Long = UBound(MemUseTrend) - Tolerance To UBound(MemUseTrend)
                If MemUseTrend(i) Then
                    t = t << 1 Or 1
                End If
            Next

            Dim val As ULong = 0
            For i As Byte = 0 To Tolerance
                val = (val << 1) Or 1
            Next
            If t = val Then
                _MemIsLeaking = True
                Debug.WriteLine("MEMORY IS LEAKING")
                Me.btnScrap1.ButtonStyle = BorderStyles.Office2003
            End If
Finish:
            LastMemAvailable = My.Computer.Info.AvailablePhysicalMemory
        Catch ex As Exception
            Me.AddConsoleLine(eConsoleItemType.ERROR, "Problem with CheckForMemLeak(). Error: " & ex.Message)
        End Try
    End Sub

#End Region 'ENVIRONMENT:MEMORY LEAK DETECTION

#End Region 'ENVIRONMENT

#Region "FORM EVENTS"

    Protected Overrides Sub WndProc(ByRef m As Message)
        Try
            Select Case m.Msg
                Case WM_USER
                    Debug.WriteLine("WM_USER: " & m.Msg)

                Case WM_DVD_EVENT
                    'Debug.WriteLine("WM_DVD_EVENT: " & m.Msg)
                    If Not Player Is Nothing Then
                        Player.HandleEvent()
                    End If
                    If Not StreamPlayer Is Nothing Then
                        StreamPlayer.BaseHandleEvent()
                    End If
                    Exit Sub

                Case WM_ENTERMENULOOP
                    'If Not KH Is Nothing Then KH.PauseHook()
                    'Debug.WriteLine("Keyboard Hook Paused On Menu Enter")
                    Debug.WriteLine("Enter menu loop")

                Case WM_EXITMENULOOP
                    'If Not KH Is Nothing Then KH.UnpauseHook()
                    'Debug.WriteLine("Keyboard Hook Paused On Menu Exit")

            End Select
            MyBase.WndProc(m)
        Catch ex As Exception
            Me.AddConsoleLine(eConsoleItemType.ERROR, "Problem with WndProc(). Error: " & ex.Message)
        End Try
    End Sub

    Private Sub frmPhoenixParent_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'LOAD THE SYSTEM JACKET PICTURE
        ShowSystemJacketPicture()

        If Not SetupEmulator() Then
            XtraMessageBox.Show(Me.LookAndFeel, Me, "Problem setting up emulator.", My.Settings.APPLICATION_NAME, MessageBoxButtons.OK, MessageBoxIcon.Stop)
            Process.GetCurrentProcess.Kill()
        End If
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub Phoenix_Form_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        If Player Is Nothing Then
            XtraMessageBox.Show(Me.LookAndFeel, Me, "Fatal exception during setup of " & My.Settings.APPLICATION_NAME & ". Application exiting. Please refer to installation documentation or contact SMT technical support for assistance.", My.Settings.APPLICATION_NAME, MessageBoxButtons.OK, MessageBoxIcon.Stop)
            Application.Exit()
        End If
        If Player.AVMode = eAVMode.DesktopVMR Then Player.ShowViewer()
    End Sub

    Private Sub frmPhoenixParent_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        ExitRoutiene()
    End Sub

    Public Sub ExitRoutiene()
        Try
            My.Settings.WINDOW_MAIN_X = Me.Left
            My.Settings.WINDOW_MAIN_Y = Me.Top
            If Player IsNot Nothing Then
                If Viewer IsNot Nothing Then
                    My.Settings.WINDOW_VIEWER_X = Viewer.Left
                    My.Settings.WINDOW_VIEWER_Y = Viewer.Top
                End If
            End If

            If Player.PlayState = ePlayState.FrameStepping Then
                Player.Play()
                System.Threading.Thread.Sleep(1500)
            End If
            Player.Dispose()

            KH.UnhookKeyboard()
            My.Settings.Save()
            Application.Exit()
        Catch ex As Exception
            Debug.WriteLine("Problem with ExitRoutine().")
        End Try
    End Sub

#End Region 'FORM EVENTS 

#Region "PANELS"

#Region "PANELS:REMOTE"

#Region "PANELS:REMOTE:DIRECTIONAL BUTTONS"

    Private Sub btnRM_Enter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRM_Enter.Click
        Player.EnterBtn()
    End Sub

    Private Sub btnRM_Up_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRM_Up.Click
        Player.DirectionalBtnHit(DvdRelButton.Upper)
    End Sub

    Private Sub btnRM_Left_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRM_Left.Click
        Player.DirectionalBtnHit(DvdRelButton.Left)
    End Sub

    Private Sub btnRM_Right_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRM_Right.Click
        Player.DirectionalBtnHit(DvdRelButton.Right)
    End Sub

    Private Sub btnRM_Down_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRM_Down.Click
        Player.DirectionalBtnHit(DvdRelButton.Lower)
    End Sub

#End Region 'PANELS:REMOTE:DIRECTIONAL BUTTONS

#Region "PANELS:REMOTE:STREAM CYCLING"

    Private Sub btnRM_CycleSubtitles_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRM_CycleSubtitles.Click
        Player.CycleSubtitles()
    End Sub

    Private Sub btnRM_CycleAngle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRM_CycleAngle.Click
        Player.CycleAngle()
    End Sub

    Private Sub btnRM_CycleAudio_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRM_CycleAudio.Click
        Player.CycleAudio()
    End Sub

#End Region 'PANELS:REMOTE:STREAM CYCLING

#Region "PANELS:REMOTE:MENU CALLS"

    Private Sub btnRM_CallRootMenu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRM_CallRootMenu.Click
        Player.GoToMenu(DvdMenuID.Root, Not CurrentUserProfile.AppOptions.UseSeparateResumeButton)
    End Sub

    Private Sub btnRM_CallSubtitleMenu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRM_CallSubtitleMenu.Click
        Player.GoToMenu(DvdMenuID.Subpicture)
    End Sub

    Private Sub btnRM_CallAudioMenu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRM_CallAudioMenu.Click
        Player.GoToMenu(DvdMenuID.Audio)
    End Sub

    Private Sub btnRM_CallSceneMenu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRM_CallSceneMenu.Click
        Player.GoToMenu(DvdMenuID.Chapter)
    End Sub

    Private Sub btnRM_CallAngleMenu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRM_CallAngleMenu.Click
        Player.GoToMenu(DvdMenuID.Angle)
    End Sub

    Private Sub btnRM_CallTitleMenu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRM_CallTitleMenu.Click
        Player.GoToMenu(DvdMenuID.Title)
    End Sub

#End Region 'PANELS:REMOTE:MENU CALLS

#Region "PANELS:REMOTE:PLAY CONTROL"

    Private Sub btnRM_Play_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRM_Play.Click
        Player.Play()
    End Sub

    Private Sub btnRM_Pause_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRM_Pause.Click
        Player.Pause()
    End Sub

    Private Sub btnRM_Stop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRM_Stop.Click
        Player.Stop()
    End Sub

    Private Sub btnRM_ChapterBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRM_ChapterBack.Click
        Player.PreviousChapter()
    End Sub

    Private Sub btnRM_Rewind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRM_Rewind.Click
        Player.Rewind()
    End Sub

    Private Sub btnRM_FastForward_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRM_FastForward.Click
        Player.FastForward()
    End Sub

    Private Sub btnRM_ChapterNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRM_ChapterNext.Click
        Player.NextChapter()
    End Sub

    Private Sub btnResume_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnResume.Click
        Player.Resume()
    End Sub

#End Region 'PANELS:REMOTE:PLAY CONTROL

#Region "PANELS:REMOTE:AB LOOP"

    Private Sub btnABLoop_A_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnABLoop_A.Click
        AB_Set_A()
    End Sub

    Private Sub btnABLoop_B_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnABLoop_B.Click
        AB_Set_B()
    End Sub

    Private Sub AB_Clear()
        Player.ABLoop_Clear()
    End Sub

    Private Sub AB_Set_A()
        If Not Player.ABLoop_SetA() Then
            XtraMessageBox.Show(Me.LookAndFeel, Me, "Time search UOP(0) is prohibited. A-B loop is impossible.", My.Settings.APPLICATION_NAME, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Else
            Me.btnABLoop_A.ButtonStyle = BorderStyles.Office2003
            Me.btnABLoop_B.Enabled = True
        End If
    End Sub

    Private Sub AB_Set_B()
        Player.ABLoop_SetB(Me.ceABLoop_Grab.Checked, Me.CurrentUserProfile.AppOptions.FrameGrabTypeAsImageFormat)
        Me.btnABLoop_B.ButtonStyle = BorderStyles.Office2003
    End Sub

#End Region 'PANELS:REMOTE:AB LOOP

#Region "PANELS:REMOTE:CPU MONITOR"

    Public CPU As New PerformanceCounter
    Public LastCPUVal As Byte = 0
    Public CPU_HighVal_Cnt As Byte

    Public Sub NextCPUValue()
        Try
            Me.LastCPUVal = CPU.NextValue
            pbCPU.Position = LastCPUVal
            'Debug.WriteLine("CPU: " & LastCPUVal & "%")

            If Me.LastCPUVal > 80 Then
                Me.pbCPU.ForeColor = Color.Red
                Me.CPU_HighVal_Cnt = 0
                'If Me.LastCPUVal = 100 Then
                '    'Beep(333, 500)
                '    If Player IsNot Nothing AndAlso Player.PlayState <> ePlayState.SystemJP Then
                '        Player.ReSyncAudio(True)
                '    End If
                '    'MsgBox("CPU hit 100%. Automatic ReSync.", MsgBoxStyle.Critical Or MsgBoxStyle.OKOnly, "CPU Pegged")
                '    Me.AddConsoleLine(eConsoleItemType.ERROR, "CPU hit 100%. Automatic ReSync.", Nothing)
                'End If
            End If

            Me.CPU_HighVal_Cnt += 1
            If Me.CPU_HighVal_Cnt > 5 Then
                Me.pbCPU.ForeColor = Color.Blue
                Me.CPU_HighVal_Cnt = 0
            End If

        Catch ex As Exception
            Debug.WriteLine("Problem with NextCPUValue. Error: " & ex.Message)
        End Try
    End Sub

    Public Sub LaunchCPUMonitor()
        Try
            CPU.CategoryName = "Processor"
            CPU.CounterName = "% Processor Time"
            CPU.InstanceName = "_Total"
        Catch ex As Exception
            Me.AddConsoleLine(eConsoleItemType.ERROR, "Problem with LaunchCPUMonitor. Error: " & ex.Message, Nothing)
        End Try
    End Sub

#End Region 'PANELS:REMOTE:CPU MONITOR

#Region "PANELS:REMOTE:FRAME GRAB CONTEXT MENU"

    Private Sub cmFrameGrab_Popup(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmFrameGrabOptions.Opened
        Dim FGT As String = CurrentUserProfile.AppOptions.FrameGrabType.ToString
        Dim FGS As String = CurrentUserProfile.AppOptions.FrameGrabSource.ToString
        Dim mi As ToolStripMenuItem
        Try
            For Each item As ToolStripItem In Me.cmFrameGrabOptions.Items
                Select Case item.Text.ToLower
                    Case "full mix"
                        If Not FeatureManagement.Features.FE_VDP_FullMix Then
                            item.Visible = False
                            GoTo NextMI
                        End If
                    Case "video and subpicture"
                        If Not FeatureManagement.Features.FE_VDP_FullMix Then
                            item.Visible = False
                            GoTo NextMI
                        End If
                    Case "video only"
                        If Not FeatureManagement.Features.FE_VDP_VideoOnly Then
                            item.Visible = False
                            GoTo NextMI
                        End If
                    Case "subpicture only"
                        If Not FeatureManagement.Features.FE_SP_Dumping Then
                            item.Visible = False
                            GoTo NextMI
                        End If
                    Case "closed caption only"
                        If Not FeatureManagement.Features.FE_L21_BitmapExtraction Then
                            item.Visible = False
                            GoTo NextMI
                        End If
                    Case "multiframe"
                        If Not FeatureManagement.Features.FE_VDP_MultiFrame Then
                            item.Visible = False
                            GoTo NextMI
                        End If
                    Case ""
                        GoTo NextMI
                End Select

                Try
                    mi = CType(item, ToolStripMenuItem)
                Catch ex As Exception
                    GoTo NextMI
                End Try

                mi.Checked = False
                If mi.Text = FGT Then
                    mi.Checked = True
                    GoTo NextMI
                End If
                If mi.Text = Replace(FGS, "_", " ") Then
                    mi.Checked = True
                    GoTo NextMI
                End If
NextMI:
            Next
        Catch ex As Exception
            Debug.WriteLine("Problem with cmFrameGrab_Popup(). Error: " & ex.Message)
        End Try
    End Sub

#Region "PANELS:REMOTE:FRAME GRAB CONTEXT MENU:FRAME GRAB SOURCE"

    Private Sub miFrameGrabContent_Video_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miFGV_VideoOnly.Click
        Me.miFGV_VideoOnly.Checked = True
        Me.miFGV_FullMix.Checked = False
        Me.miFGV_VideoAndSubpicture.Checked = False
        Me.miFGV_ClosedCaptionsOnly.Checked = False
        Me.miFGV_SubpictureOnly.Checked = False
        Me.miFGV_MultiFrame.Checked = False
        CurrentUserProfile.AppOptions.FrameGrabSource = eFrameGrabContent.Video_Only
    End Sub

    Private Sub miFrameGrabContent_VideoAndSubs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miFGV_VideoAndSubpicture.Click
        Me.miFGV_VideoOnly.Checked = False
        Me.miFGV_FullMix.Checked = False
        Me.miFGV_VideoAndSubpicture.Checked = True
        Me.miFGV_ClosedCaptionsOnly.Checked = False
        Me.miFGV_SubpictureOnly.Checked = False
        Me.miFGV_MultiFrame.Checked = False
        CurrentUserProfile.AppOptions.FrameGrabSource = eFrameGrabContent.Video_and_Subpicture
    End Sub

    Private Sub miFrameGrabContent_FullMix_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miFGV_FullMix.Click
        Me.miFGV_VideoOnly.Checked = False
        Me.miFGV_FullMix.Checked = True
        Me.miFGV_VideoAndSubpicture.Checked = False
        Me.miFGV_ClosedCaptionsOnly.Checked = False
        Me.miFGV_SubpictureOnly.Checked = False
        Me.miFGV_MultiFrame.Checked = False
        CurrentUserProfile.AppOptions.FrameGrabSource = eFrameGrabContent.Full_Mix
    End Sub

    Private Sub miFrameGrabContent_SubOnly_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miFGV_SubpictureOnly.Click
        Me.miFGV_VideoOnly.Checked = False
        Me.miFGV_FullMix.Checked = False
        Me.miFGV_VideoAndSubpicture.Checked = False
        Me.miFGV_ClosedCaptionsOnly.Checked = False
        Me.miFGV_SubpictureOnly.Checked = True
        Me.miFGV_MultiFrame.Checked = False
        CurrentUserProfile.AppOptions.FrameGrabSource = eFrameGrabContent.Subpicture_Only
    End Sub

    Private Sub miFrameGrabContent_L21Only_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miFGV_ClosedCaptionsOnly.Click
        Me.miFGV_VideoOnly.Checked = False
        Me.miFGV_FullMix.Checked = False
        Me.miFGV_VideoAndSubpicture.Checked = False
        Me.miFGV_ClosedCaptionsOnly.Checked = True
        Me.miFGV_SubpictureOnly.Checked = False
        Me.miFGV_MultiFrame.Checked = False
        CurrentUserProfile.AppOptions.FrameGrabSource = eFrameGrabContent.Closed_Caption_Only
    End Sub

    Private Sub miMultiFrameGrab_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miFGV_MultiFrame.Click
        Me.miFGV_VideoOnly.Checked = False
        Me.miFGV_FullMix.Checked = False
        Me.miFGV_VideoAndSubpicture.Checked = False
        Me.miFGV_ClosedCaptionsOnly.Checked = False
        Me.miFGV_SubpictureOnly.Checked = False
        Me.miFGV_MultiFrame.Checked = True
        CurrentUserProfile.AppOptions.FrameGrabSource = eFrameGrabContent.MultiFrame
    End Sub

#End Region 'PANELS:REMOTE:FRAME GRAB CONTEXT MENU:FRAME GRAB SOURCE

#Region "PANELS:REMOTE:FRAME GRAB CONTEXT MENU:FRAME GRAB TYPE"

    Private Sub miFGType_BM_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miFGV_Bitmap.Click
        UncheckAllImageTypes()
        CurrentUserProfile.AppOptions.FrameGrabType = eFrameGrabType.BMP
        Me.CheckImageType("Bitmap")
    End Sub

    Private Sub miFGType_Jpeg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miFGV_JPEG.Click
        UncheckAllImageTypes()
        CurrentUserProfile.AppOptions.FrameGrabType = eFrameGrabType.JPEG
        Me.CheckImageType("Jpeg")
    End Sub

    Private Sub miFGType_Gif_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miFGV_GIF.Click
        UncheckAllImageTypes()
        CurrentUserProfile.AppOptions.FrameGrabType = eFrameGrabType.GIF
        Me.CheckImageType("Gif")
    End Sub

    Private Sub miFGType_Png_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miFGV_PNG.Click
        UncheckAllImageTypes()
        CurrentUserProfile.AppOptions.FrameGrabType = eFrameGrabType.PNG
        Me.CheckImageType("Png")
    End Sub

    Private Sub miFGType_Tif_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miFGV_TIF.Click
        UncheckAllImageTypes()
        CurrentUserProfile.AppOptions.FrameGrabType = eFrameGrabType.TIF
        Me.CheckImageType("Tif")
    End Sub

    Public Sub CheckImageType(ByVal ImageType As String)
        For Each mi As ToolStripItem In Me.cmFrameGrabOptions.Items
            If mi.GetType.Name = "ToolStripMenuItem" Then
                If mi.Text = ImageType Then
                    CType(mi, ToolStripMenuItem).Checked = True
                    Exit Sub
                End If
            End If
        Next
    End Sub

    Public Sub UncheckAllImageTypes()
        For Each mi As ToolStripItem In Me.cmFrameGrabOptions.Items
            If mi.GetType.Name = "ToolStripMenuItem" Then
                If mi.Text = "Bitmap" Or mi.Text = "Jpeg" Or mi.Text = "Gif" Or mi.Text = "Tif" Or mi.Text = "Png" Then
                    CType(mi, ToolStripMenuItem).Checked = False
                End If
            End If
        Next
    End Sub

    Private Sub mi_FGV_OpenDir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mi_FGV_OpenDir.Click
        Dim t As String = Path.GetDirectoryName(Player.FrameGrabDumpDir)
        If Not Directory.Exists(t) Then Exit Sub
        Process.Start("C:\WINDOWS\explorer.exe", "/n,/e," & t)
    End Sub

#End Region 'PANELS:REMOTE:FRAME GRAB CONTEXT MENU:FRAME GRAB TYPE

#End Region 'PANELS:REMOTE:FRAME GRAB CONTEXT MENU

#Region "PANELS:REMOTE:EVENTS"

    Private Sub btnRM_GoUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRM_GoUp.Click
        Player.GoUp()
    End Sub

    Private Sub btnRM_Resume_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRM_Resume.Click
        Player.[Resume]()
    End Sub

    Private Sub btnRM_FrameStep_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRM_FrameStep.Click
        Player.FrameStep()
    End Sub

    Private Sub btnRM_JumpBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRM_JumpBack.Click
        Player.JumpBack(CurrentUserProfile.AppOptions.JumpSeconds)
    End Sub

    Private Sub btnRM_EndOfTT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRM_EndOfTT.Click
        Player.JumpToEndOfTitle()
    End Sub

    Private Sub btnRM_SlowPlayback_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRM_SlowPlayback.Click
        Player.SlowForward()
    End Sub

    Private Sub btnRM_FrameGrab_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRM_FrameGrab.Click
        If CurrentUserProfile.AppOptions.FrameGrabSource = eFrameGrabContent.MultiFrame Then
            Player.MultiFrameGrab(CurrentUserProfile.AppOptions.MultiFrameCount, Me.CurrentUserProfile.AppOptions.FrameGrabTypeAsImageFormat)
        Else
            Player.GrabScreen(Me.CurrentUserProfile.AppOptions.FrameGrabTypeAsImageFormat, CurrentUserProfile.AppOptions.FrameGrabSource, True)
        End If
    End Sub

    Private Sub btnToggleSubs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRM_ToggleSubtitles.Click
        If Player.AreSubtitlesOn Then
            Player.ToggleSubtitles(False)
        Else
            Player.ToggleSubtitles(True)
        End If
    End Sub

    Private Sub btnRM_ReSync_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRM_ReSync.Click
        Player.ReSyncAudio(False)
    End Sub

    Private Sub btnRM_ToggleClosedCaptions_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRM_ToggleClosedCaptions.Click
        Try
            If Not FeatureManagement.Features.FE_L21_Decode Then Exit Sub
            If Not Player.ToggleClosedCaptions() Then Throw New Exception("Problem with UserToggleCCs in btnCCs_Click.")
        Catch ex As Exception
            AddConsoleLine(eConsoleItemType.ERROR, "problem changing CC display status. error: " & ex.Message)
        End Try
    End Sub

    Private Sub btnKeyboardHook_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKeyboardHook.Click
        Me.ToggleKeyboardHook()
    End Sub

    Private Sub dpRemote_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles dpRemote.Resize
        Me.peSMTLogo.Top = Me.dpRemote.Height - 91
    End Sub

#End Region 'PANELS:REMOTE:EVENTS

#Region "PANELS:REMOTE:FUNCTIONALITY"

    Public Sub SetRemoteUOPs(ByVal UOPs() As Boolean, ByVal Sender As String)
        Try
            If Player.IsEjecting Or Player.Disposed Then Exit Sub

            If Player.PlayState = ePlayState.SystemJP Or Player.PlayState = ePlayState.ColorBars Then
                Dim tUOPs(24) As Boolean
                UOPs = tUOPs
            End If

            '0,1,2 prevent search function
            If Player.CurrentDomain = DvdDomain.Title Then
                Me.gbABLoop.Enabled = UOPs(0)
            Else
                Me.gbABLoop.Enabled = False
            End If

            Me.btnSPB_Stop.Enabled = UOPs(3)

            'Me.btnRM_GoUp.Enabled = UOPs(4) 'see below

            tbPlayPosition.Enabled = UOPs(5)

            Me.btnRM_ChapterBack.Enabled = UOPs(6)
            Me.btnRM_ChapterNext.Enabled = UOPs(7)
            Me.btnRM_FastForward.Enabled = UOPs(8)
            Me.btnRM_Rewind.Enabled = UOPs(9)

            Me.btnRM_CallTitleMenu.Enabled = UOPs(10)
            Me.btnRM_CallRootMenu.Enabled = UOPs(11)
            Me.btnRM_CallSubtitleMenu.Enabled = UOPs(12)
            Me.btnRM_CallAudioMenu.Enabled = UOPs(13)
            Me.btnRM_CallAngleMenu.Enabled = UOPs(14)
            Me.btnRM_CallSceneMenu.Enabled = UOPs(15)

            'If Me.lblLBTT.Text = "M" Then
            '    Me.btnRM_Resume.Enabled = UOPs(16)
            'Else
            '    Me.btnRM_Resume.Enabled = False
            'End If

            If Player.PlayState = ePlayState.SystemJP Then
                Me.btnRM_Resume.Enabled = False
                Me.btnRM_GoUp.Enabled = False

            ElseIf Player.CurrentDomain = DvdDomain.VideoManagerMenu Or Player.CurrentDomain = DvdDomain.VideoTitleSetMenu Then
                Me.btnRM_Resume.Enabled = UOPs(16)
                Me.btnRM_GoUp.Enabled = UOPs(4)

            ElseIf Player.CurrentDomain = DvdDomain.Title And Player.PlayState = ePlayState.Stopped Then
                Me.btnRM_Resume.Enabled = UOPs(16)
                Me.btnRM_GoUp.Enabled = UOPs(4)

            ElseIf Player.CurrentDomain = DvdDomain.Title And Not Player.PlayState = ePlayState.Stopped Then
                Me.btnRM_Resume.Enabled = False
                Me.btnRM_GoUp.Enabled = UOPs(4)

            ElseIf Player.CurrentDomain = DvdDomain.Stop Then
                Me.btnRM_Resume.Enabled = UOPs(16)
                Me.btnRM_GoUp.Enabled = False

            Else
                Me.btnRM_Resume.Enabled = UOPs(16)
                Me.btnRM_GoUp.Enabled = UOPs(4)

            End If

            Me.btnRM_Enter.Enabled = UOPs(17)
            Me.btnRM_Up.Enabled = UOPs(17)
            Me.btnRM_Down.Enabled = UOPs(17)
            Me.btnRM_Right.Enabled = UOPs(17)
            Me.btnRM_Left.Enabled = UOPs(17)

            If Me.btnRM_Play.Text = "Play" Then 'Currently Paused/Stopped
                Me.btnRM_Play.Enabled = UOPs(0)
            Else 'Currently Playing & text = "Pause"
                Me.btnRM_Play.Enabled = UOPs(19)
            End If

            Me.btnRM_Pause.Enabled = UOPs(19)

            Me.btnRM_Stop.Enabled = UOPs(3) 'is this correct? added 070420

            'If Player.CurrentDomain = DvdDomain.Stop Then
            '    Me.btnRM_Stop.Enabled = True
            '    Me.btnRM_Play.Enabled = True
            'End If

            Me.btnRM_CycleAudio.Enabled = UOPs(20)
            Me.btnRM_CycleSubtitles.Enabled = UOPs(21)
            Me.btnRM_ToggleSubtitles.Enabled = UOPs(21)

            If lbAvailAngles.Items.Count < 2 Then
                btnRM_CycleAngle.Enabled = False
            Else
                Me.btnRM_CycleAngle.Enabled = UOPs(22)
            End If


        Catch ex As Exception
            AddConsoleLine(eConsoleItemType.ERROR, "Problem with SetRemoteUOPs. Error: " & ex.Message)
        End Try
    End Sub

    Private Sub ToggleRemote(ByVal Enable As Boolean)
        Try
            Me.gbRM_Menus.Enabled = Enable
            Me.gbRM_Misc.Enabled = Enable
            Me.gbRM_PrimaryControls.Enabled = Enable
            Me.gbABLoop.Enabled = Enable
            Me.btnRM_ToggleClosedCaptions.ButtonStyle = BorderStyles.Default
            Me.btnRM_ToggleSubtitles.ButtonStyle = BorderStyles.Default
        Catch ex As Exception
            Me.AddConsoleLine(eConsoleItemType.ERROR, "Problem with ToggleRemote(). Error: " & ex.Message)
        End Try
    End Sub

#End Region 'PANELS:REMOTE:FUNCTIONALITY

#End Region 'PANELS:REMOTE

#Region "PANELS:DASHBOARD"

#Region "PANELS:DASHBOARD:TIMECODE CONTEXT MENU"

    Public CurrentTimecodeDisplayMode As String = "TTe"

    Private Sub miTimecodeView_TitleElapsed_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miTimecodeView_TitleElapsed.Click
        Me.miTimecodeView_TitleElapsed.Checked = True
        Me.miTimecodeView_TitleRemaining.Checked = False
        Me.CurrentTimecodeDisplayMode = "TTe"
    End Sub

    Private Sub miTimecodeView_TitleRemaining_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miTimecodeView_TitleRemaining.Click
        Me.miTimecodeView_TitleElapsed.Checked = False
        Me.miTimecodeView_TitleRemaining.Checked = True
        Me.CurrentTimecodeDisplayMode = "TTr"
    End Sub

#End Region 'PANELS:DASHBOARD:TIMECODE CONTEXT MENU

#Region "PANELS:DASHBOARD *FUNCTIONALITY*"

    Public Sub ClearDashboard(ByVal DomainChange As Boolean, ByVal FullReset As Boolean)
        Try
            If Not DomainChange Then lblProjectPath.Text = ""

            lblTotalRunningTime.Text = "0:00:00"
            lblCurrentRunningTime.Text = "0:00:00"
            lblCurrent_angle.Text = ""
            lblAnglesTotal.Text = ""
            lblCurrent_PTT.Text = ""
            lblPTTsTotal.Text = ""
            lblEXDB_CurrentTitle.Text = ""
            lblEXDB_TitleCount.Text = ""
            lblEXDB_CurrentTitle.Text = ""
            lblEXDB_TitleCount.Text = ""
            Me.lblDB_ChapterCount.Text = ""
            Me.lblDB_CurrentChapter.Text = ""
            Me.lblDB_CurrentTitle.Text = ""
            Me.lblDB_TitleCount.Text = ""
            lblVTS.Text = ""
            lblVTSsTotal.Text = ""
            lblVTSTT.Text = ""
            lblVTSTTsTotal.Text = ""
            lblPGC.Text = ""
            lblPGCsTotal.Text = ""
            lblProgramCur.Text = ""
            lblProgramsTotal.Text = ""
            lblCell.Text = ""
            lblCellsTotal.Text = ""
            lblCurrentSourceTimecode.Text = "0:00:00"
            lblCellStillTime.Text = ""

            'Current Subtitle
            lblCurrentSP_sn.Text = ""
            lblCurrentSP_lang.Text = ""
            lblCurrentSP_onoff.Text = ""
            lblCurrentSP_ext.Text = ""

            'Current Audio
            lblCurrentAudACMOD.Text = ""
            lblCurrentAud_Bitrate.Text = ""
            lblCurrentAudio_ext.Text = ""
            lblCurrentAudio_format.Text = ""
            lblCurrentAudio_lang.Text = ""
            lblCurrentAudio_sn.Text = ""

            'Volume Info
            If Not DomainChange Then
                lblVolumes.Text = ""
                lblPublisher.Text = ""
                lblVolumeID.Text = ""
                lblCurrentVol.Text = ""
                lblDVDText.Text = ""
                lblDiscSide.Text = ""
                lblCurrentDomain.Text = ""
                ToolTip.SetToolTip(lblPublisher, "")
                ToolTip.SetToolTip(lblDVDText, "")
            End If

            'Video Tab
            lblVidAtt_aspectratio.Text = ""
            lblVidAtt_compression.Text = ""
            lblVidAtt_frameheight.Text = ""
            lblVidAtt_framerate.Text = ""
            lblVidAtt_isFilmMode.Text = ""
            lblVidAtt_VidStd.Text = ""
            lblVidAtt_isSourceLetterboxed.Text = ""
            lblVidAtt_lbok.Text = ""
            lblVidAtt_line21Field1InGOP.Text = ""
            lblVidAtt_line21Field2InGOP.Text = ""
            lblVidAtt_psok.Text = ""
            lblVidAtt_sourceResolution.Text = ""
            'lblCurrentlyRunning32.Text = ""
            lblCurrentFrameRate.Text = ""
            lblCSSEncrypted.Text = ""
            lblMacrovision.Text = ""
            lbl32WhichField.Text = ""
            lblCurrentVideoIsInterlaced.Text = ""
            lbAvailAngles.Items.Clear()
            lblByteRate_Avg.Text = ""
            lblByteRate_Cur.Text = ""
            lblByteRate_High.Text = ""
            lblByteRate_Low.Text = ""
            ClearGraph()

            'Audio Tab
            Dim Audios As New seqAudioStreams
            dgAudioStreams.DataSource = Audios.AudioStreams

            'Subtitle Tab
            Dim TempSubs(-1) As seqSub
            dgSubtitles.DataSource = TempSubs

            'Volume Tab
            If Not DomainChange Then
                lbJacketPictures.Items.Clear()
                lbAvailableMenuLanguages.Items.Clear()
                lbParentalManagement.Items.Clear()
            End If

            'Params
            ClearPRMs()

            'UOPs
            ResetUOPs()

            'Remote
            lblLBTC.Text = ""
            lblLBCh.Text = ""
            lblLBTT.Text = ""

            'If Not DomainChange Then
            '    cbChapters.Items.Clear()
            'End If

            If FullReset Then
                ResetRegions()
                'cbTitles.Items.Clear()
                btnRM_Stop.BackColor = SystemColors.Control
                btnRM_Play.BackColor = SystemColors.Control
                btnRM_Pause.BackColor = SystemColors.Control
                btnRM_FastForward.BackColor = SystemColors.Control
                btnRM_Rewind.BackColor = SystemColors.Control
            End If

        Catch ex As Exception
            AddConsoleLine(eConsoleItemType.ERROR, "Problem with ClearDashboard. Error: " & ex.Message & " StackTrace: " & ex.StackTrace)
        End Try
    End Sub

    Sub EnableAllPositionLablesOnDB()
        Me.lblPGC.Enabled = True
        Me.lblPGCsTotal.Enabled = True
        Me.lblVTS.Enabled = True
        Me.lblVTSsTotal.Enabled = True
        Me.lblVTSTT.Enabled = True
        Me.lblVTSTTsTotal.Enabled = True
        Me.lblProgramCur.Enabled = True
        Me.lblProgramsTotal.Enabled = True
        Me.lblCell.Enabled = True
        Me.lblCellsTotal.Enabled = True
        Me.lblEXDB_TitleCount.Enabled = True
        Me.lblEXDB_CurrentTitle.Enabled = True
        Me.lblAnglesTotal.Enabled = True
        Me.lblCurrent_angle.Enabled = True
        Me.lblCurrent_PTT.Enabled = True
        Me.lblPTTsTotal.Enabled = True
        Me.lblEXDB_TitleCount.Enabled = True
        Me.lblEXDB_CurrentTitle.Enabled = True
        Me.lblDB_TitleCount.Enabled = True
        Me.lblDB_CurrentTitle.Enabled = True
    End Sub

    Sub DisablePositionLablesOnDBForMenuSpace()
        Me.lblVTSTT.Text = ""
        Me.lblVTSTTsTotal.Text = ""

        Me.lblEXDB_TitleCount.Text = ""
        Me.lblEXDB_CurrentTitle.Text = ""

        Me.lblDB_TitleCount.Text = ""
        Me.lblDB_CurrentTitle.Text = ""

        Me.lblAnglesTotal.Text = ""
        Me.lblCurrent_angle.Text = ""

        Me.lblCurrent_PTT.Text = ""
        Me.lblPTTsTotal.Text = ""

        Me.lblDB_CurrentChapter.Text = ""
        Me.lblDB_ChapterCount.Text = ""
    End Sub

    Private Sub UpdateDashboard()
        Try
            If CurrentTimecodeDisplayMode Is Nothing Then Exit Sub
            If Player.CurrentTitleDuration Is Nothing Then GoTo SkipTCSetup

            Select Case CurrentTimecodeDisplayMode
                Case "TTe" 'Title elapsed
                    Dim M, S, F As String

                    'Total Duration
                    If Player.CurrentTitleDuration.TotalTime.bMinutes.ToString.Length = 1 Then
                        M = 0 & Player.CurrentTitleDuration.TotalTime.bMinutes
                    Else
                        M = Player.CurrentTitleDuration.TotalTime.bMinutes
                    End If

                    If Player.CurrentTitleDuration.TotalTime.bSeconds.ToString.Length = 1 Then
                        S = 0 & Player.CurrentTitleDuration.TotalTime.bSeconds
                    Else
                        S = Player.CurrentTitleDuration.TotalTime.bSeconds
                    End If

                    'If Player.CurrentTitleDuration.TotalTime.bFrames.ToString.Length = 1 Then
                    '    F = 0 & Player.CurrentTitleDuration.TotalTime.bFrames
                    'Else
                    '    F = Player.CurrentTitleDuration.TotalTime.bFrames
                    'End If
                    'Debug.WriteLine("frames = " & F)

                    If Not Player.PlayState = ePlayState.SystemJP Then
                        Me.lblTotalRunningTime.Text = Player.CurrentTitleDuration.TotalTime.bHours & ":" & M & ":" & S '& ";" & F
                    End If

                    'Current Timecode
                    If Player.CurrentRunningTime_DVD.bMinutes.ToString.Length = 1 Then
                        M = 0 & Player.CurrentRunningTime_DVD.bMinutes
                    Else
                        M = Player.CurrentRunningTime_DVD.bMinutes
                    End If

                    If Player.CurrentRunningTime_DVD.bSeconds.ToString.Length = 1 Then
                        S = 0 & Player.CurrentRunningTime_DVD.bSeconds
                    Else
                        S = Player.CurrentRunningTime_DVD.bSeconds
                    End If

                    'If Player.CurrentRunningTime.bFrames.ToString.Length = 1 Then
                    '    F = 0 & Player.CurrentRunningTime.bFrames
                    'Else
                    '    F = Player.CurrentRunningTime.bFrames
                    'End If

                    Dim ti As String = Microsoft.VisualBasic.Right(Player.CurrentRunningTime_DVD.bHours.ToString, 1) & ":" & M & ":" & S '& ";" & F

                    If Player.PlayState = ePlayState.Paused Then
                        'ti = "Paused"
                    ElseIf Player.PlayState = ePlayState.Stopped Then
                        ti = "Stopped"
                    End If

                    lblCurrentRunningTime.Text = ti
                    lblTCDivider.Text = "/"

                Case "TTr" 'Title remaining
                    'CurrentTitleDuration - Player.CurrentRunningTime
                    Dim cur As New cTimecode(Player.CurrentRunningTime_DVD, (Player.CurrentVideoStandard = eVideoStandard.NTSC))
                    Dim tot As New cTimecode(Player.CurrentTitleDuration.TotalTime, (Player.CurrentVideoStandard = eVideoStandard.NTSC))
                    Dim rema As cTimecode = SubtractTimecode(tot, cur, Player.CurrentTargetFrameRate)
                    lblCurrentRunningTime.Text = "Rem: "
                    lblTotalRunningTime.Text = rema.ToString_NoFrames
                    lblTCDivider.Text = ""

                Case "CHe" 'Chapter elapased
                Case "CHr" 'Chapter remaining

            End Select
SkipTCSetup:
        Catch ex As Exception
            AddConsoleLine(eConsoleItemType.ERROR, "Problem with UpdateDashboard(). Error: " & ex.Message)
        End Try
    End Sub

    Public Sub SetupMenuLanguages()
        Try
            lbAvailableMenuLanguages.Items.Clear()
            Dim s() As String = Player.MenuLanguages_Strings.ToArray
            For i As Short = UBound(s) To 0 Step -1
                If Not s(i) = "BAD" Then lbAvailableMenuLanguages.Items.Add(s(i))
                'Debug.WriteLine(s)
            Next
        Catch ex As Exception
            'ignore
        End Try
    End Sub

#End Region 'PANELS:DASHBOARD *FUNCTIONALITY*

#End Region 'PANELS:DASHBOARD

#Region "PANELS:VOLUME INFO"

#Region "PANELS:VOLUME INFO *FUNCTIONALITY*"

    Public Sub ResetRegions()
        Reg_1.BackColor = GetBoolColor(True)
        Reg_2.BackColor = GetBoolColor(True)
        Reg_3.BackColor = GetBoolColor(True)
        Reg_4.BackColor = GetBoolColor(True)
        Reg_5.BackColor = GetBoolColor(True)
        Reg_6.BackColor = GetBoolColor(True)
        Reg_7.BackColor = GetBoolColor(True)
        Reg_8.BackColor = GetBoolColor(True)
    End Sub

#End Region 'PANELS:VOLUME INFO *FUNCTIONALITY*

#Region "PANELS:VOLUME INFO:EVENTS"

    Private Sub lbJacketPictures_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbJacketPictures.SelectedIndexChanged
        Try
            If Not Player.PlayState = ePlayState.Stopped And Not Player.PlayState = ePlayState.Init And Not Player.PlayState = ePlayState.ProjectJP Then
                AddConsoleLine(eConsoleItemType.WARNING, "Playback must be stopped for JP Preview.")
                Exit Sub
            End If
            Dim JPName As String = CType(lbJacketPictures.SelectedItem, cJacketPicture).Path
            Player.ShowJacketPicture(JPName)
        Catch ex As Exception
            AddConsoleLine(eConsoleItemType.ERROR, "Problem selecting jacket picture. Error: " & ex.Message)
        End Try
    End Sub

#End Region 'PANELS:VOLUME INFO:EVENTS

#End Region 'PANELS:VOLUME INFO

#Region "PANELS:UOPs"

#Region "PANELS:UOPs *FUNCTIONALITY*"

    Public Property CheckBoxMode() As Boolean
        Get
            Return _CheckBoxMode
        End Get
        Set(ByVal Value As Boolean)
            _CheckBoxMode = Value
            If Value Then
                Me.pnlUO_CheckBoxes.BringToFront()
            Else
                Me.pnlUO_ColorBoxes.BringToFront()
            End If
        End Set
    End Property
    Private _CheckBoxMode As Boolean = False

    Private Sub ResetUOPs()
        ReDim _CachedUOPs(24)
        Me.UpdateUserOperations(True)
    End Sub

    Public Sub UpdateUserOperations(ByVal Reset As Boolean)
        Dim tempUOPs(24) As Boolean

        If Not Reset Then
            Player.UpdateUOPs()
            tempUOPs = Player.CurrentUserOperations
        End If

        cbUO_0.Checked = tempUOPs(0)
        cbUO_1.Checked = tempUOPs(1)
        cbUO_2.Checked = tempUOPs(2)
        cbUO_3.Checked = tempUOPs(3)
        cbUO_4.Checked = tempUOPs(4)
        cbUO_5.Checked = tempUOPs(5)
        cbUO_6.Checked = tempUOPs(6)
        cbUO_7.Checked = tempUOPs(7)
        cbUO_8.Checked = tempUOPs(8)
        cbUO_9.Checked = tempUOPs(9)
        cbUO_10.Checked = tempUOPs(10)
        cbUO_11.Checked = tempUOPs(11)
        cbUO_12.Checked = tempUOPs(12)
        cbUO_13.Checked = tempUOPs(13)
        cbUO_14.Checked = tempUOPs(14)
        cbUO_15.Checked = tempUOPs(15)
        cbUO_16.Checked = tempUOPs(16)
        cbUO_17.Checked = tempUOPs(17)
        cbUO_18.Checked = tempUOPs(18)
        cbUO_19.Checked = tempUOPs(19)
        cbUO_20.Checked = tempUOPs(20)
        cbUO_21.Checked = tempUOPs(21)
        cbUO_22.Checked = tempUOPs(22)
        cbUO_23.Checked = tempUOPs(23)
        cbUO_24.Checked = tempUOPs(24)

        uop_0.BackColor = GetBoolColor(tempUOPs(0))
        uop_1.BackColor = GetBoolColor(tempUOPs(1))
        uop_2.BackColor = GetBoolColor(tempUOPs(2))
        uop_3.BackColor = GetBoolColor(tempUOPs(3))
        uop_4.BackColor = GetBoolColor(tempUOPs(4))
        uop_5.BackColor = GetBoolColor(tempUOPs(5))
        uop_6.BackColor = GetBoolColor(tempUOPs(6))
        uop_7.BackColor = GetBoolColor(tempUOPs(7))
        uop_8.BackColor = GetBoolColor(tempUOPs(8))
        uop_9.BackColor = GetBoolColor(tempUOPs(9))
        uop_10.BackColor = GetBoolColor(tempUOPs(10))
        uop_11.BackColor = GetBoolColor(tempUOPs(11))
        uop_12.BackColor = GetBoolColor(tempUOPs(12))
        uop_13.BackColor = GetBoolColor(tempUOPs(13))
        uop_14.BackColor = GetBoolColor(tempUOPs(14))
        uop_15.BackColor = GetBoolColor(tempUOPs(15))
        uop_16.BackColor = GetBoolColor(tempUOPs(16))
        uop_17.BackColor = GetBoolColor(tempUOPs(17))
        uop_18.BackColor = GetBoolColor(tempUOPs(18))
        uop_19.BackColor = GetBoolColor(tempUOPs(19))
        uop_20.BackColor = GetBoolColor(tempUOPs(20))
        uop_21.BackColor = GetBoolColor(tempUOPs(21))
        uop_22.BackColor = GetBoolColor(tempUOPs(22))
        uop_23.BackColor = GetBoolColor(tempUOPs(23))
        uop_24.BackColor = GetBoolColor(tempUOPs(24))

        SetRemoteUOPs(tempUOPs, "HandleUOPChange")

        'now look for changes
        If Me._CachedUOPs Is Nothing Then Exit Sub
        lvConsole.SuspendLayout()
        For i As Byte = 0 To 24
            If tempUOPs(i) <> _CachedUOPs(i) Then
                'UOP CHANGED!!
                AddConsoleLine(eConsoleItemType.UOPCHANGE, CType(i, eUOPs).ToString, tempUOPs(i).ToString)
            End If
        Next
        lvConsole.ResumeLayout()
        _CachedUOPs = tempUOPs
    End Sub
    Private _CachedUOPs() As Boolean

    Public Sub UOPTemplate_ApplyCurrent()
        Try
            Dim l As Label
            If Not Me.UserOperationTemplates Is Nothing AndAlso Not Me.UserOperationTemplates.CurrentIndex = -1 Then
                For i As Byte = 0 To 24
                    If _CachedUOPs(i) <> Me.UserOperationTemplates.Templates(Me.UserOperationTemplates.CurrentIndex).UOPs.GetUOPValue(i) Then
                        l = CType(FindControl("lblUOP_" & i, Me.dpUOPs), Label)
                        l.ForeColor = Color.DarkRed
                        l.Font = New Font(l.Font, FontStyle.Bold)
                    Else
                        l = CType(FindControl("lblUOP_" & i, Me.dpUOPs), Label)
                        l.ForeColor = Color.Black
                        l.Font = New Font(l.Font, FontStyle.Regular)
                    End If
                Next
            Else
                For i As Byte = 0 To 24
                    l = CType(FindControl("lblUOP_" & i, Me.dpUOPs), Label)
                    l.ForeColor = Color.Black
                    l.Font = New Font(l.Font, FontStyle.Regular)
                Next
            End If
        Catch ex As Exception
            Me.AddConsoleLine(eConsoleItemType.ERROR, "Problem with UOPTemplate_ApplyCurrent(). Error: " & ex.Message)
        End Try
    End Sub

    Private Function GetUOPBackgroundColorForTemplateMismatch(ByVal IsOnShouldBeOff As Boolean) As Bitmap
        Try
            Dim out As New Bitmap(16, 16, Imaging.PixelFormat.Format16bppRgb555)
            For x As Byte = 0 To 15
                For y As Byte = 0 To 7
                    If IsOnShouldBeOff Then
                        out.SetPixel(x, y, Color.Red)
                    Else
                        out.SetPixel(x, y, Color.Green)
                    End If
                Next
            Next
            For x As Byte = 0 To 15
                For y As Byte = 8 To 15
                    If IsOnShouldBeOff Then
                        out.SetPixel(x, y, Color.Green)
                    Else
                        out.SetPixel(x, y, Color.Red)
                    End If
                Next
            Next

            'For x As Byte = 0 To 7
            '    For y As Byte = 0 To 7
            '        If IsOnShouldBeOff Then
            '            out.SetPixel(x, y, Color.Red)
            '        Else
            '            out.SetPixel(x, y, Color.Green)
            '        End If
            '    Next
            'Next
            'For x As Byte = 0 To 7
            '    For y As Byte = 8 To 15
            '        If IsOnShouldBeOff Then
            '            out.SetPixel(x, y, Color.Green)
            '        Else
            '            out.SetPixel(x, y, Color.Red)
            '        End If
            '    Next
            'Next
            'For x As Byte = 7 To 15
            '    For y As Byte = 0 To 15
            '        out.SetPixel(x, y, Color.Yellow)
            '    Next
            'Next
            Return out
        Catch ex As Exception
            Me.AddConsoleLine(eConsoleItemType.ERROR, "Problem with GetUOPBackgroundColorForTemplateMismatch(). Error: " & ex.Message)
            Return Nothing
        End Try
    End Function

#End Region 'PANELS:UOPs *FUNCTIONALITY*

#Region "PANELS:UOPs:EVENTS"

    Private Sub btnManualUOP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.UpdateUserOperations(False)
    End Sub

#End Region 'PANELS:UOPs:EVENTS

#End Region 'PANELS:UOPs

#Region "PANELS:GPRMs/SPRMs"

#Region "PANELS:GPRMs/SPRMs *FUNCTIONALITY*"

    Public Sub GetRegisters()
        'GPRMs
        If Player Is Nothing Then Exit Sub
        If Me.EmulatorIsInSetup Then Exit Sub
        UpdateGPRMs(Player.CurrentGPRMs)
        UpdateSPRMs(Player.CurrentSPRMs)
    End Sub

    Private Sub UpdateGPRMs(ByVal GPRMArray() As Integer)
        Try
            lblGPRM_0.Text = GetRegVal(GPRMArray(0), False)
            lblGPRM_1.Text = GetRegVal(GPRMArray(0), True)
            lblGPRM_2.Text = GetRegVal(GPRMArray(1), False)
            lblGPRM_3.Text = GetRegVal(GPRMArray(1), True)
            lblGPRM_4.Text = GetRegVal(GPRMArray(2), False)
            lblGPRM_5.Text = GetRegVal(GPRMArray(2), True)
            lblGPRM_6.Text = GetRegVal(GPRMArray(3), False)
            lblGPRM_7.Text = GetRegVal(GPRMArray(3), True)
            lblGPRM_8.Text = GetRegVal(GPRMArray(4), False)
            lblGPRM_9.Text = GetRegVal(GPRMArray(4), True)
            lblGPRM_10.Text = GetRegVal(GPRMArray(5), False)
            lblGPRM_11.Text = GetRegVal(GPRMArray(5), True)
            lblGPRM_12.Text = GetRegVal(GPRMArray(6), False)
            lblGPRM_13.Text = GetRegVal(GPRMArray(6), True)
            lblGPRM_14.Text = GetRegVal(GPRMArray(7), False)
            lblGPRM_15.Text = GetRegVal(GPRMArray(7), True)

            lblGPRMViewer_0.Text = lblGPRM_0.Text
            lblGPRMViewer_1.Text = lblGPRM_1.Text
            lblGPRMViewer_2.Text = lblGPRM_2.Text
            lblGPRMViewer_3.Text = lblGPRM_3.Text
            lblGPRMViewer_4.Text = lblGPRM_4.Text
            lblGPRMViewer_5.Text = lblGPRM_5.Text
            lblGPRMViewer_6.Text = lblGPRM_6.Text
            lblGPRMViewer_7.Text = lblGPRM_7.Text
            lblGPRMViewer_8.Text = lblGPRM_8.Text
            lblGPRMViewer_9.Text = lblGPRM_9.Text
            lblGPRMViewer_10.Text = lblGPRM_10.Text
            lblGPRMViewer_11.Text = lblGPRM_11.Text
            lblGPRMViewer_12.Text = lblGPRM_12.Text
            lblGPRMViewer_13.Text = lblGPRM_13.Text
            lblGPRMViewer_14.Text = lblGPRM_14.Text
            lblGPRMViewer_15.Text = lblGPRM_15.Text

            UpdateWatchValues()

        Catch ex As Exception
            AddConsoleLine(eConsoleItemType.ERROR, "problem updating gprm labels. error: " & ex.Message, Nothing, Nothing)
        End Try
    End Sub

    Private Sub UpdateSPRMs(ByVal SPRMArray() As Integer)
        Try
            If Player.PlayState = ePlayState.SystemJP Then
                ReDim SPRMArray(11)
            End If

            lblSPRM_0.Text = GetRegVal(SPRMArray(0), False) 'confirdsd as default dsnu language
            lblSPRM_1.Text = GetRegVal(SPRMArray(0), True) 'confirdsd as current audio stream no
            lblSPRM_2.Text = GetRegVal(SPRMArray(1), False) 'confirdsd as current sp str no
            lblSPRM_3.Text = GetRegVal(SPRMArray(1), True)
            lblSPRM_4.Text = GetRegVal(SPRMArray(2), False) 'confirdsd as current TT no
            lblSPRM_5.Text = GetRegVal(SPRMArray(2), True) 'either 5 or 6
            lblSPRM_6.Text = GetRegVal(SPRMArray(3), False) 'either 5 or 6
            lblSPRM_7.Text = GetRegVal(SPRMArray(3), True) 'confirdsd as PTT/CH


            Dim PrevS8 As String = lblSPRM_8.Text
            Dim NewS8 As String = GetRegVal(SPRMArray(4), False)
            lblSPRM_8.Text = NewS8 'confirdsd as current btn number

            If PrevS8 <> NewS8 Then
                'UpdateButtonAvailbilityDisplay()
            End If

            lblSPRM_9.Text = GetRegVal(SPRMArray(4), True)
            lblSPRM_10.Text = GetRegVal(SPRMArray(5), False)
            lblSPRM_11.Text = GetRegVal(SPRMArray(5), True)
            lblSPRM_12.Text = GetRegVal(SPRMArray(6), False) 'confirdsd as parental country
            lblSPRM_13.Text = GetRegVal(SPRMArray(6), True) 'confirdsd as parental level
            lblSPRM_14.Text = GetRegVal(SPRMArray(7), False)
            lblSPRM_15.Text = GetRegVal(SPRMArray(7), True) 'Value seems to always be 23,552 (or 23 when you divide by 1024)
            lblSPRM_16.Text = GetRegVal(SPRMArray(8), False)
            lblSPRM_17.Text = GetRegVal(SPRMArray(8), True)
            lblSPRM_18.Text = GetRegVal(SPRMArray(9), False)
            lblSPRM_19.Text = GetRegVal(SPRMArray(9), True)
            lblSPRM_20.Text = GetRegVal(SPRMArray(10), False)
            lblSPRM_21.Text = GetRegVal(SPRMArray(10), True)
            lblSPRM_22.Text = GetRegVal(SPRMArray(11), False)
            lblSPRM_23.Text = GetRegVal(SPRMArray(11), True)
        Catch ex As Exception
            AddConsoleLine(eConsoleItemType.ERROR, "Problem with UpdateSPRMs. Error: " & ex.Message, Nothing, Nothing)
        End Try
    End Sub

    Public Function GetRegVal(ByVal WholeReg As Integer, ByVal ReturnLower As Boolean) As String
        Try
            '1) hex it
            Dim FullHexVal As String = Hex(WholeReg)

            '2) make certain that the string is exactly eight characters
            Select Case FullHexVal.ToString.Length
                Case "7"
                    FullHexVal = "0" & FullHexVal
                Case "6"
                    FullHexVal = "00" & FullHexVal
                Case "5"
                    FullHexVal = "000" & FullHexVal
                Case "4"
                    FullHexVal = "0000" & FullHexVal
                Case "3"
                    FullHexVal = "00000" & FullHexVal
                Case "2"
                    FullHexVal = "000000" & FullHexVal
                Case "1"
                    FullHexVal = "0000000" & FullHexVal
                Case "0"
                    Return 0
            End Select

            '3) split the hex string into two seperate two byte strings
            Dim UpperHex As String = Microsoft.VisualBasic.Right(FullHexVal, 4)
            Dim LowerHex As String = Microsoft.VisualBasic.Left(FullHexVal, 4)

            '4) get the int val for the hex
            Dim Out As Integer
            If ReturnLower Then
                Out = HexToDec(LowerHex)
            Else
                Out = HexToDec(UpperHex)
            End If

            Select Case CurrentRegViewMode
                Case eRegViewType.ASCII
                    Return DECtoASCII(Out)
                Case eRegViewType.Dec
                    Return Out
                Case eRegViewType.Hex
                    Return DecToHex(Out)
                Case eRegViewType.Bin
                    Return DecToBin(Out)
            End Select
        Catch ex As Exception
            AddConsoleLine(eConsoleItemType.ERROR, "Problem with GetRegVal(). Error: " & ex.Message, Nothing, Nothing)
        End Try
    End Function

    Private _CurrentRegViewMode As eRegViewType = eRegViewType.Dec
    Public Property CurrentRegViewMode() As eRegViewType
        Get
            Return Me._CurrentRegViewMode
        End Get
        Set(ByVal Value As eRegViewType)
            Me._CurrentRegViewMode = Value

            Try
                Select Case Value
                    Case eRegViewType.ASCII
                        rbGPRMs_ViewType_asc.Checked = True
                        rbSPRMs_ViewType_asc.Checked = True
                    Case eRegViewType.Dec
                        rbGPRMs_ViewType_dec.Checked = True
                        rbSPRMs_ViewType_dec.Checked = True
                    Case eRegViewType.Hex
                        rbGPRMs_ViewType_hex.Checked = True
                        rbSPRMs_ViewType_hex.Checked = True
                End Select
            Catch ex As Exception

            End Try
        End Set
    End Property

    Public Sub ClearPRMs()
        lblGPRM_0.Text = ""
        lblGPRM_1.Text = ""
        lblGPRM_2.Text = ""
        lblGPRM_3.Text = ""
        lblGPRM_4.Text = ""
        lblGPRM_5.Text = ""
        lblGPRM_6.Text = ""
        lblGPRM_7.Text = ""
        lblGPRM_8.Text = ""
        lblGPRM_9.Text = ""
        lblGPRM_10.Text = ""
        lblGPRM_11.Text = ""
        lblGPRM_12.Text = ""
        lblGPRM_13.Text = ""
        lblGPRM_14.Text = ""
        lblGPRM_15.Text = ""
        lblSPRM_0.Text = ""
        lblSPRM_1.Text = ""
        lblSPRM_2.Text = ""
        lblSPRM_3.Text = ""
        lblSPRM_4.Text = ""
        lblSPRM_5.Text = ""
        lblSPRM_6.Text = ""
        lblSPRM_7.Text = ""
        lblSPRM_8.Text = ""
        lblSPRM_9.Text = ""
        lblSPRM_10.Text = ""
        lblSPRM_11.Text = ""
        lblSPRM_12.Text = ""
        lblSPRM_13.Text = ""
        lblSPRM_14.Text = ""
        lblSPRM_15.Text = ""
        lblSPRM_16.Text = ""
        lblSPRM_17.Text = ""
        lblSPRM_18.Text = ""
        lblSPRM_19.Text = ""
        lblSPRM_20.Text = ""
        lblSPRM_21.Text = ""
        lblSPRM_22.Text = ""
        lblSPRM_23.Text = ""
    End Sub

#End Region 'PANELS:GPRMs/SPRMs *FUNCTIONALITY*

#Region "PANELS:GPRMs/SPRMs:EVENTS"

    Private Sub rbGPRM_ViewType_dec_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbGPRMs_ViewType_dec.CheckedChanged
        If Not Me.rbGPRMs_ViewType_dec.Checked Then Exit Sub
        CurrentRegViewMode = eRegViewType.Dec
        GetRegisters()
    End Sub

    Private Sub rbGPRM_ViewType_hex_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbGPRMs_ViewType_hex.CheckedChanged
        If Not Me.rbGPRMs_ViewType_hex.Checked Then Exit Sub
        CurrentRegViewMode = eRegViewType.Hex
        GetRegisters()
    End Sub

    Private Sub rbGPRM_ViewType_asc_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbGPRMs_ViewType_asc.CheckedChanged
        If Not Me.rbGPRMs_ViewType_asc.Checked Then Exit Sub
        CurrentRegViewMode = eRegViewType.ASCII
        GetRegisters()
    End Sub

    Private Sub rbSPRM_ViewType_dec_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbSPRMs_ViewType_dec.CheckedChanged
        If Not Me.rbSPRMs_ViewType_dec.Checked Then Exit Sub
        CurrentRegViewMode = eRegViewType.Dec
        GetRegisters()
    End Sub

    Private Sub rbSPRM_ViewType_hex_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbSPRMs_ViewType_hex.CheckedChanged
        If Not Me.rbSPRMs_ViewType_hex.Checked Then Exit Sub
        CurrentRegViewMode = eRegViewType.Hex
        GetRegisters()
    End Sub

    Private Sub rbSPRM_ViewType_asc_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbSPRMs_ViewType_asc.CheckedChanged
        If Not Me.rbSPRMs_ViewType_asc.Checked Then Exit Sub
        CurrentRegViewMode = eRegViewType.ASCII
        GetRegisters()
    End Sub

    Private Sub lblGPRM_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblGPRM_0.DoubleClick, lblGPRM_1.DoubleClick, lblGPRM_2.DoubleClick, lblGPRM_3.DoubleClick, lblGPRM_4.DoubleClick, lblGPRM_5.DoubleClick, lblGPRM_6.DoubleClick, lblGPRM_7.DoubleClick, lblGPRM_8.DoubleClick, lblGPRM_9.DoubleClick, lblGPRM_10.DoubleClick, lblGPRM_11.DoubleClick, lblGPRM_12.DoubleClick, lblGPRM_13.DoubleClick, lblGPRM_14.DoubleClick, lblGPRM_15.DoubleClick
        Try
            Dim SetGPRMNo As UInt32 = 0
            Select Case CType(sender, Control).Name
                Case "lblGPRM_0"
                    SetGPRMNo = 0
                Case "lblGPRM_1"
                    SetGPRMNo = 1
                Case "lblGPRM_2"
                    SetGPRMNo = 2
                Case "lblGPRM_3"
                    SetGPRMNo = 3
                Case "lblGPRM_4"
                    SetGPRMNo = 4
                Case "lblGPRM_5"
                    SetGPRMNo = 5
                Case "lblGPRM_6"
                    SetGPRMNo = 6
                Case "lblGPRM_7"
                    SetGPRMNo = 7
                Case "lblGPRM_8"
                    SetGPRMNo = 8
                Case "lblGPRM_9"
                    SetGPRMNo = 9
                Case "lblGPRM_10"
                    SetGPRMNo = 10
                Case "lblGPRM_11"
                    SetGPRMNo = 11
                Case "lblGPRM_12"
                    SetGPRMNo = 12
                Case "lblGPRM_13"
                    SetGPRMNo = 13
                Case "lblGPRM_14"
                    SetGPRMNo = 14
                Case "lblGPRM_15"
                    SetGPRMNo = 15
                Case Else
                    Exit Sub
            End Select
            Dim dlg As New dlgGenericInput(Me, "Enter New GPRM Value")
            If dlg.ShowDialog = DialogResult.OK Then
                If IsNumeric(dlg.USER_INPUT) Then
                    If dlg.USER_INPUT >= 0 And dlg.USER_INPUT <= 65535 Then
                        Player.SetGPRM(SetGPRMNo, dlg.USER_INPUT)
                        Exit Sub
                    End If
                End If
                XtraMessageBox.Show(Me.LookAndFeel, Me, "Invalid GPRM value.", My.Settings.APPLICATION_NAME, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Catch ex As Exception
            Me.AddConsoleLine(eConsoleItemType.ERROR, "Problem with SetGPRM(). Error: " & ex.Message)
        End Try
    End Sub

#End Region 'PANELS:GPRMs/SPRMs:EVENTS

#End Region 'PANELS:GPRMs/SPRMs

#Region "PANELS:VIDEO STREAM INFO"

#Region "PANELS:VIDEO STREAM INFO:BITRATE GRAPH CONTEXT MENU"

    Private Sub miBitrateGraph_Clear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miBitrateGraph_Clear.Click
        BitrateGraph_Graphics.Clear(Color.Black)
    End Sub

    Private Sub miBitrateGraph_Save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miBitrateGraph_Save.Click
        Try
            Dim fsd As New SaveFileDialog
            fsd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            fsd.OverwritePrompt = True
            fsd.Title = "Save Bitrate Graph"
            fsd.Filter = "Bitmap|*.bmp|Tif|*.tif|GIF|*.gif|JPEG|*.jpg|PNG|*.png"
            Me.KH.PauseHook()
            If fsd.ShowDialog = DialogResult.OK Then
                Select Case Path.GetExtension(fsd.FileName).ToLower
                    Case ".bmp"
                        BM.Save(fsd.FileName, System.Drawing.Imaging.ImageFormat.Bmp)
                    Case ".tif"
                        BM.Save(fsd.FileName, System.Drawing.Imaging.ImageFormat.Tiff)
                    Case ".gif"
                        BM.Save(fsd.FileName, System.Drawing.Imaging.ImageFormat.Gif)
                    Case ".jpg"
                        BM.Save(fsd.FileName, System.Drawing.Imaging.ImageFormat.Jpeg)
                    Case ".png"
                        BM.Save(fsd.FileName, System.Drawing.Imaging.ImageFormat.Png)
                End Select
            End If
            fsd = Nothing
            Me.KH.UnpauseHook()
        Catch ex As Exception
            Me.AddConsoleLine(eConsoleItemType.ERROR, "Problem with SaveBitrateGraph. Error: " & ex.Message)
        End Try
    End Sub

#End Region 'PANELS:VIDEO STREAM INFO:BITRATE GRAPH CONTEXT MENU

#Region "PANELS:VIDEO STREAM INFO:BITRATE"

    Private Low As Double = 1000000000
    Private High As Double = 0
    Private AllRates() As Double
    Private Avg As Double = 0

    Private CurrentSecond() As Integer
    Private Cnt As Short = 0

    Public Sub UpdateBitrate(ByVal BR As Integer)
        Try
            If BR < 1 Then Exit Sub
            Me.lblByteRate_Low.BackColor = SystemColors.Control
            Me.lblByteRate_High.BackColor = SystemColors.Control

            'Dim bitrate As Double = ((((BR * 8) * 100) / (10 * 1024 * 1024)) * 100) * 10
            Dim bitrate As Double = (BR * 100) / (10 * 1024 * 1024) * 100
            'Dim bitrate As Short = Math.Round((BR * 100) / (10 * 1024 * 1024) * 100, 0)

            Me.lblByteRate_Cur.Text = Math.Round(bitrate, 0)
            If bitrate < Low Then
                Me.lblByteRate_Low.Text = lblByteRate_Cur.Text
                Low = bitrate
                Me.lblByteRate_Low.BackColor = Color.Yellow
            End If

            If bitrate > High Then
                Me.lblByteRate_High.Text = lblByteRate_Cur.Text
                High = bitrate
                Me.lblByteRate_High.BackColor = Color.Yellow
            End If

            If AllRates Is Nothing Then ReDim AllRates(-1)
            ReDim Preserve AllRates(UBound(AllRates) + 1)
            AllRates(UBound(AllRates)) = bitrate

            'calc avg rate
            Dim total As Long = 0
            For Each d As Double In AllRates
                total += d
            Next
            Avg = total / AllRates.Length
            Me.lblByteRate_Avg.Text = Math.Round(CShort(Avg), 0)
            UpdateGraph(bitrate, Avg)
        Catch ex As Exception
            If InStr(ex.Message.ToLower, "overflow") Then Exit Sub
            Me.AddConsoleLine(eConsoleItemType.ERROR, "Problem with UpdateBitrate. Error: " & ex.Message)
        End Try
    End Sub

    Private Sub btnResetBitrates_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVSI_ResetBitrates.Click
        ResetRateTracking()
    End Sub

    Public Sub ResetRateTracking()
        High = 0
        Low = 1000000000
        Avg = 0
        Me.lblByteRate_Low.Text = ""
        Me.lblByteRate_High.Text = ""
        Me.lblByteRate_Avg.Text = ""
        ReDim AllRates(-1)
        BitrateGraph_Graphics.Clear(Color.Black)
    End Sub

    Public BitrateGraph_Graphics As Graphics
    Private BM As Bitmap

    Private Sub ScrollRight(ByVal CUR As Byte, ByVal AVG As Byte, ByVal CEN As Byte)
        Dim tbm As Bitmap = BM.Clone
        BitrateGraph_Graphics.DrawImage(tbm, 2, 0)

        'blacken
        BitrateGraph_Graphics.DrawLine(New Pen(Color.Black), 0, 0, 0, Me.pbBitrateGraph.Height)
        BitrateGraph_Graphics.DrawLine(New Pen(Color.Black), 1, 0, 1, Me.pbBitrateGraph.Height)

        'current
        BitrateGraph_Graphics.DrawLine(New Pen(Color.FromArgb(16, 235, 16)), 0, CUR, 1, CUR)

        'center
        BitrateGraph_Graphics.DrawLine(New Pen(Color.RoyalBlue), 0, CEN, 1, CEN)

        'avg
        BitrateGraph_Graphics.DrawLine(New Pen(Color.Yellow), 0, AVG, 1, AVG)

        Me.pbBitrateGraph.Image = BM
        'BM = tbm
    End Sub

    Public Sub ClearGraph()
        BitrateGraph_Graphics.Clear(Color.Black)
        Me.pbBitrateGraph.Image = BM
    End Sub

    Private Sub UpdateGraph(ByVal BR As Integer, ByVal AV As Integer)
        Try
            If Player.PlayState = ePlayState.SystemJP Then Exit Sub
            Dim tW As Short = Me.pbBitrateGraph.Width
            Dim tH As Short = Me.pbBitrateGraph.Height

            'write current bitrate
            Dim BRTargPix As Double = tH / 9800
            BRTargPix = BRTargPix * BR
            If BRTargPix > tH Then BRTargPix = tH
            'Debug.WriteLine("Current bitrate written to: " & 68 - CShort(BRTargPix))

            If Math.Abs(BRTargPix) > 68 Then
                Exit Sub
            End If
            Dim CUR As Byte = 68 - CShort(BRTargPix)

            'write centerline
            'Debug.WriteLine("Center line written to: 35")
            Dim CEN As Byte = 35

            'write average bitrate
            BRTargPix = tH / 9800
            BRTargPix = BRTargPix * AV
            If BRTargPix > tH Then BRTargPix = tH
            'Debug.WriteLine("Average bitrate written to: " & 68 - CShort(BRTargPix))
            Dim AVG As Byte = tH - CInt(BRTargPix)

            ScrollRight(CUR, AVG, CEN)

            'If Player.playState = ePlayState.SystemJP Then Exit Sub
            'Dim tW As Short = Me.pbBitrateGraph.Width
            'Dim tH As Short = Me.pbBitrateGraph.Height
            'Dim BM As New Bitmap(Me.pbBitrateGraph.Image)

            ''move all the pixels to the right
            'For y As Short = 0 To BM.Height - 1
            '    For x As Short = tW - 2 To 1 Step -1
            '        BM.SetPixel(x, y, BM.GetPixel(x - 1, y))
            '    Next
            'Next

            ''write current bitrate
            'Dim BRTargPix As Double = tH / 9800
            'BRTargPix = BRTargPix * BR
            'If BRTargPix > tH Then BRTargPix = tH
            ''Debug.WriteLine("Current bitrate written to: " & 68 - CShort(BRTargPix))
            'For y As Short = 0 To tH - 1
            '    If y = 68 - CShort(BRTargPix) Then
            '        BM.SetPixel(0, y, Color.FromArgb(16, 235, 16))
            '    Else
            '        BM.SetPixel(0, y, Color.Black)
            '    End If
            'Next

            ''write centerline
            ''Debug.WriteLine("Center line written to: 35")
            'BM.SetPixel(0, 35, Color.RoyalBlue)

            ''write average bitrate
            'BRTargPix = tH / 9800
            'BRTargPix = BRTargPix * AV
            'If BRTargPix > tH Then BRTargPix = tH
            ''Debug.WriteLine("Average bitrate written to: " & 68 - CShort(BRTargPix))
            'BM.SetPixel(0, tH - CInt(BRTargPix), Color.Yellow)

            'Me.pbBitrateGraph.Image = BM

            ''Debug.WriteLine("-------------------------")
            ''Debug.WriteLine("-------------------------")

        Catch ex As Exception
            Me.AddConsoleLine(eConsoleItemType.ERROR, "Problem with UpdateGraph. Error:" & ex.Message)
        End Try
    End Sub

    Private Sub SetupBitrateGraph()
        Try
            BM = New Bitmap(Me.pbBitrateGraph.Width, Me.pbBitrateGraph.Height, Imaging.PixelFormat.Format24bppRgb)
            BitrateGraph_Graphics = Graphics.FromImage(BM)
            BitrateGraph_Graphics.Clear(Color.Black)
            Me.pbBitrateGraph.Image = BM
        Catch ex As Exception
            Me.AddConsoleLine(eConsoleItemType.ERROR, "Problem with SetupBitrateGraph. Error: " & ex.Message)
        End Try
    End Sub

#End Region 'PANELS:VIDEO STREAM INFO:BITRATE

#Region "PANELS:VIDEO STREAM INFO:POSITION SCROLLER"

    Public Sub UpdatePlayPosition()
        Try
            If Not Player.CurrentDomain = DvdDomain.Title OrElse Player.PlayState = ePlayState.SystemJP Then
                Me.tbPlayPosition.Value = 0
                Exit Sub
            End If
            If Me.ScrollerMouseIsDown Then Exit Sub
            Me.tbPlayPosition.Value = TimecodeToPercentage(Player.CurrentRunningTime_DVD, Player.TotalFrameCount, lblVidAtt_VidStd.Text)
        Catch ex As Exception
            Me.AddConsoleLine(eConsoleItemType.ERROR, "Problem with UpdatePlayPosition. Error: " & ex.Message)
        End Try
    End Sub

    Private Sub ExecuteScroll()
        Try
            Player.PlayAtTime(PercentageToTimecode(Me.tbPlayPosition.Value, Player.TotalFrameCount, lblVidAtt_VidStd.Text), False)
        Catch ex As Exception
            Me.AddConsoleLine(eConsoleItemType.ERROR, "Problem with play position scroll. Error: " & ex.Message)
        End Try
    End Sub

    Private ScrollerMouseIsDown As Boolean = False
    Private ScrollStartVal As Byte = 0
    Private Sub tbPlayPosition_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles tbPlayPosition.MouseDown
        Me.ScrollerMouseIsDown = True
        Me.ScrollStartVal = Me.tbPlayPosition.Value

        Me.ToolTip.AutomaticDelay = 0
        Me.ToolTip.ShowAlways = True
    End Sub

    Private Sub tbPlayPosition_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles tbPlayPosition.MouseUp
        Me.ScrollerMouseIsDown = False
        If Me.ScrollStartVal <> Me.tbPlayPosition.Value Then
            Me.ExecuteScroll()
        End If

        Me.ToolTip.AutomaticDelay = 500
        Me.ToolTip.ShowAlways = False
    End Sub

    Private Sub tbPlayPosition_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbPlayPosition.ValueChanged
        Dim TC As New cTimecode(PercentageToTimecode(Me.tbPlayPosition.Value, Player.TotalFrameCount, lblVidAtt_VidStd.Text), (Player.CurrentVideoStandard = eVideoStandard.NTSC))
        Me.ToolTip.SetToolTip(sender, TC.ToString_NoFrames)
    End Sub

#End Region 'PANELS:VIDEO STREAM INFO:POSITION SCROLLER

#Region "PANELS:VIDEO STREAM INFO *FUNCTIONALITY*"

    Public Sub PopulateVideoStreamInfoWindow(ByVal Sender As String)
        Try
            'Debug.WriteLine("PopulateVideoTab()")
            If Player.PlayState = ePlayState.SystemJP Or Player.CurrentDomain = DvdDomain.Stop Then
                lblVidAtt_aspectratio.Text = ""
                lblVidAtt_compression.Text = ""
                lblVidAtt_frameheight.Text = ""
                lblVidAtt_framerate.Text = ""
                lblVidAtt_isFilmMode.Text = ""
                lblVidAtt_VidStd.Text = ""
                lblVidAtt_isSourceLetterboxed.Text = ""
                lblVidAtt_lbok.Text = ""
                lblVidAtt_line21Field1InGOP.Text = ""
                lblVidAtt_line21Field2InGOP.Text = ""
                lblVidAtt_psok.Text = ""
                lblVidAtt_sourceResolution.Text = ""
                'lblCurrentlyRunning32.Text = ""
                lblCurrentFrameRate.Text = ""
                lblCSSEncrypted.Text = ""
                lblMacrovision.Text = ""
                lbl32WhichField.Text = ""
                lblCurrentVideoIsInterlaced.Text = ""
                Exit Sub
            End If


            Dim VA As DvdVideoAttr = Player.GetVideoAttributes

            lblVidAtt_aspectratio.Text = VA.aspectX & " x " & VA.aspectY
            lblVidAtt_compression.Text = VA.compression.ToString.ToUpper
            lblVidAtt_frameheight.Text = VA.frameHeight
            lblVidAtt_framerate.Text = VA.frameRate
            lblVidAtt_isFilmMode.Text = VA.isFilmMode.ToString
            lblVidAtt_isSourceLetterboxed.Text = VA.isSourceLetterboxed.ToString
            lblVidAtt_line21Field1InGOP.Text = VA.line21Field1InGOP.ToString
            Me.btnRM_ToggleClosedCaptions.Enabled = lblVidAtt_line21Field1InGOP.Text
            lblVidAtt_line21Field2InGOP.Text = VA.line21Field2InGOP.ToString
            lblVidAtt_sourceResolution.Text = VA.sourceResolutionX & " x " & VA.sourceResolutionY
            Me.lblVidAtt_VidStd.Text = Player.CurrentVideoStandard.ToString

        Catch ex As Exception
            AddConsoleLine(eConsoleItemType.ERROR, "problem populating video tab. error: " & ex.Message)
        End Try
    End Sub

#End Region 'PANELS:VIDEO STREAM INFO *FUNCTIONALITY*

#End Region 'PANELS:VIDEO STREAM INFO

#Region "PANELS:AUDIO STREAM INFO"

#Region "PANELS:AUDIO STREAM INFO *FUNCTIONALITY*"

    Public Sub SetupAudioTab()
        Try
            'Debug.WriteLine("SetupAudioTab")
            Player.CloseAudDumpFile()
            Dim NumberOfStreams, CurrentStream As Short
            Player.GetAudioStreamStatus(NumberOfStreams, CurrentStream)

            Dim Audios As New seqAudioStreams
            For i As Short = 0 To NumberOfStreams - 1
                Audios.AddAudio(Player.GetAudio(i))
            Next
            dgAudioStreams.DataSource = Audios.AudioStreams

            'If NumberOfStreams < 1 Then
            '    btnMute.Enabled = False
            'Else
            '    btnMute.Enabled = True
            'End If

            Try
                For i As Short = 0 To dgAudioStreams.VisibleRowCount - 1
                    dgAudioStreams.UnSelect(i)
                Next
                dgAudioStreams.Select(CurrentStream)
            Catch ex As Exception
                'Debug.WriteLine("Could not select current audio stream in the data grid.")
            End Try


            Dim gs As New DataGridTableStyle
            gs.MappingName = Audios.AudioStreams.GetType.Name

            Dim cs As New cDataGridNoActiveCellColumn
            cs.MappingName = "StreamNumber"
            cs.HeaderText = "Stream"
            cs.Alignment = HorizontalAlignment.Center
            cs.Width = 50
            gs.GridColumnStyles.Add(cs)

            cs = New cDataGridNoActiveCellColumn
            cs.MappingName = "Language"
            cs.HeaderText = "Language"
            cs.Alignment = HorizontalAlignment.Center
            cs.Width = 75
            gs.GridColumnStyles.Add(cs)

            cs = New cDataGridNoActiveCellColumn
            cs.MappingName = "Extension"
            cs.HeaderText = "Extension"
            cs.Alignment = HorizontalAlignment.Center
            cs.Width = 75
            gs.GridColumnStyles.Add(cs)

            cs = New cDataGridNoActiveCellColumn
            cs.MappingName = "Format"
            cs.HeaderText = "Format"
            cs.Alignment = HorizontalAlignment.Center
            cs.Width = 75
            gs.GridColumnStyles.Add(cs)

            cs = New cDataGridNoActiveCellColumn
            cs.MappingName = "NumberOfChannels"
            cs.HeaderText = "Channels"
            cs.Alignment = HorizontalAlignment.Center
            cs.Width = 75
            gs.GridColumnStyles.Add(cs)

            cs = New cDataGridNoActiveCellColumn
            cs.MappingName = "AppMode"
            cs.HeaderText = "App Mode"
            cs.Alignment = HorizontalAlignment.Center
            cs.Width = 75
            gs.GridColumnStyles.Add(cs)

            cs = New cDataGridNoActiveCellColumn
            cs.MappingName = "Quantization"
            cs.HeaderText = "Quantization"
            cs.Alignment = HorizontalAlignment.Center
            cs.Width = 75
            gs.GridColumnStyles.Add(cs)

            cs = New cDataGridNoActiveCellColumn
            cs.MappingName = "Frequency"
            cs.HeaderText = "Frequency"
            cs.Alignment = HorizontalAlignment.Center
            cs.Width = 75
            gs.GridColumnStyles.Add(cs)

            cs = New cDataGridNoActiveCellColumn
            cs.MappingName = "Available"
            cs.HeaderText = "Enabled"
            cs.Alignment = HorizontalAlignment.Center
            cs.Width = 75
            gs.GridColumnStyles.Add(cs)

            dgAudioStreams.TableStyles.Clear()
            dgAudioStreams.TableStyles.Add(gs)

        Catch ex As Exception
            If InStr(ex.Message, "The data grid table styles collection already contains a table style with the same mapping name.", CompareMethod.Text) Then Exit Sub
            AddConsoleLine(eConsoleItemType.ERROR, "error setting up audio tab. error: " & ex.Message, Nothing, Nothing)
        End Try
    End Sub

#End Region 'PANELS:AUDIO STREAM INFO *FUNCTIONALITY*

#Region "PANELS:AUDIO STREAM INFO:EVENTS"

    Private Sub dgAudioStreams_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgAudioStreams.MouseUp
        Try
            Dim HTI As System.Windows.Forms.DataGrid.HitTestInfo = dgAudioStreams.HitTest(e.X, e.Y)
            If HTI.Row = -1 Then Exit Sub
            dgAudioStreams.Select(HTI.Row)
            Player.SetAudioStream(dgAudioStreams.CurrentRowIndex)
        Catch ex As Exception
            Me.AddConsoleLine(eConsoleItemType.ERROR, "Problem with dgAudio stream click. Error: " & ex.Message)
        End Try
    End Sub

#End Region 'PANELS:AUDIO STREAM INFO:EVENTS

#End Region 'PANELS:AUDIO STREAM INFO

#Region "PANELS:SUBTITLE STREAM INFO"

#Region "PANELS:SUBTITLE STREAM INFO *FUNCTIONALITY*"

    Public Sub SetupSubtitleTab()
        Try
            If Player.PlayState = ePlayState.SystemJP Then Exit Sub
            'Debug.WriteLine("SetupSubtitleTab()")

            Dim NumberOfStreams As Integer = Player.CurrentDVD.GetSPCount(Player.CurrentVTS, (Player.CurrentDomain = DvdDomain.Title))
            Dim Subtitles As seqSubs = New seqSubs
            For i As Short = 0 To NumberOfStreams - 1
                Subtitles.AddSub(Player.GetSubtitle(i))
            Next

            'ManualSubCBChange = True
            'If SubIsDisabled Then
            '    Me.cbDisplaySubtitles.Checked = False
            'Else
            '    Me.cbDisplaySubtitles.Checked = True
            'End If
            'ManualSubCBChange = False

            ''do it in another thread
            'Dim dlg As GetSubStreamStatusDelegate = New GetSubStreamStatusDelegate(AddressOf GetSubStreamStatus)
            'dlg.BeginInvoke(AddressOf GetSubStreamStatusCompleted, Nothing)

            Dim TempSubs(-1) As seqSub
            For Each S As seqSub In Subtitles.SubtitleStreams
                S.Enabled = Player.IsSubStreamEnabled(S.StreamNumber)
                ReDim Preserve TempSubs(UBound(TempSubs) + 1)
                TempSubs(UBound(TempSubs)) = S

                'Try
                '    HR = me.DVDCtrl.SelectSubpictureStream(S.StreamNumber, DvdCmdFlags.SendEvt, me.cmdOption)
                '    If HR = -2147220849 Then
                '        'Subtitle stream disabled in authoring
                '        S.Enabled = False
                '        'Marshal.ThrowExceptionForHR(HR)
                '    ElseIf HR = -2147220874 Then
                '        'Subtitle surfing disabled
                '        S.Enabled = False
                '        'Marshal.ThrowExceptionForHR(HR)
                '    ElseIf HR < 0 Then
                '        Marshal.ThrowExceptionForHR(HR)
                '    Else
                '        S.Enabled = True
                '    End If
                '    ReDim Preserve TempSubs(UBound(TempSubs) + 1)
                '    TempSubs(UBound(TempSubs)) = S
                'Catch ex As Exception
                '    If InStr(ex.Message.ToLower, "8004028f", CompareMethod.Text) Or InStr(ex.Message.ToLower, "80040276", CompareMethod.Text) Then
                '        'do nothing
                '        GoTo NextSub
                '    End If
                '    Me.AddConsoleLine(eConsoleItemType.ERROR, "Problem checking availability of subs. Error: " & ex.Message)
                'End Try
NextSub:
            Next

            ''select stream 0
            'If Not UBound(TempSubs) < 0 Then
            '    Player.SetSubtitleStream(TempSubs(0).StreamNumber, False, False)
            'End If

            'Try
            '    'return to the user selected stream number

            '    If Player.SubtitlesAreOn Then
            '        Player.SetSubtitleStream(Player.CurrentSubtitleStreamNumber, False, False)
            '    Else
            '        Player.SetSubtitleStream(Player.CurrentSubtitleStreamNumber, False, True)
            '    End If
            'Catch ex As Exception
            '    'this code throws errors in sysjp so we'll just ignore them
            'End Try









            'Try
            '    If DVDCtrl Is Nothing Or DVDInfo Is Nothing Then Exit Sub
            '    Me.TurnSubtitlesOffOn(True)
            'Catch ex As Exception
            '    If InStr(ex.Message.ToLower, "8004028f", CompareMethod.Text) Then
            '        TurnSubtitlesOffOn(False)
            '        DVDCtrl.SelectSubpictureStream(0, DvdCmdFlags.SendEvt, cmdOption)
            '        Me.AddConsoleLine("Subtitle stream " & StreamNumber & " is not available in the current title.")
            '        Exit Sub
            '    End If
            '    If CheckEx(ex, "Set subtitles") Then Exit Sub
            '    AddConsoleLine(eConsoleItemType.ERROR, "Problem toggling subtitle: " & ex.Message)
            'End Try


            dgSubtitles.DataSource = TempSubs

            'If NumberOfStreams < 1 Then
            '    me.btnToggleSubs.Enabled = False
            'Else
            '    me.btnToggleSubs.Enabled = True
            'End If

            Try
                For i As Short = 0 To Me.dgSubtitles.VisibleRowCount - 1
                    Me.dgSubtitles.UnSelect(i)
                Next
                If Player.SubtitlesAreOn Then
                    Me.dgSubtitles.Select(Player.CurrentSubtitleStreamNumber)
                End If
            Catch ex As Exception
                'Debug.WriteLine("Could not select current subtitle stream in the data grid.")
            End Try

            Dim gs As New DataGridTableStyle
            gs.MappingName = Subtitles.SubtitleStreams.GetType.Name

            Dim cs As New cDataGridNoActiveCellColumn
            'Dim cs As New DataGridTextBoxColumn
            cs.MappingName = "StreamNumber"
            cs.HeaderText = "Stream"
            cs.Alignment = HorizontalAlignment.Center
            cs.Width = 50
            gs.GridColumnStyles.Add(cs)

            cs = New cDataGridNoActiveCellColumn
            cs.MappingName = "Language"
            cs.HeaderText = "Language"
            cs.Alignment = HorizontalAlignment.Center
            cs.Width = 100
            gs.GridColumnStyles.Add(cs)

            cs = New cDataGridNoActiveCellColumn
            cs.MappingName = "Extension"
            cs.HeaderText = "Extension"
            cs.Alignment = HorizontalAlignment.Center
            cs.Width = 200
            gs.GridColumnStyles.Add(cs)

            cs = New cDataGridNoActiveCellColumn
            cs.MappingName = "Enabled"
            cs.HeaderText = "Enabled"
            cs.Alignment = HorizontalAlignment.Center
            cs.Width = 50
            gs.GridColumnStyles.Add(cs)

            'cs = New DataGridNoActiveCellColumn
            'cs.MappingName = "Type"
            'cs.HeaderText = "Type"
            'cs.Alignment = HorizontalAlignment.Center
            'cs.Width = 75
            'gs.GridColumnStyles.Add(cs)

            'cs = New DataGridNoActiveCellColumn
            'cs.MappingName = "Coding"
            'cs.HeaderText = "Coding"
            'cs.Alignment = HorizontalAlignment.Center
            'cs.Width = 75
            'gs.GridColumnStyles.Add(cs)

            Me.dgSubtitles.TableStyles.Clear()
            dgSubtitles.TableStyles.Add(gs)

        Catch ex As Exception
            If InStr(ex.Message, "The data grid table styles collection already contains a table style with the same mapping name.", CompareMethod.Text) Then Exit Sub
            Me.AddConsoleLine(eConsoleItemType.ERROR, "Problem with SetupSubtitleTab(). Error: " & ex.Message)
        End Try
    End Sub

#End Region 'PANELS:SUBTITLE STREAM INFO *FUNCTIONALITY*

#Region "PANELS:SUBTITLE STREAM INFO:EVENTS"

    Private Sub dgSubtitles_CurrentCellChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgSubtitles.CurrentCellChanged
        Player.SetSubtitleStream(dgSubtitles.CurrentRowIndex, False, False)
    End Sub

#End Region 'PANELS:SUBTITLE STREAM INFO:EVENTS

#End Region 'PANELS:SUBTITLE STREAM INFO

#Region "PANELS:VMGMVTSM"

#Region "PANELS:VMGMVTSM *FUNCTIONALITY*"

    Public Sub PopulateVMGM()
        Try
            If Player.PlayState = ePlayState.SystemJP Then
                ClearVMGM()
                Exit Sub
            End If
            Dim VMGM_aud As seqAudio = Player.GetAudio(cDVDPlayer.DVD_STREAM_DATA_VMGM)
            Dim VMGM_sub As seqSub = Player.GetSubtitle(cDVDPlayer.DVD_STREAM_DATA_VMGM)

            Me.VMGM_AppMode.Text = VMGM_aud.AppMode
            Me.VMGM_AppModeData.Text = VMGM_aud.AppModeData
            Me.VMGM_AudLang.Text = VMGM_aud.Language
            Me.VMGM_AudLangExt.Text = VMGM_aud.Extension
            Me.VMGM_Channels.Text = VMGM_aud.NumberOfChannels
            Me.VMGM_Coding.Text = VMGM_sub.Coding
            Me.VMGM_Format.Text = VMGM_aud.Format
            Me.VMGM_Freq.Text = VMGM_aud.Frequency
            Me.VMGM_HasMCNFO.Text = VMGM_aud.HasMultiChannelInfo
            Me.VMGM_Quan.Text = VMGM_aud.Quantization
            Me.VMGM_SubLang.Text = VMGM_sub.Language
            Me.VMGM_SubLangExt.Text = VMGM_sub.Extension
            Me.VMGM_Type.Text = VMGM_sub.Type
        Catch ex As Exception
            Me.AddConsoleLine(eConsoleItemType.ERROR, "Problem with PopulateVMGM. Error: " & ex.Message)
        End Try
    End Sub

    Public Sub ClearVMGM()
        Me.VMGM_AppMode.Text = ""
        Me.VMGM_AppModeData.Text = ""
        Me.VMGM_AudLang.Text = ""
        Me.VMGM_AudLangExt.Text = ""
        Me.VMGM_Channels.Text = ""
        Me.VMGM_Coding.Text = ""
        Me.VMGM_Format.Text = ""
        Me.VMGM_Freq.Text = ""
        Me.VMGM_HasMCNFO.Text = ""
        Me.VMGM_Quan.Text = ""
        Me.VMGM_SubLang.Text = ""
        Me.VMGM_SubLangExt.Text = ""
        Me.VMGM_Type.Text = ""
    End Sub

    Public Sub PopulateVTSM()
        Try
            If Player.PlayState = ePlayState.SystemJP Then
                ClearVTSM()
                Exit Sub
            End If
            Dim VTSM_aud As seqAudio = Player.GetAudio(cDVDPlayer.DVD_STREAM_DATA_VTSM)
            Dim VTSM_sub As seqSub = Player.GetSubtitle(cDVDPlayer.DVD_STREAM_DATA_VTSM)

            Me.VTSM_AppMode.Text = VTSM_aud.AppMode
            Me.VTSM_AppModeData.Text = VTSM_aud.AppModeData
            Me.VTSM_AudLang.Text = VTSM_aud.Language
            Me.VTSM_AudLangExt.Text = VTSM_aud.Extension
            Me.VTSM_Channels.Text = VTSM_aud.NumberOfChannels
            Me.VTSM_Coding.Text = VTSM_sub.Coding
            Me.VTSM_Format.Text = VTSM_aud.Format
            Me.VTSM_Freq.Text = VTSM_aud.Frequency
            Me.VTSM_HasMCNFO.Text = VTSM_aud.HasMultiChannelInfo
            Me.VTSM_Quantization.Text = VTSM_aud.Quantization
            Me.VTSM_SubLang.Text = VTSM_sub.Language
            Me.VTSM_SubLangExt.Text = VTSM_sub.Extension
            Me.VTSM_Type.Text = VTSM_sub.Type
        Catch ex As Exception
            Me.AddConsoleLine(eConsoleItemType.ERROR, "Problem with PopulateVTSM. Error: " & ex.Message)
        End Try
    End Sub

    Public Sub ClearVTSM()
        Me.VTSM_AppMode.Text = ""
        Me.VTSM_AppModeData.Text = ""
        Me.VTSM_AudLang.Text = ""
        Me.VTSM_AudLangExt.Text = ""
        Me.VTSM_Channels.Text = ""
        Me.VTSM_Coding.Text = ""
        Me.VTSM_Format.Text = ""
        Me.VTSM_Freq.Text = ""
        Me.VTSM_HasMCNFO.Text = ""
        Me.VTSM_Quantization.Text = ""
        Me.VTSM_SubLang.Text = ""
        Me.VTSM_SubLangExt.Text = ""
        Me.VTSM_Type.Text = ""
    End Sub

#End Region 'PANELS:VMGMVTSM *FUNCTIONALITY*

#End Region 'PANELS:VMGMVTSM

#Region "PANELS:MARKERS"

#Region "PANELS:MARKERS:FUNCTIONALITY"

    Public Sub Markers_UpdateListView()
        Try
            If CurrentMarkerCollection Is Nothing OrElse CurrentMarkerCollection.MarkerSets.Length < 1 Then
                Me.cbMarkers_SetList.Properties.Items.Clear()
                Me.lblMarkers_CollectionName.Text = ""
                Exit Sub
            End If

            'Load markers
            Me.lvMarkers.Items.Clear()
            Dim LVI As ListViewItem
            For i As Short = 0 To UBound(CurrentMarkerCollection.CurrentMarkerSet.Markers)
                'LVI = New ListViewItem(i)
                LVI = New ListViewItem(CurrentMarkerCollection.CurrentMarkerSet.Markers(i).MarkerName)
                'LVI.SubItems.Add(Me.CurrentMarkerSet.Markers(i).MarkerName)
                LVI.SubItems.Add(CurrentMarkerCollection.CurrentMarkerSet.Markers(i).GetMarkerLocationString)

                If Not CurrentMarkerCollection.CurrentMarkerSet.Markers(i).GOPTC Is Nothing Then
                    LVI.SubItems.Add(CurrentMarkerCollection.CurrentMarkerSet.Markers(i).GOPTC.ToString)
                Else
                    LVI.SubItems.Add("Unavailable")
                End If

                Me.lvMarkers.Items.Add(LVI)
            Next
            If Me.lvMarkers.Items.Count > 0 Then
                Me.lvMarkers.EnsureVisible(Me.lvMarkers.Items.Count - 1)
            End If

            'Load set names & select current
            Me.cbMarkers_SetList.Properties.Items.Clear()
            For Each MS As cMarkerSet In CurrentMarkerCollection.MarkerSets
                Me.cbMarkers_SetList.Properties.Items.Add(MS.SetName)
            Next
            Markers_ProgrammaticListSelect = True
            Me.cbMarkers_SetList.SelectedIndex = Me.cbMarkers_SetList.Properties.Items.IndexOf(CurrentMarkerCollection.CurrentMarkerSet.SetName)
            Markers_ProgrammaticListSelect = False

            'Collection name
            Me.lblMarkers_CollectionName.Text = CurrentMarkerCollection.Name

        Catch ex As Exception
            AddConsoleLine(eConsoleItemType.ERROR, "Problem with MarkersListView_Update-markers. Error: " & ex.Message)
        End Try
    End Sub

    Public Sub Markers_LoadSetListIntoCurrentSetsMenu()
        Try
            If Not Me.CurrentMarkerCollection Is Nothing Then
                Me.miMK_ST_CURRENTSETS.ClearLinks()
                Dim BBI As DevExpress.XtraBars.BarButtonItem
                If Me.CurrentMarkerCollection.MarkerSets.Length = 0 Then
                    BBI = New DevExpress.XtraBars.BarButtonItem(barMain, "NONE AVAILABLE")
                    Me.miPROFILES_CACHEDPROFILES.AddItem(BBI)
                Else
                    For b As Byte = 0 To Me.CurrentMarkerCollection.MarkerSets.Length - 1
                        BBI = New DevExpress.XtraBars.BarButtonItem(barMain, Me.CurrentMarkerCollection.MarkerSets(b).SetName)
                        AddHandler BBI.ItemClick, New DevExpress.XtraBars.ItemClickEventHandler(AddressOf Markers_SetListMenuItemSelect)
                        Me.miPROFILES_CACHEDPROFILES.AddItem(BBI)

                    Next
                End If
            End If
        Catch ex As Exception
            Me.AddConsoleLine(eConsoleItemType.ERROR, "Problem with LoadSetListInMarkersMenu.", ex.Message)
        End Try
    End Sub

    Public Sub Markers_SetListMenuItemSelect(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs)
        Try
            Me.CurrentMarkerCollection.SetCurrentIndexBySetName(e.Item.Name.ToLower)
        Catch ex As Exception
            Me.AddConsoleLine(eConsoleItemType.ERROR, "Problem with MarkerSetMenuItemSelect(). Error: " & ex.Message)
        End Try
    End Sub

#Region "PANELS:MARKERS:FUNCTIONALITY:COLLECTION"

    Public CurrentMarkerCollection As cMarkerSetCollection

    Public Function Markers_Collection_OpenWithDialog() As Boolean
        Try
            KH.PauseHook()
            Dim dlg As New OpenFileDialog
            dlg.DefaultExt = ".pmk"
            dlg.Filter = "Marker Set|*.pmk"
            dlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            dlg.Multiselect = False
            dlg.Title = " Select Marker Set"

            If dlg.ShowDialog = DialogResult.OK Then
                Markers_Collection_OpenFromPath(dlg.FileName)
            End If

            KH.UnpauseHook()
        Catch ex As Exception
            AddConsoleLine(eConsoleItemType.ERROR, "Problem with LoadSet. Error: " & ex.Message)
            KH.UnpauseHook()
        End Try

    End Function

    Public Function Markers_Collection_OpenFromPath(ByVal PMCPath As String) As Boolean
        Try
            Dim FS As New FileStream(PMCPath, FileMode.Open)
            Dim StR As New StreamReader(FS)
            Dim inString As String = StR.ReadToEnd
            StR.Close()
            FS.Close()

            Dim SR As New StringReader(inString)
            Dim XS As New XmlSerializer(GetType(cMarkerSetCollection))
            CurrentMarkerCollection = CType(XS.Deserialize(SR), cMarkerSetCollection)
            XS = Nothing
            SR = Nothing
            Markers_UpdateListView()
            Me.lblMarkers_CollectionName.Text = CurrentMarkerCollection.Name
            If CurrentMarkerCollection.ProjectPath.ToLower <> Player.DVDDirectory.ToLower Then
                If File.Exists(CurrentMarkerCollection.ProjectPath & "\video_ts.ifo") Then
                    If InStr(Player.DVDDirectory.ToLower, "media\blank") OrElse XtraMessageBox.Show(Me.LookAndFeel, Me, "The specified marker set is associated with the project listed below." & vbNewLine & "Do you wish to load this project?" & vbNewLine & vbNewLine & CurrentMarkerCollection.ProjectPath, My.Settings.APPLICATION_NAME, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = DialogResult.Yes Then
                        Me.RunProject(CurrentMarkerCollection.ProjectPath)
                    End If
                End If
            Else
                XtraMessageBox.Show(Me.LookAndFeel, Me, "Project path specified in marker collection does not exist. Unable to load project.", My.Settings.APPLICATION_NAME, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
            CurrentMarkerCollection.LoadedFromPath = PMCPath
        Catch ex As Exception
            AddConsoleLine(eConsoleItemType.ERROR, "Problem with LoadSet. Error: " & ex.Message)
            KH.UnpauseHook()
        End Try
    End Function

    Public Function Markers_Collection_Close() As Boolean
        CurrentMarkerCollection = Nothing
        Markers_UpdateListView()
    End Function

    Public Function Markers_Collection_Save(ByVal SavePath As String) As Boolean
        Try
            Dim XS As New XmlSerializer(GetType(cMarkerSetCollection))
            Dim TW As New StringWriter
            XS.Serialize(TW, CurrentMarkerCollection)
            XS = Nothing
            Dim Out As String = TW.ToString

            Dim FS As New FileStream(SavePath, FileMode.OpenOrCreate)
            Dim SW As New StreamWriter(FS)
            SW.Write(Out)
            SW.Close()
            FS.Close()
            FS = Nothing
        Catch ex As Exception
            AddConsoleLine(eConsoleItemType.ERROR, "Problem with SaveMarkerCollection.", ex.Message)
        End Try
    End Function

    Public Function Markers_Collectoin_SaveWithDialog() As Boolean
        Try
            KH.PauseHook()
            Dim dlg As New SaveFileDialog
            dlg.AddExtension = True
            dlg.DefaultExt = ".pmk"
            dlg.Filter = "Marker Set|*.pmk"
            dlg.FileName = CurrentMarkerCollection.MarkerSets(0).SetName & ".pmk"
            dlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            dlg.OverwritePrompt = True
            dlg.Title = "Save Marker Set"
            If dlg.ShowDialog = DialogResult.OK Then
                Markers_Collection_Save(dlg.FileName)
            End If
            KH.UnpauseHook()
            Return True
        Catch ex As Exception
            AddConsoleLine(eConsoleItemType.ERROR, "Problem with SaveSet. Error: " & ex.Message)
            KH.PauseHook()
            Return False
        End Try
        KH.UnpauseHook()
    End Function

    Public Function Markers_Collection_New() As Boolean
        Try
            If Player.PlayState = ePlayState.SystemJP Or Player.PlayState = ePlayState.Init Then
                XtraMessageBox.Show(Me.LookAndFeel, Me, "Cannot create marker collection when not running a project.", My.Settings.APPLICATION_NAME, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return False
            End If
            KH.PauseHook()
            Dim s As String = InputBox("Name for marker collection:", My.Settings.APPLICATION_NAME, "", Left + 150, Top + 50)
            KH.UnpauseHook()
            If s = "" Then
                XtraMessageBox.Show(Me.LookAndFeel, Me, "Invalid set name.", My.Settings.APPLICATION_NAME, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Function
            End If
            CurrentMarkerCollection = New cMarkerSetCollection(Player.DVDDirectory, s)
            Markers_UpdateListView()
            Return True
        Catch ex As Exception
            AddConsoleLine(eConsoleItemType.ERROR, "Problem with NewMarkerCollection.", ex.Message)
            Return False
        End Try
    End Function

    Public Function Markers_Collection_Clear() As Boolean
        Try
            ReDim CurrentMarkerCollection.MarkerSets(-1)
            Markers_UpdateListView()
        Catch ex As Exception
            AddConsoleLine(eConsoleItemType.ERROR, "Problem with Clear Marker Collection.", ex.Message)
        End Try
    End Function

    Public Function Markers_Collection_Delete() As Boolean
        Try
            If Not CurrentMarkerCollection.LoadedFromPath Is Nothing AndAlso CurrentMarkerCollection.LoadedFromPath <> "" Then
                If XtraMessageBox.Show(Me.LookAndFeel, Me, "Confirm delete of: " & vbNewLine & vbNewLine & CurrentMarkerCollection.LoadedFromPath, My.Settings.APPLICATION_NAME, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = MsgBoxResult.Yes Then
                    File.Delete(CurrentMarkerCollection.LoadedFromPath)
                End If
            End If
        Catch ex As Exception
            AddConsoleLine(eConsoleItemType.ERROR, "Problem with DeleteMarkerCollection.", ex.Message)
        End Try
    End Function

    Public Function Markers_Collection_Rename() As Boolean
        Dim s As String = InputBox("New marker collection name: ", "Rename Marker Collection", CurrentMarkerCollection.Name, Left + 150, Top + 50)
        If s <> "" Then
            CurrentMarkerCollection.Name = s
            Markers_UpdateListView()
        End If
    End Function

    Public Function Markers_Collection_EditProjectPath() As Boolean
        If CurrentMarkerCollection Is Nothing Then Exit Function
Retry:
        Dim s As String = InputBox("Edit project path:", "Collection Path", CurrentMarkerCollection.ProjectPath, Left + 150, Top + 50)
        If Directory.Exists(s) Then
            CurrentMarkerCollection.ProjectPath = s
        Else
            If XtraMessageBox.Show(Me.LookAndFeel, Me, "New project path could not be found. Would you like to retry?", My.Settings.APPLICATION_NAME, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = MsgBoxResult.Yes Then
                GoTo Retry
            End If
        End If
    End Function

    Public Function Markers_Collection_GetCollectionAsText(ByVal AsCSV As Boolean) As String
        Try
            Dim sb As New StringBuilder
            sb.Append("Marker Set Collection: " & CurrentMarkerCollection.Name & vbNewLine)
            sb.Append("Saved: " & DateTime.Now.ToString & vbNewLine)
            sb.Append("Path: " & CurrentMarkerCollection.ProjectPath & vbNewLine)
            sb.Append("--------------------------------------------------------------------------------" & vbNewLine)

            For i As Integer = 0 To CurrentMarkerCollection.MarkerSets.Length - 1
                sb.Append(vbNewLine)
                sb.Append("Marker Set: " & CurrentMarkerCollection.MarkerSets(i).SetName & vbNewLine)
                sb.Append("Set Created: " & CurrentMarkerCollection.MarkerSets(i).Created.ToString & vbNewLine)
                For s As Integer = 0 To CurrentMarkerCollection.MarkerSets(i).Markers.Length - 1
                    If AsCSV Then
                        sb.Append(CurrentMarkerCollection.MarkerSets(i).Markers(s).ToStringCSV & vbNewLine)
                    Else
                        sb.Append(CurrentMarkerCollection.MarkerSets(i).Markers(s).ToString & vbNewLine)
                    End If
                Next
                sb.Append("--------------------------------------------------------------------------------" & vbNewLine)
                sb.Append(vbNewLine)
            Next
            Return sb.ToString
        Catch ex As Exception
            AddConsoleLine(eConsoleItemType.ERROR, "Problem with MarkersAsText. Error: " & ex.Message)
            Return ""
        End Try
    End Function

    Public Function Markers_Collection_SaveAsCSVWithDialog() As Boolean
        Try
            Dim dlg As New SaveFileDialog
            dlg.AddExtension = True
            dlg.DefaultExt = ".csv"
            dlg.Filter = "Comma Separated Values (*.CSV)|*.csv"
            dlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            dlg.OverwritePrompt = True
            dlg.Title = "Save Markers CSV"
            KH.PauseHook()
            If dlg.ShowDialog = DialogResult.OK Then
                Dim FS As New FileStream(dlg.FileName, FileMode.OpenOrCreate)
                Dim sw As New StreamWriter(FS)
                sw.Write(Markers_Collection_GetCollectionAsText(True))
                sw.Close()
                FS.Close()
            End If
            KH.UnpauseHook()
            Return True
        Catch ex As Exception
            AddConsoleLine(eConsoleItemType.ERROR, "Problem with Markers_Collection_SaveAsCSVWithDialog(). Error: " & ex.Message)
            Return False
        End Try
    End Function

    Public Function Markers_Collection_SaveAsTextWithDialog() As Boolean
        Try
            Dim dlg As New SaveFileDialog
            dlg.AddExtension = True
            dlg.DefaultExt = ".txt"
            dlg.Filter = "Text File (*.TXT)|*.txt"
            dlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            dlg.OverwritePrompt = True
            dlg.Title = "Save Markers"
            KH.PauseHook()
            If dlg.ShowDialog = DialogResult.OK Then
                Dim FS As New FileStream(dlg.FileName, FileMode.OpenOrCreate)
                Dim sw As New StreamWriter(FS)
                sw.Write(Markers_Collection_GetCollectionAsText(False))
                sw.Close()
                FS.Close()
            End If
            KH.UnpauseHook()
            Return True
        Catch ex As Exception
            AddConsoleLine(eConsoleItemType.ERROR, "Problem with Markers_Collection_SaveAsTextWithDialog(). Error: " & ex.Message)
            Return False
        End Try
    End Function

    Public Function Markers_Collection_Print() As Boolean
        'TODO: Markers_Collection_Print
    End Function

#End Region 'PANELS:MARKERS:COLLECTION

#Region "PANELS:MARKERS:FUNCTIONALITY:SET"

    Public Function Markers_Set_New(Optional ByVal SetName As String = "") As Boolean
        Try
            If CurrentMarkerCollection Is Nothing OrElse CurrentMarkerCollection.Name = "" Then
                If Not Markers_Collection_New() Then Throw New Exception("Failed on call to NewMarkerCollection")
            End If

            If SetName = "" Then
TryAgain:
                KH.PauseHook()
                SetName = InputBox("Name for marker set:", "New Marker Set", "", Left + 150, Top + 50)
                KH.UnpauseHook()

                If SetName = "" Then
                    If XtraMessageBox.Show(Me.LookAndFeel, Me, "Invalid marker set name. Would you like to provide a new one?", My.Settings.APPLICATION_NAME, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = MsgBoxResult.Yes Then
                        GoTo TryAgain
                    Else
                        Return False
                    End If
                End If
            End If

            For Each MS As cMarkerSet In CurrentMarkerCollection.MarkerSets
                If MS.SetName.ToLower = SetName.ToLower Then
                    If XtraMessageBox.Show(Me.LookAndFeel, Me, "Set name already used. Would you like to provide a new one?", My.Settings.APPLICATION_NAME, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = MsgBoxResult.Yes Then
                        GoTo TryAgain
                    Else
                        Return False
                    End If
                End If
            Next

            CurrentMarkerCollection.Add(New cMarkerSet(SetName))
            CurrentMarkerCollection.CurrentSetIndex = UBound(CurrentMarkerCollection.MarkerSets)
            Markers_UpdateListView()
            Return True
        Catch ex As Exception
            AddConsoleLine(eConsoleItemType.ERROR, "Problem with NewSet. Error: " & ex.Message)
            Return False
        End Try
    End Function

    Public Sub Markers_Set_Clear()
        If Not CurrentMarkerCollection Is Nothing Then
            ReDim CurrentMarkerCollection.MarkerSets(CurrentMarkerCollection.CurrentSetIndex).Markers(-1)
        End If
    End Sub

    Public Sub Markers_Set_Rename()
        Try
            Dim s As String = InputBox("New marker set name: ", "Rename Marker Set", CurrentMarkerCollection.MarkerSets(CurrentMarkerCollection.CurrentSetIndex).SetName, Left + 150, Top + 50)
            If s <> "" Then
                CurrentMarkerCollection.MarkerSets(CurrentMarkerCollection.CurrentSetIndex).SetName = s
                Markers_UpdateListView()
            End If
        Catch ex As Exception
            AddConsoleLine(eConsoleItemType.ERROR, "Problem with rename marker set.", ex.Message)
        End Try
    End Sub

    Public Sub Markers_Set_Delete()
        Try
            If Not CurrentMarkerCollection Is Nothing Then
                If CurrentMarkerCollection.MarkerSets.Length > 0 Then CurrentMarkerCollection.RemoveAt(CurrentMarkerCollection.CurrentSetIndex)
                CurrentMarkerCollection.CurrentSetIndex = 0
                Markers_UpdateListView()
                _LastMarkerNum = 0
            End If
        Catch ex As Exception
            AddConsoleLine(eConsoleItemType.ERROR, "Problem with DeleteMarkerSet.", ex.Message)
        End Try
    End Sub

#End Region 'PANELS:MARKERS:FUNCTIONALITY:SET

#Region "PANELS:MARKERS:FUNCTIONALITY:MARKER"

    Private _LastMarkerNum As Short = 0

    Public Sub Markers_Marker_New(ByVal Manual As Boolean, ByVal Serialize As Boolean)
        Try
            If Player.CurrentDomain = DvdDomain.VideoManagerMenu Or Player.CurrentDomain = DvdDomain.VideoTitleSetMenu Then
                XtraMessageBox.Show(Me.LookAndFeel, Me, "Markers can only be created in title space.", My.Settings.APPLICATION_NAME, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            Dim nM As New cMarker

            If Manual Then
                Dim dlg As New dlgManualMarker(Me)
                If dlg.ShowDialog = DialogResult.OK Then
                    nM = dlg.NewMarker
                    If dlg.SetName <> "" Then
                        CurrentMarkerCollection.FindSetByName(dlg.SetName).AddMarker(nM)
                    Else
                        If CurrentMarkerCollection.MarkerSets.Length < 1 Then
                            Markers_Set_New()
                        End If
                        CurrentMarkerCollection.CurrentMarkerSet.AddMarker(nM)
                    End If
                    Markers_UpdateListView()
                End If
            Else

                If Not Player.LastGOPTC Is Nothing Then
                    nM.GOPTC = Player.LastGOPTC
                End If

                If CurrentMarkerCollection Is Nothing OrElse CurrentMarkerCollection.CurrentMarkerSet Is Nothing Then
                    If Not Markers_Set_New() Then Throw New Exception("NewSet failed.")
                End If

                If Not Serialize Then
                    KH.PauseHook()
                    nM.MarkerName = InputBox("Name for marker:", "New Marker", "", Left + 150, Top + 50)
                    KH.UnpauseHook()
                    If nM.MarkerName = "" Then
                        XtraMessageBox.Show(Me.LookAndFeel, Me, "Invalid marker name.", My.Settings.APPLICATION_NAME, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                Else
                    Me._LastMarkerNum += 1
                    nM.MarkerName = (Me._LastMarkerNum)
                End If

                Dim loc As DvdPlayLocation = Player.CurrentPlayLocation
                nM.ChapterNum = loc.ChapterNum
                nM.Frames = loc.timeCode.bFrames
                nM.Hours = loc.timeCode.bHours
                nM.Minutes = loc.timeCode.bMinutes
                nM.Seconds = loc.timeCode.bSeconds
                nM.TimeCodeFlags = loc.TimeCodeFlags
                nM.TitleNum = loc.TitleNum

                If CurrentMarkerCollection.MarkerSets.Length < 1 Then
                    Markers_Set_New()
                End If
                CurrentMarkerCollection.CurrentMarkerSet.AddMarker(nM)
                Markers_UpdateListView()
            End If
        Catch ex As Exception
            AddConsoleLine(eConsoleItemType.ERROR, "Problem with NewMarker. Error: " & ex.Message)
        End Try
    End Sub

    Public Sub Markers_Marker_New_Serialize()
        Markers_Marker_New(False, True)
    End Sub

    Public Sub Markers_Marker_New_Manual()
        Markers_Marker_New(True, False)
    End Sub

    Public Sub Markers_Marker_Execute(ByVal MarkerName As String, ByVal ApplyPreRoll As Boolean)
        Try
            Dim m As cMarker
            For Each mk As cMarker In CurrentMarkerCollection.CurrentMarkerSet.Markers
                If mk.MarkerName.ToLower = MarkerName.ToLower Then
                    m = mk
                    Exit For
                End If
            Next

            Dim tc As New cTimecode(m.GetDVDTimeCode, (Player.CurrentVideoStandard = eVideoStandard.NTSC))
            If ApplyPreRoll Then
                'subtract JumpSecs from tc
                tc = SubtractTimecode(tc, New cTimecode(0, 0, CurrentUserProfile.AppOptions.JumpSeconds, 0, (Player.CurrentVideoStandard = eVideoStandard.NTSC)), 30)
            End If
            Player.PlayAtTimeInTitle(tc.DVDTimeCode, m.TitleNum)
        Catch ex As Exception
            AddConsoleLine(eConsoleItemType.ERROR, "Problem with RunMarker. Error: " & ex.Message)
        End Try
    End Sub

    Public Sub Markers_Marker_ExecuteCurrent()
        Dim lvi As ListViewItem = Me.lvMarkers.SelectedItems.Item(0)
        If Not lvi Is Nothing Then
            Markers_Marker_Execute(lvi.Text, Me.cbMarkers_PreRoll.Checked)
        End If
    End Sub

    Public Sub Markers_Marker_Delete(ByVal MarkerIndex As Byte)
        CurrentMarkerCollection.CurrentMarkerSet.RemoveAt(MarkerIndex)
        Markers_UpdateListView()
    End Sub

    Public Sub Markers_Marker_Move(ByVal SrcMarker As Byte, ByVal TargSet As String)
        Try
            CurrentMarkerCollection.FindSetByName(TargSet).AddMarker(CurrentMarkerCollection.CurrentMarkerSet.Markers(SrcMarker))
            CurrentMarkerCollection.CurrentMarkerSet.RemoveAt(SrcMarker)
            Markers_UpdateListView()
        Catch ex As Exception
            AddConsoleLine(eConsoleItemType.ERROR, "Problem with MoveMarker.", ex.Message)
        End Try
    End Sub

#End Region 'PANELS:MARKERS:FUNCTIONALITY:MARKER

#End Region 'PANELS:MARKERS:FUNCTIONALITY

#Region "PANELS:MARKERS:EVENTS"

    Private Sub llMarkers_NewMarker_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llMarkers_NewMarker.LinkClicked
        Markers_Marker_New(Me.cbMarkers_ManualMarker.Checked, Me.cbMarkers_SerializeMarkerNames.Checked)
    End Sub

    Private Markers_ProgrammaticListSelect = False
    Private Sub cbMarkers_SetList_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbMarkers_SetList.SelectedIndexChanged
        If Markers_ProgrammaticListSelect Then Exit Sub
        CurrentMarkerCollection.CurrentSetIndex = Me.cbMarkers_SetList.SelectedIndex
        Markers_UpdateListView()
    End Sub

    Private Sub lvMarkers_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvMarkers.DoubleClick
        Markers_Marker_ExecuteCurrent()
    End Sub

    Private Sub lvMarkers_BeforeLabelEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.LabelEditEventArgs) Handles lvMarkers.BeforeLabelEdit
        KH.PauseHook()
    End Sub

    Private Sub lvMarkers_AfterLabelEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.LabelEditEventArgs) Handles lvMarkers.AfterLabelEdit
        KH.UnpauseHook()
        CurrentMarkerCollection.MarkerSets(0).Markers(e.Item).MarkerName = e.Label
    End Sub

#End Region 'PANELS:MARKERS:EVENTS

#Region "PANELS:MARKERS:CONTEXT MENU"

    Public Sub Markers_LoadSetListInContextMenu()
        Try
            If Not CurrentMarkerCollection Is Nothing Then
                If CurrentMarkerCollection.MarkerSets.Length = 0 Then
                    Me.miCMMK_MoveMarker.DropDownItems.Clear()
                    Me.miCMMK_MoveMarker.DropDownItems.Add("NONE AVAILABLE")
                Else
                    Me.miCMMK_MoveMarker.DropDownItems.Clear()
                    For b As Byte = 0 To CurrentMarkerCollection.MarkerSets.Length - 1
                        Me.miCMMK_MoveMarker.DropDownItems.Add(CurrentMarkerCollection.MarkerSets(b).SetName, Nothing, New EventHandler(AddressOf Markers_SetContextMenuItemSelect))
                    Next
                End If
            End If
        Catch ex As Exception
            AddConsoleLine(eConsoleItemType.ERROR, "Problem with LoadSetListInMarkersMenu.", ex.Message)
        End Try
    End Sub

    Public Sub Markers_SetContextMenuItemSelect(ByVal sender As Object, ByVal e As EventArgs)
        Try
            'MoveMarkerToSet
            Dim lvi As ListViewItem = Me.lvMarkers.SelectedItems.Item(0)
            If Not lvi Is Nothing Then
                Markers_Marker_Move(lvi.Index, CType(sender, MenuItem).Text.ToLower)
            End If
        Catch ex As Exception
            Me.AddConsoleLine(eConsoleItemType.ERROR, "Problem with MarkerSetContextMenuItemSelect(). Error: " & ex.Message)
        End Try
    End Sub

    Private Sub miCMMK_Print_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miCMMK_Print.Click
        Markers_Collection_Print()
    End Sub

    Private Sub miCMMK_SaveAsText_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miCMMK_SaveAsText.Click
        Markers_Collection_SaveAsTextWithDialog()
    End Sub

    Private Sub miCMMK_SaveAsCSV_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miCMMK_SaveAsCSV.Click
        Markers_Collection_SaveAsCSVWithDialog()
    End Sub

    Private Sub miCMMK_CopyAsText_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miCMMK_CopyAsText.Click
        Clipboard.SetDataObject(Markers_Collection_GetCollectionAsText(False))
    End Sub

    Private Sub miCMMK_CopyAsCSV_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miCMMK_CopyAsCSV.Click
        Clipboard.SetDataObject(Markers_Collection_GetCollectionAsText(True))
    End Sub

    Private Sub miCMMK_DeleteMarker_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miCMMK_DeleteMarker.Click
        Dim lvi As ListViewItem = Me.lvMarkers.SelectedItems.Item(0)
        If Not lvi Is Nothing Then
            Me.Markers_Marker_Delete(lvi.Index)
        End If
    End Sub

    Private Sub miCMMK_MoveMarker_VisibleChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miCMMK_MoveMarker.VisibleChanged
        Markers_LoadSetListInContextMenu()
    End Sub

#End Region 'PANELS:MARKERS:CONTEXT MENU

#End Region 'PANELS:MARKERS

#Region "PANELS:MACROS"

#Region "PANELS:MACROS:EVENTS"

    Private Sub MC_btnRecordMacro_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MC_btnRecordMacro.Click
        If Player.RecordingMacro Then
            Dim Name As String = InputBox("New Macro Name:")
            If Name = "" OrElse Name Is Nothing Then
                XtraMessageBox.Show(Me.LookAndFeel, Me, "Invalid Macro Name. Failed to save macro.", My.Settings.APPLICATION_NAME, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
            Player.SaveMacro(Me.MacroStorageDirectory, Name)
            Me.MC_btnRecordMacro.Text = "Record"
            Me.LoadUpMacros()
        Else
            XtraMessageBox.Show("Emulation Macro functionality is not fully implemented in Phoenix 2.0." & vbNewLine & vbNewLine & "An update will be available mid-July that will include fully functional Emulation Macros.", My.Settings.APPLICATION_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Player.NewMacro()
            Me.MC_btnRecordMacro.Text = "Stop Record"
        End If
    End Sub

    Private Sub MC_btnDeleteMacro_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MC_btnDeleteMacro.Click
        Dim MI As cMacroListboxItem = Me.CurrentlySelectedMacro
        If XtraMessageBox.Show(Me.LookAndFeel, Me, "Delete " & MI.Name & " Macro?" & vbNewLine & vbNewLine & MI.Path, My.Settings.APPLICATION_NAME, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = MsgBoxResult.No Then
            Exit Sub
        End If
        File.Delete(MI.Path)
    End Sub

    Private Sub MC_btnOpenFolder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MC_btnOpenFolder.Click
        Process.Start("C:\WINDOWS\explorer.exe", "/n,/e," & Me.MacroStorageDirectory)
    End Sub

    Private Sub MC_btnRunMacro_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MC_btnRunMacro.Click
        Player.RunMacro_OneX()
        Me.MC_gbAvailableMacros.Enabled = False
    End Sub

    Private Sub MC_btnStopMacro_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MC_btnStopMacro.Click
        Player.StopAutoMacro()
    End Sub

    Private Sub MC_btnRestartMacro_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MC_btnRestartMacro.Click
        Player.RestartMacro()
    End Sub

    Private Sub MC_lbAvailableMacros_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles MC_lbAvailableMacros.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            Dim MyFiles() As String = e.Data.GetData(DataFormats.FileDrop)
            For Each Fi As String In MyFiles
                If Path.GetExtension(Fi) = ".pmc" Then
                    e.Effect = DragDropEffects.Copy
                Else
                    e.Effect = DragDropEffects.None
                End If
            Next
        End If
    End Sub

    Private Sub MC_lbAvailableMacros_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles MC_lbAvailableMacros.DragDrop
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            MC_lbAvailableMacros.Items.Clear()
            Dim MyFiles = e.Data.GetData(DataFormats.FileDrop)
            For Each Fi As String In MyFiles
                If Path.GetExtension(Fi) = ".pmc" Then
                    File.Copy(Fi, Me.MacroStorageDirectory & Path.GetFileName(Fi))
                    MC_lbAvailableMacros.Items.Add(New cMacroListboxItem(Path.GetFileNameWithoutExtension(Fi), Me.MacroStorageDirectory & Path.GetFileName(Fi)))
                End If
            Next
        End If
    End Sub

    Private Sub MC_lbAvailableMacros_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles MC_lbAvailableMacros.SelectedIndexChanged
        Me.ActivateSelectedMacro()
    End Sub

    Private Sub MC_lbAvailableMacros_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MC_lbAvailableMacros.MouseDoubleClick
        Me.ActivateSelectedMacro()
    End Sub

#End Region 'PANELS:MACROS:EVENTS

#Region "PANELS:MACROS:FUNCTIONALITY"

    Private Sub LoadUpMacros()
        Try
            MC_lbAvailableMacros.Items.Clear()
            For Each Fi As String In Directory.GetFiles(MacroStorageDirectory, "*.pmc")
                MC_lbAvailableMacros.Items.Add(New cMacroListboxItem(Path.GetFileNameWithoutExtension(Fi), Fi))
            Next
        Catch ex As Exception
            Me.AddConsoleLine(eConsoleItemType.ERROR, "Problem with LoadUpMacros(). Error: " & ex.Message)
        End Try
    End Sub

    Public Class cMacroListboxItem
        Public Name As String
        Public Path As String

        Public Overrides Function ToString() As String
            Return Name
        End Function

        Public Sub New(ByVal nName As String, ByVal nPath As String)
            Name = nName
            Path = nPath
        End Sub

    End Class

    Public ReadOnly Property CurrentlySelectedMacro() As cMacroListboxItem
        Get
            Return CType(Me.MC_lbAvailableMacros.SelectedItem, cMacroListboxItem)
        End Get
    End Property

    Public Sub ActivateSelectedMacro()
        Try
            If Me.Player.PlayState = ePlayState.SystemJP Then Exit Sub
            If Me.MC_lbAvailableMacros.ItemCount < 1 Then Exit Sub
            If Not Player.LoadMacro(Me.CurrentlySelectedMacro.Path) Then Throw New Exception("Failed to load macro.")

            If Player.CurrentMacroPath <> Player.DVDDirectory Then
                XtraMessageBox.Show(Me.LookAndFeel, Me, "WARNING: The selected macro is not associated with this project.", My.Settings.APPLICATION_NAME, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
            'Associate with this project?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            '                   Return False
            '               End If
            '           End If

            Me.MC_lblMacroName.Text = Player.CurrentMacroName
            Me.MC_lblMacroStartLocation.Text = Player.CurrentMacroStartLocation.ToString
            Me.MC_lblMacroDuration.Text = "N/A"
            Me.MC_lblMacroOperationCount.Text = Player.CurrentMacro.Count
        Catch ex As Exception
            Me.AddConsoleLine(eConsoleItemType.ERROR, "Problem with ActivateSelectedMacro(). Error: " & ex.Message)
        End Try
    End Sub

    Public Sub AddMacroItem(ByVal Command As cDVDPlayer.eMacroCommand, ByVal ExtendedData As Long)
        Me.MC_lbMacroItems.Items.Add([Enum].GetName(GetType(cDVDPlayer.eMacroCommand), Command) & " " & ExtendedData)
        Me.MC_lbMacroItems.MakeItemVisible(Me.MC_lbMacroItems.Items.Count - 1)
    End Sub

#End Region 'PANELS:MACROS:FUNCTIONALITY

#End Region 'PANELS:MACROS

#Region "PANELS:STREAM PLAYBACK"

#Region "PANELS:STREAM PLAYBACK:EVENTS"

    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSPB_Browse.Click
        Try
            Dim FSD As New OpenFileDialog
            FSD.Filter = "All Supported Formats|*.m2v;*.ac3;*.dts;*.wav;*.pcm" _
                & "|MPEG-2 Elementary Video Streams (*.M2V)|*.m2v" _
                & "|Dolby Audio (*.AC3)|*.ac3" _
                & "|DTS Audio (*.DTS)|*.dts" _
                & "|WAV Audio (*.WAV,*.PCM)|*.wav;*.pcm"

            Dim Dir As String = LastStreamPath
            If Dir = "" Then Dir = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            FSD.InitialDirectory = Dir
            FSD.Multiselect = False
            FSD.Title = My.Settings.APPLICATION_NAME & " File Playback"
            If FSD.ShowDialog = DialogResult.OK Then

                If Not Player Is Nothing Then
                    Player.EjectProject(True)
                    Player = Nothing
                End If

                If Not StreamPlayer Is Nothing Then
                    Me.StreamPlaybackTimer.Stop()
                    StreamPlayer.Dispose()
                    StreamPlayer = Nothing
                    StreamPlaybackState = eStreamPlaybackState.Not_Initialized
                End If

                LastStreamPath = FSD.FileName
                Me.txtSPB_FilePath.Text = FSD.FileName
                Me.StreamPlaybackToggle(True)
            End If
        Catch ex As Exception
            Me.AddConsoleLine(eConsoleItemType.ERROR, "Problem with FilePlayback Browse(). Error: " & ex.Message)
        End Try
    End Sub

    Private Sub btnSPB_ReturnToDVDMode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSPB_ReturnToDVDMode.Click
        Me.StreamPlaybackToggle(False)
    End Sub

    Private Sub btnSPB_Play_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSPB_Play.Click
        If StreamPlayer Is Nothing Then Exit Sub
        Select Case StreamPlayer.PlayState
            Case ePlayState.Playing
                StreamPlayer.Pause()
            Case Else
                StreamPlayer.Play()
        End Select
    End Sub

    Private Sub btnSPB_Stop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSPB_Stop.Click
        If StreamPlayer Is Nothing Then Exit Sub
        StreamPlayer.Stop()
    End Sub

    Private Sub btnSPB_Rewind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSPB_Rewind.Click
        If StreamPlayer Is Nothing Then Exit Sub
        StreamPlayer.Rewind()
    End Sub

    Private Sub btnSPB_FastForward_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSPB_FastForward.Click
        If StreamPlayer Is Nothing Then Exit Sub
        StreamPlayer.FastForward()
    End Sub

    Private Sub StreamPlaybackTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StreamPlaybackTimer.Tick
        Try
            Dim Seconds As Long = StreamPlayer.GetPosition / 10000000

            Me.tbSPB_PlayPosition.Value = Seconds

            Dim tmp As String
            Dim dh As String
            If Seconds > 3600 Then
                tmp = CStr(Seconds / 3600)
                dh = MakeTwoDig(Microsoft.VisualBasic.Left(tmp, 1))
                Seconds -= (dh * 3600)
            Else
                dh = "00"
            End If

            Dim dm As String
            If Seconds > 60 Then
                tmp = CStr(Seconds / 60)
                Dim t() As String = Split(tmp, ".", -1, CompareMethod.Text)
                dm = MakeTwoDig(t(0))
                Seconds -= (dm * 60)
            Else
                dm = "00"
            End If

            Dim ds As String = MakeTwoDig(Seconds)
            Me.lblSPB_Position.Text = dh & ":" & dm & ":" & ds
        Catch ex As Exception
            AddConsoleLine(eConsoleItemType.ERROR, "Problem with StreamPlaybackTimer.Tick(). Error: " & ex.Message, Nothing)
        End Try
    End Sub

    Private Sub tbSPB_PlayPosition_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles tbSPB_PlayPosition.MouseDown
        StreamPlayer.Pause()
        Me.StreamPlaybackTimer.Stop()
    End Sub

    Private Sub tbSPB_PlayPosition_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles tbSPB_PlayPosition.MouseUp
        StreamPlayer.Play()
        NewStreamPlaybackPosition = Me.tbSPB_PlayPosition.Value * 100000 'Seconds * 100000 '1000000
        If StreamPlayer.PlayState = ePlayState.Stopped Or StreamPlayer.PlayState = ePlayState.Paused Then
            StreamPlayer.Play()
        End If
        Dim t As New Threading.Thread(AddressOf SetStreamPlaybackPosition_ThreadStart)
        t.Start()
        Me.StreamPlaybackTimer.Start()
    End Sub

    Public Shared NewStreamPlaybackPosition As Long
    Public Sub SetStreamPlaybackPosition_ThreadStart()
        Try
            StreamPlayer.SetPosition(NewStreamPlaybackPosition * 100)
        Catch ex As Exception
            Me.AddConsoleLine(eConsoleItemType.ERROR, "Problem with SetStreamPlaybackPosition_ThreadStart(). Error: " & ex.Message)
        End Try
    End Sub

    Private Sub btnSPB_TimeSearchGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSPB_TimeSearchGo.Click
        Try
            If Me.txtSPB_TimeSearch_h.Text = "" Then Me.txtSPB_TimeSearch_h.Text = "0"
            If Me.txtSPB_TimeSearch_m.Text = "" Then Me.txtSPB_TimeSearch_m.Text = "0"
            If Me.txtSPB_TimeSearch_s.Text = "" Then Me.txtSPB_TimeSearch_s.Text = "0"

            Dim Seconds As Short = Me.txtSPB_TimeSearch_s.Text
            Seconds += Me.txtSPB_TimeSearch_h.Text * 3600
            Seconds += Me.txtSPB_TimeSearch_m.Text * 60
            If Seconds > Me.tbSPB_PlayPosition.Maximum Then
                XtraMessageBox.Show(Me.LookAndFeel, Me, "Seek time is greater than stream duration. Seek Failed.", My.Settings.APPLICATION_NAME, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
            NewStreamPlaybackPosition = Seconds * 100000
            If StreamPlayer.PlayState = ePlayState.Stopped Or StreamPlayer.PlayState = ePlayState.Paused Then
                StreamPlayer.Play()
            End If
            Dim t As New Threading.Thread(AddressOf SetStreamPlaybackPosition_ThreadStart)
            t.Start()
        Catch ex As Exception
            Me.AddConsoleLine(eConsoleItemType.ERROR, "Problem with btnSPB_TimeSearchGo.Click(). Error: " & ex.Message)
        End Try
    End Sub

    Private Sub txtSPB_TimeSearch_h_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSPB_TimeSearch_h.GotFocus
        Me.txtSPB_TimeSearch_h.Text = ""
    End Sub

    Private Sub txtSPB_TimeSearch_m_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSPB_TimeSearch_m.GotFocus
        Me.txtSPB_TimeSearch_m.Text = ""
    End Sub

    Private Sub txtSPB_TimeSearch_s_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSPB_TimeSearch_s.GotFocus
        Me.txtSPB_TimeSearch_s.Text = ""
    End Sub

    Private Sub btnSPB_Scrap_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSPB_Scrap.Click
        Debug.WriteLine(Me.tbPlayPosition.Maximum)
    End Sub

#End Region 'PANELS:STREAM PLAYBACK:EVENTS

#Region "PANELS:STREAM PLAYBACK *FUNTIONALITY*"

    Private WithEvents StreamPlayer As cBasePlayer
    Private StreamPlaybackState As eStreamPlaybackState = eStreamPlaybackState.Not_Initialized
    Private ReadOnly Property StreamPlayback_UseVMR9() As Boolean
        Get
            Return PreferredAVMode = eAVMode.DesktopVMR
        End Get
    End Property

    Private Sub StreamPlaybackToggle(ByVal FilePlayback As Boolean)
        Try
            Me.Refresh()
            If FilePlayback Then
                If Not Player Is Nothing Then
                    Player.EjectProject()
                    Player = Nothing
                End If
                Try
                    If SetupStreamPlayer() Then
                        KH.PauseHook()
                        Me.btnSPB_ReturnToDVDMode.Enabled = True
                        ToggleDockWindows(True)
                        Me.gbPBControl.Enabled = True
                        Me.btnSPB_Play.Enabled = True
                        Me.btnSPB_Stop.Enabled = True
                    End If
                Catch ex As Exception
                    Me.AddConsoleLine(eConsoleItemType.ERROR, "Failed to setup stream player in StreamPlayerToggle(). Error: " & ex.Message)
                End Try
            Else
                KH.UnpauseHook()
                StreamPlaybackTimer.Stop()
                StreamPlayer.Dispose()
                StreamPlayer = Nothing
                StreamPlaybackState = eStreamPlaybackState.Not_Initialized
                Me.txtSPB_FilePath.Text = ""
                Me.txtSPB_TimeSearch_s.Text = "00"
                Me.txtSPB_TimeSearch_m.Text = "00"
                Me.txtSPB_TimeSearch_h.Text = "00"
                Me.lblSPB_Position.Text = "00:00:00"
                Me.lblSPB_Duration.Text = "00:00:00"
                Me.tbSPB_PlayPosition.Value = "0"
                Me.btnSPB_ReturnToDVDMode.Enabled = False
                ToggleDockWindows(False)
                Me.gbPBControl.Enabled = False
                'Player = New cDVDPlayer(Me, Me.ForceMobileMode)
                Player = New cDVDPlayer(New cSMTForm(Me), Me.PreferredAVMode, CurrentUserProfile.AppOptions.IntensityVideoScalingMode, CurrentUserProfile.AppOptions.IntensityVideoResolution, My.Settings.DVD_NOTIFY_NONSEAMLESSCELL)
                Player.InitializeSystemJacketPicture()
                If Player.AVMode = eAVMode.DesktopVMR Then Player.ShowViewer()
            End If
        Catch ex As Exception
            AddConsoleLine(eConsoleItemType.ERROR, "Problem with StreamPlaybackToggle(). Error: " & ex.Message)
        End Try
    End Sub

    Public Property LastStreamPath() As String
        Get
            Dim s As String = My.Settings.LAST_STREAM_PATH
            If s = "" Then
                s = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            Else
                s = Path.GetDirectoryName(s)
            End If
            Return s
        End Get
        Set(ByVal Value As String)
            My.Settings.LAST_STREAM_PATH = Value
            My.Settings.Save()
        End Set
    End Property

    Private Sub ToggleDockWindows(ByVal Disable As Boolean)
        If Disable Then
            For Each p As DevExpress.XtraBars.Docking.DockPanel In Me.DockManager.Panels
                'Debug.WriteLine("Panel text: " & p.Text & " Panel name: " & p.Name)
                If p.Text <> "Stream Playback" And Microsoft.VisualBasic.Right(p.Text, 2) = "dp" Then
                    p.Enabled = False
                Else
                    p.Enabled = True
                End If
            Next
        Else
            For Each p As DevExpress.XtraBars.Docking.DockPanel In Me.DockManager.Panels
                p.Enabled = True
            Next
        End If
    End Sub

    Private Function SetupStreamPlayer() As Boolean
        Try
            If Me.txtSPB_FilePath.Text = "" Then Throw New Exception("No file specified for stream playback.")
            Dim ext As String = Path.GetExtension(Me.txtSPB_FilePath.Text).ToLower
            Select Case ext
                Case ".m2v"
                    StreamPlayer = New cSDMPEG2Player
                    StreamPlayer.BuildGraph(Me.txtSPB_FilePath.Text, Me.Handle, PreferredAVMode, Me)
                    If StreamPlayback_UseVMR9 Then StreamPlayer.ShowViewer()
                    Me.btnSPB_Rewind.Enabled = True
                    Me.btnSPB_FastForward.Enabled = True
                Case ".pcm", ".wav"
                    StreamPlayer = New cPCMWAVPlayer
                    StreamPlayer.BuildGraph(Me.txtSPB_FilePath.Text, Me.Handle, StreamPlayback_UseVMR9, Me)
                    Me.btnSPB_Rewind.Enabled = False
                    Me.btnSPB_FastForward.Enabled = False
                Case ".dts", ".ac3"
                    StreamPlayer = New cDTSAC3Player()
                    StreamPlayer.BuildGraph(Me.txtSPB_FilePath.Text, Me.Handle, StreamPlayback_UseVMR9, Me)
                    Me.btnSPB_Rewind.Enabled = False
                    Me.btnSPB_FastForward.Enabled = False
                Case Else
                    Throw New Exception("File type: " & ext & " is not supported by Phoenix stream playback." & vbNewLine & "You could try changing the extension.")
            End Select

            If StreamPlayer Is Nothing Then Throw New Exception("Unexpected.")

            Me.btnSPB_Play.Text = "Play"

            Dim tDuration As Long
            Me.tbSPB_PlayPosition.Maximum = StreamPlayer.Duration_InReferenceTime / 10000000
            Me.tbSPB_PlayPosition.TickFrequency = Me.tbSPB_PlayPosition.Maximum / 100
            Me.tbSPB_PlayPosition.LargeChange = 10
            Me.tbSPB_PlayPosition.SmallChange = 1
            Me.tbSPB_PlayPosition.Value = 0

            tDuration = StreamPlayer.Duration_InReferenceTime / 10000000 'Get total seconds

            Dim tmp As String
            Dim dh As String
            If tDuration > 3600 Then
                tmp = CStr(tDuration / 3600)
                dh = MakeTwoDig(Microsoft.VisualBasic.Left(tmp, 1))
                tDuration -= (dh * 3600)
            Else
                dh = "00"
            End If

            Dim dm As String
            If tDuration > 60 Then
                tmp = CStr(tDuration / 60)
                Dim t() As String = Split(tmp, ".", -1, CompareMethod.Text)
                dm = MakeTwoDig(t(0))
                tDuration -= (dm * 60)
            Else
                dm = "00"
            End If

            Dim ds As String = MakeTwoDig(tDuration)
            lblSPB_Duration.Text = dh & ":" & dm & ":" & ds
            Me.lblSPB_Position.Text = "00:00:00"
            Return True
        Catch ex As Exception
            Me.AddConsoleLine(eConsoleItemType.ERROR, "Problem with SetupStreamPlayer(). Error: " & ex.Message)
        End Try
    End Function

    Private Enum eStreamPlaybackState
        Not_Initialized
        Ready_To_Play
        Playing
        Stopped
        Paused
        FF_RW
    End Enum

#End Region 'PANELS:STREAM PLAYBACK *FUNTIONALITY*

#Region "PANELS:STREAM PLAYBACK: PLAYER EVENT HANDLING"

    Private Sub SPB_HandleStreamTransportEvent(ByVal Type As eTransportControlTypes) Handles StreamPlayer.evTransportControl
        Try
            Select Case Type
                Case eTransportControlTypes.Play
                    Me.btnSPB_Play.Text = "Pause"
                    Me.StreamPlaybackTimer.Start()
                Case eTransportControlTypes.Pause
                    Me.btnSPB_Play.Text = "Play"
                    Me.StreamPlaybackTimer.Stop()
                Case eTransportControlTypes.Stop
                    Me.btnSPB_Play.Text = "Play"
                    Me.StreamPlaybackTimer.Stop()
                Case eTransportControlTypes.FastForward
                    Me.btnSPB_Play.Text = "Play"
                    Me.StreamPlaybackTimer.Start()
                Case eTransportControlTypes.Rewind
                    Me.btnSPB_Play.Text = "Play"
                    Me.StreamPlaybackTimer.Start()
            End Select
        Catch ex As Exception
            Me.AddConsoleLine(eConsoleItemType.ERROR, "Problem with SPB_HandleStreamTransportEvent(). Error: " & ex.Message)
        End Try
    End Sub

    Private Sub SPB_HandleStreamEnd() Handles StreamPlayer.evStreamEnd
        Me.StreamPlaybackTimer.Stop()
        Me.StreamPlayer.Dispose()
        Me.StreamPlayer = Nothing
        StreamPlaybackState = eStreamPlaybackState.Not_Initialized
        Me.txtSPB_FilePath.Text = ""
        Me.txtSPB_TimeSearch_s.Text = "00"
        Me.txtSPB_TimeSearch_m.Text = "00"
        Me.txtSPB_TimeSearch_h.Text = "00"
        Me.lblSPB_Position.Text = "00:00:00"
        Me.lblSPB_Duration.Text = "00:00:00"
        Me.tbSPB_PlayPosition.Value = "0"
        Me.gbPBControl.Enabled = False
        XtraMessageBox.Show(Me.LookAndFeel, Me, "Stream ended. Please select a new stream or click the ""Return to DVD Mode"" button.", My.Settings.APPLICATION_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

#End Region 'PANELS:STREAM PLAYBACK: PLAYER EVENT HANDLING

#End Region 'PANELS:STREAM PLAYBACK

#Region "PANELS:OPTIONS"

#Region "PANELS:OPTIONS *FUNCTIONALITY*"

    Public CurrentConfig As cEmulatorConfig

    Public Sub SetupPlayerConfigPropertyGrid()
        Try
            'Me.dpOptions.Controls.Clear()
            'OptionsPropertyGrid = New PropertyGrid
            'OptionsPropertyGrid.PropertySort = PropertySort.Alphabetical Or PropertySort.Categorized

            'Me.pgOptions = New DevExpress.XtraVerticalGrid.PropertyGridControl
            'pgOptions.LookAndFeel.SkinName = "Office 2007 Black"
            'pgOptions.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin

            ''CPG.PropertySort = PropertySort.Categorized
            ''CPG.ToolbarVisible = False
            ''CPG.CommandsVisibleIfAvailable = False
            ''CPG.HelpVisible = False
            ''CPG.CommandsVisible = False

            If CurrentConfig Is Nothing Then
                CurrentConfig = New cEmulatorConfig(Me)
            End If

            'OptionsPropertyGrid.SelectedObject = CurrentConfig
            'OptionsPropertyGrid.Dock = DockStyle.Fill
            ''CPG.Parent = Me.dpOptions
            'Me.dpOptions.Controls.Add(OptionsPropertyGrid)

            pgOptions.SelectedObject = CurrentConfig
            'OptionsPG.Dock = DockStyle.Fill
            'Me.dpOptions.Controls.Add(OptionsPG)

        Catch ex As Exception
            Me.AddConsoleLine(eConsoleItemType.ERROR, "Problem with SetupPlayerConfigPropertyGrid. Error: " & ex.Message)
        End Try
    End Sub

    Private Sub pgOptions_InvalidValueException(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.InvalidValueExceptionEventArgs) Handles pgOptions.InvalidValueException
        e.ExceptionMode = ExceptionMode.Ignore
        Me.KH.UnpauseHook()
        Me.OptionsTimer.Stop()
        Me.pgOptions.CloseEditor()
    End Sub

    Private Sub pgOptions_FocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraVerticalGrid.Events.FocusedRowChangedEventArgs) Handles pgOptions.FocusedRowChanged
        Try
            Dim tStr As String = Microsoft.VisualBasic.Right(e.Row.Name, e.Row.Name.Length - 3)
            Select Case tStr
                Case "MultiFrameCount", "JumpSeconds", "RecentProjectCount"
                    Me.KH.PauseHook()
                    Dim dlg As New dlgGenericInput(Me, tStr)
                    If dlg.ShowDialog = DialogResult.OK Then
                        Me.pgOptions.SetCellValue(Me.pgOptions.FocusedRow, Me.pgOptions.FocusedRecordCellIndex, dlg.USER_INPUT)
                    End If
                    Me.pgOptions.CloseEditor()
                    Me.pgOptions.FocusFirst()
                    Me.KH.UnpauseHook()
            End Select
        Catch ex As Exception
            Me.AddConsoleLine(eConsoleItemType.ERROR, "Problem with options grid property selection changed. Error: " & ex.Message, Nothing)
        End Try
    End Sub

#End Region 'PANELS:OPTIONS *FUNCTIONALITY*

#End Region 'PANELS:OPTIONS

#Region "PANELS:PROJECT SELECTOR"

#Region "PANELS:PROJECT SELECTOR:EVENTS"

    Private Sub btnPS_BootDisc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPS_Reboot.Click
        Me.RebootProject()
    End Sub

    Private Sub btnPS_Eject_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPS_Eject.Click
        Me.EjectProject()
    End Sub

    Private Sub btnPS_Browse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPS_Browse.Click
        SelectProject_WithDialog()
    End Sub

    Private Sub btnPS_ClearRecentProjects_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPS_Clear.Click
        Try
            If XtraMessageBox.Show(Me.LookAndFeel, Me, "Are you sure you want to remove all recent projects?", My.Settings.APPLICATION_NAME, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then Exit Sub
            Me.ClearRecentProjects()
            Me.lbPS_RecentProjects.Items.Clear()
        Catch ex As Exception
            Me.AddConsoleLine(eConsoleItemType.ERROR, "Problem clearing recent projects. Error: " & ex.Message)
        End Try
    End Sub

    Private Sub btnPS_Run_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPS_Run.Click
        RunSelectedDisc()
    End Sub

    Private Sub lbPS_RecentProjects_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbPS_RecentProjects.DoubleClick
        RunSelectedDisc()
    End Sub

#End Region 'PANELS:PROJECT SELECTOR:EVENTS

#Region "PANELS:PROJECT SELECTOR:CONTEXT MENU"

    Private Sub mi_cmProjectPath_Copy_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Clipboard.SetDataObject(lblProjectPath.Text)
    End Sub

#End Region 'PANELS:PROJECT SELECTOR:CONTEXT MENU

#Region "PANELS:PROJECT SELECTOR *FUNCTIONALITY*"

    Private Sub RunSelectedDisc()
        Try
            If Me.lbPS_RecentProjects.SelectedItem Is Nothing Then Exit Sub
            Me.RunProject(Me.lbPS_RecentProjects.SelectedItem.ToString)
        Catch ex As Exception
            Me.AddConsoleLine(eConsoleItemType.ERROR, "Problem with RunSelectedDisc(). Error: " & ex.Message)
        End Try
    End Sub

    Public Sub LoadRecentProjectsIntoProjectSelector()
        Try
            If Me.CurrentUserProfile.AppOptions.RecentProjectCount = 0 Then
NoRecentProjects:
                Me.lbPS_RecentProjects.Items.Add("No Recent Projects")
                Exit Sub
            Else
                'remove current 
                Me.lbPS_RecentProjects.Items.Clear()

                Dim tRecentProjects() As String = Me.RecentProjects
                If tRecentProjects Is Nothing Then Exit Sub

                For Each RP As String In tRecentProjects
                    If Not RP Is Nothing AndAlso Not RP = "" Then
                        Me.lbPS_RecentProjects.Items.Add(RP.ToUpper)
                    End If
                Next
            End If

            'check to see if any items were added 
            If Me.lbPS_RecentProjects.Items.Count = 0 Then GoTo NoRecentProjects

        Catch ex As Exception
            Me.AddConsoleLine(eConsoleItemType.ERROR, "Problem with LoadRecentProjects(). Error: " & ex.Message)
        End Try
    End Sub

#End Region 'PANELS:PROJECT SELECTOR *FUNCTIONALITY*

#End Region 'PANELS:PROJECT SELECTOR

#Region "PANELS:CONSOLE"

#Region "PANELS:CONSOLE *FUNCTIONALITY*"

    Private ConsoleItems As colConsoleItem
    Private PauseLogPrint As Boolean = False

    Public Sub AddConsoleLine(ByVal Type As eConsoleItemType, ByVal Msg As String, Optional ByVal ExtendedInfo As String = "", Optional ByVal GOPTC As cTimecode = Nothing)
        'Debug.WriteLine(Msg)

        If Player IsNot Nothing AndAlso Player.IsInitialized Then
            Dim tGTC As cTimecode = Player.LastGOPTC
            If Not GOPTC Is Nothing Then
                tGTC = GOPTC
            End If
            AddConsoleItem(New cConsoleItem(Type, Msg, ExtendedInfo, New cConsoleItemPlayLocation(Player.CurrentPlayLocation, tGTC)))
        Else
            AddConsoleItem(New cConsoleItem(Type, Msg, ExtendedInfo, Nothing))
        End If

    End Sub

    Private Function AddConsoleItem(ByVal CI As cConsoleItem) As Boolean
        Try
            If Not Me.CurrentUserProfile Is Nothing AndAlso Not Me.CurrentUserProfile.AppOptions Is Nothing AndAlso Not Me.Player Is Nothing Then
                If (CI.Type = eConsoleItemType.EVENT And Not Me.CurrentUserProfile.AppOptions.LogEvents) Or (Player.PlayState = ePlayState.SystemJP And CI.Type = eConsoleItemType.EVENT) Or (Player.PlayState = ePlayState.SystemJP And CI.Type = eConsoleItemType.UOPCHANGE) Then Exit Function
            End If

            If CI.Msg = "2" Then
                Debug.WriteLine("Unexpected in AddConsoleItem().")
            End If

            'If ListItems Is Nothing Then
            '    ReDim ListItems(-1)
            '    'LVIC = New ListViewItemComparer
            'End If

            Dim LVI As New ListViewItem(Me.lvConsole.Items.Count)
            LVI.SubItems.Add(CI.Type.ToString)
            LVI.SubItems.Add(CI.Msg)
            LVI.SubItems.Add(CI.ExtendedInfo)
            If CI.Location IsNot Nothing Then
                LVI.SubItems.Add(CI.Location.ToString)
            End If

            Select Case CI.Type
                Case eConsoleItemType.ERROR
                    LVI.BackColor = Color.LightPink
                    LVI.Tag = Me.miConsole_Filter_ERRORS.Checked
                    Me.dpConsole.Show()
                Case eConsoleItemType.EVENT
                    LVI.BackColor = Color.PowderBlue
                    LVI.Tag = Me.miConsole_Filter_EVENTS.Checked
                Case eConsoleItemType.NOTICE
                    LVI.Tag = Me.miConsole_Filter_NOTICES.Checked
                    LVI.BackColor = Color.LightGray
                Case eConsoleItemType.WARNING
                    LVI.BackColor = Color.LightYellow
                    LVI.Tag = Me.miConsole_Filter_WARNINGS.Checked
                Case eConsoleItemType.CLOSEDCAPTION
                    LVI.BackColor = Color.LightGreen
                    LVI.Tag = Me.miConsole_Filter_CLOSEDCAPTIONS.Checked
                Case eConsoleItemType.UOPCHANGE
                    LVI.BackColor = Color.LightCyan
                    LVI.Tag = Me.miConsole_Filter_UOPCHANGES.Checked
                Case eConsoleItemType.MACRO
                    LVI.BackColor = Color.Aquamarine
                    LVI.Tag = True
            End Select

            Me.ConsoleItems.Add(LVI)
            Me.UpdateConsole(True)

            'Me.lvConsole.Items.Add(LVI)
            'Me.lvConsole.EnsureVisible(Me.lvConsole.Items.Count - 1)

            Return True
        Catch ex As Exception
            Debug.WriteLine("Problem adding line to console. Error: " & ex.Message)
            Return False
        End Try
    End Function

    Public Sub UpdateConsole(ByVal DoAll As Boolean)
        Try
            If PauseLogPrint Then Exit Sub
            If DoAll Then
                Me.lvConsole.Items.Clear()
                For Each LVI As ListViewItem In Me.ConsoleItems
                    If LVI.Tag = True Then
                        Me.lvConsole.Items.Add(LVI)
                    End If
                Next
            Else
                Me.lvConsole.Items.Add(Me.ConsoleItems.Item(Me.ConsoleItems.Count - 1))
            End If
            If Me.lvConsole.Items.Count > 0 Then Me.lvConsole.EnsureVisible(Me.lvConsole.Items.Count - 1)
            Me.lvConsole.Refresh()
        Catch ex As Exception
            Debug.Write(eConsoleItemType.ERROR, "Problem with MarkersListView_Update. Error: " & ex.Message)
        End Try
    End Sub

    Public Sub FilterConsole(ByVal Type As eConsoleItemType, ByVal Show As Boolean)
        Try
            For Each LVI As ListViewItem In Me.ConsoleItems
                If LVI.SubItems(1).Text = Type.ToString Then
                    LVI.Tag = Show
                End If
            Next
            UpdateConsole(True)
        Catch ex As Exception
            Me.AddConsoleLine(eConsoleItemType.ERROR, "Problem with FilterListView. Error: " & ex.Message)
        End Try
    End Sub

    Public Function ClearConsole() As Boolean
        Me.lvConsole.Items.Clear()
        Me.ConsoleItems.Clear()
    End Function

    Public Function DVDPlayLocToString(ByVal PL As DvdPlayLocation) As String
        Try
            If PL.TitleNum = Nothing Then Return "N/A"
            If PL.TitleNum = -1 Then Return "Non-Title Domain"
            Dim sb As New StringBuilder
            sb.Append("TT: " & PL.TitleNum & " ")
            sb.Append("PTT: " & PL.ChapterNum & " ")
            sb.Append("TC: " & DVDTimeCodeToString(PL.timeCode))
            Return sb.ToString
        Catch ex As Exception
            AddConsoleItem(New cConsoleItem(eConsoleItemType.ERROR, ex.Message, "", Nothing))
        End Try
    End Function

    Public Function StringToDVDPlayLoc(ByVal PL As String) As DvdPlayLocation
        Try
            'TT: 1 PTT: 2 TC: 00:00:43;15
            Dim s() As String = Split(PL, " ") ' break it down by spaces
            Dim out As New DvdPlayLocation
            If s(0) = "Non-Title" Then
                out.TitleNum = -1
                Return out
            End If
            out.TitleNum = s(1)
            out.ChapterNum = s(3)
            s = Split(s(5), ":")
            Dim tc As New DvdTimeCode
            tc.bHours = s(0)
            tc.bMinutes = s(1)
            s = Split(s(2), ";")
            tc.bSeconds = s(0)
            tc.bFrames = s(1)
            Dim tTC As New cTimecode(tc, (Player.CurrentVideoStandard = eVideoStandard.NTSC))
            tTC = SubtractTimecode(tTC, New cTimecode(0, 0, 5, 0, (Player.CurrentVideoStandard = eVideoStandard.NTSC)), 30)
            out.timeCode = tTC.DVDTimeCode
            Return out
        Catch ex As Exception
            AddConsoleItem(New cConsoleItem(eConsoleItemType.ERROR, ex.Message, "", Nothing))
        End Try
    End Function

    Public Function DVDTimeCodeToString(ByVal TC As DvdTimeCode) As String
        Try
            'If TC Is Nothing Then Return ""
            Dim SB As New StringBuilder
            SB.Append(PadString(TC.bHours, 2, "0", True) & ":")
            SB.Append(PadString(TC.bMinutes, 2, "0", True) & ":")
            SB.Append(PadString(TC.bSeconds, 2, "0", True) & ";")
            SB.Append(PadString(TC.bFrames, 2, "0", True))
            Return SB.ToString
        Catch ex As Exception
            AddConsoleItem(New cConsoleItem(eConsoleItemType.ERROR, ex.Message, "", Nothing))
        End Try
    End Function

    Public Function LVtoString() As String
        Try
            Dim sb As New StringBuilder
            sb.Append("CONSOLE SNAPSHOT" & vbNewLine)
            sb.Append("Taken: " & DateTime.Now.ToString & vbNewLine)
            sb.Append("On " & My.Settings.APPLICATION_NAME & ": " & vbNewLine)
            sb.Append("-----------------------------------------------------------" & vbNewLine)
            sb.Append("-----------------------------------------------------------" & vbNewLine)
            For Each LVI As ListViewItem In Me.lvConsole.Items
                sb.Append("TYPE:" & vbNewLine & LVI.SubItems(1).Text & vbNewLine)
                sb.Append(vbNewLine)
                sb.Append("MESSAGE:" & vbNewLine & LVI.SubItems(2).Text & vbNewLine)
                sb.Append(vbNewLine)
                sb.Append("EXTENDED INFO:" & vbNewLine & LVI.SubItems(3).Text & vbNewLine)
                sb.Append(vbNewLine)
                If LVI.SubItems.Count = 5 Then
                    sb.Append("LOCATION:" & vbNewLine & LVI.SubItems(4).Text & vbNewLine)
                    sb.Append("-----------------------------------------------------------" & vbNewLine)
                    sb.Append(vbNewLine)
                End If
            Next
            Return sb.ToString
        Catch ex As Exception
            Me.AddConsoleLine(eConsoleItemType.ERROR, "Problem with LVtoString. Error: " & ex.Message, ex.StackTrace)
            Return ""
        End Try
    End Function

    Private Sub GoToEvent(ByVal LVI As ListViewItem)
        Try
            Dim s() As String = Split(LVI.SubItems(4).Text, "|")
            If InStr(s(0).ToLower, "source") Or s(0) = "" Then
                Exit Sub 'we don't have a dvd playlocation that we can use here
            End If
            Dim PL As DvdPlayLocation = StringToDVDPlayLoc(s(0))
            If PL.TitleNum < 0 Then Exit Sub
            Player.PlayAtTimeInTitle(PL.timeCode, PL.TitleNum)
        Catch ex As Exception
            Me.AddConsoleLine(eConsoleItemType.ERROR, "Problem with GoToEvent. Error: " & ex.Message)
        End Try
    End Sub

#End Region 'PANELS:CONSOLE *FUNCTIONALITY*

#Region "PANELS:CONSOLE:EVENTS"

    Private Sub lvConsole_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvConsole.DoubleClick
        Dim lvi As ListViewItem = Me.lvConsole.SelectedItems.Item(0)
        If Not lvi Is Nothing Then
            GoToEvent(lvi)
        End If
    End Sub

#End Region 'PANELS:CONSOLE:EVENTS

#Region "PANELS:CONSOLE:CONTEXT MENU"

    Private Sub miConsole_Clear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miConsole_Clear.Click
        ClearConsole()
    End Sub

    Private Sub miConsole_Save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miConsole_Save.Click
        Try
            Me.KH.PauseHook()
            Dim dlg As New SaveFileDialog
            dlg.Filter = "Text File (.txt) | *.txt"
            dlg.Title = "Save Console Text"
            dlg.AddExtension = True
            dlg.DefaultExt = ".txt"
            dlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            dlg.OverwritePrompt = True
            If dlg.ShowDialog = DialogResult.OK Then
                Dim file_name As String = dlg.FileName
                Dim stream_writer As New IO.StreamWriter(file_name, False)
                'stream_writer.Write(Me.txtConsole.Text)
                stream_writer.Write(LVtoString)
                stream_writer.Close()
            End If
            Me.KH.UnpauseHook()
        Catch ex As Exception
            Me.KH.UnpauseHook()
            Me.AddConsoleItem(New cConsoleItem(eConsoleItemType.ERROR, "Problem saving console. Error: " & ex.Message, "", Nothing))
        End Try
    End Sub

    Private Sub miConsole_Copy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miConsole_Copy.Click
        Clipboard.SetDataObject(LVtoString)
    End Sub

    Private Sub miConsole_PauseLogging_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles miConsole_PauseLogging.Click
        If Me.miConsole_PauseLogging.Checked Then
            Me.miConsole_PauseLogging.Checked = False
            PauseLogPrint = False
        Else
            Me.miConsole_PauseLogging.Checked = True
            PauseLogPrint = True
        End If
    End Sub

    Private Sub miConsole_Filter_EVENTS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miConsole_Filter_EVENTS.Click
        If Me.miConsole_Filter_EVENTS.Checked Then
            Me.FilterConsole(eConsoleItemType.EVENT, False)
            Me.miConsole_Filter_EVENTS.Checked = False
        Else
            Me.FilterConsole(eConsoleItemType.EVENT, True)
            Me.miConsole_Filter_EVENTS.Checked = True
        End If
    End Sub

    Private Sub miConsole_Filter_WARNINGS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miConsole_Filter_WARNINGS.Click
        If Me.miConsole_Filter_WARNINGS.Checked Then
            Me.FilterConsole(eConsoleItemType.WARNING, False)
            Me.miConsole_Filter_WARNINGS.Checked = False
        Else
            Me.FilterConsole(eConsoleItemType.WARNING, True)
            Me.miConsole_Filter_WARNINGS.Checked = True
        End If
    End Sub

    Private Sub miConsole_Filter_ERRORS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miConsole_Filter_ERRORS.Click
        If Me.miConsole_Filter_ERRORS.Checked Then
            Me.FilterConsole(eConsoleItemType.ERROR, False)
            Me.miConsole_Filter_ERRORS.Checked = False
        Else
            Me.FilterConsole(eConsoleItemType.ERROR, True)
            Me.miConsole_Filter_ERRORS.Checked = True
        End If
    End Sub

    Private Sub miConsole_Filter_CLOSEDCAPTIONS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miConsole_Filter_CLOSEDCAPTIONS.Click
        If Me.miConsole_Filter_CLOSEDCAPTIONS.Checked Then
            Me.FilterConsole(eConsoleItemType.CLOSEDCAPTION, False)
            Me.miConsole_Filter_CLOSEDCAPTIONS.Checked = False
        Else
            Me.FilterConsole(eConsoleItemType.CLOSEDCAPTION, True)
            Me.miConsole_Filter_CLOSEDCAPTIONS.Checked = True
        End If
    End Sub

    Private Sub miConsole_Filter_NOTICES_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miConsole_Filter_NOTICES.Click
        If Me.miConsole_Filter_NOTICES.Checked Then
            Me.FilterConsole(eConsoleItemType.NOTICE, False)
            Me.miConsole_Filter_NOTICES.Checked = False
        Else
            Me.FilterConsole(eConsoleItemType.NOTICE, True)
            Me.miConsole_Filter_NOTICES.Checked = True
        End If
    End Sub

    Private Sub miConsole_Filter_UOPCHANGES_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miConsole_Filter_UOPCHANGES.Click
        If Me.miConsole_Filter_UOPCHANGES.Checked Then
            Me.FilterConsole(eConsoleItemType.UOPCHANGE, False)
            Me.miConsole_Filter_UOPCHANGES.Checked = False
        Else
            Me.FilterConsole(eConsoleItemType.UOPCHANGE, True)
            Me.miConsole_Filter_UOPCHANGES.Checked = True
        End If
    End Sub

#End Region 'PANELS:CONSOLE:CONTEXT MENU

#End Region 'PANELS:CONSOLE

#Region "PANELS:GPRM VIEWER"

    Private Sub UpdateWatchValues()
        Try
            If Not FeatureManagement.Features.FE_OT_GPRMViewer Then Exit Sub
            'WATCH 1
            If Me.seGV_L_1.Value > 0 Then
                Me.txtGV_Val_1.Text = (Player.GPRM(Me.seGV_G_1.Value) >> Me.seGV_O_1.Value) And BinMaxValue(Me.seGV_L_1.Value)
                Me.rtbGV_Bits_1.Text = DecToBin(Player.GPRM(Me.seGV_G_1.Value)).PadLeft(16, "0")
                Me.rtbGV_Bits_1.SelectionStart = 16 - Me.seGV_O_1.Value - Me.seGV_L_1.Value
                Me.rtbGV_Bits_1.SelectionLength = Me.seGV_L_1.Value
                Me.rtbGV_Bits_1.SelectionColor = Color.Red
            Else
                Me.txtGV_Val_1.Text = 0
                Me.rtbGV_Bits_1.Text = CStr("0").PadLeft(16, "0")
                Me.rtbGV_Bits_1.SelectionLength = 0
            End If
            'WATCH 2
            If Me.seGV_L_2.Value > 0 Then
                Me.txtGV_Val_2.Text = (Player.GPRM(Me.seGV_G_2.Value) >> Me.seGV_O_2.Value) And BinMaxValue(Me.seGV_L_2.Value)
                Me.rtbGV_Bits_2.Text = DecToBin(Player.GPRM(Me.seGV_G_2.Value)).PadLeft(16, "0")
                Me.rtbGV_Bits_2.SelectionStart = 16 - Me.seGV_O_2.Value - Me.seGV_L_2.Value
                Me.rtbGV_Bits_2.SelectionLength = Me.seGV_L_2.Value
                Me.rtbGV_Bits_2.SelectionColor = Color.Red
            Else
                Me.txtGV_Val_2.Text = 0
                Me.rtbGV_Bits_2.Text = CStr("0").PadLeft(16, "0")
                Me.rtbGV_Bits_2.SelectionLength = 0
            End If
            'WATCH 3
            If Me.seGV_L_3.Value > 0 Then
                Me.txtGV_Val_3.Text = (Player.GPRM(Me.seGV_G_3.Value) >> Me.seGV_O_3.Value) And BinMaxValue(Me.seGV_L_3.Value)
                Me.rtbGV_Bits_3.Text = DecToBin(Player.GPRM(Me.seGV_G_3.Value)).PadLeft(16, "0")
                Me.rtbGV_Bits_3.SelectionStart = 16 - Me.seGV_O_3.Value - Me.seGV_L_3.Value
                Me.rtbGV_Bits_3.SelectionLength = Me.seGV_L_3.Value
                Me.rtbGV_Bits_3.SelectionColor = Color.Red
            Else
                Me.txtGV_Val_3.Text = 0
                Me.rtbGV_Bits_3.Text = CStr("0").PadLeft(16, "0")
                Me.rtbGV_Bits_3.SelectionLength = 0
            End If
            'WATCH 4
            If Me.seGV_L_4.Value > 0 Then
                Me.txtGV_Val_4.Text = (Player.GPRM(Me.seGV_G_4.Value) >> Me.seGV_O_4.Value) And BinMaxValue(Me.seGV_L_4.Value)
                Me.rtbGV_Bits_4.Text = DecToBin(Player.GPRM(Me.seGV_G_4.Value)).PadLeft(16, "0")
                Me.rtbGV_Bits_4.SelectionStart = 16 - Me.seGV_O_4.Value - Me.seGV_L_4.Value
                Me.rtbGV_Bits_4.SelectionLength = Me.seGV_L_4.Value
                Me.rtbGV_Bits_4.SelectionColor = Color.Red
            Else
                Me.txtGV_Val_4.Text = 0
                Me.rtbGV_Bits_4.Text = CStr("0").PadLeft(16, "0")
                Me.rtbGV_Bits_4.SelectionLength = 0
            End If
            'WATCH 5
            If Me.seGV_L_5.Value > 0 Then
                Me.txtGV_Val_5.Text = (Player.GPRM(Me.seGV_G_5.Value) >> Me.seGV_O_5.Value) And BinMaxValue(Me.seGV_L_5.Value)
                Me.rtbGV_Bits_5.Text = DecToBin(Player.GPRM(Me.seGV_G_5.Value)).PadLeft(16, "0")
                Me.rtbGV_Bits_5.SelectionStart = 16 - Me.seGV_O_5.Value - Me.seGV_L_5.Value
                Me.rtbGV_Bits_5.SelectionLength = Me.seGV_L_5.Value
                Me.rtbGV_Bits_5.SelectionColor = Color.Red
            Else
                Me.txtGV_Val_5.Text = 0
                Me.rtbGV_Bits_5.Text = CStr("0").PadLeft(16, "0")
                Me.rtbGV_Bits_5.SelectionLength = 0
            End If
            'WATCH 6
            If Me.seGV_L_6.Value > 0 Then
                Me.txtGV_Val_6.Text = (Player.GPRM(Me.seGV_G_6.Value) >> Me.seGV_O_6.Value) And BinMaxValue(Me.seGV_L_6.Value)
                Me.rtbGV_Bits_6.Text = DecToBin(Player.GPRM(Me.seGV_G_6.Value)).PadLeft(16, "0")
                Me.rtbGV_Bits_6.SelectionStart = 16 - Me.seGV_O_6.Value - Me.seGV_L_6.Value
                Me.rtbGV_Bits_6.SelectionLength = Me.seGV_L_6.Value
                Me.rtbGV_Bits_6.SelectionColor = Color.Red
            Else
                Me.txtGV_Val_6.Text = 0
                Me.rtbGV_Bits_6.Text = CStr("0").PadLeft(16, "0")
                Me.rtbGV_Bits_6.SelectionLength = 0
            End If
            'WATCH 7
            If Me.seGV_L_7.Value > 0 Then
                Me.txtGV_Val_7.Text = (Player.GPRM(Me.seGV_G_7.Value) >> Me.seGV_O_7.Value) And BinMaxValue(Me.seGV_L_7.Value)
                Me.rtbGV_Bits_7.Text = DecToBin(Player.GPRM(Me.seGV_G_7.Value)).PadLeft(16, "0")
                Me.rtbGV_Bits_7.SelectionStart = 16 - Me.seGV_O_7.Value - Me.seGV_L_7.Value
                Me.rtbGV_Bits_7.SelectionLength = Me.seGV_L_7.Value
                Me.rtbGV_Bits_7.SelectionColor = Color.Red
            Else
                Me.txtGV_Val_7.Text = 0
                Me.rtbGV_Bits_7.Text = CStr("0").PadLeft(16, "0")
                Me.rtbGV_Bits_7.SelectionLength = 0
            End If

            'Set display mode
            Select Case CurrentRegViewMode
                Case eRegViewType.ASCII
                    Me.txtGV_Val_1.Text = DECtoASCII(Me.txtGV_Val_1.Text)
                    Me.txtGV_Val_2.Text = DECtoASCII(Me.txtGV_Val_2.Text)
                    Me.txtGV_Val_3.Text = DECtoASCII(Me.txtGV_Val_3.Text)
                    Me.txtGV_Val_4.Text = DECtoASCII(Me.txtGV_Val_4.Text)
                    Me.txtGV_Val_5.Text = DECtoASCII(Me.txtGV_Val_5.Text)
                    Me.txtGV_Val_6.Text = DECtoASCII(Me.txtGV_Val_6.Text)
                    Me.txtGV_Val_7.Text = DECtoASCII(Me.txtGV_Val_7.Text)
                Case eRegViewType.Dec
                    'do nothing
                Case eRegViewType.Hex
                    Me.txtGV_Val_1.Text = DecToHex(Me.txtGV_Val_1.Text)
                    Me.txtGV_Val_2.Text = DecToHex(Me.txtGV_Val_2.Text)
                    Me.txtGV_Val_3.Text = DecToHex(Me.txtGV_Val_3.Text)
                    Me.txtGV_Val_4.Text = DecToHex(Me.txtGV_Val_4.Text)
                    Me.txtGV_Val_5.Text = DecToHex(Me.txtGV_Val_5.Text)
                    Me.txtGV_Val_6.Text = DecToHex(Me.txtGV_Val_6.Text)
                    Me.txtGV_Val_7.Text = DecToHex(Me.txtGV_Val_7.Text)
                Case eRegViewType.Bin
                    Me.txtGV_Val_1.Text = DecToBin(CInt(Me.txtGV_Val_1.Text))
                    Me.txtGV_Val_2.Text = DecToBin(CInt(Me.txtGV_Val_2.Text))
                    Me.txtGV_Val_3.Text = DecToBin(CInt(Me.txtGV_Val_3.Text))
                    Me.txtGV_Val_4.Text = DecToBin(CInt(Me.txtGV_Val_4.Text))
                    Me.txtGV_Val_5.Text = DecToBin(CInt(Me.txtGV_Val_5.Text))
                    Me.txtGV_Val_6.Text = DecToBin(CInt(Me.txtGV_Val_6.Text))
                    Me.txtGV_Val_7.Text = DecToBin(CInt(Me.txtGV_Val_7.Text))
            End Select

        Catch ex As Exception
            Throw New Exception("Problem with SetGPRMWatchValue(). Error: " & ex.Message, ex)
        End Try
    End Sub

    Private Function BinMaxValue(ByVal BitCount As Byte) As UInt16
        Dim out As UInt16 = 0
        For i As Integer = 0 To BitCount - 1
            out = out Or 1 << i
        Next
        Return out
    End Function

    Private Sub seGV_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles seGV_G_1.EditValueChanged, seGV_L_1.EditValueChanged, seGV_O_1.EditValueChanged, seGV_G_2.EditValueChanged, seGV_L_2.EditValueChanged, seGV_O_2.EditValueChanged, seGV_G_3.EditValueChanged, seGV_L_3.EditValueChanged, seGV_O_3.EditValueChanged, seGV_G_4.EditValueChanged, seGV_L_4.EditValueChanged, seGV_O_4.EditValueChanged, seGV_G_5.EditValueChanged, seGV_L_5.EditValueChanged, seGV_O_5.EditValueChanged, seGV_G_6.EditValueChanged, seGV_L_6.EditValueChanged, seGV_O_6.EditValueChanged, seGV_G_7.EditValueChanged, seGV_L_7.EditValueChanged, seGV_O_7.EditValueChanged
        UpdateWatchValues()
    End Sub

    Private Sub rbGPRMViewer_dec_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbGPRMViewer_dec.CheckedChanged
        If Not Me.rbGPRMViewer_dec.Checked Then Exit Sub
        Me.rbGPRMs_ViewType_dec.Checked = True
    End Sub

    Private Sub rbGPRMViewer_hex_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbGPRMViewer_hex.CheckedChanged
        If Not Me.rbGPRMViewer_hex.Checked Then Exit Sub
        Me.rbGPRMs_ViewType_hex.Checked = True
    End Sub

    Private Sub rbGPRMViewer_asc_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbGPRMViewer_asc.CheckedChanged
        If Not Me.rbGPRMViewer_asc.Checked Then Exit Sub
        Me.rbGPRMs_ViewType_asc.Checked = True
    End Sub

    Private Sub txtGV_WatchName_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtGV_WatchName_1.GotFocus, txtGV_WatchName_2.GotFocus, txtGV_WatchName_3.GotFocus, txtGV_WatchName_4.GotFocus, txtGV_WatchName_5.GotFocus, txtGV_WatchName_6.GotFocus
        KH.PauseHook()
    End Sub

    Private Sub txtGV_WatchName_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtGV_WatchName_1.LostFocus, txtGV_WatchName_2.LostFocus, txtGV_WatchName_3.LostFocus, txtGV_WatchName_4.LostFocus, txtGV_WatchName_5.LostFocus, txtGV_WatchName_6.LostFocus
        KH.UnpauseHook()
    End Sub

    Private Sub btnGV_ClearWatches_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGV_ClearWatches.Click
        ClearGPRMWatches()
    End Sub

    Private Sub ClearGPRMWatches()
        Me.txtGV_Val_1.Text = 0
        Me.txtGV_Val_2.Text = 0
        Me.txtGV_Val_3.Text = 0
        Me.txtGV_Val_4.Text = 0
        Me.txtGV_Val_5.Text = 0
        Me.txtGV_Val_6.Text = 0
        Me.txtGV_Val_7.Text = 0

        Me.seGV_G_1.Value = 0
        Me.seGV_G_2.Value = 0
        Me.seGV_G_3.Value = 0
        Me.seGV_G_4.Value = 0
        Me.seGV_G_5.Value = 0
        Me.seGV_G_6.Value = 0
        Me.seGV_G_7.Value = 0

        Me.seGV_L_1.Value = 0
        Me.seGV_L_2.Value = 0
        Me.seGV_L_3.Value = 0
        Me.seGV_L_4.Value = 0
        Me.seGV_L_5.Value = 0
        Me.seGV_L_6.Value = 0
        Me.seGV_L_7.Value = 0

        Me.seGV_O_1.Value = 0
        Me.seGV_O_2.Value = 0
        Me.seGV_O_3.Value = 0
        Me.seGV_O_4.Value = 0
        Me.seGV_O_5.Value = 0
        Me.seGV_O_6.Value = 0
        Me.seGV_O_7.Value = 0

        Me.txtGV_WatchName_1.Text = ""
        Me.txtGV_WatchName_2.Text = ""
        Me.txtGV_WatchName_3.Text = ""
        Me.txtGV_WatchName_4.Text = ""
        Me.txtGV_WatchName_5.Text = ""
        Me.txtGV_WatchName_6.Text = ""
        Me.txtGV_WatchName_7.Text = ""
    End Sub

    Private ReadOnly Property GPRMWatchSetSavePath() As String
        Get
            Dim app_data As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
            Dim watch_set_folder As String = app_data & "\watch_sets\"
            If Not Directory.Exists(watch_set_folder) Then Directory.CreateDirectory(watch_set_folder)
            Return watch_set_folder
        End Get
    End Property

    Private ReadOnly Property SelectedGPRMWatchSetName() As String
        Get
            If Me.cbSavedWatchSets.SelectedItem Is Nothing Then Return ""
            Return Me.cbSavedWatchSets.SelectedItem.ToString()
        End Get
    End Property

    Private Sub llGV_SaveWatchSet_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llGV_SaveWatchSet.LinkClicked
        Try
            Dim WS As New cGPRMWatchSet
            WS.Name = InputBox("Name for watch set: ", "Watch Set", "")
            If WS.Name = "" Then Exit Sub

            'WATCH 1
            If Me.seGV_L_1.Value > 0 Then
                WS.Items.Add(New cGPRMWatchSet.cGPRMWatchItem(Me.txtGV_WatchName_1.Text, Me.seGV_G_1.Value, Me.seGV_O_1.Value, Me.seGV_L_1.Value))
            End If
            'WATCH 2
            If Me.seGV_L_2.Value > 0 Then
                WS.Items.Add(New cGPRMWatchSet.cGPRMWatchItem(Me.txtGV_WatchName_2.Text, Me.seGV_G_2.Value, Me.seGV_O_2.Value, Me.seGV_L_2.Value))
            End If
            'WATCH 3
            If Me.seGV_L_3.Value > 0 Then
                WS.Items.Add(New cGPRMWatchSet.cGPRMWatchItem(Me.txtGV_WatchName_3.Text, Me.seGV_G_3.Value, Me.seGV_O_3.Value, Me.seGV_L_3.Value))
            End If
            'WATCH 4
            If Me.seGV_L_4.Value > 0 Then
                WS.Items.Add(New cGPRMWatchSet.cGPRMWatchItem(Me.txtGV_WatchName_4.Text, Me.seGV_G_4.Value, Me.seGV_O_4.Value, Me.seGV_L_4.Value))
            End If
            'WATCH 5
            If Me.seGV_L_5.Value > 0 Then
                WS.Items.Add(New cGPRMWatchSet.cGPRMWatchItem(Me.txtGV_WatchName_5.Text, Me.seGV_G_5.Value, Me.seGV_O_5.Value, Me.seGV_L_5.Value))
            End If
            'WATCH 6
            If Me.seGV_L_6.Value > 0 Then
                WS.Items.Add(New cGPRMWatchSet.cGPRMWatchItem(Me.txtGV_WatchName_6.Text, Me.seGV_G_6.Value, Me.seGV_O_6.Value, Me.seGV_L_6.Value))
            End If
            'WATCH 7
            If Me.seGV_L_7.Value > 0 Then
                WS.Items.Add(New cGPRMWatchSet.cGPRMWatchItem(Me.txtGV_WatchName_4.Text, Me.seGV_G_4.Value, Me.seGV_O_4.Value, Me.seGV_L_4.Value))
            End If

            If File.Exists(GPRMWatchSetSavePath & WS.Name & ".phoenix_gprm_watch_set") Then
                If XtraMessageBox.Show(Me.LookAndFeel, Me, "Are you sure that you want to overwrite set: " & WS.Name & "?", My.Settings.APPLICATION_NAME, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then Exit Sub
            End If
            SerializeToFile(WS, GPRMWatchSetSavePath & WS.Name & ".phoenix_gprm_watch_set")
            LoadSavedGPRMWatchSets()
            Me.cbSavedWatchSets.SelectedIndex = Me.cbSavedWatchSets.Properties.Items.IndexOf(WS.Name)
        Catch ex As Exception
            Me.AddConsoleLine(eConsoleItemType.ERROR, "Problem with SaveWatchSet(). Error: " & ex.Message)
        End Try
    End Sub

    Private Sub LoadSavedGPRMWatchSets()
        Try
            Dim Fs() As String = Directory.GetFiles(GPRMWatchSetSavePath)
            Me.cbSavedWatchSets.Properties.Items.Clear()
            If Fs.Length < 1 Then Exit Sub
            For Each f As String In Fs
                Me.cbSavedWatchSets.Properties.Items.Add(Path.GetFileNameWithoutExtension(f))
            Next
        Catch ex As Exception
            Me.AddConsoleLine(eConsoleItemType.ERROR, "Problem in LoadSavedWatchSets(). Error: " & ex.Message)
        End Try
    End Sub

    Private Sub ApplySavedWatchSet(ByVal Name As String)
        Try
            If Name = "" Then Exit Sub
            If Not File.Exists(GPRMWatchSetSavePath & Name & ".phoenix_gprm_watch_set") Then Throw New Exception("File not found.")
            Dim WS As cGPRMWatchSet = DeserializeFromFile(GetType(cGPRMWatchSet), GPRMWatchSetSavePath & Name & ".phoenix_gprm_watch_set")
            If WS.Items.Count = 0 Then Exit Sub
            ClearGPRMWatches()
            For i As Byte = 0 To WS.Items.Count - 1
                Select Case i
                    Case 0
                        Me.txtGV_WatchName_1.Text = WS.Items(i).Name
                        Me.seGV_G_1.Value = WS.Items(i).GPRM
                        Me.seGV_O_1.Value = WS.Items(i).Offset
                        Me.seGV_L_1.Value = WS.Items(i).Length
                    Case 1
                        Me.txtGV_WatchName_2.Text = WS.Items(i).Name
                        Me.seGV_G_2.Value = WS.Items(i).GPRM
                        Me.seGV_O_2.Value = WS.Items(i).Offset
                        Me.seGV_L_2.Value = WS.Items(i).Length
                    Case 2
                        Me.txtGV_WatchName_3.Text = WS.Items(i).Name
                        Me.seGV_G_3.Value = WS.Items(i).GPRM
                        Me.seGV_O_3.Value = WS.Items(i).Offset
                        Me.seGV_L_3.Value = WS.Items(i).Length
                    Case 3
                        Me.txtGV_WatchName_4.Text = WS.Items(i).Name
                        Me.seGV_G_4.Value = WS.Items(i).GPRM
                        Me.seGV_O_4.Value = WS.Items(i).Offset
                        Me.seGV_L_4.Value = WS.Items(i).Length
                    Case 4
                        Me.txtGV_WatchName_5.Text = WS.Items(i).Name
                        Me.seGV_G_5.Value = WS.Items(i).GPRM
                        Me.seGV_O_5.Value = WS.Items(i).Offset
                        Me.seGV_L_5.Value = WS.Items(i).Length
                    Case 5
                        Me.txtGV_WatchName_6.Text = WS.Items(i).Name
                        Me.seGV_G_6.Value = WS.Items(i).GPRM
                        Me.seGV_O_6.Value = WS.Items(i).Offset
                        Me.seGV_L_6.Value = WS.Items(i).Length
                    Case 6
                        Me.txtGV_WatchName_7.Text = WS.Items(i).Name
                        Me.seGV_G_7.Value = WS.Items(i).GPRM
                        Me.seGV_O_7.Value = WS.Items(i).Offset
                        Me.seGV_L_7.Value = WS.Items(i).Length
                End Select
            Next
        Catch ex As Exception
            Me.AddConsoleLine(eConsoleItemType.ERROR, "Problem in ApplySavedGPRMWatchSet(). Error: " & ex.Message)
        End Try
    End Sub

    Private Sub llGV_Apply_WatchSet_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llGV_Apply_WatchSet.LinkClicked
        If SelectedGPRMWatchSetName = "" Then Exit Sub
        ApplySavedWatchSet(SelectedGPRMWatchSetName)
    End Sub

    Private Sub llGV_Delete_WatchSet_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llGV_Delete_WatchSet.LinkClicked
        If SelectedGPRMWatchSetName = "" Then Exit Sub
        If XtraMessageBox.Show(Me.LookAndFeel, Me, "Are you sure that you want to delete set: " & SelectedGPRMWatchSetName & "?", My.Settings.APPLICATION_NAME, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then Exit Sub
        If File.Exists(GPRMWatchSetSavePath & SelectedGPRMWatchSetName & ".phoenix_gprm_watch_set") Then File.Delete(GPRMWatchSetSavePath & SelectedGPRMWatchSetName & ".phoenix_gprm_watch_set")
        LoadSavedGPRMWatchSets()
        Me.cbSavedWatchSets.SelectedIndex = -1
        Me.cbSavedWatchSets.Text = ""
    End Sub

#End Region 'PANELS:GPRM VIEWER

#Region "PANELS:PLAY LOG"

    Private PlayLog_IsActive As Boolean = False

    Private Sub SetupPlayLog()
        If WindowsVersion <> eWindowsVersion.Windows7 Then
            dpPlayLog.Visibility = DockVisibility.Hidden
            dpProjectSelector.Show()
        Else
            HideCaret(rtbPlayLog.Handle)
            PlayLog_IsActive = True
        End If
    End Sub

    Private Sub PlayLog_AddLine(ByVal Msg As String)
        If Not PlayLog_IsActive Then Exit Sub
        Me.rtbPlayLog.AppendText(Msg & vbNewLine)
        Me.rtbPlayLog.ScrollToCaret()
        HideCaret(Me.rtbPlayLog.Handle)
    End Sub

    Private Sub PlayLog_Clear()
        Me.rtbPlayLog.Clear()
    End Sub

#End Region 'PANELS:PLAY LOG

#End Region 'PANELS

#Region "MENUS"

#Region "MENUS:SHARED EVENTS"

    Private Sub mnu_Popup(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuFILE.Popup, mnuWINDOWS.Popup, mnuPROFILES.Popup, mnuACCELERATORS.Popup, mnuMARKERS.Popup, mnuTOOLS.Popup, mnuHELP.Popup
        Me.KH.PauseHook()
    End Sub

    Private Sub mnu_CloseUp(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuFILE.CloseUp, mnuWINDOWS.CloseUp, mnuPROFILES.CloseUp, mnuACCELERATORS.CloseUp, mnuMARKERS.CloseUp, mnuTOOLS.Popup, mnuHELP.CloseUp
        Me.KH.UnpauseHook()
    End Sub

#End Region 'MENUS:SHARED EVENTS

#Region "MENUS:FILE"

    Private Sub miFILE_Browse_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles miFILE_Browse.ItemClick
        Me.SelectProject_WithDialog()
    End Sub

    Private Sub miFILE_Eject_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles miFILE_Eject.ItemClick
        Me.EjectProject()
    End Sub

    Private Sub miFILE_Exit_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles miFILE_Exit.ItemClick
        Application.Exit()
    End Sub

#End Region 'MENUS:FILE

#Region "MENUS:WINDOWS"

    Private Sub WindowMenuItem_CheckedChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles miWINDOWS_SPRMs.CheckedChanged, miWINDOWS_Remote.CheckedChanged, miWINDOWS_Dashboard.CheckedChanged, miWINDOWS_ExtDashboard.CheckedChanged, miWINDOWS_VolumeInfo.CheckedChanged, miWINDOWS_UOPs.CheckedChanged, miWINDOWS_GPRMs.CheckedChanged, miWINDOWS_VideoStreamInfo.CheckedChanged, miWINDOWS_AudioStreamInfo.CheckedChanged, miWINDOWS_SubtitleStreamInfo.CheckedChanged, miWINDOWS_VMGMVTSM.CheckedChanged, miWINDOWS_Markers.CheckedChanged, miWINDOWS_StreamPlayback.CheckedChanged, miWINDOWS_Options.CheckedChanged, miWINDOWS_ProjectSelector.CheckedChanged, miWINDOWS_Console.CheckedChanged
        If EmulatorIsInSetup Then Exit Sub
        Dim dp As DockPanel = Me.DockManager.Panels(e.Item.Tag)
        Select Case dp.Visibility
            Case DockVisibility.AutoHide
                'do nothing
            Case DockVisibility.Hidden
                dp.Visibility = DockVisibility.Visible
                dp.Top = Me.Top + 50
                dp.Left = Me.Left + 50
            Case DockVisibility.Visible
                dp.Visibility = DockVisibility.Hidden
        End Select
    End Sub

    Private Sub miWINDOWS_ExtendedOptions_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles miWINDOWS_ExtendedOptions.ItemClick
        If ExtendedOptionsForm Is Nothing OrElse ExtendedOptionsForm.IsDisposed Then
            ExtendedOptionsForm = New ExtendedOptions_Form(Me)
            ExtendedOptionsForm.Top = Me.Top + 60
            ExtendedOptionsForm.Left = Me.Left + 50
            ExtendedOptionsForm.Show()
        Else
            ExtendedOptionsForm.Visible = True
            ExtendedOptionsForm.BringToFront()
        End If
    End Sub

    Private Sub miNonSeamlessCells_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles miNonSeamlessCells.ItemClick
        If Player Is Nothing OrElse Player.PlayState = ePlayState.SystemJP Then
            MsgBox("The non-seamless cells window is available only when a DVD-Video project is loaded in the emulator.")
            Exit Sub
        End If
        If Player.NonSeamlessCells.Count = 0 Then
            If XtraMessageBox.Show(Me.LookAndFeel, Me, "There are no relevant non-seamless cells found in the current project.", My.Settings.APPLICATION_NAME, MessageBoxButtons.OK, MessageBoxIcon.Exclamation) = MsgBoxResult.Ok Then
                Exit Sub
            End If
        End If

        If NonSeamlessCellsDialog Is Nothing OrElse NonSeamlessCellsDialog.IsDisposed Then
            NonSeamlessCellsDialog = New dlgNonSeamlessCells(Me)
            NonSeamlessCellsDialog.Show()
        Else
            NonSeamlessCellsDialog.Visible = True
            NonSeamlessCellsDialog.BringToFront()
        End If
    End Sub

#End Region 'MENUS:WINDOWS

#Region "MENUS:PROFILES"

    Private Sub mnuProfiles_Popup(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPROFILES.Popup
        LoadCachedProfilesIntoMenu()
    End Sub

    Private Sub miPROFILES_SaveProfile_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles miPROFILES_SaveProfile.ItemClick
        KH.PauseHook()
        Dim str As String = CurrentUserProfile.SaveWithDialog()
        Me.mnuPROFILES.Caption = "&Profile (" & Path.GetFileNameWithoutExtension(str) & ")"
        KH.UnhookKeyboard()
    End Sub

    Private Sub miPROFILES_LoadProfile_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles miPROFILES_LoadProfile.ItemClick
        Me.LoadUserProfile_WithDialog()
    End Sub

#Region "MENUS:PROFILES:DEFAULT PROFILES"

    Private Sub miPROFILES_DEFAULTPROFILES_Standard_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles miPROFILES_DEFAULTPROFILES_Standard.ItemClick
        If XtraMessageBox.Show(Me.LookAndFeel, Me, "Would you like to set STANDARD as the default profile?", My.Settings.APPLICATION_NAME, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = MsgBoxResult.Yes Then
            My.Settings.DEFAULT_PROFILE = "STANDARD"
        End If
        LoadUserProfile_Defaults(eDefaultProfileVersion.Standard)
    End Sub

    Private Sub miPROFILES_DEFAULTPROFILES_StreamQC_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles miPROFILES_DEFAULTPROFILES_StreamQC.ItemClick
        LoadUserProfile_Defaults(eDefaultProfileVersion.StreamQC)
    End Sub

    Private Sub miPROFILES_DEFAULTPROFILES_TCS_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles miPROFILES_DEFAULTPROFILES_TCS.ItemClick
        If XtraMessageBox.Show(Me.LookAndFeel, Me, "Would you like to set Alternate 1152x864 as the default profile?", My.Settings.APPLICATION_NAME, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = MsgBoxResult.Yes Then
            My.Settings.DEFAULT_PROFILE = "Alternate1152x864"
        End If
        LoadUserProfile_FromResource("1152x864_alt.pup")
    End Sub

    Private Sub miPROFILES_DEFAULTPROFILES_1920x1200_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles miPROFILES_DEFAULTPROFILES_1920x1200.ItemClick
        If XtraMessageBox.Show(Me.LookAndFeel, Me, "Would you like to set 1920x1200 as the default profile?", My.Settings.APPLICATION_NAME, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = MsgBoxResult.Yes Then
            My.Settings.DEFAULT_PROFILE = "_1920x1200"
        End If
        LoadUserProfile_FromResource("1920x1200.pup")
    End Sub

#End Region 'MENUS:PROFILES:DEFAULT PROFILES

#Region "MENUS:PROFILES:PROFILE CACHE"

    Private Sub miPROFILES_PurgeCache1_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles miPROFILES_PurgeCache.ItemClick
        PurgeProfileCache()
    End Sub

#End Region 'MENUS:PROFILES:PROFILE CACHE

#End Region 'MENUS:PROFILES

#Region "MENUS:ACCELERATORS"

    Private Sub miACCELERATORS_PlayerDefaults_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles miACCELERATORS_PlayerDefaults.ItemClick
        Try
            'If Player.PlayState = ePlayState.SystemJP Then Exit Sub
            Dim dlg As New dlgPlayerDefaultAccelerator(Me)
            If dlg.ShowDialog = DialogResult.OK Then
                'hi
            End If
        Catch ex As Exception
            Me.AddConsoleLine(eConsoleItemType.ERROR, "Problem with PlayerDefaults Accelerator. Error: " & ex.Message, Nothing)
        End Try
    End Sub

    Private Sub miACCELERATORS_TimeSearch_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles miACCELERATORS_TimeSearch.ItemClick
        If Player.PlayState = ePlayState.SystemJP Then Exit Sub
        If Not Player.CurrentUserOperations(5) Then
            XtraMessageBox.Show(Me.LookAndFeel, Me, "Time search is currently prohibited.", My.Settings.APPLICATION_NAME, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        ShowTimeSearchAccelerator()
    End Sub

    Private Sub miACCELERATORS_ChapterSearch_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles miACCELERATORS_ChapterSearch.ItemClick
        If Player.PlayState = ePlayState.SystemJP Then Exit Sub
        If Not Player.CurrentUserOperations(1) Then
            XtraMessageBox.Show(Me.LookAndFeel, Me, "Chapter search is currently prohibited.", My.Settings.APPLICATION_NAME, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        ShowChapterSearchAccelerator()
    End Sub

    Private Sub miACCELERATORS_TitleSearch_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles miACCELERATORS_TitleSearch.ItemClick
        If Player.PlayState = ePlayState.SystemJP Then Exit Sub
        If Not Player.CurrentUserOperations(2) Then
            XtraMessageBox.Show(Me.LookAndFeel, Me, "Title search is currently prohibited.", My.Settings.APPLICATION_NAME, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        ShowTitleSearchAccelerator()
    End Sub

    Private Sub miACCELERATOR_Audio_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles miACCELERATOR_Audio.ItemClick
        If Player.PlayState = ePlayState.SystemJP Then Exit Sub
        If Not Player.CurrentUserOperations(20) Then
            XtraMessageBox.Show(Me.LookAndFeel, Me, "Audio select is currently prohibited.", My.Settings.APPLICATION_NAME, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        ShowAudioSelectAccelerator()
    End Sub

    Private Sub miACCELERATORS_Subtitle_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles miACCELERATORS_Subtitle.ItemClick
        If Player.PlayState = ePlayState.SystemJP Then Exit Sub
        If Not Player.CurrentUserOperations(21) Then
            XtraMessageBox.Show(Me.LookAndFeel, Me, "Subtitle select is currently prohibited.", My.Settings.APPLICATION_NAME, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        ShowSubtitleSelectAccelerator()
    End Sub

#End Region 'MENUS:ACCELERATORS

#Region "MENUS:MARKERS"

    Private Sub miMARKERS_Help_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles miMARKERS_Help.ItemClick

    End Sub

#Region "MENUS:MARKERS:COLLECTION"

    Private Sub miMK_CO_New_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles miMK_CO_New.ItemClick
        Markers_Collection_New()
    End Sub

    Private Sub miMK_CO_Open_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles miMK_CO_Open.ItemClick
        Markers_Collection_OpenWithDialog()
    End Sub

    Private Sub miMK_CO_Close_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles miMK_CO_Close.ItemClick
        Markers_Collection_Close()
    End Sub

    Private Sub miMK_CO_Save_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles miMK_CO_Save.ItemClick

    End Sub

    Private Sub miMK_CO_SaveAs_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles miMK_CO_SaveAs.ItemClick
        Markers_Collectoin_SaveWithDialog()
    End Sub

    Private Sub miMK_CO_Delete_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles miMK_CO_Delete.ItemClick
        Markers_Collection_Delete()
    End Sub

    Private Sub miMK_CO_Clear_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles miMK_CO_Clear.ItemClick
        Markers_Collection_Clear()
    End Sub

    Private Sub miMK_CO_Rename_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles miMK_CO_Rename.ItemClick
        Markers_Collection_Rename()
    End Sub

    Private Sub miMK_CO_EditProjectPath_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles miMK_CO_EditProjectPath.ItemClick
        Markers_Collection_EditProjectPath()
    End Sub

#End Region 'MENUS:MARKERS:COLLECTION

#Region "MENUS:MARKERS:SET"

    Private Sub miMK_ST_New_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles miMK_ST_New.ItemClick
        Markers_Set_New()
    End Sub

    Private Sub miMK_ST_Delete_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles miMK_ST_Delete.ItemClick
        Markers_Set_Delete()
    End Sub

    Private Sub miMK_ST_Rename_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles miMK_ST_Rename.ItemClick
        Markers_Set_Rename()
    End Sub

    Private Sub miMK_ST_ClearContents_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles miMK_ST_ClearContents.ItemClick
        Markers_Set_Clear()
    End Sub

    Private Sub miMK_ST_CURRENTSETS_Popup(ByVal sender As Object, ByVal e As System.EventArgs) Handles miMK_ST_CURRENTSETS.Popup
        Markers_LoadSetListIntoCurrentSetsMenu()
    End Sub

#End Region 'MENUS:MARKERS:SET

#Region "MENUS:MARKERS:MARKER"

    Private Sub miMK_MK_New_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles miMK_MK_New.ItemClick
        Markers_Marker_New(cbMarkers_ManualMarker.Checked, cbMarkers_SerializeMarkerNames.Checked)
    End Sub

    Private Sub miMK_MK_NewSerialize_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles miMK_MK_NewSerialize.ItemClick
        Markers_Marker_New_Serialize()
    End Sub

    Private Sub miMK_MK_NewManual_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles miMK_MK_NewManual.ItemClick
        Markers_Marker_New_Manual()
    End Sub

    Private Sub miMK_MK_Execute_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles miMK_MK_Execute.ItemClick
        Markers_Marker_ExecuteCurrent()
    End Sub

#End Region 'MENUS:MARKERS:MARKER

#End Region 'MENUS:MARKERS

#Region "MENUS:TOOLS"

#Region "MENUS:TOOLS:EVENTS"

    Private Sub mnuTOOLS_Popup(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuTOOLS.Popup
        If FeatureManagement.Features.FE_DVD_UOPTemplates Then
            miUOPTEMPLATES.Enabled = True
            LoadUOPTemplatesIntoToolsMenu()
        Else
            miUOPTEMPLATES.Enabled = False
        End If
    End Sub

    Private Sub miTOOLS_ImageMountUtility_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles miTOOLS_ImageMountUtility.ItemClick
        Dim dlg As New dlgImageMounting(Me)
        dlg.ShowDialog()
    End Sub

#End Region 'MENUS:TOOLS:EVENTS

#Region "MENUS:TOOLS:FUNCTIONALITY"

    Private Sub LoadUOPTemplatesIntoToolsMenu()
        Try
            Me.miUOPTEMPLATES.ClearLinks()
            If Me.UserOperationTemplates Is Nothing OrElse Me.UserOperationTemplates.Templates.Length = 0 Then
                Me.miUOPTEMPLATES.AddItem(New DevExpress.XtraBars.BarButtonItem(barMain, "NONE AVAILABLE"))
                Exit Sub
            End If

            'Dim BBI As DevExpress.XtraBars.BarButtonItem
            Dim BCI As DevExpress.XtraBars.BarCheckItem
            For Each UOPT As cUserOperationTemplate In UserOperationTemplates.Templates
                BCI = New DevExpress.XtraBars.BarCheckItem(barMain, False)
                BCI.Caption = UOPT.Name
                If UOPT.Name = UserOperationTemplates.CurrentName Then
                    BCI.Checked = True
                End If
                AddHandler BCI.CheckedChanged, New DevExpress.XtraBars.ItemClickEventHandler(AddressOf UOPTemplateMenuItemClickHandler)
                Me.miUOPTEMPLATES.AddItem(BCI)
            Next
        Catch ex As Exception
            Me.AddConsoleLine(eConsoleItemType.ERROR, "Problem with LoadUOPTemplatesIntoToolsMenu(). Error: " & ex.Message)
        End Try
    End Sub

    Private Sub UOPTemplateMenuItemClickHandler(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs)
        Try
            If e.Item.GetType IsNot GetType(DevExpress.XtraBars.BarCheckItem) Then Exit Sub
            Dim ClickedItem As DevExpress.XtraBars.BarCheckItem = CType(e.Item, DevExpress.XtraBars.BarCheckItem)
            If ClickedItem Is Nothing Then Exit Sub
            If ClickedItem.Checked Then
                If UserOperationTemplates.ApplyByName(ClickedItem.Caption) Then
                    'Dim BCI As DevExpress.XtraBars.BarCheckItem
                    'For i As Short = 0 To Me.miUOPTEMPLATES.ItemLinks.Count - 1
                    '    BCI = CType(Me.miUOPTEMPLATES.ItemLinks(i).Item, DevExpress.XtraBars.BarCheckItem)
                    'Next
                Else
                    Me.AddConsoleLine(eConsoleItemType.WARNING, "Unable to find the desired UOP Template.")
                End If
            Else
                Me.UserOperationTemplates.CurrentIndex = -1
            End If
        Catch ex As Exception
            Me.AddConsoleLine(eConsoleItemType.ERROR, "Problem with UOPTemplateMenuItemClickHandler(). Error: " & ex.Message)
        End Try
    End Sub

#End Region 'MENUS:TOOLS:FUNCTIONALITY

#End Region 'MENUS:TOOLS

#Region "MENUS:HELP"

    Private Sub miHELP_OnlineSupport_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles miHELP_OnlineSupport.ItemClick
        Process.Start("http://support.SEQMT.com?TGT=Phoenix")
    End Sub

    Private Sub miHELP_UserManual_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles miHELP_UserManual.ItemClick
        Dim pPath As String = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)
        Dim pAppName As String = Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetExecutingAssembly().Location)
        Dim tPath As String = pPath & "\Documentation\" & "Phoenix_UserGuide.pdf"
        If Not File.Exists(tPath) Then
            XtraMessageBox.Show(Me.LookAndFeel, Me, "The manual does not exist in the program directory.", My.Settings.APPLICATION_NAME, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Else
            Process.Start(tPath)
        End If
    End Sub

    Private Sub miHELP_SMTHomePage_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles miHELP_SMTHomePage.ItemClick
        Process.Start("http://www.SEQMT.com")
    End Sub

    Private Sub miHELP_AboutPhoenix_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles miHELP_AboutPhoenix.ItemClick
        Me.KH.PauseHook()
        Dim ss As New Phoenix_SplashScreen(False, SplashBitmap, Me.LicensedVersion, False)
        ss.ShowDialog()
        Me.KH.UnpauseHook()
    End Sub

#End Region 'MENUS:HELP

#End Region 'MENUS

#Region "EMULATOR SETUP"

    Public EmulatorIsInSetup As Boolean = True
    Public EmulatorIsSetup As Boolean = False

    Private Function SetupEmulator() As Boolean
        Try
            EmulatorIsInSetup = True
            ConsoleItems = New colConsoleItem
            SetupBitrateGraph()
            LoadUpMacros()
            UserOperationTemplates = New cUserOperationTemplates
            UserOperationTemplates.Load()
            ReDim _CachedUOPs(24)

            Me.cbSPB_UseDecklink.Enabled = Me.DecklinkAvailable

            LaunchCPUMonitor()
            LoadRecentProjectsIntoProjectSelector()
            ppHookupKeyboard()
            LoadSavedGPRMWatchSets()

            Me.Left = My.Settings.WINDOW_MAIN_X
            Me.Top = My.Settings.WINDOW_MAIN_Y

            If Viewer IsNot Nothing Then
                Viewer.Left = My.Settings.WINDOW_VIEWER_X
                Viewer.Top = My.Settings.WINDOW_VIEWER_Y
            End If

            ApplyFeatureManagement()

            EmulatorIsInSetup = False
            EmulatorIsSetup = True

            SetupPlayLog()
            SetupUIForFeatureManagement()

            ' START THE PROJECT FOR A DOUBLE CLICKED IFO FILE
            If My.Application.CommandLineArgs.Count > 0 Then
                Dim pth As String = My.Application.CommandLineArgs(My.Application.CommandLineArgs.Count - 1)
                If Path.GetExtension(pth).ToLower = ".ifo" Then
                    pth = Path.GetDirectoryName(pth)
                    If File.Exists(pth & "\video_ts.ifo") Then
                        RunProject(pth & "\video_ts.ifo")
                    End If
                End If
            End If

            Return True
        Catch ex As Exception
            'Throw New Exception("Problem with SetupEmulator(). Error: " & ex.Message)
            Return False
        End Try
    End Function

    Private Sub ApplyFeatureManagement()
        Try
            Me.dpGPRMViewer.Enabled = FeatureManagement.Features.FE_OT_GPRMViewer
            Me.btnRM_ToggleClosedCaptions.Enabled = FeatureManagement.Features.FE_L21_Decode
        Catch ex As Exception
            Throw New Exception("Problem with ApplyFeatureManagement(). Error: " & ex.Message, ex)
        End Try
    End Sub

#End Region 'EMULATOR SETUP

#Region "PLAYER"

#Region "PLAYER:SETUP/TRANSITIONING"

    Public Sub ShowSystemJacketPicture()
        Try
            If Player IsNot Nothing Then
                Player.EjectProject(True)
                Player.Dispose()
                Player = Nothing
                ViewerDispose()
            End If
            Player = New cDVDPlayer(New cSMTForm(Me), Me.PreferredAVMode, Me.CurrentUserProfile.AppOptions.IntensityVideoScalingMode, CurrentUserProfile.AppOptions.IntensityVideoResolution, My.Settings.DVD_NOTIFY_NONSEAMLESSCELL)
            If Not Player.InitializeSystemJacketPicture() Then
                XtraMessageBox.Show(Me.LookAndFeel, Me, "Problem initializing player. Contact SMT for support." & vbNewLine & vbNewLine & "Application exiting.", My.Settings.APPLICATION_NAME, MessageBoxButtons.OK, MessageBoxIcon.Stop)
                Process.GetCurrentProcess.Kill()
            End If
        Catch ex As Exception
            Throw New Exception("Problem with ShowSystemJacketPicture(). Error: " & ex.Message, ex)
        End Try
    End Sub

    Public Sub SelectProject_WithDialog()
        Me.KH.PauseHook()
        Dim dlg As New OpenFileDialog
        Dim dir As String = My.Settings.LAST_PROJECT
        If dir = "" Then dir = "C:\"
        dir = Path.GetDirectoryName(dir)
        With dlg
            .Filter = "VIDEO_TS.IFO|VIDEO_TS.IFO"
            .InitialDirectory = dir
            .Multiselect = False
            .Title = "Select VIDEO_TS.IFO"
        End With
        If dlg.ShowDialog = DialogResult.OK Then
            My.Settings.LAST_PROJECT = dlg.FileName
            My.Settings.Save()
            Me.RunProject(dlg.FileName)
            LoadRecentProjectsIntoProjectSelector()
        End If
        Me.KH.UnpauseHook()
    End Sub

    Public Sub RunProject(ByVal VIDEO_TS As String)
        Try
            If Not Directory.Exists(Path.GetDirectoryName(VIDEO_TS)) Then
                DevExpress.XtraEditors.XtraMessageBox.Show("The selected VIDEO_TS does not exist." & vbNewLine & VIDEO_TS, "IO Failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
            Me.AddRecentProject(VIDEO_TS)
            If Path.HasExtension(VIDEO_TS) Then
                VIDEO_TS = Path.GetDirectoryName(VIDEO_TS)
            End If
            If Player Is Nothing Then
                'Player = New cDVDPlayer(Me, Me.ForceMobileMode)
                Player = New cDVDPlayer(New cSMTForm(Me), Me.PreferredAVMode, CurrentUserProfile.AppOptions.IntensityVideoScalingMode, CurrentUserProfile.AppOptions.IntensityVideoResolution, My.Settings.DVD_NOTIFY_NONSEAMLESSCELL)
                If Player.InitializePlayer(VIDEO_TS, Me.PrepPlayerDefaults) Then
                    If Player.AVMode = eAVMode.DesktopVMR Then Player.ShowViewer()
                Else
                    EjectProject()
                End If
            Else
                If Player.PlayState = ePlayState.SystemJP Then
                    If Player.InitializePlayer(VIDEO_TS, Me.PrepPlayerDefaults) Then
                        If Player.AVMode = eAVMode.DesktopVMR Then Player.ShowViewer()
                    Else
                        EjectProject()
                    End If
                Else
                    If Player.EjectProject() Then
                        Player = Nothing
                        GC.Collect()
                        Player = New cDVDPlayer(New cSMTForm(Me), Me.PreferredAVMode, CurrentUserProfile.AppOptions.IntensityVideoScalingMode, CurrentUserProfile.AppOptions.IntensityVideoResolution, My.Settings.DVD_NOTIFY_NONSEAMLESSCELL)
                        If Player.InitializePlayer(VIDEO_TS, Me.PrepPlayerDefaults) Then
                            If Player.AVMode = eAVMode.DesktopVMR Then Player.ShowViewer()
                        Else
                            EjectProject()
                        End If
                    Else
                        'FATAL ERROR - APP MUST BE KILLED
                        If XtraMessageBox.Show(Me.LookAndFeel, Me, "Failed to eject project. Please restart " & My.Settings.APPLICATION_NAME & ".", My.Settings.APPLICATION_NAME, MessageBoxButtons.OK, MessageBoxIcon.Stop) = MsgBoxResult.Ok Then
                            Application.Exit()
                        End If
                    End If
                End If
            End If

            If PreferredAVMode = eAVMode.DesktopVMR Then
                If Viewer IsNot Nothing Then
                    Viewer.Left = My.Settings.WINDOW_VIEWER_X
                    Viewer.Top = My.Settings.WINDOW_VIEWER_Y
                End If
            End If

        Catch ex As Exception
            Me.AddConsoleLine(eConsoleItemType.ERROR, "Problem with RunProject(). Error: " & ex.Message)
        End Try
    End Sub

    Public Sub RebootProject()
        Try
            If MostRecentProject = "" Then Exit Sub
            RunProject(Me.MostRecentProject)
        Catch ex As Exception
            Throw New Exception("Problem with RebootProject(). Error: " & ex.Message)
        End Try
    End Sub

    Public Sub EjectProject(Optional ByVal Force As Boolean = False)
        Try
            If Not Player.EjectProject(Force) Then
                'FATAL ERROR - APP MUST BE KILLED
                If XtraMessageBox.Show(Me.LookAndFeel, Me, "Failed to eject project. Please restart " & My.Settings.APPLICATION_NAME & ".", My.Settings.APPLICATION_NAME, MessageBoxButtons.OK, MessageBoxIcon.Stop) = MsgBoxResult.Ok Then
                    Application.Exit()
                End If
            Else
                Player = Nothing
                ShowSystemJacketPicture()
                ''Player = New cDVDPlayer(Me, Me.ForceMobileMode)
                'Player = New cDVDPlayer(Me, Me.PreferredAVMode, CurrentUserProfile.AppOptions.IntensityVideoScalingMode, CurrentUserProfile.AppOptions.IntensityVideoResolution)
                'If Player.InitializeSystemJacketPicture() Then
                '    If Player.AVMode = eAVMode.DesktopVMR Then Player.ShowViewer()
                'Else
                '    'FATAL ERROR - APP MUST BE KILLED
                '    If XtraMessageBox.Show(Me.LookAndFeel, Me, "Failed to eject project. Please restart " & My.Settings.APPLICATION_NAME & ".", My.Settings.APPLICATION_NAME, MessageBoxButtons.OK, MessageBoxIcon.Stop) = MsgBoxResult.Ok Then
                '        Application.Exit()
                '    End If
                'End If
            End If
        Catch ex As Exception
            Me.AddConsoleLine(eConsoleItemType.ERROR, "Problem with EjectProject(). Error: " & ex.Message)
        End Try
    End Sub

    Public Sub ViewerDispose()
        If Viewer Is Nothing Then Exit Sub
        My.Settings.WINDOW_VIEWER_X = Viewer.Left
        My.Settings.WINDOW_VIEWER_Y = Viewer.Top
        Viewer.Close()
    End Sub

#End Region 'PLAYER:SETUP/TRANSITITIONING

#Region "PLAYER:DEFAULT SETUP"

    Public Function PrepPlayerDefaults() As sNavigatorSetup
        Try
            Dim Out As sNavigatorSetup
            Out.DEFAULT_MENU_LANGUAGE = Me.CurrentConfig.MenuLanguage
            Out.DEFAULT_AUDIO_LANGUAGE = Me.CurrentConfig.AudioLanguage
            Out.DEFAULT_AUDIO_EXTENSION = Me.CurrentConfig.AudioExtension
            Out.DEFAULT_SUBTITLE_LANGUAGE = Me.CurrentConfig.SubtitleLanguage
            Out.DEFAULT_SUBTITLE_EXTENSION = Me.CurrentConfig.SubtitleExtension
            Out.PARENTAL_LEVEL = Me.CurrentConfig.ParentalLevel
            Out.PARENTAL_COUNTRY = Me.CurrentConfig.ParentalCountry
            Out.ASPECT_RATIO = Me.CurrentConfig.AspectRatio
            Out.PLAYER_REGION = Me.CurrentConfig.Region
            Out.IsInitalized = True
            Return Out
        Catch ex As Exception
            Me.AddConsoleLine(eConsoleItemType.ERROR, "Problem with PrepPlayerDefaults(). Error: " & ex.Message)
        End Try
    End Function

#End Region 'PLAYER:DEFAULT SETUP

#Region "PLAYER:EVENTS"

#Region "PLAYER:EVENTS:INITIALIZATION"

    Private Sub HandlePlayerInitialized() Handles Player.evPlayerInitialized

        Me.dpAudioStreamInfo.Enabled = True
        Me.dpDashboard.Enabled = True
        Me.dpExtDashboard.Enabled = True
        Me.dpGPRMs.Enabled = True
        Me.dpGPRMViewer.Enabled = True
        Me.dpMacros.Enabled = True
        Me.dpMarkers.Enabled = True
        Me.dpOptions.Enabled = True
        Me.dpPlayLog.Enabled = True
        Me.dpSPRMs.Enabled = True
        Me.dpStreamPlayback.Enabled = False
        Me.dpSubtitleStreamInfo.Enabled = True
        Me.dpUOPs.Enabled = True
        Me.dpVideoStreamInfo.Enabled = True
        Me.dpVMGMVTSM.Enabled = True
        Me.dpVolumeInfo.Enabled = True

        ToggleRemote(True)

        lblVolumes.Text = Player.VolumeCount
        lblCurrentVol.Text = Player.CurrentVolume
        lblDiscSide.Text = Player.DiscSide.ToString
        lblPublisher.Text = Player.CurrentDVD.VMGM.ProviderID
        lblVTSsTotal.Text = Player.VTSCount

        lblProjectPath.Text = Player.DVDDirectory

        Me.btnPS_Eject.Enabled = True
        Me.btnPS_Reboot.Enabled = True
        Me.miFILE_Eject.Enabled = True

        'disc text
        lblDVDText.Text = Player.DVDText
        ToolTip.SetToolTip(lblDVDText, lblDVDText.Text)
        If Not lblDVDText.Text = "Null" And Not lblDVDText.Text = "" Then
            'VW.Text = "Phoenix - " & Me.lblDVDText.Text
        Else
            If InStr(Player.DVDDirectory.ToLower, "d:\", CompareMethod.Text) Then
                'VW.Text = " " & Me.GetVolumeLabel("D") & " disc in DVD Drive"
            End If
        End If

        Me.lbParentalManagement.Items.Clear()
        Dim PMs() As cParentalManagement_US = Player.CurrentDVD.VMGM.GlobalTitleParentalManagement.Titles
        For Each PM As cParentalManagement_US In PMs
            Me.lbParentalManagement.Items.Add(PM.ToString)
        Next

        Me.CurrentUserProfile.AppOptions.Apply()
        PlayLog_Clear()

        SetupUIForFeatureManagement()

    End Sub

    Public Sub HandleSystemJacketPictureDisplayed() Handles Player.evSystemJacketPictureDisplayed
        Me.dpAudioStreamInfo.Enabled = False
        Me.dpDashboard.Enabled = False
        Me.dpExtDashboard.Enabled = False
        Me.dpGPRMs.Enabled = False
        Me.dpGPRMViewer.Enabled = False
        Me.dpMacros.Enabled = False
        Me.dpMarkers.Enabled = False
        Me.dpOptions.Enabled = False
        Me.dpPlayLog.Enabled = False
        Me.dpSPRMs.Enabled = False
        Me.dpStreamPlayback.Enabled = True
        Me.dpSubtitleStreamInfo.Enabled = False
        Me.dpUOPs.Enabled = False
        Me.dpVideoStreamInfo.Enabled = False
        Me.dpVMGMVTSM.Enabled = False
        Me.dpVolumeInfo.Enabled = False

        Me.btnPS_Reboot.Enabled = False
        btnPS_Eject.Enabled = False
        Me.miFILE_Eject.Enabled = False
        Me.miFILE_Browse.Enabled = True
        ToggleRemote(False)
        If ExtendedOptionsForm IsNot Nothing Then ExtendedOptionsForm.Cursor = Cursors.Default
    End Sub

#End Region 'PLAYER:EVENTS:INITIALIZATION

#Region "PLAYER:EVENTS:PARAMETER CHANGES"

    Private Sub HandleGPRMChanged(ByVal GPRM As Integer, ByVal NewVal As Integer) Handles Player.evGPRMChanged
        Me.AddConsoleLine(eConsoleItemType.GPRM_CHANGE, "GPRM " & GPRM & " = " & NewVal)
        Me.PlayLog_AddLine("      GPRM" & GPRM & " CHANGED TO " & NewVal)
    End Sub

    Private Sub HandleSPRMChanged(ByVal SPRM As Integer, ByVal NewVal As Integer) Handles Player.evSPRMChanged
        Me.AddConsoleLine(eConsoleItemType.SPRM_CHANGE, "SPRM " & SPRM & " = " & NewVal)
        Me.PlayLog_AddLine("      SPRM" & SPRM & " CHANGED TO " & NewVal)
    End Sub

#End Region 'PLAYER:EVENTS:PARAMETER CHANGES

#Region "PLAYER:EVENTS:NAV CMDS"

    Private Sub HandleBeginNavCmd(ByVal Source As eNavCmdType, ByVal DetailedPlayLocation As cDetailedPlayLocation) Handles Player.evBeginNavCommands
        Dim str As String = "BEGIN NAV CMDS | Source = " & Source.ToString & " | " & DetailedPlayLocation.ToString
        Me.AddConsoleLine(eConsoleItemType.NAV_CMD, str)
        Me.PlayLog_AddLine(vbNewLine & vbNewLine & str)
    End Sub

    Private Sub HandleNavCmd(ByVal Cmd As cCMD) Handles Player.evNavCommand
        Dim str As String = Cmd.ToString
        Me.AddConsoleLine(eConsoleItemType.NAV_CMD, str)
        Me.PlayLog_AddLine(str)
    End Sub

#End Region 'PLAYER:EVENTS:NAV CMDS

#Region "PLAYER:EVENTS:LOCATION CHANGES"

    Public Sub HandleDomainChange() Handles Player.evDomainChange
        Try
            'Debug.WriteLine("Domain Change: " & CType(p1, DvdDomain).ToString)
            AddConsoleLine(eConsoleItemType.EVENT, "Domain Change", Player.CurrentDomain.ToString, Nothing)

            PopulateVMGM() 'setup the vmgm info on the vtsm/vmgm toolwindow. this will happen more often than needed but oh well.

            EnableAllPositionLablesOnDB()
            HandleVTSChange(Player.CurrentVTS) 'this is needed so that everything gets updated for a change between menu and title space in the same vts

            gbRM_Misc.Enabled = True

            If Not Player.PlayState = ePlayState.SystemJP Then
                Dim s As String
                Select Case Player.CurrentDomain
                    Case DvdDomain.FirstPlay
                        s = "First Play"
                        ClearVTSM()
                        Me.ClearDashboard(True, False)

                    Case DvdDomain.Stop
                        s = "Stop"
                        ClearVTSM()
                        Me.ClearDashboard(True, False)
                        gbRM_Misc.Enabled = False
                        btnRM_CallRootMenu.Enabled = True
                        Me.btnRM_Play.Enabled = True
                        Me.btnRM_Stop.Enabled = True

                    Case DvdDomain.Title
                        s = "Title"
                        HandleTitleChange(-1)
                        'TODO: IMPORTANT - need to change total PGCs to title space here
                        Me.btnRM_SlowPlayback.Enabled = True

                    Case DvdDomain.VideoManagerMenu
                        s = "VMGM"
                        ClearVTSM()
                        SetupSubtitleTab()
                        DisablePositionLablesOnDBForMenuSpace()
                        Me.btnRM_SlowPlayback.Enabled = False
                        'TODO: IMPORTANT - need to change total PGCs to menu space here

                    Case DvdDomain.VideoTitleSetMenu
                        s = "VTSM"
                        ClearVTSM()
                        SetupSubtitleTab()
                        DisablePositionLablesOnDBForMenuSpace()
                        Me.btnRM_SlowPlayback.Enabled = False
                        'TODO: IMPORTANT - need to change total PGCs to menu space here

                End Select
                lblCurrentDomain.Text = s
            End If

        Catch ex As Exception
            AddConsoleLine(eConsoleItemType.ERROR, "Problem with DomainChanged. Error: " & ex.Message, Nothing, Nothing)
        End Try
    End Sub

    Public Sub HandleVTSChange(ByVal NewVTS As Byte) Handles Player.evVTSChange
        Try
            If Player.PlayState <> ePlayState.SystemJP And Player.PlayState <> ePlayState.Init Then
                Me.lblVTS.Text = NewVTS
                'Me.CurrentVTS = p1
                Dim VTS As cVTS = Player.CurrentDVD.VTSs(NewVTS - 1)
                lblVTSTTsTotal.Text = VTS.TitleCount
                If Player.CurrentDomain = DvdDomain.Title Then
                    lblPGCsTotal.Text = VTS.PGCCount_TitleSpace
                ElseIf Player.CurrentDomain = DvdDomain.VideoManagerMenu Or Player.CurrentDomain = DvdDomain.VideoTitleSetMenu Then
                    lblPGCsTotal.Text = VTS.PGCCount_MenuSpace
                Else
                    lblPGCsTotal.Text = "N/A"
                End If

                'Menu Language Sets
                Dim Langs() As String = Player.MenuLanguages_Strings.ToArray
                lbAvailableMenuLanguages.Items.Clear()
                For ix As Short = UBound(Langs) To 0 Step -1
                    lbAvailableMenuLanguages.Items.Add(Langs(ix))
                Next
                Me.HandlePGCChange(Player.CurrentPGC)
                SetupSubtitleTab()
            Else
                lblVTS.Text = ""
                lblVTSTTsTotal.Text = ""
                lblPGCsTotal.Text = ""
            End If
        Catch ex As Exception
            Debug.WriteLine("Problem with HandleVTSChanged(). Error: " & ex.Message)
        End Try
    End Sub

    Public Sub HandleTitleChange(ByVal NewTitle As Short) Handles Player.evTitleChange
        Try
            Thread.Sleep(125)
            If Player.CurrentDomain = DvdDomain.Stop Or Player.CurrentDomain = DvdDomain.FirstPlay Or Player.CurrentDomain = DvdDomain.VideoManagerMenu Or Player.CurrentDomain = DvdDomain.VideoTitleSetMenu Then Exit Sub

            If Player.PlayState = ePlayState.SystemJP Then
                Me.ClearDashboard(False, True)
                Exit Sub
            End If
            Debug.WriteLine("Title Change. TTNo: " & NewTitle)
            PopulateVTSM() 'populate the VTSM info on the vtsm vmgm tool window.
            ResetRateTracking()

            'This is needed for when a disc goes into a title for the first time
            'the nav does not throw a titlechanged event when we've not yet been in any title
            'so DomainChanged-Title calls TitleChange with "GetTitle"
            If NewTitle = -1 Then
                NewTitle = Player.CurrentTitle

                If NewTitle = 0 Then
                    PopulateVideoStreamInfoWindow("TitleChange")
                    Me.lblTotalRunningTime.Text = "0:00:00"
                    Exit Sub
                End If
            End If

            Me.lblDB_TitleCount.Text = Player.GlobalTTCount
            Me.lblDB_CurrentTitle.Text = NewTitle
            Me.lblDB_CurrentChapter.Text = Player.CurrentPlayLocation.ChapterNum
            Me.lblDB_ChapterCount.Text = Player.ChaptersInCurrentTitle

            lblEXDB_CurrentTitle.Text = NewTitle
            lblEXDB_TitleCount.Text = Player.GlobalTTCount

            lblCurrent_PTT.Text = Player.CurrentPlayLocation.ChapterNum
            lblPTTsTotal.Text = Player.ChaptersInCurrentTitle

            'Angles for TT
            lbAvailAngles.Items.Clear()
            lblAnglesTotal.Text = Player.CurrentTitleAngleCount
            lblCurrent_angle.Text = Player.CurrentAngleStream
            If Player.CurrentTitleAngleCount > 0 Then
                For i As Short = 1 To Player.CurrentTitleAngleCount
                    lbAvailAngles.Items.Add(i)
                Next
            End If

            'Timecode
            CurrentTimecodeDisplayMode = "TTe"

            Dim TC As DvdTimeCode
            Dim M, S, F As String
            If Player.CurrentTitleTRT.bMinutes.ToString.Length = 1 Then
                M = 0 & TC.bMinutes
            Else
                M = TC.bMinutes
            End If

            If Player.CurrentTitleTRT.bSeconds.ToString.Length = 1 Then
                S = 0 & TC.bSeconds
            Else
                S = TC.bSeconds
            End If

            Me.lblTotalRunningTime.Text = Player.CurrentTitleTRT.bHours & ":" & M & ":" & S '& ";" & F

            lblVTS.Text = Player.CurrentVTS

            lblVTSTT.Text = Player.CurrentGlobalTT.VTS_TTN
            Dim VTS As cVTS = Player.CurrentDVD.VTSs(Player.CurrentGlobalTT.VTSN - 1)
            lblVTSTTsTotal.Text = VTS.TitleCount

            If Player.CurrentDomain = DvdDomain.Title Then
                lblPGCsTotal.Text = VTS.PGCCount_TitleSpace
            End If

            Debug.WriteLine("SetupTitleMetaData()")
            If Not Player Is Nothing Then Player.ClearOSD()
            UpdateDashboard()

            'Subtitles
            'Dim MI As New MethodInvoker(AddressOf SSI.SetupSubtitleTab)
            'MI.BeginInvoke(Nothing, Nothing)

            SetupSubtitleTab()
            Me.HandleSubtitleStreamChanged(Player.CurrentSubtitleStreamNumber)

            'Audio
            SetupAudioTab()
            Me.HandleAudioStreamChanged(Nothing, Nothing)

            'Video
            PopulateVideoStreamInfoWindow("TitleChange2")

        Catch ex As Exception
            If Player.StartingDVD Then Exit Sub
            If Player.PlayState = ePlayState.SystemJP Then Exit Sub
            'If CheckEx(ex, "Title Change") Then Exit Sub
            AddConsoleLine(eConsoleItemType.ERROR, "Problem with HandleTitleChanged(). Error: " & ex.Message, Nothing, Nothing)
        End Try
    End Sub

    Public Sub HandleChapterChange(ByVal NewChapterNumber As Byte) Handles Player.evChapterChange
        UpdateDashboard()
        If Player.PlayState = ePlayState.SystemJP Then
            lblCurrent_PTT.Text = ""
            Me.lblDB_CurrentChapter.Text = ""
            Me.lblDB_ChapterCount.Text = ""
            lblPTTsTotal.Text = ""
        Else
            lblCurrent_PTT.Text = NewChapterNumber
            lblDB_CurrentChapter.Text = NewChapterNumber
            lblPTTsTotal.Text = Player.ChaptersInCurrentTitle
            lblDB_ChapterCount.Text = Player.ChaptersInCurrentTitle
        End If
    End Sub

    Private Sub HandlePGCChange(ByVal NewPGC As Short) Handles Player.evPGCChange
        Try
            If Player.PlayState <> ePlayState.SystemJP And Player.PlayState <> ePlayState.Init Then
                Me.lblPGC.Text = NewPGC
                Dim PGC As cPGC
                If Player.CurrentDomain = DvdDomain.Title Then

                    ''old way WORKS
                    'Dim loc As DvdPlayLocation
                    'Dim HR As Integer
                    'HR = Player.DVDInfo.GetCurrentLocation(loc)
                    'If HR < 0 Then Marshal.ThrowExceptionForHR(HR)
                    'Debug.WriteLine(loc.TitleNum & " - " & Player.CurrentTitle)
                    'PGC = Player.CurrentDVD.FindPGCByGTT(loc.TitleNum)

                    'New way
                    PGC = Player.CurrentDVD.FindPGCByGTT(Player.CurrentTitle)
                    Me.lblDB_ChapterCount.Text = Player.ChaptersInCurrentTitle

                ElseIf Player.CurrentDomain = DvdDomain.VideoManagerMenu Or Player.CurrentDomain = DvdDomain.VideoTitleSetMenu Then
                    PGC = Player.CurrentDVD.VTSs(Player.CurrentVTS - 1).VTSM_PGCI_UT.FindLUByLang("en").VXXM_PGCs(NewPGC - 1)
                Else
                    lblCellsTotal.Text = ""
                    lblProgramsTotal.Text = ""
                    Exit Sub
                End If

                If Not PGC Is Nothing Then
                    lblCellsTotal.Text = PGC.CellCount
                    lblProgramsTotal.Text = PGC.ProgramCount
                Else
                    lblPGC.Text = "x"
                    lblCellsTotal.Text = "x"
                    lblCell.Text = "x"
                    lblProgramsTotal.Text = "x"
                    lblProgramCur.Text = "x"
                End If
            Else
                lblPGC.Text = ""
                lblCellsTotal.Text = ""
                lblProgramsTotal.Text = ""
            End If
        Catch ex As Exception
            Debug.WriteLine(eConsoleItemType.ERROR, "Problem with HandlePGCChange(). Error: " & ex.Message)
        End Try
    End Sub

    Public Sub HandleProgramChange(ByVal NewPG As Short) Handles Player.evProgramChange
        Me.lblProgramCur.Text = NewPG
    End Sub

    Public Sub HandleCellChange(ByVal NewCell As Short) Handles Player.evCellChange
        Me.lblCell.Text = NewCell
    End Sub


#End Region 'PLAYER:EVENTS:LOCATION CHANGES

#Region "PLAYER:EVENTS:LOG LOCATION"

    Private Sub Handle_LogDomainChange(ByVal NewDomain As DvdDomain) Handles Player.evLOG_DomainChange
        Me.PlayLog_AddLine("      DOMAIN = " & NewDomain.ToString)
    End Sub

    Private Sub Handle_LogVTSChange(ByVal NewVTS As Byte) Handles Player.evLOG_VTSChange
        Me.PlayLog_AddLine("      VTS = " & NewVTS)
    End Sub

    Private Sub Handle_LogPGCChange(ByVal NewPGC As UInt16) Handles Player.evLOG_PGCChange
        Me.PlayLog_AddLine("      PGC = " & NewPGC)
    End Sub

    Private Sub Handle_LogProgramChange(ByVal NewPG As UInt16) Handles Player.evLOG_ProgramChange
        Me.PlayLog_AddLine("      PG = " & NewPG)
    End Sub

    Private Sub Handle_LogCellChange(ByVal NewCell As UInt16) Handles Player.evLOG_CellChange
        Me.PlayLog_AddLine("      Cell = " & NewCell)
    End Sub

    Private Sub Handle_LogTitleChange(ByVal NewTitle As UInt16) Handles Player.evLOG_TitleChange
        Me.PlayLog_AddLine("      Title = " & NewTitle)
    End Sub

    Private Sub Handle_LogChapterChange(ByVal NewChapter As UInt16) Handles Player.evLOG_ChapterChange
        Me.PlayLog_AddLine("      Chapter = " & NewChapter)
    End Sub

#End Region 'PLAYER:EVENTS:LOG LOCATION

#Region "PLAYER:EVENTS:MISC"

    Private Sub Handle_LB_OK_SET(ByVal Value As Boolean) Handles Player.evLetterboxAllowed_SET
        lblVidAtt_lbok.Text = Value.ToString
    End Sub

    Private Sub Handle_PS_OK_SET(ByVal Value As Boolean) Handles Player.evPanscanAllowed_SET
        lblVidAtt_psok.Text = Value.ToString
    End Sub

    Private Sub Handle_TileRegionMask_SET(ByVal Value() As Boolean) Handles Player.evRegionMask_SET
        Reg_1.BackColor = GetBoolColor(Value(0))
        Reg_2.BackColor = GetBoolColor(Value(1))
        Reg_3.BackColor = GetBoolColor(Value(2))
        Reg_4.BackColor = GetBoolColor(Value(3))
        Reg_5.BackColor = GetBoolColor(Value(4))
        Reg_6.BackColor = GetBoolColor(Value(5))
        Reg_7.BackColor = GetBoolColor(Value(6))
        Reg_8.BackColor = GetBoolColor(Value(7))
    End Sub

    Private Sub Handle_TitleCount_SET(ByVal Value As Short) Handles Player.evGlobalTitleCount_SET
        lblEXDB_TitleCount.Text = Value
    End Sub

    Private Sub Handle_VTSCount_SET(ByVal Value As Short) Handles Player.evVTSCount_SET
        lblVTSsTotal.Text = Value
    End Sub

    Private Sub HandleClearConsole() Handles Player.evClearConsole
        Me.lvConsole.Items.Clear()
    End Sub

    Private Sub HandleConsoleLine(ByVal Type As eConsoleItemType, ByVal Msg As String, ByVal ExtendedInfo As String, ByVal GOPTC As cTimecode) Handles Player.evConsoleLine
        Me.AddConsoleLine(Type, Msg, ExtendedInfo, GOPTC)
    End Sub

    Private Sub HandleWrongRegion(ByVal PlayerRegion As Byte, ByVal ProjectRegions() As Boolean) Handles Player.evWrongRegion
        Player.PlayState = ePlayState.SystemJP 'to prevent the one second timer from trying to update the screen - graph is currently 'broken'
        Dim dlg As New dlgWrongRegion(PlayerRegion, ProjectRegions, Me)
        If dlg.ShowDialog() = DialogResult.OK Then
        End If
        RebootProject()
    End Sub

    Private Sub HandleCurrentBitrate(ByVal Bitrate As Integer) Handles Player.evCurrentBitrateNotification
        UpdateBitrate(Bitrate)
    End Sub

    Private Sub HandleFrameRateChange(ByVal NewRate As Double) Handles Player.evFrameRateChange
        lblCurrentFrameRate.Text = NewRate
    End Sub

    Private Sub HandlePlaybackStarted() Handles Player.evPlaybackStarted
        'UpdateDashboard()
        AB_Clear()
    End Sub

    Private Sub HandlePlaybackStopped(ByVal Success As Boolean) Handles Player.evPlaybackStopped
        btnRM_Stop.BackColor = SystemColors.Control
        btnRM_Pause.BackColor = SystemColors.Control
        btnRM_FastForward.BackColor = SystemColors.Control
        btnRM_Rewind.BackColor = SystemColors.Control
        btnRM_Stop.BackColor = SystemColors.ControlDarkDark
        btnRM_Play.BackColor = SystemColors.Control
        btnRM_Stop.BackColor = SystemColors.ControlDarkDark
    End Sub

    Private Sub HandleProjectEjected(ByVal FromDoubleStopState As Boolean) Handles Player.evProjectEjected
        Try
            ClearDashboard(False, True)
            Me.btnPS_Eject.Enabled = False
            Me.btnPS_Reboot.Enabled = False
            Me.miFILE_Eject.Enabled = False
        Catch ex As Exception
            Me.AddConsoleLine(eConsoleItemType.ERROR, "Problem with HandleProjectEjected(). Error: " & ex.Message)
        End Try
    End Sub

    Private Sub HandlePlaybackPaused(ByVal Paused As Boolean) Handles Player.evPlaybackPaused
        If Paused Then
            btnRM_Pause.BackColor = SystemColors.Control
            btnRM_FastForward.BackColor = SystemColors.Control
            btnRM_Rewind.BackColor = SystemColors.Control
            btnRM_Stop.BackColor = SystemColors.Control
            btnRM_Play.BackColor = SystemColors.ControlDarkDark
        Else
            btnRM_Pause.BackColor = SystemColors.ControlDarkDark
            btnRM_FastForward.BackColor = SystemColors.Control
            btnRM_Rewind.BackColor = SystemColors.Control
            btnRM_Stop.BackColor = SystemColors.Control
            btnRM_Play.BackColor = SystemColors.Control
        End If
        UpdateDashboard()
    End Sub

    Private Sub HandleFastForward(ByVal Rate As Double) Handles Player.evFastForward
        btnRM_Pause.BackColor = SystemColors.Control
        btnRM_FastForward.BackColor = SystemColors.ControlDarkDark
        btnRM_Rewind.BackColor = SystemColors.Control
        btnRM_Stop.BackColor = SystemColors.Control
        btnRM_Play.BackColor = SystemColors.Control
    End Sub

    Private Sub HandleRewind(ByVal Rate As Double) Handles Player.evRewind
        btnRM_Pause.BackColor = SystemColors.Control
        btnRM_FastForward.BackColor = SystemColors.Control
        btnRM_Rewind.BackColor = SystemColors.ControlDarkDark
        btnRM_Stop.BackColor = SystemColors.Control
        btnRM_Play.BackColor = SystemColors.Control
    End Sub

    Private Sub HandleAudioCycled() Handles Player.evAudioCycled
        Try
            For i As Short = 0 To dgAudioStreams.VisibleRowCount - 1
                dgAudioStreams.UnSelect(i)
            Next
            dgAudioStreams.Select(Player.CurrentAudioStreamNumber)
            'If Player.CurrentAudioStreamNumber = Player.CurrentAudioStreamCount Then
            '    dgAudioStreams.Select(0)
            'Else
            'End If
        Catch ex As Exception
            'Debug.WriteLine("Could not select current audio stream in the data grid.")
        End Try
    End Sub

    Private Sub HandleAudioStreamSet(ByVal StreamNumber As Short) Handles Player.evAudioStreamSet
        lblCurrentAudACMOD.Text = ""
        lblCurrentAud_Bitrate.Text = ""
        lblCurrentAudio_ext.Text = ""
        lblCurrentAudio_format.Text = ""
        lblCurrentAudio_lang.Text = ""
        lblCurrentAudio_sn.Text = ""

        Try
            For i As Short = 0 To dgAudioStreams.VisibleRowCount - 1
                dgAudioStreams.UnSelect(i)
            Next
            dgAudioStreams.Select(StreamNumber)
        Catch ex As Exception
            'Debug.WriteLine("Could not select current audio stream in the data grid.")
        End Try
    End Sub

    Private Sub HandleAudioStreamChanged(ByVal CurrentStream As Integer, ByVal NumberOfStreams As Integer)
        Try
            If Player.PlayState = ePlayState.SystemJP Then Exit Sub

            If CurrentStream = Nothing Or NumberOfStreams = Nothing Then
                Player.GetAudioStreamStatus(NumberOfStreams, CurrentStream)
            End If

            Dim A As seqAudio = Player.GetAudio(CurrentStream)
            If Player.PlayState = ePlayState.SystemJP Then
                lblCurrentAudio_lang.Text = ""
                lblCurrentAudio_sn.Text = ""
                lblCurrentAudio_ext.Text = ""
                lblCurrentAudio_format.Text = ""
                lblCurrentAudACMOD.Text = ""
                lblCurrentAud_Bitrate.Text = ""
            Else
                lblCurrentAudio_lang.Text = A.Language
                lblCurrentAudio_sn.Text = CurrentStream
                lblCurrentAudio_ext.Text = A.Extension
                lblCurrentAudio_format.Text = A.Format.ToUpper

                Dim i As Integer = Player.GetAudioBitrate

                If InStr(A.Format.ToLower, "ac3") Then
                    lblCurrentAud_Bitrate.Text = (i / 1024) & "k"

                    i = Player.GetAudioAC3_ACMOD
                    lblCurrentAudACMOD.Text = GetAC3ChannelMappingFromAppModeData(i)

                    i = Player.GetAudioSurroundEncoded

                    If i = 2 Then
                        lblCurrentAudACMOD.Text &= " -SUR"
                    End If

                    If Player.CurrentAudioIsAC3EX Then
                        lblCurrentAudio_format.Text &= "-EX"
                    End If

                ElseIf InStr(A.Format.ToLower, "dts") Then
                    lblCurrentAudACMOD.Text = ""
                    lblCurrentAud_Bitrate.Text = (i / 1000) & "k"
                Else
                    lblCurrentAudACMOD.Text = ""
                End If
            End If

            A = Nothing
        Catch ex As Exception
            If Player.StartingDVD Then Exit Sub
            If Player.PlayState = ePlayState.SystemJP Then Exit Sub
            If InStr(ex.Message, "80040290", CompareMethod.Text) Then Exit Sub
            AddConsoleLine(eConsoleItemType.ERROR, "problem on audio stream change. error: " & ex.Message, Nothing, Nothing)
        End Try
    End Sub

    'Private Sub HandleSubtitlesCycled() Handles Player.evSubtitlesCycled
    '    Try
    '        For i As Short = 0 To dgSubtitles.VisibleRowCount - 1
    '            dgSubtitles.UnSelect(i)
    '        Next
    '        If Player.SubtitlesAreOn Then
    '            dgSubtitles.Select(Player.CurrentSubtitleStreamNumber)
    '        End If
    '    Catch ex As Exception
    '        'Debug.WriteLine("Could not select current subtitle stream in the data grid.")
    '    End Try
    'End Sub

    'Private Sub HandleSubtitlesToggled(ByVal TurnedOn As Boolean) Handles Player.evSubtitlesToggled
    '    If TurnedOn Then
    '        lblCurrentSP_onoff.Text = "On"
    '        Me.btnRM_ToggleSubtitles.ButtonStyle = BorderStyles.Office2003
    '    Else
    '        lblCurrentSP_onoff.Text = "Off"
    '        Me.btnRM_ToggleSubtitles.ButtonStyle = BorderStyles.Default
    '    End If
    'End Sub

    Private Sub DVDPlaybackRateReturnedToOneX() Handles Player.evDVDPlaybackRateReturnedToOneX
        btnRM_Pause.BackColor = SystemColors.Control
        btnRM_FastForward.BackColor = SystemColors.Control
        btnRM_Rewind.BackColor = SystemColors.Control
        btnRM_Stop.BackColor = SystemColors.Control
        btnRM_Play.BackColor = SystemColors.ControlDarkDark
    End Sub

    Private Sub HandleSubtitleStreamChanged(ByVal NewStreamNumber As Byte) Handles Player.evSubtitleStreamChanged
        Try
            If NewStreamNumber = Nothing Then
                NewStreamNumber = Player.CurrentSubtitleStreamNumber()
            End If

            For i As Short = 0 To dgSubtitles.VisibleRowCount - 1
                dgSubtitles.UnSelect(i)
            Next

            If Player.SubtitlesAreOn Then
                dgSubtitles.Select(NewStreamNumber)
            End If

            Debug.WriteLine("SubtitleStreamChanged()")
            Try
                If Player.CurrentDomain = DvdDomain.VideoManagerMenu Or Player.CurrentDomain = DvdDomain.VideoTitleSetMenu Or Player.PlayState = ePlayState.SystemJP Then
                    lblCurrentSP_onoff.Text = ""
                    lblCurrentSP_lang.Text = ""
                    lblCurrentSP_sn.Text = ""
                    lblCurrentSP_ext.Text = ""
                    Exit Sub
                End If

                Dim S As seqSub = Player.GetSubtitle(Player.CurrentSubtitleStreamNumber)
                lblCurrentSP_lang.Text = S.Language
                lblCurrentSP_sn.Text = Player.CurrentSubtitleStreamNumber
                lblCurrentSP_ext.Text = S.Extension

                For i As Short = 0 To dgSubtitles.VisibleRowCount - 1
                    dgSubtitles.UnSelect(i)
                Next

                If Player.SubtitlesAreOn Then
                    lblCurrentSP_onoff.Text = "On"
                    Me.btnRM_ToggleSubtitles.ButtonStyle = BorderStyles.Office2003
                    dgSubtitles.Select(Player.CurrentSubtitleStreamNumber)
                Else
                    lblCurrentSP_onoff.Text = "Off"
                    Me.btnRM_ToggleSubtitles.ButtonStyle = BorderStyles.Default
                End If

                S = Nothing

            Catch ex As Exception
                If Player.StartingDVD Then Exit Sub
                If Player.PlayState = ePlayState.SystemJP Then Exit Sub
                If InStr(ex.Message, "80040290", CompareMethod.Text) Then Exit Sub
                AddConsoleLine(eConsoleItemType.ERROR, "problem on subtitle stream change. error: " & ex.Message, Nothing, Nothing)
            End Try
        Catch ex As Exception
            'Debug.WriteLine("Could not select current subtitle stream in the data grid.")
        End Try
    End Sub

    Private Sub HandleAngleCycled(ByVal NewAngle As Byte) Handles Player.evAngleCycled
        lblCurrent_angle.Text = NewAngle
    End Sub

    Private Sub HandleClosedCaptionToggle() Handles Player.evClosedCaptionToggle
        If Player.ClosedCaptionsAreOn Then
            Me.btnRM_ToggleClosedCaptions.ButtonStyle = BorderStyles.Office2003
        Else
            Me.btnRM_ToggleClosedCaptions.ButtonStyle = BorderStyles.Default
        End If
    End Sub

    Private Sub HandleRunningTimeTick() Handles Player.evRunningTimeTick
        UpdateDashboard()
        UpdatePlayPosition()

        If Player.CurrentDomain = DvdDomain.VideoManagerMenu Or Player.CurrentDomain = DvdDomain.VideoTitleSetMenu Then
            If CompareDVDTimecodes(Player.CurrentRunningTime_DVD, Player.CurrentRunningTime_DVD) = 2 Then
                'it has looped, store the loop time
                lblTotalRunningTime.Text = Player.CurrentRunningTime_DVD.bHours.ToString & ":" & PadString(Player.CurrentRunningTime_DVD.bMinutes.ToString, 2, "0", True) & ":" & PadString(Player.CurrentRunningTime_DVD.bSeconds.ToString, 2, "0", True) '& ";" & F
            End If
        End If
    End Sub

    Private Sub HandleKEYSTONE_INTERLACING(ByVal Interlaced As Boolean) Handles Player.evKEYSTONE_Interlacing
        If Not Player.PlayState = ePlayState.SystemJP Then
            If Me.CurrentUserProfile.AppOptions.BeepOnVideoPropChange Then
                Beep(500, 125)
            End If
            'AddConsoleLine(eConsoleItemType.NOTICE, "Video source interlacing changed", Player.Interlaced, Nothing)
            lblCurrentVideoIsInterlaced.Text = Player.Interlaced.ToString
        Else
            lblCurrentVideoIsInterlaced.Text = ""
        End If
    End Sub

    Public Sub HandleKEYSTONE_FIELDORDER(ByVal TopFieldFirst As Boolean) Handles Player.evKEYSTONE_FieldOrder
        If Not Player.PlayState = ePlayState.SystemJP Then
            Dim s As String
            If TopFieldFirst Then
                s = "Top"
            Else
                s = "Bottom"
            End If

            If s <> lbl32WhichField.Text Then
                If Me.CurrentUserProfile.AppOptions.BeepOnVideoPropChange Then
                    Beep(500, 125)
                End If
                AddConsoleLine(eConsoleItemType.NOTICE, "Video field order changed", s, Nothing)
                lbl32WhichField.Text = s
            End If
        Else
            lbl32WhichField.Text = ""
        End If
    End Sub

    Public Sub HandleMPEG_Timecode(ByVal TC As cTimecode) Handles Player.evMPEG_Timecode
        lblCurrentSourceTimecode.Text = TC.ToString_NoFrames
    End Sub

    Public Sub HandlePROGRESSIVE_SEQUENCE(ByVal ProgressiveSequence As Boolean) Handles Player.evKEYSTONE_ProgressiveSequence

    End Sub

    Public Sub HandleMacrovisionStatus(ByVal Status As String) Handles Player.evMacrovisionStatus
        Me.lblMacrovision.Text = Status
    End Sub

    Public Sub HandleLayerbreakSet(ByVal Location As cNonSeamlessCell) Handles Player.evLayerbreakSet
        lblLBCh.Text = Location.PTT
        lblLBTT.Text = Location.GTTn
        lblLBTC.Text = Location.LBTCToString
    End Sub

    Public Sub HandleAngleChanged(ByVal NewAngle As Byte) Handles Player.evAngleChanged
        PopulateVideoStreamInfoWindow("AngleChanged")
    End Sub

    Public Sub HandleJacketPicturesFound(ByVal JacketPicturePaths() As String) Handles Player.evJacketPicturesFound
        lbJacketPictures.Items.Clear()
        If Not FeatureManagement.Features.FE_DVD_JacketPictures Then Exit Sub
        For Each s As String In JacketPicturePaths
            lbJacketPictures.Items.Add(New cJacketPicture(s))
        Next
    End Sub

    Public Sub HandleCorruptedVideo(ByVal VSETC As sTransferErrorTime) Handles Player.evCorruptedVideoData
        Me.AddConsoleLine(eConsoleItemType.WARNING, "Corrupted video data.", VSETC.RunningTime.ToString, VSETC.SourceTime)
        Dim s As String = ""
        Dim mbb As MessageBoxButtons = MessageBoxButtons.OK
        If Not Player.PlayerType <> ePlayerType.DVD And Player.CurrentDomain = DvdDomain.Title Then
            s = vbNewLine & vbNewLine & "Would you like to JumpBack?"
            mbb = MessageBoxButtons.YesNo
        End If

        KH.PauseHook()
        If XtraMessageBox.Show(Me.LookAndFeel, Me, "Video stream data is corrupted." & s, My.Settings.APPLICATION_NAME, mbb, MessageBoxIcon.Stop) = MsgBoxResult.Yes Then
            Dim tc1 As New cTimecode(VSETC.RunningTime.timeCode, (Player.CurrentVideoStandard = eVideoStandard.NTSC))
            tc1.Subtract(0, 0, 5, 0)
            Player.PlayAtTime_Clean(tc1.DVDTimeCode, False)
            'Player.PlayAtTimeInTitle(tc1.DVDTimeCode, lblEXDB_CurrentTitle.Text)
        End If
        KH.UnpauseHook()
        Player.CloseCorruptVideoDataDlg()
    End Sub

    'Private Sub HandlePlayerModeSet(ByVal Mode As ePlayerType) Handles Player.evPlaybackPaused
    '    Me.AddConsoleLine(eConsoleItemType.NOTICE, "Player mode set to: " & Mode.ToString)
    'End Sub

    Private Sub HandleProjectHasCallSSRSM127() Handles Player.evProjectHasCallSSRSM127
        Try
            Dim Locations() As cDVDLocation = Player.CurrentDVD.CallSSRSM127_Location
            Me.EjectProject()
            For Each Location As cDVDLocation In Locations
                XtraMessageBox.Show(Me.LookAndFeel, Me, """CallSS RSM=127"" command found in the current project at the following location: " & Location.ToString & vbNewLine & vbNewLine & "The project cannot run.", My.Settings.APPLICATION_NAME, MessageBoxButtons.OK, MessageBoxIcon.Stop)
            Next
        Catch ex As Exception
            Me.AddConsoleLine(eConsoleItemType.ERROR, "Problem with HandleProjectHasCallSSRSM127(). Error: " & ex.Message)
        End Try
    End Sub

    Public Sub HandlePlayerKeyStrike(ByVal e As KeyEventArgs) Handles Player.evKeyStrike
        Me.HandleKeyStrike(e)
    End Sub

    Public Sub HandleMacroEvent(ByVal Command As cDVDPlayer.eMacroCommand, ByVal ExtendedData As Long) Handles Player.evMacroItemExecuted
        Me.AddMacroItem(Command, ExtendedData)
    End Sub

    Public Sub HandleMacroCompleted() Handles Player.evMacroCompleted
        Me.MC_gbAvailableMacros.Enabled = True
        Player.LoadMacro(Player.CurrentMacroSourceFilePath)
        'Me.MC_lbMacroItems.Items.Clear()
    End Sub

    Public Sub HandleCellStillTime(ByVal Time As Integer) Handles Player.evCellStillTime
        Select Case Time
            Case &HFFFFFFFF
                'infinite cell still
                'Debug.WriteLine("Cell Still: Infinite")
                Me.lblCellStillTime.Text = "INF"
            Case 0
                Me.lblCellStillTime.Text = ""
            Case Else
                'Debug.WriteLine("Cell Still: " & Time)
                Me.lblCellStillTime.Text = Time
        End Select
    End Sub

    Public Sub HandleTripleStop() Handles Player.evTripleStop
        Me.EjectProject()
    End Sub

    Public Sub HandleDroppedFrame() Handles Player.evFrameDropped
        If Player.AVMode <> eAVMode.DesktopVMR And Player.PlayState = ePlayState.Playing Then
            'Me.AddConsoleLine(eConsoleItemType.NOTICE, "Frame Dropped", Nothing, Nothing)
        End If
    End Sub

    Public Sub HandleFailedDoubleStopPlay() Handles Player.evDoubleStopPlay_UOP2_Prohibited
        XtraMessageBox.Show(Me.LookAndFeel, Me, "Phoenix is unable to seek to the beginning of title one due to UOP restrictions." & vbNewLine & "This is a difference between Phoenix and most set-top players." & vbNewLine & "Phoenix's inability to go to the beginning of title 1 in this situation is not an indication of a problem in the authoring.", My.Settings.APPLICATION_NAME, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    End Sub

    Public Sub HandleABLoopCleared() Handles Player.evABLoopCleared
        Me.btnABLoop_A.ButtonStyle = BorderStyles.Default
        Me.btnABLoop_B.ButtonStyle = BorderStyles.Default
        Me.btnABLoop_B.Enabled = False
    End Sub

    Public Sub HandleKeystoneTimeout() Handles Player.evKeystoneTimeout
        Try
            XtraMessageBox.Show(Me.LookAndFeel, Me, "Trial timer has expired.", My.Settings.APPLICATION_NAME, MessageBoxButtons.OK, MessageBoxIcon.Stop)
        Catch ex As Exception

        End Try
        Process.GetCurrentProcess.Kill()
    End Sub

#End Region 'PLAYER:EVENTS:MISC

#End Region 'PLAYER:EVENTS

#End Region 'PLAYER

#Region "VIEWER"

    Private Sub SetupViewer() Handles Player.evSetupViewer
        Try
            If Viewer IsNot Nothing Then Viewer.Close()
            Viewer = New SMT.Multimedia.UI.WPF.Viewer.Viewer_WPF(Player, "Phoenix")
            Viewer.Icon = Phoenix_WPFIcon
            Viewer.Left = My.Settings.WINDOW_VIEWER_X
            Viewer.Top = My.Settings.WINDOW_VIEWER_Y
            Viewer.Show()
            Viewer.ViewerSize = eViewerSize.HD_Quarter_480x270
        Catch ex As Exception
            Throw New Exception("Problem with SetupViewer(). Error: " & ex.Message, ex)
        End Try
    End Sub

    Public ReadOnly Property Phoenix_WPFIcon() As System.Windows.Media.ImageSource
        Get
            Dim str As Stream = Me.GetType.Module.Assembly.GetManifestResourceStream("SMT.Applications.Phoenix.phx_070509.ico")
            Dim ibd As New System.Windows.Media.Imaging.IconBitmapDecoder(str, System.Windows.Media.Imaging.BitmapCreateOptions.None, System.Windows.Media.Imaging.BitmapCacheOption.Default)
            Return ibd.Frames(3)
        End Get
    End Property

    Private Sub ViewerActivated(ByVal sender As Object, ByVal e As EventArgs) Handles Viewer.Activated
        If KH Is Nothing Then Exit Sub
        KH.ParentContainsFocusOverride = True
    End Sub

    Private Sub ViewerDeactivated(ByVal sender As Object, ByVal e As EventArgs) Handles Viewer.Deactivated
        If KH Is Nothing Then Exit Sub
        KH.ParentContainsFocusOverride = False
    End Sub

    Private Sub ViewerLocationChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Viewer.LocationChanged
        My.Settings.WINDOW_VIEWER_X = Viewer.Left
        My.Settings.WINDOW_VIEWER_Y = Viewer.Top
    End Sub

#End Region 'VIEWER

#Region "ACCELERATORS"

    Public Sub ShowTimeSearchAccelerator()
        Try
            Dim dlg As New dlgTimesearchAccelerator(Me)
            If dlg.ShowDialog = DialogResult.OK Then
                'hi
            End If
        Catch ex As Exception
            Me.AddConsoleLine(eConsoleItemType.ERROR, "Problem with TimeSearch Accelerator. Error: " & ex.Message, Nothing)
        End Try
    End Sub

    Public Sub ShowChapterSearchAccelerator()
        Try
            If Player.CurrentDomain <> DvdDomain.Title Then Exit Sub
            Dim dlg As New dlgCHTTSearchAccelerator(Me, eSearchTypes.Chapter)
            If dlg.ShowDialog = DialogResult.OK Then
                'hi
            End If
        Catch ex As Exception
            Me.AddConsoleLine(eConsoleItemType.ERROR, "Problem with ShowChapterSearchAccelerator(). Error: " & ex.Message, Nothing)
        End Try
    End Sub

    Public Sub ShowTitleSearchAccelerator()
        Try
            Dim dlg As New dlgCHTTSearchAccelerator(Me, eSearchTypes.Title)
            If dlg.ShowDialog = DialogResult.OK Then
                'hi
            End If
        Catch ex As Exception
            Me.AddConsoleLine(eConsoleItemType.ERROR, "Problem with ShowTitleSearchAccelerator(). Error: " & ex.Message, Nothing)
        End Try
    End Sub

    Public Sub ShowAudioSelectAccelerator()
        Try
            If Player.CurrentDomain <> DvdDomain.Title Then Exit Sub
            Dim dlg As New dlgCHTTSearchAccelerator(Me, eSearchTypes.AudioStream)
            If dlg.ShowDialog = DialogResult.OK Then
                'hi
            End If
        Catch ex As Exception
            Me.AddConsoleLine(eConsoleItemType.ERROR, "Problem with ShowAudioSelectAccelerator(). Error: " & ex.Message, Nothing)
        End Try
    End Sub

    Public Sub ShowSubtitleSelectAccelerator()
        Try
            If Player.CurrentDomain <> DvdDomain.Title Then Exit Sub
            Dim dlg As New dlgCHTTSearchAccelerator(Me, eSearchTypes.SubtitleStream)
            If dlg.ShowDialog = DialogResult.OK Then
                'hi
            End If
        Catch ex As Exception
            Me.AddConsoleLine(eConsoleItemType.ERROR, "Problem with ShowSubtitleSelectAccelerator(). Error: " & ex.Message, Nothing)
        End Try
    End Sub

#End Region 'ACCELERATORS

#Region "USER PROFILE"

    Public CurrentUserProfile As cPhoenixUserProfile

    Public Function InitializeUserProfile() As Boolean
        Try

            ''DEBUGGING
            'Return True
            ''DEBUGGING

            Dim result As Boolean = False

            Dim DP As String = My.Settings.DEFAULT_PROFILE

            'is there a recent user profile to use? if not just load defaults
            If My.Settings.LAST_PROFILE_PATH Is Nothing OrElse My.Settings.LAST_PROFILE_PATH = "" Then
LoadDefaults:
                Select Case My.Settings.DEFAULT_PROFILE.ToLower
                    Case "1920x1200"
                        LoadUserProfile_FromResource("1920x1200.pup")
                        Me.AddConsoleLine(eConsoleItemType.NOTICE, "Default profile loaded.")
                        result = True
                    Case "alternate1152x864"
                        LoadUserProfile_FromResource("1152x864_alt.pup")
                        Me.AddConsoleLine(eConsoleItemType.NOTICE, "Alternate 1152x864 profile loaded.")
                        result = True
                    Case Else 'Including "STANDARD"
                        Me.LoadUserProfile_Defaults(eDefaultProfileVersion.Standard)
                        'Me.AddConsoleLine(eConsoleItemType.NOTICE, "Default profile loaded.")
                        result = True
                End Select

            ElseIf InStr(My.Settings.LAST_PROFILE_PATH, "****") > 0 Then
                'last profile is a built-in profile
                Select Case My.Settings.LAST_PROFILE_PATH.ToLower
                    Case "****standard"
                        Me.LoadUserProfile_Defaults(eDefaultProfileVersion.Standard)
                    Case "****advanced"
                        Me.LoadUserProfile_Defaults(eDefaultProfileVersion.Advanced)
                    Case "****stream"
                        Me.LoadUserProfile_Defaults(eDefaultProfileVersion.StreamQC)
                    Case "****alternate1152x864"
                        Me.LoadUserProfile_FromResource("1152x864_alt.pup")
                    Case "****1920x1200"
                        Me.LoadUserProfile_FromResource("1920x1200.pup")
                End Select
                'Me.AddConsoleLine(eConsoleItemType.NOTICE, "Default profile loaded.")
                result = True
            Else
                'Last profile is a path to a profile
                If Not File.Exists(My.Settings.LAST_PROFILE_PATH) Then
                    GoTo LoadDefaults
                Else
                    LoadProfile_FromPath(My.Settings.LAST_PROFILE_PATH, True)
                    'Me.AddConsoleLine(eConsoleItemType.NOTICE, "Profile loaded: " & Path.GetFileName(My.Settings.LAST_PROFILE_PATH))
                End If
            End If

            'SETUP MACHINE-BOUND PREFERENCES
            CurrentUserProfile.AppOptions.IntensityVideoScalingMode = My.Settings.PREFERRED_HDMI_SCALINGMODE
            CurrentUserProfile.AppOptions.IntensityVideoResolution = My.Settings.PREFERRED_HDMI_RESOLUTION
            CurrentUserProfile.AppOptions.IntensityVideoMaximized = My.Settings.PREFERRED_HDMI_MAXIMIZED

            Return result

        Catch ex As Exception
            Me.AddConsoleLine(eConsoleItemType.ERROR, "Problem with InitializeUserProfile(). Error: " & ex.Message)
        End Try
    End Function

#Region "Profile Loading"

    Public Function LoadUserProfile_WithDialog() As Boolean
        Try
            KH.PauseHook()
            Dim dlg As New OpenFileDialog
            dlg.Filter = "User Profiles | *.pup"
            dlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            dlg.Multiselect = False
            dlg.Title = "Load User Profile"
            If dlg.ShowDialog = DialogResult.OK Then
                KH.UnpauseHook()
                Return Me.LoadProfile_FromPath(dlg.FileName, True)
            End If
            KH.UnpauseHook()
            Return True
        Catch ex As Exception
            Me.AddConsoleLine(eConsoleItemType.ERROR, "Problem with LoadUserProfile_WithDialog. Error: " & ex.Message)
        End Try
    End Function

    Public Function LoadUserProfile_Defaults(ByVal WhichDefaults As eDefaultProfileVersion) As Boolean
        Try
            Select Case WhichDefaults
                Case eDefaultProfileVersion.Standard
                    Me.CurrentUserProfile = New cPhoenixUserProfile(Me)
                    Me.CurrentUserProfile.InitializeWithDefaults()
                    My.Settings.LAST_PROFILE_PATH = "****standard"
                    Me.mnuPROFILES.Caption = "&Profile (Standard)"
                    Me.pgOptions.Update()
                    Me.pgOptions.Refresh()
                    Return True
                Case Else
                    Return False
            End Select
        Catch ex As Exception
            Me.AddConsoleLine(eConsoleItemType.ERROR, "Problem with LoadUserProfile_Defaults(). Error: " & ex.Message)
            Return False
        End Try
    End Function

    Public Function LoadUserProfile_FromResource(ByVal ProfileName As String) As Boolean
        Try
            Dim Assm As System.Reflection.Assembly = Me.GetType().Assembly.GetEntryAssembly
            Dim ProfileStream As Stream = Assm.GetManifestResourceStream("SMT.Applications.Phoenix." & ProfileName)
            Dim StR As New StreamReader(ProfileStream)
            Dim inString As String = StR.ReadToEnd
            StR.Close()
            ProfileStream.Close()

            Me.CurrentUserProfile = New cPhoenixUserProfile(inString, True, Me)
            My.Settings.LAST_PROFILE_PATH = ProfileName
            Me.mnuPROFILES.Caption = "&Profile (" & ProfileName & ")"
            Me.pgOptions.Update()
            Me.pgOptions.Refresh()
            Return True
        Catch ex As Exception
            Me.AddConsoleLine(eConsoleItemType.ERROR, "Problem with LoadUserProfile_FromResource. Error: " & ex.Message)
            Return False
        End Try
    End Function

    Public Function LoadProfile_FromPath(ByVal PathToProfile As String, ByVal AddProjectToCache As Boolean) As Boolean
        Try
            If Not File.Exists(PathToProfile) Then Throw New Exception("Profile at path: " & PathToProfile & " does not exist.")
            Me.CurrentUserProfile = New cPhoenixUserProfile(PathToProfile, False, Me)
            Me.mnuPROFILES.Caption = "&Profile (" & Path.GetFileNameWithoutExtension(PathToProfile) & ")"
            If AddProjectToCache Then
                Me.AddProfileFileToCache(PathToProfile)
            End If
            My.Settings.LAST_PROFILE_PATH = PathToProfile
            'Me.AddConsoleLine(eConsoleItemType.NOTICE, "Profile loaded: " & PathToProfile)
            Me.pgOptions.Update()
            Me.pgOptions.Refresh()
            Return True
        Catch ex As Exception
            Me.AddConsoleLine(eConsoleItemType.ERROR, "Problem with LoadUserProfile. Error: " & ex.Message)
            Return False
        End Try
    End Function

#End Region 'Profile Loading

#Region "Profile Caching"

    Public Sub AddProfileFileToCache(ByVal ProfilePath As String)
        Try
            'We're only going to keep the last five files in the cache.
            'copy file to cache, overwrite if needed
            'if overwriting then don't delete the oldest one
            Dim D As Boolean = True
            Dim di As New DirectoryInfo(ProfileCacheDirectory)
            Dim FIa() As FileInfo = di.GetFiles

            For Each fi As FileInfo In FIa
                If fi.Name.ToLower = Path.GetFileName(ProfilePath).ToLower Then
                    D = False
                    File.Delete(ProfileCacheDirectory & fi.Name)
                End If
            Next

            If FIa.Length < 5 Then
                D = False
            Else

            End If

            Dim dest As String = ProfileCacheDirectory & Path.GetFileName(ProfilePath)
            If File.Exists(dest) Then File.Delete(dest)

            File.Copy(ProfilePath, dest, True)
            'Dim f As New FileInfo(ProfileCacheDirectory & Path.GetFileName(ProfilePath))
            'f.Attributes = FileAttributes.Hidden

            If D Then
                'delete oldest file in cache
                Dim FC As New cGetOldestFileInDir
                Array.Sort(FIa, FC)
                File.Delete(FIa(0).Name)
            End If
        Catch ex As Exception
            Me.AddConsoleLine(eConsoleItemType.ERROR, "Problem with AddProfileFileToCache. Error: " & ex.Message)
        End Try
    End Sub

    Public ReadOnly Property ProfileCacheDirectory() As String
        Get
            Dim CD As String = GetExePath() & "ProfileCache\"
            If Not Directory.Exists(CD) Then
                Directory.CreateDirectory(CD)
                Dim DI As New DirectoryInfo(CD)
                DI.Attributes = FileAttributes.Hidden
            End If
            Return CD
        End Get
    End Property

    Public Sub LoadCachedProfilesIntoMenu()
        Try
            Me.miPROFILES_CACHEDPROFILES.ClearLinks()
            Dim di As New DirectoryInfo(ProfileCacheDirectory)
            Dim FIa() As FileInfo = di.GetFiles
            If FIa.Length = 0 Then
                Me.miPROFILES_CACHEDPROFILES.AddItem(New DevExpress.XtraBars.BarButtonItem(barMain, "NONE AVAILABLE"))
                Exit Sub
            End If
            Dim BBI As DevExpress.XtraBars.BarButtonItem
            For Each fi As FileInfo In FIa
                BBI = New DevExpress.XtraBars.BarButtonItem(barMain, Path.GetFileNameWithoutExtension(fi.Name))
                AddHandler BBI.ItemClick, New DevExpress.XtraBars.ItemClickEventHandler(AddressOf CachedProfileMenuItemClickHandler)
                Me.miPROFILES_CACHEDPROFILES.AddItem(BBI)
            Next
        Catch ex As Exception
            Me.AddConsoleLine(eConsoleItemType.ERROR, "Problem with LoadCachedProfilesIntoMenu. Error: " & ex.Message)
        End Try
    End Sub

    Public Sub CachedProfileMenuItemClickHandler(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs)
        Try
            Dim di As New DirectoryInfo(ProfileCacheDirectory)
            Dim FIa() As FileInfo = di.GetFiles
            Dim ClickedItemText As String = CType(e.Item, DevExpress.XtraBars.BarButtonItem).Caption.ToLower
            For Each FI As FileInfo In FIa
                If Path.GetFileNameWithoutExtension(FI.Name).ToLower = ClickedItemText Then
                    LoadProfile_FromPath(FI.FullName, False)
                    Exit Sub
                End If
            Next
        Catch ex As Exception
            Me.AddConsoleLine(eConsoleItemType.ERROR, "Problem with CachedProfileMenuItemClickHandler. Error: " & ex.Message)
        End Try
    End Sub

    Public Sub PurgeProfileCache()
        Try
            Me.miPROFILES_CACHEDPROFILES.ClearLinks()
            Me.miPROFILES_CACHEDPROFILES.AddItem(New DevExpress.XtraBars.BarButtonItem(barMain, "NONE AVAILABLE"))
            Dim di As New DirectoryInfo(ProfileCacheDirectory)
            Dim FIa() As FileInfo = di.GetFiles
            For Each fi As FileInfo In FIa
                File.Delete(fi.FullName)
            Next
        Catch ex As Exception
            Me.AddConsoleLine(eConsoleItemType.ERROR, "Problem with PurgeProfileCache(). Error: " & ex.Message)
        End Try
    End Sub

#End Region 'Profile Caching

#End Region 'USER PROFILE

#Region "KEYBOARD HOOK"

    Public WithEvents KH As cKeyboardHook

    Public Sub ppHookupKeyboard()
        Try
            If KH Is Nothing Then
                KH = New cKeyboardHook(Me)
            End If
            If Not KH.HookKeyboard() Then
                Throw New Exception("Keyboard hook failed.")
            End If
        Catch ex As Exception
            Me.AddConsoleLine(eConsoleItemType.ERROR, "Problem with PP_HookupKeyboard. Error: " & ex.Message)
        End Try
    End Sub

    Public Sub ppUnhookKeyboard()
        Try
            KH.UnhookKeyboard()
        Catch ex As Exception
            Me.AddConsoleLine(eConsoleItemType.ERROR, "Problem with PP_UnhookKeyboard. Error: " & ex.Message)
        End Try
    End Sub

    Public Sub HandleHookedKeyPress(ByVal Code As Integer, ByVal wParam As Integer, ByVal lParam As KBDLLHOOKSTRUCT, ByVal eWPar As cKeyboardHook.eWParam, ByVal cK As cKeyboardHook.cKBDLLHOOKSTRUCT) Handles KH.KeyPress

        If cK.flags.TranlationState = cKeyboardHook.eKeyTranslationState.Released Then Exit Sub

        'Debug.WriteLine("wP: " & eWPar.ToString & vbNewLine & "lParam: " & vbNewLine & cK.ToString)

        'Debug.WriteLine("Modifier Keys: " & Control.ModifierKeys.ToString)
        If (Control.ModifierKeys And Keys.Control) <> 0 Then
            cK.vkCode = cK.vkCode Or 131072 'this is the dec of the bin for the ctrl flag bit
        End If

        Me.HandleKeyStrike(New KeyEventArgs(cK.vkCode))

    End Sub

    Public Function HandleKeyStrike(ByRef e As KeyEventArgs) As Boolean 'Handles Player.evKeyStrike 'this is also called from directly above
        Try
            If Me.pgOptions.ContainsFocus Then Return False
            'If Player.PlayState = ePlayState.SystemJP Then Return False

            If InStr(e.KeyCode.ToString, "NumPad") Or (e.KeyCode > 47 And e.KeyCode < 58) Then
                Dim Num As String
                If e.KeyCode > 47 And e.KeyCode < 58 Then
                    Num = e.KeyCode - 48
                Else
                    Num = Microsoft.VisualBasic.Right(e.KeyCode.ToString, 1)
                End If

                If IsNumeric(Num) Then
                    If DockManager.Panels("dpAudioStreamInfo").ContainsFocus Then
                        Player.SetAudioStream(Num)
                        Return True
                    ElseIf DockManager.Panels("dpSubtitleStreamInfo").ContainsFocus Then
                        Player.SetSubtitleStream(Num, True, True)
                        Return True
                    Else
                        If Num = 0 Then Return True 'zero is never a valid button number
                        Player.NumberEntry(Num)
                        Return True
                    End If
                End If
            End If

            Select Case e.KeyCode
                Case Keys.VolumeMute
                    If Player.Muting Then
                        Player.UnMute()
                    Else
                        Player.Mute()
                    End If
                    Return True

                Case Keys.MediaNextTrack
                    Player.NextChapter()
                    Return True

                Case Keys.MediaPlayPause
                    Player.PlayPauseToggle()
                    Return True

                Case Keys.MediaPreviousTrack
                    Player.PreviousChapter()
                    Return True

                Case Keys.BrowserForward
                    Player.NextChapter()
                    Return True

                Case Keys.BrowserBack
                    Player.PreviousChapter()
                    Return True

                Case Keys.MediaStop
                    Player.Stop()
                    Return True

                Case Keys.Enter
                    Player.EnterBtn()
                    Return True

                Case Keys.Up
                    Player.DirectionalBtnHit(DvdRelButton.Upper)
                    Return True

                Case Keys.Down
                    Player.DirectionalBtnHit(DvdRelButton.Lower)
                    Return True

                Case Keys.Left
                    Player.DirectionalBtnHit(DvdRelButton.Left)
                    Return True

                Case Keys.Right
                    Player.DirectionalBtnHit(DvdRelButton.Right)
                    Return True

                Case Keys.PrintScreen
                    Player.GrabScreen(CurrentUserProfile.AppOptions.FrameGrabTypeAsImageFormat, Me.CurrentUserProfile.AppOptions.FrameGrabSource, True)
                    Return True

                Case Keys.Escape
                    If Viewer IsNot Nothing Then
                        Viewer.CancelFullScreen()
                        Return True
                    End If

                Case Keys.Oemtilde
                    Viewer.ViewerSize = eViewerSize.Fullscreen
                    Return True

            End Select

            'Debug.WriteLine("Key: " & e.KeyCode.ToString & " " & e.Control)
            'Debug.WriteLine("KeyData: " & e.KeyData)

            'find operation
            Dim Key As String = mConversionsAndSuch.DecimalToBinary(e.KeyData)
            Key = PadString(Key, 19, "0", True)

            If e.Control Or e.KeyCode = Keys.RControlKey Then
                Mid(Key, 2, 1) = 1
            End If

            'If e.Alt Then
            '    Mid(Key, 1, 1) = 1
            'End If

            'If e.Shift Then
            '    Mid(Key, 3, 1) = 1
            'End If

            If CurrentUserProfile.KeyMapping Is Nothing Then Throw New Exception("CurrentKeyMapping is nothing")

            For Each PF As cPhoenixFunction In CurrentUserProfile.KeyMapping.CurrentKeyMapping
                If PF.pfKeyEventArgs IsNot Nothing AndAlso PF.pfKeyEventArgs.KeyData = e.KeyData Then
                    Return (KeyStrikeExecutionEngine(PF.FunctionName))
                End If
            Next

            'key not found in mapping
            Debug.WriteLine("Key Map Not Found For: " & e.KeyCode.ToString & " " & e.Control)
            Return False
        Catch ex As Exception
            Me.AddConsoleLine(eConsoleItemType.ERROR, "Problem with HandleKeyStrike. Error: " & ex.Message & vbNewLine & "StackTrace: " & ex.StackTrace)
            'Me.AddConsoleLine("Line 3947 is null check of CurrentKeyMapping")
            Return False
        End Try
    End Function

    Public Function KeyStrikeExecutionEngine(ByVal Operation As String) As Boolean
        Try
            'Debug.WriteLine("KeyStrikeExecutionEngine: " & Operation)

            Select Case Operation.ToLower
                Case "stop"
                    Player.Stop()
                    'Case "play"
                    '    Player.Play()
                    'Case "pause"
                    '    Player.Pause()
                Case "fastforward"
                    Player.FastForward()
                Case "rewind"
                    Player.Rewind()
                Case "cycleaudio"
                    Player.CycleAudio()
                Case "cyclesubtitles"
                    Player.CycleSubtitles()
                Case "rootmenu"
                    Player.GoToMenu(DvdMenuID.Root, Not CurrentUserProfile.AppOptions.UseSeparateResumeButton)
                Case "titlemenu"
                    Player.GoToMenu(DvdMenuID.Title)
                Case "anglemenu"
                    Player.GoToMenu(DvdMenuID.Angle)
                Case "chaptermenu"
                    Player.GoToMenu(DvdMenuID.Chapter)
                Case "subtitlemenu"
                    Player.GoToMenu(DvdMenuID.Subpicture)
                Case "audiomenu"
                    Player.GoToMenu(DvdMenuID.Audio)
                Case "chapterback"
                    Player.PreviousChapter()
                Case "chapternext"
                    Player.NextChapter()
                Case "jumpback"
                    Player.JumpBack(Me.CurrentConfig.JumpSeconds)
                Case "framestep"
                    Player.FrameStep()
                Case "resume"
                    Player.[Resume]()
                Case "goup"
                    Player.GoUp()
                Case "gotoend"
                    Player.JumpToEndOfTitle()
                Case "grabframe"
                    Player.GrabScreen(CurrentUserProfile.AppOptions.FrameGrabTypeAsImageFormat, Me.CurrentUserProfile.AppOptions.FrameGrabSource, True)
                Case "gotolayerbreak"
                    Player.GoToLayerbreak()
                Case "toggleccs"
                    If Not FeatureManagement.Features.FE_L21_Decode Then Return True
                    Player.ToggleClosedCaptions()
                Case "slowforward"
                    Player.SlowForward()
                Case "eject"
                    Me.EjectProject()
                Case "selectproject"
                    SelectProject_WithDialog()
                Case "bootproject"
                    RebootProject()
                Case "mute"
                    If Player.Muting Then
                        Player.UnMute()
                    Else
                        Player.Mute()
                    End If
                Case "upbutton"
                    Player.DirectionalBtnHit(DvdRelButton.Upper)
                Case "downbutton"
                    Player.DirectionalBtnHit(DvdRelButton.Lower)
                Case "leftbutton"
                    Player.DirectionalBtnHit(DvdRelButton.Left)
                Case "rightbutton"
                    Player.DirectionalBtnHit(DvdRelButton.Right)
                Case "activatebutton"
                    Player.EnterBtn()
                Case "resync"
                    Player.ReSyncAudio(False)
                Case "burnsourcetimecode"
                    If Not FeatureManagement.Features.FE_M2M_GOPTimecodes Then Return True
                    If CurrentUserProfile.AppOptions.BurnGOPTimecodes Then
                        Me.CurrentUserProfile.AppOptions.BurnGOPTimecodes = False
                    Else
                        Me.CurrentUserProfile.AppOptions.BurnGOPTimecodes = True
                    End If
                Case "actiontitleguides"
                    If Not FeatureManagement.Features.FE_GI_ActionTitleSafe Then Return True
                    If Me.CurrentUserProfile.AppOptions.ActionTitleGuides Then
                        CurrentUserProfile.AppOptions.ActionTitleGuides = False
                    Else
                        CurrentUserProfile.AppOptions.ActionTitleGuides = True
                    End If
                Case "newmarker"
                    'MK.NewMarker(Me.MK.cbManualMarker.Checked, Me.MK.cbSerializeMarkerNames.Checked)
                Case "multiframegrab"
                    Player.MultiFrameGrab(Me.CurrentUserProfile.AppOptions.MultiFrameCount, Me.CurrentUserProfile.AppOptions.FrameGrabTypeAsImageFormat)
                Case "timesearchaccelerator"
                    Me.ShowTimeSearchAccelerator()
                Case "chaptersearchaccelerator"
                    Me.ShowChapterSearchAccelerator()
                Case "titlesearchaccelerator"
                    Me.ShowTitleSearchAccelerator()
                Case "splitfields"
                    If Not FeatureManagement.Features.FE_VA_FieldSplitting Then Return True
                    Player.FieldSplitting = Not Player.FieldSplitting
                Case "restartchapter"
                    Player.GoToBeginningOfCurrentChapter()
                Case "gotoprenextchapter_next"
                    Player.GoToPreChapterStart(True, CurrentConfig.JumpSeconds)
                Case "gotoprenextchapter_back"
                    Player.GoToPreChapterStart(False, CurrentConfig.JumpSeconds)
                Case "togglesubtitles"
                    Player.ToggleSubtitles(Not Player.SubtitlesAreOn)
                Case "playpausetoggle"
                    Player.PlayPauseToggle()
                Case "ab_set_a"
                    AB_Set_A()
                Case "ab_set_b"
                    AB_Set_B()
                Case "ab_clear"
                    AB_Clear()
                Case "audioselectaccelerator"
                    ShowAudioSelectAccelerator()
                Case "subtitleselectaccelerator"
                    ShowSubtitleSelectAccelerator()
            End Select
            Return True
            'Debug.WriteLine("Finished KeyStrikeExecutionEngine")
        Catch ex As Exception
            Me.AddConsoleLine(eConsoleItemType.ERROR, "Problem with KeyStrikeExecutionEngine. Error: " & ex.Message)
            Return False
        End Try
    End Function

    Public Sub ToggleKeyboardHook()
        Try
            If KH Is Nothing OrElse Not KH.HasHandle Then
                'they must want it on
                KH = New cKeyboardHook(Me)
                If Not KH.HookKeyboard() Then
                    Throw New Exception("Keyboard hook failed.")
                End If
            Else
                If KH.HookIsActive Then
                    KH.PauseHook()
                Else
                    KH.UnpauseHook()
                End If
            End If
        Catch ex As Exception
            Me.AddConsoleLine(eConsoleItemType.ERROR, "Problem with ToggleKeyboardHook(). Error: " & ex.Message)
        End Try
    End Sub

    Private Sub UpdateKeyHookButton()
        If KH Is Nothing Then Exit Sub
        If KH.HookIsActive Then
            Me.btnKeyboardHook.Text = "Unhook Keyboard"
        Else
            Me.btnKeyboardHook.Text = "Hook Keyboard"
        End If
    End Sub

#End Region 'KEYBOARD HOOK

#Region "TIMERS"

    Public DVDStartPauseCnt As Short = 3
    Public TenthSecondTimerRestartCounter As Short = 5 'Number of seconds before the tenth second timer is automatically restarted

    Private Sub OneSecondTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OneSecondTimer.Tick
        Me.DongleCheck()

        If Player Is Nothing Then Exit Sub
        If Player.IsEjecting Then Exit Sub

        Me.txtRM_PlayState.Text = Player.PlayState.ToString

        CheckForMemLeak()
        UpdateKeyHookButton()

        If Player.PlayerType <> ePlayerType.DVD Then
            Player.GetBitrate()
            Player.GetDroppedFrames()
            Player.GetKeystoneFramerate(lblCurrentFrameRate.Text)
            NextCPUValue()
            Exit Sub
        End If

        If (Player.IsInitialized And Player.PlayState = ePlayState.Playing) And Not Player.CurrentDomain = DvdDomain.Stop Then
            Player.GetBitrate()
            Player.GetKeystoneFramerate(lblCurrentFrameRate.Text)
            HandleAudioStreamChanged(Nothing, Nothing)
            'HandleSubtitleStreamChanged(Nothing) - not needed as of now
            PopulateVideoStreamInfoWindow("OneSecondTick")
            lblCSSEncrypted.Text = Player.VideoEncrypted

            'DEBUGGING
            'Me.AddConsoleLine(eConsoleItemType.NOTICE, "VideoIsRunning: " & Player.VideoIsRunning)
            'Me.AddConsoleLine(eConsoleItemType.NOTICE, "DVDIsInStill: " & Player.DVDIsInStill)
            Me.lblVideoIsRunning.Text = Player.VideoIsRunning
            'Debug.WriteLine("VideoIsRunning: " & Player.VideoIsRunning)
            'Debug.WriteLine("DVDIsInStill: " & Player.DVDIsInStill)
            'Beep(1000, 1)
            'Select Case Me.cbScrap.SelectedIndex
            '    Case -1
            '        Me.cbScrap.SelectedIndex = 0
            '    Case 0
            '        If Player.DVDIsInStill Or (Not Player.VideoIsRunning) Then
            '            Player.ReSizeMixSendLastFrame()
            '        End If
            '    Case 1
            '        If Player.DVDIsInStill Then
            '            Player.ReSizeMixSendLastFrame()
            '        End If
            '    Case 2
            '        If Not Player.VideoIsRunning Then
            '            Player.ReSizeMixSendLastFrame()
            '        End If
            'End Select
            'If Not Player.VideoIsRunning Then
            '    Player.ReSizeMixSendLastFrame()
            'End If
            'DEBUGGING

        End If

        If Not Me.TenthSecondTimer.Enabled Then
            If TenthSecondTimerRestartCounter = 0 Then
                TenthSecondTimerRestartCounter = 4
                Me.TenthSecondTimer.Start()
            Else
                TenthSecondTimerRestartCounter -= 1
            End If
        Else
            TenthSecondTimerRestartCounter = 4
        End If

        UOPTemplate_ApplyCurrent()

        NextCPUValue()
        Player.LastGOPTC = Nothing
    End Sub

    Private WriteOnly Property MenusHaveFocus() As Boolean
        Set(ByVal value As Boolean)
            Try
                If value Then
                    'pause the key hook if it is not already paused
                    Me.KH.PauseHook()
                    Me.KeyHookPauseDueToMenuFocus = True
                Else
                    'unpause the key hook if it is paused because of menu focus
                    If Me.KH.IsPaused And Me.KeyHookPauseDueToMenuFocus Then
                        Me.KH.UnpauseHook()
                        Me.KeyHookPauseDueToMenuFocus = False
                    End If
                End If
            Catch ex As Exception
                Debug.WriteLine("Problem with MenusHaveFocus SET. Error: " & ex.Message)
            End Try
            Me._MenusHaveFocus = value
        End Set
    End Property
    Private _MenusHaveFocus As Boolean = False
    Private KeyHookPauseDueToMenuFocus As Boolean = False

    Private Sub TenthSecondTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TenthSecondTimer.Tick
        Try
            Dim fi As FieldInfo = GetType(DevExpress.XtraBars.BarManager).GetField("selectionInfo", BindingFlags.NonPublic Or BindingFlags.Instance)
            Dim selectionInfo As DevExpress.XtraBars.ViewInfo.BarSelectionInfo = TryCast(fi.GetValue(Me.barMain), DevExpress.XtraBars.ViewInfo.BarSelectionInfo)
            If Not selectionInfo.ActiveBarControl Is Nothing Then
                'Debug.WriteLine("bar has focus")
                MenusHaveFocus = True
            Else
                'Debug.WriteLine("bar DOES NOT have focus")
                MenusHaveFocus = False
            End If
        Catch ex As Exception
            'doesn't matter
        End Try

        Try
            If Player Is Nothing Then Exit Sub
            'If Player.PlayerMode = ePlayerMode.Stream Or Player.CurrentDomain = DvdDomain.Stop Then Exit Sub
            'If Player.IsInitialized Then
            '    Me.UpdateUserOperations(False)
            '    GetRegisters()
            'End If
            If Player.PlayerType <> ePlayerType.DVD Then Exit Sub
            If Player.IsInitialized Then
                If Not Player.CurrentDomain = DvdDomain.Stop Then
                    Me.UpdateUserOperations(False)
                End If
                'Me.UpdateUserOperations(False)
                'If Not Player.PlayState = ePlayState.DoubleStopped Then
                '    GetRegisters()
                'End If
                GetRegisters()
            End If

        Catch ex As Exception
            Me.AddConsoleLine(eConsoleItemType.ERROR, "Problem with TenthSecondTick. Error: " & ex.Message)
        End Try
    End Sub

    Private TICKS As Long
    Private Sub TICKS_ELAPSED(ByVal Sender As String)
        Dim ELAPSED As Long = Now.Ticks - TICKS
        Debug.WriteLine(Sender & " duration: " & ELAPSED)
        Dim dSeconds As Double = ELAPSED / 10000000
        Debug.WriteLine(dSeconds & " seconds")
    End Sub

#End Region 'TIMERS

#Region "RECENT PROJECTS"

    Public Sub AddRecentProject(ByVal ProjectPath As String)
        Try
            If InStr(ProjectPath.ToLower, "\blankntsc\") Or InStr(ProjectPath.ToLower, "\blankpal\") Then Exit Sub

            ProjectPath = Path.GetDirectoryName(ProjectPath)

            If Not Microsoft.VisualBasic.Right(ProjectPath, 1) = "\" Then
                ProjectPath &= "\"
            End If

            If ProjectPath.ToLower = RecentProjects(0).ToLower Then Exit Sub 'this is already the most recent project

            Dim tRecentProjects() As String = RecentProjects

            'Check to see if the project is already in the list.
            'If it is then move all items in lower positions down one spot then place ours @ 0
            Dim AlreadyInList As Boolean = False
            Dim tProj As String

            For i As Short = 1 To CurrentUserProfile.AppOptions.RecentProjectCount - 1
                tProj = tRecentProjects(i)
                If Not tProj Is Nothing AndAlso tProj.ToLower = ProjectPath.ToLower Then
                    AlreadyInList = True

                    'It's already in the list at position i. 
                    'Move others down to make a space at 0

                    For ix As Short = i - 1 To 0 Step -1
                        tRecentProjects(ix + 1) = tRecentProjects(ix)
                    Next
                    Exit For
                End If
            Next
            If AlreadyInList Then
                tRecentProjects(0) = ProjectPath
            Else
                'Item is not in list. Move all items down and place this one at 0
                For ix As Short = CurrentUserProfile.AppOptions.RecentProjectCount - 1 To 1 Step -1
                    tRecentProjects(ix) = tRecentProjects(ix - 1)
                Next
                tRecentProjects(0) = ProjectPath
            End If

            My.Settings.RECENT_PROJECTS = SerializeToString(tRecentProjects)
            My.Settings.Save()
            LoadRecentProjectsIntoProjectSelector()
        Catch ex As Exception
            AddConsoleLine(eConsoleItemType.ERROR, "Problem with AddRecentProject. Error: " & ex.Message)
        End Try
    End Sub

    Public ReadOnly Property RecentProjects() As String()
        Get
            Try
                Dim out() As String
                Dim RPS As String = My.Settings.RECENT_PROJECTS
                If RPS <> "" Then
                    out = DeserializeFromString(GetType(String()), RPS)
                    If out(0) Is Nothing Then
                        GoTo PrepEmptyArray
                    End If
                    If out.Length < CurrentUserProfile.AppOptions.RecentProjectCount Then
                        ReDim Preserve out(CurrentUserProfile.AppOptions.RecentProjectCount - 1)
                    End If
                Else
PrepEmptyArray:
                    ReDim out(CurrentUserProfile.AppOptions.RecentProjectCount - 1)
                    out(0) = ""
                End If
                Return out
            Catch ex As Exception
                Me.AddConsoleLine(eConsoleItemType.ERROR, "Problem with RecentProjects property. Error: " & ex.Message)
            End Try
        End Get
    End Property

    Public ReadOnly Property MostRecentProject() As String
        Get
            Return RecentProjects(0)
        End Get
    End Property

    Public Sub ClearRecentProjects()
        Try
            Dim RPs(CurrentUserProfile.AppOptions.RecentProjectCount - 1) As String
            For Each RP As String In RPs
                RP = ""
            Next
            My.Settings.RECENT_PROJECTS = SerializeToString(RPs)
            My.Settings.Save()
        Catch ex As Exception
            AddConsoleLine(eConsoleItemType.ERROR, "Problem with ClearRecentProjects. Error: " & ex.Message)
        End Try
    End Sub

#End Region 'RECENT PROJECTS

#Region "LICENSE PROTECTION"

    Public ReadOnly Property LicensedVersion() As ePhoenixLicense
        Get
            If Dongle_LicensedVersion = ePhoenixLicense.UNLICENSED Then Return ePhoenixLicense.UNLICENSED
            If Dongle_LicensedVersion = ePhoenixLicense.Trial Then Return ePhoenixLicense.Trial
            If Dongle_LicensedVersion = ePhoenixLicense.Mobile Then Return ePhoenixLicense.Mobile
            If Dongle_LicensedVersion = ePhoenixLicense.Ultimate Then Return ePhoenixLicense.Ultimate
        End Get
    End Property

#Region "DONGLE"

    Private Dongle As cPhoenixDongle
    Public CurrentPermissions As sPhoenixFeaturePermissions
    Private ReadOnly Property Dongle_LicensedVersion() As ePhoenixLicense
        Get
            Return _Dongle_LicensedVersion
        End Get
    End Property
    Public _Dongle_LicensedVersion As ePhoenixLicense = ePhoenixLicense.Trial

    Public Structure sPhoenixFeaturePermissions
        Public RunUltimate As Boolean
        Public RunPro As Boolean
        Public RunMobile As Boolean
    End Structure

    Private Sub InitializeDongle()
        Dongle = New cPhoenixDongle(My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor)
        If Dongle.GetLicense() <> cSafeNetDongle.eSafeNetStatusCodes.SP_SUCCESS Then
            DongleFailure()
        Else
            InitializeFeaturePermissions()
            DongleCheck()
        End If
    End Sub

    Private Sub InitializeFeaturePermissions()
        Try
            Me.CurrentPermissions = New sPhoenixFeaturePermissions

            Me.CurrentPermissions.RunMobile = Dongle.PhoenixFeatureQuery(ePhoenixLicense.Mobile) = cSafeNetDongle.eSafeNetStatusCodes.SP_SUCCESS
            Me.CurrentPermissions.RunPro = Dongle.PhoenixFeatureQuery(ePhoenixLicense.Pro) = cSafeNetDongle.eSafeNetStatusCodes.SP_SUCCESS
            Me.CurrentPermissions.RunUltimate = Dongle.PhoenixFeatureQuery(ePhoenixLicense.Ultimate) = cSafeNetDongle.eSafeNetStatusCodes.SP_SUCCESS

            If Me.CurrentPermissions.RunMobile Then
                _Dongle_LicensedVersion = ePhoenixLicense.Mobile
            End If
            If Me.CurrentPermissions.RunPro Then
                _Dongle_LicensedVersion = ePhoenixLicense.Pro
            End If
            If Me.CurrentPermissions.RunUltimate Then
                _Dongle_LicensedVersion = ePhoenixLicense.Ultimate
            End If

        Catch ex As Exception
            XtraMessageBox.Show(Me.LookAndFeel, Me, "Problem with InitializeFeaturePermissions(). This is a fatal error. The application will close.", My.Settings.APPLICATION_NAME, MessageBoxButtons.OK, MessageBoxIcon.Stop)
            Process.GetCurrentProcess.Kill()
        End Try
    End Sub

    Private Sub DongleCheck()
        If Dongle_LicensedVersion = ePhoenixLicense.Trial Then
            If Not Player Is Nothing Then Player.SetKeystoneTrialMode(True)
        Else
            If Dongle.PhoenixFeatureQuery(_Dongle_LicensedVersion) <> cSafeNetDongle.eSafeNetStatusCodes.SP_SUCCESS Then DongleFailure()
        End If
        If Not Me.OneSecondTimer.Enabled Then Me.OneSecondTimer.Start()
    End Sub

    Private Sub DongleFailure()
        _Dongle_LicensedVersion = ePhoenixLicense.Trial
        'If Dongle Is Nothing Then Exit Sub
        'Me.OneSecondTimer.Stop()
        '        Me.PERIODIC_DONGLE_CHECK_FAILURE_TIMEOUT = New System.Windows.Forms.Timer
        '        Me.PERIODIC_DONGLE_CHECK_FAILURE_TIMEOUT.Interval = 10000
        '        Me.PERIODIC_DONGLE_CHECK_FAILURE_TIMEOUT.Start()

        '        If XtraMessageBox.Show(Me.LookAndFeel, Me, "Dongle authorization failed. Retry?", My.Settings.APPLICATION_NAME, MessageBoxButtons.RetryCancel, MessageBoxIcon.Stop) = MsgBoxResult.Retry Then
        '            Me.PERIODIC_DONGLE_CHECK_FAILURE_TIMEOUT.Stop()
        '            If Not Dongle.GetLicense() = cSafeNetDongle.eSafeNetStatusCodes.SP_SUCCESS Then GoTo Failure
        '            DongleCheck()
        '        Else
        'Failure:
        '            Process.GetCurrentProcess.Kill()
        '        End If
    End Sub

#End Region 'DONGLE

#Region "FEATURE MANAGEMENT"

    Public ReadOnly Property FeatureManagement() As cPhoenixFeatureManagement
        Get
            Return _FeatureManagement
        End Get
    End Property
    Private _FeatureManagement As cPhoenixFeatureManagement

    Private Sub SetupUIForFeatureManagement()
        Try
            With FeatureManagement.Features

                If Not .FE_DVD_ParentalManagement Then
                    HidePropertyGridItem_Start(pgOptions.Rows, "ParentalLevel")
                    HidePropertyGridItem_Start(pgOptions.Rows, "ParentalCountry")
                End If

                If Not .FE_GI_ActionTitleSafe Then
                    HidePropertyGridItem_Start(pgOptions.Rows, "ActionTitleGuides")
                    HidePropertyGridItem_Start(pgOptions.Rows, "ActionTitleGuideColor")
                End If

                Me.btnRM_ToggleClosedCaptions.Enabled = .FE_L21_Decode

                If Not .FE_L21_TextExtraction Then
                    HidePropertyGridItem_Start(pgOptions.Rows, "Line21")
                End If

                If Not .FE_M2M_GOPTimecodes Then
                    HidePropertyGridItem_Start(pgOptions.Rows, "SourceTimecode")
                    HidePropertyGridItem_Start(pgOptions.Rows, "Red_IFrames")
                End If

                Me.dpGPRMViewer.Enabled = .FE_OT_GPRMViewer

                If Not .FE_SP_HighContrast Then
                    HidePropertyGridItem_Start(pgOptions.Rows, "ContrastSPColors")
                End If

            End With
        Catch ex As Exception
            Me.AddConsoleLine(eConsoleItemType.ERROR, "Problem with SetupUIForFeatureManagement(). Error: " & ex.Message)
        End Try
    End Sub

    Private Sub HidePropertyGridItem_Start(ByVal Rows As DevExpress.XtraVerticalGrid.Rows.VGridRows, ByVal SearchName As String)
        For Each r As DevExpress.XtraVerticalGrid.Rows.BaseRow In Rows
            If HidePropertyGridItem(r, SearchName) Then Exit Sub
        Next
    End Sub

    Private Function HidePropertyGridItem(ByVal Row As DevExpress.XtraVerticalGrid.Rows.BaseRow, ByVal SearchName As String) As Boolean
        If Row.Properties.Caption = SearchName Then
            pgOptions.Rows.Remove(Row)
            'Dim pr As DevExpress.XtraVerticalGrid.Rows.BaseRow = Row.ParentRow
            'pr.ChildRows.Remove(Row)
            'Row.ParentRow.ChildRows.Remove(Row)
            Return True
        End If
        For Each ChildRow As DevExpress.XtraVerticalGrid.Rows.BaseRow In Row.ChildRows
            If ChildRow.Properties.FieldName = SearchName Then
                Row.ChildRows.Remove(ChildRow)
                Return True
            Else
                If ChildRow.HasChildren Then
                    If HidePropertyGridItem(ChildRow, SearchName) Then Return True
                End If
            End If
        Next
        Return False
    End Function

    Public Property PreferredAVMode() As eAVMode
        Get
            If My.Settings.PREFERRED_AV_MODE > MaxAVMode Then
                My.Settings.PREFERRED_AV_MODE = MaxAVMode
                My.Settings.Save()
            End If
            If CType(My.Settings.PREFERRED_AV_MODE, eAVMode) = eAVMode.Intensity And Not IntensityAvailable Then
                My.Settings.PREFERRED_AV_MODE = eAVMode.DesktopVMR
                My.Settings.Save()
            End If
            If CType(My.Settings.PREFERRED_AV_MODE, eAVMode) = eAVMode.Decklink And Not DecklinkAvailable Then
                My.Settings.PREFERRED_AV_MODE = eAVMode.DesktopVMR
                My.Settings.Save()
            End If
            Select Case LicensedVersion
                Case ePhoenixLicense.UNLICENSED
                    Throw New Exception("UNLICENSED")
                Case ePhoenixLicense.Mobile
                    Return eAVMode.DesktopVMR
                Case ePhoenixLicense.Pro
                    Select Case My.Settings.PREFERRED_AV_MODE
                        Case 0
                            Return eAVMode.DesktopVMR
                        Case 1, 2
                            Return eAVMode.Intensity
                    End Select
                Case ePhoenixLicense.Ultimate, ePhoenixLicense.Trial
                    Select Case My.Settings.PREFERRED_AV_MODE
                        Case 0
                            Return eAVMode.DesktopVMR
                        Case 1
                            Return eAVMode.Intensity
                        Case 2
                            Return eAVMode.Decklink
                    End Select
            End Select
        End Get
        Set(ByVal value As eAVMode)
            If value > Me.MaxAVMode Then value = Me.MaxAVMode
            Select Case LicensedVersion
                Case ePhoenixLicense.UNLICENSED
                    Throw New Exception("UNLICENSED")
                Case ePhoenixLicense.Mobile
                    My.Settings.PREFERRED_AV_MODE = 0
                Case ePhoenixLicense.Pro
                    If value <= eAVMode.Intensity Then
                        My.Settings.PREFERRED_AV_MODE = value
                    Else
                        My.Settings.PREFERRED_AV_MODE = 1
                    End If
                Case ePhoenixLicense.Ultimate, ePhoenixLicense.Trial
                    My.Settings.PREFERRED_AV_MODE = value
            End Select
            My.Settings.Save()
        End Set
    End Property

    Public ReadOnly Property MaxAVMode() As eAVMode
        Get
            Return _MaxAVMode
        End Get
    End Property
    Private _MaxAVMode As eAVMode = eAVMode.DesktopVMR

    ''' <summary>
    ''' This method MUST be protected.
    ''' </summary>
    ''' <param name="Mode"></param>
    ''' <remarks></remarks>
    Public Sub SetMaxAVMode(ByVal Mode As eAVMode)
        _MaxAVMode = Mode
    End Sub

#End Region 'FEATURE MANAGEMENT

#End Region 'LICENSE PROTECTION

#Region "UTILITY"

    Private Sub btnScrap_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnScrap1.Click
        'Player.Stop()
        'Player.Test()
        'Player.SetNULLTimestamps()
        'Me.ShowWaitAnimation()
        'Me.AddConsoleLine(eConsoleItemType.ERROR, "test")
        'SetDeveloperRegEntry()
        barMain.SaveToXml("C:\StandardUILayout.xml")
        'Beep(1000, 1)
        'Player.ResendLastSample(1)
        'Player.ReverseFieldOrder = True
    End Sub

    Private Sub btnScrap2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnScrap2.Click
        Player.GoToMenu(DvdMenuID.Root, Not CurrentUserProfile.AppOptions.UseSeparateResumeButton)

        'Player.Test2()
        'Dim mb As DevExpress.XtraEditors.XtraMessageBox
        'mb.AllowCustomLookAndFeel = True
        'XtraMessageBox.Show(Me.LookAndFeel, Me, "test text", "test caption", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
        'Player.Mute()
        'Try
        '    barMain.RestoreFromXml("C:\StandardUILayout.xml")
        'Catch ex As Exception
        '    Debug.WriteLine(ex.Message)
        'End Try
        'Player.ReverseFieldOrder = False
    End Sub

#End Region 'UTILITY

End Class
