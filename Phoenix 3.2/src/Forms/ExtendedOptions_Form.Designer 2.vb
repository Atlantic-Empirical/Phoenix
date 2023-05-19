<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ExtendedOptions_Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ExtendedOptions_Form))
        Me.tcMain = New DevExpress.XtraTab.XtraTabControl
        Me.tpOutputConfig = New DevExpress.XtraTab.XtraTabPage
        Me.gbHDMIAudio = New DevExpress.XtraEditors.GroupControl
        Me.Label11 = New System.Windows.Forms.Label
        Me.cbHDMIAudioFormat = New DevExpress.XtraEditors.ComboBoxEdit
        Me.gbAV_AnalogVideoConfig = New DevExpress.XtraEditors.GroupControl
        Me.Label9 = New System.Windows.Forms.Label
        Me.cbAV_AnalogFormat = New DevExpress.XtraEditors.ComboBoxEdit
        Me.GroupControl7 = New DevExpress.XtraEditors.GroupControl
        Me.btnApplyDeviceChanges = New DevExpress.XtraEditors.SimpleButton
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.rbIntensity = New System.Windows.Forms.RadioButton
        Me.rbDesktop = New System.Windows.Forms.RadioButton
        Me.rbDecklink = New System.Windows.Forms.RadioButton
        Me.gbHDMIScaling = New System.Windows.Forms.GroupBox
        Me.ceHDMIVidScl_Maximize = New DevExpress.XtraEditors.CheckEdit
        Me.rbHDMIVidScl_AdjustToAspect = New System.Windows.Forms.RadioButton
        Me.rbHDMIVidScl_Native = New System.Windows.Forms.RadioButton
        Me.gbHDMIResolution = New System.Windows.Forms.GroupBox
        Me.rbIntensityResolution_486 = New System.Windows.Forms.RadioButton
        Me.rbIntensityResolution_576 = New System.Windows.Forms.RadioButton
        Me.rbIntensityResolution_1080 = New System.Windows.Forms.RadioButton
        Me.rbIntensityResolution_720 = New System.Windows.Forms.RadioButton
        Me.tpFrameGrabViewer = New DevExpress.XtraTab.XtraTabPage
        Me.btnFGV_OpenTargetDir = New DevExpress.XtraEditors.SimpleButton
        Me.btnFGV_SaveCurrent = New DevExpress.XtraEditors.SimpleButton
        Me.btnFGV_DeleteCurrent = New DevExpress.XtraEditors.SimpleButton
        Me.cbFGV_MultiFrameDirectories = New DevExpress.XtraEditors.ComboBoxEdit
        Me.cbFGV_ViewMulti = New DevExpress.XtraEditors.CheckEdit
        Me.btnFGV_ViewNext = New DevExpress.XtraEditors.SimpleButton
        Me.btnFGV_ViewPrevious = New DevExpress.XtraEditors.SimpleButton
        Me.btnFGV_GrabFrame = New DevExpress.XtraEditors.SimpleButton
        Me.cmFGV_FrameGrabOptions = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.miFGV_FullMix = New System.Windows.Forms.ToolStripMenuItem
        Me.miFGV_VideoAndSubpicture = New System.Windows.Forms.ToolStripMenuItem
        Me.miFGV_VideoOnly = New System.Windows.Forms.ToolStripMenuItem
        Me.miFGV_SubpictureOnly = New System.Windows.Forms.ToolStripMenuItem
        Me.miFGV_ClosedCaptionsOnly = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator
        Me.miFGV_MultiFrame = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripSeparator
        Me.miFGV_Bitmap = New System.Windows.Forms.ToolStripMenuItem
        Me.miFGV_JPEG = New System.Windows.Forms.ToolStripMenuItem
        Me.miFGV_GIF = New System.Windows.Forms.ToolStripMenuItem
        Me.miFGV_PNG = New System.Windows.Forms.ToolStripMenuItem
        Me.miFGV_TIF = New System.Windows.Forms.ToolStripMenuItem
        Me.lblFGV_FileName = New System.Windows.Forms.Label
        Me.pbFrameGrab = New System.Windows.Forms.PictureBox
        Me.cmFGV_AspectRatio = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mi853480 = New System.Windows.Forms.ToolStripMenuItem
        Me.mi853576 = New System.Windows.Forms.ToolStripMenuItem
        Me.mi720480 = New System.Windows.Forms.ToolStripMenuItem
        Me.mi720576 = New System.Windows.Forms.ToolStripMenuItem
        Me.tpSubpicture = New DevExpress.XtraTab.XtraTabPage
        Me.gbSUB_Positioning = New DevExpress.XtraEditors.GroupControl
        Me.cbSPPlacementEnabled = New System.Windows.Forms.CheckBox
        Me.txtSPPlacement_Y = New System.Windows.Forms.TextBox
        Me.tbSPPlacement_X = New System.Windows.Forms.TrackBar
        Me.txtSPPlacement_X = New System.Windows.Forms.TextBox
        Me.Label23 = New System.Windows.Forms.Label
        Me.Label24 = New System.Windows.Forms.Label
        Me.tbSPPlacement_Y = New System.Windows.Forms.TrackBar
        Me.GroupBox17 = New System.Windows.Forms.GroupBox
        Me.Button5 = New System.Windows.Forms.Button
        Me.TextBox3 = New System.Windows.Forms.TextBox
        Me.TrackBar2 = New System.Windows.Forms.TrackBar
        Me.GroupBox19 = New System.Windows.Forms.GroupBox
        Me.Label18 = New System.Windows.Forms.Label
        Me.Label21 = New System.Windows.Forms.Label
        Me.Button3 = New System.Windows.Forms.Button
        Me.Button4 = New System.Windows.Forms.Button
        Me.Label22 = New System.Windows.Forms.Label
        Me.TextBox2 = New System.Windows.Forms.TextBox
        Me.tpVideo = New DevExpress.XtraTab.XtraTabPage
        Me.GroupControl8 = New DevExpress.XtraEditors.GroupControl
        Me.cbVID_NonSeamlessCellNotification = New System.Windows.Forms.CheckBox
        Me.GroupControl10 = New DevExpress.XtraEditors.GroupControl
        Me.cbVID_ReverseFieldOrder = New DevExpress.XtraEditors.CheckEdit
        Me.btnTestPattern = New DevExpress.XtraEditors.SimpleButton
        Me.btnColorBars = New DevExpress.XtraEditors.SimpleButton
        Me.cbSplitFields = New System.Windows.Forms.CheckBox
        Me.gbVID_ProcAmp = New DevExpress.XtraEditors.GroupControl
        Me.cbDoProcAmp = New System.Windows.Forms.CheckBox
        Me.cbProcAmpHalfFrame = New System.Windows.Forms.CheckBox
        Me.llPA_LoadSettings = New System.Windows.Forms.LinkLabel
        Me.llPA_DeleteSettings = New System.Windows.Forms.LinkLabel
        Me.tbBrightness = New System.Windows.Forms.TrackBar
        Me.llPA_SaveSettings = New System.Windows.Forms.LinkLabel
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.cbPA_Presets = New System.Windows.Forms.ComboBox
        Me.tbContrast = New System.Windows.Forms.TrackBar
        Me.Label6 = New System.Windows.Forms.Label
        Me.tbHue = New System.Windows.Forms.TrackBar
        Me.Label7 = New System.Windows.Forms.Label
        Me.tbSaturation = New System.Windows.Forms.TrackBar
        Me.txtContrast = New System.Windows.Forms.TextBox
        Me.txtBrightness = New System.Windows.Forms.TextBox
        Me.txtHue = New System.Windows.Forms.TextBox
        Me.txtSaturation = New System.Windows.Forms.TextBox
        Me.gbVID_MPEGFrameFilter = New DevExpress.XtraEditors.GroupControl
        Me.rbVidMode_I = New System.Windows.Forms.RadioButton
        Me.rbVidMode_IBP = New System.Windows.Forms.RadioButton
        Me.rbVidMode_IP = New System.Windows.Forms.RadioButton
        Me.gbVID_YUVFilter = New DevExpress.XtraEditors.GroupControl
        Me.rbFilters_none = New System.Windows.Forms.RadioButton
        Me.rbFilters_YUV_MinusU = New System.Windows.Forms.RadioButton
        Me.rbFilters_bandw = New System.Windows.Forms.RadioButton
        Me.rbFilters_YUV_MinusV = New System.Windows.Forms.RadioButton
        Me.cbBumpFields = New System.Windows.Forms.CheckBox
        Me.GroupBox8 = New System.Windows.Forms.GroupBox
        Me.btnDEINT_CheckStatus = New System.Windows.Forms.Button
        Me.GroupBox5 = New System.Windows.Forms.GroupBox
        Me.rbDEINT_Control_Video = New System.Windows.Forms.RadioButton
        Me.rbDEINT_Control_Film = New System.Windows.Forms.RadioButton
        Me.rbDEINT_Control_Smart = New System.Windows.Forms.RadioButton
        Me.rbDEINT_Control_Auto = New System.Windows.Forms.RadioButton
        Me.GroupBox9 = New System.Windows.Forms.GroupBox
        Me.rbDEINT_Mode_WeaveFiltered = New System.Windows.Forms.RadioButton
        Me.rbDEINT_Mode_Spad = New System.Windows.Forms.RadioButton
        Me.rbDEINT_Mode_Bob = New System.Windows.Forms.RadioButton
        Me.rbDEINT_Mode_Weave = New System.Windows.Forms.RadioButton
        Me.rbDEINT_Mode_Normal = New System.Windows.Forms.RadioButton
        Me.cbForceFieldReversal = New System.Windows.Forms.CheckBox
        Me.btnProcAmpScrap = New System.Windows.Forms.Button
        Me.btnVideoScrap = New System.Windows.Forms.Button
        Me.GroupBox10 = New System.Windows.Forms.GroupBox
        Me.btnTimeDeltaSet = New System.Windows.Forms.Button
        Me.txtTimeDeltaVal = New System.Windows.Forms.TextBox
        Me.tbTimeDelta = New System.Windows.Forms.TrackBar
        Me.GroupBox11 = New System.Windows.Forms.GroupBox
        Me.lblFramesCaptured = New System.Windows.Forms.Label
        Me.Label17 = New System.Windows.Forms.Label
        Me.btnStartStopVideoCapture = New System.Windows.Forms.Button
        Me.btnCaptureBrowse = New System.Windows.Forms.Button
        Me.Label16 = New System.Windows.Forms.Label
        Me.txtCaptureFileName = New System.Windows.Forms.TextBox
        Me.tpAudio = New DevExpress.XtraTab.XtraTabPage
        Me.GroupBox13 = New System.Windows.Forms.GroupBox
        Me.AU_lblFramesCaptured = New System.Windows.Forms.Label
        Me.Label19 = New System.Windows.Forms.Label
        Me.AU_btnStartStopVideoCapture = New System.Windows.Forms.Button
        Me.AU_btnCaptureBrowse = New System.Windows.Forms.Button
        Me.Label20 = New System.Windows.Forms.Label
        Me.AU_txtCaptureFileName = New System.Windows.Forms.TextBox
        Me.GroupBox14 = New System.Windows.Forms.GroupBox
        Me.AU_btnTimeDeltaSet = New System.Windows.Forms.Button
        Me.AU_txtTimeDeltaVal = New System.Windows.Forms.TextBox
        Me.AU_tbTimeDelta = New System.Windows.Forms.TrackBar
        Me.tpClosedCaptions = New DevExpress.XtraTab.XtraTabPage
        Me.gcLine21Placement = New DevExpress.XtraEditors.GroupControl
        Me.cbClosedCaptionPlacement = New System.Windows.Forms.CheckBox
        Me.txtCCPlacementY = New System.Windows.Forms.TextBox
        Me.tbClosedCaptionPlacement_X = New System.Windows.Forms.TrackBar
        Me.txtCCPlacementX = New System.Windows.Forms.TextBox
        Me.Label29 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.tbClosedCaptionPlacement_Y = New System.Windows.Forms.TrackBar
        Me.GroupBox21 = New System.Windows.Forms.GroupBox
        Me.Label32 = New System.Windows.Forms.Label
        Me.Label33 = New System.Windows.Forms.Label
        Me.Button14 = New System.Windows.Forms.Button
        Me.Button15 = New System.Windows.Forms.Button
        Me.Label34 = New System.Windows.Forms.Label
        Me.TextBox11 = New System.Windows.Forms.TextBox
        Me.GroupBox22 = New System.Windows.Forms.GroupBox
        Me.Label30 = New System.Windows.Forms.Label
        Me.Label35 = New System.Windows.Forms.Label
        Me.Button12 = New System.Windows.Forms.Button
        Me.Button13 = New System.Windows.Forms.Button
        Me.Label31 = New System.Windows.Forms.Label
        Me.TextBox10 = New System.Windows.Forms.TextBox
        Me.tpGuides = New DevExpress.XtraTab.XtraTabPage
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl
        Me.GroupControl5 = New DevExpress.XtraEditors.GroupControl
        Me.Label1 = New System.Windows.Forms.Label
        Me.lblGuideBox_W = New System.Windows.Forms.Label
        Me.lblGuideBox_H = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.lblAspect = New System.Windows.Forms.Label
        Me.GroupControl4 = New DevExpress.XtraEditors.GroupControl
        Me.cbGDS_SavedGuides = New DevExpress.XtraEditors.ComboBoxEdit
        Me.btnGDS_DeleteSelectedGuides = New DevExpress.XtraEditors.SimpleButton
        Me.btnGDS_LoadSelectedGuides = New DevExpress.XtraEditors.SimpleButton
        Me.Label37 = New System.Windows.Forms.Label
        Me.btnGDS_SaveCurrentGuides = New DevExpress.XtraEditors.SimpleButton
        Me.GroupControl3 = New DevExpress.XtraEditors.GroupControl
        Me.btnGDS_MoveAll_Down = New DevExpress.XtraEditors.SimpleButton
        Me.btnGDS_MoveAll_Up = New DevExpress.XtraEditors.SimpleButton
        Me.btnGDS_MoveAll_Right = New DevExpress.XtraEditors.SimpleButton
        Me.btnGDS_MoveAll_Left = New DevExpress.XtraEditors.SimpleButton
        Me.Label36 = New System.Windows.Forms.Label
        Me.ceGuideColor = New DevExpress.XtraEditors.ColorEdit
        Me.Label28 = New System.Windows.Forms.Label
        Me.Label27 = New System.Windows.Forms.Label
        Me.Label25 = New System.Windows.Forms.Label
        Me.Label26 = New System.Windows.Forms.Label
        Me.cbEnableFlexGuides = New DevExpress.XtraEditors.CheckEdit
        Me.txtGuide_R = New System.Windows.Forms.TextBox
        Me.tbGuide_T = New System.Windows.Forms.TrackBar
        Me.txtGuide_L = New System.Windows.Forms.TextBox
        Me.tbGuide_R = New System.Windows.Forms.TrackBar
        Me.txtGuide_B = New System.Windows.Forms.TextBox
        Me.tbGuide_B = New System.Windows.Forms.TrackBar
        Me.txtGuide_T = New System.Windows.Forms.TextBox
        Me.tbGuide_L = New System.Windows.Forms.TrackBar
        Me.tpUOPTemplates = New DevExpress.XtraTab.XtraTabPage
        Me.GroupControl6 = New DevExpress.XtraEditors.GroupControl
        Me.lvUOPT_ExistingTemplates = New System.Windows.Forms.ListBox
        Me.btnUOPT_SaveSelectedTemplate = New DevExpress.XtraEditors.SimpleButton
        Me.btnUOPT_SaveNewTemplate = New DevExpress.XtraEditors.SimpleButton
        Me.cbUO_24 = New System.Windows.Forms.CheckBox
        Me.btnUOPT_RenameSelectedTemplate = New DevExpress.XtraEditors.SimpleButton
        Me.cbUO_23 = New System.Windows.Forms.CheckBox
        Me.cbUOPT_EnableSelectedTemplate = New DevExpress.XtraEditors.CheckEdit
        Me.cbUO_22 = New System.Windows.Forms.CheckBox
        Me.btnUOPT_DeleteSelectedTemplate = New DevExpress.XtraEditors.SimpleButton
        Me.cbUO_21 = New System.Windows.Forms.CheckBox
        Me.Label187 = New System.Windows.Forms.Label
        Me.cbUO_20 = New System.Windows.Forms.CheckBox
        Me.Label192 = New System.Windows.Forms.Label
        Me.cbUO_19 = New System.Windows.Forms.CheckBox
        Me.Label191 = New System.Windows.Forms.Label
        Me.cbUO_18 = New System.Windows.Forms.CheckBox
        Me.Label190 = New System.Windows.Forms.Label
        Me.cbUO_17 = New System.Windows.Forms.CheckBox
        Me.Label189 = New System.Windows.Forms.Label
        Me.cbUO_16 = New System.Windows.Forms.CheckBox
        Me.Label188 = New System.Windows.Forms.Label
        Me.cbUO_15 = New System.Windows.Forms.CheckBox
        Me.Label186 = New System.Windows.Forms.Label
        Me.cbUO_14 = New System.Windows.Forms.CheckBox
        Me.Label185 = New System.Windows.Forms.Label
        Me.cbUO_13 = New System.Windows.Forms.CheckBox
        Me.Label184 = New System.Windows.Forms.Label
        Me.cbUO_12 = New System.Windows.Forms.CheckBox
        Me.Label183 = New System.Windows.Forms.Label
        Me.cbUO_11 = New System.Windows.Forms.CheckBox
        Me.Label182 = New System.Windows.Forms.Label
        Me.cbUO_10 = New System.Windows.Forms.CheckBox
        Me.Label181 = New System.Windows.Forms.Label
        Me.cbUO_9 = New System.Windows.Forms.CheckBox
        Me.Label180 = New System.Windows.Forms.Label
        Me.cbUO_8 = New System.Windows.Forms.CheckBox
        Me.Label179 = New System.Windows.Forms.Label
        Me.cbUO_7 = New System.Windows.Forms.CheckBox
        Me.Label178 = New System.Windows.Forms.Label
        Me.cbUO_6 = New System.Windows.Forms.CheckBox
        Me.Label177 = New System.Windows.Forms.Label
        Me.cbUO_5 = New System.Windows.Forms.CheckBox
        Me.Label176 = New System.Windows.Forms.Label
        Me.cbUO_4 = New System.Windows.Forms.CheckBox
        Me.Label175 = New System.Windows.Forms.Label
        Me.cbUO_3 = New System.Windows.Forms.CheckBox
        Me.Label174 = New System.Windows.Forms.Label
        Me.cbUO_2 = New System.Windows.Forms.CheckBox
        Me.Label173 = New System.Windows.Forms.Label
        Me.cbUO_1 = New System.Windows.Forms.CheckBox
        Me.Label42 = New System.Windows.Forms.Label
        Me.cbUO_0 = New System.Windows.Forms.CheckBox
        Me.Label41 = New System.Windows.Forms.Label
        Me.Label38 = New System.Windows.Forms.Label
        Me.Label40 = New System.Windows.Forms.Label
        Me.Label39 = New System.Windows.Forms.Label
        Me.tpKeyMapping = New DevExpress.XtraTab.XtraTabPage
        Me.lbKM_Keys = New System.Windows.Forms.ListBox
        Me.cbKM_ControlKey = New DevExpress.XtraEditors.CheckEdit
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel
        Me.Button1 = New System.Windows.Forms.Button
        Me.llKM_ReturnToDefaults = New System.Windows.Forms.LinkLabel
        Me.llKM_PrintKeyMapping = New System.Windows.Forms.LinkLabel
        Me.txtCrntFunct = New System.Windows.Forms.TextBox
        Me.llKM_SetKeyMap = New System.Windows.Forms.LinkLabel
        Me.txtValue = New System.Windows.Forms.TextBox
        Me.dgKM_KeyMapping = New System.Windows.Forms.DataGrid
        Me.gtsOne = New System.Windows.Forms.DataGridTableStyle
        Me.DataGridTextBoxColumn1 = New System.Windows.Forms.DataGridTextBoxColumn
        Me.DataGridTextBoxColumn2 = New System.Windows.Forms.DataGridTextBoxColumn
        Me.tpUIOptions = New DevExpress.XtraTab.XtraTabPage
        Me.GroupControl14 = New DevExpress.XtraEditors.GroupControl
        Me.ceUseCheckboxesForUOPs = New DevExpress.XtraEditors.CheckEdit
        Me.GroupControl13 = New DevExpress.XtraEditors.GroupControl
        Me.rbRootResume_SeparateButtons = New System.Windows.Forms.RadioButton
        Me.rbRootResume_RootIsResume = New System.Windows.Forms.RadioButton
        Me.tpDumping = New DevExpress.XtraTab.XtraTabPage
        Me.cbDumpAudio = New System.Windows.Forms.CheckBox
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl
        Me.btnOpenDumpDirectory = New DevExpress.XtraEditors.SimpleButton
        Me.btnDumpDirectoryBrowse = New DevExpress.XtraEditors.SimpleButton
        Me.txtDumpDirectory = New System.Windows.Forms.TextBox
        Me.btnResetAllOptions = New System.Windows.Forms.Button
        Me.cbARs = New System.Windows.Forms.ComboBox
        Me.CheckBox1 = New System.Windows.Forms.CheckBox
        Me.tpAdmin = New DevExpress.XtraTab.XtraTabPage
        Me.btnADMIN_SaveCurrentLayout = New DevExpress.XtraEditors.SimpleButton
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.tpGraphSetup = New System.Windows.Forms.TabPage
        Me.GroupBox23 = New System.Windows.Forms.GroupBox
        Me.rbGraphProfiles_3 = New System.Windows.Forms.RadioButton
        Me.Label49 = New System.Windows.Forms.Label
        Me.rbGraphProfiles_2 = New System.Windows.Forms.RadioButton
        Me.rbGraphProfiles_0 = New System.Windows.Forms.RadioButton
        Me.cbROTGraph = New System.Windows.Forms.CheckBox
        Me.tpDebugging = New System.Windows.Forms.TabPage
        Me.btnGetDefSubLang = New System.Windows.Forms.Button
        Me.btnGetPlayerPL = New System.Windows.Forms.Button
        Me.btnGetTitlePL = New System.Windows.Forms.Button
        Me.Button2 = New System.Windows.Forms.Button
        Me.btnAspectRatio = New System.Windows.Forms.Button
        Me.btnGetProcAmp = New System.Windows.Forms.Button
        Me.btnProcAmpRange = New System.Windows.Forms.Button
        Me.btnGetVidWin = New System.Windows.Forms.Button
        Me.btnGetDefMenuLang = New System.Windows.Forms.Button
        Me.btnGetDefAudLang = New System.Windows.Forms.Button
        Me.tpFeatures = New System.Windows.Forms.TabPage
        Me.cbAdmin_CCs = New System.Windows.Forms.CheckBox
        Me.cbAdmin_LayerbreakSimulation = New System.Windows.Forms.CheckBox
        Me.btnAdmin_Set = New System.Windows.Forms.Button
        Me.GroupBox24 = New System.Windows.Forms.GroupBox
        Me.cbAdmin_VidStd_NTSC = New System.Windows.Forms.CheckBox
        Me.cbAdmin_VidStd_PAL = New System.Windows.Forms.CheckBox
        Me.GroupBox25 = New System.Windows.Forms.GroupBox
        Me.cbAdmin_Region_8 = New System.Windows.Forms.CheckBox
        Me.cbAdmin_Region_7 = New System.Windows.Forms.CheckBox
        Me.cbAdmin_Region_6 = New System.Windows.Forms.CheckBox
        Me.cbAdmin_Region_5 = New System.Windows.Forms.CheckBox
        Me.cbAdmin_Region_4 = New System.Windows.Forms.CheckBox
        Me.cbAdmin_Region_3 = New System.Windows.Forms.CheckBox
        Me.cbAdmin_Region_2 = New System.Windows.Forms.CheckBox
        Me.cbAdmin_Region_1 = New System.Windows.Forms.CheckBox
        Me.cbAdmin_DTSAudio = New System.Windows.Forms.CheckBox
        Me.cbAdmin_Component = New System.Windows.Forms.CheckBox
        Me.cbAdmin_ParentalManagement = New System.Windows.Forms.CheckBox
        Me.cbAdmin_UOPs = New System.Windows.Forms.CheckBox
        Me.cbAdmin_SPRMs = New System.Windows.Forms.CheckBox
        Me.cbAdmin_GPRMs = New System.Windows.Forms.CheckBox
        Me.cbAdmin_FrameGrab = New System.Windows.Forms.CheckBox
        Me.cbAdmin_JacketPicturePreview = New System.Windows.Forms.CheckBox
        Me.tpPPs = New System.Windows.Forms.TabPage
        Me.btnDebugging = New System.Windows.Forms.Button
        Me.btnKeystonePP = New System.Windows.Forms.Button
        Me.btnProperties_ar = New System.Windows.Forms.Button
        Me.btnProperties_aud = New System.Windows.Forms.Button
        Me.btnProperties_vr = New System.Windows.Forms.Button
        Me.btnProperties_vid = New System.Windows.Forms.Button
        Me.Two_Second_Timer = New System.Windows.Forms.Timer(Me.components)
        CType(Me.tcMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tcMain.SuspendLayout()
        Me.tpOutputConfig.SuspendLayout()
        CType(Me.gbHDMIAudio, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbHDMIAudio.SuspendLayout()
        CType(Me.cbHDMIAudioFormat.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gbAV_AnalogVideoConfig, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbAV_AnalogVideoConfig.SuspendLayout()
        CType(Me.cbAV_AnalogFormat.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl7, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl7.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.gbHDMIScaling.SuspendLayout()
        CType(Me.ceHDMIVidScl_Maximize.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbHDMIResolution.SuspendLayout()
        Me.tpFrameGrabViewer.SuspendLayout()
        CType(Me.cbFGV_MultiFrameDirectories.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbFGV_ViewMulti.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cmFGV_FrameGrabOptions.SuspendLayout()
        CType(Me.pbFrameGrab, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cmFGV_AspectRatio.SuspendLayout()
        Me.tpSubpicture.SuspendLayout()
        CType(Me.gbSUB_Positioning, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbSUB_Positioning.SuspendLayout()
        CType(Me.tbSPPlacement_X, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbSPPlacement_Y, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox17.SuspendLayout()
        CType(Me.TrackBar2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox19.SuspendLayout()
        Me.tpVideo.SuspendLayout()
        CType(Me.GroupControl8, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl8.SuspendLayout()
        CType(Me.GroupControl10, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl10.SuspendLayout()
        CType(Me.cbVID_ReverseFieldOrder.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gbVID_ProcAmp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbVID_ProcAmp.SuspendLayout()
        CType(Me.tbBrightness, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbContrast, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbHue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbSaturation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gbVID_MPEGFrameFilter, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbVID_MPEGFrameFilter.SuspendLayout()
        CType(Me.gbVID_YUVFilter, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbVID_YUVFilter.SuspendLayout()
        Me.GroupBox8.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox9.SuspendLayout()
        Me.GroupBox10.SuspendLayout()
        CType(Me.tbTimeDelta, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox11.SuspendLayout()
        Me.tpAudio.SuspendLayout()
        Me.GroupBox13.SuspendLayout()
        Me.GroupBox14.SuspendLayout()
        CType(Me.AU_tbTimeDelta, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpClosedCaptions.SuspendLayout()
        CType(Me.gcLine21Placement, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gcLine21Placement.SuspendLayout()
        CType(Me.tbClosedCaptionPlacement_X, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbClosedCaptionPlacement_Y, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox21.SuspendLayout()
        Me.GroupBox22.SuspendLayout()
        Me.tpGuides.SuspendLayout()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        CType(Me.GroupControl5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl5.SuspendLayout()
        CType(Me.GroupControl4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl4.SuspendLayout()
        CType(Me.cbGDS_SavedGuides.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl3.SuspendLayout()
        CType(Me.ceGuideColor.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbEnableFlexGuides.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbGuide_T, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbGuide_R, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbGuide_B, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbGuide_L, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpUOPTemplates.SuspendLayout()
        CType(Me.GroupControl6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl6.SuspendLayout()
        CType(Me.cbUOPT_EnableSelectedTemplate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpKeyMapping.SuspendLayout()
        CType(Me.cbKM_ControlKey.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgKM_KeyMapping, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpUIOptions.SuspendLayout()
        CType(Me.GroupControl14, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl14.SuspendLayout()
        CType(Me.ceUseCheckboxesForUOPs.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl13, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl13.SuspendLayout()
        Me.tpDumping.SuspendLayout()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        Me.tpAdmin.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.tpGraphSetup.SuspendLayout()
        Me.GroupBox23.SuspendLayout()
        Me.tpDebugging.SuspendLayout()
        Me.tpFeatures.SuspendLayout()
        Me.GroupBox24.SuspendLayout()
        Me.GroupBox25.SuspendLayout()
        Me.tpPPs.SuspendLayout()
        Me.SuspendLayout()
        '
        'tcMain
        '
        Me.tcMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tcMain.Location = New System.Drawing.Point(0, 0)
        Me.tcMain.LookAndFeel.SkinName = "Office 2007 Black"
        Me.tcMain.MultiLine = DevExpress.Utils.DefaultBoolean.[True]
        Me.tcMain.Name = "tcMain"
        Me.tcMain.SelectedTabPage = Me.tpOutputConfig
        Me.tcMain.Size = New System.Drawing.Size(632, 470)
        Me.tcMain.TabIndex = 0
        Me.tcMain.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.tpOutputConfig, Me.tpFrameGrabViewer, Me.tpSubpicture, Me.tpVideo, Me.tpAudio, Me.tpClosedCaptions, Me.tpGuides, Me.tpUOPTemplates, Me.tpKeyMapping, Me.tpUIOptions, Me.tpDumping, Me.tpAdmin})
        '
        'tpOutputConfig
        '
        Me.tpOutputConfig.Controls.Add(Me.gbHDMIAudio)
        Me.tpOutputConfig.Controls.Add(Me.gbAV_AnalogVideoConfig)
        Me.tpOutputConfig.Controls.Add(Me.GroupControl7)
        Me.tpOutputConfig.Name = "tpOutputConfig"
        Me.tpOutputConfig.Size = New System.Drawing.Size(623, 419)
        Me.tpOutputConfig.Text = "Output Config"
        '
        'gbHDMIAudio
        '
        Me.gbHDMIAudio.Controls.Add(Me.Label11)
        Me.gbHDMIAudio.Controls.Add(Me.cbHDMIAudioFormat)
        Me.gbHDMIAudio.Location = New System.Drawing.Point(176, 62)
        Me.gbHDMIAudio.Name = "gbHDMIAudio"
        Me.gbHDMIAudio.Size = New System.Drawing.Size(152, 44)
        Me.gbHDMIAudio.TabIndex = 5
        Me.gbHDMIAudio.Text = "HDMI Audio"
        Me.gbHDMIAudio.Visible = False
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(5, 24)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(45, 13)
        Me.Label11.TabIndex = 5
        Me.Label11.Text = "Format:"
        '
        'cbHDMIAudioFormat
        '
        Me.cbHDMIAudioFormat.EditValue = "Compressed"
        Me.cbHDMIAudioFormat.Location = New System.Drawing.Point(50, 21)
        Me.cbHDMIAudioFormat.Name = "cbHDMIAudioFormat"
        Me.cbHDMIAudioFormat.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cbHDMIAudioFormat.Properties.AppearanceDisabled.BackColor2 = System.Drawing.Color.WhiteSmoke
        Me.cbHDMIAudioFormat.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.cbHDMIAudioFormat.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cbHDMIAudioFormat.Properties.Items.AddRange(New Object() {"Compressed", "Uncompressed"})
        Me.cbHDMIAudioFormat.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.cbHDMIAudioFormat.Size = New System.Drawing.Size(97, 20)
        Me.cbHDMIAudioFormat.TabIndex = 0
        '
        'gbAV_AnalogVideoConfig
        '
        Me.gbAV_AnalogVideoConfig.Controls.Add(Me.Label9)
        Me.gbAV_AnalogVideoConfig.Controls.Add(Me.cbAV_AnalogFormat)
        Me.gbAV_AnalogVideoConfig.Location = New System.Drawing.Point(176, 12)
        Me.gbAV_AnalogVideoConfig.Name = "gbAV_AnalogVideoConfig"
        Me.gbAV_AnalogVideoConfig.Size = New System.Drawing.Size(152, 44)
        Me.gbAV_AnalogVideoConfig.TabIndex = 4
        Me.gbAV_AnalogVideoConfig.Text = "Analog Video Configuration"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(5, 24)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(45, 13)
        Me.Label9.TabIndex = 5
        Me.Label9.Text = "Format:"
        '
        'cbAV_AnalogFormat
        '
        Me.cbAV_AnalogFormat.EditValue = "Component"
        Me.cbAV_AnalogFormat.Location = New System.Drawing.Point(50, 21)
        Me.cbAV_AnalogFormat.Name = "cbAV_AnalogFormat"
        Me.cbAV_AnalogFormat.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cbAV_AnalogFormat.Properties.AppearanceDisabled.BackColor2 = System.Drawing.Color.WhiteSmoke
        Me.cbAV_AnalogFormat.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.cbAV_AnalogFormat.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cbAV_AnalogFormat.Properties.Items.AddRange(New Object() {"Component", "Composite"})
        Me.cbAV_AnalogFormat.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.cbAV_AnalogFormat.Size = New System.Drawing.Size(97, 20)
        Me.cbAV_AnalogFormat.TabIndex = 0
        '
        'GroupControl7
        '
        Me.GroupControl7.Controls.Add(Me.btnApplyDeviceChanges)
        Me.GroupControl7.Controls.Add(Me.GroupBox1)
        Me.GroupControl7.Controls.Add(Me.gbHDMIScaling)
        Me.GroupControl7.Controls.Add(Me.gbHDMIResolution)
        Me.GroupControl7.Location = New System.Drawing.Point(9, 12)
        Me.GroupControl7.Name = "GroupControl7"
        Me.GroupControl7.Size = New System.Drawing.Size(155, 295)
        Me.GroupControl7.TabIndex = 3
        Me.GroupControl7.Text = "AV Output Configuration"
        '
        'btnApplyDeviceChanges
        '
        Me.btnApplyDeviceChanges.Enabled = False
        Me.btnApplyDeviceChanges.Location = New System.Drawing.Point(5, 266)
        Me.btnApplyDeviceChanges.Name = "btnApplyDeviceChanges"
        Me.btnApplyDeviceChanges.Size = New System.Drawing.Size(144, 23)
        Me.btnApplyDeviceChanges.TabIndex = 6
        Me.btnApplyDeviceChanges.Text = "Apply Changes"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rbIntensity)
        Me.GroupBox1.Controls.Add(Me.rbDesktop)
        Me.GroupBox1.Controls.Add(Me.rbDecklink)
        Me.GroupBox1.Location = New System.Drawing.Point(5, 21)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(144, 73)
        Me.GroupBox1.TabIndex = 3
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Output Device"
        '
        'rbIntensity
        '
        Me.rbIntensity.AutoSize = True
        Me.rbIntensity.Location = New System.Drawing.Point(7, 33)
        Me.rbIntensity.Name = "rbIntensity"
        Me.rbIntensity.Size = New System.Drawing.Size(122, 17)
        Me.rbIntensity.TabIndex = 2
        Me.rbIntensity.Text = "Blackmagic Intensity"
        Me.rbIntensity.UseVisualStyleBackColor = True
        Me.rbIntensity.Visible = False
        '
        'rbDesktop
        '
        Me.rbDesktop.AutoSize = True
        Me.rbDesktop.Checked = True
        Me.rbDesktop.Location = New System.Drawing.Point(7, 15)
        Me.rbDesktop.Name = "rbDesktop"
        Me.rbDesktop.Size = New System.Drawing.Size(139, 17)
        Me.rbDesktop.TabIndex = 0
        Me.rbDesktop.TabStop = True
        Me.rbDesktop.Text = "Desktop (graphics card)"
        Me.rbDesktop.UseVisualStyleBackColor = True
        '
        'rbDecklink
        '
        Me.rbDecklink.AutoSize = True
        Me.rbDecklink.Location = New System.Drawing.Point(7, 50)
        Me.rbDecklink.Name = "rbDecklink"
        Me.rbDecklink.Size = New System.Drawing.Size(117, 17)
        Me.rbDecklink.TabIndex = 1
        Me.rbDecklink.Text = "Blackmagic Decklink"
        Me.rbDecklink.UseVisualStyleBackColor = True
        '
        'gbHDMIScaling
        '
        Me.gbHDMIScaling.Controls.Add(Me.ceHDMIVidScl_Maximize)
        Me.gbHDMIScaling.Controls.Add(Me.rbHDMIVidScl_AdjustToAspect)
        Me.gbHDMIScaling.Controls.Add(Me.rbHDMIVidScl_Native)
        Me.gbHDMIScaling.Enabled = False
        Me.gbHDMIScaling.Location = New System.Drawing.Point(5, 189)
        Me.gbHDMIScaling.Name = "gbHDMIScaling"
        Me.gbHDMIScaling.Size = New System.Drawing.Size(144, 71)
        Me.gbHDMIScaling.TabIndex = 2
        Me.gbHDMIScaling.TabStop = False
        Me.gbHDMIScaling.Text = "Intensity Scaling"
        Me.gbHDMIScaling.Visible = False
        '
        'ceHDMIVidScl_Maximize
        '
        Me.ceHDMIVidScl_Maximize.Location = New System.Drawing.Point(8, 49)
        Me.ceHDMIVidScl_Maximize.Name = "ceHDMIVidScl_Maximize"
        Me.ceHDMIVidScl_Maximize.Properties.Caption = "Maximize"
        Me.ceHDMIVidScl_Maximize.Size = New System.Drawing.Size(75, 18)
        Me.ceHDMIVidScl_Maximize.TabIndex = 3
        '
        'rbHDMIVidScl_AdjustToAspect
        '
        Me.rbHDMIVidScl_AdjustToAspect.AutoSize = True
        Me.rbHDMIVidScl_AdjustToAspect.Checked = True
        Me.rbHDMIVidScl_AdjustToAspect.Location = New System.Drawing.Point(10, 32)
        Me.rbHDMIVidScl_AdjustToAspect.Name = "rbHDMIVidScl_AdjustToAspect"
        Me.rbHDMIVidScl_AdjustToAspect.Size = New System.Drawing.Size(104, 17)
        Me.rbHDMIVidScl_AdjustToAspect.TabIndex = 2
        Me.rbHDMIVidScl_AdjustToAspect.TabStop = True
        Me.rbHDMIVidScl_AdjustToAspect.Text = "Adjust to aspect"
        Me.rbHDMIVidScl_AdjustToAspect.UseVisualStyleBackColor = True
        '
        'rbHDMIVidScl_Native
        '
        Me.rbHDMIVidScl_Native.AutoSize = True
        Me.rbHDMIVidScl_Native.Location = New System.Drawing.Point(10, 15)
        Me.rbHDMIVidScl_Native.Name = "rbHDMIVidScl_Native"
        Me.rbHDMIVidScl_Native.Size = New System.Drawing.Size(56, 17)
        Me.rbHDMIVidScl_Native.TabIndex = 0
        Me.rbHDMIVidScl_Native.Text = "Native"
        Me.rbHDMIVidScl_Native.UseVisualStyleBackColor = True
        '
        'gbHDMIResolution
        '
        Me.gbHDMIResolution.Controls.Add(Me.rbIntensityResolution_486)
        Me.gbHDMIResolution.Controls.Add(Me.rbIntensityResolution_576)
        Me.gbHDMIResolution.Controls.Add(Me.rbIntensityResolution_1080)
        Me.gbHDMIResolution.Controls.Add(Me.rbIntensityResolution_720)
        Me.gbHDMIResolution.Enabled = False
        Me.gbHDMIResolution.Location = New System.Drawing.Point(5, 97)
        Me.gbHDMIResolution.Name = "gbHDMIResolution"
        Me.gbHDMIResolution.Size = New System.Drawing.Size(144, 89)
        Me.gbHDMIResolution.TabIndex = 1
        Me.gbHDMIResolution.TabStop = False
        Me.gbHDMIResolution.Text = "Intensity Resolution"
        Me.gbHDMIResolution.Visible = False
        '
        'rbIntensityResolution_486
        '
        Me.rbIntensityResolution_486.AutoSize = True
        Me.rbIntensityResolution_486.Location = New System.Drawing.Point(10, 65)
        Me.rbIntensityResolution_486.Name = "rbIntensityResolution_486"
        Me.rbIntensityResolution_486.Size = New System.Drawing.Size(43, 17)
        Me.rbIntensityResolution_486.TabIndex = 4
        Me.rbIntensityResolution_486.Text = "486"
        Me.rbIntensityResolution_486.UseVisualStyleBackColor = True
        '
        'rbIntensityResolution_576
        '
        Me.rbIntensityResolution_576.AutoSize = True
        Me.rbIntensityResolution_576.Location = New System.Drawing.Point(10, 48)
        Me.rbIntensityResolution_576.Name = "rbIntensityResolution_576"
        Me.rbIntensityResolution_576.Size = New System.Drawing.Size(43, 17)
        Me.rbIntensityResolution_576.TabIndex = 3
        Me.rbIntensityResolution_576.Text = "576"
        Me.rbIntensityResolution_576.UseVisualStyleBackColor = True
        '
        'rbIntensityResolution_1080
        '
        Me.rbIntensityResolution_1080.AutoSize = True
        Me.rbIntensityResolution_1080.Checked = True
        Me.rbIntensityResolution_1080.Location = New System.Drawing.Point(10, 14)
        Me.rbIntensityResolution_1080.Name = "rbIntensityResolution_1080"
        Me.rbIntensityResolution_1080.Size = New System.Drawing.Size(49, 17)
        Me.rbIntensityResolution_1080.TabIndex = 2
        Me.rbIntensityResolution_1080.TabStop = True
        Me.rbIntensityResolution_1080.Text = "1080"
        Me.rbIntensityResolution_1080.UseVisualStyleBackColor = True
        '
        'rbIntensityResolution_720
        '
        Me.rbIntensityResolution_720.AutoSize = True
        Me.rbIntensityResolution_720.Location = New System.Drawing.Point(10, 31)
        Me.rbIntensityResolution_720.Name = "rbIntensityResolution_720"
        Me.rbIntensityResolution_720.Size = New System.Drawing.Size(43, 17)
        Me.rbIntensityResolution_720.TabIndex = 1
        Me.rbIntensityResolution_720.Text = "720"
        Me.rbIntensityResolution_720.UseVisualStyleBackColor = True
        '
        'tpFrameGrabViewer
        '
        Me.tpFrameGrabViewer.Controls.Add(Me.btnFGV_OpenTargetDir)
        Me.tpFrameGrabViewer.Controls.Add(Me.btnFGV_SaveCurrent)
        Me.tpFrameGrabViewer.Controls.Add(Me.btnFGV_DeleteCurrent)
        Me.tpFrameGrabViewer.Controls.Add(Me.cbFGV_MultiFrameDirectories)
        Me.tpFrameGrabViewer.Controls.Add(Me.cbFGV_ViewMulti)
        Me.tpFrameGrabViewer.Controls.Add(Me.btnFGV_ViewNext)
        Me.tpFrameGrabViewer.Controls.Add(Me.btnFGV_ViewPrevious)
        Me.tpFrameGrabViewer.Controls.Add(Me.btnFGV_GrabFrame)
        Me.tpFrameGrabViewer.Controls.Add(Me.lblFGV_FileName)
        Me.tpFrameGrabViewer.Controls.Add(Me.pbFrameGrab)
        Me.tpFrameGrabViewer.Name = "tpFrameGrabViewer"
        Me.tpFrameGrabViewer.Size = New System.Drawing.Size(623, 417)
        Me.tpFrameGrabViewer.Text = "Frame Grab Viewer"
        '
        'btnFGV_OpenTargetDir
        '
        Me.btnFGV_OpenTargetDir.Location = New System.Drawing.Point(478, 24)
        Me.btnFGV_OpenTargetDir.Name = "btnFGV_OpenTargetDir"
        Me.btnFGV_OpenTargetDir.Size = New System.Drawing.Size(132, 23)
        Me.btnFGV_OpenTargetDir.TabIndex = 30
        Me.btnFGV_OpenTargetDir.Text = "Open Grab Directory"
        '
        'btnFGV_SaveCurrent
        '
        Me.btnFGV_SaveCurrent.Location = New System.Drawing.Point(525, 1)
        Me.btnFGV_SaveCurrent.Name = "btnFGV_SaveCurrent"
        Me.btnFGV_SaveCurrent.Size = New System.Drawing.Size(85, 23)
        Me.btnFGV_SaveCurrent.TabIndex = 25
        Me.btnFGV_SaveCurrent.Text = "Save Current"
        '
        'btnFGV_DeleteCurrent
        '
        Me.btnFGV_DeleteCurrent.Location = New System.Drawing.Point(434, 1)
        Me.btnFGV_DeleteCurrent.Name = "btnFGV_DeleteCurrent"
        Me.btnFGV_DeleteCurrent.Size = New System.Drawing.Size(85, 23)
        Me.btnFGV_DeleteCurrent.TabIndex = 24
        Me.btnFGV_DeleteCurrent.Text = "Delete Current"
        '
        'cbFGV_MultiFrameDirectories
        '
        Me.cbFGV_MultiFrameDirectories.Location = New System.Drawing.Point(297, 2)
        Me.cbFGV_MultiFrameDirectories.Name = "cbFGV_MultiFrameDirectories"
        Me.cbFGV_MultiFrameDirectories.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cbFGV_MultiFrameDirectories.Size = New System.Drawing.Size(131, 20)
        Me.cbFGV_MultiFrameDirectories.TabIndex = 23
        '
        'cbFGV_ViewMulti
        '
        Me.cbFGV_ViewMulti.Location = New System.Drawing.Point(273, 5)
        Me.cbFGV_ViewMulti.Name = "cbFGV_ViewMulti"
        Me.cbFGV_ViewMulti.Properties.Caption = ""
        Me.cbFGV_ViewMulti.Size = New System.Drawing.Size(18, 18)
        Me.cbFGV_ViewMulti.TabIndex = 22
        '
        'btnFGV_ViewNext
        '
        Me.btnFGV_ViewNext.Location = New System.Drawing.Point(182, 1)
        Me.btnFGV_ViewNext.Name = "btnFGV_ViewNext"
        Me.btnFGV_ViewNext.Size = New System.Drawing.Size(85, 23)
        Me.btnFGV_ViewNext.TabIndex = 21
        Me.btnFGV_ViewNext.Text = "View Next"
        '
        'btnFGV_ViewPrevious
        '
        Me.btnFGV_ViewPrevious.Location = New System.Drawing.Point(91, 1)
        Me.btnFGV_ViewPrevious.Name = "btnFGV_ViewPrevious"
        Me.btnFGV_ViewPrevious.Size = New System.Drawing.Size(85, 23)
        Me.btnFGV_ViewPrevious.TabIndex = 20
        Me.btnFGV_ViewPrevious.Text = "View Previous"
        '
        'btnFGV_GrabFrame
        '
        Me.btnFGV_GrabFrame.ContextMenuStrip = Me.cmFGV_FrameGrabOptions
        Me.btnFGV_GrabFrame.Location = New System.Drawing.Point(0, 1)
        Me.btnFGV_GrabFrame.Name = "btnFGV_GrabFrame"
        Me.btnFGV_GrabFrame.Size = New System.Drawing.Size(85, 23)
        Me.btnFGV_GrabFrame.TabIndex = 19
        Me.btnFGV_GrabFrame.Text = "Frame Grab"
        '
        'cmFGV_FrameGrabOptions
        '
        Me.cmFGV_FrameGrabOptions.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.miFGV_FullMix, Me.miFGV_VideoAndSubpicture, Me.miFGV_VideoOnly, Me.miFGV_SubpictureOnly, Me.miFGV_ClosedCaptionsOnly, Me.ToolStripMenuItem1, Me.miFGV_MultiFrame, Me.ToolStripMenuItem2, Me.miFGV_Bitmap, Me.miFGV_JPEG, Me.miFGV_GIF, Me.miFGV_PNG, Me.miFGV_TIF})
        Me.cmFGV_FrameGrabOptions.Name = "cmFGV_FrameGrabOptions"
        Me.cmFGV_FrameGrabOptions.Size = New System.Drawing.Size(188, 258)
        '
        'miFGV_FullMix
        '
        Me.miFGV_FullMix.Name = "miFGV_FullMix"
        Me.miFGV_FullMix.Size = New System.Drawing.Size(187, 22)
        Me.miFGV_FullMix.Text = "Full Mix"
        '
        'miFGV_VideoAndSubpicture
        '
        Me.miFGV_VideoAndSubpicture.Name = "miFGV_VideoAndSubpicture"
        Me.miFGV_VideoAndSubpicture.Size = New System.Drawing.Size(187, 22)
        Me.miFGV_VideoAndSubpicture.Text = "Video and Subpicture"
        '
        'miFGV_VideoOnly
        '
        Me.miFGV_VideoOnly.Name = "miFGV_VideoOnly"
        Me.miFGV_VideoOnly.Size = New System.Drawing.Size(187, 22)
        Me.miFGV_VideoOnly.Text = "Video Only"
        '
        'miFGV_SubpictureOnly
        '
        Me.miFGV_SubpictureOnly.Name = "miFGV_SubpictureOnly"
        Me.miFGV_SubpictureOnly.Size = New System.Drawing.Size(187, 22)
        Me.miFGV_SubpictureOnly.Text = "Subpicture Only"
        '
        'miFGV_ClosedCaptionsOnly
        '
        Me.miFGV_ClosedCaptionsOnly.Name = "miFGV_ClosedCaptionsOnly"
        Me.miFGV_ClosedCaptionsOnly.Size = New System.Drawing.Size(187, 22)
        Me.miFGV_ClosedCaptionsOnly.Text = "Closed Caption Only"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(184, 6)
        '
        'miFGV_MultiFrame
        '
        Me.miFGV_MultiFrame.Name = "miFGV_MultiFrame"
        Me.miFGV_MultiFrame.Size = New System.Drawing.Size(187, 22)
        Me.miFGV_MultiFrame.Text = "MultiFrame"
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(184, 6)
        '
        'miFGV_Bitmap
        '
        Me.miFGV_Bitmap.Name = "miFGV_Bitmap"
        Me.miFGV_Bitmap.Size = New System.Drawing.Size(187, 22)
        Me.miFGV_Bitmap.Text = "BMP"
        '
        'miFGV_JPEG
        '
        Me.miFGV_JPEG.Name = "miFGV_JPEG"
        Me.miFGV_JPEG.Size = New System.Drawing.Size(187, 22)
        Me.miFGV_JPEG.Text = "JPEG"
        '
        'miFGV_GIF
        '
        Me.miFGV_GIF.Name = "miFGV_GIF"
        Me.miFGV_GIF.Size = New System.Drawing.Size(187, 22)
        Me.miFGV_GIF.Text = "GIF"
        '
        'miFGV_PNG
        '
        Me.miFGV_PNG.Name = "miFGV_PNG"
        Me.miFGV_PNG.Size = New System.Drawing.Size(187, 22)
        Me.miFGV_PNG.Text = "PNG"
        '
        'miFGV_TIF
        '
        Me.miFGV_TIF.Name = "miFGV_TIF"
        Me.miFGV_TIF.Size = New System.Drawing.Size(187, 22)
        Me.miFGV_TIF.Text = "TIF"
        '
        'lblFGV_FileName
        '
        Me.lblFGV_FileName.AutoSize = True
        Me.lblFGV_FileName.Location = New System.Drawing.Point(6, 31)
        Me.lblFGV_FileName.Name = "lblFGV_FileName"
        Me.lblFGV_FileName.Size = New System.Drawing.Size(0, 13)
        Me.lblFGV_FileName.TabIndex = 18
        '
        'pbFrameGrab
        '
        Me.pbFrameGrab.BackColor = System.Drawing.Color.Black
        Me.pbFrameGrab.ContextMenuStrip = Me.cmFGV_AspectRatio
        Me.pbFrameGrab.Location = New System.Drawing.Point(0, 53)
        Me.pbFrameGrab.Name = "pbFrameGrab"
        Me.pbFrameGrab.Size = New System.Drawing.Size(540, 360)
        Me.pbFrameGrab.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbFrameGrab.TabIndex = 9
        Me.pbFrameGrab.TabStop = False
        '
        'cmFGV_AspectRatio
        '
        Me.cmFGV_AspectRatio.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mi853480, Me.mi853576, Me.mi720480, Me.mi720576})
        Me.cmFGV_AspectRatio.Name = "cmFGV_AspectRatio"
        Me.cmFGV_AspectRatio.Size = New System.Drawing.Size(116, 92)
        '
        'mi853480
        '
        Me.mi853480.Name = "mi853480"
        Me.mi853480.Size = New System.Drawing.Size(115, 22)
        Me.mi853480.Text = "853x480"
        '
        'mi853576
        '
        Me.mi853576.Name = "mi853576"
        Me.mi853576.Size = New System.Drawing.Size(115, 22)
        Me.mi853576.Text = "853x576"
        '
        'mi720480
        '
        Me.mi720480.Checked = True
        Me.mi720480.CheckState = System.Windows.Forms.CheckState.Checked
        Me.mi720480.Name = "mi720480"
        Me.mi720480.Size = New System.Drawing.Size(115, 22)
        Me.mi720480.Text = "720x480"
        '
        'mi720576
        '
        Me.mi720576.Name = "mi720576"
        Me.mi720576.Size = New System.Drawing.Size(115, 22)
        Me.mi720576.Text = "720x576"
        '
        'tpSubpicture
        '
        Me.tpSubpicture.Controls.Add(Me.gbSUB_Positioning)
        Me.tpSubpicture.Controls.Add(Me.GroupBox17)
        Me.tpSubpicture.Controls.Add(Me.GroupBox19)
        Me.tpSubpicture.Name = "tpSubpicture"
        Me.tpSubpicture.Size = New System.Drawing.Size(623, 417)
        Me.tpSubpicture.Text = "Subpicture"
        '
        'gbSUB_Positioning
        '
        Me.gbSUB_Positioning.Controls.Add(Me.cbSPPlacementEnabled)
        Me.gbSUB_Positioning.Controls.Add(Me.txtSPPlacement_Y)
        Me.gbSUB_Positioning.Controls.Add(Me.tbSPPlacement_X)
        Me.gbSUB_Positioning.Controls.Add(Me.txtSPPlacement_X)
        Me.gbSUB_Positioning.Controls.Add(Me.Label23)
        Me.gbSUB_Positioning.Controls.Add(Me.Label24)
        Me.gbSUB_Positioning.Controls.Add(Me.tbSPPlacement_Y)
        Me.gbSUB_Positioning.Location = New System.Drawing.Point(3, 3)
        Me.gbSUB_Positioning.Name = "gbSUB_Positioning"
        Me.gbSUB_Positioning.Size = New System.Drawing.Size(226, 100)
        Me.gbSUB_Positioning.TabIndex = 45
        Me.gbSUB_Positioning.Text = "Placement"
        '
        'cbSPPlacementEnabled
        '
        Me.cbSPPlacementEnabled.Location = New System.Drawing.Point(8, 26)
        Me.cbSPPlacementEnabled.Name = "cbSPPlacementEnabled"
        Me.cbSPPlacementEnabled.Size = New System.Drawing.Size(176, 16)
        Me.cbSPPlacementEnabled.TabIndex = 0
        Me.cbSPPlacementEnabled.Text = "Enable Placement Adjustment"
        '
        'txtSPPlacement_Y
        '
        Me.txtSPPlacement_Y.Location = New System.Drawing.Point(188, 70)
        Me.txtSPPlacement_Y.Name = "txtSPPlacement_Y"
        Me.txtSPPlacement_Y.Size = New System.Drawing.Size(32, 21)
        Me.txtSPPlacement_Y.TabIndex = 4
        Me.txtSPPlacement_Y.Text = "0"
        Me.txtSPPlacement_Y.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'tbSPPlacement_X
        '
        Me.tbSPPlacement_X.AutoSize = False
        Me.tbSPPlacement_X.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.tbSPPlacement_X.LargeChange = 50
        Me.tbSPPlacement_X.Location = New System.Drawing.Point(24, 46)
        Me.tbSPPlacement_X.Maximum = 720
        Me.tbSPPlacement_X.Minimum = -720
        Me.tbSPPlacement_X.Name = "tbSPPlacement_X"
        Me.tbSPPlacement_X.Size = New System.Drawing.Size(164, 16)
        Me.tbSPPlacement_X.SmallChange = 10
        Me.tbSPPlacement_X.TabIndex = 1
        Me.tbSPPlacement_X.TickFrequency = 50
        '
        'txtSPPlacement_X
        '
        Me.txtSPPlacement_X.Location = New System.Drawing.Point(188, 46)
        Me.txtSPPlacement_X.Name = "txtSPPlacement_X"
        Me.txtSPPlacement_X.Size = New System.Drawing.Size(32, 21)
        Me.txtSPPlacement_X.TabIndex = 3
        Me.txtSPPlacement_X.Text = "0"
        Me.txtSPPlacement_X.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label23
        '
        Me.Label23.Location = New System.Drawing.Point(4, 46)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(24, 16)
        Me.Label23.TabIndex = 32
        Me.Label23.Text = "X"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label24
        '
        Me.Label24.Location = New System.Drawing.Point(4, 70)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(24, 16)
        Me.Label24.TabIndex = 34
        Me.Label24.Text = "Y"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'tbSPPlacement_Y
        '
        Me.tbSPPlacement_Y.AutoSize = False
        Me.tbSPPlacement_Y.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.tbSPPlacement_Y.LargeChange = 50
        Me.tbSPPlacement_Y.Location = New System.Drawing.Point(24, 70)
        Me.tbSPPlacement_Y.Maximum = 576
        Me.tbSPPlacement_Y.Minimum = -576
        Me.tbSPPlacement_Y.Name = "tbSPPlacement_Y"
        Me.tbSPPlacement_Y.Size = New System.Drawing.Size(164, 16)
        Me.tbSPPlacement_Y.SmallChange = 10
        Me.tbSPPlacement_Y.TabIndex = 2
        Me.tbSPPlacement_Y.TickFrequency = 50
        '
        'GroupBox17
        '
        Me.GroupBox17.Controls.Add(Me.Button5)
        Me.GroupBox17.Controls.Add(Me.TextBox3)
        Me.GroupBox17.Controls.Add(Me.TrackBar2)
        Me.GroupBox17.Enabled = False
        Me.GroupBox17.Location = New System.Drawing.Point(17, 397)
        Me.GroupBox17.Name = "GroupBox17"
        Me.GroupBox17.Size = New System.Drawing.Size(260, 40)
        Me.GroupBox17.TabIndex = 42
        Me.GroupBox17.TabStop = False
        Me.GroupBox17.Text = "Time Delta - Shift in MS"
        Me.GroupBox17.Visible = False
        '
        'Button5
        '
        Me.Button5.Location = New System.Drawing.Point(216, 12)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(40, 20)
        Me.Button5.TabIndex = 42
        Me.Button5.Text = "Set"
        '
        'TextBox3
        '
        Me.TextBox3.Location = New System.Drawing.Point(172, 12)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(40, 21)
        Me.TextBox3.TabIndex = 41
        Me.TextBox3.Text = "0"
        Me.TextBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TrackBar2
        '
        Me.TrackBar2.AutoSize = False
        Me.TrackBar2.LargeChange = 10
        Me.TrackBar2.Location = New System.Drawing.Point(4, 16)
        Me.TrackBar2.Maximum = 1000
        Me.TrackBar2.Minimum = -1000
        Me.TrackBar2.Name = "TrackBar2"
        Me.TrackBar2.Size = New System.Drawing.Size(164, 16)
        Me.TrackBar2.SmallChange = 4
        Me.TrackBar2.TabIndex = 30
        Me.TrackBar2.TickFrequency = 100
        '
        'GroupBox19
        '
        Me.GroupBox19.Controls.Add(Me.Label18)
        Me.GroupBox19.Controls.Add(Me.Label21)
        Me.GroupBox19.Controls.Add(Me.Button3)
        Me.GroupBox19.Controls.Add(Me.Button4)
        Me.GroupBox19.Controls.Add(Me.Label22)
        Me.GroupBox19.Controls.Add(Me.TextBox2)
        Me.GroupBox19.Enabled = False
        Me.GroupBox19.Location = New System.Drawing.Point(17, 305)
        Me.GroupBox19.Name = "GroupBox19"
        Me.GroupBox19.Size = New System.Drawing.Size(260, 84)
        Me.GroupBox19.TabIndex = 43
        Me.GroupBox19.TabStop = False
        Me.GroupBox19.Text = "Subpicture Capture"
        Me.GroupBox19.Visible = False
        '
        'Label18
        '
        Me.Label18.Location = New System.Drawing.Point(192, 60)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(64, 16)
        Me.Label18.TabIndex = 5
        '
        'Label21
        '
        Me.Label21.Location = New System.Drawing.Point(92, 60)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(96, 16)
        Me.Label21.TabIndex = 4
        Me.Label21.Text = "Frames Captured:"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(4, 56)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(80, 23)
        Me.Button3.TabIndex = 3
        Me.Button3.Text = "Start Capture"
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(204, 12)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(52, 20)
        Me.Button4.TabIndex = 2
        Me.Button4.Text = "Browse"
        '
        'Label22
        '
        Me.Label22.Location = New System.Drawing.Point(4, 16)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(100, 16)
        Me.Label22.TabIndex = 1
        Me.Label22.Text = "Target File"
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(4, 32)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(252, 21)
        Me.TextBox2.TabIndex = 0
        '
        'tpVideo
        '
        Me.tpVideo.Controls.Add(Me.GroupControl8)
        Me.tpVideo.Controls.Add(Me.GroupControl10)
        Me.tpVideo.Controls.Add(Me.gbVID_ProcAmp)
        Me.tpVideo.Controls.Add(Me.gbVID_MPEGFrameFilter)
        Me.tpVideo.Controls.Add(Me.gbVID_YUVFilter)
        Me.tpVideo.Controls.Add(Me.cbBumpFields)
        Me.tpVideo.Controls.Add(Me.GroupBox8)
        Me.tpVideo.Controls.Add(Me.cbForceFieldReversal)
        Me.tpVideo.Controls.Add(Me.btnProcAmpScrap)
        Me.tpVideo.Controls.Add(Me.btnVideoScrap)
        Me.tpVideo.Controls.Add(Me.GroupBox10)
        Me.tpVideo.Controls.Add(Me.GroupBox11)
        Me.tpVideo.Name = "tpVideo"
        Me.tpVideo.Size = New System.Drawing.Size(623, 419)
        Me.tpVideo.Text = "Video"
        '
        'GroupControl8
        '
        Me.GroupControl8.Controls.Add(Me.cbVID_NonSeamlessCellNotification)
        Me.GroupControl8.Location = New System.Drawing.Point(3, 198)
        Me.GroupControl8.Name = "GroupControl8"
        Me.GroupControl8.Size = New System.Drawing.Size(342, 49)
        Me.GroupControl8.TabIndex = 66
        Me.GroupControl8.Text = "DVD-Video"
        '
        'cbVID_NonSeamlessCellNotification
        '
        Me.cbVID_NonSeamlessCellNotification.Location = New System.Drawing.Point(7, 25)
        Me.cbVID_NonSeamlessCellNotification.Name = "cbVID_NonSeamlessCellNotification"
        Me.cbVID_NonSeamlessCellNotification.Size = New System.Drawing.Size(229, 16)
        Me.cbVID_NonSeamlessCellNotification.TabIndex = 61
        Me.cbVID_NonSeamlessCellNotification.Text = "Non-seamless cell / layerbreak notification"
        '
        'GroupControl10
        '
        Me.GroupControl10.Controls.Add(Me.cbVID_ReverseFieldOrder)
        Me.GroupControl10.Controls.Add(Me.btnTestPattern)
        Me.GroupControl10.Controls.Add(Me.btnColorBars)
        Me.GroupControl10.Controls.Add(Me.cbSplitFields)
        Me.GroupControl10.Location = New System.Drawing.Point(351, 3)
        Me.GroupControl10.Name = "GroupControl10"
        Me.GroupControl10.Size = New System.Drawing.Size(102, 129)
        Me.GroupControl10.TabIndex = 65
        Me.GroupControl10.Text = "Analysis"
        '
        'cbVID_ReverseFieldOrder
        '
        Me.cbVID_ReverseFieldOrder.Location = New System.Drawing.Point(5, 102)
        Me.cbVID_ReverseFieldOrder.Name = "cbVID_ReverseFieldOrder"
        Me.cbVID_ReverseFieldOrder.Properties.Caption = "Flip Field Order"
        Me.cbVID_ReverseFieldOrder.Size = New System.Drawing.Size(94, 18)
        Me.cbVID_ReverseFieldOrder.TabIndex = 66
        '
        'btnTestPattern
        '
        Me.btnTestPattern.Location = New System.Drawing.Point(7, 72)
        Me.btnTestPattern.Name = "btnTestPattern"
        Me.btnTestPattern.Size = New System.Drawing.Size(75, 23)
        Me.btnTestPattern.TabIndex = 63
        Me.btnTestPattern.Text = "Test Pattern"
        '
        'btnColorBars
        '
        Me.btnColorBars.Location = New System.Drawing.Point(7, 45)
        Me.btnColorBars.Name = "btnColorBars"
        Me.btnColorBars.Size = New System.Drawing.Size(75, 23)
        Me.btnColorBars.TabIndex = 62
        Me.btnColorBars.Text = "Color Bars"
        '
        'cbSplitFields
        '
        Me.cbSplitFields.Location = New System.Drawing.Point(7, 25)
        Me.cbSplitFields.Name = "cbSplitFields"
        Me.cbSplitFields.Size = New System.Drawing.Size(84, 16)
        Me.cbSplitFields.TabIndex = 61
        Me.cbSplitFields.Text = "Split Fields"
        '
        'gbVID_ProcAmp
        '
        Me.gbVID_ProcAmp.Controls.Add(Me.cbDoProcAmp)
        Me.gbVID_ProcAmp.Controls.Add(Me.cbProcAmpHalfFrame)
        Me.gbVID_ProcAmp.Controls.Add(Me.llPA_LoadSettings)
        Me.gbVID_ProcAmp.Controls.Add(Me.llPA_DeleteSettings)
        Me.gbVID_ProcAmp.Controls.Add(Me.tbBrightness)
        Me.gbVID_ProcAmp.Controls.Add(Me.llPA_SaveSettings)
        Me.gbVID_ProcAmp.Controls.Add(Me.Label8)
        Me.gbVID_ProcAmp.Controls.Add(Me.Label4)
        Me.gbVID_ProcAmp.Controls.Add(Me.Label5)
        Me.gbVID_ProcAmp.Controls.Add(Me.cbPA_Presets)
        Me.gbVID_ProcAmp.Controls.Add(Me.tbContrast)
        Me.gbVID_ProcAmp.Controls.Add(Me.Label6)
        Me.gbVID_ProcAmp.Controls.Add(Me.tbHue)
        Me.gbVID_ProcAmp.Controls.Add(Me.Label7)
        Me.gbVID_ProcAmp.Controls.Add(Me.tbSaturation)
        Me.gbVID_ProcAmp.Controls.Add(Me.txtContrast)
        Me.gbVID_ProcAmp.Controls.Add(Me.txtBrightness)
        Me.gbVID_ProcAmp.Controls.Add(Me.txtHue)
        Me.gbVID_ProcAmp.Controls.Add(Me.txtSaturation)
        Me.gbVID_ProcAmp.Location = New System.Drawing.Point(100, 3)
        Me.gbVID_ProcAmp.Name = "gbVID_ProcAmp"
        Me.gbVID_ProcAmp.Size = New System.Drawing.Size(245, 189)
        Me.gbVID_ProcAmp.TabIndex = 64
        Me.gbVID_ProcAmp.Text = "ProcAmp"
        '
        'cbDoProcAmp
        '
        Me.cbDoProcAmp.Location = New System.Drawing.Point(8, 45)
        Me.cbDoProcAmp.Name = "cbDoProcAmp"
        Me.cbDoProcAmp.Size = New System.Drawing.Size(88, 16)
        Me.cbDoProcAmp.TabIndex = 48
        Me.cbDoProcAmp.Text = "Do ProcAmp"
        '
        'cbProcAmpHalfFrame
        '
        Me.cbProcAmpHalfFrame.Location = New System.Drawing.Point(100, 45)
        Me.cbProcAmpHalfFrame.Name = "cbProcAmpHalfFrame"
        Me.cbProcAmpHalfFrame.Size = New System.Drawing.Size(84, 16)
        Me.cbProcAmpHalfFrame.TabIndex = 47
        Me.cbProcAmpHalfFrame.Text = "Half Frame"
        '
        'llPA_LoadSettings
        '
        Me.llPA_LoadSettings.Location = New System.Drawing.Point(164, 25)
        Me.llPA_LoadSettings.Name = "llPA_LoadSettings"
        Me.llPA_LoadSettings.Size = New System.Drawing.Size(76, 16)
        Me.llPA_LoadSettings.TabIndex = 45
        Me.llPA_LoadSettings.TabStop = True
        Me.llPA_LoadSettings.Text = "Load Settings"
        Me.llPA_LoadSettings.Visible = False
        '
        'llPA_DeleteSettings
        '
        Me.llPA_DeleteSettings.Location = New System.Drawing.Point(80, 25)
        Me.llPA_DeleteSettings.Name = "llPA_DeleteSettings"
        Me.llPA_DeleteSettings.Size = New System.Drawing.Size(84, 16)
        Me.llPA_DeleteSettings.TabIndex = 46
        Me.llPA_DeleteSettings.TabStop = True
        Me.llPA_DeleteSettings.Text = "Delete Settings"
        '
        'tbBrightness
        '
        Me.tbBrightness.AutoSize = False
        Me.tbBrightness.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.tbBrightness.LargeChange = 12
        Me.tbBrightness.Location = New System.Drawing.Point(68, 89)
        Me.tbBrightness.Maximum = 100
        Me.tbBrightness.Minimum = -100
        Me.tbBrightness.Name = "tbBrightness"
        Me.tbBrightness.Size = New System.Drawing.Size(140, 16)
        Me.tbBrightness.SmallChange = 6
        Me.tbBrightness.TabIndex = 1
        Me.tbBrightness.TickFrequency = 6
        '
        'llPA_SaveSettings
        '
        Me.llPA_SaveSettings.Location = New System.Drawing.Point(4, 25)
        Me.llPA_SaveSettings.Name = "llPA_SaveSettings"
        Me.llPA_SaveSettings.Size = New System.Drawing.Size(76, 16)
        Me.llPA_SaveSettings.TabIndex = 44
        Me.llPA_SaveSettings.TabStop = True
        Me.llPA_SaveSettings.Text = "Save Settings"
        '
        'Label8
        '
        Me.Label8.Location = New System.Drawing.Point(4, 89)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(60, 16)
        Me.Label8.TabIndex = 2
        Me.Label8.Text = "Brightness"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(16, 65)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(48, 23)
        Me.Label4.TabIndex = 43
        Me.Label4.Text = "Presets:"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(8, 113)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(56, 16)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "Contrast"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cbPA_Presets
        '
        Me.cbPA_Presets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbPA_Presets.Items.AddRange(New Object() {"Defaults", "MPEG Blocking", "Noise"})
        Me.cbPA_Presets.Location = New System.Drawing.Point(72, 65)
        Me.cbPA_Presets.Name = "cbPA_Presets"
        Me.cbPA_Presets.Size = New System.Drawing.Size(124, 21)
        Me.cbPA_Presets.TabIndex = 42
        '
        'tbContrast
        '
        Me.tbContrast.AutoSize = False
        Me.tbContrast.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.tbContrast.LargeChange = 12
        Me.tbContrast.Location = New System.Drawing.Point(68, 113)
        Me.tbContrast.Maximum = 100
        Me.tbContrast.Minimum = -100
        Me.tbContrast.Name = "tbContrast"
        Me.tbContrast.Size = New System.Drawing.Size(140, 16)
        Me.tbContrast.SmallChange = 6
        Me.tbContrast.TabIndex = 5
        Me.tbContrast.TickFrequency = 6
        Me.tbContrast.Value = -80
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(8, 137)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(56, 16)
        Me.Label6.TabIndex = 9
        Me.Label6.Text = "Hue"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'tbHue
        '
        Me.tbHue.AutoSize = False
        Me.tbHue.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.tbHue.LargeChange = 12
        Me.tbHue.Location = New System.Drawing.Point(68, 137)
        Me.tbHue.Maximum = 100
        Me.tbHue.Minimum = -100
        Me.tbHue.Name = "tbHue"
        Me.tbHue.Size = New System.Drawing.Size(140, 16)
        Me.tbHue.SmallChange = 6
        Me.tbHue.TabIndex = 8
        Me.tbHue.TickFrequency = 6
        '
        'Label7
        '
        Me.Label7.Location = New System.Drawing.Point(8, 161)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(56, 16)
        Me.Label7.TabIndex = 12
        Me.Label7.Text = "Saturation"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'tbSaturation
        '
        Me.tbSaturation.AutoSize = False
        Me.tbSaturation.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.tbSaturation.LargeChange = 12
        Me.tbSaturation.Location = New System.Drawing.Point(68, 161)
        Me.tbSaturation.Maximum = 100
        Me.tbSaturation.Minimum = -100
        Me.tbSaturation.Name = "tbSaturation"
        Me.tbSaturation.Size = New System.Drawing.Size(140, 16)
        Me.tbSaturation.SmallChange = 6
        Me.tbSaturation.TabIndex = 11
        Me.tbSaturation.TickFrequency = 6
        Me.tbSaturation.Value = -80
        '
        'txtContrast
        '
        Me.txtContrast.Location = New System.Drawing.Point(208, 111)
        Me.txtContrast.Name = "txtContrast"
        Me.txtContrast.Size = New System.Drawing.Size(28, 21)
        Me.txtContrast.TabIndex = 22
        Me.txtContrast.Text = "-80"
        Me.txtContrast.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtBrightness
        '
        Me.txtBrightness.Location = New System.Drawing.Point(208, 87)
        Me.txtBrightness.Name = "txtBrightness"
        Me.txtBrightness.Size = New System.Drawing.Size(28, 21)
        Me.txtBrightness.TabIndex = 21
        Me.txtBrightness.Text = "0"
        Me.txtBrightness.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtHue
        '
        Me.txtHue.Location = New System.Drawing.Point(208, 135)
        Me.txtHue.Name = "txtHue"
        Me.txtHue.Size = New System.Drawing.Size(28, 21)
        Me.txtHue.TabIndex = 23
        Me.txtHue.Text = "0"
        Me.txtHue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtSaturation
        '
        Me.txtSaturation.Location = New System.Drawing.Point(208, 159)
        Me.txtSaturation.Name = "txtSaturation"
        Me.txtSaturation.Size = New System.Drawing.Size(28, 21)
        Me.txtSaturation.TabIndex = 24
        Me.txtSaturation.Text = "-80"
        Me.txtSaturation.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'gbVID_MPEGFrameFilter
        '
        Me.gbVID_MPEGFrameFilter.Controls.Add(Me.rbVidMode_I)
        Me.gbVID_MPEGFrameFilter.Controls.Add(Me.rbVidMode_IBP)
        Me.gbVID_MPEGFrameFilter.Controls.Add(Me.rbVidMode_IP)
        Me.gbVID_MPEGFrameFilter.Location = New System.Drawing.Point(3, 107)
        Me.gbVID_MPEGFrameFilter.Name = "gbVID_MPEGFrameFilter"
        Me.gbVID_MPEGFrameFilter.Size = New System.Drawing.Size(91, 84)
        Me.gbVID_MPEGFrameFilter.TabIndex = 63
        Me.gbVID_MPEGFrameFilter.Text = "MPEG Frames"
        '
        'rbVidMode_I
        '
        Me.rbVidMode_I.Location = New System.Drawing.Point(5, 61)
        Me.rbVidMode_I.Name = "rbVidMode_I"
        Me.rbVidMode_I.Size = New System.Drawing.Size(80, 16)
        Me.rbVidMode_I.TabIndex = 5
        Me.rbVidMode_I.Text = "I"
        '
        'rbVidMode_IBP
        '
        Me.rbVidMode_IBP.Checked = True
        Me.rbVidMode_IBP.Location = New System.Drawing.Point(5, 25)
        Me.rbVidMode_IBP.Name = "rbVidMode_IBP"
        Me.rbVidMode_IBP.Size = New System.Drawing.Size(80, 16)
        Me.rbVidMode_IBP.TabIndex = 0
        Me.rbVidMode_IBP.TabStop = True
        Me.rbVidMode_IBP.Text = "IBP"
        '
        'rbVidMode_IP
        '
        Me.rbVidMode_IP.Location = New System.Drawing.Point(5, 43)
        Me.rbVidMode_IP.Name = "rbVidMode_IP"
        Me.rbVidMode_IP.Size = New System.Drawing.Size(80, 16)
        Me.rbVidMode_IP.TabIndex = 4
        Me.rbVidMode_IP.Text = "IP"
        '
        'gbVID_YUVFilter
        '
        Me.gbVID_YUVFilter.Controls.Add(Me.rbFilters_none)
        Me.gbVID_YUVFilter.Controls.Add(Me.rbFilters_YUV_MinusU)
        Me.gbVID_YUVFilter.Controls.Add(Me.rbFilters_bandw)
        Me.gbVID_YUVFilter.Controls.Add(Me.rbFilters_YUV_MinusV)
        Me.gbVID_YUVFilter.Location = New System.Drawing.Point(3, 3)
        Me.gbVID_YUVFilter.Name = "gbVID_YUVFilter"
        Me.gbVID_YUVFilter.Size = New System.Drawing.Size(91, 100)
        Me.gbVID_YUVFilter.TabIndex = 62
        Me.gbVID_YUVFilter.Text = "Color Filters"
        '
        'rbFilters_none
        '
        Me.rbFilters_none.Checked = True
        Me.rbFilters_none.Location = New System.Drawing.Point(7, 23)
        Me.rbFilters_none.Name = "rbFilters_none"
        Me.rbFilters_none.Size = New System.Drawing.Size(80, 16)
        Me.rbFilters_none.TabIndex = 6
        Me.rbFilters_none.TabStop = True
        Me.rbFilters_none.Text = "No Filter"
        '
        'rbFilters_YUV_MinusU
        '
        Me.rbFilters_YUV_MinusU.Location = New System.Drawing.Point(7, 77)
        Me.rbFilters_YUV_MinusU.Name = "rbFilters_YUV_MinusU"
        Me.rbFilters_YUV_MinusU.Size = New System.Drawing.Size(80, 16)
        Me.rbFilters_YUV_MinusU.TabIndex = 5
        Me.rbFilters_YUV_MinusU.Text = "- U"
        '
        'rbFilters_bandw
        '
        Me.rbFilters_bandw.Location = New System.Drawing.Point(7, 41)
        Me.rbFilters_bandw.Name = "rbFilters_bandw"
        Me.rbFilters_bandw.Size = New System.Drawing.Size(80, 16)
        Me.rbFilters_bandw.TabIndex = 0
        Me.rbFilters_bandw.Text = "B and W"
        '
        'rbFilters_YUV_MinusV
        '
        Me.rbFilters_YUV_MinusV.Location = New System.Drawing.Point(7, 59)
        Me.rbFilters_YUV_MinusV.Name = "rbFilters_YUV_MinusV"
        Me.rbFilters_YUV_MinusV.Size = New System.Drawing.Size(80, 16)
        Me.rbFilters_YUV_MinusV.TabIndex = 4
        Me.rbFilters_YUV_MinusV.Text = "- V"
        '
        'cbBumpFields
        '
        Me.cbBumpFields.Location = New System.Drawing.Point(215, 540)
        Me.cbBumpFields.Name = "cbBumpFields"
        Me.cbBumpFields.Size = New System.Drawing.Size(120, 16)
        Me.cbBumpFields.TabIndex = 59
        Me.cbBumpFields.Text = "Bump Fields Down"
        Me.cbBumpFields.Visible = False
        '
        'GroupBox8
        '
        Me.GroupBox8.Controls.Add(Me.btnDEINT_CheckStatus)
        Me.GroupBox8.Controls.Add(Me.GroupBox5)
        Me.GroupBox8.Controls.Add(Me.GroupBox9)
        Me.GroupBox8.Location = New System.Drawing.Point(581, 475)
        Me.GroupBox8.Name = "GroupBox8"
        Me.GroupBox8.Size = New System.Drawing.Size(264, 148)
        Me.GroupBox8.TabIndex = 58
        Me.GroupBox8.TabStop = False
        Me.GroupBox8.Text = "Deinterlacing"
        Me.GroupBox8.Visible = False
        '
        'btnDEINT_CheckStatus
        '
        Me.btnDEINT_CheckStatus.Location = New System.Drawing.Point(8, 120)
        Me.btnDEINT_CheckStatus.Name = "btnDEINT_CheckStatus"
        Me.btnDEINT_CheckStatus.Size = New System.Drawing.Size(92, 23)
        Me.btnDEINT_CheckStatus.TabIndex = 2
        Me.btnDEINT_CheckStatus.Text = "Check Status"
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.rbDEINT_Control_Video)
        Me.GroupBox5.Controls.Add(Me.rbDEINT_Control_Film)
        Me.GroupBox5.Controls.Add(Me.rbDEINT_Control_Smart)
        Me.GroupBox5.Controls.Add(Me.rbDEINT_Control_Auto)
        Me.GroupBox5.Location = New System.Drawing.Point(128, 16)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(120, 100)
        Me.GroupBox5.TabIndex = 1
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Control"
        '
        'rbDEINT_Control_Video
        '
        Me.rbDEINT_Control_Video.Location = New System.Drawing.Point(4, 64)
        Me.rbDEINT_Control_Video.Name = "rbDEINT_Control_Video"
        Me.rbDEINT_Control_Video.Size = New System.Drawing.Size(104, 16)
        Me.rbDEINT_Control_Video.TabIndex = 3
        Me.rbDEINT_Control_Video.Text = "Video"
        '
        'rbDEINT_Control_Film
        '
        Me.rbDEINT_Control_Film.Location = New System.Drawing.Point(4, 48)
        Me.rbDEINT_Control_Film.Name = "rbDEINT_Control_Film"
        Me.rbDEINT_Control_Film.Size = New System.Drawing.Size(104, 16)
        Me.rbDEINT_Control_Film.TabIndex = 2
        Me.rbDEINT_Control_Film.Text = "Film"
        '
        'rbDEINT_Control_Smart
        '
        Me.rbDEINT_Control_Smart.Location = New System.Drawing.Point(4, 32)
        Me.rbDEINT_Control_Smart.Name = "rbDEINT_Control_Smart"
        Me.rbDEINT_Control_Smart.Size = New System.Drawing.Size(104, 16)
        Me.rbDEINT_Control_Smart.TabIndex = 1
        Me.rbDEINT_Control_Smart.Text = "Smart"
        '
        'rbDEINT_Control_Auto
        '
        Me.rbDEINT_Control_Auto.Location = New System.Drawing.Point(4, 16)
        Me.rbDEINT_Control_Auto.Name = "rbDEINT_Control_Auto"
        Me.rbDEINT_Control_Auto.Size = New System.Drawing.Size(104, 16)
        Me.rbDEINT_Control_Auto.TabIndex = 0
        Me.rbDEINT_Control_Auto.Text = "Auto"
        '
        'GroupBox9
        '
        Me.GroupBox9.Controls.Add(Me.rbDEINT_Mode_WeaveFiltered)
        Me.GroupBox9.Controls.Add(Me.rbDEINT_Mode_Spad)
        Me.GroupBox9.Controls.Add(Me.rbDEINT_Mode_Bob)
        Me.GroupBox9.Controls.Add(Me.rbDEINT_Mode_Weave)
        Me.GroupBox9.Controls.Add(Me.rbDEINT_Mode_Normal)
        Me.GroupBox9.Location = New System.Drawing.Point(4, 16)
        Me.GroupBox9.Name = "GroupBox9"
        Me.GroupBox9.Size = New System.Drawing.Size(120, 100)
        Me.GroupBox9.TabIndex = 0
        Me.GroupBox9.TabStop = False
        Me.GroupBox9.Text = "Mode"
        '
        'rbDEINT_Mode_WeaveFiltered
        '
        Me.rbDEINT_Mode_WeaveFiltered.Location = New System.Drawing.Point(4, 78)
        Me.rbDEINT_Mode_WeaveFiltered.Name = "rbDEINT_Mode_WeaveFiltered"
        Me.rbDEINT_Mode_WeaveFiltered.Size = New System.Drawing.Size(104, 16)
        Me.rbDEINT_Mode_WeaveFiltered.TabIndex = 4
        Me.rbDEINT_Mode_WeaveFiltered.Text = "Weave Filtered"
        '
        'rbDEINT_Mode_Spad
        '
        Me.rbDEINT_Mode_Spad.Location = New System.Drawing.Point(4, 64)
        Me.rbDEINT_Mode_Spad.Name = "rbDEINT_Mode_Spad"
        Me.rbDEINT_Mode_Spad.Size = New System.Drawing.Size(104, 16)
        Me.rbDEINT_Mode_Spad.TabIndex = 3
        Me.rbDEINT_Mode_Spad.Text = "Spad"
        '
        'rbDEINT_Mode_Bob
        '
        Me.rbDEINT_Mode_Bob.Location = New System.Drawing.Point(4, 48)
        Me.rbDEINT_Mode_Bob.Name = "rbDEINT_Mode_Bob"
        Me.rbDEINT_Mode_Bob.Size = New System.Drawing.Size(104, 16)
        Me.rbDEINT_Mode_Bob.TabIndex = 2
        Me.rbDEINT_Mode_Bob.Text = "Bob"
        '
        'rbDEINT_Mode_Weave
        '
        Me.rbDEINT_Mode_Weave.Location = New System.Drawing.Point(4, 32)
        Me.rbDEINT_Mode_Weave.Name = "rbDEINT_Mode_Weave"
        Me.rbDEINT_Mode_Weave.Size = New System.Drawing.Size(104, 16)
        Me.rbDEINT_Mode_Weave.TabIndex = 1
        Me.rbDEINT_Mode_Weave.Text = "Weave"
        '
        'rbDEINT_Mode_Normal
        '
        Me.rbDEINT_Mode_Normal.Location = New System.Drawing.Point(4, 16)
        Me.rbDEINT_Mode_Normal.Name = "rbDEINT_Mode_Normal"
        Me.rbDEINT_Mode_Normal.Size = New System.Drawing.Size(104, 16)
        Me.rbDEINT_Mode_Normal.TabIndex = 0
        Me.rbDEINT_Mode_Normal.Text = "Normal"
        '
        'cbForceFieldReversal
        '
        Me.cbForceFieldReversal.Location = New System.Drawing.Point(335, 540)
        Me.cbForceFieldReversal.Name = "cbForceFieldReversal"
        Me.cbForceFieldReversal.Size = New System.Drawing.Size(128, 16)
        Me.cbForceFieldReversal.TabIndex = 57
        Me.cbForceFieldReversal.Text = "Reverse Field Order"
        Me.cbForceFieldReversal.Visible = False
        '
        'btnProcAmpScrap
        '
        Me.btnProcAmpScrap.Enabled = False
        Me.btnProcAmpScrap.Location = New System.Drawing.Point(215, 562)
        Me.btnProcAmpScrap.Name = "btnProcAmpScrap"
        Me.btnProcAmpScrap.Size = New System.Drawing.Size(96, 20)
        Me.btnProcAmpScrap.TabIndex = 55
        Me.btnProcAmpScrap.Text = "PA Scrap"
        Me.btnProcAmpScrap.Visible = False
        '
        'btnVideoScrap
        '
        Me.btnVideoScrap.Location = New System.Drawing.Point(311, 562)
        Me.btnVideoScrap.Name = "btnVideoScrap"
        Me.btnVideoScrap.Size = New System.Drawing.Size(96, 20)
        Me.btnVideoScrap.TabIndex = 52
        Me.btnVideoScrap.Text = "Scrap"
        Me.btnVideoScrap.Visible = False
        '
        'GroupBox10
        '
        Me.GroupBox10.Controls.Add(Me.btnTimeDeltaSet)
        Me.GroupBox10.Controls.Add(Me.txtTimeDeltaVal)
        Me.GroupBox10.Controls.Add(Me.tbTimeDelta)
        Me.GroupBox10.Enabled = False
        Me.GroupBox10.Location = New System.Drawing.Point(3, 527)
        Me.GroupBox10.Name = "GroupBox10"
        Me.GroupBox10.Size = New System.Drawing.Size(204, 60)
        Me.GroupBox10.TabIndex = 51
        Me.GroupBox10.TabStop = False
        Me.GroupBox10.Text = "Time Delta - Shift in MS"
        Me.GroupBox10.Visible = False
        '
        'btnTimeDeltaSet
        '
        Me.btnTimeDeltaSet.Location = New System.Drawing.Point(48, 36)
        Me.btnTimeDeltaSet.Name = "btnTimeDeltaSet"
        Me.btnTimeDeltaSet.Size = New System.Drawing.Size(40, 20)
        Me.btnTimeDeltaSet.TabIndex = 42
        Me.btnTimeDeltaSet.Text = "Set"
        '
        'txtTimeDeltaVal
        '
        Me.txtTimeDeltaVal.Location = New System.Drawing.Point(4, 36)
        Me.txtTimeDeltaVal.Name = "txtTimeDeltaVal"
        Me.txtTimeDeltaVal.Size = New System.Drawing.Size(40, 21)
        Me.txtTimeDeltaVal.TabIndex = 41
        Me.txtTimeDeltaVal.Text = "0"
        Me.txtTimeDeltaVal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'tbTimeDelta
        '
        Me.tbTimeDelta.AutoSize = False
        Me.tbTimeDelta.LargeChange = 10
        Me.tbTimeDelta.Location = New System.Drawing.Point(4, 16)
        Me.tbTimeDelta.Maximum = 1000
        Me.tbTimeDelta.Minimum = -1000
        Me.tbTimeDelta.Name = "tbTimeDelta"
        Me.tbTimeDelta.Size = New System.Drawing.Size(196, 16)
        Me.tbTimeDelta.SmallChange = 4
        Me.tbTimeDelta.TabIndex = 30
        Me.tbTimeDelta.TickFrequency = 100
        '
        'GroupBox11
        '
        Me.GroupBox11.Controls.Add(Me.lblFramesCaptured)
        Me.GroupBox11.Controls.Add(Me.Label17)
        Me.GroupBox11.Controls.Add(Me.btnStartStopVideoCapture)
        Me.GroupBox11.Controls.Add(Me.btnCaptureBrowse)
        Me.GroupBox11.Controls.Add(Me.Label16)
        Me.GroupBox11.Controls.Add(Me.txtCaptureFileName)
        Me.GroupBox11.Enabled = False
        Me.GroupBox11.Location = New System.Drawing.Point(416, 313)
        Me.GroupBox11.Name = "GroupBox11"
        Me.GroupBox11.Size = New System.Drawing.Size(204, 100)
        Me.GroupBox11.TabIndex = 50
        Me.GroupBox11.TabStop = False
        Me.GroupBox11.Text = "Video Capture"
        Me.GroupBox11.Visible = False
        '
        'lblFramesCaptured
        '
        Me.lblFramesCaptured.Location = New System.Drawing.Point(104, 80)
        Me.lblFramesCaptured.Name = "lblFramesCaptured"
        Me.lblFramesCaptured.Size = New System.Drawing.Size(72, 16)
        Me.lblFramesCaptured.TabIndex = 5
        '
        'Label17
        '
        Me.Label17.Location = New System.Drawing.Point(4, 80)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(100, 16)
        Me.Label17.TabIndex = 4
        Me.Label17.Text = "Frames Captured:"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnStartStopVideoCapture
        '
        Me.btnStartStopVideoCapture.Location = New System.Drawing.Point(120, 52)
        Me.btnStartStopVideoCapture.Name = "btnStartStopVideoCapture"
        Me.btnStartStopVideoCapture.Size = New System.Drawing.Size(80, 20)
        Me.btnStartStopVideoCapture.TabIndex = 3
        Me.btnStartStopVideoCapture.Text = "Start Capture"
        '
        'btnCaptureBrowse
        '
        Me.btnCaptureBrowse.Location = New System.Drawing.Point(148, 12)
        Me.btnCaptureBrowse.Name = "btnCaptureBrowse"
        Me.btnCaptureBrowse.Size = New System.Drawing.Size(52, 20)
        Me.btnCaptureBrowse.TabIndex = 2
        Me.btnCaptureBrowse.Text = "Browse"
        '
        'Label16
        '
        Me.Label16.Location = New System.Drawing.Point(4, 16)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(100, 16)
        Me.Label16.TabIndex = 1
        Me.Label16.Text = "Target File"
        '
        'txtCaptureFileName
        '
        Me.txtCaptureFileName.Location = New System.Drawing.Point(4, 32)
        Me.txtCaptureFileName.Name = "txtCaptureFileName"
        Me.txtCaptureFileName.Size = New System.Drawing.Size(196, 21)
        Me.txtCaptureFileName.TabIndex = 0
        '
        'tpAudio
        '
        Me.tpAudio.Controls.Add(Me.GroupBox13)
        Me.tpAudio.Controls.Add(Me.GroupBox14)
        Me.tpAudio.Name = "tpAudio"
        Me.tpAudio.PageVisible = False
        Me.tpAudio.Size = New System.Drawing.Size(623, 417)
        Me.tpAudio.Text = "Audio"
        '
        'GroupBox13
        '
        Me.GroupBox13.Controls.Add(Me.AU_lblFramesCaptured)
        Me.GroupBox13.Controls.Add(Me.Label19)
        Me.GroupBox13.Controls.Add(Me.AU_btnStartStopVideoCapture)
        Me.GroupBox13.Controls.Add(Me.AU_btnCaptureBrowse)
        Me.GroupBox13.Controls.Add(Me.Label20)
        Me.GroupBox13.Controls.Add(Me.AU_txtCaptureFileName)
        Me.GroupBox13.Enabled = False
        Me.GroupBox13.Location = New System.Drawing.Point(1, 70)
        Me.GroupBox13.Name = "GroupBox13"
        Me.GroupBox13.Size = New System.Drawing.Size(260, 84)
        Me.GroupBox13.TabIndex = 45
        Me.GroupBox13.TabStop = False
        Me.GroupBox13.Text = "Audio Capture"
        Me.GroupBox13.Visible = False
        '
        'AU_lblFramesCaptured
        '
        Me.AU_lblFramesCaptured.Location = New System.Drawing.Point(192, 60)
        Me.AU_lblFramesCaptured.Name = "AU_lblFramesCaptured"
        Me.AU_lblFramesCaptured.Size = New System.Drawing.Size(64, 16)
        Me.AU_lblFramesCaptured.TabIndex = 5
        '
        'Label19
        '
        Me.Label19.Location = New System.Drawing.Point(92, 60)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(96, 16)
        Me.Label19.TabIndex = 4
        Me.Label19.Text = "Frames Captured:"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'AU_btnStartStopVideoCapture
        '
        Me.AU_btnStartStopVideoCapture.Location = New System.Drawing.Point(4, 56)
        Me.AU_btnStartStopVideoCapture.Name = "AU_btnStartStopVideoCapture"
        Me.AU_btnStartStopVideoCapture.Size = New System.Drawing.Size(80, 23)
        Me.AU_btnStartStopVideoCapture.TabIndex = 3
        Me.AU_btnStartStopVideoCapture.Text = "Start Capture"
        '
        'AU_btnCaptureBrowse
        '
        Me.AU_btnCaptureBrowse.Location = New System.Drawing.Point(204, 12)
        Me.AU_btnCaptureBrowse.Name = "AU_btnCaptureBrowse"
        Me.AU_btnCaptureBrowse.Size = New System.Drawing.Size(52, 20)
        Me.AU_btnCaptureBrowse.TabIndex = 2
        Me.AU_btnCaptureBrowse.Text = "Browse"
        '
        'Label20
        '
        Me.Label20.Location = New System.Drawing.Point(4, 16)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(100, 16)
        Me.Label20.TabIndex = 1
        Me.Label20.Text = "Target File"
        '
        'AU_txtCaptureFileName
        '
        Me.AU_txtCaptureFileName.Location = New System.Drawing.Point(4, 32)
        Me.AU_txtCaptureFileName.Name = "AU_txtCaptureFileName"
        Me.AU_txtCaptureFileName.Size = New System.Drawing.Size(252, 21)
        Me.AU_txtCaptureFileName.TabIndex = 0
        '
        'GroupBox14
        '
        Me.GroupBox14.Controls.Add(Me.AU_btnTimeDeltaSet)
        Me.GroupBox14.Controls.Add(Me.AU_txtTimeDeltaVal)
        Me.GroupBox14.Controls.Add(Me.AU_tbTimeDelta)
        Me.GroupBox14.Enabled = False
        Me.GroupBox14.Location = New System.Drawing.Point(1, 154)
        Me.GroupBox14.Name = "GroupBox14"
        Me.GroupBox14.Size = New System.Drawing.Size(260, 40)
        Me.GroupBox14.TabIndex = 44
        Me.GroupBox14.TabStop = False
        Me.GroupBox14.Text = "Time Delta - Shift in MS"
        Me.GroupBox14.Visible = False
        '
        'AU_btnTimeDeltaSet
        '
        Me.AU_btnTimeDeltaSet.Location = New System.Drawing.Point(216, 12)
        Me.AU_btnTimeDeltaSet.Name = "AU_btnTimeDeltaSet"
        Me.AU_btnTimeDeltaSet.Size = New System.Drawing.Size(40, 20)
        Me.AU_btnTimeDeltaSet.TabIndex = 42
        Me.AU_btnTimeDeltaSet.Text = "Set"
        '
        'AU_txtTimeDeltaVal
        '
        Me.AU_txtTimeDeltaVal.Location = New System.Drawing.Point(172, 12)
        Me.AU_txtTimeDeltaVal.Name = "AU_txtTimeDeltaVal"
        Me.AU_txtTimeDeltaVal.Size = New System.Drawing.Size(40, 21)
        Me.AU_txtTimeDeltaVal.TabIndex = 41
        Me.AU_txtTimeDeltaVal.Text = "0"
        Me.AU_txtTimeDeltaVal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'AU_tbTimeDelta
        '
        Me.AU_tbTimeDelta.AutoSize = False
        Me.AU_tbTimeDelta.LargeChange = 10
        Me.AU_tbTimeDelta.Location = New System.Drawing.Point(4, 16)
        Me.AU_tbTimeDelta.Maximum = 1000
        Me.AU_tbTimeDelta.Minimum = -1000
        Me.AU_tbTimeDelta.Name = "AU_tbTimeDelta"
        Me.AU_tbTimeDelta.Size = New System.Drawing.Size(164, 16)
        Me.AU_tbTimeDelta.SmallChange = 4
        Me.AU_tbTimeDelta.TabIndex = 30
        Me.AU_tbTimeDelta.TickFrequency = 100
        '
        'tpClosedCaptions
        '
        Me.tpClosedCaptions.Controls.Add(Me.gcLine21Placement)
        Me.tpClosedCaptions.Controls.Add(Me.GroupBox21)
        Me.tpClosedCaptions.Controls.Add(Me.GroupBox22)
        Me.tpClosedCaptions.Name = "tpClosedCaptions"
        Me.tpClosedCaptions.Size = New System.Drawing.Size(623, 417)
        Me.tpClosedCaptions.Text = "Closed Captions"
        '
        'gcLine21Placement
        '
        Me.gcLine21Placement.Controls.Add(Me.cbClosedCaptionPlacement)
        Me.gcLine21Placement.Controls.Add(Me.txtCCPlacementY)
        Me.gcLine21Placement.Controls.Add(Me.tbClosedCaptionPlacement_X)
        Me.gcLine21Placement.Controls.Add(Me.txtCCPlacementX)
        Me.gcLine21Placement.Controls.Add(Me.Label29)
        Me.gcLine21Placement.Controls.Add(Me.Label10)
        Me.gcLine21Placement.Controls.Add(Me.tbClosedCaptionPlacement_Y)
        Me.gcLine21Placement.Location = New System.Drawing.Point(3, 3)
        Me.gcLine21Placement.Name = "gcLine21Placement"
        Me.gcLine21Placement.Size = New System.Drawing.Size(226, 100)
        Me.gcLine21Placement.TabIndex = 48
        Me.gcLine21Placement.Text = "Placement"
        '
        'cbClosedCaptionPlacement
        '
        Me.cbClosedCaptionPlacement.Location = New System.Drawing.Point(8, 26)
        Me.cbClosedCaptionPlacement.Name = "cbClosedCaptionPlacement"
        Me.cbClosedCaptionPlacement.Size = New System.Drawing.Size(176, 16)
        Me.cbClosedCaptionPlacement.TabIndex = 0
        Me.cbClosedCaptionPlacement.Text = "Enable Placement Adjustment"
        '
        'txtCCPlacementY
        '
        Me.txtCCPlacementY.Location = New System.Drawing.Point(188, 70)
        Me.txtCCPlacementY.Name = "txtCCPlacementY"
        Me.txtCCPlacementY.Size = New System.Drawing.Size(32, 21)
        Me.txtCCPlacementY.TabIndex = 4
        Me.txtCCPlacementY.Text = "0"
        Me.txtCCPlacementY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'tbClosedCaptionPlacement_X
        '
        Me.tbClosedCaptionPlacement_X.AutoSize = False
        Me.tbClosedCaptionPlacement_X.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.tbClosedCaptionPlacement_X.LargeChange = 50
        Me.tbClosedCaptionPlacement_X.Location = New System.Drawing.Point(24, 46)
        Me.tbClosedCaptionPlacement_X.Maximum = 720
        Me.tbClosedCaptionPlacement_X.Minimum = -720
        Me.tbClosedCaptionPlacement_X.Name = "tbClosedCaptionPlacement_X"
        Me.tbClosedCaptionPlacement_X.Size = New System.Drawing.Size(164, 16)
        Me.tbClosedCaptionPlacement_X.SmallChange = 10
        Me.tbClosedCaptionPlacement_X.TabIndex = 1
        Me.tbClosedCaptionPlacement_X.TickFrequency = 50
        '
        'txtCCPlacementX
        '
        Me.txtCCPlacementX.Location = New System.Drawing.Point(188, 46)
        Me.txtCCPlacementX.Name = "txtCCPlacementX"
        Me.txtCCPlacementX.Size = New System.Drawing.Size(32, 21)
        Me.txtCCPlacementX.TabIndex = 3
        Me.txtCCPlacementX.Text = "0"
        Me.txtCCPlacementX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label29
        '
        Me.Label29.Location = New System.Drawing.Point(4, 46)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(24, 16)
        Me.Label29.TabIndex = 32
        Me.Label29.Text = "X"
        Me.Label29.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label10
        '
        Me.Label10.Location = New System.Drawing.Point(4, 70)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(24, 16)
        Me.Label10.TabIndex = 34
        Me.Label10.Text = "Y"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'tbClosedCaptionPlacement_Y
        '
        Me.tbClosedCaptionPlacement_Y.AutoSize = False
        Me.tbClosedCaptionPlacement_Y.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.tbClosedCaptionPlacement_Y.LargeChange = 50
        Me.tbClosedCaptionPlacement_Y.Location = New System.Drawing.Point(24, 70)
        Me.tbClosedCaptionPlacement_Y.Maximum = 576
        Me.tbClosedCaptionPlacement_Y.Minimum = -576
        Me.tbClosedCaptionPlacement_Y.Name = "tbClosedCaptionPlacement_Y"
        Me.tbClosedCaptionPlacement_Y.Size = New System.Drawing.Size(164, 16)
        Me.tbClosedCaptionPlacement_Y.SmallChange = 10
        Me.tbClosedCaptionPlacement_Y.TabIndex = 2
        Me.tbClosedCaptionPlacement_Y.TickFrequency = 50
        '
        'GroupBox21
        '
        Me.GroupBox21.Controls.Add(Me.Label32)
        Me.GroupBox21.Controls.Add(Me.Label33)
        Me.GroupBox21.Controls.Add(Me.Button14)
        Me.GroupBox21.Controls.Add(Me.Button15)
        Me.GroupBox21.Controls.Add(Me.Label34)
        Me.GroupBox21.Controls.Add(Me.TextBox11)
        Me.GroupBox21.Enabled = False
        Me.GroupBox21.Location = New System.Drawing.Point(33, 313)
        Me.GroupBox21.Name = "GroupBox21"
        Me.GroupBox21.Size = New System.Drawing.Size(260, 84)
        Me.GroupBox21.TabIndex = 46
        Me.GroupBox21.TabStop = False
        Me.GroupBox21.Text = "Line21 Image Capture"
        Me.GroupBox21.Visible = False
        '
        'Label32
        '
        Me.Label32.Location = New System.Drawing.Point(192, 60)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(64, 16)
        Me.Label32.TabIndex = 5
        '
        'Label33
        '
        Me.Label33.Location = New System.Drawing.Point(92, 60)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(96, 16)
        Me.Label33.TabIndex = 4
        Me.Label33.Text = "Frames Captured:"
        Me.Label33.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Button14
        '
        Me.Button14.Location = New System.Drawing.Point(4, 56)
        Me.Button14.Name = "Button14"
        Me.Button14.Size = New System.Drawing.Size(80, 23)
        Me.Button14.TabIndex = 3
        Me.Button14.Text = "Start Capture"
        '
        'Button15
        '
        Me.Button15.Location = New System.Drawing.Point(204, 12)
        Me.Button15.Name = "Button15"
        Me.Button15.Size = New System.Drawing.Size(52, 20)
        Me.Button15.TabIndex = 2
        Me.Button15.Text = "Browse"
        '
        'Label34
        '
        Me.Label34.Location = New System.Drawing.Point(4, 16)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(100, 16)
        Me.Label34.TabIndex = 1
        Me.Label34.Text = "Target File"
        '
        'TextBox11
        '
        Me.TextBox11.Location = New System.Drawing.Point(4, 32)
        Me.TextBox11.Name = "TextBox11"
        Me.TextBox11.Size = New System.Drawing.Size(252, 21)
        Me.TextBox11.TabIndex = 0
        '
        'GroupBox22
        '
        Me.GroupBox22.Controls.Add(Me.Label30)
        Me.GroupBox22.Controls.Add(Me.Label35)
        Me.GroupBox22.Controls.Add(Me.Button12)
        Me.GroupBox22.Controls.Add(Me.Button13)
        Me.GroupBox22.Controls.Add(Me.Label31)
        Me.GroupBox22.Controls.Add(Me.TextBox10)
        Me.GroupBox22.Enabled = False
        Me.GroupBox22.Location = New System.Drawing.Point(33, 397)
        Me.GroupBox22.Name = "GroupBox22"
        Me.GroupBox22.Size = New System.Drawing.Size(260, 84)
        Me.GroupBox22.TabIndex = 45
        Me.GroupBox22.TabStop = False
        Me.GroupBox22.Text = "Line21 Data Capture"
        Me.GroupBox22.Visible = False
        '
        'Label30
        '
        Me.Label30.Location = New System.Drawing.Point(192, 60)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(64, 16)
        Me.Label30.TabIndex = 5
        Me.Label30.Visible = False
        '
        'Label35
        '
        Me.Label35.Location = New System.Drawing.Point(92, 60)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(96, 16)
        Me.Label35.TabIndex = 4
        Me.Label35.Text = "Frames Captured:"
        Me.Label35.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Button12
        '
        Me.Button12.Location = New System.Drawing.Point(4, 56)
        Me.Button12.Name = "Button12"
        Me.Button12.Size = New System.Drawing.Size(80, 23)
        Me.Button12.TabIndex = 3
        Me.Button12.Text = "Start Capture"
        '
        'Button13
        '
        Me.Button13.Location = New System.Drawing.Point(204, 12)
        Me.Button13.Name = "Button13"
        Me.Button13.Size = New System.Drawing.Size(52, 20)
        Me.Button13.TabIndex = 2
        Me.Button13.Text = "Browse"
        '
        'Label31
        '
        Me.Label31.Location = New System.Drawing.Point(4, 16)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(100, 16)
        Me.Label31.TabIndex = 1
        Me.Label31.Text = "Target File"
        '
        'TextBox10
        '
        Me.TextBox10.Location = New System.Drawing.Point(4, 32)
        Me.TextBox10.Name = "TextBox10"
        Me.TextBox10.Size = New System.Drawing.Size(252, 21)
        Me.TextBox10.TabIndex = 0
        '
        'tpGuides
        '
        Me.tpGuides.Controls.Add(Me.GroupControl2)
        Me.tpGuides.Name = "tpGuides"
        Me.tpGuides.Size = New System.Drawing.Size(623, 417)
        Me.tpGuides.Text = "Guides"
        '
        'GroupControl2
        '
        Me.GroupControl2.Controls.Add(Me.GroupControl5)
        Me.GroupControl2.Controls.Add(Me.GroupControl4)
        Me.GroupControl2.Controls.Add(Me.GroupControl3)
        Me.GroupControl2.Controls.Add(Me.Label36)
        Me.GroupControl2.Controls.Add(Me.ceGuideColor)
        Me.GroupControl2.Controls.Add(Me.Label28)
        Me.GroupControl2.Controls.Add(Me.Label27)
        Me.GroupControl2.Controls.Add(Me.Label25)
        Me.GroupControl2.Controls.Add(Me.Label26)
        Me.GroupControl2.Controls.Add(Me.cbEnableFlexGuides)
        Me.GroupControl2.Controls.Add(Me.txtGuide_R)
        Me.GroupControl2.Controls.Add(Me.tbGuide_T)
        Me.GroupControl2.Controls.Add(Me.txtGuide_L)
        Me.GroupControl2.Controls.Add(Me.tbGuide_R)
        Me.GroupControl2.Controls.Add(Me.txtGuide_B)
        Me.GroupControl2.Controls.Add(Me.tbGuide_B)
        Me.GroupControl2.Controls.Add(Me.txtGuide_T)
        Me.GroupControl2.Controls.Add(Me.tbGuide_L)
        Me.GroupControl2.Location = New System.Drawing.Point(3, 3)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.Size = New System.Drawing.Size(616, 273)
        Me.GroupControl2.TabIndex = 93
        Me.GroupControl2.Text = "Flex Guides"
        '
        'GroupControl5
        '
        Me.GroupControl5.Controls.Add(Me.Label1)
        Me.GroupControl5.Controls.Add(Me.lblGuideBox_W)
        Me.GroupControl5.Controls.Add(Me.lblGuideBox_H)
        Me.GroupControl5.Controls.Add(Me.Label2)
        Me.GroupControl5.Controls.Add(Me.Label3)
        Me.GroupControl5.Controls.Add(Me.lblAspect)
        Me.GroupControl5.Location = New System.Drawing.Point(487, 41)
        Me.GroupControl5.Name = "GroupControl5"
        Me.GroupControl5.Size = New System.Drawing.Size(123, 104)
        Me.GroupControl5.TabIndex = 90
        Me.GroupControl5.Text = "Guide Info"
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(13, 34)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(44, 16)
        Me.Label1.TabIndex = 64
        Me.Label1.Text = "Width:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblGuideBox_W
        '
        Me.lblGuideBox_W.Location = New System.Drawing.Point(57, 34)
        Me.lblGuideBox_W.Name = "lblGuideBox_W"
        Me.lblGuideBox_W.Size = New System.Drawing.Size(44, 16)
        Me.lblGuideBox_W.TabIndex = 65
        '
        'lblGuideBox_H
        '
        Me.lblGuideBox_H.Location = New System.Drawing.Point(57, 50)
        Me.lblGuideBox_H.Name = "lblGuideBox_H"
        Me.lblGuideBox_H.Size = New System.Drawing.Size(44, 16)
        Me.lblGuideBox_H.TabIndex = 66
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(13, 50)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(44, 16)
        Me.Label2.TabIndex = 67
        Me.Label2.Text = "Height:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(13, 66)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(44, 16)
        Me.Label3.TabIndex = 69
        Me.Label3.Text = "Aspect:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblAspect
        '
        Me.lblAspect.Location = New System.Drawing.Point(57, 66)
        Me.lblAspect.Name = "lblAspect"
        Me.lblAspect.Size = New System.Drawing.Size(44, 16)
        Me.lblAspect.TabIndex = 68
        '
        'GroupControl4
        '
        Me.GroupControl4.Controls.Add(Me.cbGDS_SavedGuides)
        Me.GroupControl4.Controls.Add(Me.btnGDS_DeleteSelectedGuides)
        Me.GroupControl4.Controls.Add(Me.btnGDS_LoadSelectedGuides)
        Me.GroupControl4.Controls.Add(Me.Label37)
        Me.GroupControl4.Controls.Add(Me.btnGDS_SaveCurrentGuides)
        Me.GroupControl4.Location = New System.Drawing.Point(5, 188)
        Me.GroupControl4.Name = "GroupControl4"
        Me.GroupControl4.Size = New System.Drawing.Size(539, 79)
        Me.GroupControl4.TabIndex = 89
        Me.GroupControl4.Text = "Save/Load Guides"
        '
        'cbGDS_SavedGuides
        '
        Me.cbGDS_SavedGuides.Location = New System.Drawing.Point(83, 52)
        Me.cbGDS_SavedGuides.Name = "cbGDS_SavedGuides"
        Me.cbGDS_SavedGuides.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cbGDS_SavedGuides.Size = New System.Drawing.Size(190, 20)
        Me.cbGDS_SavedGuides.TabIndex = 70
        '
        'btnGDS_DeleteSelectedGuides
        '
        Me.btnGDS_DeleteSelectedGuides.Location = New System.Drawing.Point(410, 51)
        Me.btnGDS_DeleteSelectedGuides.Name = "btnGDS_DeleteSelectedGuides"
        Me.btnGDS_DeleteSelectedGuides.Size = New System.Drawing.Size(125, 23)
        Me.btnGDS_DeleteSelectedGuides.TabIndex = 73
        Me.btnGDS_DeleteSelectedGuides.Text = "Delete Selected"
        '
        'btnGDS_LoadSelectedGuides
        '
        Me.btnGDS_LoadSelectedGuides.Location = New System.Drawing.Point(279, 51)
        Me.btnGDS_LoadSelectedGuides.Name = "btnGDS_LoadSelectedGuides"
        Me.btnGDS_LoadSelectedGuides.Size = New System.Drawing.Size(125, 23)
        Me.btnGDS_LoadSelectedGuides.TabIndex = 72
        Me.btnGDS_LoadSelectedGuides.Text = "Load Selected"
        '
        'Label37
        '
        Me.Label37.Location = New System.Drawing.Point(9, 53)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(76, 16)
        Me.Label37.TabIndex = 71
        Me.Label37.Text = "Saved Guides:"
        Me.Label37.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnGDS_SaveCurrentGuides
        '
        Me.btnGDS_SaveCurrentGuides.Location = New System.Drawing.Point(5, 23)
        Me.btnGDS_SaveCurrentGuides.Name = "btnGDS_SaveCurrentGuides"
        Me.btnGDS_SaveCurrentGuides.Size = New System.Drawing.Size(268, 23)
        Me.btnGDS_SaveCurrentGuides.TabIndex = 69
        Me.btnGDS_SaveCurrentGuides.Text = "Save Current Guides"
        '
        'GroupControl3
        '
        Me.GroupControl3.Controls.Add(Me.btnGDS_MoveAll_Down)
        Me.GroupControl3.Controls.Add(Me.btnGDS_MoveAll_Up)
        Me.GroupControl3.Controls.Add(Me.btnGDS_MoveAll_Right)
        Me.GroupControl3.Controls.Add(Me.btnGDS_MoveAll_Left)
        Me.GroupControl3.Location = New System.Drawing.Point(358, 41)
        Me.GroupControl3.Name = "GroupControl3"
        Me.GroupControl3.Size = New System.Drawing.Size(123, 104)
        Me.GroupControl3.TabIndex = 88
        Me.GroupControl3.Text = "Move All"
        '
        'btnGDS_MoveAll_Down
        '
        Me.btnGDS_MoveAll_Down.Location = New System.Drawing.Point(33, 74)
        Me.btnGDS_MoveAll_Down.Name = "btnGDS_MoveAll_Down"
        Me.btnGDS_MoveAll_Down.Size = New System.Drawing.Size(54, 23)
        Me.btnGDS_MoveAll_Down.TabIndex = 71
        Me.btnGDS_MoveAll_Down.Text = "DOWN"
        '
        'btnGDS_MoveAll_Up
        '
        Me.btnGDS_MoveAll_Up.Location = New System.Drawing.Point(34, 23)
        Me.btnGDS_MoveAll_Up.Name = "btnGDS_MoveAll_Up"
        Me.btnGDS_MoveAll_Up.Size = New System.Drawing.Size(54, 23)
        Me.btnGDS_MoveAll_Up.TabIndex = 68
        Me.btnGDS_MoveAll_Up.Text = "UP"
        '
        'btnGDS_MoveAll_Right
        '
        Me.btnGDS_MoveAll_Right.Location = New System.Drawing.Point(62, 48)
        Me.btnGDS_MoveAll_Right.Name = "btnGDS_MoveAll_Right"
        Me.btnGDS_MoveAll_Right.Size = New System.Drawing.Size(54, 23)
        Me.btnGDS_MoveAll_Right.TabIndex = 70
        Me.btnGDS_MoveAll_Right.Text = "RIGHT"
        '
        'btnGDS_MoveAll_Left
        '
        Me.btnGDS_MoveAll_Left.Location = New System.Drawing.Point(5, 48)
        Me.btnGDS_MoveAll_Left.Name = "btnGDS_MoveAll_Left"
        Me.btnGDS_MoveAll_Left.Size = New System.Drawing.Size(54, 23)
        Me.btnGDS_MoveAll_Left.TabIndex = 69
        Me.btnGDS_MoveAll_Left.Text = "LEFT"
        '
        'Label36
        '
        Me.Label36.Location = New System.Drawing.Point(5, 159)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(44, 16)
        Me.Label36.TabIndex = 56
        Me.Label36.Text = "Color"
        Me.Label36.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'ceGuideColor
        '
        Me.ceGuideColor.EditValue = System.Drawing.Color.DarkBlue
        Me.ceGuideColor.Location = New System.Drawing.Point(58, 158)
        Me.ceGuideColor.Name = "ceGuideColor"
        Me.ceGuideColor.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ceGuideColor.Size = New System.Drawing.Size(169, 20)
        Me.ceGuideColor.TabIndex = 55
        '
        'Label28
        '
        Me.Label28.Location = New System.Drawing.Point(5, 127)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(44, 16)
        Me.Label28.TabIndex = 54
        Me.Label28.Text = "Right"
        Me.Label28.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label27
        '
        Me.Label27.Location = New System.Drawing.Point(5, 99)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(44, 16)
        Me.Label27.TabIndex = 53
        Me.Label27.Text = "Left"
        Me.Label27.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label25
        '
        Me.Label25.Location = New System.Drawing.Point(5, 73)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(44, 16)
        Me.Label25.TabIndex = 52
        Me.Label25.Text = "Bottom"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label26
        '
        Me.Label26.Location = New System.Drawing.Point(5, 45)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(44, 16)
        Me.Label26.TabIndex = 51
        Me.Label26.Text = "Top"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cbEnableFlexGuides
        '
        Me.cbEnableFlexGuides.Location = New System.Drawing.Point(55, 23)
        Me.cbEnableFlexGuides.Name = "cbEnableFlexGuides"
        Me.cbEnableFlexGuides.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbEnableFlexGuides.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.cbEnableFlexGuides.Properties.Appearance.Options.UseFont = True
        Me.cbEnableFlexGuides.Properties.Appearance.Options.UseForeColor = True
        Me.cbEnableFlexGuides.Properties.Caption = "Enable Flex Guides"
        Me.cbEnableFlexGuides.Size = New System.Drawing.Size(138, 19)
        Me.cbEnableFlexGuides.TabIndex = 30
        '
        'txtGuide_R
        '
        Me.txtGuide_R.Location = New System.Drawing.Point(309, 123)
        Me.txtGuide_R.Name = "txtGuide_R"
        Me.txtGuide_R.Size = New System.Drawing.Size(32, 21)
        Me.txtGuide_R.TabIndex = 38
        Me.txtGuide_R.Text = "0"
        Me.txtGuide_R.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'tbGuide_T
        '
        Me.tbGuide_T.AutoSize = False
        Me.tbGuide_T.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.tbGuide_T.LargeChange = 10
        Me.tbGuide_T.Location = New System.Drawing.Point(55, 48)
        Me.tbGuide_T.Maximum = 576
        Me.tbGuide_T.Name = "tbGuide_T"
        Me.tbGuide_T.Size = New System.Drawing.Size(248, 16)
        Me.tbGuide_T.TabIndex = 31
        Me.tbGuide_T.TickFrequency = 30
        '
        'txtGuide_L
        '
        Me.txtGuide_L.Location = New System.Drawing.Point(309, 95)
        Me.txtGuide_L.Name = "txtGuide_L"
        Me.txtGuide_L.Size = New System.Drawing.Size(32, 21)
        Me.txtGuide_L.TabIndex = 37
        Me.txtGuide_L.Text = "0"
        Me.txtGuide_L.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'tbGuide_R
        '
        Me.tbGuide_R.AutoSize = False
        Me.tbGuide_R.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.tbGuide_R.LargeChange = 10
        Me.tbGuide_R.Location = New System.Drawing.Point(55, 129)
        Me.tbGuide_R.Maximum = 720
        Me.tbGuide_R.Name = "tbGuide_R"
        Me.tbGuide_R.Size = New System.Drawing.Size(248, 16)
        Me.tbGuide_R.TabIndex = 34
        Me.tbGuide_R.TickFrequency = 30
        '
        'txtGuide_B
        '
        Me.txtGuide_B.Location = New System.Drawing.Point(309, 69)
        Me.txtGuide_B.Name = "txtGuide_B"
        Me.txtGuide_B.Size = New System.Drawing.Size(32, 21)
        Me.txtGuide_B.TabIndex = 36
        Me.txtGuide_B.Text = "0"
        Me.txtGuide_B.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'tbGuide_B
        '
        Me.tbGuide_B.AutoSize = False
        Me.tbGuide_B.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.tbGuide_B.LargeChange = 10
        Me.tbGuide_B.Location = New System.Drawing.Point(55, 76)
        Me.tbGuide_B.Maximum = 576
        Me.tbGuide_B.Name = "tbGuide_B"
        Me.tbGuide_B.Size = New System.Drawing.Size(248, 16)
        Me.tbGuide_B.TabIndex = 32
        Me.tbGuide_B.TickFrequency = 30
        '
        'txtGuide_T
        '
        Me.txtGuide_T.Location = New System.Drawing.Point(309, 41)
        Me.txtGuide_T.Name = "txtGuide_T"
        Me.txtGuide_T.Size = New System.Drawing.Size(32, 21)
        Me.txtGuide_T.TabIndex = 35
        Me.txtGuide_T.Text = "0"
        Me.txtGuide_T.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'tbGuide_L
        '
        Me.tbGuide_L.AutoSize = False
        Me.tbGuide_L.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.tbGuide_L.LargeChange = 10
        Me.tbGuide_L.Location = New System.Drawing.Point(55, 102)
        Me.tbGuide_L.Maximum = 720
        Me.tbGuide_L.Name = "tbGuide_L"
        Me.tbGuide_L.Size = New System.Drawing.Size(248, 16)
        Me.tbGuide_L.TabIndex = 33
        Me.tbGuide_L.TickFrequency = 30
        '
        'tpUOPTemplates
        '
        Me.tpUOPTemplates.Controls.Add(Me.GroupControl6)
        Me.tpUOPTemplates.Name = "tpUOPTemplates"
        Me.tpUOPTemplates.Size = New System.Drawing.Size(623, 417)
        Me.tpUOPTemplates.Text = "UOP Templates"
        '
        'GroupControl6
        '
        Me.GroupControl6.Controls.Add(Me.lvUOPT_ExistingTemplates)
        Me.GroupControl6.Controls.Add(Me.btnUOPT_SaveSelectedTemplate)
        Me.GroupControl6.Controls.Add(Me.btnUOPT_SaveNewTemplate)
        Me.GroupControl6.Controls.Add(Me.cbUO_24)
        Me.GroupControl6.Controls.Add(Me.btnUOPT_RenameSelectedTemplate)
        Me.GroupControl6.Controls.Add(Me.cbUO_23)
        Me.GroupControl6.Controls.Add(Me.cbUOPT_EnableSelectedTemplate)
        Me.GroupControl6.Controls.Add(Me.cbUO_22)
        Me.GroupControl6.Controls.Add(Me.btnUOPT_DeleteSelectedTemplate)
        Me.GroupControl6.Controls.Add(Me.cbUO_21)
        Me.GroupControl6.Controls.Add(Me.Label187)
        Me.GroupControl6.Controls.Add(Me.cbUO_20)
        Me.GroupControl6.Controls.Add(Me.Label192)
        Me.GroupControl6.Controls.Add(Me.cbUO_19)
        Me.GroupControl6.Controls.Add(Me.Label191)
        Me.GroupControl6.Controls.Add(Me.cbUO_18)
        Me.GroupControl6.Controls.Add(Me.Label190)
        Me.GroupControl6.Controls.Add(Me.cbUO_17)
        Me.GroupControl6.Controls.Add(Me.Label189)
        Me.GroupControl6.Controls.Add(Me.cbUO_16)
        Me.GroupControl6.Controls.Add(Me.Label188)
        Me.GroupControl6.Controls.Add(Me.cbUO_15)
        Me.GroupControl6.Controls.Add(Me.Label186)
        Me.GroupControl6.Controls.Add(Me.cbUO_14)
        Me.GroupControl6.Controls.Add(Me.Label185)
        Me.GroupControl6.Controls.Add(Me.cbUO_13)
        Me.GroupControl6.Controls.Add(Me.Label184)
        Me.GroupControl6.Controls.Add(Me.cbUO_12)
        Me.GroupControl6.Controls.Add(Me.Label183)
        Me.GroupControl6.Controls.Add(Me.cbUO_11)
        Me.GroupControl6.Controls.Add(Me.Label182)
        Me.GroupControl6.Controls.Add(Me.cbUO_10)
        Me.GroupControl6.Controls.Add(Me.Label181)
        Me.GroupControl6.Controls.Add(Me.cbUO_9)
        Me.GroupControl6.Controls.Add(Me.Label180)
        Me.GroupControl6.Controls.Add(Me.cbUO_8)
        Me.GroupControl6.Controls.Add(Me.Label179)
        Me.GroupControl6.Controls.Add(Me.cbUO_7)
        Me.GroupControl6.Controls.Add(Me.Label178)
        Me.GroupControl6.Controls.Add(Me.cbUO_6)
        Me.GroupControl6.Controls.Add(Me.Label177)
        Me.GroupControl6.Controls.Add(Me.cbUO_5)
        Me.GroupControl6.Controls.Add(Me.Label176)
        Me.GroupControl6.Controls.Add(Me.cbUO_4)
        Me.GroupControl6.Controls.Add(Me.Label175)
        Me.GroupControl6.Controls.Add(Me.cbUO_3)
        Me.GroupControl6.Controls.Add(Me.Label174)
        Me.GroupControl6.Controls.Add(Me.cbUO_2)
        Me.GroupControl6.Controls.Add(Me.Label173)
        Me.GroupControl6.Controls.Add(Me.cbUO_1)
        Me.GroupControl6.Controls.Add(Me.Label42)
        Me.GroupControl6.Controls.Add(Me.cbUO_0)
        Me.GroupControl6.Controls.Add(Me.Label41)
        Me.GroupControl6.Controls.Add(Me.Label38)
        Me.GroupControl6.Controls.Add(Me.Label40)
        Me.GroupControl6.Controls.Add(Me.Label39)
        Me.GroupControl6.Location = New System.Drawing.Point(7, 8)
        Me.GroupControl6.Name = "GroupControl6"
        Me.GroupControl6.Size = New System.Drawing.Size(491, 389)
        Me.GroupControl6.TabIndex = 5
        Me.GroupControl6.Text = "Existing UOP Templates"
        '
        'lvUOPT_ExistingTemplates
        '
        Me.lvUOPT_ExistingTemplates.FormattingEnabled = True
        Me.lvUOPT_ExistingTemplates.Location = New System.Drawing.Point(5, 25)
        Me.lvUOPT_ExistingTemplates.Name = "lvUOPT_ExistingTemplates"
        Me.lvUOPT_ExistingTemplates.Size = New System.Drawing.Size(187, 277)
        Me.lvUOPT_ExistingTemplates.TabIndex = 6
        '
        'btnUOPT_SaveSelectedTemplate
        '
        Me.btnUOPT_SaveSelectedTemplate.Location = New System.Drawing.Point(200, 358)
        Me.btnUOPT_SaveSelectedTemplate.Name = "btnUOPT_SaveSelectedTemplate"
        Me.btnUOPT_SaveSelectedTemplate.Size = New System.Drawing.Size(187, 23)
        Me.btnUOPT_SaveSelectedTemplate.TabIndex = 177
        Me.btnUOPT_SaveSelectedTemplate.Text = "Save Selected Template"
        '
        'btnUOPT_SaveNewTemplate
        '
        Me.btnUOPT_SaveNewTemplate.Location = New System.Drawing.Point(200, 329)
        Me.btnUOPT_SaveNewTemplate.Name = "btnUOPT_SaveNewTemplate"
        Me.btnUOPT_SaveNewTemplate.Size = New System.Drawing.Size(187, 23)
        Me.btnUOPT_SaveNewTemplate.TabIndex = 176
        Me.btnUOPT_SaveNewTemplate.Text = "Save New Template"
        '
        'cbUO_24
        '
        Me.cbUO_24.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbUO_24.Location = New System.Drawing.Point(344, 199)
        Me.cbUO_24.Name = "cbUO_24"
        Me.cbUO_24.Size = New System.Drawing.Size(16, 14)
        Me.cbUO_24.TabIndex = 175
        '
        'btnUOPT_RenameSelectedTemplate
        '
        Me.btnUOPT_RenameSelectedTemplate.Location = New System.Drawing.Point(5, 358)
        Me.btnUOPT_RenameSelectedTemplate.Name = "btnUOPT_RenameSelectedTemplate"
        Me.btnUOPT_RenameSelectedTemplate.Size = New System.Drawing.Size(187, 23)
        Me.btnUOPT_RenameSelectedTemplate.TabIndex = 4
        Me.btnUOPT_RenameSelectedTemplate.Text = "Rename Selected Template"
        '
        'cbUO_23
        '
        Me.cbUO_23.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbUO_23.Location = New System.Drawing.Point(344, 183)
        Me.cbUO_23.Name = "cbUO_23"
        Me.cbUO_23.Size = New System.Drawing.Size(16, 14)
        Me.cbUO_23.TabIndex = 174
        '
        'cbUOPT_EnableSelectedTemplate
        '
        Me.cbUOPT_EnableSelectedTemplate.Enabled = False
        Me.cbUOPT_EnableSelectedTemplate.Location = New System.Drawing.Point(10, 305)
        Me.cbUOPT_EnableSelectedTemplate.Name = "cbUOPT_EnableSelectedTemplate"
        Me.cbUOPT_EnableSelectedTemplate.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.cbUOPT_EnableSelectedTemplate.Properties.Appearance.Options.UseFont = True
        Me.cbUOPT_EnableSelectedTemplate.Properties.Caption = "Enable Selected Template"
        Me.cbUOPT_EnableSelectedTemplate.Size = New System.Drawing.Size(187, 19)
        Me.cbUOPT_EnableSelectedTemplate.TabIndex = 2
        '
        'cbUO_22
        '
        Me.cbUO_22.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbUO_22.Location = New System.Drawing.Point(344, 168)
        Me.cbUO_22.Name = "cbUO_22"
        Me.cbUO_22.Size = New System.Drawing.Size(16, 14)
        Me.cbUO_22.TabIndex = 173
        '
        'btnUOPT_DeleteSelectedTemplate
        '
        Me.btnUOPT_DeleteSelectedTemplate.Location = New System.Drawing.Point(5, 329)
        Me.btnUOPT_DeleteSelectedTemplate.Name = "btnUOPT_DeleteSelectedTemplate"
        Me.btnUOPT_DeleteSelectedTemplate.Size = New System.Drawing.Size(187, 23)
        Me.btnUOPT_DeleteSelectedTemplate.TabIndex = 3
        Me.btnUOPT_DeleteSelectedTemplate.Text = "Delete Selected Template"
        '
        'cbUO_21
        '
        Me.cbUO_21.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbUO_21.Location = New System.Drawing.Point(344, 152)
        Me.cbUO_21.Name = "cbUO_21"
        Me.cbUO_21.Size = New System.Drawing.Size(16, 14)
        Me.cbUO_21.TabIndex = 172
        '
        'Label187
        '
        Me.Label187.Location = New System.Drawing.Point(216, 23)
        Me.Label187.Name = "Label187"
        Me.Label187.Size = New System.Drawing.Size(112, 16)
        Me.Label187.TabIndex = 126
        Me.Label187.Text = "Title/Time Play"
        Me.Label187.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'cbUO_20
        '
        Me.cbUO_20.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbUO_20.Location = New System.Drawing.Point(344, 136)
        Me.cbUO_20.Name = "cbUO_20"
        Me.cbUO_20.Size = New System.Drawing.Size(16, 14)
        Me.cbUO_20.TabIndex = 171
        '
        'Label192
        '
        Me.Label192.Location = New System.Drawing.Point(360, 86)
        Me.Label192.Name = "Label192"
        Me.Label192.Size = New System.Drawing.Size(120, 16)
        Me.Label192.TabIndex = 137
        Me.Label192.Text = "Button Select/Activate"
        Me.Label192.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'cbUO_19
        '
        Me.cbUO_19.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbUO_19.Location = New System.Drawing.Point(344, 120)
        Me.cbUO_19.Name = "cbUO_19"
        Me.cbUO_19.Size = New System.Drawing.Size(16, 14)
        Me.cbUO_19.TabIndex = 170
        '
        'Label191
        '
        Me.Label191.Location = New System.Drawing.Point(360, 102)
        Me.Label191.Name = "Label191"
        Me.Label191.Size = New System.Drawing.Size(112, 16)
        Me.Label191.TabIndex = 138
        Me.Label191.Text = "Start From Still"
        Me.Label191.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'cbUO_18
        '
        Me.cbUO_18.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbUO_18.Location = New System.Drawing.Point(344, 104)
        Me.cbUO_18.Name = "cbUO_18"
        Me.cbUO_18.Size = New System.Drawing.Size(16, 14)
        Me.cbUO_18.TabIndex = 169
        '
        'Label190
        '
        Me.Label190.Location = New System.Drawing.Point(360, 118)
        Me.Label190.Name = "Label190"
        Me.Label190.Size = New System.Drawing.Size(132, 16)
        Me.Label190.TabIndex = 139
        Me.Label190.Text = "Pause/Menu Lang Select"
        Me.Label190.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'cbUO_17
        '
        Me.cbUO_17.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbUO_17.Location = New System.Drawing.Point(344, 88)
        Me.cbUO_17.Name = "cbUO_17"
        Me.cbUO_17.Size = New System.Drawing.Size(16, 14)
        Me.cbUO_17.TabIndex = 168
        '
        'Label189
        '
        Me.Label189.Location = New System.Drawing.Point(360, 165)
        Me.Label189.Name = "Label189"
        Me.Label189.Size = New System.Drawing.Size(128, 16)
        Me.Label189.TabIndex = 140
        Me.Label189.Text = "Angle Change/PM Level"
        Me.Label189.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'cbUO_16
        '
        Me.cbUO_16.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbUO_16.Location = New System.Drawing.Point(344, 73)
        Me.cbUO_16.Name = "cbUO_16"
        Me.cbUO_16.Size = New System.Drawing.Size(16, 14)
        Me.cbUO_16.TabIndex = 167
        '
        'Label188
        '
        Me.Label188.Location = New System.Drawing.Point(216, 183)
        Me.Label188.Name = "Label188"
        Me.Label188.Size = New System.Drawing.Size(80, 16)
        Me.Label188.TabIndex = 141
        Me.Label188.Text = "Title Menu"
        Me.Label188.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'cbUO_15
        '
        Me.cbUO_15.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbUO_15.Location = New System.Drawing.Point(344, 57)
        Me.cbUO_15.Name = "cbUO_15"
        Me.cbUO_15.Size = New System.Drawing.Size(16, 14)
        Me.cbUO_15.TabIndex = 166
        '
        'Label186
        '
        Me.Label186.Location = New System.Drawing.Point(216, 200)
        Me.Label186.Name = "Label186"
        Me.Label186.Size = New System.Drawing.Size(80, 16)
        Me.Label186.TabIndex = 142
        Me.Label186.Text = "Root Menu"
        Me.Label186.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'cbUO_14
        '
        Me.cbUO_14.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbUO_14.Location = New System.Drawing.Point(344, 41)
        Me.cbUO_14.Name = "cbUO_14"
        Me.cbUO_14.Size = New System.Drawing.Size(16, 14)
        Me.cbUO_14.TabIndex = 165
        '
        'Label185
        '
        Me.Label185.Location = New System.Drawing.Point(216, 216)
        Me.Label185.Name = "Label185"
        Me.Label185.Size = New System.Drawing.Size(80, 16)
        Me.Label185.TabIndex = 143
        Me.Label185.Text = "Subtitle Menu"
        Me.Label185.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'cbUO_13
        '
        Me.cbUO_13.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbUO_13.Location = New System.Drawing.Point(344, 25)
        Me.cbUO_13.Name = "cbUO_13"
        Me.cbUO_13.Size = New System.Drawing.Size(16, 14)
        Me.cbUO_13.TabIndex = 164
        '
        'Label184
        '
        Me.Label184.Location = New System.Drawing.Point(360, 23)
        Me.Label184.Name = "Label184"
        Me.Label184.Size = New System.Drawing.Size(80, 16)
        Me.Label184.TabIndex = 144
        Me.Label184.Text = "Audio Menu"
        Me.Label184.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'cbUO_12
        '
        Me.cbUO_12.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbUO_12.Location = New System.Drawing.Point(200, 218)
        Me.cbUO_12.Name = "cbUO_12"
        Me.cbUO_12.Size = New System.Drawing.Size(16, 14)
        Me.cbUO_12.TabIndex = 163
        '
        'Label183
        '
        Me.Label183.Location = New System.Drawing.Point(216, 56)
        Me.Label183.Name = "Label183"
        Me.Label183.Size = New System.Drawing.Size(112, 16)
        Me.Label183.TabIndex = 128
        Me.Label183.Text = "Title Play"
        Me.Label183.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'cbUO_11
        '
        Me.cbUO_11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbUO_11.Location = New System.Drawing.Point(200, 201)
        Me.cbUO_11.Name = "cbUO_11"
        Me.cbUO_11.Size = New System.Drawing.Size(16, 14)
        Me.cbUO_11.TabIndex = 162
        '
        'Label182
        '
        Me.Label182.Location = New System.Drawing.Point(360, 39)
        Me.Label182.Name = "Label182"
        Me.Label182.Size = New System.Drawing.Size(80, 16)
        Me.Label182.TabIndex = 145
        Me.Label182.Text = "Angle Menu"
        Me.Label182.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'cbUO_10
        '
        Me.cbUO_10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbUO_10.Location = New System.Drawing.Point(200, 185)
        Me.cbUO_10.Name = "cbUO_10"
        Me.cbUO_10.Size = New System.Drawing.Size(16, 14)
        Me.cbUO_10.TabIndex = 161
        '
        'Label181
        '
        Me.Label181.Location = New System.Drawing.Point(360, 56)
        Me.Label181.Name = "Label181"
        Me.Label181.Size = New System.Drawing.Size(80, 16)
        Me.Label181.TabIndex = 146
        Me.Label181.Text = "Chapter Menu"
        Me.Label181.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'cbUO_9
        '
        Me.cbUO_9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbUO_9.Location = New System.Drawing.Point(200, 169)
        Me.cbUO_9.Name = "cbUO_9"
        Me.cbUO_9.Size = New System.Drawing.Size(16, 14)
        Me.cbUO_9.TabIndex = 160
        '
        'Label180
        '
        Me.Label180.Location = New System.Drawing.Point(360, 134)
        Me.Label180.Name = "Label180"
        Me.Label180.Size = New System.Drawing.Size(128, 16)
        Me.Label180.TabIndex = 147
        Me.Label180.Text = "Change Audio Stream"
        Me.Label180.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'cbUO_8
        '
        Me.cbUO_8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbUO_8.Location = New System.Drawing.Point(200, 153)
        Me.cbUO_8.Name = "cbUO_8"
        Me.cbUO_8.Size = New System.Drawing.Size(16, 14)
        Me.cbUO_8.TabIndex = 159
        '
        'Label179
        '
        Me.Label179.Location = New System.Drawing.Point(216, 70)
        Me.Label179.Name = "Label179"
        Me.Label179.Size = New System.Drawing.Size(112, 16)
        Me.Label179.TabIndex = 129
        Me.Label179.Text = "Stop"
        Me.Label179.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'cbUO_7
        '
        Me.cbUO_7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbUO_7.Location = New System.Drawing.Point(200, 137)
        Me.cbUO_7.Name = "cbUO_7"
        Me.cbUO_7.Size = New System.Drawing.Size(16, 14)
        Me.cbUO_7.TabIndex = 158
        '
        'Label178
        '
        Me.Label178.Location = New System.Drawing.Point(216, 87)
        Me.Label178.Name = "Label178"
        Me.Label178.Size = New System.Drawing.Size(112, 16)
        Me.Label178.TabIndex = 130
        Me.Label178.Text = "GoUp"
        Me.Label178.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'cbUO_6
        '
        Me.cbUO_6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbUO_6.Location = New System.Drawing.Point(200, 121)
        Me.cbUO_6.Name = "cbUO_6"
        Me.cbUO_6.Size = New System.Drawing.Size(16, 14)
        Me.cbUO_6.TabIndex = 157
        '
        'Label177
        '
        Me.Label177.Location = New System.Drawing.Point(360, 149)
        Me.Label177.Name = "Label177"
        Me.Label177.Size = New System.Drawing.Size(128, 16)
        Me.Label177.TabIndex = 148
        Me.Label177.Text = "Change Subtitle Stream"
        Me.Label177.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'cbUO_5
        '
        Me.cbUO_5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbUO_5.Location = New System.Drawing.Point(200, 105)
        Me.cbUO_5.Name = "cbUO_5"
        Me.cbUO_5.Size = New System.Drawing.Size(16, 14)
        Me.cbUO_5.TabIndex = 156
        '
        'Label176
        '
        Me.Label176.Location = New System.Drawing.Point(216, 103)
        Me.Label176.Name = "Label176"
        Me.Label176.Size = New System.Drawing.Size(112, 16)
        Me.Label176.TabIndex = 131
        Me.Label176.Text = "Time/Ch Search"
        Me.Label176.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'cbUO_4
        '
        Me.cbUO_4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbUO_4.Location = New System.Drawing.Point(200, 89)
        Me.cbUO_4.Name = "cbUO_4"
        Me.cbUO_4.Size = New System.Drawing.Size(16, 14)
        Me.cbUO_4.TabIndex = 155
        '
        'Label175
        '
        Me.Label175.Location = New System.Drawing.Point(360, 181)
        Me.Label175.Name = "Label175"
        Me.Label175.Size = New System.Drawing.Size(128, 16)
        Me.Label175.TabIndex = 149
        Me.Label175.Text = "Karaoke Mode Change"
        Me.Label175.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'cbUO_3
        '
        Me.cbUO_3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbUO_3.Location = New System.Drawing.Point(200, 73)
        Me.cbUO_3.Name = "cbUO_3"
        Me.cbUO_3.Size = New System.Drawing.Size(16, 14)
        Me.cbUO_3.TabIndex = 154
        '
        'Label174
        '
        Me.Label174.Location = New System.Drawing.Point(360, 198)
        Me.Label174.Name = "Label174"
        Me.Label174.Size = New System.Drawing.Size(120, 16)
        Me.Label174.TabIndex = 150
        Me.Label174.Text = "Video Mode Change"
        Me.Label174.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'cbUO_2
        '
        Me.cbUO_2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbUO_2.Location = New System.Drawing.Point(200, 57)
        Me.cbUO_2.Name = "cbUO_2"
        Me.cbUO_2.Size = New System.Drawing.Size(16, 14)
        Me.cbUO_2.TabIndex = 153
        '
        'Label173
        '
        Me.Label173.Location = New System.Drawing.Point(216, 119)
        Me.Label173.Name = "Label173"
        Me.Label173.Size = New System.Drawing.Size(120, 16)
        Me.Label173.TabIndex = 132
        Me.Label173.Text = "Chapter Back"
        Me.Label173.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'cbUO_1
        '
        Me.cbUO_1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbUO_1.Location = New System.Drawing.Point(200, 41)
        Me.cbUO_1.Name = "cbUO_1"
        Me.cbUO_1.Size = New System.Drawing.Size(16, 14)
        Me.cbUO_1.TabIndex = 152
        '
        'Label42
        '
        Me.Label42.Location = New System.Drawing.Point(216, 135)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(112, 16)
        Me.Label42.TabIndex = 133
        Me.Label42.Text = "Chapter Next"
        Me.Label42.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'cbUO_0
        '
        Me.cbUO_0.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbUO_0.Location = New System.Drawing.Point(200, 25)
        Me.cbUO_0.Name = "cbUO_0"
        Me.cbUO_0.Size = New System.Drawing.Size(16, 14)
        Me.cbUO_0.TabIndex = 151
        '
        'Label41
        '
        Me.Label41.Location = New System.Drawing.Point(216, 39)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(112, 16)
        Me.Label41.TabIndex = 127
        Me.Label41.Text = "Chapter Play"
        Me.Label41.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label38
        '
        Me.Label38.Location = New System.Drawing.Point(216, 150)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(112, 16)
        Me.Label38.TabIndex = 134
        Me.Label38.Text = "Fast Forward"
        Me.Label38.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label40
        '
        Me.Label40.Location = New System.Drawing.Point(216, 165)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(112, 16)
        Me.Label40.TabIndex = 135
        Me.Label40.Text = "Rewind"
        Me.Label40.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label39
        '
        Me.Label39.Location = New System.Drawing.Point(360, 70)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(112, 16)
        Me.Label39.TabIndex = 136
        Me.Label39.Text = "Resume"
        Me.Label39.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'tpKeyMapping
        '
        Me.tpKeyMapping.Controls.Add(Me.lbKM_Keys)
        Me.tpKeyMapping.Controls.Add(Me.cbKM_ControlKey)
        Me.tpKeyMapping.Controls.Add(Me.LinkLabel1)
        Me.tpKeyMapping.Controls.Add(Me.Button1)
        Me.tpKeyMapping.Controls.Add(Me.llKM_ReturnToDefaults)
        Me.tpKeyMapping.Controls.Add(Me.llKM_PrintKeyMapping)
        Me.tpKeyMapping.Controls.Add(Me.txtCrntFunct)
        Me.tpKeyMapping.Controls.Add(Me.llKM_SetKeyMap)
        Me.tpKeyMapping.Controls.Add(Me.txtValue)
        Me.tpKeyMapping.Controls.Add(Me.dgKM_KeyMapping)
        Me.tpKeyMapping.Name = "tpKeyMapping"
        Me.tpKeyMapping.Size = New System.Drawing.Size(623, 417)
        Me.tpKeyMapping.Text = "Key Mapping"
        '
        'lbKM_Keys
        '
        Me.lbKM_Keys.Location = New System.Drawing.Point(357, 31)
        Me.lbKM_Keys.Name = "lbKM_Keys"
        Me.lbKM_Keys.Size = New System.Drawing.Size(158, 368)
        Me.lbKM_Keys.TabIndex = 44
        '
        'cbKM_ControlKey
        '
        Me.cbKM_ControlKey.Location = New System.Drawing.Point(355, 400)
        Me.cbKM_ControlKey.Name = "cbKM_ControlKey"
        Me.cbKM_ControlKey.Properties.Caption = "Control Key"
        Me.cbKM_ControlKey.Size = New System.Drawing.Size(95, 18)
        Me.cbKM_ControlKey.TabIndex = 56
        '
        'LinkLabel1
        '
        Me.LinkLabel1.Location = New System.Drawing.Point(213, 9)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(64, 13)
        Me.LinkLabel1.TabIndex = 54
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "Save "
        Me.LinkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(671, 31)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(48, 20)
        Me.Button1.TabIndex = 53
        Me.Button1.Text = "Button1"
        Me.Button1.Visible = False
        '
        'llKM_ReturnToDefaults
        '
        Me.llKM_ReturnToDefaults.Location = New System.Drawing.Point(103, 7)
        Me.llKM_ReturnToDefaults.Name = "llKM_ReturnToDefaults"
        Me.llKM_ReturnToDefaults.Size = New System.Drawing.Size(100, 16)
        Me.llKM_ReturnToDefaults.TabIndex = 50
        Me.llKM_ReturnToDefaults.TabStop = True
        Me.llKM_ReturnToDefaults.Text = "Return To Defaults"
        Me.llKM_ReturnToDefaults.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'llKM_PrintKeyMapping
        '
        Me.llKM_PrintKeyMapping.Location = New System.Drawing.Point(5, 6)
        Me.llKM_PrintKeyMapping.Name = "llKM_PrintKeyMapping"
        Me.llKM_PrintKeyMapping.Size = New System.Drawing.Size(100, 16)
        Me.llKM_PrintKeyMapping.TabIndex = 49
        Me.llKM_PrintKeyMapping.TabStop = True
        Me.llKM_PrintKeyMapping.Text = "Print Key Mapping"
        Me.llKM_PrintKeyMapping.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtCrntFunct
        '
        Me.txtCrntFunct.Location = New System.Drawing.Point(671, 83)
        Me.txtCrntFunct.Name = "txtCrntFunct"
        Me.txtCrntFunct.Size = New System.Drawing.Size(176, 21)
        Me.txtCrntFunct.TabIndex = 48
        Me.txtCrntFunct.Text = "[HIDE THIS]"
        Me.txtCrntFunct.Visible = False
        '
        'llKM_SetKeyMap
        '
        Me.llKM_SetKeyMap.Location = New System.Drawing.Point(307, 54)
        Me.llKM_SetKeyMap.Name = "llKM_SetKeyMap"
        Me.llKM_SetKeyMap.Size = New System.Drawing.Size(44, 16)
        Me.llKM_SetKeyMap.TabIndex = 47
        Me.llKM_SetKeyMap.TabStop = True
        Me.llKM_SetKeyMap.Text = "<< SET"
        '
        'txtValue
        '
        Me.txtValue.Location = New System.Drawing.Point(671, 57)
        Me.txtValue.Name = "txtValue"
        Me.txtValue.Size = New System.Drawing.Size(176, 21)
        Me.txtValue.TabIndex = 45
        Me.txtValue.Text = "[HIDE THIS]"
        Me.txtValue.Visible = False
        '
        'dgKM_KeyMapping
        '
        Me.dgKM_KeyMapping.DataMember = ""
        Me.dgKM_KeyMapping.FlatMode = True
        Me.dgKM_KeyMapping.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.dgKM_KeyMapping.Location = New System.Drawing.Point(4, 31)
        Me.dgKM_KeyMapping.Name = "dgKM_KeyMapping"
        Me.dgKM_KeyMapping.RowHeadersVisible = False
        Me.dgKM_KeyMapping.Size = New System.Drawing.Size(301, 385)
        Me.dgKM_KeyMapping.TabIndex = 46
        Me.dgKM_KeyMapping.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.gtsOne})
        '
        'gtsOne
        '
        Me.gtsOne.AllowSorting = False
        Me.gtsOne.DataGrid = Me.dgKM_KeyMapping
        Me.gtsOne.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DataGridTextBoxColumn1, Me.DataGridTextBoxColumn2})
        Me.gtsOne.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.gtsOne.MappingName = "KeyMapping"
        Me.gtsOne.PreferredColumnWidth = 125
        Me.gtsOne.ReadOnly = True
        Me.gtsOne.RowHeaderWidth = 0
        '
        'DataGridTextBoxColumn1
        '
        Me.DataGridTextBoxColumn1.Alignment = System.Windows.Forms.HorizontalAlignment.Right
        Me.DataGridTextBoxColumn1.Format = ""
        Me.DataGridTextBoxColumn1.FormatInfo = Nothing
        Me.DataGridTextBoxColumn1.HeaderText = "Function Name  ."
        Me.DataGridTextBoxColumn1.MappingName = "FunctionName"
        Me.DataGridTextBoxColumn1.ReadOnly = True
        Me.DataGridTextBoxColumn1.Width = 125
        '
        'DataGridTextBoxColumn2
        '
        Me.DataGridTextBoxColumn2.Format = ""
        Me.DataGridTextBoxColumn2.FormatInfo = Nothing
        Me.DataGridTextBoxColumn2.HeaderText = "Current Mapping"
        Me.DataGridTextBoxColumn2.MappingName = "CurrentMapping"
        Me.DataGridTextBoxColumn2.ReadOnly = True
        Me.DataGridTextBoxColumn2.Width = 125
        '
        'tpUIOptions
        '
        Me.tpUIOptions.Controls.Add(Me.GroupControl14)
        Me.tpUIOptions.Controls.Add(Me.GroupControl13)
        Me.tpUIOptions.Name = "tpUIOptions"
        Me.tpUIOptions.Size = New System.Drawing.Size(623, 417)
        Me.tpUIOptions.Text = "UI Options"
        '
        'GroupControl14
        '
        Me.GroupControl14.Controls.Add(Me.ceUseCheckboxesForUOPs)
        Me.GroupControl14.Location = New System.Drawing.Point(9, 78)
        Me.GroupControl14.Name = "GroupControl14"
        Me.GroupControl14.Size = New System.Drawing.Size(246, 48)
        Me.GroupControl14.TabIndex = 4
        Me.GroupControl14.Text = "UOPs"
        '
        'ceUseCheckboxesForUOPs
        '
        Me.ceUseCheckboxesForUOPs.Location = New System.Drawing.Point(5, 23)
        Me.ceUseCheckboxesForUOPs.Name = "ceUseCheckboxesForUOPs"
        Me.ceUseCheckboxesForUOPs.Properties.Caption = "Use checkboxes for UOP display"
        Me.ceUseCheckboxesForUOPs.Size = New System.Drawing.Size(180, 18)
        Me.ceUseCheckboxesForUOPs.TabIndex = 0
        '
        'GroupControl13
        '
        Me.GroupControl13.Controls.Add(Me.rbRootResume_SeparateButtons)
        Me.GroupControl13.Controls.Add(Me.rbRootResume_RootIsResume)
        Me.GroupControl13.Location = New System.Drawing.Point(9, 3)
        Me.GroupControl13.Name = "GroupControl13"
        Me.GroupControl13.Size = New System.Drawing.Size(246, 69)
        Me.GroupControl13.TabIndex = 3
        Me.GroupControl13.Text = "Root / Resume Buttons"
        '
        'rbRootResume_SeparateButtons
        '
        Me.rbRootResume_SeparateButtons.AutoSize = True
        Me.rbRootResume_SeparateButtons.Location = New System.Drawing.Point(5, 23)
        Me.rbRootResume_SeparateButtons.Name = "rbRootResume_SeparateButtons"
        Me.rbRootResume_SeparateButtons.Size = New System.Drawing.Size(198, 17)
        Me.rbRootResume_SeparateButtons.TabIndex = 0
        Me.rbRootResume_SeparateButtons.Text = "Separate Root And Resume Buttons"
        Me.rbRootResume_SeparateButtons.UseVisualStyleBackColor = True
        '
        'rbRootResume_RootIsResume
        '
        Me.rbRootResume_RootIsResume.AutoSize = True
        Me.rbRootResume_RootIsResume.Checked = True
        Me.rbRootResume_RootIsResume.Location = New System.Drawing.Point(5, 40)
        Me.rbRootResume_RootIsResume.Name = "rbRootResume_RootIsResume"
        Me.rbRootResume_RootIsResume.Size = New System.Drawing.Size(237, 17)
        Me.rbRootResume_RootIsResume.TabIndex = 1
        Me.rbRootResume_RootIsResume.TabStop = True
        Me.rbRootResume_RootIsResume.Text = "Root Button Acts As Resume In Menu Space"
        Me.rbRootResume_RootIsResume.UseVisualStyleBackColor = True
        '
        'tpDumping
        '
        Me.tpDumping.Controls.Add(Me.cbDumpAudio)
        Me.tpDumping.Controls.Add(Me.GroupControl1)
        Me.tpDumping.Controls.Add(Me.btnResetAllOptions)
        Me.tpDumping.Controls.Add(Me.cbARs)
        Me.tpDumping.Controls.Add(Me.CheckBox1)
        Me.tpDumping.Name = "tpDumping"
        Me.tpDumping.Size = New System.Drawing.Size(623, 417)
        Me.tpDumping.Text = "Dumping"
        '
        'cbDumpAudio
        '
        Me.cbDumpAudio.Location = New System.Drawing.Point(9, 78)
        Me.cbDumpAudio.Name = "cbDumpAudio"
        Me.cbDumpAudio.Size = New System.Drawing.Size(184, 16)
        Me.cbDumpAudio.TabIndex = 92
        Me.cbDumpAudio.Text = "Dump audio to default directory"
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.btnOpenDumpDirectory)
        Me.GroupControl1.Controls.Add(Me.btnDumpDirectoryBrowse)
        Me.GroupControl1.Controls.Add(Me.txtDumpDirectory)
        Me.GroupControl1.Location = New System.Drawing.Point(4, 7)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(543, 55)
        Me.GroupControl1.TabIndex = 91
        Me.GroupControl1.Text = "Dump Directory"
        '
        'btnOpenDumpDirectory
        '
        Me.btnOpenDumpDirectory.Location = New System.Drawing.Point(449, 23)
        Me.btnOpenDumpDirectory.Name = "btnOpenDumpDirectory"
        Me.btnOpenDumpDirectory.Size = New System.Drawing.Size(85, 23)
        Me.btnOpenDumpDirectory.TabIndex = 33
        Me.btnOpenDumpDirectory.Text = "Open Dir"
        '
        'btnDumpDirectoryBrowse
        '
        Me.btnDumpDirectoryBrowse.Location = New System.Drawing.Point(358, 23)
        Me.btnDumpDirectoryBrowse.Name = "btnDumpDirectoryBrowse"
        Me.btnDumpDirectoryBrowse.Size = New System.Drawing.Size(85, 23)
        Me.btnDumpDirectoryBrowse.TabIndex = 32
        Me.btnDumpDirectoryBrowse.Text = "Browse"
        '
        'txtDumpDirectory
        '
        Me.txtDumpDirectory.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.txtDumpDirectory.Location = New System.Drawing.Point(5, 25)
        Me.txtDumpDirectory.Name = "txtDumpDirectory"
        Me.txtDumpDirectory.ReadOnly = True
        Me.txtDumpDirectory.Size = New System.Drawing.Size(347, 21)
        Me.txtDumpDirectory.TabIndex = 30
        '
        'btnResetAllOptions
        '
        Me.btnResetAllOptions.Location = New System.Drawing.Point(525, 470)
        Me.btnResetAllOptions.Name = "btnResetAllOptions"
        Me.btnResetAllOptions.Size = New System.Drawing.Size(172, 23)
        Me.btnResetAllOptions.TabIndex = 90
        Me.btnResetAllOptions.Text = "Reset All Options To Defaults"
        Me.btnResetAllOptions.Visible = False
        '
        'cbARs
        '
        Me.cbARs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbARs.Items.AddRange(New Object() {"None", "4x3 (1.33)", "14x9 (1.55)", "16x9 (1.78)", "1.85", "2.35", "2.40"})
        Me.cbARs.Location = New System.Drawing.Point(527, 499)
        Me.cbARs.Name = "cbARs"
        Me.cbARs.Size = New System.Drawing.Size(122, 21)
        Me.cbARs.TabIndex = 89
        Me.cbARs.Visible = False
        '
        'CheckBox1
        '
        Me.CheckBox1.Enabled = False
        Me.CheckBox1.Location = New System.Drawing.Point(525, 456)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(216, 16)
        Me.CheckBox1.TabIndex = 88
        Me.CheckBox1.Text = "Warn if subpicture falls outside guides."
        Me.CheckBox1.Visible = False
        '
        'tpAdmin
        '
        Me.tpAdmin.Controls.Add(Me.btnADMIN_SaveCurrentLayout)
        Me.tpAdmin.Controls.Add(Me.TabControl1)
        Me.tpAdmin.Name = "tpAdmin"
        Me.tpAdmin.PageVisible = False
        Me.tpAdmin.Size = New System.Drawing.Size(623, 417)
        Me.tpAdmin.Text = "Admin"
        '
        'btnADMIN_SaveCurrentLayout
        '
        Me.btnADMIN_SaveCurrentLayout.Location = New System.Drawing.Point(590, 25)
        Me.btnADMIN_SaveCurrentLayout.Name = "btnADMIN_SaveCurrentLayout"
        Me.btnADMIN_SaveCurrentLayout.Size = New System.Drawing.Size(192, 23)
        Me.btnADMIN_SaveCurrentLayout.TabIndex = 8
        Me.btnADMIN_SaveCurrentLayout.Text = "Save Current Layout To XML"
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.tpGraphSetup)
        Me.TabControl1.Controls.Add(Me.tpDebugging)
        Me.TabControl1.Controls.Add(Me.tpFeatures)
        Me.TabControl1.Controls.Add(Me.tpPPs)
        Me.TabControl1.Location = New System.Drawing.Point(3, 3)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(568, 416)
        Me.TabControl1.TabIndex = 7
        '
        'tpGraphSetup
        '
        Me.tpGraphSetup.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.tpGraphSetup.Controls.Add(Me.GroupBox23)
        Me.tpGraphSetup.Location = New System.Drawing.Point(4, 22)
        Me.tpGraphSetup.Name = "tpGraphSetup"
        Me.tpGraphSetup.Size = New System.Drawing.Size(560, 390)
        Me.tpGraphSetup.TabIndex = 0
        Me.tpGraphSetup.Text = "Graph"
        '
        'GroupBox23
        '
        Me.GroupBox23.Controls.Add(Me.rbGraphProfiles_3)
        Me.GroupBox23.Controls.Add(Me.Label49)
        Me.GroupBox23.Controls.Add(Me.rbGraphProfiles_2)
        Me.GroupBox23.Controls.Add(Me.rbGraphProfiles_0)
        Me.GroupBox23.Controls.Add(Me.cbROTGraph)
        Me.GroupBox23.Location = New System.Drawing.Point(8, 4)
        Me.GroupBox23.Name = "GroupBox23"
        Me.GroupBox23.Size = New System.Drawing.Size(336, 104)
        Me.GroupBox23.TabIndex = 0
        Me.GroupBox23.TabStop = False
        Me.GroupBox23.Text = "Profiles"
        '
        'rbGraphProfiles_3
        '
        Me.rbGraphProfiles_3.Location = New System.Drawing.Point(8, 64)
        Me.rbGraphProfiles_3.Name = "rbGraphProfiles_3"
        Me.rbGraphProfiles_3.Size = New System.Drawing.Size(320, 16)
        Me.rbGraphProfiles_3.TabIndex = 4
        Me.rbGraphProfiles_3.Text = "Sonic Decoders/Default DS/VMR9"
        '
        'Label49
        '
        Me.Label49.Location = New System.Drawing.Point(3, 16)
        Me.Label49.Name = "Label49"
        Me.Label49.Size = New System.Drawing.Size(181, 16)
        Me.Label49.TabIndex = 3
        Me.Label49.Text = "All profiles use MS DVD Navigator"
        '
        'rbGraphProfiles_2
        '
        Me.rbGraphProfiles_2.Checked = True
        Me.rbGraphProfiles_2.Location = New System.Drawing.Point(8, 48)
        Me.rbGraphProfiles_2.Name = "rbGraphProfiles_2"
        Me.rbGraphProfiles_2.Size = New System.Drawing.Size(320, 16)
        Me.rbGraphProfiles_2.TabIndex = 2
        Me.rbGraphProfiles_2.TabStop = True
        Me.rbGraphProfiles_2.Text = "nVidia Decoders/Default DS/VMR9"
        '
        'rbGraphProfiles_0
        '
        Me.rbGraphProfiles_0.Location = New System.Drawing.Point(8, 32)
        Me.rbGraphProfiles_0.Name = "rbGraphProfiles_0"
        Me.rbGraphProfiles_0.Size = New System.Drawing.Size(320, 16)
        Me.rbGraphProfiles_0.TabIndex = 0
        Me.rbGraphProfiles_0.Text = "nVidia Decoders, Default DirectSound, OM & VR"
        '
        'cbROTGraph
        '
        Me.cbROTGraph.Checked = True
        Me.cbROTGraph.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbROTGraph.Location = New System.Drawing.Point(8, 80)
        Me.cbROTGraph.Name = "cbROTGraph"
        Me.cbROTGraph.Size = New System.Drawing.Size(224, 16)
        Me.cbROTGraph.TabIndex = 1
        Me.cbROTGraph.Text = "Add graph to running object table (ROT)"
        '
        'tpDebugging
        '
        Me.tpDebugging.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.tpDebugging.Controls.Add(Me.btnGetDefSubLang)
        Me.tpDebugging.Controls.Add(Me.btnGetPlayerPL)
        Me.tpDebugging.Controls.Add(Me.btnGetTitlePL)
        Me.tpDebugging.Controls.Add(Me.Button2)
        Me.tpDebugging.Controls.Add(Me.btnAspectRatio)
        Me.tpDebugging.Controls.Add(Me.btnGetProcAmp)
        Me.tpDebugging.Controls.Add(Me.btnProcAmpRange)
        Me.tpDebugging.Controls.Add(Me.btnGetVidWin)
        Me.tpDebugging.Controls.Add(Me.btnGetDefMenuLang)
        Me.tpDebugging.Controls.Add(Me.btnGetDefAudLang)
        Me.tpDebugging.Location = New System.Drawing.Point(4, 22)
        Me.tpDebugging.Name = "tpDebugging"
        Me.tpDebugging.Size = New System.Drawing.Size(560, 390)
        Me.tpDebugging.TabIndex = 1
        Me.tpDebugging.Text = "Debugging"
        Me.tpDebugging.Visible = False
        '
        'btnGetDefSubLang
        '
        Me.btnGetDefSubLang.Location = New System.Drawing.Point(4, 84)
        Me.btnGetDefSubLang.Name = "btnGetDefSubLang"
        Me.btnGetDefSubLang.Size = New System.Drawing.Size(168, 23)
        Me.btnGetDefSubLang.TabIndex = 72
        Me.btnGetDefSubLang.Text = "Get Default Subtitle Language"
        '
        'btnGetPlayerPL
        '
        Me.btnGetPlayerPL.Location = New System.Drawing.Point(4, 116)
        Me.btnGetPlayerPL.Name = "btnGetPlayerPL"
        Me.btnGetPlayerPL.Size = New System.Drawing.Size(168, 23)
        Me.btnGetPlayerPL.TabIndex = 73
        Me.btnGetPlayerPL.Text = "Get Player Parental Level"
        '
        'btnGetTitlePL
        '
        Me.btnGetTitlePL.Location = New System.Drawing.Point(4, 140)
        Me.btnGetTitlePL.Name = "btnGetTitlePL"
        Me.btnGetTitlePL.Size = New System.Drawing.Size(168, 23)
        Me.btnGetTitlePL.TabIndex = 74
        Me.btnGetTitlePL.Text = "Get Title Parental Level"
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(4, 172)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(80, 23)
        Me.Button2.TabIndex = 75
        Me.Button2.Text = "Scrap Button"
        '
        'btnAspectRatio
        '
        Me.btnAspectRatio.Location = New System.Drawing.Point(4, 4)
        Me.btnAspectRatio.Name = "btnAspectRatio"
        Me.btnAspectRatio.Size = New System.Drawing.Size(80, 23)
        Me.btnAspectRatio.TabIndex = 69
        Me.btnAspectRatio.Text = "Aspect Ratio"
        '
        'btnGetProcAmp
        '
        Me.btnGetProcAmp.Location = New System.Drawing.Point(308, 4)
        Me.btnGetProcAmp.Name = "btnGetProcAmp"
        Me.btnGetProcAmp.Size = New System.Drawing.Size(120, 23)
        Me.btnGetProcAmp.TabIndex = 77
        Me.btnGetProcAmp.Text = "Get Proc Amp"
        '
        'btnProcAmpRange
        '
        Me.btnProcAmpRange.Location = New System.Drawing.Point(308, 28)
        Me.btnProcAmpRange.Name = "btnProcAmpRange"
        Me.btnProcAmpRange.Size = New System.Drawing.Size(120, 23)
        Me.btnProcAmpRange.TabIndex = 78
        Me.btnProcAmpRange.Text = "Get Proc Amp Range"
        '
        'btnGetVidWin
        '
        Me.btnGetVidWin.Location = New System.Drawing.Point(92, 4)
        Me.btnGetVidWin.Name = "btnGetVidWin"
        Me.btnGetVidWin.Size = New System.Drawing.Size(80, 23)
        Me.btnGetVidWin.TabIndex = 68
        Me.btnGetVidWin.Text = "Get VidWin"
        '
        'btnGetDefMenuLang
        '
        Me.btnGetDefMenuLang.Location = New System.Drawing.Point(4, 36)
        Me.btnGetDefMenuLang.Name = "btnGetDefMenuLang"
        Me.btnGetDefMenuLang.Size = New System.Drawing.Size(168, 23)
        Me.btnGetDefMenuLang.TabIndex = 70
        Me.btnGetDefMenuLang.Text = "Get Default Menu Language"
        '
        'btnGetDefAudLang
        '
        Me.btnGetDefAudLang.Location = New System.Drawing.Point(4, 60)
        Me.btnGetDefAudLang.Name = "btnGetDefAudLang"
        Me.btnGetDefAudLang.Size = New System.Drawing.Size(168, 23)
        Me.btnGetDefAudLang.TabIndex = 71
        Me.btnGetDefAudLang.Text = "Get Default Audio Language"
        '
        'tpFeatures
        '
        Me.tpFeatures.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.tpFeatures.Controls.Add(Me.cbAdmin_CCs)
        Me.tpFeatures.Controls.Add(Me.cbAdmin_LayerbreakSimulation)
        Me.tpFeatures.Controls.Add(Me.btnAdmin_Set)
        Me.tpFeatures.Controls.Add(Me.GroupBox24)
        Me.tpFeatures.Controls.Add(Me.GroupBox25)
        Me.tpFeatures.Controls.Add(Me.cbAdmin_DTSAudio)
        Me.tpFeatures.Controls.Add(Me.cbAdmin_Component)
        Me.tpFeatures.Controls.Add(Me.cbAdmin_ParentalManagement)
        Me.tpFeatures.Controls.Add(Me.cbAdmin_UOPs)
        Me.tpFeatures.Controls.Add(Me.cbAdmin_SPRMs)
        Me.tpFeatures.Controls.Add(Me.cbAdmin_GPRMs)
        Me.tpFeatures.Controls.Add(Me.cbAdmin_FrameGrab)
        Me.tpFeatures.Controls.Add(Me.cbAdmin_JacketPicturePreview)
        Me.tpFeatures.Location = New System.Drawing.Point(4, 22)
        Me.tpFeatures.Name = "tpFeatures"
        Me.tpFeatures.Size = New System.Drawing.Size(560, 390)
        Me.tpFeatures.TabIndex = 2
        Me.tpFeatures.Text = "Features"
        Me.tpFeatures.Visible = False
        '
        'cbAdmin_CCs
        '
        Me.cbAdmin_CCs.Checked = True
        Me.cbAdmin_CCs.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbAdmin_CCs.Location = New System.Drawing.Point(8, 152)
        Me.cbAdmin_CCs.Name = "cbAdmin_CCs"
        Me.cbAdmin_CCs.Size = New System.Drawing.Size(144, 16)
        Me.cbAdmin_CCs.TabIndex = 12
        Me.cbAdmin_CCs.Text = "Closed Captions"
        '
        'cbAdmin_LayerbreakSimulation
        '
        Me.cbAdmin_LayerbreakSimulation.Checked = True
        Me.cbAdmin_LayerbreakSimulation.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbAdmin_LayerbreakSimulation.Location = New System.Drawing.Point(8, 136)
        Me.cbAdmin_LayerbreakSimulation.Name = "cbAdmin_LayerbreakSimulation"
        Me.cbAdmin_LayerbreakSimulation.Size = New System.Drawing.Size(144, 16)
        Me.cbAdmin_LayerbreakSimulation.TabIndex = 11
        Me.cbAdmin_LayerbreakSimulation.Text = "Layerbreak Simulation"
        '
        'btnAdmin_Set
        '
        Me.btnAdmin_Set.Location = New System.Drawing.Point(280, 144)
        Me.btnAdmin_Set.Name = "btnAdmin_Set"
        Me.btnAdmin_Set.Size = New System.Drawing.Size(75, 23)
        Me.btnAdmin_Set.TabIndex = 10
        Me.btnAdmin_Set.Text = "Set"
        '
        'GroupBox24
        '
        Me.GroupBox24.Controls.Add(Me.cbAdmin_VidStd_NTSC)
        Me.GroupBox24.Controls.Add(Me.cbAdmin_VidStd_PAL)
        Me.GroupBox24.Location = New System.Drawing.Point(232, 0)
        Me.GroupBox24.Name = "GroupBox24"
        Me.GroupBox24.Size = New System.Drawing.Size(72, 56)
        Me.GroupBox24.TabIndex = 9
        Me.GroupBox24.TabStop = False
        Me.GroupBox24.Text = "Vid. Stds."
        '
        'cbAdmin_VidStd_NTSC
        '
        Me.cbAdmin_VidStd_NTSC.Checked = True
        Me.cbAdmin_VidStd_NTSC.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbAdmin_VidStd_NTSC.Location = New System.Drawing.Point(8, 16)
        Me.cbAdmin_VidStd_NTSC.Name = "cbAdmin_VidStd_NTSC"
        Me.cbAdmin_VidStd_NTSC.Size = New System.Drawing.Size(56, 16)
        Me.cbAdmin_VidStd_NTSC.TabIndex = 3
        Me.cbAdmin_VidStd_NTSC.Text = "NTSC"
        '
        'cbAdmin_VidStd_PAL
        '
        Me.cbAdmin_VidStd_PAL.Checked = True
        Me.cbAdmin_VidStd_PAL.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbAdmin_VidStd_PAL.Location = New System.Drawing.Point(8, 32)
        Me.cbAdmin_VidStd_PAL.Name = "cbAdmin_VidStd_PAL"
        Me.cbAdmin_VidStd_PAL.Size = New System.Drawing.Size(56, 16)
        Me.cbAdmin_VidStd_PAL.TabIndex = 10
        Me.cbAdmin_VidStd_PAL.Text = "PAL"
        '
        'GroupBox25
        '
        Me.GroupBox25.Controls.Add(Me.cbAdmin_Region_8)
        Me.GroupBox25.Controls.Add(Me.cbAdmin_Region_7)
        Me.GroupBox25.Controls.Add(Me.cbAdmin_Region_6)
        Me.GroupBox25.Controls.Add(Me.cbAdmin_Region_5)
        Me.GroupBox25.Controls.Add(Me.cbAdmin_Region_4)
        Me.GroupBox25.Controls.Add(Me.cbAdmin_Region_3)
        Me.GroupBox25.Controls.Add(Me.cbAdmin_Region_2)
        Me.GroupBox25.Controls.Add(Me.cbAdmin_Region_1)
        Me.GroupBox25.Location = New System.Drawing.Point(160, 0)
        Me.GroupBox25.Name = "GroupBox25"
        Me.GroupBox25.Size = New System.Drawing.Size(64, 152)
        Me.GroupBox25.TabIndex = 8
        Me.GroupBox25.TabStop = False
        Me.GroupBox25.Text = "Regions"
        '
        'cbAdmin_Region_8
        '
        Me.cbAdmin_Region_8.Checked = True
        Me.cbAdmin_Region_8.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbAdmin_Region_8.Location = New System.Drawing.Point(16, 128)
        Me.cbAdmin_Region_8.Name = "cbAdmin_Region_8"
        Me.cbAdmin_Region_8.Size = New System.Drawing.Size(40, 16)
        Me.cbAdmin_Region_8.TabIndex = 9
        Me.cbAdmin_Region_8.Text = "8"
        '
        'cbAdmin_Region_7
        '
        Me.cbAdmin_Region_7.Checked = True
        Me.cbAdmin_Region_7.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbAdmin_Region_7.Location = New System.Drawing.Point(16, 112)
        Me.cbAdmin_Region_7.Name = "cbAdmin_Region_7"
        Me.cbAdmin_Region_7.Size = New System.Drawing.Size(40, 16)
        Me.cbAdmin_Region_7.TabIndex = 8
        Me.cbAdmin_Region_7.Text = "7"
        '
        'cbAdmin_Region_6
        '
        Me.cbAdmin_Region_6.Checked = True
        Me.cbAdmin_Region_6.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbAdmin_Region_6.Location = New System.Drawing.Point(16, 96)
        Me.cbAdmin_Region_6.Name = "cbAdmin_Region_6"
        Me.cbAdmin_Region_6.Size = New System.Drawing.Size(40, 16)
        Me.cbAdmin_Region_6.TabIndex = 7
        Me.cbAdmin_Region_6.Text = "6"
        '
        'cbAdmin_Region_5
        '
        Me.cbAdmin_Region_5.Checked = True
        Me.cbAdmin_Region_5.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbAdmin_Region_5.Location = New System.Drawing.Point(16, 80)
        Me.cbAdmin_Region_5.Name = "cbAdmin_Region_5"
        Me.cbAdmin_Region_5.Size = New System.Drawing.Size(40, 16)
        Me.cbAdmin_Region_5.TabIndex = 6
        Me.cbAdmin_Region_5.Text = "5"
        '
        'cbAdmin_Region_4
        '
        Me.cbAdmin_Region_4.Checked = True
        Me.cbAdmin_Region_4.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbAdmin_Region_4.Location = New System.Drawing.Point(16, 64)
        Me.cbAdmin_Region_4.Name = "cbAdmin_Region_4"
        Me.cbAdmin_Region_4.Size = New System.Drawing.Size(40, 16)
        Me.cbAdmin_Region_4.TabIndex = 5
        Me.cbAdmin_Region_4.Text = "4"
        '
        'cbAdmin_Region_3
        '
        Me.cbAdmin_Region_3.Checked = True
        Me.cbAdmin_Region_3.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbAdmin_Region_3.Location = New System.Drawing.Point(16, 48)
        Me.cbAdmin_Region_3.Name = "cbAdmin_Region_3"
        Me.cbAdmin_Region_3.Size = New System.Drawing.Size(40, 16)
        Me.cbAdmin_Region_3.TabIndex = 4
        Me.cbAdmin_Region_3.Text = "3"
        '
        'cbAdmin_Region_2
        '
        Me.cbAdmin_Region_2.Checked = True
        Me.cbAdmin_Region_2.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbAdmin_Region_2.Location = New System.Drawing.Point(16, 32)
        Me.cbAdmin_Region_2.Name = "cbAdmin_Region_2"
        Me.cbAdmin_Region_2.Size = New System.Drawing.Size(40, 16)
        Me.cbAdmin_Region_2.TabIndex = 3
        Me.cbAdmin_Region_2.Text = "2"
        '
        'cbAdmin_Region_1
        '
        Me.cbAdmin_Region_1.Checked = True
        Me.cbAdmin_Region_1.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbAdmin_Region_1.Location = New System.Drawing.Point(16, 16)
        Me.cbAdmin_Region_1.Name = "cbAdmin_Region_1"
        Me.cbAdmin_Region_1.Size = New System.Drawing.Size(40, 16)
        Me.cbAdmin_Region_1.TabIndex = 2
        Me.cbAdmin_Region_1.Text = "1"
        '
        'cbAdmin_DTSAudio
        '
        Me.cbAdmin_DTSAudio.Checked = True
        Me.cbAdmin_DTSAudio.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbAdmin_DTSAudio.Location = New System.Drawing.Point(8, 120)
        Me.cbAdmin_DTSAudio.Name = "cbAdmin_DTSAudio"
        Me.cbAdmin_DTSAudio.Size = New System.Drawing.Size(144, 16)
        Me.cbAdmin_DTSAudio.TabIndex = 7
        Me.cbAdmin_DTSAudio.Text = "DTS Audio"
        '
        'cbAdmin_Component
        '
        Me.cbAdmin_Component.Checked = True
        Me.cbAdmin_Component.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbAdmin_Component.Location = New System.Drawing.Point(8, 104)
        Me.cbAdmin_Component.Name = "cbAdmin_Component"
        Me.cbAdmin_Component.Size = New System.Drawing.Size(144, 16)
        Me.cbAdmin_Component.TabIndex = 6
        Me.cbAdmin_Component.Text = "Component Video Out"
        '
        'cbAdmin_ParentalManagement
        '
        Me.cbAdmin_ParentalManagement.Checked = True
        Me.cbAdmin_ParentalManagement.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbAdmin_ParentalManagement.Location = New System.Drawing.Point(8, 88)
        Me.cbAdmin_ParentalManagement.Name = "cbAdmin_ParentalManagement"
        Me.cbAdmin_ParentalManagement.Size = New System.Drawing.Size(144, 16)
        Me.cbAdmin_ParentalManagement.TabIndex = 5
        Me.cbAdmin_ParentalManagement.Text = "Parental Management"
        '
        'cbAdmin_UOPs
        '
        Me.cbAdmin_UOPs.Checked = True
        Me.cbAdmin_UOPs.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbAdmin_UOPs.Location = New System.Drawing.Point(8, 72)
        Me.cbAdmin_UOPs.Name = "cbAdmin_UOPs"
        Me.cbAdmin_UOPs.Size = New System.Drawing.Size(144, 16)
        Me.cbAdmin_UOPs.TabIndex = 4
        Me.cbAdmin_UOPs.Text = "UOP Monitor"
        '
        'cbAdmin_SPRMs
        '
        Me.cbAdmin_SPRMs.Checked = True
        Me.cbAdmin_SPRMs.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbAdmin_SPRMs.Location = New System.Drawing.Point(8, 56)
        Me.cbAdmin_SPRMs.Name = "cbAdmin_SPRMs"
        Me.cbAdmin_SPRMs.Size = New System.Drawing.Size(144, 16)
        Me.cbAdmin_SPRMs.TabIndex = 3
        Me.cbAdmin_SPRMs.Text = "SPRM Monitor"
        '
        'cbAdmin_GPRMs
        '
        Me.cbAdmin_GPRMs.Checked = True
        Me.cbAdmin_GPRMs.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbAdmin_GPRMs.Location = New System.Drawing.Point(8, 40)
        Me.cbAdmin_GPRMs.Name = "cbAdmin_GPRMs"
        Me.cbAdmin_GPRMs.Size = New System.Drawing.Size(144, 16)
        Me.cbAdmin_GPRMs.TabIndex = 2
        Me.cbAdmin_GPRMs.Text = "GPRM Monitor"
        '
        'cbAdmin_FrameGrab
        '
        Me.cbAdmin_FrameGrab.Checked = True
        Me.cbAdmin_FrameGrab.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbAdmin_FrameGrab.Location = New System.Drawing.Point(8, 24)
        Me.cbAdmin_FrameGrab.Name = "cbAdmin_FrameGrab"
        Me.cbAdmin_FrameGrab.Size = New System.Drawing.Size(144, 16)
        Me.cbAdmin_FrameGrab.TabIndex = 1
        Me.cbAdmin_FrameGrab.Text = "Frame Grab"
        '
        'cbAdmin_JacketPicturePreview
        '
        Me.cbAdmin_JacketPicturePreview.Checked = True
        Me.cbAdmin_JacketPicturePreview.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbAdmin_JacketPicturePreview.Location = New System.Drawing.Point(8, 8)
        Me.cbAdmin_JacketPicturePreview.Name = "cbAdmin_JacketPicturePreview"
        Me.cbAdmin_JacketPicturePreview.Size = New System.Drawing.Size(144, 16)
        Me.cbAdmin_JacketPicturePreview.TabIndex = 0
        Me.cbAdmin_JacketPicturePreview.Text = "Jacket Picture Preview"
        '
        'tpPPs
        '
        Me.tpPPs.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.tpPPs.Controls.Add(Me.btnDebugging)
        Me.tpPPs.Controls.Add(Me.btnKeystonePP)
        Me.tpPPs.Controls.Add(Me.btnProperties_ar)
        Me.tpPPs.Controls.Add(Me.btnProperties_aud)
        Me.tpPPs.Controls.Add(Me.btnProperties_vr)
        Me.tpPPs.Controls.Add(Me.btnProperties_vid)
        Me.tpPPs.Location = New System.Drawing.Point(4, 22)
        Me.tpPPs.Name = "tpPPs"
        Me.tpPPs.Size = New System.Drawing.Size(560, 390)
        Me.tpPPs.TabIndex = 3
        Me.tpPPs.Text = "Property Pages"
        '
        'btnDebugging
        '
        Me.btnDebugging.Location = New System.Drawing.Point(24, 120)
        Me.btnDebugging.Name = "btnDebugging"
        Me.btnDebugging.Size = New System.Drawing.Size(75, 23)
        Me.btnDebugging.TabIndex = 19
        Me.btnDebugging.Text = "Debugging"
        '
        'btnKeystonePP
        '
        Me.btnKeystonePP.Location = New System.Drawing.Point(8, 64)
        Me.btnKeystonePP.Name = "btnKeystonePP"
        Me.btnKeystonePP.Size = New System.Drawing.Size(88, 23)
        Me.btnKeystonePP.TabIndex = 18
        Me.btnKeystonePP.Text = "Keystone"
        '
        'btnProperties_ar
        '
        Me.btnProperties_ar.Location = New System.Drawing.Point(104, 32)
        Me.btnProperties_ar.Name = "btnProperties_ar"
        Me.btnProperties_ar.Size = New System.Drawing.Size(88, 23)
        Me.btnProperties_ar.TabIndex = 17
        Me.btnProperties_ar.Text = "AR Props"
        '
        'btnProperties_aud
        '
        Me.btnProperties_aud.Location = New System.Drawing.Point(104, 8)
        Me.btnProperties_aud.Name = "btnProperties_aud"
        Me.btnProperties_aud.Size = New System.Drawing.Size(88, 23)
        Me.btnProperties_aud.TabIndex = 16
        Me.btnProperties_aud.Text = "Aud Dec Props"
        '
        'btnProperties_vr
        '
        Me.btnProperties_vr.Location = New System.Drawing.Point(8, 32)
        Me.btnProperties_vr.Name = "btnProperties_vr"
        Me.btnProperties_vr.Size = New System.Drawing.Size(88, 23)
        Me.btnProperties_vr.TabIndex = 15
        Me.btnProperties_vr.Text = "VR Props"
        '
        'btnProperties_vid
        '
        Me.btnProperties_vid.Location = New System.Drawing.Point(8, 8)
        Me.btnProperties_vid.Name = "btnProperties_vid"
        Me.btnProperties_vid.Size = New System.Drawing.Size(88, 23)
        Me.btnProperties_vid.TabIndex = 14
        Me.btnProperties_vid.Text = "Vid Dec Props"
        '
        'Two_Second_Timer
        '
        Me.Two_Second_Timer.Enabled = True
        Me.Two_Second_Timer.Interval = 2000
        '
        'ExtendedOptions_Form
        '
        Me.ClientSize = New System.Drawing.Size(632, 470)
        Me.Controls.Add(Me.tcMain)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.LookAndFeel.SkinName = "Office 2007 Black"
        Me.LookAndFeel.UseDefaultLookAndFeel = False
        Me.MaximizeBox = False
        Me.Name = "ExtendedOptions_Form"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Phoenix Extended Options"
        CType(Me.tcMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tcMain.ResumeLayout(False)
        Me.tpOutputConfig.ResumeLayout(False)
        CType(Me.gbHDMIAudio, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbHDMIAudio.ResumeLayout(False)
        Me.gbHDMIAudio.PerformLayout()
        CType(Me.cbHDMIAudioFormat.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gbAV_AnalogVideoConfig, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbAV_AnalogVideoConfig.ResumeLayout(False)
        Me.gbAV_AnalogVideoConfig.PerformLayout()
        CType(Me.cbAV_AnalogFormat.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl7, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl7.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.gbHDMIScaling.ResumeLayout(False)
        Me.gbHDMIScaling.PerformLayout()
        CType(Me.ceHDMIVidScl_Maximize.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbHDMIResolution.ResumeLayout(False)
        Me.gbHDMIResolution.PerformLayout()
        Me.tpFrameGrabViewer.ResumeLayout(False)
        Me.tpFrameGrabViewer.PerformLayout()
        CType(Me.cbFGV_MultiFrameDirectories.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbFGV_ViewMulti.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cmFGV_FrameGrabOptions.ResumeLayout(False)
        CType(Me.pbFrameGrab, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cmFGV_AspectRatio.ResumeLayout(False)
        Me.tpSubpicture.ResumeLayout(False)
        CType(Me.gbSUB_Positioning, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbSUB_Positioning.ResumeLayout(False)
        Me.gbSUB_Positioning.PerformLayout()
        CType(Me.tbSPPlacement_X, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbSPPlacement_Y, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox17.ResumeLayout(False)
        Me.GroupBox17.PerformLayout()
        CType(Me.TrackBar2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox19.ResumeLayout(False)
        Me.GroupBox19.PerformLayout()
        Me.tpVideo.ResumeLayout(False)
        CType(Me.GroupControl8, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl8.ResumeLayout(False)
        CType(Me.GroupControl10, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl10.ResumeLayout(False)
        CType(Me.cbVID_ReverseFieldOrder.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gbVID_ProcAmp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbVID_ProcAmp.ResumeLayout(False)
        Me.gbVID_ProcAmp.PerformLayout()
        CType(Me.tbBrightness, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbContrast, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbHue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbSaturation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gbVID_MPEGFrameFilter, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbVID_MPEGFrameFilter.ResumeLayout(False)
        CType(Me.gbVID_YUVFilter, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbVID_YUVFilter.ResumeLayout(False)
        Me.GroupBox8.ResumeLayout(False)
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox9.ResumeLayout(False)
        Me.GroupBox10.ResumeLayout(False)
        Me.GroupBox10.PerformLayout()
        CType(Me.tbTimeDelta, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox11.ResumeLayout(False)
        Me.GroupBox11.PerformLayout()
        Me.tpAudio.ResumeLayout(False)
        Me.GroupBox13.ResumeLayout(False)
        Me.GroupBox13.PerformLayout()
        Me.GroupBox14.ResumeLayout(False)
        Me.GroupBox14.PerformLayout()
        CType(Me.AU_tbTimeDelta, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpClosedCaptions.ResumeLayout(False)
        CType(Me.gcLine21Placement, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gcLine21Placement.ResumeLayout(False)
        Me.gcLine21Placement.PerformLayout()
        CType(Me.tbClosedCaptionPlacement_X, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbClosedCaptionPlacement_Y, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox21.ResumeLayout(False)
        Me.GroupBox21.PerformLayout()
        Me.GroupBox22.ResumeLayout(False)
        Me.GroupBox22.PerformLayout()
        Me.tpGuides.ResumeLayout(False)
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        Me.GroupControl2.PerformLayout()
        CType(Me.GroupControl5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl5.ResumeLayout(False)
        CType(Me.GroupControl4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl4.ResumeLayout(False)
        CType(Me.cbGDS_SavedGuides.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl3.ResumeLayout(False)
        CType(Me.ceGuideColor.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbEnableFlexGuides.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbGuide_T, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbGuide_R, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbGuide_B, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbGuide_L, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpUOPTemplates.ResumeLayout(False)
        CType(Me.GroupControl6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl6.ResumeLayout(False)
        CType(Me.cbUOPT_EnableSelectedTemplate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpKeyMapping.ResumeLayout(False)
        Me.tpKeyMapping.PerformLayout()
        CType(Me.cbKM_ControlKey.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgKM_KeyMapping, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpUIOptions.ResumeLayout(False)
        CType(Me.GroupControl14, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl14.ResumeLayout(False)
        CType(Me.ceUseCheckboxesForUOPs.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl13, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl13.ResumeLayout(False)
        Me.GroupControl13.PerformLayout()
        Me.tpDumping.ResumeLayout(False)
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        Me.tpAdmin.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.tpGraphSetup.ResumeLayout(False)
        Me.GroupBox23.ResumeLayout(False)
        Me.tpDebugging.ResumeLayout(False)
        Me.tpFeatures.ResumeLayout(False)
        Me.GroupBox24.ResumeLayout(False)
        Me.GroupBox25.ResumeLayout(False)
        Me.tpPPs.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tcMain As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents tpDumping As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents tpFrameGrabViewer As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents btnResetAllOptions As System.Windows.Forms.Button
    Friend WithEvents cbARs As System.Windows.Forms.ComboBox
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents pbFrameGrab As System.Windows.Forms.PictureBox
    Friend WithEvents tpKeyMapping As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents llKM_ReturnToDefaults As System.Windows.Forms.LinkLabel
    Friend WithEvents llKM_PrintKeyMapping As System.Windows.Forms.LinkLabel
    Friend WithEvents txtCrntFunct As System.Windows.Forms.TextBox
    Friend WithEvents llKM_SetKeyMap As System.Windows.Forms.LinkLabel
    Friend WithEvents txtValue As System.Windows.Forms.TextBox
    Friend WithEvents lbKM_Keys As System.Windows.Forms.ListBox
    Friend WithEvents dgKM_KeyMapping As System.Windows.Forms.DataGrid
    Friend WithEvents gtsOne As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents DataGridTextBoxColumn1 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn2 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents tpVideo As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents cbSplitFields As System.Windows.Forms.CheckBox
    Friend WithEvents cbBumpFields As System.Windows.Forms.CheckBox
    Friend WithEvents rbFilters_none As System.Windows.Forms.RadioButton
    Friend WithEvents rbFilters_YUV_MinusU As System.Windows.Forms.RadioButton
    Friend WithEvents rbFilters_YUV_MinusV As System.Windows.Forms.RadioButton
    Friend WithEvents rbFilters_bandw As System.Windows.Forms.RadioButton
    Friend WithEvents rbVidMode_I As System.Windows.Forms.RadioButton
    Friend WithEvents rbVidMode_IP As System.Windows.Forms.RadioButton
    Friend WithEvents rbVidMode_IBP As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox8 As System.Windows.Forms.GroupBox
    Friend WithEvents btnDEINT_CheckStatus As System.Windows.Forms.Button
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents rbDEINT_Control_Video As System.Windows.Forms.RadioButton
    Friend WithEvents rbDEINT_Control_Film As System.Windows.Forms.RadioButton
    Friend WithEvents rbDEINT_Control_Smart As System.Windows.Forms.RadioButton
    Friend WithEvents rbDEINT_Control_Auto As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox9 As System.Windows.Forms.GroupBox
    Friend WithEvents rbDEINT_Mode_WeaveFiltered As System.Windows.Forms.RadioButton
    Friend WithEvents rbDEINT_Mode_Spad As System.Windows.Forms.RadioButton
    Friend WithEvents rbDEINT_Mode_Bob As System.Windows.Forms.RadioButton
    Friend WithEvents rbDEINT_Mode_Weave As System.Windows.Forms.RadioButton
    Friend WithEvents rbDEINT_Mode_Normal As System.Windows.Forms.RadioButton
    Friend WithEvents cbForceFieldReversal As System.Windows.Forms.CheckBox
    Friend WithEvents btnProcAmpScrap As System.Windows.Forms.Button
    Friend WithEvents btnVideoScrap As System.Windows.Forms.Button
    Friend WithEvents GroupBox10 As System.Windows.Forms.GroupBox
    Friend WithEvents btnTimeDeltaSet As System.Windows.Forms.Button
    Friend WithEvents txtTimeDeltaVal As System.Windows.Forms.TextBox
    Friend WithEvents tbTimeDelta As System.Windows.Forms.TrackBar
    Friend WithEvents GroupBox11 As System.Windows.Forms.GroupBox
    Friend WithEvents lblFramesCaptured As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents btnStartStopVideoCapture As System.Windows.Forms.Button
    Friend WithEvents btnCaptureBrowse As System.Windows.Forms.Button
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtCaptureFileName As System.Windows.Forms.TextBox
    Friend WithEvents cbDoProcAmp As System.Windows.Forms.CheckBox
    Friend WithEvents cbProcAmpHalfFrame As System.Windows.Forms.CheckBox
    Friend WithEvents llPA_DeleteSettings As System.Windows.Forms.LinkLabel
    Friend WithEvents llPA_SaveSettings As System.Windows.Forms.LinkLabel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cbPA_Presets As System.Windows.Forms.ComboBox
    Friend WithEvents txtContrast As System.Windows.Forms.TextBox
    Friend WithEvents txtBrightness As System.Windows.Forms.TextBox
    Friend WithEvents tbSaturation As System.Windows.Forms.TrackBar
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents tbHue As System.Windows.Forms.TrackBar
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents tbContrast As System.Windows.Forms.TrackBar
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents tbBrightness As System.Windows.Forms.TrackBar
    Friend WithEvents txtSaturation As System.Windows.Forms.TextBox
    Friend WithEvents txtHue As System.Windows.Forms.TextBox
    Friend WithEvents llPA_LoadSettings As System.Windows.Forms.LinkLabel
    Friend WithEvents tpAudio As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents GroupBox13 As System.Windows.Forms.GroupBox
    Friend WithEvents AU_lblFramesCaptured As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents AU_btnStartStopVideoCapture As System.Windows.Forms.Button
    Friend WithEvents AU_btnCaptureBrowse As System.Windows.Forms.Button
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents AU_txtCaptureFileName As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox14 As System.Windows.Forms.GroupBox
    Friend WithEvents AU_btnTimeDeltaSet As System.Windows.Forms.Button
    Friend WithEvents AU_txtTimeDeltaVal As System.Windows.Forms.TextBox
    Friend WithEvents AU_tbTimeDelta As System.Windows.Forms.TrackBar
    Friend WithEvents tpSubpicture As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents cbSPPlacementEnabled As System.Windows.Forms.CheckBox
    Friend WithEvents txtSPPlacement_Y As System.Windows.Forms.TextBox
    Friend WithEvents txtSPPlacement_X As System.Windows.Forms.TextBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents tbSPPlacement_Y As System.Windows.Forms.TrackBar
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents tbSPPlacement_X As System.Windows.Forms.TrackBar
    Friend WithEvents GroupBox17 As System.Windows.Forms.GroupBox
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents TextBox3 As System.Windows.Forms.TextBox
    Friend WithEvents TrackBar2 As System.Windows.Forms.TrackBar
    Friend WithEvents GroupBox19 As System.Windows.Forms.GroupBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents tpClosedCaptions As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents cbClosedCaptionPlacement As System.Windows.Forms.CheckBox
    Friend WithEvents txtCCPlacementY As System.Windows.Forms.TextBox
    Friend WithEvents txtCCPlacementX As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents tbClosedCaptionPlacement_Y As System.Windows.Forms.TrackBar
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents tbClosedCaptionPlacement_X As System.Windows.Forms.TrackBar
    Friend WithEvents GroupBox21 As System.Windows.Forms.GroupBox
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents Button14 As System.Windows.Forms.Button
    Friend WithEvents Button15 As System.Windows.Forms.Button
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents TextBox11 As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox22 As System.Windows.Forms.GroupBox
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents Button12 As System.Windows.Forms.Button
    Friend WithEvents Button13 As System.Windows.Forms.Button
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents TextBox10 As System.Windows.Forms.TextBox
    Friend WithEvents tpAdmin As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents tpGraphSetup As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox23 As System.Windows.Forms.GroupBox
    Friend WithEvents rbGraphProfiles_3 As System.Windows.Forms.RadioButton
    Friend WithEvents Label49 As System.Windows.Forms.Label
    Friend WithEvents rbGraphProfiles_2 As System.Windows.Forms.RadioButton
    Friend WithEvents rbGraphProfiles_0 As System.Windows.Forms.RadioButton
    Friend WithEvents cbROTGraph As System.Windows.Forms.CheckBox
    Friend WithEvents tpDebugging As System.Windows.Forms.TabPage
    Friend WithEvents btnGetDefSubLang As System.Windows.Forms.Button
    Friend WithEvents btnGetPlayerPL As System.Windows.Forms.Button
    Friend WithEvents btnGetTitlePL As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents btnAspectRatio As System.Windows.Forms.Button
    Friend WithEvents btnGetProcAmp As System.Windows.Forms.Button
    Friend WithEvents btnProcAmpRange As System.Windows.Forms.Button
    Friend WithEvents btnGetVidWin As System.Windows.Forms.Button
    Friend WithEvents btnGetDefMenuLang As System.Windows.Forms.Button
    Friend WithEvents btnGetDefAudLang As System.Windows.Forms.Button
    Friend WithEvents tpFeatures As System.Windows.Forms.TabPage
    Friend WithEvents cbAdmin_CCs As System.Windows.Forms.CheckBox
    Friend WithEvents cbAdmin_LayerbreakSimulation As System.Windows.Forms.CheckBox
    Friend WithEvents btnAdmin_Set As System.Windows.Forms.Button
    Friend WithEvents GroupBox24 As System.Windows.Forms.GroupBox
    Friend WithEvents cbAdmin_VidStd_NTSC As System.Windows.Forms.CheckBox
    Friend WithEvents cbAdmin_VidStd_PAL As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox25 As System.Windows.Forms.GroupBox
    Friend WithEvents cbAdmin_Region_8 As System.Windows.Forms.CheckBox
    Friend WithEvents cbAdmin_Region_7 As System.Windows.Forms.CheckBox
    Friend WithEvents cbAdmin_Region_6 As System.Windows.Forms.CheckBox
    Friend WithEvents cbAdmin_Region_5 As System.Windows.Forms.CheckBox
    Friend WithEvents cbAdmin_Region_4 As System.Windows.Forms.CheckBox
    Friend WithEvents cbAdmin_Region_3 As System.Windows.Forms.CheckBox
    Friend WithEvents cbAdmin_Region_2 As System.Windows.Forms.CheckBox
    Friend WithEvents cbAdmin_Region_1 As System.Windows.Forms.CheckBox
    Friend WithEvents cbAdmin_DTSAudio As System.Windows.Forms.CheckBox
    Friend WithEvents cbAdmin_Component As System.Windows.Forms.CheckBox
    Friend WithEvents cbAdmin_ParentalManagement As System.Windows.Forms.CheckBox
    Friend WithEvents cbAdmin_UOPs As System.Windows.Forms.CheckBox
    Friend WithEvents cbAdmin_SPRMs As System.Windows.Forms.CheckBox
    Friend WithEvents cbAdmin_GPRMs As System.Windows.Forms.CheckBox
    Friend WithEvents cbAdmin_FrameGrab As System.Windows.Forms.CheckBox
    Friend WithEvents cbAdmin_JacketPicturePreview As System.Windows.Forms.CheckBox
    Friend WithEvents tpPPs As System.Windows.Forms.TabPage
    Friend WithEvents btnDebugging As System.Windows.Forms.Button
    Friend WithEvents btnKeystonePP As System.Windows.Forms.Button
    Friend WithEvents btnProperties_ar As System.Windows.Forms.Button
    Friend WithEvents btnProperties_aud As System.Windows.Forms.Button
    Friend WithEvents btnProperties_vr As System.Windows.Forms.Button
    Friend WithEvents btnProperties_vid As System.Windows.Forms.Button
    Friend WithEvents lblFGV_FileName As System.Windows.Forms.Label
    Friend WithEvents cmFGV_AspectRatio As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mi853480 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mi853576 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mi720480 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mi720576 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnFGV_SaveCurrent As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnFGV_DeleteCurrent As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents cbFGV_MultiFrameDirectories As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents cbFGV_ViewMulti As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents btnFGV_ViewNext As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnFGV_ViewPrevious As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnFGV_GrabFrame As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents cmFGV_FrameGrabOptions As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents miFGV_FullMix As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents miFGV_VideoAndSubpicture As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents miFGV_VideoOnly As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents miFGV_SubpictureOnly As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents miFGV_ClosedCaptionsOnly As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents miFGV_MultiFrame As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents miFGV_Bitmap As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents miFGV_JPEG As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents miFGV_GIF As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents miFGV_PNG As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents miFGV_TIF As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnFGV_OpenTargetDir As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents cbKM_ControlKey As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents btnOpenDumpDirectory As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnDumpDirectoryBrowse As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents txtDumpDirectory As System.Windows.Forms.TextBox
    Friend WithEvents btnADMIN_SaveCurrentLayout As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents tpUOPTemplates As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents cbUOPT_EnableSelectedTemplate As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents btnUOPT_RenameSelectedTemplate As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnUOPT_DeleteSelectedTemplate As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GroupControl6 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents cbUO_24 As System.Windows.Forms.CheckBox
    Friend WithEvents cbUO_23 As System.Windows.Forms.CheckBox
    Friend WithEvents cbUO_22 As System.Windows.Forms.CheckBox
    Friend WithEvents cbUO_21 As System.Windows.Forms.CheckBox
    Friend WithEvents cbUO_20 As System.Windows.Forms.CheckBox
    Friend WithEvents cbUO_19 As System.Windows.Forms.CheckBox
    Friend WithEvents cbUO_18 As System.Windows.Forms.CheckBox
    Friend WithEvents cbUO_17 As System.Windows.Forms.CheckBox
    Friend WithEvents cbUO_16 As System.Windows.Forms.CheckBox
    Friend WithEvents cbUO_15 As System.Windows.Forms.CheckBox
    Friend WithEvents cbUO_14 As System.Windows.Forms.CheckBox
    Friend WithEvents cbUO_13 As System.Windows.Forms.CheckBox
    Friend WithEvents cbUO_12 As System.Windows.Forms.CheckBox
    Friend WithEvents cbUO_11 As System.Windows.Forms.CheckBox
    Friend WithEvents cbUO_10 As System.Windows.Forms.CheckBox
    Friend WithEvents cbUO_9 As System.Windows.Forms.CheckBox
    Friend WithEvents cbUO_8 As System.Windows.Forms.CheckBox
    Friend WithEvents cbUO_7 As System.Windows.Forms.CheckBox
    Friend WithEvents cbUO_6 As System.Windows.Forms.CheckBox
    Friend WithEvents cbUO_5 As System.Windows.Forms.CheckBox
    Friend WithEvents cbUO_4 As System.Windows.Forms.CheckBox
    Friend WithEvents cbUO_3 As System.Windows.Forms.CheckBox
    Friend WithEvents cbUO_2 As System.Windows.Forms.CheckBox
    Friend WithEvents cbUO_1 As System.Windows.Forms.CheckBox
    Friend WithEvents cbUO_0 As System.Windows.Forms.CheckBox
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents Label41 As System.Windows.Forms.Label
    Friend WithEvents Label42 As System.Windows.Forms.Label
    Friend WithEvents Label173 As System.Windows.Forms.Label
    Friend WithEvents Label174 As System.Windows.Forms.Label
    Friend WithEvents Label175 As System.Windows.Forms.Label
    Friend WithEvents Label176 As System.Windows.Forms.Label
    Friend WithEvents Label177 As System.Windows.Forms.Label
    Friend WithEvents Label178 As System.Windows.Forms.Label
    Friend WithEvents Label179 As System.Windows.Forms.Label
    Friend WithEvents Label180 As System.Windows.Forms.Label
    Friend WithEvents Label181 As System.Windows.Forms.Label
    Friend WithEvents Label182 As System.Windows.Forms.Label
    Friend WithEvents Label183 As System.Windows.Forms.Label
    Friend WithEvents Label184 As System.Windows.Forms.Label
    Friend WithEvents Label185 As System.Windows.Forms.Label
    Friend WithEvents Label186 As System.Windows.Forms.Label
    Friend WithEvents Label187 As System.Windows.Forms.Label
    Friend WithEvents Label188 As System.Windows.Forms.Label
    Friend WithEvents Label189 As System.Windows.Forms.Label
    Friend WithEvents Label190 As System.Windows.Forms.Label
    Friend WithEvents Label191 As System.Windows.Forms.Label
    Friend WithEvents Label192 As System.Windows.Forms.Label
    Friend WithEvents btnUOPT_SaveNewTemplate As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnUOPT_SaveSelectedTemplate As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents lvUOPT_ExistingTemplates As System.Windows.Forms.ListBox
    Friend WithEvents Two_Second_Timer As System.Windows.Forms.Timer
    Friend WithEvents gbVID_MPEGFrameFilter As DevExpress.XtraEditors.GroupControl
    Friend WithEvents gbVID_YUVFilter As DevExpress.XtraEditors.GroupControl
    Friend WithEvents gbVID_ProcAmp As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GroupControl10 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents btnTestPattern As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnColorBars As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents gbSUB_Positioning As DevExpress.XtraEditors.GroupControl
    Friend WithEvents gcLine21Placement As DevExpress.XtraEditors.GroupControl
    Friend WithEvents tpOutputConfig As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents gbHDMIResolution As System.Windows.Forms.GroupBox
    Friend WithEvents rbIntensityResolution_1080 As System.Windows.Forms.RadioButton
    Friend WithEvents rbIntensityResolution_720 As System.Windows.Forms.RadioButton
    Friend WithEvents rbIntensity As System.Windows.Forms.RadioButton
    Friend WithEvents rbDecklink As System.Windows.Forms.RadioButton
    Friend WithEvents rbDesktop As System.Windows.Forms.RadioButton
    Friend WithEvents tpGuides As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GroupControl5 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblGuideBox_W As System.Windows.Forms.Label
    Friend WithEvents lblGuideBox_H As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblAspect As System.Windows.Forms.Label
    Friend WithEvents GroupControl4 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents cbGDS_SavedGuides As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents btnGDS_DeleteSelectedGuides As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnGDS_LoadSelectedGuides As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents btnGDS_SaveCurrentGuides As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GroupControl3 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents btnGDS_MoveAll_Down As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnGDS_MoveAll_Up As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnGDS_MoveAll_Right As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnGDS_MoveAll_Left As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents ceGuideColor As DevExpress.XtraEditors.ColorEdit
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents cbEnableFlexGuides As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents txtGuide_R As System.Windows.Forms.TextBox
    Friend WithEvents tbGuide_T As System.Windows.Forms.TrackBar
    Friend WithEvents txtGuide_L As System.Windows.Forms.TextBox
    Friend WithEvents tbGuide_R As System.Windows.Forms.TrackBar
    Friend WithEvents txtGuide_B As System.Windows.Forms.TextBox
    Friend WithEvents tbGuide_B As System.Windows.Forms.TrackBar
    Friend WithEvents txtGuide_T As System.Windows.Forms.TextBox
    Friend WithEvents tbGuide_L As System.Windows.Forms.TrackBar
    Friend WithEvents cbDumpAudio As System.Windows.Forms.CheckBox
    Friend WithEvents tpUIOptions As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents rbRootResume_SeparateButtons As System.Windows.Forms.RadioButton
    Friend WithEvents rbRootResume_RootIsResume As System.Windows.Forms.RadioButton
    Friend WithEvents GroupControl14 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ceUseCheckboxesForUOPs As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents GroupControl13 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GroupControl7 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents gbAV_AnalogVideoConfig As DevExpress.XtraEditors.GroupControl
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents cbAV_AnalogFormat As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents cbVID_ReverseFieldOrder As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents gbHDMIScaling As System.Windows.Forms.GroupBox
    Friend WithEvents rbHDMIVidScl_AdjustToAspect As System.Windows.Forms.RadioButton
    Friend WithEvents rbHDMIVidScl_Native As System.Windows.Forms.RadioButton
    Friend WithEvents gbHDMIAudio As DevExpress.XtraEditors.GroupControl
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents cbHDMIAudioFormat As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents ceHDMIVidScl_Maximize As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents rbIntensityResolution_486 As System.Windows.Forms.RadioButton
    Friend WithEvents rbIntensityResolution_576 As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnApplyDeviceChanges As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GroupControl8 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents cbVID_NonSeamlessCellNotification As System.Windows.Forms.CheckBox
End Class
