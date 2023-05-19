Imports SMT.Multimedia.Players.DVD.Enums
Imports System.ComponentModel
Imports SMT.Multimedia.Players.DVD.Classes
Imports SMT.Multimedia.DirectShow
Imports SMT.Multimedia.Players.DVD
Imports System.IO
Imports SMT.Multimedia.Formats.DVD.Globalization
Imports SMT.Multimedia.Players.DVD.Structures
Imports SMT.Multimedia.Utility.Timecode
Imports SMT.DotNet.Utility
Imports System.Xml.Serialization
Imports System.Text
Imports SMT.Applications.Phoenix.Enums
Imports SMT.Multimedia.Enums
Imports SMT.DotNet.AppConsole

Namespace Classes

    ''' <summary>
    ''' This class is used only for populating the user option property grid. The values are stored in the user's current profile (cPhoenixUserProfile).
    ''' </summary>
    ''' <remarks></remarks>
    Public Class cEmulatorConfig

        Private Phoenix As Phoenix_Form

        Public Sub New(ByRef nPhoenix As Phoenix_Form)
            Phoenix = nPhoenix

            'Dim Assm As System.Reflection.Assembly = Me.GetType().Assembly.GetEntryAssembly
            'Dim CountriesCSV As Stream = Assm.GetManifestResourceStream("SMT.Applications.Phoenix.Countries.csv")
            'Dim LanguagesCSV As Stream = Assm.GetManifestResourceStream("SMT.Applications.Phoenix.Languages.csv")

            Me.Countries = New cCountries()
            Me.Languages = New cLanguages()

        End Sub

#Region "Player Defaults"

        Public Countries As cCountries
        Public Languages As cLanguages

        <Category("Emulator Configuration"), Browsable(True), [ReadOnly](False), Bindable(False), DefaultValue(""), DesignOnly(False), Description("Select the country for parental management.")> _
        Public Property ParentalCountry() As eCountries
            Get
                Return My.Settings.PLAYER_PARENTAL_COUNTRY
            End Get
            Set(ByVal Value As eCountries)
                If Not Phoenix.FeatureManagement.Features.FE_DVD_ParentalManagement Then Exit Property
                If Phoenix.Player.PlayState <> ePlayState.Stopped And Phoenix.Player.PlayState <> ePlayState.Init And Phoenix.Player.PlayState <> ePlayState.SystemJP Then Exit Property
                My.Settings.PLAYER_PARENTAL_COUNTRY = Value
                My.Settings.Save()
            End Set
        End Property

        <Category("Emulator Configuration"), Browsable(True), [ReadOnly](False), Bindable(False), DefaultValue(""), DesignOnly(False), Description("Select the level for parental management.")> _
        Public Property ParentalLevel() As eParentalLevels
            Get
                Return My.Settings.PLAYER_PARENTAL_LEVEL
            End Get
            Set(ByVal Value As eParentalLevels)
                If Not Phoenix.FeatureManagement.Features.FE_DVD_ParentalManagement Then Exit Property
                If Phoenix.Player.PlayState <> ePlayState.Stopped And Phoenix.Player.PlayState <> ePlayState.Init And Phoenix.Player.PlayState <> ePlayState.SystemJP Then Exit Property
                My.Settings.PLAYER_PARENTAL_LEVEL = Value
                My.Settings.Save()
            End Set
        End Property

        <Category("Emulator Configuration"), Browsable(True), [ReadOnly](False), Bindable(False), DefaultValue(""), DesignOnly(False), Description("Preferred aspect ratio for video playback. Interpolation used for letterbox and panscan.")> _
        Public Property AspectRatio() As ePreferredAspectRatio
            Get
                Return My.Settings.PLAYER_ASPECT_RATIO
            End Get
            Set(ByVal Value As ePreferredAspectRatio)
                If Phoenix.Player.PlayState <> ePlayState.Stopped And Phoenix.Player.PlayState <> ePlayState.Init And Phoenix.Player.PlayState <> ePlayState.SystemJP Then Exit Property
                My.Settings.PLAYER_ASPECT_RATIO = Value
                My.Settings.Save()
            End Set
        End Property

        <Category("Emulator Configuration"), Browsable(True), [ReadOnly](False), Bindable(False), DefaultValue(""), DesignOnly(False), Description("Default menu language.")> _
        Public Property MenuLanguage() As eLanguages
            Get
                Return My.Settings.PLAYER_MENU_LANG
            End Get
            Set(ByVal Value As eLanguages)
                If Phoenix.Player.PlayState <> ePlayState.Stopped And Phoenix.Player.PlayState <> ePlayState.Init And Phoenix.Player.PlayState <> ePlayState.SystemJP Then Exit Property
                My.Settings.PLAYER_MENU_LANG = Value
                My.Settings.Save()
            End Set
        End Property

        <Category("Emulator Configuration"), Browsable(True), [ReadOnly](False), Bindable(False), DefaultValue(""), DesignOnly(False), Description("Default audio language.")> _
        Public Property AudioLanguage() As eLanguages
            Get
                Return My.Settings.PLAYER_AUDIO_LANG
            End Get
            Set(ByVal Value As eLanguages)
                If Phoenix.Player.PlayState <> ePlayState.Stopped And Phoenix.Player.PlayState <> ePlayState.Init And Phoenix.Player.PlayState <> ePlayState.SystemJP Then Exit Property
                My.Settings.PLAYER_AUDIO_LANG = Value
                My.Settings.Save()
            End Set
        End Property

        <Category("Emulator Configuration"), Browsable(True), [ReadOnly](False), Bindable(False), DefaultValue(""), DesignOnly(False), Description("Default audio language extension.")> _
        Public Property AudioExtension() As eAudioExtensions
            Get
                Return My.Settings.PLAYER_AUDIO_LANG_EXT
            End Get
            Set(ByVal Value As eAudioExtensions)
                If Phoenix.Player.PlayState <> ePlayState.Stopped And Phoenix.Player.PlayState <> ePlayState.Init And Phoenix.Player.PlayState <> ePlayState.SystemJP Then Exit Property
                My.Settings.PLAYER_AUDIO_LANG_EXT = Value
                My.Settings.Save()
            End Set
        End Property

        <Category("Emulator Configuration"), Browsable(True), [ReadOnly](False), Bindable(False), DefaultValue(""), DesignOnly(False), Description("Default subtitle language.")> _
        Public Property SubtitleLanguage() As eLanguages
            Get
                Return My.Settings.PLAYER_SUB_LANG
            End Get
            Set(ByVal Value As eLanguages)
                If Phoenix.Player.PlayState <> ePlayState.Stopped And Phoenix.Player.PlayState <> ePlayState.Init And Phoenix.Player.PlayState <> ePlayState.SystemJP Then Exit Property
                My.Settings.PLAYER_SUB_LANG = Value
                My.Settings.Save()
            End Set
        End Property

        <Category("Emulator Configuration"), Browsable(True), [ReadOnly](False), Bindable(False), DefaultValue(""), DesignOnly(False), Description("Default subtitle language extension.")> _
        Public Property SubtitleExtension() As eSubExtensions
            Get
                Return My.Settings.PLAYER_SUB_LANG_EXT
            End Get
            Set(ByVal Value As eSubExtensions)
                If Phoenix.Player.PlayState <> ePlayState.Stopped And Phoenix.Player.PlayState <> ePlayState.Init And Phoenix.Player.PlayState <> ePlayState.SystemJP Then Exit Property
                My.Settings.PLAYER_SUB_LANG_EXT = Value
                My.Settings.Save()
            End Set
        End Property

        <Category("Emulator Configuration"), Browsable(True), [ReadOnly](False), Bindable(False), DefaultValue(""), DesignOnly(False), Description("Emulator region.")> _
        Public Property Region() As eRegions
            Get
                Return My.Settings.PLAYER_REGION
            End Get
            Set(ByVal Value As eRegions)
                If Phoenix.Player.PlayState <> ePlayState.Stopped And Phoenix.Player.PlayState <> ePlayState.Init And Phoenix.Player.PlayState <> ePlayState.SystemJP Then Exit Property
                My.Settings.PLAYER_REGION = Value
                My.Settings.Save()
            End Set
        End Property

#End Region 'Player Defaults

#Region "User Preferences"

        '<Category("User"), Browsable(True), [ReadOnly](False), Bindable(False), DefaultValue(""), DesignOnly(False), Description("Use checkboxes in UOPs window and elsewhere instead of colored boxes.")> _
        'Public Property UseCheckboxes() As Boolean
        '    Get
        '        Return Phoenix.CurrentUserProfile.AppOptions.UseCheckboxesInUI
        '    End Get
        '    Set(ByVal Value As Boolean)
        '        Phoenix.CurrentUserProfile.AppOptions.UseCheckboxesInUI = Value
        '    End Set
        'End Property

        <Category("User"), Browsable(False), [ReadOnly](False), Bindable(False), DefaultValue(""), DesignOnly(False), Description("Log playback events.")> _
        Public Property LogEvents() As Boolean
            Get
                Return Phoenix.CurrentUserProfile.AppOptions.LogEvents
            End Get
            Set(ByVal Value As Boolean)
                Phoenix.CurrentUserProfile.AppOptions.LogEvents = Value
            End Set
        End Property

        <Category("User"), Browsable(True), [ReadOnly](False), Bindable(False), DefaultValue(""), DesignOnly(False), Description("Number of projects to store in recent project list.")> _
        Public Property RecentProjectCount() As Byte
            Get
                Return Phoenix.CurrentUserProfile.AppOptions.RecentProjectCount
            End Get
            Set(ByVal Value As Byte)
                Phoenix.CurrentUserProfile.AppOptions.RecentProjectCount = Value
            End Set
        End Property

        <Category("User"), Browsable(False), [ReadOnly](False), Bindable(False), DefaultValue(""), DesignOnly(False), Description("Toggles the bitrate animation on the dashboard.")> _
        Public Property AnimateBitrate() As Boolean
            Get
                Return Phoenix.CurrentUserProfile.AppOptions.AnimateBitrate
            End Get
            Set(ByVal Value As Boolean)
                Phoenix.CurrentUserProfile.AppOptions.AnimateBitrate = Value
            End Set
        End Property

        '<Category("User"), Browsable(True), [ReadOnly](False), Bindable(False), DefaultValue(""), DesignOnly(False), Description("Determines whether CHAPTER BACK goes to the beginning of the current chapter or the beginning of the previous chapter.")> _
        'Public Property CHBackGoesTwo() As Boolean
        '    Get
        '        Return Phoenix.CurrentUserProfile.AppOptions.ChBackGoesTwo
        '    End Get
        '    Set(ByVal Value As Boolean)
        '        Phoenix.CurrentUserProfile.AppOptions.ChBackGoesTwo = Value
        '    End Set
        'End Property

        <Category("User"), Browsable(True), [ReadOnly](False), Bindable(False), DefaultValue(""), DesignOnly(False), Description("Number of seconds to jump when using jump forward/back.")> _
        Public Property JumpSeconds() As Byte
            Get
                Return Phoenix.CurrentUserProfile.AppOptions.JumpSeconds
            End Get
            Set(ByVal Value As Byte)
                Phoenix.CurrentUserProfile.AppOptions.JumpSeconds = Value
            End Set
        End Property

        <Category("Line21"), Browsable(True), [ReadOnly](False), Bindable(False), DefaultValue(""), DesignOnly(False), Description("Include line-21 positioning commands in log.")> _
        Public Property LogLine21Commands() As Boolean
            Get
                Return Phoenix.CurrentUserProfile.AppOptions.LogLine21_Commands
            End Get
            Set(ByVal Value As Boolean)
                Phoenix.CurrentUserProfile.AppOptions.LogLine21_Commands = Value
            End Set
        End Property

        <Category("Line21"), Browsable(True), [ReadOnly](False), Bindable(False), DefaultValue(""), DesignOnly(False), Description("Log line-21 text data to console.")> _
        Public Property LogLine21() As Boolean
            Get
                Return Phoenix.CurrentUserProfile.AppOptions.LogLine21
            End Get
            Set(ByVal Value As Boolean)
                Phoenix.CurrentUserProfile.AppOptions.LogLine21 = Value
            End Set
        End Property

        <Category("Video"), Browsable(True), [ReadOnly](False), Bindable(False), DefaultValue(""), DesignOnly(False), Description("Letterbox color. Only significant when emulator is set to letterbox mode.")> _
        Public Property LetterboxColor() As eLetterboxColors
            Get
                Return Phoenix.CurrentUserProfile.AppOptions.LetterBoxColor
            End Get
            Set(ByVal Value As eLetterboxColors)
                Phoenix.CurrentUserProfile.AppOptions.LetterBoxColor = Value
            End Set
        End Property

        <Category("Video"), Browsable(True), [ReadOnly](False), Bindable(False), DefaultValue(""), DesignOnly(False), Description("Burn-in MPEG GOP timecodes.")> _
        Public Property SourceTimecode() As Boolean
            Get
                Return Phoenix.CurrentUserProfile.AppOptions.BurnGOPTimecodes
            End Get
            Set(ByVal Value As Boolean)
                Phoenix.CurrentUserProfile.AppOptions.BurnGOPTimecodes = Value
            End Set
        End Property

        <Category("Video"), Browsable(True), [ReadOnly](False), Bindable(False), DefaultValue(""), DesignOnly(False), Description("Burn-in MPEG GOP timecodes.")> _
        Public Property Red_IFrames() As Boolean
            Get
                Return Phoenix.CurrentUserProfile.AppOptions.GOPTimecodes_RedIFrames
            End Get
            Set(ByVal Value As Boolean)
                Dim out As Short = 0
                If Value Then
                    out = 1
                End If
                Phoenix.CurrentUserProfile.AppOptions.GOPTimecodes_RedIFrames = out
                Phoenix.CurrentUserProfile.AppOptions.BurnGOPTimecodes = Phoenix.CurrentUserProfile.AppOptions.BurnGOPTimecodes
            End Set
        End Property

        <Category("Video"), Browsable(True), [ReadOnly](False), Bindable(False), DefaultValue(""), DesignOnly(False), Description("Display action-title safe guides.")> _
        Public Property ActionTitleGuides() As Boolean
            Get
                Return Phoenix.CurrentUserProfile.AppOptions.ActionTitleGuides
            End Get
            Set(ByVal Value As Boolean)
                Phoenix.CurrentUserProfile.AppOptions.ActionTitleGuides = Value
            End Set
        End Property

        <Category("Video"), Browsable(True), [ReadOnly](False), Bindable(False), DefaultValue(""), DesignOnly(False), Description("Color of action-title safe guides.")> _
        Public Property ActionTitleGuideColor() As eActionTitleSafeColors
            Get
                Return Phoenix.CurrentUserProfile.AppOptions.ActionTitleSafeColor
            End Get
            Set(ByVal Value As eActionTitleSafeColors)
                Phoenix.CurrentUserProfile.AppOptions.ActionTitleSafeColor = Value
            End Set
        End Property

        <Category("Video"), Browsable(True), [ReadOnly](False), Bindable(False), DefaultValue(""), DesignOnly(False), Description("Render 8bits with high-contrast colors.")> _
        Public Property ContrastSPColors() As Boolean
            Get
                Return Phoenix.CurrentUserProfile.AppOptions.ContrastSPColors
            End Get
            Set(ByVal Value As Boolean)
                Phoenix.CurrentUserProfile.AppOptions.ContrastSPColors = Value
            End Set
        End Property

        <Category("Video"), Browsable(False), [ReadOnly](False), Bindable(False), DefaultValue(""), DesignOnly(False), Description("Show on-screen display burn-ins.")> _
        Public Property ShowOSD() As Boolean
            Get
                Return Phoenix.CurrentUserProfile.AppOptions.ShowOSD
            End Get
            Set(ByVal Value As Boolean)
                Phoenix.CurrentUserProfile.AppOptions.ShowOSD = Value
            End Set
        End Property

        <Category("Video"), Browsable(True), [ReadOnly](False), Bindable(False), DefaultValue(""), DesignOnly(False), Description("Beep when source video interlacing or frame rate changes.")> _
        Public Property BeepOnVideoPropChange() As Boolean
            Get
                Return Phoenix.CurrentUserProfile.AppOptions.BeepOnVideoPropChange
            End Get
            Set(ByVal Value As Boolean)
                Phoenix.CurrentUserProfile.AppOptions.BeepOnVideoPropChange = Value
            End Set
        End Property

        '<Category("Video"), Browsable(True), [ReadOnly](False), Bindable(False), DefaultValue(""), DesignOnly(False), Description("Beep when source video interlacing or frame rate changes.")> _
        'Public Property AnalogVideoOutputFormat() As eAnalogVideoOutputFormat
        '    Get
        '        Return Phoenix.CurrentUserProfile.AppOptions.AnalogVideoOutputFormat
        '    End Get
        '    Set(ByVal Value As eAnalogVideoOutputFormat)
        '        Phoenix.CurrentUserProfile.AppOptions.AnalogVideoOutputFormat = Value
        '    End Set
        'End Property

        <Category("Frame Grabbing"), Browsable(True), [ReadOnly](False), Bindable(False), DefaultValue(60), DesignOnly(False), Description("Number of frames to dump in a multi frame dump operation.")> _
        Public Property MultiFrameCount() As Byte
            Get
                Return Phoenix.CurrentUserProfile.AppOptions.MultiFrameCount
            End Get
            Set(ByVal Value As Byte)
                Phoenix.CurrentUserProfile.AppOptions.MultiFrameCount = Value
            End Set
        End Property

        <Category("Frame Grabbing"), Browsable(False), [ReadOnly](False), Bindable(False), DefaultValue(""), DesignOnly(False), Description("Directory in which frame grabs should be saved.")> _
        Public Property FrameGrabLocation() As String
            Get
                Return Phoenix.Player.FrameGrabDumpDir
            End Get
            Set(ByVal Value As String)
                Value = Microsoft.VisualBasic.Replace(Value.ToString, "framegrabs\", "")
                Phoenix.Player.DumpDirectory = Value
            End Set
        End Property

        <Category("Frame Grabbing"), Browsable(True), [ReadOnly](False), Bindable(False), DefaultValue(""), DesignOnly(False), Description("Frame grab type.")> _
        Public Property FrameGrabSource() As eFrameGrabContent
            Get
                Return Phoenix.CurrentUserProfile.AppOptions.FrameGrabSource
            End Get
            Set(ByVal Value As eFrameGrabContent)
                Phoenix.CurrentUserProfile.AppOptions.FrameGrabSource = Value
            End Set
        End Property

        <Category("Frame Grabbing"), Browsable(True), [ReadOnly](False), Bindable(False), DefaultValue(""), DesignOnly(False), Description("Filetype for frame grab.")> _
        Public Property FrameGrabType() As eFrameGrabType
            Get
                Return Phoenix.CurrentUserProfile.AppOptions.FrameGrabType
            End Get
            Set(ByVal Value As eFrameGrabType)
                Phoenix.CurrentUserProfile.AppOptions.FrameGrabType = Value
            End Set
        End Property

        '<Category("Video"), Browsable(True), [ReadOnly](False), Bindable(False), DefaultValue(""), DesignOnly(False), Description("Filetype for frame grab.")> _
        'Public Property ReverseFieldOrder() As Boolean
        '    Get
        '        If Not Phoenix.Player Is Nothing Then
        '            Return Phoenix.CurrentUserProfile.AppOptions.ReverseFieldOrder
        '        Else
        '            Return False
        '        End If
        '    End Get
        '    Set(ByVal Value As Boolean)
        '        Phoenix.CurrentUserProfile.AppOptions.ReverseFieldOrder = Value
        '    End Set
        'End Property

        '<Category("Non-Seamless Cells"), Browsable(True), [ReadOnly](False), Bindable(False), DefaultValue(""), DesignOnly(False), Description("Select type of non-seamless cells to simulate.")> _
        'Public Property NonSeamlessType() As eNSCsToSimulate
        '    Get
        '        Return Phoenix.CurrentUserProfile.AppOptions.NSCsToSimulate
        '    End Get
        '    Set(ByVal Value As eNSCsToSimulate)
        '        Phoenix.CurrentUserProfile.AppOptions.NSCsToSimulate = Value
        '    End Set
        'End Property

        '<Category("Non-Seamless Cells"), Browsable(True), [ReadOnly](False), Bindable(False), DefaultValue(""), DesignOnly(False), Description("Select preroll for NSC simulation. Default is one second and fourteen frames.")> _
        'Public Property NSCPrerollSeconds() As Byte
        '    Get
        '        Return Phoenix.CurrentUserProfile.AppOptions.NSC_Preroll.Seconds
        '    End Get
        '    Set(ByVal Value As Byte)
        '        Phoenix.CurrentUserProfile.AppOptions.NSC_Preroll.Seconds = Value
        '    End Set
        'End Property

        '<Category("Non-Seamless Cells"), Browsable(True), [ReadOnly](False), Bindable(False), DefaultValue(""), DesignOnly(False), Description("Select preroll for NSC simulation. Default is one second and fourteen frames.")> _
        'Public Property NSCPrerollFrames() As Byte
        '    Get
        '        Return Phoenix.CurrentUserProfile.AppOptions.NSC_Preroll.Frames
        '    End Get
        '    Set(ByVal Value As Byte)
        '        Phoenix.CurrentUserProfile.AppOptions.NSC_Preroll.Frames = Value
        '    End Set
        'End Property

        '<Category("Non-Seamless Cells"), Browsable(True), [ReadOnly](False), Bindable(False), DefaultValue(""), DesignOnly(False), Description("Set non-seamless cell laser refocus interval. Measured in milliseconds (1/1000th of a second). Must be a number between 0 and 3000.")> _
        'Public Property NSCInterval() As Short
        '    Get
        '        Return Phoenix.CurrentUserProfile.AppOptions.NSC_Preroll.Interval
        '    End Get
        '    Set(ByVal Value As Short)
        '        If (Value < 0 Or Value > 3000) Then
        '            MsgBox("Refocus interval must be a number between 0 and 3000")
        '        End If
        '        Phoenix.CurrentUserProfile.AppOptions.NSC_Preroll.Interval = Value
        '    End Set
        'End Property

#End Region 'User Preferences

    End Class

#Region "MARKERS"

    Public Class cMarker
        Public TitleNum As Integer
        Public ChapterNum As Integer
        Public Hours As Byte
        Public Minutes As Byte
        Public Seconds As Byte
        Public Frames As Byte
        Public TimeCodeFlags As Integer
        Public GOPTC As cTimecode

        Public MarkerName As String

        Public Sub New()
        End Sub

        Public Sub SetLocation(ByVal loc As DvdPlayLocation)
            TitleNum = loc.TitleNum
            ChapterNum = loc.ChapterNum
            Hours = loc.timeCode.bHours
            Minutes = loc.timeCode.bMinutes
            Seconds = loc.timeCode.bSeconds
            Frames = loc.timeCode.bFrames
            TimeCodeFlags = loc.TimeCodeFlags
        End Sub

        Public Sub SetGOPTC(ByVal TC As cTimecode)
            With GOPTC
                .Framerate = ""
                .Frames = 1
                .Hours = 1
                .Minutes = 1
                .Seconds = 1
            End With
        End Sub

        Public Function GetLocation() As DvdPlayLocation
            Dim out As New DvdPlayLocation
            out.ChapterNum = ChapterNum
            out.TitleNum = TitleNum
            out.timeCode.bHours = Hours
            out.timeCode.bMinutes = Minutes
            out.timeCode.bSeconds = Seconds
            out.timeCode.bFrames = Frames
            out.TimeCodeFlags = TimeCodeFlags
        End Function

        Public Function GetDVDTimeCode() As DvdTimeCode
            Dim out As New DvdTimeCode
            out.bFrames = Me.Frames
            out.bHours = Me.Hours
            out.bMinutes = Me.Minutes
            out.bSeconds = Me.Seconds
            Return out
        End Function

        Public Function GetMarkerLocationString() As String
            Try
                Dim sb As New StringBuilder
                sb.Append("TT: " & Me.TitleNum)
                sb.Append("  PTT: " & Me.ChapterNum)
                sb.Append("  TC: " & Me.Hours & ":" & PadString(Me.Minutes, 2, "0", True) & ":" & PadString(Me.Seconds, 2, "0", True) & ";" & PadString(Me.Frames, 2, "0", True))
                Return sb.ToString
            Catch ex As Exception
                Throw New Exception("Problem with GetMarkerLocationString. Error: " & ex.Message, ex)
            End Try
        End Function

        Public Overrides Function ToString() As String
            Try
                Dim sb As New StringBuilder
                If Not Me.GOPTC Is Nothing Then
                    sb.Append(MarkerName & vbTab & Me.GetMarkerLocationString & vbTab & "Source: " & GOPTC.ToString)
                Else
                    sb.Append(MarkerName & vbTab & Me.GetMarkerLocationString)
                End If
                Return sb.ToString
            Catch ex As Exception
                Throw New Exception("Problem with MarkerToString. Error: " & ex.Message, ex)
            End Try
        End Function

        Public Function ToStringCSV() As String
            Try
                Dim sb As New StringBuilder
                If Not Me.GOPTC Is Nothing Then
                    sb.Append(MarkerName & "," & Me.GetMarkerLocationString & "," & "Source: " & GOPTC.ToString)
                Else
                    sb.Append(MarkerName & "," & Me.GetMarkerLocationString)
                End If
                Return sb.ToString
            Catch ex As Exception
                Throw New Exception("Problem with MarkerToString. Error: " & ex.Message, ex)
            End Try
        End Function

    End Class

    Public Class cMarkerSet
        Public Markers() As cMarker
        Public SetName As String
        Public Created As DateTime

        Public Sub New(ByVal SName As String)
            ReDim Markers(-1)
            Created = DateTime.Now
            SetName = SName
        End Sub

        Public Sub New()
        End Sub

        Public Sub AddMarker(ByVal M As cMarker)
            ReDim Preserve Markers(UBound(Markers) + 1)
            Markers(UBound(Markers)) = M
        End Sub

        Public Sub RemoveAt(ByVal Index As Byte)
            Try
                Dim tMS(-1) As cMarker
                For s As Short = 0 To Markers.Length - 1
                    If Not s = Index Then
                        ReDim Preserve tMS(UBound(tMS) + 1)
                        tMS(UBound(tMS)) = Markers(s)
                    End If
                Next
                Markers = tMS
            Catch ex As Exception
                Debug.WriteLine("Problem with RemoveAt in cMarkerSet. Error: " & ex.Message)
            End Try
        End Sub

    End Class

    Public Class cMarkerSetCollection

        Public MarkerSets() As cMarkerSet
        Public ProjectPath As String
        Public Name As String
        Public LoadedFromPath As String
        Public CurrentSetIndex As Byte = 0

        Public Function Add(ByVal MS As cMarkerSet) As Integer
            ReDim Preserve Me.MarkerSets(UBound(MarkerSets) + 1)
            Me.MarkerSets(UBound(MarkerSets)) = MS
        End Function

        Public Sub Remove(ByVal Item As cMarkerSet)
            'MyBase.List.Remove(Item)
        End Sub

        Public Sub RemoveAt(ByVal Index As Short)
            Try
                Dim tMSC As New cMarkerSetCollection
                For i As Short = 0 To Me.MarkerSets.Length - 1
                    If Not i = Index Then
                        tMSC.Add(Me.MarkerSets(i))
                    End If
                Next
                ReDim Me.MarkerSets(-1)
                For i As Short = 0 To tMSC.MarkerSets.Length - 1
                    Me.Add(tMSC.MarkerSets(i))
                Next
            Catch ex As Exception
                Debug.WriteLine("Problem with RemoveAt in cMarkerSetCollection. Error: " & ex.Message)
            End Try
        End Sub

        Public Sub New()
            ReDim MarkerSets(-1)
            Me.ProjectPath = ""
            Me.LoadedFromPath = ""
            Me.Name = ""
        End Sub

        Public Sub New(ByVal PrjPath As String, ByVal _Name As String)
            ProjectPath = PrjPath
            Name = _Name
            Me.LoadedFromPath = ""
            ReDim MarkerSets(-1)
        End Sub

        Public Sub SetCurrentIndexBySetName(ByVal Name As String)
            Try
                For b As Byte = 0 To MarkerSets.Length - 1
                    If Me.MarkerSets(b).SetName.ToLower = Name.ToLower Then
                        Me.CurrentSetIndex = b
                        Exit Sub
                    End If
                Next
            Catch ex As Exception
                Debug.WriteLine("Problem with SetCurrentIndexBySetName. Error: " & ex.Message)
            End Try
        End Sub

        Public ReadOnly Property CurrentMarkerSet() As cMarkerSet
            Get
                If Me.MarkerSets.Length < 1 Then Return Nothing
                Return Me.MarkerSets(Me.CurrentSetIndex)
            End Get
        End Property

        Public Function FindSetByName(ByVal Name As String) As cMarkerSet
            For Each MS As cMarkerSet In Me.MarkerSets
                If MS.SetName.ToLower = Name.ToLower Then
                    Return MS
                End If
            Next
        End Function

    End Class

#End Region 'MARKERS

#Region "USER PROFILE"

    <Serializable()> _
    Public Class cPhoenixUserProfile

        <XmlIgnore()> _
        Public PP As Phoenix_Form

        Public KeyMapping As cKeyMapping
        Public AppOptions As cPhoenixAppOptions
        Public DockingLayout As cDockingLayout

#Region "Constructor/Destructor"

        Public Sub New()
        End Sub

        Public Sub New(ByRef nPP As Phoenix_Form)
            PP = nPP
            Me.KeyMapping = New cKeyMapping()
            Me.AppOptions = New cPhoenixAppOptions(nPP)
            Me.DockingLayout = New cDockingLayout(nPP)
        End Sub

        Public Sub New(ByVal PathOrXMLString As String, ByVal ArgIsXML As Boolean, ByVal nPP As Phoenix_Form)
            Try
                PP = nPP

                Dim XMLString As String

                If ArgIsXML Then
                    XMLString = PathOrXMLString
                Else
                    Dim FS As New FileStream(PathOrXMLString, FileMode.Open)
                    Dim StR As New StreamReader(FS)
                    XMLString = StR.ReadToEnd
                    StR.Close()
                    FS.Close()
                End If

                Dim SR As New StringReader(XMLString)
                Dim XS As New XmlSerializer(GetType(cPhoenixUserProfile))
                Dim tmp As cPhoenixUserProfile = CType(XS.Deserialize(SR), cPhoenixUserProfile)
                XS = Nothing
                SR = Nothing

                Me.KeyMapping = tmp.KeyMapping
                Me.KeyMapping.FinishDeSerialization()
                Me.DockingLayout = tmp.DockingLayout
                Me.AppOptions = tmp.AppOptions

                Me.AppOptions.PP = nPP
                Me.DockingLayout.PP = nPP

                Me.Apply()

            Catch ex As Exception
                Throw New Exception("Problem with New cPhoenixUserProfile. Error: " & ex.Message)
            End Try
        End Sub

        Public Sub InitializeWithDefaults()
            Try
                Me.AppOptions.InitializeDefaultValues()
                Me.KeyMapping.InitializeDefaultKeymapping()
                Me.DockingLayout.InitializeDefaultLayout("std")
                Me.Apply()
            Catch ex As Exception
                Throw New Exception("Problem with InitializeToDefaults(). Error: " & ex.Message)
            End Try
        End Sub

#End Region 'Constructor/Destructor

#Region "Input/Output"

        Public Function Save(ByVal SaveToPath As String) As Boolean
            Try
                Me.DockingLayout.Update()

                Dim XS As New XmlSerializer(GetType(cPhoenixUserProfile))
                Dim TW As New StringWriter
                XS.Serialize(TW, Me)
                XS = Nothing
                Dim Out As String = TW.ToString

                Dim FS As New FileStream(SaveToPath, FileMode.OpenOrCreate)
                Dim SW As New StreamWriter(FS)
                SW.Write(Out)
                SW.Close()
                FS.Close()
                FS = Nothing
                Return True
            Catch ex As Exception
                Throw New Exception("Problem with Save() in cPhoenixUserProfile. Error: " & ex.Message)
            End Try
        End Function

        Public Function SaveWithDialog() As String
            Try
                Dim dlg As New SaveFileDialog
                dlg.Filter = "User Profile | *.pup"
                dlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
                dlg.Title = "Save User Profile"
                dlg.AddExtension = True
                If dlg.ShowDialog = DialogResult.OK Then
                    If File.Exists(dlg.FileName) Then File.Delete(dlg.FileName)
                    If Me.Save(dlg.FileName) Then
                        Return dlg.FileName
                    End If
                End If
            Catch ex As Exception
                Throw New Exception("Problem with LoadUserProfile_WithDialog. Error: " & ex.Message)
            End Try
        End Function

#End Region 'Input/Output

        Public Sub Apply()
            Me.DockingLayout.Apply()
            Me.AppOptions.Apply()
        End Sub

    End Class

#Region "App Options"

    <Serializable()> _
    Public Class cPhoenixAppOptions

        Private HR As Integer

        <NonSerialized(), XmlIgnore()> _
        Public PP As Phoenix_Form

        Public Sub New()
        End Sub

        Public Sub New(ByRef _PP As Phoenix_Form)
            PP = _PP
        End Sub

        Public Sub InitializeDefaultValues()
            Try
                LogLine21 = False
                LogLine21_Commands = False
                ShowOSD = True
                JumpSeconds = 20
                FrameGrabSource = eFrameGrabContent.Full_Mix
                FrameGrabType = eFrameGrabType.BMP
                DumpLocation = "C:\Temp\"
                'ChBackGoesTwo = True
                AnimateBitrate = True
                RecentProjectCount = 10
                NSCsToSimulate = eNSCsToSimulate.LBOnly
                LogEvents = True
                MultiFrameCount = 60
                ActionTitleGuides = False
                ActionTitleSafeColor = eActionTitleSafeColors.DarkRed
                BurnGOPTimecodes = False
                LetterBoxColor = eLetterboxColors.LightBlack
                UseCheckboxesInUI = False
                AnalogVideoOutputFormat = eAnalogVideoOutputFormat.Component

                PA_DoProcAmp = False
                PA_CurrentSettings = New cProcAmpTemplate(PP)
                PA_CurrentSettings.Name = "Defaults"
                PA_CurrentSettings.Brightness = 0
                PA_CurrentSettings.Contrast = -80
                PA_CurrentSettings.Hue = 0
                PA_CurrentSettings.Saturation = -80
                ReDim Me.PA_SavedTemplates(-1)

                NSC_Preroll = New cNSCPreRoll
                NSC_Preroll.Seconds = 1
                NSC_Preroll.Frames = 14
                NSC_Preroll.Interval = 1000
            Catch ex As Exception
                Throw New Exception("Problem with InitializeDefaultValues - cPhoenixAppOptions. Error: " & ex.Message)
            End Try
        End Sub

        Public Sub Apply()
            Try
                Me.ActionTitleGuides = Me._ActionTitleGuides
                Me.BeepOnVideoPropChange = Me._BeepOnVideoPropChange
                Me.BurnGOPTimecodes = Me._BurnGOPTimecodes
                Me.ContrastSPColors = Me._ContrastSPColors
                Me.DumpLocation = Me._DumpLocation
                Me.FrameGrabSource = Me._FrameGrabSource
                Me.FrameGrabType = Me._FrameGrabType
                Me.LetterBoxColor = Me._LetterBoxColor
                Me.LogLine21 = Me._LogLine21
                Me.RecentProjectCount = Me._RecentProjectCount
                Me.PA_CurrentSettings = Me._PA_CurrentSettings
                Me.PA_HalfFrame = Me._PA_HalfFrame
                Me.PA_DoProcAmp = Me._PA_DoProcAmp
                Me.UseCheckboxesInUI = Me._UseCheckboxesInUI
                Me.AnalogVideoOutputFormat = Me._AnalogVideoOutputFormat
                PP.SetupPlayerConfigPropertyGrid()
                'PP.ScrollOptionsGridToTop()
            Catch ex As Exception
                PP.AddConsoleLine(eConsoleItemType.ERROR, "Problem with AddProcAmpTemplate. Error: " & ex.Message)
            End Try
        End Sub

        <XmlIgnore()> _
        Public Property LogLine21() As Boolean
            Get
                If Not PP.FeatureManagement.Features.FE_L21_TextExtraction Then _LogLine21 = False
                Return _LogLine21
            End Get
            Set(ByVal Value As Boolean)
                _LogLine21 = Value
                If PP Is Nothing OrElse PP.Player Is Nothing Then Exit Property
                If Not PP.FeatureManagement.Features.FE_L21_TextExtraction Then _LogLine21 = False
                PP.Player.ClosedCaptionLogging = _LogLine21
            End Set
        End Property
        Public _LogLine21 As Boolean

        <XmlIgnore()> _
        Public Property LogLine21_Commands() As Boolean
            Get
                Return _LogLine21_Commands
            End Get
            Set(ByVal value As Boolean)
                _LogLine21_Commands = value
                If PP Is Nothing OrElse PP.Player Is Nothing Then Exit Property
                PP.Player.ClosedCaptionLogging_IncludeCommands = value
            End Set
        End Property
        Private _LogLine21_Commands As Boolean

        Public ShowOSD As Boolean
        Public JumpSeconds As Byte

        <XmlIgnore()> _
        Public Property FrameGrabType() As eFrameGrabType
            Get
                Return _FrameGrabType
            End Get
            Set(ByVal Value As eFrameGrabType)
                _FrameGrabType = Value
                If Not PP Is Nothing AndAlso Not PP.pgOptions Is Nothing Then PP.pgOptions.Refresh()
            End Set
        End Property
        Public _FrameGrabType As eFrameGrabType

        <XmlIgnore()> _
        Public ReadOnly Property FrameGrabTypeAsImageFormat() As System.Drawing.Imaging.ImageFormat
            Get
                Select Case FrameGrabType
                    Case eFrameGrabType.BMP
                        Return System.Drawing.Imaging.ImageFormat.Bmp
                    Case eFrameGrabType.GIF
                        Return System.Drawing.Imaging.ImageFormat.Gif
                    Case eFrameGrabType.JPEG
                        Return System.Drawing.Imaging.ImageFormat.Jpeg
                    Case eFrameGrabType.PNG
                        Return System.Drawing.Imaging.ImageFormat.Png
                    Case eFrameGrabType.TIF
                        Return System.Drawing.Imaging.ImageFormat.Tiff
                End Select
            End Get
        End Property

        <XmlIgnore()> _
        Public Property FrameGrabSource() As eFrameGrabContent
            Get
                Select Case _FrameGrabSource
                    Case eFrameGrabContent.Closed_Caption_Only, eFrameGrabContent.MultiFrame, eFrameGrabContent.Subpicture_Only, eFrameGrabContent.Video_and_Subpicture
                        If Not PP.FeatureManagement.Features.FE_VDP_MultiFrame Then _FrameGrabSource = eFrameGrabContent.Video_Only
                    Case eFrameGrabContent.Full_Mix
                        If Not PP.FeatureManagement.Features.FE_VDP_FullMix Then _FrameGrabSource = eFrameGrabContent.Video_Only
                End Select
                Return _FrameGrabSource
            End Get
            Set(ByVal Value As eFrameGrabContent)
                _FrameGrabSource = Value
                If PP Is Nothing Then Exit Property
                If Not PP Is Nothing AndAlso Not PP.pgOptions Is Nothing Then PP.pgOptions.Refresh()
            End Set
        End Property
        Public _FrameGrabSource As eFrameGrabContent

        '<XmlIgnore()> _
        'Public Property ChBackGoesTwo() As Boolean
        '    Get
        '        Return _ChBackGoesTwo
        '    End Get
        '    Set(ByVal value As Boolean)
        '        _ChBackGoesTwo = value
        '        If PP Is Nothing OrElse PP.Player Is Nothing Then Exit Property
        '        PP.Player.ChBackGoesTwo = value
        '    End Set
        'End Property
        'Public _ChBackGoesTwo As Boolean

        Public AnimateBitrate As Boolean
        Public LogEvents As Boolean
        Public MultiFrameCount As Byte

        <XmlIgnore()> _
        Public Property LetterBoxColor() As eLetterboxColors
            Get
                Return _LetterBoxColor
            End Get
            Set(ByVal Value As eLetterboxColors)
                Dim R, G, B As Integer
                _LetterBoxColor = Value
                Select Case Value
                    Case eLetterboxColors.Blue
                        R = 16
                        G = 16
                        B = 235
                    Case eLetterboxColors.DarkBlack
                        R = 16
                        G = 16
                        B = 16
                    Case eLetterboxColors.Green
                        R = 16
                        G = 235
                        B = 16
                    Case eLetterboxColors.LightBlack
                        R = 31
                        G = 31
                        B = 31
                    Case eLetterboxColors.Red
                        R = 235
                        G = 16
                        B = 16
                End Select
                If PP Is Nothing OrElse PP.Player Is Nothing Then Exit Property
                PP.Player.LetterboxColor() = Color.FromArgb(255, R, G, B)
            End Set
        End Property
        Public _LetterBoxColor As eLetterboxColors

        <XmlIgnore()> _
        Public Property RecentProjectCount() As Byte
            Get
                Return _RecentProjectCount
            End Get
            Set(ByVal Value As Byte)
                Me._RecentProjectCount = Value
                If Value < 1 Or Value > 20 Then
                    Value = 1
                End If
            End Set
        End Property
        Public _RecentProjectCount As Byte

        <XmlIgnore()> _
        Public Property ActionTitleGuides() As Boolean
            Get
                If Not PP.FeatureManagement.Features.FE_GI_ActionTitleSafe Then _ActionTitleGuides = False
                Return _ActionTitleGuides
            End Get
            Set(ByVal Value As Boolean)
                _ActionTitleGuides = Value
                Try
                    If PP Is Nothing Or PP.Player Is Nothing Then Exit Property
                    If Not PP.FeatureManagement.Features.FE_GI_ActionTitleSafe Then _ActionTitleGuides = False
                    PP.Player.SetActionTitleGuides(_ActionTitleGuides, 50, 200, 50)
                    If Not PP.pgOptions Is Nothing Then
                        'me.ScrollOptionsGridToItem("ActionTitleGuides")
                        PP.pgOptions.Refresh()
                    End If
                Catch ex As Exception
                    PP.AddConsoleLine(eConsoleItemType.ERROR, "Problem with SetGuides. Error: " & ex.Message)
                End Try
            End Set
        End Property
        Public _ActionTitleGuides As Boolean

        <XmlIgnore()> _
        Public Property ActionTitleSafeColor() As eActionTitleSafeColors
            Get
                Return _ActionTitleSafeColor
            End Get
            Set(ByVal Value As eActionTitleSafeColors)
                Dim R, G, B As Byte
                _ActionTitleSafeColor = Value
                Select Case Value
                    Case eActionTitleSafeColors.White
                        R = 235
                        G = 235
                        B = 235
                    Case eActionTitleSafeColors.Grey
                        R = 70
                        G = 70
                        B = 70
                    Case eActionTitleSafeColors.DarkGrey
                        R = 35
                        G = 35
                        B = 35
                    Case eActionTitleSafeColors.DarkBlue
                        R = 70
                        G = 20
                        B = 20
                    Case eActionTitleSafeColors.DarkRed
                        R = 20
                        G = 20
                        B = 70
                End Select
                If PP Is Nothing Or PP.Player Is Nothing Then Exit Property
                PP.Player.ActionTitleSafeColor = Color.FromArgb(255, R, G, B)
            End Set
        End Property
        Public _ActionTitleSafeColor As eActionTitleSafeColors

        <XmlIgnore()> _
        Public Property BurnGOPTimecodes() As Boolean
            Get
                If Not PP.FeatureManagement.Features.FE_M2M_GOPTimecodes Then _BurnGOPTimecodes = False
                Return _BurnGOPTimecodes
            End Get
            Set(ByVal Value As Boolean)
                Try
                    _BurnGOPTimecodes = Value
                    If PP Is Nothing OrElse PP.Player Is Nothing Then Exit Property
                    If Not PP.FeatureManagement.Features.FE_M2M_GOPTimecodes Then _BurnGOPTimecodes = False
                    PP.Player.SetGOPTimecodeBurnIn(_BurnGOPTimecodes, GOPTimecodes_RedIFrames)
                    If Not PP.pgOptions Is Nothing Then PP.pgOptions.Refresh()
                Catch ex As Exception
                    PP.AddConsoleLine(eConsoleItemType.ERROR, "Problem with BurnMPEGTCs. Error: " & ex.Message)
                End Try
            End Set
        End Property
        Public _BurnGOPTimecodes As Boolean

        <XmlIgnore()> _
        Public Property GOPTimecodes_RedIFrames() As Short
            Get
                Return _GOPTimecodes_RedIFrames
            End Get
            Set(ByVal Value As Short)
                _GOPTimecodes_RedIFrames = Value
                If PP Is Nothing OrElse PP.Player Is Nothing Then Exit Property
                PP.Player.SetGOPTimecodeBurnIn(BurnGOPTimecodes, GOPTimecodes_RedIFrames)
            End Set
        End Property
        Public _GOPTimecodes_RedIFrames As Short

        <XmlIgnore()> _
        Public Property DumpLocation() As String
            Get
                If _DumpLocation Is Nothing OrElse _DumpLocation = "" Then
                    _DumpLocation = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
                End If
                Return _DumpLocation
            End Get
            Set(ByVal Value As String)
                If Value Is Nothing Then
                    _DumpLocation = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
                Else
                    _DumpLocation = Value
                End If
                If Not PP Is Nothing AndAlso Not PP.pgOptions Is Nothing Then PP.pgOptions.Refresh()
                If Not PP.Player Is Nothing Then
                    PP.Player.DumpDirectory = Value
                End If
            End Set
        End Property
        Public _DumpLocation As String

        Public PA_SavedTemplates() As cProcAmpTemplate

        <XmlIgnore()> _
        Public Property PA_CurrentSettings() As cProcAmpTemplate
            Get
                Return _PA_CurrentSettings
            End Get
            Set(ByVal Value As cProcAmpTemplate)
                Try
                    Me._PA_CurrentSettings = Value
                    If PP Is Nothing Then Exit Property
                Catch ex As Exception
                    PP.AddConsoleLine(eConsoleItemType.ERROR, "Problem with set PA_CurrentSettings. Error: " & ex.Message)
                End Try
            End Set
        End Property
        Public _PA_CurrentSettings As cProcAmpTemplate

        <XmlIgnore()> _
        Public Property PA_HalfFrame() As Boolean
            Get
                Return _PA_HalfFrame
            End Get
            Set(ByVal Value As Boolean)
                _PA_HalfFrame = Value
                If PP Is Nothing Or PP.Player Is Nothing Then Exit Property
                If Value Then PP.Player.ProcAmp_Active = True
                PP.Player.ProcAmp_HalfScreen = Value
            End Set
        End Property
        Public _PA_HalfFrame As Boolean

        <XmlIgnore()> _
        Public Property PA_DoProcAmp() As Boolean
            Get
                Return _PA_DoProcAmp
            End Get
            Set(ByVal Value As Boolean)
                If PP Is Nothing Then Exit Property
                If Not PP.FeatureManagement.Features.FE_VA_ProcAmp Then Value = False
                _PA_DoProcAmp = Value
                If PP Is Nothing OrElse PP.Player Is Nothing Then Exit Property
                PP.Player.ProcAmp_Active = Value
            End Set
        End Property
        Public _PA_DoProcAmp As Boolean

        Public NSCsToSimulate As eNSCsToSimulate
        Public NSC_Preroll As cNSCPreRoll

        <XmlIgnore()> _
        Public Property ContrastSPColors() As Boolean
            Get
                If Not PP.FeatureManagement.Features.FE_SP_HighContrast Then _ContrastSPColors = False
                Return _ContrastSPColors
            End Get
            Set(ByVal Value As Boolean)
                _ContrastSPColors = Value
                If PP Is Nothing Or PP.Player Is Nothing Then Exit Property
                If Not PP.FeatureManagement.Features.FE_SP_HighContrast Then _ContrastSPColors = False
                PP.Player.HighContrastSubpictures = _ContrastSPColors
            End Set
        End Property
        Public _ContrastSPColors As Boolean

        <XmlIgnore()> _
        Public Property BeepOnVideoPropChange() As Boolean
            Get
                Return _BeepOnVideoPropChange
            End Get
            Set(ByVal Value As Boolean)
                _BeepOnVideoPropChange = Value
            End Set
        End Property
        Public _BeepOnVideoPropChange As Boolean

        <XmlIgnore()> _
        Public Property UseCheckboxesInUI() As Boolean
            Get
                Return _UseCheckboxesInUI
            End Get
            Set(ByVal Value As Boolean)
                _UseCheckboxesInUI = Value
                If Not PP Is Nothing Then
                    PP.CheckBoxMode = Value
                End If
            End Set
        End Property
        Public _UseCheckboxesInUI As Boolean

        Public Sub AddProcAmpTemplate(ByVal PAT As cProcAmpTemplate)
            Try
                If PA_SavedTemplates Is Nothing Then
                    ReDim PA_SavedTemplates(0)
                    PA_SavedTemplates(0) = PAT
                Else
                    'first check to see if a template of this name already exists
                    For i As Integer = 0 To UBound(PA_SavedTemplates)
                        If PA_SavedTemplates(i).Name.ToLower = PAT.Name.ToLower Then
                            'Template of same name found. Update it and bail.
                            PA_SavedTemplates(i) = PAT
                            Exit Sub
                        End If
                    Next

                    'Same name not found. Save new.
                    ReDim Preserve PA_SavedTemplates(UBound(PA_SavedTemplates) + 1)
                    PA_SavedTemplates(UBound(PA_SavedTemplates)) = PAT
                End If
            Catch ex As Exception
                Throw New Exception("Problem with AddProcAmpTemplate(). Error: " & ex.Message, ex)
            End Try
        End Sub

        <XmlIgnore()> _
       Public Property AnalogVideoOutputFormat() As eAnalogVideoOutputFormat
            Get
                Return Me._AnalogVideoOutputFormat
            End Get
            Set(ByVal Value As eAnalogVideoOutputFormat)
                If Not PP Is Nothing AndAlso Not PP.Player Is Nothing Then
                    Try
                        If Not PP.Player.Graph.SetDecklinkAnalogVideoOutputType(Value = eAnalogVideoOutputFormat.Component) Then
                            GoTo Failed
                        End If
                    Catch ex As Exception
                        GoTo Failed
                    End Try

                    Me._AnalogVideoOutputFormat = Value
                    Exit Property
Failed:
                    'do not change _AnalogVideoOutputFormat
                End If
            End Set
        End Property
        Public _AnalogVideoOutputFormat As eAnalogVideoOutputFormat

        Public Property UseSeparateResumeButton() As Boolean
            Get
                Return _UseSeparateResumeButton
            End Get
            Set(ByVal value As Boolean)
                _UseSeparateResumeButton = value

                If PP IsNot Nothing Then
                    If value Then
                        PP.btnResume.Visible = True
                    Else
                        PP.btnResume.Visible = False
                    End If
                End If

            End Set
        End Property
        Private _UseSeparateResumeButton As Boolean = False

        Public Property IntensityVideoScalingMode() As eScalingMode
            Get
                Return _IntensityScalingMode
            End Get
            Set(ByVal value As eScalingMode)
                _IntensityScalingMode = value
                My.Settings.PREFERRED_HDMI_SCALINGMODE = value
                My.Settings.Save()
            End Set
        End Property
        Private _IntensityScalingMode As eScalingMode = eScalingMode.Native_ScaleToAR

        Public Property IntensityVideoResolution() As eVideoResolution
            Get
                Return _IntensityVideoResolution
            End Get
            Set(ByVal value As eVideoResolution)
                _IntensityVideoResolution = value
                My.Settings.PREFERRED_HDMI_RESOLUTION = value
                My.Settings.Save()
            End Set
        End Property
        Private _IntensityVideoResolution As eVideoResolution = eVideoResolution._1920x1080

        Public Property IntensityVideoMaximized() As Boolean
            Get
                Return _IntensityVideoMaximized
            End Get
            Set(ByVal value As Boolean)
                _IntensityVideoMaximized = value
                My.Settings.PREFERRED_HDMI_MAXIMIZED = value
                My.Settings.Save()
            End Set
        End Property
        Private _IntensityVideoMaximized As Boolean = False

    End Class

    <Serializable()> _
    Public Class cProcAmpTemplate

        Public Sub New()
        End Sub

        Public Sub New(ByRef _PP As Phoenix_Form)
            PP = _PP
            Me.Brightness = 0
            Me.Contrast = -80
            Me.Hue = 0
            Me.Saturation = -80
        End Sub

        <NonSerialized(), XmlIgnore()> _
        Private PP As Phoenix_Form

        Public Name As String

        <XmlIgnore()> _
        Public Property Brightness() As Short
            Get
                Return _Brightness
            End Get
            Set(ByVal Value As Short)
                _Brightness = Value
                Dim out As Double
                'range is -255 to 255
                If Not PP Is Nothing AndAlso Not PP.Player Is Nothing Then
                    out = CDbl(2.55 * Value)
                    PP.Player.ProcAmp_Brightness = out
                End If
            End Set
        End Property
        Public _Brightness As Short

        <XmlIgnore()> _
        Public Property Contrast() As Short
            Get
                Return _Contrast
            End Get
            Set(ByVal Value As Short)
                _Contrast = Value
                Dim out As Double
                'range = 0 to 10
                If Value < 0 Then
                    ''-100 to -1
                    out = Value / 100 'get it as a percent
                    out = out * 5 'how far is this percentage towards five?
                    out = Math.Abs(out)
                    out = 5 - out
                ElseIf Value = 0 Then
                    out = 5
                ElseIf Value > 0 Then
                    out = Value / 100 'get it as a percent
                    out = out * 5 'how far is this percentage towards five?
                    out = out + 5 'shift it up to be between 5 and 10
                End If
                If Not PP Is Nothing AndAlso Not PP.Player Is Nothing Then
                    PP.Player.ProcAmp_Contrast = out
                End If
            End Set
        End Property
        Public _Contrast As Short

        <XmlIgnore()> _
        Public Property Saturation() As Short
            Get
                Return _Saturation
            End Get
            Set(ByVal Value As Short)
                _Saturation = Value
                '0.0 to 10.0, default 1.0
                Dim out As Double
                If Value < 0 Then
                    out = Value / 100 'get it as a percent
                    out = out * 5 'how far is this percentage towards five?
                    out = Math.Abs(out)
                    out = 5 - out
                ElseIf Value = 0 Then
                    out = 5
                ElseIf Value > 0 Then
                    out = Value / 100 'get it as a percent
                    out = out * 5 'how far is this percentage towards five?
                    out = out + 5 'shift it up to be between 5 and 10
                End If
                If Not PP Is Nothing AndAlso Not PP.Player Is Nothing Then
                    PP.Player.ProcAmp_Saturation = out
                End If
            End Set
        End Property
        Public _Saturation As Short

        <XmlIgnore()> _
        Public Property Hue() As Short
            Get
                Return _Hue
            End Get
            Set(ByVal Value As Short)
                _Hue = Value
                Dim out As Double
                '-180.0 to +180.0, default 0.0
                If Not PP Is Nothing AndAlso Not PP.Player Is Nothing Then
                    out = CDbl(1.88 * Value)
                    PP.Player.ProcAmp_Hue = out
                End If
            End Set
        End Property
        Public _Hue As Short

    End Class

#End Region 'App Options

#Region "Key Mapping"

    <Serializable()> _
    Public Class cKeyMapping

        Public Sub New()
            SetupKeyStrokeArrays() 'VITAL!!!
            SetupKeymapingFunctionList()
        End Sub

        Public Sub FinishDeSerialization()
            Try
                For Each pf As cPhoenixFunction In CurrentKeyMapping
                    pf.pfKeyEventArgs = New KeyEventArgs(pf._SerializedKeyData)
                Next
            Catch ex As Exception
                Throw New Exception("Problem with FinishDeSerialization(). Error: " & ex.Message)
            End Try
        End Sub

        Public CurrentKeyMapping() As cPhoenixFunction

        <XmlIgnore()> _
        Public KeyMappingPlainTextPath As String = ""

        <XmlIgnore()> _
        Public nKeys As colKeys

        <XmlIgnore()> _
        Public KeyMappingFunctions() As String

        Private Sub SetupKeyStrokeArrays()
            Try
                nKeys = New colKeys

                nKeys.Add(Keys.A)
                nKeys.Add(Keys.B)
                nKeys.Add(Keys.C)
                nKeys.Add(Keys.D)
                nKeys.Add(Keys.E)
                nKeys.Add(Keys.F)
                nKeys.Add(Keys.G)
                nKeys.Add(Keys.H)
                nKeys.Add(Keys.I)
                nKeys.Add(Keys.J)
                nKeys.Add(Keys.K)
                nKeys.Add(Keys.L)
                nKeys.Add(Keys.M)
                nKeys.Add(Keys.N)
                nKeys.Add(Keys.O)
                nKeys.Add(Keys.P)
                nKeys.Add(Keys.Q)
                nKeys.Add(Keys.R)
                nKeys.Add(Keys.S)
                nKeys.Add(Keys.T)
                nKeys.Add(Keys.U)
                nKeys.Add(Keys.V)
                nKeys.Add(Keys.W)
                nKeys.Add(Keys.X)
                nKeys.Add(Keys.Y)
                nKeys.Add(Keys.Z)
                nKeys.Add(Keys.D1)
                nKeys.Add(Keys.D2)
                nKeys.Add(Keys.D3)
                nKeys.Add(Keys.D4)
                nKeys.Add(Keys.D5)
                nKeys.Add(Keys.D6)
                nKeys.Add(Keys.D7)
                nKeys.Add(Keys.D8)
                nKeys.Add(Keys.D9)
                nKeys.Add(Keys.D0)
                nKeys.Add(Keys.Space)

                nKeys.Add(Keys.F1)
                nKeys.Add(Keys.F2)
                nKeys.Add(Keys.F3)
                nKeys.Add(Keys.F4)
                nKeys.Add(Keys.F5)
                nKeys.Add(Keys.F6)
                nKeys.Add(Keys.F7)
                nKeys.Add(Keys.F8)
                nKeys.Add(Keys.F9)
                nKeys.Add(Keys.F10)
                nKeys.Add(Keys.F11)
                nKeys.Add(Keys.F12)

                nKeys.Add(Keys.End)
                nKeys.Add(Keys.Home)
                nKeys.Add(Keys.PageUp)
                nKeys.Add(Keys.PageDown)
                nKeys.Add(Keys.Insert)
                nKeys.Add(Keys.Delete)

                nKeys.Add(Keys.OemPeriod)
                nKeys.Add(Keys.Oemcomma)
                'nKeys.Add(Keys.Oemtilde)   'assigned to Viewer.ViewerSize = Fullscreen
                'nKeys.Add(Keys.OemOpenBrackets)    'can't support because keys.tostring on this comes up with oem#
                'nKeys.Add(Keys.OemCloseBrackets)   'can't support because keys.tostring on this comes up with oem#
                nKeys.Add(Keys.OemPipe)
                nKeys.Add(Keys.OemMinus)
                nKeys.Add(Keys.Oemplus)
                'nKeys.Add(Keys.OemSemicolon)       'can't support because keys.tostring on this comes up with oem#
                nKeys.Add(Keys.OemQuestion)

                nKeys.Add(Keys.NumPad0)
                nKeys.Add(Keys.NumPad1)
                nKeys.Add(Keys.NumPad2)
                nKeys.Add(Keys.NumPad3)
                nKeys.Add(Keys.NumPad4)
                nKeys.Add(Keys.NumPad5)
                nKeys.Add(Keys.NumPad6)
                nKeys.Add(Keys.NumPad7)
                nKeys.Add(Keys.NumPad8)
                nKeys.Add(Keys.NumPad9)

                'nKeys.Add(CType(DecimalToBinary(111), Keys)) '"NumDivide" 
                'nKeys.Add(New cKey("NumMultiply", DecimalToBinary(106)))
                'nKeys.Add(New cKey("NumSubtract", DecimalToBinary(109)))
                'nKeys.Add(New cKey("NumAdd", DecimalToBinary(107)))
                'nKeys.Add(New cKey("NumDecimalPoint", DecimalToBinary(110)))

                nKeys.Add(Keys.None)

            Catch ex As Exception
                Throw New Exception("Problem with SetupKeyStrokeArrays(). Error: " & ex.Message)
            End Try
        End Sub

        Private Sub SetupKeymapingFunctionList()
            Try
                Me.AddKeymappingFunction("Stop")
                'Me.AddKeymappingFunction("Play")
                'Me.AddKeymappingFunction("Pause")
                Me.AddKeymappingFunction("FastForward")
                Me.AddKeymappingFunction("Rewind")
                Me.AddKeymappingFunction("CycleAudio")
                Me.AddKeymappingFunction("CycleAngle")
                Me.AddKeymappingFunction("CycleSubtitles")
                Me.AddKeymappingFunction("RootMenu")
                Me.AddKeymappingFunction("TitleMenu")
                Me.AddKeymappingFunction("AngleMenu")
                Me.AddKeymappingFunction("ChapterMenu")
                Me.AddKeymappingFunction("SubtitleMenu")
                Me.AddKeymappingFunction("AudioMenu")
                Me.AddKeymappingFunction("ChapterBack")
                Me.AddKeymappingFunction("ChapterNext")
                Me.AddKeymappingFunction("JumpBack")
                Me.AddKeymappingFunction("FrameStep")
                Me.AddKeymappingFunction("Resume")
                Me.AddKeymappingFunction("GoUp")
                Me.AddKeymappingFunction("GoToEnd")
                Me.AddKeymappingFunction("GrabFrame")
                Me.AddKeymappingFunction("GoToLayerbreak")
                Me.AddKeymappingFunction("ToggleCCs")
                Me.AddKeymappingFunction("SlowForward")
                Me.AddKeymappingFunction("Eject")
                Me.AddKeymappingFunction("SelectProject")
                Me.AddKeymappingFunction("BootProject")
                'Me.AddKeymappingFunction("Help")
                'Me.AddKeymappingFunction("Exit")
                Me.AddKeymappingFunction("ReSync")
                Me.AddKeymappingFunction("BurnSourceTimecode")
                Me.AddKeymappingFunction("ActionTitleGuides")
                Me.AddKeymappingFunction("NewMarker")
                Me.AddKeymappingFunction("MultiFrameGrab")
                Me.AddKeymappingFunction("TimeSearchAccelerator")
                Me.AddKeymappingFunction("SplitFields")
                'Me.AddKeymappingFunction("AdvancedVideoSettings")
                Me.AddKeymappingFunction("RestartChapter")
                'Me.AddKeymappingFunction("GoToPreNextChapter_Next")
                'Me.AddKeymappingFunction("GoToPreNextChapter_Back")
                Me.AddKeymappingFunction("ToggleSubtitles")
                Me.AddKeymappingFunction("ChapterSearchAccelerator")
                Me.AddKeymappingFunction("TitleSearchAccelerator")
                Me.AddKeymappingFunction("PlayPauseToggle")
                Me.AddKeymappingFunction("AB_Set_A")
                Me.AddKeymappingFunction("AB_Set_B")
                Me.AddKeymappingFunction("AB_Clear")
                Me.AddKeymappingFunction("AudioSelectAccelerator")
                Me.AddKeymappingFunction("SubtitleSelectAccelerator")
            Catch ex As Exception
                Throw New Exception("Problem with SetupKeymapingFunctionList(). Error: " & ex.Message)
            End Try
        End Sub

        Public Sub AddKeymappingFunction(ByVal FN As String)
            Try
                If Me.KeyMappingFunctions Is Nothing Then ReDim KeyMappingFunctions(-1)
                ReDim Preserve Me.KeyMappingFunctions(UBound(KeyMappingFunctions) + 1)
                Me.KeyMappingFunctions(UBound(Me.KeyMappingFunctions)) = FN
            Catch ex As Exception
                Throw New Exception("Problem with AddKeymappingFunction(). Error: " & ex.Message)
            End Try
        End Sub

        Public Sub InitializeDefaultKeymapping()
            Try

                Dim nCurrentKeyMapping As New colPhoenixFunctions

                nCurrentKeyMapping.Add(New cPhoenixFunction("Stop", New KeyEventArgs(Keys.S)))
                'nCurrentKeyMapping.Add(New cPhoenixFunction("Play", New KeyEventArgs(Keys.Space)))
                'nCurrentKeyMapping.Add(New cPhoenixFunction("Pause", New KeyEventArgs(Keys.Space))) 'does control space work?
                nCurrentKeyMapping.Add(New cPhoenixFunction("FastForward", New KeyEventArgs(Keys.M)))
                nCurrentKeyMapping.Add(New cPhoenixFunction("Rewind", New KeyEventArgs(Keys.V)))
                nCurrentKeyMapping.Add(New cPhoenixFunction("CycleAudio", New KeyEventArgs(Keys.A)))
                nCurrentKeyMapping.Add(New cPhoenixFunction("CycleAngle", New KeyEventArgs(Keys.A Or Keys.Control)))
                nCurrentKeyMapping.Add(New cPhoenixFunction("CycleSubtitles", New KeyEventArgs(Keys.S Or Keys.Control)))
                nCurrentKeyMapping.Add(New cPhoenixFunction("RootMenu", New KeyEventArgs(Keys.R)))
                nCurrentKeyMapping.Add(New cPhoenixFunction("TitleMenu", New KeyEventArgs(Keys.T)))
                nCurrentKeyMapping.Add(New cPhoenixFunction("AngleMenu", New KeyEventArgs(Keys.Y)))
                nCurrentKeyMapping.Add(New cPhoenixFunction("ChapterMenu", New KeyEventArgs(Keys.C)))
                nCurrentKeyMapping.Add(New cPhoenixFunction("SubtitleMenu", New KeyEventArgs(Keys.W)))
                nCurrentKeyMapping.Add(New cPhoenixFunction("AudioMenu", New KeyEventArgs(Keys.Q)))
                nCurrentKeyMapping.Add(New cPhoenixFunction("ChapterBack", New KeyEventArgs(Keys.B)))
                nCurrentKeyMapping.Add(New cPhoenixFunction("ChapterNext", New KeyEventArgs(Keys.N)))
                nCurrentKeyMapping.Add(New cPhoenixFunction("JumpBack", New KeyEventArgs(Keys.J)))
                nCurrentKeyMapping.Add(New cPhoenixFunction("FrameStep", New KeyEventArgs(Keys.OemPeriod)))
                nCurrentKeyMapping.Add(New cPhoenixFunction("Resume", New KeyEventArgs(Keys.R Or Keys.Control)))
                nCurrentKeyMapping.Add(New cPhoenixFunction("GoUp", New KeyEventArgs(Keys.U)))
                nCurrentKeyMapping.Add(New cPhoenixFunction("GoToEnd", New KeyEventArgs(Keys.End)))
                nCurrentKeyMapping.Add(New cPhoenixFunction("GrabFrame", New KeyEventArgs(Keys.G)))
                nCurrentKeyMapping.Add(New cPhoenixFunction("GoToLayerbreak", New KeyEventArgs(Keys.OemPipe Or Keys.Control)))
                nCurrentKeyMapping.Add(New cPhoenixFunction("ToggleCCs", New KeyEventArgs(Keys.L)))
                nCurrentKeyMapping.Add(New cPhoenixFunction("SlowForward", New KeyEventArgs(Keys.P Or Keys.Control)))
                nCurrentKeyMapping.Add(New cPhoenixFunction("Eject", New KeyEventArgs(Keys.F2)))
                nCurrentKeyMapping.Add(New cPhoenixFunction("SelectProject", New KeyEventArgs(Keys.F3)))
                nCurrentKeyMapping.Add(New cPhoenixFunction("BootProject", New KeyEventArgs(Keys.F1)))
                'nCurrentKeyMapping.Add(New cPhoenixFunction("Help", Nothing))
                'nCurrentKeyMapping.Add(New cPhoenixFunction("Exit", Nothing))
                nCurrentKeyMapping.Add(New cPhoenixFunction("ReSync", New KeyEventArgs(Keys.K)))
                nCurrentKeyMapping.Add(New cPhoenixFunction("BurnSourceTimecode", New KeyEventArgs(Keys.X)))
                nCurrentKeyMapping.Add(New cPhoenixFunction("ActionTitleGuides", New KeyEventArgs(Keys.Z)))
                nCurrentKeyMapping.Add(New cPhoenixFunction("NewMarker", New KeyEventArgs(Keys.Oemplus)))

                nCurrentKeyMapping.Add(New cPhoenixFunction("MultiFrameGrab", New KeyEventArgs(Keys.G Or Keys.Control)))
                nCurrentKeyMapping.Add(New cPhoenixFunction("TimeSearchAccelerator", New KeyEventArgs(Keys.F5))) 'F5
                nCurrentKeyMapping.Add(New cPhoenixFunction("ChapterSearchAccelerator", New KeyEventArgs(Keys.F6))) 'F6
                nCurrentKeyMapping.Add(New cPhoenixFunction("TitleSearchAccelerator", New KeyEventArgs(Keys.F7))) 'F7
                nCurrentKeyMapping.Add(New cPhoenixFunction("AudioSelectAccelerator", New KeyEventArgs(Keys.F8)))
                nCurrentKeyMapping.Add(New cPhoenixFunction("SubtitleSelectAccelerator", New KeyEventArgs(Keys.F9)))

                nCurrentKeyMapping.Add(New cPhoenixFunction("SplitFields", New KeyEventArgs(Keys.F10)))
                'nCurrentKeyMapping.Add(New cPhoenixFunction("AdvancedVideoSettings", Nothing)) 

                nCurrentKeyMapping.Add(New cPhoenixFunction("RestartChapter", New KeyEventArgs(Keys.Home))) 'Home
                'nCurrentKeyMapping.Add(New cPhoenixFunction("GoToPreNextChapter_Next", DecimalToBinary(72))) 'H
                'nCurrentKeyMapping.Add(New cPhoenixFunction("GoToPreNextChapter_Back", DecimalToBinary(73))) '
                nCurrentKeyMapping.Add(New cPhoenixFunction("ToggleSubtitles", New KeyEventArgs(Keys.D))) '
                nCurrentKeyMapping.Add(New cPhoenixFunction("PlayPauseToggle", New KeyEventArgs(Keys.Space))) '

                nCurrentKeyMapping.Add(New cPhoenixFunction("AB_Set_A", New KeyEventArgs(Keys.O))) '
                nCurrentKeyMapping.Add(New cPhoenixFunction("AB_Set_B", New KeyEventArgs(Keys.P))) '
                nCurrentKeyMapping.Add(New cPhoenixFunction("AB_Clear", New KeyEventArgs(Keys.OemPipe)))
                'REMBEMBER TO ADD NEW ITEMS TO SetupKeymapingFunctionList and KeyStrikeExecutionEngine!!!


                'For Each f As cPhoenixFunction In nCurrentKeyMapping
                '    f.KeyData = PadString(f.KeyData, 19, "0", True)
                'Next

                'We've setup the default keymapping
                ReDim CurrentKeyMapping(nCurrentKeyMapping.Count - 1)
                For i As Short = 0 To nCurrentKeyMapping.Count - 1
                    CurrentKeyMapping(i) = nCurrentKeyMapping.Item(i)
                Next

            Catch ex As Exception
                Throw New Exception("Problem with SetupKeyMapping(). Error: " & ex.Message)
            End Try
        End Sub

        Public Function FindKMFunctionByName(ByVal Name As String) As cPhoenixFunction
            Try
                For Each PF As cPhoenixFunction In CurrentKeyMapping
                    If PF.FunctionName.ToLower = Name.ToLower Then
                        Return PF
                    End If
                Next
                Return Nothing
            Catch ex As Exception
                Debug.WriteLine("Problem with FindFunctionByName. Error: " & ex.Message)
                Return Nothing
            End Try
        End Function

        Public Function FindKMFunctionByNameReturnIndex(ByVal Name As String) As Short
            Try
                For i As Short = 0 To CurrentKeyMapping.Length - 1
                    If CurrentKeyMapping(i).FunctionName.ToLower = Name.ToLower Then
                        Return i
                    End If
                Next
                Return -1
            Catch ex As Exception
                Debug.WriteLine("Problem with FindKMFunctionByNameReturnIndex. Error: " & ex.Message)
                Return -1
            End Try
        End Function

        Public Function SavePlainTextString(ByVal PromptForPath As Boolean) As String
            Try
                Dim outFilePath As String = ""
                If PromptForPath Then
                    Dim dlg As New SaveFileDialog
                    dlg.AddExtension = True
                    dlg.DefaultExt = ".txt"
                    dlg.Filter = "Text (*.txt)|*.txt"
                    dlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
                    dlg.OverwritePrompt = True
                    dlg.Title = My.Settings.APPLICATION_NAME
                    If dlg.ShowDialog = DialogResult.OK Then
                        outFilePath = dlg.FileName
                    Else
                        Return ""
                    End If
                Else
                    Dim Desktop As String = Path.GetDirectoryName(Environment.GetFolderPath(Environment.SpecialFolder.Desktop))
                    outFilePath = Desktop & "\" & "PhoenixKeymapping_PlainText.txt"
                End If

                If File.Exists(outFilePath) Then File.Delete(outFilePath)

                Dim KMS As String = Me.GetKeyMappingString
                'Dim KMSPath As String = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) & "\Phoenix_KeyMapping.txt"
                Dim FS As New FileStream(outFilePath, FileMode.OpenOrCreate)
                Dim SW As New StreamWriter(FS)
                SW.Write(KMS)
                SW.Close()
                FS.Close()
                Return outFilePath
            Catch ex As Exception
                Throw New Exception("Problem with KeyMapping_SavePlainTextString. Error: " & ex.Message)
            End Try
        End Function

        Public Function GetKeyMappingString() As String
            Try
                Dim XS As New XmlSerializer(GetType(cPhoenixFunction()))
                Dim TW As New StringWriter
                XS.Serialize(TW, Me.CurrentKeyMapping)
                XS = Nothing
                Dim XML As String = TW.ToString

                'Now, convert the xml into friendly text.
                Dim SR As New StringReader(XML)
                Dim Out As String

                Out = My.Settings.APPLICATION_NAME & " Key Mapping" & vbNewLine & vbNewLine

                'move up to the fourth line
                SR.ReadLine()
                SR.ReadLine()
                SR.ReadLine()
                Dim tFunction, tKey As String
                tKey = SR.ReadLine
                While Not tKey Is Nothing
                    tKey = Replace(tKey, "<_SerializedKeyData>", "", 1, -1, CompareMethod.Text)
                    tKey = Replace(tKey, "</_SerializedKeyData>", "", 1, -1, CompareMethod.Text)
                    tKey = Trim(tKey)
                    If tKey <> "None" Then
                        tKey = KeyEventArgsFriendlyString(New KeyEventArgs(tKey))
                    End If

                    tFunction = SR.ReadLine
                    tFunction = Replace(tFunction, "<FunctionName>", "", 1, -1, CompareMethod.Text)
                    tFunction = Replace(tFunction, "</FunctionName>", "", 1, -1, CompareMethod.Text)
                    tFunction = Trim(tFunction)

                    SR.ReadLine() '</cPhoenixFunction>
                    SR.ReadLine() '<cPhoenixFunction>

                    Out &= PadString(tFunction, 30, " ", True) & " - " & tKey & vbNewLine
                    tKey = SR.ReadLine
                    If tKey Is Nothing Then
                        Return Out
                    End If
                End While
            Catch ex As Exception
                Throw New Exception("Problem getting key mapping as string for printing. Error: " & ex.Message, ex)
            End Try
        End Function

        Public Function KeyDataToString(ByVal KEA As KeyEventArgs) As String
            Try
                'Dim kF As New KeyEventArgs(BinToDec(BinaryKeyData))
                'Dim Bin As String = Microsoft.VisualBasic.Right(BinaryKeyData, 8)
                'Dim Dec As Short = BinToDec(Bin)
                'Dim k As New KeyEventArgs(Dec)

                Dim s As String = ""
                If KEA.Control Then
                    s &= "Control-"
                End If
                If KEA.Alt Then
                    s &= "Alt-"
                End If
                If KEA.Shift Then
                    s &= "Shift-"
                End If
                s &= KEA.KeyCode.ToString
                If InStr(s, "Prior") Then
                    s = Replace(s, "Prior", "PageUp")
                End If
                If InStr(s, "Next") Then
                    s = Replace(s, "Next", "PageDown")
                End If
                Return Replace(s, "Oem", "")
            Catch ex As Exception
                Throw New Exception("Problem with KeyDataToString(). Error: " & ex.Message)
            End Try
        End Function

        Public Function KeyEventArgsFriendlyString(ByVal KEA As KeyEventArgs) As String
            Try
                If KEA.Control Then
                    'Return "Control-" & [Enum].GetName(GetType(Keys), KEA.KeyCode)
                    Return "Control-" & [Enum].Parse(GetType(Keys), KEA.KeyCode).ToString()
                Else
                    'Return [Enum].GetName(GetType(Keys), KEA.KeyCode)
                    Return [Enum].Parse(GetType(Keys), KEA.KeyCode).ToString
                End If
            Catch ex As Exception
                Throw New Exception("Problem with KeyEventArgsFriendlyString. Error: " & ex.Message, ex)
            End Try
        End Function

    End Class

    '<Serializable()> _
    'Public Class cKey
    '    Private _KeyName As String
    '    Private _KeyData As String

    '    Public Sub New()
    '    End Sub

    '    Public Sub New(ByVal KeyName As String, ByVal KeyData As String)
    '        _KeyName = KeyName
    '        _KeyData = KeyData
    '    End Sub

    '    Public Overloads Overrides Function ToString() As String
    '        Return _KeyName
    '    End Function

    '    Public Property KeyName() As String
    '        Get
    '            Return _KeyName
    '        End Get
    '        Set(ByVal Value As String)
    '            _KeyName = Value
    '        End Set
    '    End Property

    '    Public Property KeyData() As String
    '        Get
    '            Return _KeyData
    '        End Get
    '        Set(ByVal Value As String)
    '            _KeyData = Value
    '        End Set
    '    End Property

    'End Class

    <Serializable()> _
    Public Class cPhoenixFunction

        Public Sub New()
        End Sub

        Public Sub New(ByVal FunctionName As String, ByVal KeyCode As KeyEventArgs)
            _FunctionName = FunctionName
            _KeyCode = KeyCode
            _SerializedKeyData = KeyCode.KeyData
        End Sub

        Public Overloads Overrides Function ToString() As String
            Return _FunctionName
        End Function

        Public Property FunctionName() As String
            Get
                Return _FunctionName
            End Get
            Set(ByVal Value As String)
                _FunctionName = Value
            End Set
        End Property
        Private _FunctionName As String

        <XmlIgnore()> _
        Public Property pfKeyEventArgs() As KeyEventArgs
            Get
                Return _KeyCode
            End Get
            Set(ByVal value As KeyEventArgs)
                _KeyCode = value
                _SerializedKeyData = value.KeyData
            End Set
        End Property
        Private _KeyCode As KeyEventArgs

        Public _SerializedKeyData As Integer

    End Class

    Public Class colPhoenixFunctions
        Inherits CollectionBase

        Public Function Add(ByVal cPF As cPhoenixFunction) As Integer
            Return MyBase.List.Add(cPF)
        End Function

        Default Public ReadOnly Property Item(ByVal index As Integer) As cPhoenixFunction
            Get
                Return MyBase.List.Item(index)
            End Get
        End Property

        Public Sub Remove(ByVal Item As cPhoenixFunction)
            MyBase.List.Remove(Item)
        End Sub

    End Class

    Public Class colKeys
        Inherits CollectionBase

        Public Function Add(ByVal cK As Keys) As Integer
            Return MyBase.List.Add(cK)
        End Function

        Default Public ReadOnly Property Item(ByVal index As Integer) As Keys
            Get
                Return MyBase.List.Item(index)
            End Get
        End Property

        Public Sub Remove(ByVal Item As Keys)
            MyBase.List.Remove(Item)
        End Sub

    End Class

#End Region 'Key Mapping

#Region "Docking Layout"

    <Serializable()> _
    Public Class cDockingLayout

        <XmlIgnore()> _
        Public PP As Phoenix_Form

        Public Sub New()
        End Sub

        Public Sub New(ByVal nPP As Phoenix_Form)
            PP = nPP
        End Sub

        Public DockingLayoutXML As String

        Public Sub Update()
            DockingLayoutXML = Me.CurrentDockingWindowXML
        End Sub

        Private Function CurrentDockingWindowXML() As String
            Try
                Dim MS As New MemoryStream
                PP.barMain.SaveLayoutToStream(MS)
                Return StreamToString(MS)
            Catch ex As Exception
                Throw New Exception("Problem with GetDockingWindowXML(). Error: " & ex.Message)
            End Try
        End Function

        Private Function StreamToString(ByVal STR As MemoryStream) As String
            Try
                STR.Position = 0
                Dim sr As New StreamReader(STR)
                Dim out As String = sr.ReadToEnd
                sr.Close()
                STR.Close()
                Return out
            Catch ex As Exception
                Throw New Exception("Problem with MemoryStreamToString. Error: " & ex.Message)
            End Try
        End Function

        Public Sub Apply()
            Try
                Dim MS As New MemoryStream()
                Dim enc As New UTF8Encoding
                Dim arrBytData() As Byte = enc.GetBytes(Me.DockingLayoutXML)
                MS.Write(arrBytData, 0, arrBytData.Length)
                MS.Position = 0
                PP.barMain.RestoreLayoutFromStream(MS)
                MS.Close()
                MS = Nothing

                ''debugging
                'Dim MS As New MemoryStream(Me.DockingLayoutXML.Length + 1024)
                'Dim SW As New StreamWriter(MS)
                'SW.Write(Me.DockingLayoutXML)

                'Dim sr As New StreamReader(MS)
                'sr.BaseStream.Position = 0
                'Debug.Write(sr.ReadToEnd)
                'sr.BaseStream.Position = 0
                'Dim str As String = sr.ReadToEnd
                ''debugging
            Catch ex As Exception
                Throw New Exception("Problem with ApplyDockingLayout(). Error: " & ex.Message)
            End Try
        End Sub

        Public Sub InitializeDefaultLayout(ByVal Layout As String)
            Try
                Select Case Layout.ToLower
                    Case "std"
std:
                        Dim Assm As System.Reflection.Assembly = Me.GetType().Assembly.GetEntryAssembly
                        Dim LayoutXML As Stream = Assm.GetManifestResourceStream("SMT.Applications.Phoenix.StandardUILayout.xml")
                        Dim SR As New StreamReader(LayoutXML)
                        Me.DockingLayoutXML = SR.ReadToEnd
                        LayoutXML.Close()

                    Case "adv"
                    Case "stm"
                    Case Else
                        GoTo std
                End Select
            Catch ex As Exception
                Throw New Exception("Problem with LoadDefaultLayout(). Error: " & ex.Message)
            End Try
        End Sub

    End Class

#End Region 'Docking Layout

#End Region 'USER PROFILE

#Region "MACHINE PROFILE"

    'These are options/settings/etc that shall remain persistent on the local system (ie are not stored in the user profile)

    Public Class cUserOperationTemplates

        Public Templates() As cUserOperationTemplate
        Public CurrentIndex As Short = -1

        Public ReadOnly Property CurrentName() As String
            Get
                If CurrentIndex = -1 Then Return ""
                Return Me.Templates(CurrentIndex).Name
            End Get
        End Property

        Public Sub New()
        End Sub

        Public Sub Add(ByVal Template As cUserOperationTemplate)
            ReDim Preserve Me.Templates(UBound(Templates) + 1)
            Templates(UBound(Templates)) = Template
        End Sub

        Public Sub Delete(ByVal index As Integer)
            Try
                If Me.Templates Is Nothing Then Exit Sub
                If index > UBound(Templates) Then Exit Sub
                If index = CurrentIndex Then CurrentIndex = -1

                Dim tempNewTemplates(Me.Templates.Length - 2) As cUserOperationTemplate

                If index = 0 Then
                    'just skip the first item
                    Array.Copy(Me.Templates, 1, tempNewTemplates, 0, tempNewTemplates.Length)
                    Me.Templates = tempNewTemplates
                ElseIf index = UBound(Templates) Then
                    'just redim to remove the last item
                    ReDim Preserve Me.Templates(UBound(Templates) - 1)
                Else
                    'ok it's a middle item, more complex...
                    Array.Copy(Me.Templates, 0, tempNewTemplates, 0, index)
                    Array.Copy(Me.Templates, index + 1, tempNewTemplates, index, UBound(Templates) - index + 1)
                    Me.Templates = tempNewTemplates
                End If
            Catch ex As Exception
                Throw New Exception("Problem with Delete(). Error: " & ex.Message, ex)
            End Try
        End Sub

        Public Sub Rename(ByVal index As Integer, ByVal NewName As String)
            If Me.Templates Is Nothing Then Exit Sub
            If index > UBound(Templates) Then Exit Sub
            Me.Templates(index).Name = NewName
        End Sub

        Public Sub Update(ByVal index As Integer, ByVal Template As cUserOperationTemplate)
            If Me.Templates Is Nothing Then Exit Sub
            If index > UBound(Templates) Then Exit Sub
            Me.Templates(index) = Template
        End Sub

        Public Sub Save()
            Try
                Dim XS As New XmlSerializer(GetType(cUserOperationTemplates))
                Dim TW As New StringWriter
                XS.Serialize(TW, Me)
                XS = Nothing
                Dim Out As String = TW.ToString
                My.Settings.UOP_TEMPLATES = Out
                My.Settings.Save()
            Catch ex As Exception
                Throw New Exception("Problem with Save(). Error: " & ex.Message, ex)
            End Try
        End Sub

        Public Sub Load()
            Try
                If My.Settings.UOP_TEMPLATES = "" Then
                    ReDim Me.Templates(-1)
                Else
                    Dim SR As New StringReader(My.Settings.UOP_TEMPLATES)
                    Dim XS As New XmlSerializer(GetType(cUserOperationTemplates))
                    Dim tmp As cUserOperationTemplates = CType(XS.Deserialize(SR), cUserOperationTemplates)
                    XS = Nothing
                    SR = Nothing
                    Me.Templates = tmp.Templates
                End If
            Catch ex As Exception
                Throw New Exception("Problem with Load(). Error: " & ex.Message, ex)
            End Try
        End Sub

        Public Function ApplyByName(ByVal Name As String) As Boolean
            Try
                If Templates Is Nothing Then Return False
                For i As Short = 0 To UBound(Templates)
                    If Templates(i).Name = Name Then
                        Me.CurrentIndex = i
                        Return True
                    End If
                Next
                Return False
            Catch ex As Exception
                Throw New Exception("Problem with ApplyByName(). Error: " & ex.Message, ex)
            End Try
        End Function

    End Class

    Public Class cUserOperationTemplate

        Public Name As String
        Public UOPs As cUserOperations

        Public Sub New()
            UOPs = New cUserOperations
        End Sub

    End Class

    <Serializable()> _
    Public Class cGPRMWatchSet

        Public Name As String
        Public Items As List(Of cGPRMWatchItem)

        Public Sub New()
            Items = New List(Of cGPRMWatchItem)
        End Sub

        <Serializable()> _
        Public Class cGPRMWatchItem

            Public Name As String
            Public GPRM As Byte
            Public Offset As Byte
            Public Length As Byte

            Public Sub New()
            End Sub

            Public Sub New(ByVal nName As String, ByVal nGPRM As Byte, ByVal nOffset As Byte, ByVal nLength As Byte)
                Name = nName
                GPRM = nGPRM
                Offset = nOffset
                Length = nLength
            End Sub

        End Class

    End Class

#End Region 'MACHINE PROFILE

#Region "FEATURE MANAGEMENT"

    Public Class cPhoenixFeatureManagement

        Public Features As sControlledFunctionality

        Public Sub New(ByVal LicensedVersion As ePhoenixLicense)
            Features = New sControlledFunctionality
            Select Case LicensedVersion
                Case ePhoenixLicense.Ultimate
                    SetupFor_Ultimate()
                Case ePhoenixLicense.Pro
                    SetupFor_Pro()
                Case ePhoenixLicense.Mobile
                    SetupFor_Std()
                Case ePhoenixLicense.Trial
                    SetupFor_Ultimate()
                Case Else
                    Throw New Exception("Unexpected.")
            End Select
        End Sub

        Private Sub SetupFor_Ultimate()
            With Features
                ' AUDIO FORMAT
                .FE_AD_F_DTS = True
                .FE_AD_Dumping = True

                ' AV OUTPUT
                .FE_AVO_Blackmagic_Decklink = True
                .FE_AVO_Blackmagic_Intensity = True
                '.FE_AVO_Desktop = True

                ' BUTTONS
                .FE_BTN_BurnIn = True
                .FE_BTN_CommandList = True
                .FE_BTN_NavIndication = True

                ' DVD
                .FE_DVD_JacketPictures = True
                .FE_DVD_LayerbreakNotification = True
                .FE_DVD_NavigationCommandLogging = True
                .FE_DVD_ParentalManagement = True
                .FE_DVD_UOPTemplates = True

                ' GUIDES
                .FE_GI_ActionTitleSafe = True
                .FE_GI_Dynamic = True

                ' LINE-21
                .FE_L12_Positioning = True
                .FE_L21_BitmapExtraction = True
                .FE_L21_Decode = True
                .FE_L21_TextExtraction = True

                ' MPEG-2
                .FE_M2M_GOPTimecodes = True

                ' SUBPICTURES
                .FE_SP_HighContrast = True
                .FE_SP_Positioning = True
                .FE_SP_Dumping = True

                ' VIDEO ANALYSIS
                .FE_VA_FieldSplitting = True
                .FE_VA_FrameFiltering = True
                .FE_VA_ProcAmp = True
                .FE_VA_TestPatterns = True
                .FE_VA_TransferErrorDetection = True
                .FE_VA_YUVChannelFiltering = True
                .FE_VA_ReverseFieldOrder = True

                ' VIDEO DUMPING
                .FE_VDP_FullMix = True
                .FE_VDP_MultiFrame = True
                .FE_VDP_VideoOnly = True

                'OTHER
                .FE_OT_GPRMViewer = True

            End With
        End Sub

        Private Sub SetupFor_Pro()
            With Features
                ' AUDIO FORMAT
                .FE_AD_F_DTS = False
                .FE_AD_Dumping = False

                ' AV OUTPUT
                .FE_AVO_Blackmagic_Decklink = False
                .FE_AVO_Blackmagic_Intensity = True
                '.FE_AVO_Desktop = True

                ' BUTTONS
                .FE_BTN_BurnIn = False
                .FE_BTN_CommandList = False
                .FE_BTN_NavIndication = False

                ' DVD
                .FE_DVD_JacketPictures = True
                .FE_DVD_LayerbreakNotification = False
                .FE_DVD_NavigationCommandLogging = False
                .FE_DVD_ParentalManagement = True
                .FE_DVD_UOPTemplates = True

                ' GUIDES
                .FE_GI_ActionTitleSafe = True
                .FE_GI_Dynamic = False

                ' LINE-21
                .FE_L12_Positioning = False
                .FE_L21_BitmapExtraction = False
                .FE_L21_Decode = False
                .FE_L21_TextExtraction = False

                ' MPEG-2
                .FE_M2M_GOPTimecodes = True

                ' SUBPICTURES
                .FE_SP_HighContrast = False
                .FE_SP_Positioning = False
                .FE_SP_Dumping = False

                ' VIDEO ANALYSIS
                .FE_VA_FieldSplitting = False
                .FE_VA_FrameFiltering = False
                .FE_VA_ProcAmp = False
                .FE_VA_TestPatterns = False
                .FE_VA_TransferErrorDetection = False
                .FE_VA_YUVChannelFiltering = False
                .FE_VA_ReverseFieldOrder = False

                ' VIDEO DUMPING
                .FE_VDP_FullMix = True
                .FE_VDP_MultiFrame = False
                .FE_VDP_VideoOnly = True

                'OTHER
                .FE_OT_GPRMViewer = True

            End With
        End Sub

        Private Sub SetupFor_Std()
            With Features
                ' AUDIO FORMAT
                .FE_AD_F_DTS = False
                .FE_AD_Dumping = False

                ' AV OUTPUT
                .FE_AVO_Blackmagic_Decklink = False
                .FE_AVO_Blackmagic_Intensity = False
                '.FE_AVO_Desktop = True

                ' BUTTONS
                .FE_BTN_BurnIn = False
                .FE_BTN_CommandList = False
                .FE_BTN_NavIndication = False

                ' DVD
                .FE_DVD_JacketPictures = False
                .FE_DVD_LayerbreakNotification = False
                .FE_DVD_NavigationCommandLogging = False
                .FE_DVD_ParentalManagement = False
                .FE_DVD_UOPTemplates = False

                ' GUIDES
                .FE_GI_ActionTitleSafe = False
                .FE_GI_Dynamic = False

                ' LINE-21
                .FE_L12_Positioning = False
                .FE_L21_BitmapExtraction = False
                .FE_L21_Decode = False
                .FE_L21_TextExtraction = False

                ' MPEG-2
                .FE_M2M_GOPTimecodes = False

                ' SUBPICTURES
                .FE_SP_HighContrast = False
                .FE_SP_Positioning = False
                .FE_SP_Dumping = False

                ' VIDEO ANALYSIS
                .FE_VA_FieldSplitting = False
                .FE_VA_FrameFiltering = False
                .FE_VA_ProcAmp = False
                .FE_VA_TestPatterns = False
                .FE_VA_TransferErrorDetection = False
                .FE_VA_YUVChannelFiltering = False
                .FE_VA_ReverseFieldOrder = False

                ' VIDEO DUMPING
                .FE_VDP_FullMix = False
                .FE_VDP_MultiFrame = False
                .FE_VDP_VideoOnly = True

                'OTHER
                .FE_OT_GPRMViewer = False

            End With
        End Sub

        Public Structure sControlledFunctionality

            ' AV OUTPUT
            Public FE_AVO_Blackmagic_Intensity As Boolean
            Public FE_AVO_Blackmagic_Decklink As Boolean
            'Public FE_AVO_Desktop As Boolean

            ' AUDIO
            Public FE_AD_F_DTS As Boolean
            Public FE_AD_Dumping As Boolean

            ' DVD
            Public FE_DVD_ParentalManagement As Boolean
            Public FE_DVD_NavigationCommandLogging As Boolean
            Public FE_DVD_JacketPictures As Boolean
            Public FE_DVD_LayerbreakNotification As Boolean
            Public FE_DVD_UOPTemplates As Boolean

            ' BUTTONS
            Public FE_BTN_BurnIn As Boolean
            Public FE_BTN_CommandList As Boolean
            Public FE_BTN_NavIndication As Boolean

            ' LINE-21
            Public WriteOnly Property FE_L21_Group() As Boolean
                Set(ByVal value As Boolean)
                    FE_L12_Positioning = value
                    FE_L21_BitmapExtraction = value
                    FE_L21_Decode = value
                    FE_L21_TextExtraction = value
                End Set
            End Property
            Public FE_L21_Decode As Boolean
            Public FE_L12_Positioning As Boolean
            Public FE_L21_TextExtraction As Boolean
            Public FE_L21_BitmapExtraction As Boolean

            ' VIDEO DUMPING
            Public FE_VDP_FullMix As Boolean
            Public FE_VDP_VideoOnly As Boolean
            Public FE_VDP_MultiFrame As Boolean

            ' MPEG-2 METADATA
            Public FE_M2M_GOPTimecodes As Boolean

            ' VIDEO ANALYSIS
            Public WriteOnly Property VideoAnalysis() As Boolean
                Set(ByVal value As Boolean)
                    FE_VA_FrameFiltering = value
                    FE_VA_TestPatterns = value
                    FE_VA_ProcAmp = value
                    FE_VA_YUVChannelFiltering = value
                    FE_VA_FieldSplitting = value
                    FE_VA_TransferErrorDetection = value
                End Set
            End Property
            Public FE_VA_FrameFiltering As Boolean
            Public FE_VA_TestPatterns As Boolean
            Public FE_VA_ProcAmp As Boolean
            Public FE_VA_YUVChannelFiltering As Boolean
            Public FE_VA_FieldSplitting As Boolean
            Public FE_VA_TransferErrorDetection As Boolean
            Public FE_VA_ReverseFieldOrder As Boolean

            ' SUBPICTURES
            Public FE_SP_HighContrast As Boolean
            Public FE_SP_Positioning As Boolean
            Public FE_SP_Dumping As Boolean

            ' GUIDES
            Public FE_GI_ActionTitleSafe As Boolean
            Public FE_GI_Dynamic As Boolean

            ' OTHER
            Public FE_OT_GPRMViewer As Boolean
            'Public FE_OT_Markers As Boolean

        End Structure

    End Class

#End Region 'FEATURE MANAGEMENT

End Namespace

Namespace Enums

    Public Enum eDefaultProfileVersion
        Standard
        StreamQC
        Advanced
        Alternate1152x864
        _1920x1200
    End Enum

    Public Enum eSearchTypes
        Chapter
        Title
        RunningTime
        SourceTime
        SubtitleStream
        AudioStream
    End Enum

End Namespace
