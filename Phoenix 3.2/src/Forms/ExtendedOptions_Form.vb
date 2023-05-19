#Region "IMPORTS"

Imports System.IO
Imports SMT.Multimedia.Formats.DVD.IFO
Imports System.Xml.Serialization
Imports System.Drawing.Printing
Imports Microsoft.Win32
Imports SMT.DotNet.Utility
Imports SMT.Applications.Phoenix.Phoenix_Form
Imports SMT.Applications.Phoenix.Classes
Imports SMT.Multimedia.Utility
Imports SMT.Multimedia.Filters.nVidia
Imports System.Runtime.InteropServices
Imports SMT.Multimedia.DirectShow
Imports SMT.Multimedia.Formats.DVD.Globalization
Imports SMT.Multimedia.Players.DVD.Enums
Imports SMT.Multimedia.Players.DVD.Classes
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Controls
Imports SMT.Win.Printing
Imports SMT.Applications.Phoenix.Enums
Imports SMT.Multimedia.Enums
Imports SMT.DotNet.Reflection
Imports SMT.DotNet.AppConsole
Imports SMT.Multimedia.Classes
Imports SMT.IPProtection.SafeNet.Phoenix

#End Region 'IMPORTS

Public Class ExtendedOptions_Form
    Inherits DevExpress.XtraEditors.XtraForm

#Region "PROPERTIES"

    Private PP As Phoenix_Form
    Private HR As Integer

#End Region 'PROPERTIES

#Region "CONSTRUCTOR"

    Public Sub New(ByRef nPP As Phoenix_Form)
        MyBase.New()

        PP = nPP

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
    End Sub

#End Region 'CONSTRUCTOR

#Region "SHARED EVENTS"

    Private Sub tcMain_SelectedPageChanged(ByVal sender As Object, ByVal e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles tcMain.SelectedPageChanged
        PP.KH.UnpauseHook()
        Select Case Me.tcMain.SelectedTabPage.Text
            Case "Admin"
                If InputBox("Please enter admin password.") = "123" Then
                    Me.tcMain.SelectedTabPage = e.Page
                Else
                    Me.tcMain.SelectedTabPageIndex = 0
                End If
            Case "Key Mapping"
                PP.KH.PauseHook()
        End Select
    End Sub

#End Region 'SHARED EVENTS

#Region "AV OUTPUT"

    Private AVSettingsLoading As Boolean = True

    Private Sub Form_Loaded_AVOutput(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load

        Me.tpOutputConfig.PageEnabled = PP.LicensedVersion >= ePhoenixLicense.Pro

        ' DEVICE
        Me.rbDecklink.Enabled = PP.DecklinkAvailable And PP.LicensedVersion >= ePhoenixLicense.Ultimate
        Me.rbIntensity.Enabled = PP.IntensityAvailable And PP.LicensedVersion >= ePhoenixLicense.Pro

        Me.gbHDMIResolution.Enabled = False
        Me.gbHDMIScaling.Enabled = False

        Select Case PP.PreferredAVMode
            Case eAVMode.DesktopVMR
                Me.rbDesktop.Checked = True
            Case eAVMode.Decklink
                Me.rbDecklink.Checked = True
            Case eAVMode.Intensity
                Me.rbIntensity.Checked = True
                Me.gbHDMIResolution.Enabled = True
                Me.gbHDMIScaling.Enabled = True
        End Select

        'HDMI SETUP
        Select Case PP.CurrentUserProfile.AppOptions.IntensityVideoScalingMode
            Case eScalingMode.Native
                rbHDMIVidScl_Native.Checked = True
                Me.ceHDMIVidScl_Maximize.Checked = False
            Case eScalingMode.Native_ScaleToAR
                rbHDMIVidScl_AdjustToAspect.Checked = True
                Me.ceHDMIVidScl_Maximize.Checked = False
            Case eScalingMode.Maximize_NativeAR
                rbHDMIVidScl_Native.Checked = True
                ceHDMIVidScl_Maximize.Checked = True
            Case eScalingMode.Maximize_ScaleToAR
                rbHDMIVidScl_AdjustToAspect.Checked = True
                ceHDMIVidScl_Maximize.Checked = True
        End Select
        Select Case PP.CurrentUserProfile.AppOptions.IntensityVideoResolution
            Case eVideoResolution._1920x1080
                rbIntensityResolution_1080.Checked = True
            Case eVideoResolution._1280x720
                rbIntensityResolution_720.Checked = True
            Case eVideoResolution._720x576
                rbIntensityResolution_576.Checked = True
            Case eVideoResolution._720x486, eVideoResolution._720x480
                rbIntensityResolution_486.Checked = True
        End Select

        'ANALOG CONFIG
        Select Case PP.PreferredAVMode
            Case eAVMode.Decklink, eAVMode.Intensity
                If PP.Player IsNot Nothing Then
                    Me.gbAV_AnalogVideoConfig.Enabled = PP.Player.Graph.DecklinkSupportsAnalogVideo
                End If
                Me.cbAV_AnalogFormat.SelectedIndex = PP.CurrentUserProfile.AppOptions.AnalogVideoOutputFormat
            Case eAVMode.DesktopVMR
                Me.gbAV_AnalogVideoConfig.Enabled = False
        End Select

        'AUDIO FORMAT
        Me.gbHDMIAudio.Enabled = (PP.PreferredAVMode = eAVMode.Intensity)
        If PP.Player IsNot Nothing Then
            If PP.Player.Graph.nVidiaAudio_IsSetToMobile Then
                Me.cbHDMIAudioFormat.SelectedIndex = 1
            Else
                Me.cbHDMIAudioFormat.SelectedIndex = 0
            End If
        End If

        AVSettingsLoading = False
    End Sub

#Region "AV OUTPUT:DEVICE"

    Private Sub rbDesktop_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbDesktop.CheckedChanged
        If AVSettingsLoading Then Exit Sub
        If rbDesktop.Checked Then
            PP.PreferredAVMode = eAVMode.DesktopVMR
            Me.gbHDMIResolution.Enabled = False
            Me.gbHDMIScaling.Enabled = False
            Me.gbHDMIAudio.Enabled = False
        End If
        Me.btnApplyDeviceChanges.Enabled = True
    End Sub

    Private Sub rbIntensity_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbIntensity.CheckedChanged
        If AVSettingsLoading Then Exit Sub
        If Me.rbIntensity.Checked Then
            PP.PreferredAVMode = eAVMode.Intensity
            Me.gbHDMIResolution.Enabled = True
            Me.gbHDMIScaling.Enabled = True
            Me.gbHDMIAudio.Enabled = True
        End If
        Me.btnApplyDeviceChanges.Enabled = True
    End Sub

    Private Sub rbDecklink_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbDecklink.CheckedChanged
        If AVSettingsLoading Then Exit Sub
        If rbDecklink.Checked Then
            PP.PreferredAVMode = eAVMode.Decklink
            Me.gbHDMIResolution.Enabled = False
            Me.gbHDMIScaling.Enabled = False
            Me.gbHDMIAudio.Enabled = False
        End If
        Me.btnApplyDeviceChanges.Enabled = True
    End Sub

    Private Sub rbIntensityResolution_1080_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbIntensityResolution_1080.CheckedChanged
        If AVSettingsLoading Then Exit Sub
        If rbIntensityResolution_1080.Checked Then
            PP.CurrentUserProfile.AppOptions.IntensityVideoResolution = eVideoResolution._1920x1080
            Me.btnApplyDeviceChanges.Enabled = True
        End If
    End Sub

    Private Sub rbIntensityResolution_720_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbIntensityResolution_720.CheckedChanged
        If AVSettingsLoading Then Exit Sub
        If rbIntensityResolution_720.Checked Then
            PP.CurrentUserProfile.AppOptions.IntensityVideoResolution = eVideoResolution._1280x720
            Me.btnApplyDeviceChanges.Enabled = True
        End If
    End Sub

    Private Sub rbIntensityResolution_576_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbIntensityResolution_576.CheckedChanged
        If AVSettingsLoading Then Exit Sub
        If rbIntensityResolution_576.Checked Then
            PP.CurrentUserProfile.AppOptions.IntensityVideoResolution = eVideoResolution._720x576
            Me.btnApplyDeviceChanges.Enabled = True
        End If
    End Sub

    Private Sub rbIntensityResolution_486_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbIntensityResolution_486.CheckedChanged
        If AVSettingsLoading Then Exit Sub
        If rbIntensityResolution_486.Checked Then
            PP.CurrentUserProfile.AppOptions.IntensityVideoResolution = eVideoResolution._720x486
            Me.btnApplyDeviceChanges.Enabled = True
        End If
    End Sub

    Private Sub rbHDMIVidScl_Native_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbHDMIVidScl_Native.CheckedChanged
        If AVSettingsLoading Then Exit Sub
        If rbHDMIVidScl_Native.Checked Then
            If Me.ceHDMIVidScl_Maximize.Checked Then
                PP.CurrentUserProfile.AppOptions.IntensityVideoScalingMode = eScalingMode.Maximize_NativeAR
            Else
                PP.CurrentUserProfile.AppOptions.IntensityVideoScalingMode = eScalingMode.Native
            End If
        End If
        UpdateHDMIOutputConfiguration()
    End Sub

    Private Sub rbHDMIVidScl_AdjustToAspect_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbHDMIVidScl_AdjustToAspect.CheckedChanged
        If AVSettingsLoading Then Exit Sub
        If rbHDMIVidScl_AdjustToAspect.Checked Then
            If Me.ceHDMIVidScl_Maximize.Checked Then
                PP.CurrentUserProfile.AppOptions.IntensityVideoScalingMode = eScalingMode.Maximize_ScaleToAR
            Else
                PP.CurrentUserProfile.AppOptions.IntensityVideoScalingMode = eScalingMode.Native_ScaleToAR
            End If
        End If
        UpdateHDMIOutputConfiguration()
    End Sub

    Private Sub ceHDMIVidScl_Maximize_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ceHDMIVidScl_Maximize.CheckedChanged
        If AVSettingsLoading Then Exit Sub
        If ceHDMIVidScl_Maximize.Checked Then
            If rbHDMIVidScl_Native.Checked Then
                PP.CurrentUserProfile.AppOptions.IntensityVideoScalingMode = eScalingMode.Maximize_NativeAR
            Else
                PP.CurrentUserProfile.AppOptions.IntensityVideoScalingMode = eScalingMode.Maximize_ScaleToAR
            End If
        Else
            If rbHDMIVidScl_Native.Checked Then
                PP.CurrentUserProfile.AppOptions.IntensityVideoScalingMode = eScalingMode.Native
            Else
                PP.CurrentUserProfile.AppOptions.IntensityVideoScalingMode = eScalingMode.Native_ScaleToAR
            End If
        End If
        UpdateHDMIOutputConfiguration()
    End Sub

    Private Sub UpdateHDMIOutputConfiguration()
        Try
            If PP.Player Is Nothing Then Exit Sub
            If PP.Player.AVMode <> eAVMode.Intensity Then Exit Sub

            Dim x, y As Byte

            If PP.Player.CurrentAspectRatio = eAspectRatio.ar16x9 Then
                x = 16
                y = 9
            Else
                x = 4
                y = 3
            End If
            Dim b1 As Boolean = PP.Player.Graph.MCE_SetScaling(PP.CurrentUserProfile.AppOptions.IntensityVideoResolution, PP.CurrentUserProfile.AppOptions.IntensityVideoScalingMode, x, y)
            'System.Threading.Thread.Sleep(300)
            'Dim b2 As Boolean = PP.Player.Graph.MCE_SetScaling(PP.CurrentUserProfile.AppOptions.IntensityVideoResolution, PP.CurrentUserProfile.AppOptions.IntensityVideoScalingMode, x, y)
            If Not b1 Then MsgBox("Failed to set scaling.")
        Catch ex As Exception
            PP.AddConsoleLine(eConsoleItemType.ERROR, "Problem with UpdateHDMIOutputConfiguration(). Error: " & ex.Message)
        End Try
    End Sub

    Private Sub btnApplyDeviceChanges_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApplyDeviceChanges.Click
        Try
            Cursor = Cursors.WaitCursor
            If PP.Player.PlayState <> ePlayState.SystemJP Then
                If Not XtraMessageBox.Show(Me.LookAndFeel, Me, "This will restart the project. Do you wish to continue?", My.Settings.APPLICATION_NAME, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then Exit Sub
                PP.ViewerDispose()
                PP.RebootProject()
            Else
                PP.ShowSystemJacketPicture()
            End If
            Me.btnApplyDeviceChanges.Enabled = False
            Cursor = Cursors.Default
        Catch ex As Exception
            PP.AddConsoleLine(eConsoleItemType.ERROR, "Problem with RestartPlayerForResolutionChange(). Error: " & ex.Message)
        End Try
    End Sub

#End Region 'AV OUTPUT:DEVICE

#Region "AV OUTPUT:ANALOG FORMAT"

    Private Sub cbAV_AnalogFormat_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbAV_AnalogFormat.SelectedIndexChanged
        PP.CurrentUserProfile.AppOptions.AnalogVideoOutputFormat = Me.cbAV_AnalogFormat.SelectedIndex
    End Sub

#End Region 'AV OUTPUT:ANALOG FORMAT

#Region "AV OUTPUT:AUDIO FORMAT"

    Private Sub cbHDMIAudioFormat_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbHDMIAudioFormat.SelectedIndexChanged
        Try
            If AVSettingsLoading Then Exit Sub
            If PP.Player Is Nothing Then Exit Sub
            PP.Player.Graph.InitializeAudioDecoder(cbHDMIAudioFormat.SelectedItem.ToString.ToLower = "uncompressed")
        Catch ex As Exception
            PP.AddConsoleLine(eConsoleItemType.ERROR, "Problem with change hdmi audio format. Error: " & ex.Message)
        End Try
    End Sub

#End Region 'AV OUTPUT:AUDIO FORMAT

#End Region 'AV OUTPUT

#Region "DUMPING"

    Private Sub Form_Load_Dumping(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.txtDumpDirectory.Text = PP.Player.DumpDirectory
        Me.cbDumpAudio.Enabled = PP.FeatureManagement.Features.FE_AD_Dumping
    End Sub

    Private Sub btnDumpDirectoryBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDumpDirectoryBrowse.Click
        Try
            PP.KH.PauseHook()
            Dim dlg As New FolderBrowserDialog
            dlg.RootFolder = Environment.SpecialFolder.Desktop
            dlg.ShowNewFolderButton = True
            dlg.Description = "Frame Grab Folder"
            dlg.SelectedPath = PP.Player.FrameGrabDumpDir
            If dlg.ShowDialog = DialogResult.OK Then
                Dim s As String = dlg.SelectedPath & "\"
                s = Microsoft.VisualBasic.Replace(s.ToLower, "framegrabs\", "")
                PP.CurrentUserProfile.AppOptions.DumpLocation = s
                Me.txtDumpDirectory.Text = s
            End If
            PP.KH.UnpauseHook()
        Catch ex As Exception
            PP.AddConsoleLine(eConsoleItemType.ERROR, "Problem browsing for frame grab target folder. Error: " & ex.Message, Nothing)
        End Try
    End Sub

    Private Sub btnOpenDumpDirectory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpenDumpDirectory.Click
        Dim t As String = Path.GetDirectoryName(Me.txtDumpDirectory.Text)
        If Not Directory.Exists(t) Then Exit Sub
        Process.Start("C:\WINDOWS\explorer.exe", "/n,/e," & t)
    End Sub

#End Region 'DUMPING

#Region "GUIDES"

    Private Sub Form_Load_Guides(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.tpGuides.PageEnabled = PP.FeatureManagement.Features.FE_GI_Dynamic
        If Not PP.Player.GuideInfo Is Nothing Then
            Me.txtGuide_T.Text = PP.Player.GuideInfo.T
            Me.txtGuide_B.Text = PP.Player.GuideInfo.B
            Me.txtGuide_R.Text = PP.Player.GuideInfo.R
            Me.txtGuide_L.Text = PP.Player.GuideInfo.L
            Me.ceGuideColor.Color = PP.Player.GuideInfo.GuideColor
            Me.cbEnableFlexGuides.Checked = PP.Player.GuideInfo.GuidesEnabled
        End If
        LoadUpGuidePresets()
    End Sub

    Private Sub Form_Close_Guides(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Dim GI As New cGuidePlacementInfo
        GI.B = Me.txtGuide_B.Text
        GI.L = Me.txtGuide_L.Text
        GI.R = Me.txtGuide_R.Text
        GI.T = Me.txtGuide_T.Text
        GI.GuideColor = Me.ceGuideColor.Color
        GI.GuidesEnabled = cbEnableFlexGuides.Checked
        PP.Player.GuideInfo = GI
    End Sub

#Region "GUIDES *FUNCTIONALITY*"

    Private Sub SetGuides()
        Try
            If cbEnableFlexGuides.Checked And PP.FeatureManagement.Features.FE_GI_Dynamic Then
                PP.Player.SetFlexGuides(Me.txtGuide_L.Text, Me.txtGuide_T.Text, Me.txtGuide_R.Text, Me.txtGuide_B.Text, mNTCSColor.ConformToNTSCColor(Me.ceGuideColor.Color))
                Me.lblGuideBox_H.Text = Math.Abs(Me.txtGuide_T.Text - Me.txtGuide_B.Text)
                Me.lblGuideBox_W.Text = Math.Abs(Me.txtGuide_R.Text - Me.txtGuide_L.Text)
                Me.lblAspect.Text = Math.Round(Me.lblGuideBox_W.Text / Me.lblGuideBox_H.Text, 2)
            Else
                PP.Player.ClearFlexGuides()
            End If
        Catch ex As Exception
            PP.AddConsoleLine(eConsoleItemType.ERROR, "Problem with xfSubpicture-SetGuides. Error: " * ex.Message)
        End Try
    End Sub

#End Region 'GUIDES *FUNCTIONALITY*

#Region "GUIDES:EVENTS"

    Private Sub cbDisplayGuides_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbEnableFlexGuides.CheckedChanged
        SetGuides()
    End Sub

    Private Sub tbGuide_T_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbGuide_T.Scroll
        Me.txtGuide_T.Text = tbGuide_T.Value
    End Sub

    Private Sub tbGuide_B_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbGuide_B.Scroll
        Me.txtGuide_B.Text = tbGuide_B.Value
    End Sub

    Private Sub tbGuide_L_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbGuide_L.Scroll
        Me.txtGuide_L.Text = tbGuide_L.Value
    End Sub

    Private Sub tbGuide_R_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbGuide_R.Scroll
        Me.txtGuide_R.Text = tbGuide_R.Value
    End Sub

    Private Sub txtGuide_T_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtGuide_T.TextChanged
        If PP Is Nothing Then Exit Sub
        If Not IsNumeric(Me.txtGuide_T.Text) Or Not IsNumeric(Me.txtGuide_B.Text) Then Exit Sub
        If CShort(Me.txtGuide_T.Text) > CShort(Me.txtGuide_B.Text) Then
            Me.txtGuide_B.Text = Me.txtGuide_T.Text
        End If
        If Me.txtGuide_T.Text > 576 Or Me.txtGuide_T.Text < 0 Then Me.txtGuide_T.Text = 0
        Me.tbGuide_T.Value = Me.txtGuide_T.Text
        SetGuides()
    End Sub

    Private Sub txtGuide_B_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtGuide_B.TextChanged
        If PP Is Nothing Then Exit Sub
        If Not IsNumeric(Me.txtGuide_B.Text) Or Not IsNumeric(Me.txtGuide_T.Text) Then Exit Sub
        If CShort(Me.txtGuide_T.Text) > CShort(Me.txtGuide_B.Text) Then
            Me.txtGuide_T.Text = Me.txtGuide_B.Text
        End If
        If Me.txtGuide_B.Text > 576 Or Me.txtGuide_B.Text < 0 Then Me.txtGuide_B.Text = 0
        Me.tbGuide_B.Value = Me.txtGuide_B.Text
        SetGuides()
    End Sub

    Private Sub txtGuide_L_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtGuide_L.TextChanged
        If PP Is Nothing Then Exit Sub
        If Not IsNumeric(Me.txtGuide_L.Text) Or Not IsNumeric(Me.txtGuide_R.Text) Then Exit Sub
        If CShort(Me.txtGuide_R.Text) < CShort(Me.txtGuide_L.Text) Then
            Me.txtGuide_R.Text = Me.txtGuide_L.Text
        End If
        If Me.txtGuide_L.Text > 720 Or Me.txtGuide_L.Text < 0 Then Me.txtGuide_L.Text = 0
        Me.tbGuide_L.Value = Me.txtGuide_L.Text
        SetGuides()
    End Sub

    Private Sub txtGuide_R_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtGuide_R.TextChanged
        If PP Is Nothing Then Exit Sub
        If Not IsNumeric(Me.txtGuide_R.Text) Or Not IsNumeric(Me.txtGuide_L.Text) Then Exit Sub
        If CShort(Me.txtGuide_L.Text) > CShort(Me.txtGuide_R.Text) Then
            Me.txtGuide_L.Text = Me.txtGuide_R.Text
        End If
        If Me.txtGuide_R.Text > 720 Or Me.txtGuide_R.Text < 0 Then Me.txtGuide_R.Text = 0
        Me.tbGuide_R.Value = Me.txtGuide_R.Text
        SetGuides()
    End Sub

    Private Sub cbARs_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbARs.SelectedIndexChanged
        'Dim NTSC As Boolean = (PP.CurrentVidStd = Media.DVD.IFOProcessing.eVideoStandard.NTSC)
        Dim H As Short = PP.Player.CurrentVideoDimensions.Y
        Dim Hn As Short

        Select Case Me.cbARs.SelectedIndex
            Case 0 'none
                Hn = H
            Case 1 '4x3(1.33)
                Hn = 541
            Case 2 '14x9(1.55)
                Hn = 464
            Case 3 '16x9(1.77)
                Hn = 405
            Case 4 '1.85
                Hn = 389
            Case 5  '2.35
                Hn = 306
            Case 6  '2.40
                Hn = 300
        End Select

        Dim Hs As Short = H - Hn
        Dim Hs2 As Short = Hs / 2
        Me.txtGuide_T.Text = Hs2
        Me.txtGuide_B.Text = Hs2 + Hn
        Me.txtGuide_L.Text = 0
        Me.txtGuide_R.Text = 720
        cbEnableFlexGuides.Checked = True
    End Sub

    Private Sub ceGuideColor_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ceGuideColor.EditValueChanged
        SetGuides()
    End Sub

#End Region 'GUIDES:EVENTS

#Region "GUIDES:MOVE ALL"

    Private Sub btnGDS_MoveAll_Up_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGDS_MoveAll_Up.Click
        Me.txtGuide_B.Text -= 1
        Me.txtGuide_T.Text -= 1
    End Sub

    Private Sub btnGDS_MoveAll_Down_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGDS_MoveAll_Down.Click
        Me.txtGuide_B.Text += 1
        Me.txtGuide_T.Text += 1
    End Sub

    Private Sub btnGDS_MoveAll_Left_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGDS_MoveAll_Left.Click
        Me.txtGuide_L.Text -= 1
        Me.txtGuide_R.Text -= 1
    End Sub

    Private Sub btnGDS_MoveAll_Right_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGDS_MoveAll_Right.Click
        Me.txtGuide_L.Text += 1
        Me.txtGuide_R.Text += 1
    End Sub

#End Region 'GUIDES:MOVE ALL

#Region "GUIDES:PRESETS"

    Public Function SetRegGuidePreset(ByVal PresetName As String, ByVal Guide As cGuides) As Boolean
        Try
            Dim rKey As RegistryKey = Registry.LocalMachine.OpenSubKey("Software\Sequoyan\Phoenix\GuidePresets\" & PresetName, True)
            If rKey Is Nothing Then
                rKey = Registry.LocalMachine.CreateSubKey("Software\Sequoyan\Phoenix\GuidePresets\" & PresetName)
            End If
            rKey.SetValue("Top", Guide.Top)
            rKey.SetValue("Bottom", Guide.Bottom)
            rKey.SetValue("Left", Guide.Left)
            rKey.SetValue("Right", Guide.Right)
            rKey.Close()
            Return True

        Catch ex As Exception
            PP.AddConsoleLine(eConsoleItemType.ERROR, "Problem with SetRegOption. Error: " & ex.Message)
            Return False
        End Try
    End Function

    Public Function GetRegGuidePreset(ByVal PresetName As String) As cGuides
        Try
TryGetRegUserOptionAgain:
            Dim rKey As RegistryKey = Registry.LocalMachine.OpenSubKey("Software\Sequoyan\Phoenix\GuidePresets\" & PresetName, True)
            If rKey Is Nothing Then
                'Me.SetRegEmuConfig(ValName, DefaultValue)
                'GoTo TryGetRegUserOptionAgain
                Return Nothing
            End If
            Dim out As New cGuides
            out.Top = rKey.GetValue("Top")
            out.Bottom = rKey.GetValue("Bottom")
            out.Left = rKey.GetValue("Left")
            out.Right = rKey.GetValue("Right")
            rKey.Close()
            Return out
        Catch ex As Exception
            PP.AddConsoleLine(eConsoleItemType.ERROR, "Problem with GetRegOption. Error: " & ex.Message)
            Return Nothing
        End Try
    End Function

    Private Sub SaveGuidePreset()
        Try
            Dim nm As String = InputBox("Name for preset:", "Guide Preset", "", Me.Left, Me.Top)
            If nm = "" Then
                XtraMessageBox.Show(Me.LookAndFeel, Me, "Invalid name.", My.Settings.APPLICATION_NAME, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            Dim rKey As RegistryKey = Registry.LocalMachine.OpenSubKey("Software\Sequoyan\Phoenix\GuidePresets", True)
            If rKey Is Nothing Then
                rKey = Registry.LocalMachine.CreateSubKey("Software\Sequoyan\Phoenix\GuidePresets")
                rKey.Close()
                Exit Sub
            End If
            Dim ss() As String = rKey.GetSubKeyNames()
            rKey.Close()
            For Each s As String In ss
                If s = nm Then
                    If XtraMessageBox.Show(Me.LookAndFeel, Me, "Preset " & nm & " already exists. Do you want to overwrite it?", My.Settings.APPLICATION_NAME, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = MsgBoxResult.No Then
                        Exit Sub
                    End If
                End If
            Next

            Dim Coll As ComboBoxItemCollection = cbGDS_SavedGuides.Properties.Items
            Coll.BeginUpdate()
            Try
                Coll.Add(nm)
            Finally
                Coll.EndUpdate()
            End Try

            Dim G As New cGuides
            G.Top = Me.txtGuide_T.Text
            G.Bottom = Me.txtGuide_B.Text
            G.Left = Me.txtGuide_L.Text
            G.Right = Me.txtGuide_R.Text
            SetRegGuidePreset(nm, G)
            LoadUpGuidePresets()
        Catch ex As Exception
            PP.AddConsoleLine(eConsoleItemType.ERROR, "Problem with DeleteGuidePreset. Error: " & ex.Message, Nothing)
        End Try
    End Sub

    Private Sub DeleteGuidePreset()
        Try
            If Me.cbGDS_SavedGuides.SelectedItem Is Nothing Then Exit Sub
            Dim rKey As RegistryKey = Registry.LocalMachine.OpenSubKey("Software\Sequoyan\Phoenix\GuidePresets", True)
            If rKey Is Nothing Then
                rKey = Registry.LocalMachine.CreateSubKey("Software\Sequoyan\Phoenix\GuidePresets")
                rKey.Close()
                Exit Sub
            End If
            rKey.DeleteSubKey(Me.cbGDS_SavedGuides.SelectedItem.ToString)
            Me.cbGDS_SavedGuides.Text = ""
            LoadUpGuidePresets()
        Catch ex As Exception
            PP.AddConsoleLine(eConsoleItemType.ERROR, "Problem with DeleteGuidePreset. Error: " & ex.Message, Nothing)
        End Try
    End Sub

    Public Sub LoadUpGuidePresets()
        Try
            Me.cbGDS_SavedGuides.Properties.Items.Clear()
            Dim rKey As RegistryKey = Registry.LocalMachine.OpenSubKey("Software\Sequoyan\Phoenix\GuidePresets", True)
            If rKey Is Nothing Then
                rKey = Registry.LocalMachine.CreateSubKey("Software\Sequoyan\Phoenix\GuidePresets")
                rKey.Close()
                Exit Sub
            End If
            Dim ss() As String = rKey.GetSubKeyNames()
            Dim Coll As ComboBoxItemCollection = cbGDS_SavedGuides.Properties.Items
            Coll.BeginUpdate()
            Try
                For Each s As String In ss
                    Coll.Add(s)
                Next
            Finally
                Coll.EndUpdate()
            End Try
        Catch ex As Exception
            PP.AddConsoleLine(eConsoleItemType.ERROR, "Problem with LoadUpGuidePresets. Error: " & ex.Message, Nothing)
        End Try
    End Sub

    Sub OpenGuidePreset(ByVal Name As String)
        Try
            Dim G As cGuides = Me.GetRegGuidePreset(Name)
            Me.txtGuide_B.Text = G.Bottom
            Me.txtGuide_L.Text = G.Left
            Me.txtGuide_R.Text = G.Right
            Me.txtGuide_T.Text = G.Top
        Catch ex As Exception
            PP.AddConsoleLine(eConsoleItemType.ERROR, "Problem with OpenGuidePreset. Error: " & ex.Message, Nothing)
        End Try
    End Sub

    Private Sub btnGDS_DeleteSelectedGuides_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGDS_DeleteSelectedGuides.Click
        Me.DeleteGuidePreset()
    End Sub

    Private Sub btnGDS_LoadSelectedGuides_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGDS_LoadSelectedGuides.Click
        If Me.cbGDS_SavedGuides.SelectedItem Is Nothing Then Exit Sub
        Me.OpenGuidePreset(cbGDS_SavedGuides.SelectedItem.ToString)
    End Sub

    Private Sub btnGDS_SaveCurrentGuides_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGDS_SaveCurrentGuides.Click
        Me.SaveGuidePreset()
    End Sub

#End Region 'GUIDES:PRESETS

#Region "GUIDES:KEY PRESS HANDLING"

    Private Sub UpDownKeys(ByVal Sender As TextBox, ByVal Up As Boolean)
        Try
            If Up Then
                Sender.Text += 1
            Else
                Sender.Text -= 1
            End If
        Catch ex As Exception
            PP.AddConsoleLine(eConsoleItemType.ERROR, "Problem with UpDownKeys in Extended Options window. Error: " & ex.Message, Nothing)
        End Try
    End Sub

    Private Sub txtGuide_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles txtGuide_B.KeyDown, txtGuide_L.KeyDown, txtGuide_R.KeyDown, txtGuide_T.KeyDown
        Try
            Dim Up As Boolean
            If e.KeyData = Keys.Up Or e.KeyData = Keys.Right Then
                Up = True
            ElseIf e.KeyData = Keys.Down Or e.KeyData = Keys.Left Then
                Up = False
            Else
                Exit Sub
            End If
            Debug.WriteLine("txtB keyup: " & e.KeyData.ToString)
            Dim tb As TextBox = CType(sender, TextBox)
            Me.UpDownKeys(tb, Up)
        Catch ex As Exception
            'do nothing
        End Try
    End Sub

#End Region 'GUIDES:KEY PRESS HANDLING

#End Region 'GUIDES

#Region "FRAME GRAB VIEWER"

    Public FrameGrabPaths() As String
    Public CurrentFrameGrabPath As String
    Public CurrentFrameGrabIndex As Short = -1

    Private Sub Form_Load_FrameGrabViewer(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        UpdateMultiFGList()
        SetupFrameGrabPaths()
    End Sub

    Private Sub btnFGV_OpenTargetDir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFGV_OpenTargetDir.Click
        Dim t As String = Path.GetDirectoryName(PP.Player.FrameGrabDumpDir)
        If Not Directory.Exists(t) Then Exit Sub
        Process.Start("C:\WINDOWS\explorer.exe", "/n,/e," & t)
    End Sub

    Public Sub SetupFrameGrabPaths()
        Try
            Dim FGDir As String = PP.Player.FrameGrabDumpDir
            If Me.cbFGV_ViewMulti.Checked Then
                If Not Me.cbFGV_MultiFrameDirectories.SelectedItem Is Nothing Then
                    FGDir &= Me.cbFGV_MultiFrameDirectories.SelectedItem.ToString
                Else
                    Exit Sub
                End If
            End If
            If Not Directory.Exists(FGDir) Then
                UpdateMultiFGList()
                Exit Sub
            End If
            ReDim FrameGrabPaths(-1)
            Dim Files() As String = Directory.GetFiles(FGDir)
            Dim Ext As String
            For i As Short = 0 To UBound(Files)
                Ext = Path.GetExtension(Files(i)).ToLower
                If Ext = ".bmp" Or Ext = ".jpg" Or Ext = ".jpeg" Or Ext = ".gif" Or Ext = ".png" Or Ext = ".tif" Or Ext = ".tiff" Then
                    Me.AddFrameGrab(Files(i))
                End If
            Next
        Catch ex As Exception
            PP.AddConsoleLine(eConsoleItemType.ERROR, "Problem with SetupFrameGrabPaths. Error: " & ex.Message)
        End Try
    End Sub

    Public Sub AddFrameGrab(ByVal FGPath As String)
        Try
            ReDim Preserve FrameGrabPaths(UBound(FrameGrabPaths) + 1)
            FrameGrabPaths(UBound(FrameGrabPaths)) = FGPath
        Catch ex As Exception
            PP.AddConsoleLine(eConsoleItemType.ERROR, "Problem with AddFrameGrab in FrameGrabViewer. Error: " & ex.Message)
        End Try
    End Sub

    Public FrameGrab As cFrameGrab

    Public Class cFrameGrab
        Private BM As Bitmap
        Private FGV As ExtendedOptions_Form

        Public Sub New(ByVal nFGV As ExtendedOptions_Form, ByVal nBM As Bitmap)
            FGV = nFGV
            Me.Set(nBM)
        End Sub

        Public Sub Dispose()
            BM.Dispose()
            BM = Nothing
        End Sub

        Public Function [Get]() As Bitmap
            Return BM
        End Function

        Public Sub [Set](ByVal nBM As Bitmap)
            BM = nBM
            If Not FGV.pbFrameGrab.Image Is Nothing Then
                FGV.pbFrameGrab.Image.Dispose()
                FGV.pbFrameGrab.Image = Nothing
            End If

            FGV.pbFrameGrab.Image = nBM
        End Sub

    End Class

    Private Sub btnNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFGV_ViewNext.Click
        ViewNext()
    End Sub

    Private CurrentMultiFrameIndex As Byte = 1
    Private ReadOnly Property CurrentMultiFrameDirectory() As String
        Get
            Return PP.Player.FrameGrabDumpDir & Me.cbFGV_MultiFrameDirectories.SelectedItem.ToString & "\"
        End Get
    End Property

    Private ReadOnly Property CurrentMultiFrameExtension() As String
        Get
            Return Path.GetExtension(Me.FrameGrabPaths(0))
        End Get
    End Property

    Public Function ViewNext() As Boolean
        Try
            If FrameGrabPaths Is Nothing Then Throw New Exception("We seem to have no frame grab paths.")
            If Me.cbFGV_ViewMulti.Checked Then
TryMultiAgain:
                If CurrentMultiFrameIndex = FrameGrabPaths.Length Then
                    CurrentMultiFrameIndex = 1
                Else
                    CurrentMultiFrameIndex += 1
                End If
                Dim tPath As String = CurrentMultiFrameDirectory & CurrentMultiFrameIndex & CurrentMultiFrameExtension
                If File.Exists(tPath) Then
                    Dim index As Integer = Array.IndexOf(Me.FrameGrabPaths, tPath)
                    CurrentFrameGrabPath = FrameGrabPaths(index)
                    Me.FrameGrab = New cFrameGrab(Me, New Bitmap(CurrentFrameGrabPath))
                    lblFGV_FileName.Text = CurrentFrameGrabPath
                    Return True
                Else
                    GoTo TryMultiAgain
                End If
            Else
                CurrentFrameGrabIndex += 1
                If UBound(FrameGrabPaths) < CurrentFrameGrabIndex Then
                    CurrentFrameGrabIndex = 0
                End If
                If CurrentFrameGrabIndex > UBound(FrameGrabPaths) Then
                    XtraMessageBox.Show(Me.LookAndFeel, Me, "No frame grabs currently available.", My.Settings.APPLICATION_NAME, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Function
                End If
                CurrentFrameGrabPath = FrameGrabPaths(CurrentFrameGrabIndex)
                If Not File.Exists(CurrentFrameGrabPath) Then
                    ViewNext()
                    Exit Function
                End If
                Me.FrameGrab = New cFrameGrab(Me, New Bitmap(CurrentFrameGrabPath))
                lblFGV_FileName.Text = CurrentFrameGrabPath
                Return True
            End If
        Catch ex As Exception
            PP.AddConsoleLine(eConsoleItemType.ERROR, "Problem with ViewNext(). Error: " & ex.Message)
        End Try
    End Function

    Private Sub btnPrevious_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFGV_ViewPrevious.Click
        ViewPrevious()
    End Sub

    Public Function ViewPrevious() As Boolean
        Try
            If FrameGrabPaths Is Nothing Then Throw New Exception("We seem to have no frame grab paths.")

            If Me.cbFGV_ViewMulti.Checked Then
TryMultiAgain:
                If CurrentMultiFrameIndex = 1 Then
                    CurrentMultiFrameIndex = FrameGrabPaths.Length
                Else
                    CurrentMultiFrameIndex -= 1
                End If
                Dim tPath As String = CurrentMultiFrameDirectory & CurrentMultiFrameIndex & CurrentMultiFrameExtension
                If File.Exists(tPath) Then
                    Dim index As Integer = Array.IndexOf(Me.FrameGrabPaths, tPath)
                    CurrentFrameGrabPath = FrameGrabPaths(index)
                    Me.FrameGrab = New cFrameGrab(Me, New Bitmap(CurrentFrameGrabPath))
                    lblFGV_FileName.Text = CurrentFrameGrabPath
                    Return True
                Else
                    GoTo TryMultiAgain
                End If
            Else

                CurrentFrameGrabIndex -= 1
                If 0 > CurrentFrameGrabIndex Then
                    CurrentFrameGrabIndex = UBound(FrameGrabPaths)
                    If CurrentFrameGrabIndex = -1 Then
                        XtraMessageBox.Show(Me.LookAndFeel, Me, "No frame grabs currently available.", My.Settings.APPLICATION_NAME, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Function
                    End If
                End If
                CurrentFrameGrabPath = FrameGrabPaths(CurrentFrameGrabIndex)
                If Not File.Exists(CurrentFrameGrabPath) Then
                    ViewPrevious()
                    Exit Function
                End If
                Me.FrameGrab = New cFrameGrab(Me, New Bitmap(CurrentFrameGrabPath))
                lblFGV_FileName.Text = CurrentFrameGrabPath
                Return True
            End If
        Catch ex As Exception
            PP.AddConsoleLine(eConsoleItemType.ERROR, "Problem with ViewPrevious(). Error: " & ex.Message)
        End Try
    End Function

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFGV_DeleteCurrent.Click
        Try
            If Not File.Exists(Me.CurrentFrameGrabPath) Then Exit Sub
            Dim t As String = Me.CurrentFrameGrabPath
            Me.pbFrameGrab.Image = Nothing
            FrameGrab.Dispose()
            System.Threading.Thread.Sleep(250)
            File.Delete(t)
            SetupFrameGrabPaths()
            ViewNext()
        Catch ex As Exception
            PP.AddConsoleLine(eConsoleItemType.ERROR, "Problem deleting frame grab. Error: " & ex.Message)
        End Try
    End Sub

    Private Sub btnSaveAs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFGV_SaveCurrent.Click
        Try
            If Not File.Exists(Me.CurrentFrameGrabPath) Then Exit Sub
            Dim fsd As New SaveFileDialog
            fsd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            fsd.OverwritePrompt = True
            fsd.Title = "Save Frame Grab"
            fsd.DefaultExt = "." & Path.GetExtension(CurrentFrameGrabPath)
            fsd.FileName = Path.GetFileNameWithoutExtension(CurrentFrameGrabPath)
            fsd.Filter = "Bitmap|*.bmp|Tif|*.tif|GIF|*.gif|JPEG|*.jpg|PNG|*.png"
            PP.KH.PauseHook()
            If fsd.ShowDialog = DialogResult.OK Then
                'File.Move(CurrentFrameGrabPath, fsd.FileName)
                Dim bm As New Bitmap(CurrentFrameGrabPath)
                Select Case Path.GetExtension(fsd.FileName).ToLower
                    Case ".bmp"
                        bm.Save(fsd.FileName, System.Drawing.Imaging.ImageFormat.Bmp)
                    Case ".tif"
                        bm.Save(fsd.FileName, System.Drawing.Imaging.ImageFormat.Tiff)
                    Case ".gif"
                        bm.Save(fsd.FileName, System.Drawing.Imaging.ImageFormat.Gif)
                    Case ".jpg"
                        bm.Save(fsd.FileName, System.Drawing.Imaging.ImageFormat.Jpeg)
                    Case ".png"
                        bm.Save(fsd.FileName, System.Drawing.Imaging.ImageFormat.Png)
                End Select
                bm.Dispose()
                bm = Nothing
            End If
            fsd = Nothing
            PP.KH.UnpauseHook()
        Catch ex As Exception
            PP.AddConsoleLine(eConsoleItemType.ERROR, "Problem with SaveFrameGrab. Error: " & ex.Message)
        End Try
    End Sub

    Private Sub btnGrabFrame_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFGV_GrabFrame.Click

        PP.Player.GrabScreen(PP.CurrentUserProfile.AppOptions.FrameGrabTypeAsImageFormat, PP.CurrentUserProfile.AppOptions.FrameGrabSource, True)
        System.Threading.Thread.Sleep(250)
        ViewNewest()
    End Sub

    Private Sub ViewNewest()
        Try
            Dim di As New DirectoryInfo(PP.Player.FrameGrabDumpDir)
            Dim FIa() As FileInfo = di.GetFiles
            Dim FC As New cGetNewestFileInDir
            Array.Sort(FIa, FC)

            Me.CurrentFrameGrabPath = FIa(0).FullName
            Me.FrameGrab = New cFrameGrab(Me, New Bitmap(FIa(0).FullName))
        Catch ex As Exception
            PP.AddConsoleLine(eConsoleItemType.ERROR, "Problem with ViewNewest. Error: " & ex.Message)
        End Try
    End Sub

    Public Class cGetNewestFileInDir
        Implements IComparer
        Public Function Compare(ByVal X As Object, ByVal Y As Object) As Integer Implements IComparer.compare
            Dim File1 As FileInfo = DirectCast(X, FileInfo)
            Dim File2 As FileInfo = DirectCast(Y, FileInfo)
            Return Date.Compare(File2.CreationTime, File1.CreationTime)
        End Function
    End Class

    Public Sub UpdateMultiFGList()
        Try
            Dim Coll As ComboBoxItemCollection = Me.cbFGV_MultiFrameDirectories.Properties.Items
            Coll.BeginUpdate()
            Coll.Clear()
            Try
                Dim s() As String
                For Each d As String In Directory.GetDirectories(PP.Player.FrameGrabDumpDir)
                    s = Split(d, Path.DirectorySeparatorChar)
                    Coll.Add(s(UBound(s)))
                Next
            Finally
                Coll.EndUpdate()
            End Try
            Me.cbFGV_MultiFrameDirectories.SelectedIndex = -1
        Catch ex As Exception
            PP.AddConsoleLine(eConsoleItemType.ERROR, "Problem with UpdateMultiFGList. Error: " & ex.Message, Nothing)
        End Try
    End Sub

    Private Sub cbViewMulti_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbFGV_ViewMulti.CheckedChanged
        CurrentMultiFrameIndex = 1
        If Me.cbFGV_MultiFrameDirectories.SelectedIndex = -1 Then
            Me.cbFGV_MultiFrameDirectories.SelectedIndex = 0
        Else
            Me.SetupFrameGrabPaths()
        End If
    End Sub

    Private Sub cbMultis_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbFGV_MultiFrameDirectories.SelectedIndexChanged
        If Me.cbFGV_ViewMulti.Checked Then
            Me.SetupFrameGrabPaths()
        End If
    End Sub

#Region "Frame Grab - Aspect Ratio Context Menu"

    Private Property CurrentFrameGrabAspectRatio() As eFrameGrabAspectRatio
        Get
            Return _CurrentFrameGrabAspectRatio
        End Get
        Set(ByVal value As eFrameGrabAspectRatio)
            _CurrentFrameGrabAspectRatio = value
            Select Case value
                Case eFrameGrabAspectRatio._720480
                    Me.mi720480.Checked = True
                    Me.mi720576.Checked = False
                    Me.mi853480.Checked = False
                    Me.mi853576.Checked = False
                    Me.pbFrameGrab.Size = New System.Drawing.Size(720, 480)
                Case eFrameGrabAspectRatio._720576
                    Me.mi720480.Checked = False
                    Me.mi720576.Checked = True
                    Me.mi853480.Checked = False
                    Me.mi853576.Checked = False
                    Me.pbFrameGrab.Size = New System.Drawing.Size(720, 576)
                Case eFrameGrabAspectRatio._853480
                    Me.mi720480.Checked = False
                    Me.mi720576.Checked = False
                    Me.mi853480.Checked = True
                    Me.mi853576.Checked = False
                    Me.pbFrameGrab.Size = New System.Drawing.Size(853, 480)
                Case eFrameGrabAspectRatio._853576
                    Me.mi720480.Checked = False
                    Me.mi720576.Checked = False
                    Me.mi853480.Checked = False
                    Me.mi853576.Checked = True
                    Me.pbFrameGrab.Size = New System.Drawing.Size(853, 576)
            End Select
        End Set
    End Property
    Private _CurrentFrameGrabAspectRatio As eFrameGrabAspectRatio = eFrameGrabAspectRatio._720480

    Private Enum eFrameGrabAspectRatio
        _853480
        _853576
        _720480
        _720576
    End Enum

    Private Sub mi853480_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mi853480.Click
        Me.CurrentFrameGrabAspectRatio = eFrameGrabAspectRatio._853480
    End Sub

    Private Sub mi853576_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mi853576.Click
        Me.CurrentFrameGrabAspectRatio = eFrameGrabAspectRatio._853576
    End Sub

    Private Sub mi720480_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mi720480.Click
        Me.CurrentFrameGrabAspectRatio = eFrameGrabAspectRatio._720480
    End Sub

    Private Sub mi720576_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mi720576.Click
        Me.CurrentFrameGrabAspectRatio = eFrameGrabAspectRatio._720576
    End Sub

#End Region 'Frame Grab - Aspect Ratio Context Menu

#Region "Frame Grab - Grab Options Context Menu"

    Private Sub cmFrameGrab_Popup(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmFGV_FrameGrabOptions.Opened
        Dim FGT As String = PP.CurrentUserProfile.AppOptions.FrameGrabType.ToString
        Dim FGS As String = PP.CurrentUserProfile.AppOptions.FrameGrabSource.ToString
        Dim mi As ToolStripMenuItem
        Try
            For Each item As ToolStripItem In Me.cmFGV_FrameGrabOptions.Items
                Select Case item.Text.ToLower
                    Case "full mix"
                        If Not PP.FeatureManagement.Features.FE_VDP_FullMix Then
                            item.Visible = False
                            GoTo NextMI
                        End If
                    Case "video and subpicture"
                        If Not PP.FeatureManagement.Features.FE_VDP_FullMix Then
                            item.Visible = False
                            GoTo NextMI
                        End If
                    Case "video only"
                        If Not PP.FeatureManagement.Features.FE_VDP_VideoOnly Then
                            item.Visible = False
                            GoTo NextMI
                        End If
                    Case "subpicture only"
                        If Not PP.FeatureManagement.Features.FE_SP_Dumping Then
                            item.Visible = False
                            GoTo NextMI
                        End If
                    Case "closed caption only"
                        If Not PP.FeatureManagement.Features.FE_L21_BitmapExtraction Then
                            item.Visible = False
                            GoTo NextMI
                        End If
                    Case "multiframe"
                        If Not PP.FeatureManagement.Features.FE_VDP_MultiFrame Then
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

#Region "Frame Grab Source"

    Private Sub miFrameGrabContent_Video_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miFGV_VideoOnly.Click
        Me.miFGV_VideoOnly.Checked = True
        Me.miFGV_FullMix.Checked = False
        Me.miFGV_VideoAndSubpicture.Checked = False
        Me.miFGV_ClosedCaptionsOnly.Checked = False
        Me.miFGV_SubpictureOnly.Checked = False
        Me.miFGV_MultiFrame.Checked = False
        PP.CurrentUserProfile.AppOptions.FrameGrabSource = eFrameGrabContent.Video_Only
    End Sub

    Private Sub miFrameGrabContent_VideoAndSubs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miFGV_VideoAndSubpicture.Click
        Me.miFGV_VideoOnly.Checked = False
        Me.miFGV_FullMix.Checked = False
        Me.miFGV_VideoAndSubpicture.Checked = True
        Me.miFGV_ClosedCaptionsOnly.Checked = False
        Me.miFGV_SubpictureOnly.Checked = False
        Me.miFGV_MultiFrame.Checked = False
        PP.CurrentUserProfile.AppOptions.FrameGrabSource = eFrameGrabContent.Video_and_Subpicture
    End Sub

    Private Sub miFrameGrabContent_FullMix_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miFGV_FullMix.Click
        Me.miFGV_VideoOnly.Checked = False
        Me.miFGV_FullMix.Checked = True
        Me.miFGV_VideoAndSubpicture.Checked = False
        Me.miFGV_ClosedCaptionsOnly.Checked = False
        Me.miFGV_SubpictureOnly.Checked = False
        Me.miFGV_MultiFrame.Checked = False
        PP.CurrentUserProfile.AppOptions.FrameGrabSource = eFrameGrabContent.Full_Mix
    End Sub

    Private Sub miFrameGrabContent_SubOnly_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miFGV_SubpictureOnly.Click
        Me.miFGV_VideoOnly.Checked = False
        Me.miFGV_FullMix.Checked = False
        Me.miFGV_VideoAndSubpicture.Checked = False
        Me.miFGV_ClosedCaptionsOnly.Checked = False
        Me.miFGV_SubpictureOnly.Checked = True
        Me.miFGV_MultiFrame.Checked = False
        PP.CurrentUserProfile.AppOptions.FrameGrabSource = eFrameGrabContent.Subpicture_Only
    End Sub

    Private Sub miFrameGrabContent_L21Only_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miFGV_ClosedCaptionsOnly.Click
        Me.miFGV_VideoOnly.Checked = False
        Me.miFGV_FullMix.Checked = False
        Me.miFGV_VideoAndSubpicture.Checked = False
        Me.miFGV_ClosedCaptionsOnly.Checked = True
        Me.miFGV_SubpictureOnly.Checked = False
        Me.miFGV_MultiFrame.Checked = False
        PP.CurrentUserProfile.AppOptions.FrameGrabSource = eFrameGrabContent.Closed_Caption_Only
    End Sub

    Private Sub miMultiFrameGrab_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miFGV_MultiFrame.Click
        Me.miFGV_VideoOnly.Checked = False
        Me.miFGV_FullMix.Checked = False
        Me.miFGV_VideoAndSubpicture.Checked = False
        Me.miFGV_ClosedCaptionsOnly.Checked = False
        Me.miFGV_SubpictureOnly.Checked = False
        Me.miFGV_MultiFrame.Checked = True
        PP.CurrentUserProfile.AppOptions.FrameGrabSource = eFrameGrabContent.MultiFrame
    End Sub

#End Region 'Frame Grab Source

#Region "Frame Grab Type"

    Private Sub miFGType_BM_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miFGV_Bitmap.Click
        UncheckAllImageTypes()
        PP.CurrentUserProfile.AppOptions.FrameGrabType = eFrameGrabType.BMP
        Me.CheckImageType("Bitmap")
    End Sub

    Private Sub miFGType_Jpeg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miFGV_JPEG.Click
        UncheckAllImageTypes()
        PP.CurrentUserProfile.AppOptions.FrameGrabType = eFrameGrabType.JPEG
        Me.CheckImageType("Jpeg")
    End Sub

    Private Sub miFGType_Gif_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miFGV_GIF.Click
        UncheckAllImageTypes()
        PP.CurrentUserProfile.AppOptions.FrameGrabType = eFrameGrabType.GIF
        Me.CheckImageType("Gif")
    End Sub

    Private Sub miFGType_Png_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miFGV_PNG.Click
        UncheckAllImageTypes()
        PP.CurrentUserProfile.AppOptions.FrameGrabType = eFrameGrabType.PNG
        Me.CheckImageType("Png")
    End Sub

    Private Sub miFGType_Tif_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miFGV_TIF.Click
        UncheckAllImageTypes()
        PP.CurrentUserProfile.AppOptions.FrameGrabType = eFrameGrabType.TIF
        Me.CheckImageType("Tif")
    End Sub

    Public Sub CheckImageType(ByVal ImageType As String)
        For Each mi As ToolStripMenuItem In Me.cmFGV_FrameGrabOptions.Items
            If mi.Text = ImageType Then
                mi.Checked = True
                Exit Sub
            End If
        Next
    End Sub

    Public Sub UncheckAllImageTypes()
        For Each mi As ToolStripMenuItem In Me.cmFGV_FrameGrabOptions.Items
            If mi.Text = "Bitmap" Or mi.Text = "Jpeg" Or mi.Text = "Gif" Or mi.Text = "Tif" Or mi.Text = "Png" Then
                mi.Checked = False
            End If
        Next
    End Sub

#End Region 'Frame Grab Type

#End Region 'Frame Grab - Grab Options Context Menu

#End Region 'FRAME GRAB VIEWER

#Region "KEY MAPPING"

    Private Sub Form_Load_KeyMapping(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetMeUp()
        PP.KH.PauseHook()
    End Sub

    Private Sub ExtendedOptions_Form_KeyUp(ByVal sender As Object, ByVal e As KeyEventArgs) Handles Me.KeyUp
        If Me.tcMain.SelectedTabPage.Text = "Key Mapping" Then
            Me.KeyMapping_HandleKeystrike(Me, e)
        End If
    End Sub

    Private Sub dgKM_KeyMapping_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles dgKM_KeyMapping.MouseDown
        Dim hti As DataGrid.HitTestInfo
        hti = Me.dgKM_KeyMapping.HitTest(New Point(e.X, e.Y))
        Select Case hti.Type
            Case System.Windows.Forms.DataGrid.HitTestType.None
                Debug.WriteLine("You clicked the background.")
            Case System.Windows.Forms.DataGrid.HitTestType.Cell
                Debug.WriteLine("You clicked cell at row " & hti.Row & ", col " & hti.Column)
                If hti.Row > Me.dgKM_KeyMapping.DataSource.table.rows.count - 1 Then
                    Exit Sub
                End If
                Me.dgKM_KeyMapping.Select(hti.Row)
                Me.dgKM_KeyMapping.CurrentRowIndex = hti.Row

                'Debug.WriteLine(Me.dgKeyMapping.Controls(2).Top)

                SetSETTop(e.Y + 12)

                Me.txtCrntFunct.Text = Me.dgKM_KeyMapping.Item(Me.dgKM_KeyMapping.CurrentRowIndex, 0)
                If PP.CurrentUserProfile.KeyMapping.FindKMFunctionByNameReturnIndex(Me.txtCrntFunct.Text) = -1 Then
                    'no need to select key
                Else
                    Me.SelectKey(PP.CurrentUserProfile.KeyMapping.CurrentKeyMapping(Me.dgKM_KeyMapping.CurrentRowIndex).pfKeyEventArgs)
                End If

            Case System.Windows.Forms.DataGrid.HitTestType.ColumnHeader
                Debug.WriteLine("You clicked the column header for column " & hti.Column)
            Case System.Windows.Forms.DataGrid.HitTestType.RowHeader
                Debug.WriteLine("You clicked the row header for row " & hti.Row)
            Case System.Windows.Forms.DataGrid.HitTestType.ColumnResize
                Debug.WriteLine("You clicked the column resizer for column " & hti.Column)
            Case System.Windows.Forms.DataGrid.HitTestType.RowResize
                Debug.WriteLine("You clicked the row resizer for row " & hti.Row)
            Case System.Windows.Forms.DataGrid.HitTestType.Caption
                Debug.WriteLine("You clicked the caption")
            Case System.Windows.Forms.DataGrid.HitTestType.ParentRows
                Debug.WriteLine("You clicked the parent row")
        End Select
    End Sub

    Public Sub SetMeUp()
        Try
            Me.dgKM_KeyMapping.DataSource = Nothing
            Dim dt As New DataTable("KeyMapping")
            dt.Columns.Add("FunctionName")
            dt.Columns(0).ReadOnly = True
            dt.Columns.Add("CurrentMapping")
            dt.Columns(1).ReadOnly = True

            Dim dr As DataRow
            Dim tPF As cPhoenixFunction
            For Each s As String In PP.CurrentUserProfile.KeyMapping.KeyMappingFunctions
                dr = dt.NewRow
                dr.Item(0) = s
                tPF = PP.CurrentUserProfile.KeyMapping.FindKMFunctionByName(s)
                If tPF Is Nothing Then
                    dr.Item(1) = "None"
                Else
                    dr.Item(1) = PP.CurrentUserProfile.KeyMapping.KeyEventArgsFriendlyString(tPF.pfKeyEventArgs)
                End If
                dt.Rows.Add(dr)
            Next

            'For Each PF As cPhoenixFunction In PP.CurrentUserProfile.KeyMapping.Array
            '    dr = dt.NewRow
            '    dr.Item(0) = PF.FunctionName
            '    dr.Item(1) = KeyDataToString(PF.KeyData)
            '    dt.Rows.Add(dr)
            'Next

            Dim dv As New DataView(dt)
            Me.dgKM_KeyMapping.DataSource = dv
            Me.dgKM_KeyMapping.TableStyles(0).GridColumnStyles(0).Width = 175
            Me.dgKM_KeyMapping.TableStyles(0).GridColumnStyles(0).Alignment = HorizontalAlignment.Right
            Me.dgKM_KeyMapping.TableStyles(0).GridColumnStyles(0).ReadOnly = True
            Me.dgKM_KeyMapping.TableStyles(0).GridColumnStyles(1).Width = 100
            Me.dgKM_KeyMapping.TableStyles(0).GridColumnStyles(1).ReadOnly = True
            Me.dgKM_KeyMapping.TableStyles(0).RowHeadersVisible = False

            ''Put functions into listbox
            'Me.lbFunctions.Items.Clear()
            'For Each PF As cPhoenixFunction In PP.nCurrentKeyMapping
            '    Me.lbFunctions.Items.Add(PF)
            'Next

            'Put keys into listbox
            Me.lbKM_Keys.Items.Clear()
            Dim kc As New KeysConverter
            For Each K As Keys In PP.CurrentUserProfile.KeyMapping.nKeys
                Me.lbKM_Keys.Items.Add([Enum].Parse(GetType(Keys), K))
            Next

            'For Each s As String In [Enum].GetNames(GetType(Keys))
            '    Me.lbKM_Keys.Items.Add(s)
            'Next

            'put modifiers into listbox
            'Me.lbModifiers.Items.Clear()
            'For Each K As cKey In PP.nModifiers
            '    Me.lbModifiers.Items.Add(k)
            'Next

            'Me.lbFunctions.SelectedIndex = -1
            Me.lbKM_Keys.SelectedIndex = -1
            'Me.lbModifiers.SelectedIndex = -1
        Catch ex As Exception
            PP.AddConsoleLine(eConsoleItemType.ERROR, "Problem setting up key mapping form. Error: " & ex.Message)
        End Try
    End Sub

    Private Sub SetSETTop(ByVal v As Short)
        Me.llKM_SetKeyMap.Top = v + 5
    End Sub

    Private Sub lbKeys_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbKM_Keys.SelectedIndexChanged
        UpdateValue()
    End Sub

    Public Sub UpdateValue()
        Try
            If Me.lbKM_Keys.SelectedIndices.Count > 1 Then
                XtraMessageBox.Show(Me.LookAndFeel, Me, "Select only one key for this function.", My.Settings.APPLICATION_NAME, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Me.lbKM_Keys.SelectedIndex = -1
                Me.txtValue.Text = "0"
                Exit Sub
            End If
            If Me.lbKM_Keys.SelectedItem Is Nothing Then
                Me.txtValue.Text = "0"
            Else
                'Me.txtValue.Text = CType(Me.lbKM_Keys.SelectedItem, cKey).KeyData
                Dim val As Long = [Enum].Parse(GetType(Keys), lbKM_Keys.SelectedItem.ToString)
                If Me.cbKM_ControlKey.Checked Then
                    val = val Or Keys.Control
                End If
                Me.txtValue.Text = val.ToString
            End If
            'CType(Me.lbFunctions.SelectedItem, cPhoenixFunction).KeyData = Me.txtValue.Text
        Catch ex As Exception
            PP.AddConsoleLine(eConsoleItemType.ERROR, "Problem with key mapping UpdateValue. Error: " & ex.Message)
        End Try
    End Sub

    Private Sub llSET_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llKM_SetKeyMap.LinkClicked
        Try
            'Check to see if we're overwriting a differnt KeyMap
            Dim ExistingItemKeyData As Long
            Dim NewItemKeyData As Long
            For i As Short = 0 To PP.CurrentUserProfile.KeyMapping.CurrentKeyMapping.Length - 1
                ExistingItemKeyData = PP.CurrentUserProfile.KeyMapping.CurrentKeyMapping(i).pfKeyEventArgs.KeyData
                NewItemKeyData = Me.txtValue.Text
                If ExistingItemKeyData = NewItemKeyData And PP.CurrentUserProfile.KeyMapping.CurrentKeyMapping(i).FunctionName.ToLower <> Me.txtCrntFunct.Text.ToLower Then
                    If XtraMessageBox.Show(Me.LookAndFeel, Me, "WARNING: This key mapping is currently assigned to " & PP.CurrentUserProfile.KeyMapping.CurrentKeyMapping(i).FunctionName & "." & vbNewLine & "The newly assigned mapping will be applied and " & PP.CurrentUserProfile.KeyMapping.CurrentKeyMapping(i).FunctionName & " will no longer be mapped to a key combination." & vbNewLine & "Do you wish to continue?", My.Settings.APPLICATION_NAME, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = MsgBoxResult.Yes Then
                        PP.CurrentUserProfile.KeyMapping.CurrentKeyMapping(i).pfKeyEventArgs = New KeyEventArgs(0)
                        Exit For
                    Else
                        Exit Sub
                    End If
                End If
            Next

            Dim ix As Short = PP.CurrentUserProfile.KeyMapping.FindKMFunctionByNameReturnIndex(Me.txtCrntFunct.Text.ToLower)
            If ix = -1 Then
                ReDim Preserve PP.CurrentUserProfile.KeyMapping.CurrentKeyMapping(UBound(PP.CurrentUserProfile.KeyMapping.CurrentKeyMapping) + 1)
                PP.CurrentUserProfile.KeyMapping.CurrentKeyMapping(UBound(PP.CurrentUserProfile.KeyMapping.CurrentKeyMapping)) = New cPhoenixFunction(Me.txtCrntFunct.Text, New KeyEventArgs(Me.txtValue.Text))
            Else
                If Not Me.cbKM_ControlKey.Checked Then
                    PP.CurrentUserProfile.KeyMapping.CurrentKeyMapping(ix).pfKeyEventArgs = New KeyEventArgs([Enum].Parse(GetType(Keys), Me.lbKM_Keys.SelectedItem.ToString))
                Else
                    PP.CurrentUserProfile.KeyMapping.CurrentKeyMapping(ix).pfKeyEventArgs = New KeyEventArgs([Enum].Parse(GetType(Keys), Me.lbKM_Keys.SelectedItem.ToString) Or Keys.Control)
                End If
            End If
            SetMeUp()
        Catch ex As Exception
            PP.AddConsoleLine(eConsoleItemType.ERROR, "Problem with KM SET.", ex.Message)
        End Try
    End Sub

    Public Sub SelectKey(ByVal k As KeyEventArgs)
        Try
            Me.lbKM_Keys.ClearSelected()
            Me.cbKM_ControlKey.Checked = False
        Catch ex As Exception
        End Try
        Try
            Me.lbKM_Keys.SelectedIndex = Me.lbKM_Keys.FindStringExact([Enum].GetName(GetType(Keys), k.KeyCode))
            Me.cbKM_ControlKey.Checked = k.Control
        Catch ex As Exception
            PP.AddConsoleLine(eConsoleItemType.ERROR, "Problem with SelectKey. Error: " & ex.Message)
        End Try
    End Sub

    Private Sub txtValue_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtValue.TextChanged
        If Me.txtValue.Text.Length < 8 Then
            Me.txtValue.Text = PadString(Me.txtValue.Text, 8, "0", True)
        End If
    End Sub

    Public Function SetKeyBits(ByVal InKeyVal As String, ByVal Ctrl As Boolean) As String
        Try
            Dim Out As String
            If InKeyVal > 16 Then
                'cut off the extra values
                Out = Microsoft.VisualBasic.Right(InKeyVal, 16)
            ElseIf InKeyVal < 16 Then
                'pad upto 16
                Out = PadString(InKeyVal, 16, "0", True)
            End If

            'set shift to false
            Out = "0" & Out

            If Ctrl Then
                Out = "1" & Out
            Else
                Out = "0" & Out
            End If

            'set alt to false
            Out = "0" & Out
            Return Out
        Catch ex As Exception
            PP.AddConsoleLine(eConsoleItemType.ERROR, "Problem with SetKeyBits in KeyMapping. Error: " & ex.Message)
            Return Nothing
        End Try
    End Function

    Private Sub llPrintKeyMapping_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llKM_PrintKeyMapping.LinkClicked
        Dim s As String = PP.CurrentUserProfile.KeyMapping.SavePlainTextString(False)
        If File.Exists(s) Then
            PrintTextFileWithNotepad(s)
        End If
    End Sub

    Private Sub llReturnToDefaults_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llKM_ReturnToDefaults.LinkClicked
        If XtraMessageBox.Show(Me.LookAndFeel, Me, "Are you sure you want to reset the key mapping to the default setup?", My.Settings.APPLICATION_NAME, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = MsgBoxResult.No Then Exit Sub
        PP.CurrentUserProfile.KeyMapping.InitializeDefaultKeymapping()
        SetMeUp()
    End Sub

    Private Sub KeyMapping_HandleKeystrike(ByVal sender As Object, ByVal e As KeyEventArgs) Handles lbKM_Keys.KeyUp
        Try

            If e.KeyCode = Keys.Up Or e.KeyCode = Keys.Down Or e.KeyCode = Keys.Right Or e.KeyCode = Keys.Left Then
                Exit Sub
            ElseIf e.KeyCode = Keys.Enter Then
                llSET_LinkClicked(Me, Nothing)
                Exit Sub
            End If

            Me.lbKM_Keys.SelectedIndex = -1
            Dim tStr As String = e.KeyData.ToString
            'Debug.WriteLine(tStr)
            'Debug.WriteLine(e.KeyValue)
            Me.lbKM_Keys.SelectedIndex = Me.lbKM_Keys.FindString(tStr)

            'Select Case tStr
            '    Case "D1", "D2", "D3", "D4", "D5", "D6", "D7", "D8", "D9", "D0"
            '        Me.lbKM_Keys.SelectedIndex = Me.lbKM_Keys.FindString(tStr)
            '    Case "F1"
            '        Me.lbKM_Keys.SelectedIndex = Me.lbKM_Keys.FindString("F1")
            '    Case "F2"
            '        Me.lbKM_Keys.SelectedIndex = Me.lbKM_Keys.FindString("F2")
            '    Case "F3"
            '        Me.lbKM_Keys.SelectedIndex = Me.lbKM_Keys.FindString("F3")
            '    Case "F4"
            '        Me.lbKM_Keys.SelectedIndex = Me.lbKM_Keys.FindString("F4")
            '    Case "F5"
            '        Me.lbKM_Keys.SelectedIndex = Me.lbKM_Keys.FindString("F5")
            '    Case "F6"
            '        Me.lbKM_Keys.SelectedIndex = Me.lbKM_Keys.FindString("F6")
            '    Case "F7"
            '        Me.lbKM_Keys.SelectedIndex = Me.lbKM_Keys.FindString("F7")
            '    Case "F8"
            '        Me.lbKM_Keys.SelectedIndex = Me.lbKM_Keys.FindString("F8")
            '    Case "F9"
            '        Me.lbKM_Keys.SelectedIndex = Me.lbKM_Keys.FindString("F9")
            '    Case "F10"
            '        Me.lbKM_Keys.SelectedIndex = Me.lbKM_Keys.FindString("F10")
            '    Case "NumPad1", "NumPad2", "NumPad3", "NumPad4", "NumPad5", "NumPad6", "NumPad7", "NumPad8", "NumPad9"
            '        Me.lbKM_Keys.SelectedIndex = Me.lbKM_Keys.FindString(e.KeyData.ToString)
            '    Case "Space"
            '        Me.lbKM_Keys.SelectedIndex = Me.lbKM_Keys.FindString("Space")
            '    Case "End"
            '        Me.lbKM_Keys.SelectedIndex = Me.lbKM_Keys.FindString("End")
            '    Case "Delete"
            '        Me.lbKM_Keys.SelectedIndex = Me.lbKM_Keys.FindString("Delete")
            '    Case "Insert"
            '        Me.lbKM_Keys.SelectedIndex = Me.lbKM_Keys.FindString("Insert")
            '    Case "Home"
            '        Me.lbKM_Keys.SelectedIndex = Me.lbKM_Keys.FindString("Home")
            '    Case "Prior"
            '        Me.lbKM_Keys.SelectedIndex = Me.lbKM_Keys.FindString("PageUp")
            '    Case "Next"
            '        Me.lbKM_Keys.SelectedIndex = Me.lbKM_Keys.FindString("PageDown")
            '    Case "OemOpenBrackets"
            '        Me.lbKM_Keys.SelectedIndex = Me.lbKM_Keys.FindString("[")
            '    Case "OemCloseBrackets"
            '        Me.lbKM_Keys.SelectedIndex = Me.lbKM_Keys.FindString("]")
            '    Case "OemSemicolon"
            '        Me.lbKM_Keys.SelectedIndex = Me.lbKM_Keys.FindString(";")
            '    Case "OemQuotes"
            '        Me.lbKM_Keys.SelectedIndex = Me.lbKM_Keys.FindString("'")
            '    Case "OemQuestion"
            '        Me.lbKM_Keys.SelectedIndex = Me.lbKM_Keys.FindString("/")
            '    Case "Oemcomma"
            '        Me.lbKM_Keys.SelectedIndex = Me.lbKM_Keys.FindString(",")
            '    Case "Oemplus"
            '        Me.lbKM_Keys.SelectedIndex = Me.lbKM_Keys.FindString("=")
            '    Case "OemPeriod"
            '        Me.lbKM_Keys.SelectedIndex = Me.lbKM_Keys.FindString("OemPeriod")
            '    Case "OemPipe"
            '        Me.lbKM_Keys.SelectedIndex = Me.lbKM_Keys.FindString("\")
            '    Case "Oemtilde"
            '        Me.lbKM_Keys.SelectedIndex = Me.lbKM_Keys.FindString("`")
            '    Case "OemMinus"
            '        Me.lbKM_Keys.SelectedIndex = Me.lbKM_Keys.FindString("-")
            '    Case "Divide"
            '        Me.lbKM_Keys.SelectedIndex = Me.lbKM_Keys.FindString("NumDivide")
            '    Case "Multiply"
            '        Me.lbKM_Keys.SelectedIndex = Me.lbKM_Keys.FindString("NumMultiply")
            '    Case "Subtract"
            '        Me.lbKM_Keys.SelectedIndex = Me.lbKM_Keys.FindString("NumSubtract")
            '    Case "Add"
            '        Me.lbKM_Keys.SelectedIndex = Me.lbKM_Keys.FindString("NumAdd")
            '    Case "Decimal"
            '        Me.lbKM_Keys.SelectedIndex = Me.lbKM_Keys.FindString("NumDecimalPoint")
            '    Case Else
            '        Me.lbKM_Keys.SelectedIndex = Me.lbKM_Keys.FindString(tStr)

            '        'Case "A"
            '        '    Me.lbKeys.SelectedIndex = Me.lbKeys.FindString("D")
            '        'Case "B"
            '        '    Me.lbKeys.SelectedIndex = Me.lbKeys.FindString("D")
            '        'Case "D"
            '        '    Me.lbKeys.SelectedIndex = Me.lbKeys.FindString("D")
            '        'Case "D"
            '        '    Me.lbKeys.SelectedIndex = Me.lbKeys.FindString("D")
            '        'Case "E"
            '        '    Me.lbKeys.SelectedIndex = Me.lbKeys.FindString("E")
            '        'Case "F"
            '        '    Me.lbKeys.SelectedIndex = Me.lbKeys.FindString("F")
            '        'Case "H"
            '        '    Me.lbKeys.SelectedIndex = Me.lbKeys.FindString("H")
            '        'Case "I"
            '        '    Me.lbKeys.SelectedIndex = Me.lbKeys.FindString("I")
            '        'Case "N"
            '        '    Me.lbKeys.SelectedIndex = Me.lbKeys.FindString("N")
            '        'Case "P"
            '        '    Me.lbKeys.SelectedIndex = Me.lbKeys.FindString("P")
            '        'Case "S"
            '        '    Me.lbKeys.SelectedIndex = Me.lbKeys.FindString("S")

            '        'Exit Sub
            'End Select

            e.Handled = True
        Catch ex As Exception
            PP.AddConsoleLine(eConsoleItemType.ERROR, "Problem with KeyDown in KM. Error: " & ex.Message, Nothing)
        End Try
    End Sub

    Private Sub cbKM_ControlKey_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbKM_ControlKey.CheckedChanged
        UpdateValue()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.dgKM_KeyMapping.RowHeadersVisible = False
    End Sub

    Private Sub llSave_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        PP.CurrentUserProfile.KeyMapping.SavePlainTextString(True)
    End Sub

#End Region 'KEY MAPPING

#Region "VIDEO"

    Private FormIsLoading As Boolean = True

    Private Sub Form_Load_Video(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Me.tpVideo.PageEnabled = PP.FeatureManagement.Features.FE_VA_ProcAmp 'currently applying ProcAmp to all

        Me.cbPA_Presets.SelectedIndex = 0
        LoadProcAmpConfigNamesIntoDropDown()
        Me.cbForceFieldReversal.Checked = PP.Player.ReverseFieldOrder
        Me.cbBumpFields.Checked = PP.Player.BumpingFieldsDown
        'SetCurrentDeinterlacing()
        SetCurrentMPEGFrameMode()

        Me.cbSplitFields.Checked = PP.Player.FieldSplitting
        Me.cbVID_ReverseFieldOrder.Checked = PP.Player.ReverseFieldOrder

        'Load ProcAmp Settings
        Me.txtBrightness.Text = PP.CurrentUserProfile.AppOptions.PA_CurrentSettings.Brightness
        Me.tbBrightness.Value = PP.CurrentUserProfile.AppOptions.PA_CurrentSettings.Brightness
        Me.txtContrast.Text = PP.CurrentUserProfile.AppOptions.PA_CurrentSettings.Contrast
        Me.tbContrast.Value = PP.CurrentUserProfile.AppOptions.PA_CurrentSettings.Contrast
        Me.txtHue.Text = PP.CurrentUserProfile.AppOptions.PA_CurrentSettings.Hue
        Me.tbHue.Value = PP.CurrentUserProfile.AppOptions.PA_CurrentSettings.Hue
        Me.txtSaturation.Text = PP.CurrentUserProfile.AppOptions.PA_CurrentSettings.Saturation
        Me.tbSaturation.Value = PP.CurrentUserProfile.AppOptions.PA_CurrentSettings.Saturation
        Me.cbProcAmpHalfFrame.Checked = PP.CurrentUserProfile.AppOptions.PA_HalfFrame
        Me.cbDoProcAmp.Checked = PP.CurrentUserProfile.AppOptions.PA_DoProcAmp

        Me.cbVID_NonSeamlessCellNotification.Checked = My.Settings.DVD_NOTIFY_NONSEAMLESSCELL

        FormIsLoading = False
    End Sub

#Region "Video Test Signals"

    Private Sub btnColorBars_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnColorBars.Click
        If Not PP.FeatureManagement.Features.FE_VA_TestPatterns Then Exit Sub
        Try
            If Me.btnColorBars.ButtonStyle = BorderStyles.Default Then
                PP.Player.ShowColorBars(True)
                Me.btnColorBars.ButtonStyle = BorderStyles.Office2003
            Else
                PP.Player.ShowColorBars(False)
                Me.btnColorBars.ButtonStyle = BorderStyles.Default
            End If
        Catch ex As Exception
            PP.AddConsoleLine(eConsoleItemType.ERROR, "Problem with btnColorBars_Click. Error: " & ex.Message)
        End Try
    End Sub

    Private Sub btnTestPattern_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTestPattern.Click
        If Not PP.FeatureManagement.Features.FE_VA_TestPatterns Then Exit Sub
        Try
            If Me.btnTestPattern.ButtonStyle = BorderStyles.Default Then
                PP.Player.ShowTestPattern(True)
                Me.btnTestPattern.ButtonStyle = BorderStyles.Office2003
            Else
                PP.Player.ShowTestPattern(False)
                Me.btnTestPattern.ButtonStyle = BorderStyles.Default
            End If
        Catch ex As Exception
            PP.AddConsoleLine(eConsoleItemType.ERROR, "Problem with btnTestPattern_Click. Error: " & ex.Message)
        End Try
    End Sub

#End Region 'Video Test Signals

#Region "ProcAmp"

    Private Sub cbDoProcAmp_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbDoProcAmp.CheckedChanged
        If FormIsLoading Then Exit Sub
        If PP.CurrentUserProfile.AppOptions.PA_DoProcAmp Then
            PP.CurrentUserProfile.AppOptions.PA_DoProcAmp = False
        Else
            PP.CurrentUserProfile.AppOptions.PA_DoProcAmp = True
        End If
    End Sub

    Private Sub cbProcAmpHalfFrame_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbProcAmpHalfFrame.CheckedChanged
        If FormIsLoading Then Exit Sub
        PP.CurrentUserProfile.AppOptions.PA_HalfFrame = CType(sender, CheckBox).Checked
    End Sub

#Region "ProcAmp Settings Storage"

    Public Sub GetRegProcAmpConfig(ByVal ConfigName As String)
        Try
            Dim pa As cProcAmpTemplate
            For Each p As cProcAmpTemplate In PP.CurrentUserProfile.AppOptions.PA_SavedTemplates
                If p.Name.ToLower = ConfigName.ToLower Then
                    pa = p
                    Exit For
                End If
            Next

            Me.tbBrightness.Value = pa.Brightness
            Me.txtBrightness.Text = pa.Brightness

            Me.tbContrast.Value = pa.Contrast
            Me.txtContrast.Text = pa.Contrast

            Me.tbHue.Value = pa.Hue
            Me.txtHue.Text = pa.Hue

            Me.tbSaturation.Value = pa.Saturation
            Me.txtSaturation.Text = pa.Saturation

        Catch ex As Exception
            PP.AddConsoleLine(eConsoleItemType.ERROR, "Problem with GetRegProcAmp. Error: " & ex.Message)
        End Try
    End Sub

    Public Function CheckIfPANameIsDuplicate(ByVal ConfigName As String) As Boolean
        Try
            For Each pa As cProcAmpTemplate In PP.CurrentUserProfile.AppOptions.PA_SavedTemplates
                If pa.Name.ToLower = ConfigName.ToLower Then
                    Return True
                End If
            Next
            Return False
        Catch ex As Exception
            PP.AddConsoleLine(eConsoleItemType.ERROR, "Problem with CheckIfPANameIsDuplicate. Error: " & ex.Message)
        End Try
    End Function

    Public ReadOnly Property ProcAmpSettingsFolder() As String
        Get
            Dim s As String = GetExePath() & "\ProcAmpSettings\"
            If Not Directory.Exists(s) Then
                Directory.CreateDirectory(s)
            End If
            Return s
        End Get
    End Property

    Private Sub llPA_SaveSettings_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llPA_SaveSettings.LinkClicked
        'save current settings.
RestartSaveSettings:
        PP.KH.PauseHook()
        Dim s As String = InputBox("Name for settings:", "ProcAmp Settings", , Me.Left, Me.Top)
        If s = "" Then
            If XtraMessageBox.Show(Me.LookAndFeel, Me, "Invalid Name. Settings not saved. Try again?", My.Settings.APPLICATION_NAME, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = MsgBoxResult.No Then
                PP.KH.UnpauseHook()
                Exit Sub
            End If
            GoTo RestartSaveSettings
        Else
            PP.KH.UnpauseHook()
            'save settings
            'ProcAmpSettingsFolder

            'REGISTRY VERSION
            If CheckIfPANameIsDuplicate(s) Then
                Dim MBR As Microsoft.VisualBasic.MsgBoxResult = XtraMessageBox.Show(Me.LookAndFeel, Me, "ProcAmp config name is duplicate. Overwrite?", My.Settings.APPLICATION_NAME, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
                If MBR = MsgBoxResult.Yes Then
                    GoTo SaveSettings
                ElseIf MBR = MsgBoxResult.No Then
                    GoTo RestartSaveSettings
                ElseIf MBR = MsgBoxResult.Cancel Then
                    Exit Sub
                End If
            End If

SaveSettings:
            Dim PAT As New cProcAmpTemplate(PP)
            PAT.Brightness = Me.txtBrightness.Text
            PAT.Contrast = Me.txtContrast.Text
            PAT.Hue = Me.txtHue.Text
            PAT.Saturation = Me.txtSaturation.Text
            PAT.Name = s
            PP.CurrentUserProfile.AppOptions.AddProcAmpTemplate(PAT)

            Dim i As Integer = Me.cbPA_Presets.FindStringExact(s)
            If i < 0 Then
                Me.cbPA_Presets.Items.Add(s)
                i = Me.cbPA_Presets.FindStringExact(s)
            End If
            Me.cbPA_Presets.SelectedIndex = i
        End If
    End Sub

    Private Sub llPA_LoadSettings_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llPA_LoadSettings.LinkClicked
        'REGISTRY VERSION
        'display a list of available procamp configs from reg, let user select which one



        'FILE VERSION
        'Dim fsd As New OpenFileDialog
        'fsd.Filter = "ProcAmp Settings | *.ProcAmp"
        'fsd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
        'fsd.Multiselect = True
        'fsd.Title = "Select ProcAmp file(s)"
        'If fsd.ShowDialog = DialogResult.OK Then
        '    For i As Short = 0 To UBound(fsd.FileNames)
        '        'if the file isn't already in the right place copy it there
        '        If Not Path.GetDirectoryName(fsd.FileNames(i)) = ProcAmpSettingsFolder Then
        '            File.Move(fsd.FileNames(i), ProcAmpSettingsFolder & Path.GetFileName(fsd.FileNames(i)))
        '        End If
        '        'now add it to the combo box
        '        Me.cbPA_Presets.Items.Add(Path.GetFileNameWithoutExtension(fsd.FileNames(i)))
        '        'and now select it
        '        Me.cbPA_Presets.SelectedIndex = Me.cbPA_Presets.Items.Count - 1
        '    Next
        'End If
    End Sub

    Public Function GetProcAmpConfigNames() As String()
        Try
            If PP.CurrentUserProfile.AppOptions.PA_SavedTemplates Is Nothing Then Return Nothing
            Dim o(-1) As String
            For Each pa As cProcAmpTemplate In PP.CurrentUserProfile.AppOptions.PA_SavedTemplates
                ReDim Preserve o(UBound(o) + 1)
                o(UBound(o)) = pa.Name
            Next
            Return o
        Catch ex As Exception
            PP.AddConsoleLine(eConsoleItemType.ERROR, "Problem with GetProcAmpConfigNames. Error: " & ex.Message)
        End Try
    End Function

    Public Sub LoadProcAmpConfigNamesIntoDropDown()
        Try
            'REGISTRY VERSION
            'get names of procamp configs from registry
            Dim Names() As String = Me.GetProcAmpConfigNames
            If Names Is Nothing Then Exit Sub
            For Each s As String In Names
                Me.cbPA_Presets.Items.Add(s)
            Next

            'FILE VERSION
            'Dim s() As String = Directory.GetFileSystemEntries(ProcAmpSettingsFolder)
            'For i As Short = 0 To UBound(s)
            '    If Path.GetExtension(s(i)).ToLower = ".procamp" Then
            '        Me.cbPA_Presets.Items.Add(Path.GetFileNameWithoutExtension(s(i)))
            '    End If
            'Next
        Catch ex As Exception
            PP.AddConsoleLine(eConsoleItemType.ERROR, "Problem with LoadProcAmpSettings. Error: " & ex.Message)
        End Try
    End Sub

    'Public Sub LoadSettingsFromFile(ByVal SN As String)
    '    'this is where we actually open the file and get the user's setings
    '    Dim s As String = ProcAmpSettingsFolder & SN & ".ProcAmp"
    '    If Not File.Exists(s) Then
    '        MsgBox("Settings file (" & SN & ") could not be found. Please hang up or try your call again later.", MsgBoxStyle.Information Or MsgBoxStyle.OKCancel)
    '        Exit Sub
    '    End If
    '    Dim FS As New FileStream(s, FileMode.Open)
    '    Dim SR As New StreamReader(FS)
    '    s = SR.ReadToEnd

    '    'TODO: finish this.
    '    'you need to make a class that stores the current settings in an array
    '    'for serialization.

    '    FS.Close()
    '    FS = Nothing
    'End Sub

    Private Sub llPA_DeleteSettings_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llPA_DeleteSettings.LinkClicked
        'REGISTRY VERSION
        Try
            Dim s As String = Me.cbPA_Presets.SelectedItem.ToString.ToLower
            If s = "mpeg blocking" Or s = "defaults" Then
                XtraMessageBox.Show(Me.LookAndFeel, Me, "Cannot delete system ProcAmp configs.", My.Settings.APPLICATION_NAME, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
            If XtraMessageBox.Show(Me.LookAndFeel, Me, "Delete ProcAmp Config: " & s & " ?", My.Settings.APPLICATION_NAME, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = MsgBoxResult.Yes Then
                Dim pa(-1) As cProcAmpTemplate
                For Each p As cProcAmpTemplate In PP.CurrentUserProfile.AppOptions.PA_SavedTemplates
                    If p.Name.ToLower <> s.ToLower Then
                        ReDim Preserve pa(UBound(pa) + 1)
                        pa(UBound(pa)) = p
                    End If
                Next
                PP.CurrentUserProfile.AppOptions.PA_SavedTemplates = pa

                Me.cbPA_Presets.Items.Remove(s)
                Me.cbPA_Presets.SelectedIndex = 0
            End If
        Catch ex As Exception
            PP.AddConsoleLine(eConsoleItemType.ERROR, "Problem deleting ProcAmp settings. Error: " & ex.Message)
        End Try

        'FILE VERSION
        'Process.Start(ProcAmpSettingsFolder)
    End Sub

    Private Sub cbPA_Presets_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbPA_Presets.SelectedIndexChanged
        Try
            Dim s As String = Me.cbPA_Presets.SelectedItem.ToString.ToLower
            Select Case s
                Case "defaults"
                    'default 0.0
                    Me.txtBrightness.Text = 0
                    Me.tbBrightness.Value = 0

                    'default 1.0
                    Me.txtContrast.Text = -80
                    Me.tbContrast.Value = -80

                    'default 0.0
                    Me.txtHue.Text = 0
                    Me.tbHue.Value = 0

                    'default 1.0
                    Me.txtSaturation.Text = -80
                    Me.tbSaturation.Value = -80

                Case "mpeg blocking"
                    Me.tbBrightness.Value = -100
                    Me.txtBrightness.Text = -100

                    Me.tbContrast.Value = -100
                    Me.txtContrast.Text = -100

                    Me.tbHue.Value = 100
                    Me.txtHue.Text = 100

                    Me.tbSaturation.Value = 100
                    Me.txtSaturation.Text = 100

                    Me.cbProcAmpHalfFrame.Checked = True
                    Me.cbDoProcAmp.Checked = True

                Case "noise"
                    Me.tbBrightness.Value = -100
                    Me.txtBrightness.Text = -100

                    Me.tbContrast.Value = 10
                    Me.txtContrast.Text = 10

                    Me.tbHue.Value = -100
                    Me.txtHue.Text = -100

                    Me.tbSaturation.Value = -100
                    Me.txtSaturation.Text = -100

                    Me.cbProcAmpHalfFrame.Checked = True
                    Me.cbDoProcAmp.Checked = True

                Case Else
                    'it's a custom procamp config
                    GetRegProcAmpConfig(s)

            End Select
        Catch ex As Exception
            PP.AddConsoleLine(eConsoleItemType.ERROR, "Problem with procamp presets. Error: " & ex.Message)
        End Try
    End Sub

#End Region 'ProcAmp Settings Storage

    Private Sub btnDefaults_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        'default 0.0
        Me.txtBrightness.Text = 0
        Me.tbBrightness.Value = 0

        'default 1.0
        Me.txtContrast.Text = -80
        Me.tbContrast.Value = -80

        'default 0.0
        Me.txtHue.Text = 0
        Me.tbHue.Value = 0

        'default 1.0
        Me.txtSaturation.Text = -80
        Me.tbSaturation.Value = -80

        'Me.txtGamma.Text = 0
        'Me.tbGamma.Value = 0
    End Sub

    Private Sub tbBrightness_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbBrightness.Scroll
        If FormIsLoading Then Exit Sub
        Me.txtBrightness.Text = Me.tbBrightness.Value
    End Sub

    Private Sub txtBrightness_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBrightness.TextChanged
        If FormIsLoading Then Exit Sub
        If PP Is Nothing Then Exit Sub
        If Not IsNumeric(Me.txtBrightness.Text) Then Exit Sub
        If Me.txtBrightness.Text > 100 Or Me.txtBrightness.Text < -100 Then Me.txtBrightness.Text = 0
        PP.CurrentUserProfile.AppOptions.PA_CurrentSettings.Brightness = Me.txtBrightness.Text
        Me.tbBrightness.Value = Me.txtBrightness.Text
    End Sub

    Private Sub tbContrast_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbContrast.Scroll
        If FormIsLoading Then Exit Sub
        Me.txtContrast.Text = Me.tbContrast.Value
    End Sub

    Private Sub txtContrast_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtContrast.TextChanged
        If FormIsLoading Then Exit Sub
        If PP Is Nothing Then Exit Sub
        If Not IsNumeric(Me.txtContrast.Text) Then Exit Sub
        If Me.txtContrast.Text > 100 Or Me.txtContrast.Text < -100 Then Me.txtContrast.Text = 0
        PP.CurrentUserProfile.AppOptions.PA_CurrentSettings.Contrast = Me.txtContrast.Text
        Me.tbContrast.Value = Me.txtContrast.Text
    End Sub

    Private Sub tbhue_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbHue.Scroll
        If FormIsLoading Then Exit Sub
        Me.txtHue.Text = Me.tbHue.Value
    End Sub

    Private Sub txthue_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtHue.TextChanged
        If FormIsLoading Then Exit Sub
        If PP Is Nothing Then Exit Sub
        If Not IsNumeric(Me.txtHue.Text) Then Exit Sub
        If Me.txtHue.Text > 100 Or Me.txtHue.Text < -100 Then Me.txtHue.Text = 0
        PP.CurrentUserProfile.AppOptions.PA_CurrentSettings.Hue = Me.txtHue.Text
        Me.tbHue.Value = Me.txtHue.Text
    End Sub

    Private Sub tbsaturation_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbSaturation.Scroll
        If FormIsLoading Then Exit Sub
        Me.txtSaturation.Text = Me.tbSaturation.Value
    End Sub

    Private Sub txtsaturation_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSaturation.TextChanged
        If FormIsLoading Then Exit Sub
        If PP Is Nothing Then Exit Sub
        If Not IsNumeric(Me.txtSaturation.Text) Then Exit Sub
        If Me.txtSaturation.Text > 100 Or Me.txtSaturation.Text < -100 Then Me.txtSaturation.Text = 0
        PP.CurrentUserProfile.AppOptions.PA_CurrentSettings.Saturation = Me.txtSaturation.Text
        Me.tbSaturation.Value = Me.txtSaturation.Text
    End Sub

#End Region 'ProcAmp

#Region "Capture"

    Private Sub btnCaptureBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCaptureBrowse.Click
        Try
            Dim dlg As New Windows.Forms.SaveFileDialog
            dlg.AddExtension = True
            dlg.DefaultExt = ".AVI"
            dlg.Filter = "Audio Video Interleave | *.avi"
            dlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)
            dlg.OverwritePrompt = True
            dlg.Title = "Select/Create Video Capture File"
            dlg.ValidateNames = True
            If dlg.ShowDialog = DialogResult.OK Then
                Me.txtCaptureFileName.Text = dlg.FileName
            End If
            dlg.Dispose()
            dlg = Nothing
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnStartStopVideoCapture_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStartStopVideoCapture.Click
        Try
            If VideoCaptureRunning Then
                StopVideoCapture()
            Else
                StartVideoCapture()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public VideoCaptureRunning As Boolean
    Public Function StartVideoCapture() As Boolean
        Try
            VideoCaptureRunning = True
            Me.btnStartStopVideoCapture.Text = "Stop Capture"
            Me.btnStartStopVideoCapture.BackColor = Color.Red
            Return True
        Catch ex As Exception
            PP.AddConsoleLine(eConsoleItemType.ERROR, "Problem with StartVideoCapture. Error: " & ex.Message)
            Return False
        End Try
    End Function

    Public Function StopVideoCapture() As Boolean
        Try
            VideoCaptureRunning = False
            Me.btnStartStopVideoCapture.Text = "Start Capture"
            Me.btnStartStopVideoCapture.BackColor = SystemColors.Control
            Return True
        Catch ex As Exception
            PP.AddConsoleLine(eConsoleItemType.ERROR, "Problem with StopVideoCapture. Error: " & ex.Message)
            Return False
        End Try
    End Function

#End Region 'Capture

#Region "Time Delta"

    Private Sub tbTimeDelta_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbTimeDelta.Scroll
        Me.txtTimeDeltaVal.Text = Me.tbTimeDelta.Value
    End Sub

    Private Sub txtTimeDeltaVal_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTimeDeltaVal.TextChanged
        If Not IsNumeric(Me.txtTimeDeltaVal.Text) Then Exit Sub
        If Me.txtTimeDeltaVal.Text > 1000 Or Me.txtTimeDeltaVal.Text < -1000 Then
            PP.AddConsoleLine(eConsoleItemType.NOTICE, "Invalid video time delta value. Must be between -1000 and 1000.")
            Me.txtTimeDeltaVal.Text = 0
            Exit Sub
        End If
        Me.tbTimeDelta.Value = Me.txtTimeDeltaVal.Text
    End Sub

    Private Sub btnTimeDeltaSet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTimeDeltaSet.Click
        SetVideoTimeDelta()
    End Sub

    Public Function SetVideoTimeDelta()
        MsgBox("todo")
    End Function

#End Region 'Time Delta

#Region "Color Filters"

    Private Sub rbFilters_none_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbFilters_none.CheckedChanged
        PP.Player.ProcAmp_ChannelFilter = False
    End Sub

    Private Sub rbFilters_bandw_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbFilters_bandw.CheckedChanged
        If Not PP.FeatureManagement.Features.FE_VA_YUVChannelFiltering Then Exit Sub
        PP.Player.ProcAmp_WhichChannelFilter = eChannelFilter.Y_Only
        PP.Player.ProcAmp_ChannelFilter = True
    End Sub

    Private Sub rbFilters_Yb_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbFilters_YUV_MinusV.CheckedChanged
        If Not PP.FeatureManagement.Features.FE_VA_YUVChannelFiltering Then Exit Sub
        PP.Player.ProcAmp_WhichChannelFilter = eChannelFilter.YUV_MinusV
        PP.Player.ProcAmp_ChannelFilter = True
    End Sub

    Private Sub rbFilters_Yr_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbFilters_YUV_MinusU.CheckedChanged
        If Not PP.FeatureManagement.Features.FE_VA_YUVChannelFiltering Then Exit Sub
        PP.Player.ProcAmp_WhichChannelFilter = eChannelFilter.YUV_MinusU
        PP.Player.ProcAmp_ChannelFilter = True
    End Sub

#End Region 'Color Filters

    Private Sub btnVideoScrap_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVideoScrap.Click
        Try
            'Dim Val As Integer
            'PP.nvVideoAtts.GetLong(nvcommon.ENvVideoDecoderProps.NVVIDDEC_CONFIG, nvcommon.ENvidiaVideoDecoderProps_ConfigTypes.NVVIDDEC_CONFIG_BRIGHTNESS, Val)
            'PP.AddConsoleLine("Val: " & Val)
            'GetProcAmpControlRanges()
        Catch ex As Exception
            PP.AddConsoleLine(eConsoleItemType.ERROR, "Problem with video adjustments scrap. Error: " & ex.Message)
        End Try
    End Sub

    Private Sub cbForceFieldReversal_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbForceFieldReversal.CheckedChanged
        If Not PP.FeatureManagement.Features.FE_VA_ReverseFieldOrder Then Exit Sub
        PP.Player.ReverseFieldOrder = Me.cbForceFieldReversal.Checked
    End Sub

    Private Sub cbBumpFields_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbBumpFields.CheckedChanged
        PP.Player.BumpFieldsDown(Me.cbBumpFields.Checked)
    End Sub

#Region "Deinterlacing"

    'Private Sub SetCurrentDeinterlacing()
    '    Dim i As nvcommon.ENVVIDDEC_CONFIG_DEINTERLACE_MODE
    '    PP.Player.Graph.nvVideoAtts.GetLong(nvcommon.ENvVideoDecoderProps.NVVIDDEC_CONFIG, nvcommon.ENvidiaVideoDecoderProps_ConfigTypes.NVVIDDEC_CONFIG_DEINTERLACE_MODE, i)

    '    Select Case i
    '        Case nvcommon.ENVVIDDEC_CONFIG_DEINTERLACE_MODE.NVVIDDEC_CONFIG_DEINTERLACE_BOB
    '            Me.rbDEINT_Mode_Bob.Checked = True
    '        Case nvcommon.ENVVIDDEC_CONFIG_DEINTERLACE_MODE.NVVIDDEC_CONFIG_DEINTERLACE_FILTERED_WEAVE
    '            Me.rbDEINT_Mode_WeaveFiltered.Checked = True
    '        Case nvcommon.ENVVIDDEC_CONFIG_DEINTERLACE_MODE.NVVIDDEC_CONFIG_DEINTERLACE_NORMAL
    '            Me.rbDEINT_Mode_Normal.Checked = True
    '        Case nvcommon.ENVVIDDEC_CONFIG_DEINTERLACE_MODE.NVVIDDEC_CONFIG_DEINTERLACE_SPAD
    '            Me.rbDEINT_Mode_Spad.Checked = True
    '        Case nvcommon.ENVVIDDEC_CONFIG_DEINTERLACE_MODE.NVVIDDEC_CONFIG_DEINTERLACE_WEAVE
    '            Me.rbDEINT_Mode_Weave.Checked = True
    '    End Select

    '    Dim ii As nvcommon.ENVVIDDEC_CONFIG_DEINTERLACE_MODE_CTRL
    '    PP.Player.Graph.nvVideoAtts.GetLong(nvcommon.ENvVideoDecoderProps.NVVIDDEC_CONFIG, nvcommon.ENvidiaVideoDecoderProps_ConfigTypes.NVVIDDEC_CONFIG_DEINTERLACE_CONTROL, ii)

    '    Select Case ii
    '        Case nvcommon.ENVVIDDEC_CONFIG_DEINTERLACE_MODE_CTRL.NVVIDDEC_CONFIG_DEINTERLACE_CTRL_AUTO
    '            Me.rbDEINT_Control_Auto.Checked = True
    '        Case nvcommon.ENVVIDDEC_CONFIG_DEINTERLACE_MODE_CTRL.NVVIDDEC_CONFIG_DEINTERLACE_CTRL_FILM
    '            Me.rbDEINT_Control_Film.Checked = True
    '        Case nvcommon.ENVVIDDEC_CONFIG_DEINTERLACE_MODE_CTRL.NVVIDDEC_CONFIG_DEINTERLACE_CTRL_SMART
    '            Me.rbDEINT_Control_Smart.Checked = True
    '        Case nvcommon.ENVVIDDEC_CONFIG_DEINTERLACE_MODE_CTRL.NVVIDDEC_CONFIG_DEINTERLACE_CTRL_VIDEO
    '            Me.rbDEINT_Control_Video.Checked = True
    '    End Select

    'End Sub

    'Private Sub rbDEINT_Mode_Normal_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbDEINT_Mode_Normal.CheckedChanged
    '    If CType(sender, RadioButton).Checked Then
    '        PP.Player.Graph.nvVideoAtts.SetLong(nvcommon.ENvVideoDecoderProps.NVVIDDEC_CONFIG, nvcommon.ENvidiaVideoDecoderProps_ConfigTypes.NVVIDDEC_CONFIG_DEINTERLACE_MODE, nvcommon.ENVVIDDEC_CONFIG_DEINTERLACE_MODE.NVVIDDEC_CONFIG_DEINTERLACE_NORMAL)
    '    End If
    'End Sub

    'Private Sub rbDEINT_Mode_Weave_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbDEINT_Mode_Weave.CheckedChanged
    '    If CType(sender, RadioButton).Checked Then
    '        PP.Player.Graph.nvVideoAtts.SetLong(nvcommon.ENvVideoDecoderProps.NVVIDDEC_CONFIG, nvcommon.ENvidiaVideoDecoderProps_ConfigTypes.NVVIDDEC_CONFIG_DEINTERLACE_MODE, nvcommon.ENVVIDDEC_CONFIG_DEINTERLACE_MODE.NVVIDDEC_CONFIG_DEINTERLACE_WEAVE)
    '    End If
    'End Sub

    'Private Sub rbDEINT_Mode_Bob_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbDEINT_Mode_Bob.CheckedChanged
    '    If CType(sender, RadioButton).Checked Then
    '        PP.Player.Graph.nvVideoAtts.SetLong(nvcommon.ENvVideoDecoderProps.NVVIDDEC_CONFIG, nvcommon.ENvidiaVideoDecoderProps_ConfigTypes.NVVIDDEC_CONFIG_DEINTERLACE_MODE, nvcommon.ENVVIDDEC_CONFIG_DEINTERLACE_MODE.NVVIDDEC_CONFIG_DEINTERLACE_BOB)
    '    End If
    'End Sub

    'Private Sub rbDEINT_Mode_Spad_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbDEINT_Mode_Spad.CheckedChanged
    '    If CType(sender, RadioButton).Checked Then
    '        PP.Player.Graph.nvVideoAtts.SetLong(nvcommon.ENvVideoDecoderProps.NVVIDDEC_CONFIG, nvcommon.ENvidiaVideoDecoderProps_ConfigTypes.NVVIDDEC_CONFIG_DEINTERLACE_MODE, nvcommon.ENVVIDDEC_CONFIG_DEINTERLACE_MODE.NVVIDDEC_CONFIG_DEINTERLACE_SPAD)
    '    End If
    'End Sub

    'Private Sub rbDEINT_Mode_WeaveFiltered_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbDEINT_Mode_WeaveFiltered.CheckedChanged
    '    If CType(sender, RadioButton).Checked Then
    '        PP.Player.Graph.nvVideoAtts.SetLong(nvcommon.ENvVideoDecoderProps.NVVIDDEC_CONFIG, nvcommon.ENvidiaVideoDecoderProps_ConfigTypes.NVVIDDEC_CONFIG_DEINTERLACE_MODE, nvcommon.ENVVIDDEC_CONFIG_DEINTERLACE_MODE.NVVIDDEC_CONFIG_DEINTERLACE_FILTERED_WEAVE)
    '    End If
    'End Sub

    'Private Sub rbDEINT_Control_Auto_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbDEINT_Control_Auto.CheckedChanged
    '    If CType(sender, RadioButton).Checked Then
    '        PP.Player.Graph.nvVideoAtts.SetLong(nvcommon.ENvVideoDecoderProps.NVVIDDEC_CONFIG, nvcommon.ENvidiaVideoDecoderProps_ConfigTypes.NVVIDDEC_CONFIG_DEINTERLACE_CONTROL, nvcommon.ENVVIDDEC_CONFIG_DEINTERLACE_MODE_CTRL.NVVIDDEC_CONFIG_DEINTERLACE_CTRL_AUTO)
    '    End If
    'End Sub

    'Private Sub rbDEINT_Control_Smart_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbDEINT_Control_Smart.CheckedChanged
    '    If CType(sender, RadioButton).Checked Then
    '        PP.Player.Graph.nvVideoAtts.SetLong(nvcommon.ENvVideoDecoderProps.NVVIDDEC_CONFIG, nvcommon.ENvidiaVideoDecoderProps_ConfigTypes.NVVIDDEC_CONFIG_DEINTERLACE_CONTROL, nvcommon.ENVVIDDEC_CONFIG_DEINTERLACE_MODE_CTRL.NVVIDDEC_CONFIG_DEINTERLACE_CTRL_SMART)
    '    End If
    'End Sub

    'Private Sub rbDEINT_Control_Film_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbDEINT_Control_Film.CheckedChanged
    '    If CType(sender, RadioButton).Checked Then
    '        PP.Player.Graph.nvVideoAtts.SetLong(nvcommon.ENvVideoDecoderProps.NVVIDDEC_CONFIG, nvcommon.ENvidiaVideoDecoderProps_ConfigTypes.NVVIDDEC_CONFIG_DEINTERLACE_CONTROL, nvcommon.ENVVIDDEC_CONFIG_DEINTERLACE_MODE_CTRL.NVVIDDEC_CONFIG_DEINTERLACE_CTRL_FILM)
    '    End If
    'End Sub

    'Private Sub rbDEINT_Control_Video_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbDEINT_Control_Video.CheckedChanged
    '    If CType(sender, RadioButton).Checked Then
    '        PP.Player.Graph.nvVideoAtts.SetLong(nvcommon.ENvVideoDecoderProps.NVVIDDEC_CONFIG, nvcommon.ENvidiaVideoDecoderProps_ConfigTypes.NVVIDDEC_CONFIG_DEINTERLACE_CONTROL, nvcommon.ENVVIDDEC_CONFIG_DEINTERLACE_MODE_CTRL.NVVIDDEC_CONFIG_DEINTERLACE_CTRL_VIDEO)
    '    End If
    'End Sub

    'Private Sub btnDEINT_CheckStatus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDEINT_CheckStatus.Click
    '    Dim i As nvcommon.ENVVIDDEC_CONFIG_DEINTERLACE_MODE
    '    PP.Player.Graph.nvVideoAtts.GetLong(nvcommon.ENvVideoDecoderProps.NVVIDDEC_CONFIG, nvcommon.ENvidiaVideoDecoderProps_ConfigTypes.NVVIDDEC_CONFIG_DEINTERLACE_MODE, i)
    '    PP.AddConsoleLine(eConsoleItemType.NOTICE, "deinterlace mode: " & i.ToString)

    '    Dim ii As nvcommon.ENVVIDDEC_CONFIG_DEINTERLACE_MODE_CTRL
    '    PP.Player.Graph.nvVideoAtts.GetLong(nvcommon.ENvVideoDecoderProps.NVVIDDEC_CONFIG, nvcommon.ENvidiaVideoDecoderProps_ConfigTypes.NVVIDDEC_CONFIG_DEINTERLACE_CONTROL, ii)
    '    PP.AddConsoleLine(eConsoleItemType.NOTICE, "deinterlace control: " & ii.ToString)
    'End Sub

#End Region 'Deinterlacing

#Region "Video Mode"

    Private ManualMPEGFrameModeSet As Boolean = False
    Private Sub SetCurrentMPEGFrameMode()
        Me.ManualMPEGFrameModeSet = True
        Select Case PP.Player.CurrentMPEGFrameMode
            Case 1
                Me.rbVidMode_IBP.Checked = True
            Case 2
                Me.rbVidMode_IP.Checked = True
            Case 3
                Me.rbVidMode_I.Checked = True
        End Select
        Me.ManualMPEGFrameModeSet = False
    End Sub

    Private Sub rbVidMode_IBP_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbVidMode_IBP.CheckedChanged
        If Not PP.FeatureManagement.Features.FE_VA_FrameFiltering Then Exit Sub
        If Me.ManualMPEGFrameModeSet = True Then Exit Sub
        If CType(sender, RadioButton).Checked Then PP.Player.SetMPEGFrameMode(1)
    End Sub

    Private Sub rbVidMode_IP_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbVidMode_IP.CheckedChanged
        If Not PP.FeatureManagement.Features.FE_VA_FrameFiltering Then Exit Sub
        If Me.ManualMPEGFrameModeSet = True Then Exit Sub
        If CType(sender, RadioButton).Checked Then PP.Player.SetMPEGFrameMode(2)
    End Sub

    Private Sub rbVidMode_I_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbVidMode_I.CheckedChanged
        If Not PP.FeatureManagement.Features.FE_VA_FrameFiltering Then Exit Sub
        If Me.ManualMPEGFrameModeSet = True Then Exit Sub
        If CType(sender, RadioButton).Checked Then PP.Player.SetMPEGFrameMode(3)
    End Sub

#End Region 'Video Mode

    Private Sub cbSplitFields_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbSplitFields.CheckedChanged
        If FormIsLoading Then Exit Sub
        If Not PP.FeatureManagement.Features.FE_VA_FieldSplitting Then Exit Sub
        PP.Player.FieldSplitting = Me.cbSplitFields.Checked
    End Sub

    Private Sub cbVID_ReverseFieldOrder_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbVID_ReverseFieldOrder.CheckedChanged
        If FormIsLoading Then Exit Sub
        If Not PP.FeatureManagement.Features.FE_VA_ReverseFieldOrder Then Exit Sub

        PP.Player.ReverseFieldOrder = Me.cbVID_ReverseFieldOrder.Checked
    End Sub

    Private Sub cbVID_NonSeamlessCellNotification_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbVID_NonSeamlessCellNotification.CheckedChanged
        If FormIsLoading Then Exit Sub
        If PP.Player IsNot Nothing Then
            PP.Player.NonSeamlessCellNotification = cbVID_NonSeamlessCellNotification.Checked
        End If
        My.Settings.DVD_NOTIFY_NONSEAMLESSCELL = Me.cbVID_NonSeamlessCellNotification.Checked
        My.Settings.Save()
    End Sub

#End Region 'VIDEO

#Region "AUDIO"

#Region "Time Delta"

    Private Sub AU_tbTimeDelta_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AU_tbTimeDelta.Scroll
        Me.AU_txtTimeDeltaVal.Text = Me.AU_tbTimeDelta.Value
    End Sub

    Private Sub AU_txtTimeDeltaVal_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AU_txtTimeDeltaVal.TextChanged
        If Not IsNumeric(Me.AU_txtTimeDeltaVal.Text) Then Exit Sub
        If Me.AU_txtTimeDeltaVal.Text > 1000 Or Me.AU_txtTimeDeltaVal.Text < -1000 Then
            PP.AddConsoleLine(eConsoleItemType.NOTICE, "Invalid video time delta value. Must be between -1000 and 1000.")
            Me.AU_txtTimeDeltaVal.Text = 0
            Exit Sub
        End If
        Me.AU_tbTimeDelta.Value = Me.AU_txtTimeDeltaVal.Text
    End Sub

#End Region 'Time Delta

    Private Sub cbDumpAudio_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Not PP.FeatureManagement.Features.FE_AD_Dumping Then Exit Sub
        PP.Player.DumpAudio = cbDumpAudio.Checked
    End Sub

#End Region 'AUDIO

#Region "SUBPICTURE"

    Private Sub Form_Load_Subpicture(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.gbSUB_Positioning.Enabled = PP.FeatureManagement.Features.FE_SP_Positioning
        If Not PP.Player.SPPlacementInfo Is Nothing Then
            Me.cbSPPlacementEnabled.Checked = PP.Player.GuideInfo.PlacementEnabled
            Me.txtSPPlacement_X.Text = PP.Player.GuideInfo.X
            Me.txtSPPlacement_Y.Text = PP.Player.GuideInfo.Y
        End If
    End Sub

    Private Sub Form_Closed_Subpicture(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Dim GI As New cGuidePlacementInfo
        GI.X = Me.txtSPPlacement_X.Text
        GI.Y = Me.txtSPPlacement_Y.Text
        GI.PlacementEnabled = Me.cbSPPlacementEnabled.Checked
        PP.Player.SPPlacementInfo = GI
        PP.UserOperationTemplates.Save()
        PP.KH.UnpauseHook()
    End Sub

#Region "Placement"

    Private Sub tbSPPlacement_X_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbSPPlacement_X.Scroll
        Me.txtSPPlacement_X.Text = Me.tbSPPlacement_X.Value
    End Sub

    Private Sub tbSPPlacement_Y_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbSPPlacement_Y.Scroll
        Me.txtSPPlacement_Y.Text = Me.tbSPPlacement_Y.Value
    End Sub

    Private Sub txtSPPlacement_X_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSPPlacement_X.TextChanged
        If PP Is Nothing Then Exit Sub
        If Not IsNumeric(Me.txtSPPlacement_X.Text) Then Exit Sub
        If Me.txtSPPlacement_X.Text > 720 Or Me.txtSPPlacement_X.Text < -720 Then Me.txtSPPlacement_X.Text = 0
        Me.tbSPPlacement_X.Value = Me.txtSPPlacement_X.Text
        SetSPPlacement()
    End Sub

    Private Sub txtSPPlacement_Y_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSPPlacement_Y.TextChanged
        If PP Is Nothing Then Exit Sub
        If Not IsNumeric(Me.txtSPPlacement_Y.Text) Then Exit Sub
        If Me.txtSPPlacement_Y.Text > 576 Or Me.txtSPPlacement_Y.Text < -576 Then Me.txtSPPlacement_Y.Text = 0
        Me.tbSPPlacement_Y.Value = Me.txtSPPlacement_Y.Text
        SetSPPlacement()
    End Sub

    Private Sub SetSPPlacement()
        If Me.cbSPPlacementEnabled.Checked And PP.FeatureManagement.Features.FE_SP_Positioning Then
            PP.Player.SetSubpicturePlacement(Me.txtSPPlacement_X.Text, Me.txtSPPlacement_Y.Text)
        Else
            PP.Player.SetSubpicturePlacement(0, 0)
        End If
    End Sub

    Private Sub cbSPPlacementEnabled_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbSPPlacementEnabled.CheckedChanged
        SetSPPlacement()
    End Sub

#End Region 'Placement

    Private Sub SPUpDownKeys(ByVal Sender As TextBox, ByVal Up As Boolean)
        Try
            If Up Then
                Sender.Text += 1
            Else
                Sender.Text -= 1
            End If
        Catch ex As Exception
            PP.AddConsoleLine(eConsoleItemType.ERROR, "Problem with UpDownKeys in Extended Options window. Error: " & ex.Message, Nothing)
        End Try
    End Sub

    Private Sub txtSPPlacement_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles txtSPPlacement_X.KeyDown, txtSPPlacement_Y.KeyDown
        Try
            Dim Up As Boolean
            If e.KeyData = Keys.Up Or e.KeyData = Keys.Right Then
                Up = True
            ElseIf e.KeyData = Keys.Down Or e.KeyData = Keys.Left Then
                Up = False
            Else
                Exit Sub
            End If
            Debug.WriteLine("txtB keyup: " & e.KeyData.ToString)
            Dim tb As TextBox = CType(sender, TextBox)
            Me.UpDownKeys(tb, Up)
        Catch ex As Exception
            'do nothing
        End Try
    End Sub

#End Region 'SUBPICTURE

#Region "CLOSED CAPTIONS"

    Private Sub Form_Load_L21(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        gcLine21Placement.Enabled = PP.FeatureManagement.Features.FE_L12_Positioning
    End Sub

    Private Sub tbPlacement_X_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbClosedCaptionPlacement_X.Scroll
        Me.txtCCPlacementX.Text = Me.tbClosedCaptionPlacement_X.Value
    End Sub

    Private Sub tbPlacement_Y_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbClosedCaptionPlacement_Y.Scroll
        Me.txtCCPlacementY.Text = Me.tbClosedCaptionPlacement_Y.Value
    End Sub

    Private Sub txtPlacement_X_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCCPlacementX.TextChanged
        If PP Is Nothing Then Exit Sub
        If Not IsNumeric(Me.txtCCPlacementX.Text) Then Exit Sub
        If Me.txtCCPlacementX.Text > 720 Or Me.txtCCPlacementX.Text < -720 Then Me.txtCCPlacementX.Text = 0
        Me.tbClosedCaptionPlacement_X.Value = Me.txtCCPlacementX.Text
        SetCCPlacement()
    End Sub

    Private Sub txtPlacement_Y_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCCPlacementY.TextChanged
        If PP Is Nothing Then Exit Sub
        If Not IsNumeric(Me.txtSPPlacement_Y.Text) Then Exit Sub
        If Me.txtCCPlacementY.Text > 576 Or Me.txtCCPlacementY.Text < -576 Then Me.txtCCPlacementY.Text = 0
        Me.tbClosedCaptionPlacement_Y.Value = Me.txtCCPlacementY.Text
        SetCCPlacement()
    End Sub

    Private Sub SetCCPlacement()
        If Not PP.FeatureManagement.Features.FE_L12_Positioning Then Exit Sub
        If Me.cbClosedCaptionPlacement.Checked Then
            PP.Player.SetClosedCaptionPlacement(Me.txtCCPlacementX.Text, Me.txtCCPlacementY.Text)
        Else
            PP.Player.SetClosedCaptionPlacement(40, 0) 'default is 40,0
        End If
    End Sub

    Private Sub cbPlacementEnabled_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbClosedCaptionPlacement.CheckedChanged
        SetCCPlacement()
    End Sub

    Private Sub CCUpDownKeys(ByVal Sender As TextBox, ByVal Up As Boolean)
        Try
            If Up Then
                Sender.Text += 1
            Else
                Sender.Text -= 1
            End If
        Catch ex As Exception
            PP.AddConsoleLine(eConsoleItemType.ERROR, "Problem with UpDownKeys in Extended Options window. Error: " & ex.Message, Nothing)
        End Try
    End Sub

    Private Sub txtCCPlacement_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles txtCCPlacementX.KeyDown, txtCCPlacementY.KeyDown
        Try
            Dim Up As Boolean
            If e.KeyData = Keys.Up Or e.KeyData = Keys.Right Then
                Up = True
            ElseIf e.KeyData = Keys.Down Or e.KeyData = Keys.Left Then
                Up = False
            Else
                Exit Sub
            End If
            Debug.WriteLine("txtB keyup: " & e.KeyData.ToString)
            Dim tb As TextBox = CType(sender, TextBox)
            Me.UpDownKeys(tb, Up)
        Catch ex As Exception
            'do nothing
        End Try
    End Sub

#End Region 'CLOSED CAPTIONS

#Region "UOP TEMPLATES"

    Private Sub Form_Load_UOPTemplates(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        tpUOPTemplates.PageEnabled = PP.FeatureManagement.Features.FE_DVD_UOPTemplates
    End Sub

    Private _UOPTemplates_Enabled As Boolean = False

    Private Property UOPTemplates_CurrentIndex() As Short
        Get
            If Me.lvUOPT_ExistingTemplates.Items.Count = 0 Then
                Return -1
            ElseIf Me.lvUOPT_ExistingTemplates.SelectedItems.Count = 1 Then
                Return Me.lvUOPT_ExistingTemplates.SelectedIndex
            ElseIf _UOPTemplates_CurrentIndex <> -1 Then
                Return _UOPTemplates_CurrentIndex
            Else
                Return 0
            End If
        End Get
        Set(ByVal value As Short)
            _UOPTemplates_CurrentIndex = value
        End Set
    End Property
    Private _UOPTemplates_CurrentIndex As Short = -1

    Private Sub UOPTemplates_Load()
        Try
            If PP.UserOperationTemplates Is Nothing Then Exit Sub
            If PP.UserOperationTemplates.Templates Is Nothing Then Exit Sub
            Me.lvUOPT_ExistingTemplates.Items.Clear()
            For Each t As cUserOperationTemplate In PP.UserOperationTemplates.Templates
                Me.lvUOPT_ExistingTemplates.Items.Add(t.Name)
            Next
            If Me.lvUOPT_ExistingTemplates.Items.Count > 0 Then
                Me.cbUOPT_EnableSelectedTemplate.Enabled = True
            Else
                Me.cbUOPT_EnableSelectedTemplate.Enabled = False
            End If
        Catch ex As Exception
            Throw New Exception("Problem with UOPTemplates_Load(). Error: " & ex.Message, ex)
        End Try
    End Sub

    Private Sub UOPTemplate_SaveNew()
        Try
            If PP.UserOperationTemplates Is Nothing Then Exit Sub
            If PP.UserOperationTemplates.Templates Is Nothing Then Exit Sub
            Dim out As cUserOperationTemplate = Me.UOPTemplate_GetCurrentCheckBoxValues
            Dim name As String = InputBox("Enter a name for the new template:", My.Settings.APPLICATION_NAME, "")
            If name = "" Then Exit Sub
            out.Name = name
            PP.UserOperationTemplates.Add(out)
            Me.UOPTemplates_Load()
            PP.UserOperationTemplates.Save()
        Catch ex As Exception
            Throw New Exception("Problem with UOPTemplates_Load(). Error: " & ex.Message, ex)
        End Try
    End Sub

    Private Sub UOPTemplate_SaveExisting(ByVal index As Integer)
        Try
            If PP.UserOperationTemplates Is Nothing Then Exit Sub
            If PP.UserOperationTemplates.Templates Is Nothing Then Exit Sub
            Dim out As cUserOperationTemplate = Me.UOPTemplate_GetCurrentCheckBoxValues
            out.Name = Me.lvUOPT_ExistingTemplates.Items(index).ToString
            PP.UserOperationTemplates.Update(index, out)
            Me.UOPTemplates_Load()
            PP.UserOperationTemplates.Save()
        Catch ex As Exception
            Throw New Exception("Problem with UOPTemplate_SaveExisting(). Error: " & ex.Message, ex)
        End Try
    End Sub

    Private Sub UOPTemplate_Delete(ByVal index As Integer)
        Try
            If PP.UserOperationTemplates Is Nothing Then Exit Sub
            If PP.UserOperationTemplates.Templates Is Nothing Then Exit Sub
            PP.UserOperationTemplates.Delete(index)
            Me.UOPTemplates_Load()
        Catch ex As Exception
            Throw New Exception("Problem with UOPTemplate_SaveExisting(). Error: " & ex.Message, ex)
        End Try
    End Sub

    Private Sub UOPTemplate_Rename(ByVal index As Integer, ByVal NewName As String)
        Try
            If NewName = "" Then Exit Sub
            If PP.UserOperationTemplates Is Nothing Then Exit Sub
            If PP.UserOperationTemplates.Templates Is Nothing Then Exit Sub
            PP.UserOperationTemplates.Rename(index, NewName)
            Me.UOPTemplates_Load()
        Catch ex As Exception
            Throw New Exception("Problem with UOPTemplate_SaveExisting(). Error: " & ex.Message, ex)
        End Try
    End Sub

    Private Sub UOPTemplate_Select(ByVal index As Integer)
        Try
            If PP.UserOperationTemplates Is Nothing Then Exit Sub
            If PP.UserOperationTemplates.Templates Is Nothing Then Exit Sub
            With PP.UserOperationTemplates.Templates(index).UOPs
                Me.cbUO_0.Checked = ._0_Time_Title_Play
                Me.cbUO_1.Checked = ._1_Chapter_Play
                Me.cbUO_2.Checked = ._2_Title_Play
                Me.cbUO_3.Checked = ._3_Stop
                Me.cbUO_4.Checked = ._4_GoUp
                Me.cbUO_5.Checked = ._5_Time_Chapter_Search
                Me.cbUO_6.Checked = ._6_Chapter_Back
                Me.cbUO_7.Checked = ._7_Chapter_Next
                Me.cbUO_8.Checked = ._8_Fast_Forward
                Me.cbUO_9.Checked = ._9_Rewind
                Me.cbUO_10.Checked = ._10_Title_Menu
                Me.cbUO_11.Checked = ._11_Root_Menu
                Me.cbUO_12.Checked = ._12_Subtitle_Menu
                Me.cbUO_13.Checked = ._13_Audio_Menu
                Me.cbUO_14.Checked = ._14_Angle_Menu
                Me.cbUO_15.Checked = ._15_Chapter_Menu
                Me.cbUO_16.Checked = ._16_Resume
                Me.cbUO_17.Checked = ._17_Button_Select_Activate
                Me.cbUO_18.Checked = ._18_Start_From_Still
                Me.cbUO_19.Checked = ._19_Pause_Menu_Lang_Select
                Me.cbUO_20.Checked = ._20_Change_Audio_Stream
                Me.cbUO_21.Checked = ._21_Change_Subtitle_Stream
                Me.cbUO_22.Checked = ._22_Angle_Change_Parental_Management_Level_Change
                Me.cbUO_23.Checked = ._23_Karaoke_Mode_Change
                Me.cbUO_24.Checked = ._24_Video_Mode_Change
            End With
        Catch ex As Exception
            Throw New Exception("Problem with UOPTemplate_Select(). Error: " & ex.Message, ex)
        End Try
    End Sub

    Private Sub UOPTemplate_Apply(ByVal index As Integer)
        If Not PP.FeatureManagement.Features.FE_DVD_UOPTemplates Then Exit Sub
        PP.UserOperationTemplates.CurrentIndex = index
    End Sub

    Private Sub UOPTemplate_Disable()
        PP.UserOperationTemplates.CurrentIndex = -1
    End Sub

    Private Function UOPTemplate_GetCurrentCheckBoxValues() As cUserOperationTemplate
        Try
            Dim out As New cUserOperationTemplate
            With out.UOPs
                ._0_Time_Title_Play = Me.cbUO_0.Checked
                ._1_Chapter_Play = Me.cbUO_1.Checked
                ._2_Title_Play = Me.cbUO_2.Checked
                ._3_Stop = Me.cbUO_3.Checked
                ._4_GoUp = Me.cbUO_4.Checked
                ._5_Time_Chapter_Search = Me.cbUO_5.Checked
                ._6_Chapter_Back = Me.cbUO_6.Checked
                ._7_Chapter_Next = Me.cbUO_7.Checked
                ._8_Fast_Forward = Me.cbUO_8.Checked
                ._9_Rewind = Me.cbUO_9.Checked
                ._10_Title_Menu = Me.cbUO_10.Checked
                ._11_Root_Menu = Me.cbUO_11.Checked
                ._12_Subtitle_Menu = Me.cbUO_12.Checked
                ._13_Audio_Menu = Me.cbUO_13.Checked
                ._14_Angle_Menu = Me.cbUO_14.Checked
                ._15_Chapter_Menu = Me.cbUO_15.Checked
                ._16_Resume = Me.cbUO_16.Checked
                ._17_Button_Select_Activate = Me.cbUO_17.Checked
                ._18_Start_From_Still = Me.cbUO_18.Checked
                ._19_Pause_Menu_Lang_Select = Me.cbUO_19.Checked
                ._20_Change_Audio_Stream = Me.cbUO_20.Checked
                ._21_Change_Subtitle_Stream = Me.cbUO_21.Checked
                ._22_Angle_Change_Parental_Management_Level_Change = Me.cbUO_22.Checked
                ._23_Karaoke_Mode_Change = Me.cbUO_23.Checked
                ._24_Video_Mode_Change = Me.cbUO_24.Checked
            End With
            Return out
        Catch ex As Exception
            Throw New Exception("Probelm with UOPTemplate_GetCurrent(). Error: " & ex.Message, ex)
        End Try
    End Function

#Region "UOP TEMPLATES:EVENTS"

    Private Sub UOPTemplates_FormLoad(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        UOPTemplates_Load()
    End Sub

    Private Sub cbUOPT_EnableSelectedTemplate_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbUOPT_EnableSelectedTemplate.CheckedChanged
        If Me.cbUOPT_EnableSelectedTemplate.Checked Then
            UOPTemplate_Apply(UOPTemplates_CurrentIndex)
            _UOPTemplates_Enabled = True
        Else
            UOPTemplate_Disable()
            _UOPTemplates_Enabled = False
        End If
    End Sub

    Private Sub btnUOPT_DeleteSelectedTemplate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUOPT_DeleteSelectedTemplate.Click
        If Me.lvUOPT_ExistingTemplates.Items.Count < 1 Then Exit Sub
        UOPTemplate_Delete(UOPTemplates_CurrentIndex)
    End Sub

    Private Sub btnUOPT_RenameSelectedTemplate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUOPT_RenameSelectedTemplate.Click
        If Me.lvUOPT_ExistingTemplates.Items.Count < 1 Then Exit Sub
        If Me.lvUOPT_ExistingTemplates.Items.Count < 1 Then Exit Sub
        Dim s As String = InputBox("Enter new name for template:", , "")
        UOPTemplate_Rename(UOPTemplates_CurrentIndex, s)
    End Sub

    Private Sub btnUOPT_SaveNewTemplate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUOPT_SaveNewTemplate.Click
        UOPTemplate_SaveNew()
    End Sub

    Private Sub btnUOPT_SaveSelectedTemplate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUOPT_SaveSelectedTemplate.Click
        If Me.lvUOPT_ExistingTemplates.Items.Count < 1 Then Exit Sub
        UOPTemplate_SaveExisting(UOPTemplates_CurrentIndex)
    End Sub

    Private Sub lvUOPT_ExistingTemplates_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvUOPT_ExistingTemplates.SelectedIndexChanged
        UOPTemplates_CurrentIndex = Me.lvUOPT_ExistingTemplates.SelectedIndex
        UOPTemplate_Select(UOPTemplates_CurrentIndex)
        If _UOPTemplates_Enabled Then
            UOPTemplate_Apply(UOPTemplates_CurrentIndex)
        End If
    End Sub

#End Region 'UOP TEMPLATES:EVENTS

#End Region 'UOP TEMPLATES

#Region "UI OPTIONS"

    Public Sub Form_Load_Options(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.ceUseCheckboxesForUOPs.Checked = PP.CurrentUserProfile.AppOptions.UseCheckboxesInUI
        Me.rbRootResume_SeparateButtons.Checked = PP.CurrentUserProfile.AppOptions.UseSeparateResumeButton
    End Sub

#Region "UI OPTIONS:ROOT/RESUME"

    Private Sub rbRootResume_SeparateButtons_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbRootResume_SeparateButtons.CheckedChanged
        PP.CurrentUserProfile.AppOptions.UseSeparateResumeButton = rbRootResume_SeparateButtons.Checked
    End Sub

#End Region 'UI OPTIONS:ROOT/RESUME

#Region "UI OPTIONS:UOPs"

    Private Sub ceUseCheckboxesForUOPs_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ceUseCheckboxesForUOPs.CheckedChanged
        PP.CurrentUserProfile.AppOptions.UseCheckboxesInUI = ceUseCheckboxesForUOPs.Checked
    End Sub

#End Region 'UI OPTIONS:UOPs

#End Region 'UI OPTIONS

#Region "ADMIN"

    Private Sub btnADMIN_SaveCurrentLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnADMIN_SaveCurrentLayout.Click
        Try
            Dim outpath As String = "C:\StandardUILayout.xml"
            If File.Exists(outpath) Then File.Delete(outpath)
            Dim FS As New FileStream(outpath, FileMode.OpenOrCreate)
            PP.barMain.SaveLayoutToStream(FS)
            FS.Close()
        Catch ex As Exception
            MsgBox("Problem saving layout. Error: " & ex.Message)
        End Try
    End Sub

#Region "Property Pages"

    Public PGs As DsCAUUID
    Private Sub btnProperties_vid_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProperties_vid.Click
        Try
            PP.Player.Graph.VideoDecoder_PP.GetPages(PGs)
            Dim FI As New FilterInfo
            DsUtils.OleCreatePropertyFrame(PP.Handle, 0, 0, FI.achName, 1, PP.Player.Graph.VSDecoder, PGs.cElems, PGs.pElems, 0, 0, Nothing)
        Catch ex As Exception
            PP.AddConsoleLine(eConsoleItemType.ERROR, "Problem getting video decoder properties. error: " & ex.Message)
        End Try
    End Sub

    Private Sub btnProperties_vr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProperties_vr.Click
        Try
            PP.Player.Graph.VideoRenderer_PP.GetPages(PGs)
            Dim FI As New FilterInfo
            DsUtils.OleCreatePropertyFrame(PP.Handle, 0, 0, FI.achName, 1, PP.Player.Graph.DLVideo, PGs.cElems, PGs.pElems, 0, 0, Nothing)
        Catch ex As Exception
            PP.AddConsoleLine(eConsoleItemType.WARNING, "The item selected does not currently have property pages.")
        End Try
    End Sub

    Private Sub btnProperties_aud_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProperties_aud.Click
        Try
            PP.Player.Graph.AudioDecoder_PP.GetPages(PGs)
            Dim FI As New FilterInfo
            'AudioDecoder.QueryFilterInfo(FI)
            DsUtils.OleCreatePropertyFrame(PP.Handle, 0, 0, FI.achName, 1, PP.Player.Graph.AudioDecoder, PGs.cElems, PGs.pElems, 0, 0, Nothing)
        Catch ex As Exception
            PP.AddConsoleLine(eConsoleItemType.ERROR, "Problem getting video decoder properties. error: " & ex.Message)
        End Try
    End Sub

    Private Sub btnProperties_ar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProperties_ar.Click
        Try
            PP.Player.Graph.AudioRenderer_PP.GetPages(PGs)
            Dim FI As New FilterInfo
            DsUtils.OleCreatePropertyFrame(PP.Handle, 0, 0, FI.achName, 1, PP.Player.Graph.AudioRenderer, PGs.cElems, PGs.pElems, 0, 0, Nothing)
        Catch ex As Exception
            PP.AddConsoleLine(eConsoleItemType.ERROR, "Problem getting properties. error: " & ex.Message)
        End Try
    End Sub

    Private Sub btnKeystonePP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKeystonePP.Click
        Try
            PP.Player.Graph.KO_PP.GetPages(PGs)
            Dim FI As New FilterInfo
            DsUtils.OleCreatePropertyFrame(PP.Handle, 0, 0, FI.achName, 1, PP.Player.Graph.KeystoneOMNI, PGs.cElems, PGs.pElems, 0, 0, Nothing)
        Catch ex As Exception
            PP.AddConsoleLine(eConsoleItemType.ERROR, "Problem getting properties. error: " & ex.Message)
        End Try
    End Sub

#End Region 'Property Pages

#Region "Debugging"

    Private Sub btnDebugging_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDebugging.Click
        Try
            PP.Player.Graph.Dint_pp.GetPages(PGs)
            Dim FI As New FilterInfo
            DsUtils.OleCreatePropertyFrame(PP.Handle, 0, 0, FI.achName, 1, PP.Player.Graph.Dint, PGs.cElems, PGs.pElems, 0, 0, Nothing)
        Catch ex As Exception
            PP.AddConsoleLine(eConsoleItemType.ERROR, "Problem getting video decoder properties. error: " & ex.Message)
        End Try
    End Sub

    Private Sub btnGetVidWin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim h, w, t, l As Integer
            'PP.VideoWin.get_Height(h)
            'PP.VideoWin.get_Width(w)
            'PP.VideoWin.get_Left(l)
            'PP.VideoWin.get_Top(t)
            MsgBox("h:" & h & " w:" & w & " l:" & l & " t:" & t)
        Catch ex As Exception
            PP.AddConsoleLine(eConsoleItemType.ERROR, "Problem getting video win. Error: " & ex.Message)
        End Try
    End Sub

    Private Sub btnGetDefMenuLang_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetDefMenuLang.Click
        Try
            PP.AddConsoleLine(eConsoleItemType.NOTICE, "Default menu language: " & PP.Player.DefaultMenuLanguage.ToString)
        Catch ex As Exception
            PP.AddConsoleLine(eConsoleItemType.ERROR, "Problem getting default menu language. Error: " & ex.Message)
        End Try
    End Sub

    Private Sub btnGetDefAudLang_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetDefAudLang.Click
        Try
            PP.AddConsoleLine(eConsoleItemType.NOTICE, "Default audio language: " & PP.Player.DefaultAudioLanguage.ToString & " " & PP.Player.DefaultAudioExtension.ToString)
        Catch ex As Exception
            PP.AddConsoleLine(eConsoleItemType.ERROR, "Problem getting default audio language. Error: " & ex.Message)
        End Try
    End Sub

    Private Sub btnGetDefSubLang_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetDefSubLang.Click
        Try
            PP.AddConsoleLine(eConsoleItemType.NOTICE, "Default subtitle language: " & PP.Player.DefaultSubtitleLanguage.ToString & " " & PP.Player.DefaultSubtitleExtension.ToString)
        Catch ex As Exception
            PP.AddConsoleLine(eConsoleItemType.ERROR, "Problem getting default subtitle language. Error: " & ex.Message)
        End Try
    End Sub

    'Private Sub btnGetPlayerPL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetPlayerPL.Click
    '    Try
    '        Dim BufferSize As Integer
    '        Dim PL As Integer
    '        Dim b(1) As Byte
    '        Dim handle As GCHandle = GCHandle.Alloc(b, GCHandleType.Pinned)
    '        Dim ptrBuffer As IntPtr = handle.AddrOfPinnedObject()
    '        HR = PP.DVDInfo.GetPlayerParentalLevel(PL, ptrBuffer)
    '        If HR < 0 Then Marshal.ThrowExceptionForHR(HR)
    '        b = RemoveExtraBytesFromArray(b, False)
    '        Dim Out As String = System.Text.Encoding.ASCII.GetString(b)
    '        If Out.ToString.Length = 0 Then Out = "Null"
    '        Dim G As New cCountries(PP.GetExePath & "Countries.csv")
    '        Out = G.GetCountryFromAlpha(Out)
    '        If PL = -1 Then
    '            Out &= " - Off"
    '        Else
    '            Out &= " - " & PL
    '        End If
    '        PP.AddConsoleLine("Player Parental Level: " & Out)
    '    Catch ex As Exception
    '        PP.AddConsoleLine(eConsoleItemType.ERROR, "Problem getting player parental level. Error: " & ex.Message)
    '    End Try
    'End Sub

    Private Sub btnGetTitlePL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetTitlePL.Click
        Try
            PP.AddConsoleLine(eConsoleItemType.NOTICE, "Title Parental Level: " & PP.Player.ParentalLevelAndCountry)
        Catch ex As Exception
            PP.AddConsoleLine(eConsoleItemType.ERROR, "Problem getting title parental level. Error: " & ex.Message)
        End Try
    End Sub

    Private Declare Auto Function GetVolumeInformation Lib "Kernel32" (ByVal RootPathName As String) As System.IntPtr

    Private Sub btnProcAmpScrap_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            'Dim H, W As Integer
            'HR = PP.Player.Graph.BasicVideo.GetVideoSize(W, H)
            'If HR < 0 Then Marshal.ThrowExceptionForHR(HR)
            'Exit Sub

            'Dim rect As New DirectShow.VMR9.VMR9NormalizedRect
            'HR = PP.SinkVMRMixerControl.GetOutputRect(0, rect)
            'If HR < 0 Then Marshal.ThrowExceptionForHR(HR)

            'HR = PP.SinkVMRMixerControl.GetOutputRect(1, rect)
            'If HR < 0 Then Marshal.ThrowExceptionForHR(HR)

            'HR = PP.SinkVMRMixerControl.GetOutputRect(2, rect)
            'If HR < 0 Then Marshal.ThrowExceptionForHR(HR)

            Exit Sub

            'HR = PP.DVDCtrl.Stop
            'If HR < 0 Then Marshal.ThrowExceptionForHR(HR)

            'HR = PP.DVDCtrl.SetOption(DirectShow.Dvd.DvdOptionFlag.ResetOnStop, True)
            'If HR < 0 Then Marshal.ThrowExceptionForHR(HR)

            'HR = PP.DVDCtrl.SetDVDDirectory("d:\video_ts")
            'If HR < 0 Then Marshal.ThrowExceptionForHR(HR)

            'PP._SystemJPPath = "C:\Documents and Settings\Emulator User\Desktop\asdf_sysjp"
            'PP.DisplaySystemJP("scrap")

            'MsgBox(Me.Height & " " & Me.Width)
            'PP.AddConsoleLine("Volume Label for drive D: " & PP.GetVolumeLabel("D"))
        Catch ex As Exception
            PP.AddConsoleLine(eConsoleItemType.ERROR, "Problem with scrap button. Error: " & ex.Message)
        End Try
    End Sub

    Private Sub btnGetProcAmp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim PAC As New VMR9ProcAmpControl
            PAC.dwSize = Convert.ToUInt32(Marshal.SizeOf(PAC))
            HR = PP.Player.Graph.VMRMixerControl.GetProcAmpControl(0, PAC)
            If HR < 0 Then Marshal.ThrowExceptionForHR(HR)
        Catch ex As Exception
            PP.AddConsoleLine(eConsoleItemType.ERROR, "Problem with getprocamp. Error: " & ex.Message)
        End Try
    End Sub

    Private Sub btnProcAmpRange_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim PACR As New VMR9ProcAmpControlRange
            PACR.dwSize = Convert.ToUInt32(Marshal.SizeOf(PACR))
            PACR.dwProperty = 1 '1=brightness, 2=contrast, 4=hue, 8=saturation
            HR = PP.Player.Graph.VMRMixerControl.GetProcAmpControlRange(0, PACR)
            If HR < 0 Then Marshal.ThrowExceptionForHR(HR)
        Catch ex As Exception

        End Try
    End Sub

    'Private Sub btnAspectRatio_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAspectRatio.Click
    '    Dim f As New frmAspectRatio(DB)
    '    f.Show()
    '    f.Visible = True
    'End Sub

    Private Sub btnAspectRatio_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAspectRatio.Click
        Try
            'PP.Player.Graph.DVDInfo.getpl()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnGetVidWin_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetVidWin.Click

    End Sub

    Private Sub btnScrap_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVideoScrap.Click

    End Sub

    Private Sub btnGetProcAmp_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetProcAmp.Click

    End Sub

    Private Sub btnProcAmpRange_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProcAmpRange.Click

    End Sub

#End Region 'Debugging

#End Region 'ADMIN

#Region "TIMERS"

    Private Sub Two_Second_Timer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Two_Second_Timer.Tick
        SetupFrameGrabPaths()
    End Sub

#End Region 'TIEMRS

End Class
