Imports SMT.Multimedia.Enums
Imports SMT.Multimedia.Players.DVD
Imports SMT.Multimedia.Players.DVD.Enums
Imports SMT.DotNet.AppConsole
Imports SMT.Multimedia.GraphConstruction

Imports DevExpress.XtraEditors

Public Class dlgPlayerDefaultAccelerator
    Inherits DevExpress.XtraEditors.XtraForm

#Region " Windows Form Designer generated code "

    Private PP As Phoenix_Form
    Public Sub New(ByVal _PP As Phoenix_Form)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        PP = _PP
    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Timer As System.Windows.Forms.Timer
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cbAudLang As System.Windows.Forms.ComboBox
    Friend WithEvents cbSubLang As System.Windows.Forms.ComboBox
    Friend WithEvents cbMenuLang As System.Windows.Forms.ComboBox
    Friend WithEvents cbAspectRatio As System.Windows.Forms.ComboBox
    Friend WithEvents cbRegion As System.Windows.Forms.ComboBox
    Friend WithEvents cbOpenNew As System.Windows.Forms.CheckBox
    Friend WithEvents btnApply As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cbParentalLevel As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cbParentalCountry As System.Windows.Forms.ComboBox
    Friend WithEvents cbApplyAudToAll As System.Windows.Forms.CheckBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.cbAudLang = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Timer = New System.Windows.Forms.Timer(Me.components)
        Me.Label2 = New System.Windows.Forms.Label
        Me.cbSubLang = New System.Windows.Forms.ComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.cbMenuLang = New System.Windows.Forms.ComboBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.cbAspectRatio = New System.Windows.Forms.ComboBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.cbRegion = New System.Windows.Forms.ComboBox
        Me.cbOpenNew = New System.Windows.Forms.CheckBox
        Me.cbApplyAudToAll = New System.Windows.Forms.CheckBox
        Me.btnApply = New DevExpress.XtraEditors.SimpleButton
        Me.Label6 = New System.Windows.Forms.Label
        Me.cbParentalLevel = New System.Windows.Forms.ComboBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.cbParentalCountry = New System.Windows.Forms.ComboBox
        Me.SuspendLayout()
        '
        'cbAudLang
        '
        Me.cbAudLang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbAudLang.Location = New System.Drawing.Point(76, 22)
        Me.cbAudLang.Name = "cbAudLang"
        Me.cbAudLang.Size = New System.Drawing.Size(121, 21)
        Me.cbAudLang.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(4, 26)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(72, 17)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "Audio:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Timer
        '
        Me.Timer.Interval = 250
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(4, 52)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(72, 17)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "Subtitle:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cbSubLang
        '
        Me.cbSubLang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbSubLang.Location = New System.Drawing.Point(76, 47)
        Me.cbSubLang.Name = "cbSubLang"
        Me.cbSubLang.Size = New System.Drawing.Size(121, 21)
        Me.cbSubLang.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(4, 78)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(72, 17)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "Menu:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cbMenuLang
        '
        Me.cbMenuLang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbMenuLang.Location = New System.Drawing.Point(76, 73)
        Me.cbMenuLang.Name = "cbMenuLang"
        Me.cbMenuLang.Size = New System.Drawing.Size(121, 21)
        Me.cbMenuLang.TabIndex = 3
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(4, 103)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(72, 18)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "Aspect Ratio:"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cbAspectRatio
        '
        Me.cbAspectRatio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbAspectRatio.Location = New System.Drawing.Point(76, 99)
        Me.cbAspectRatio.Name = "cbAspectRatio"
        Me.cbAspectRatio.Size = New System.Drawing.Size(121, 21)
        Me.cbAspectRatio.TabIndex = 4
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(4, 129)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(72, 17)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "Region:"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cbRegion
        '
        Me.cbRegion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbRegion.Location = New System.Drawing.Point(76, 125)
        Me.cbRegion.Name = "cbRegion"
        Me.cbRegion.Size = New System.Drawing.Size(121, 21)
        Me.cbRegion.TabIndex = 5
        '
        'cbOpenNew
        '
        Me.cbOpenNew.Location = New System.Drawing.Point(79, 203)
        Me.cbOpenNew.Name = "cbOpenNew"
        Me.cbOpenNew.Size = New System.Drawing.Size(116, 17)
        Me.cbOpenNew.TabIndex = 8
        Me.cbOpenNew.Text = "Open New Project"
        '
        'cbApplyAudToAll
        '
        Me.cbApplyAudToAll.Location = New System.Drawing.Point(76, 4)
        Me.cbApplyAudToAll.Name = "cbApplyAudToAll"
        Me.cbApplyAudToAll.Size = New System.Drawing.Size(116, 18)
        Me.cbApplyAudToAll.TabIndex = 0
        Me.cbApplyAudToAll.Text = "Apply Aud. To All"
        '
        'btnApply
        '
        Me.btnApply.Location = New System.Drawing.Point(4, 222)
        Me.btnApply.Name = "btnApply"
        Me.btnApply.Size = New System.Drawing.Size(193, 23)
        Me.btnApply.TabIndex = 9
        Me.btnApply.Text = "Apply"
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(4, 180)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(72, 17)
        Me.Label6.TabIndex = 17
        Me.Label6.Text = "Parental LV:"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cbParentalLevel
        '
        Me.cbParentalLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbParentalLevel.Location = New System.Drawing.Point(76, 176)
        Me.cbParentalLevel.Name = "cbParentalLevel"
        Me.cbParentalLevel.Size = New System.Drawing.Size(121, 21)
        Me.cbParentalLevel.TabIndex = 7
        '
        'Label7
        '
        Me.Label7.Location = New System.Drawing.Point(4, 154)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(72, 18)
        Me.Label7.TabIndex = 16
        Me.Label7.Text = "Parental CT:"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cbParentalCountry
        '
        Me.cbParentalCountry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbParentalCountry.Location = New System.Drawing.Point(76, 150)
        Me.cbParentalCountry.Name = "cbParentalCountry"
        Me.cbParentalCountry.Size = New System.Drawing.Size(121, 21)
        Me.cbParentalCountry.TabIndex = 6
        '
        'dlgPlayerDefaultAccelerator
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        Me.ClientSize = New System.Drawing.Size(202, 249)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.cbParentalLevel)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.cbParentalCountry)
        Me.Controls.Add(Me.btnApply)
        Me.Controls.Add(Me.cbApplyAudToAll)
        Me.Controls.Add(Me.cbOpenNew)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.cbRegion)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.cbAspectRatio)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cbMenuLang)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cbSubLang)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cbAudLang)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.KeyPreview = True
        Me.Name = "dlgPlayerDefaultAccelerator"
        Me.ShowInTaskbar = False
        Me.Text = " Player Default Accelerator"
        Me.TopMost = True
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub dlgPlayerDefaults_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Setup()
        Me.Top = PP.Top + 50
        Me.Left = PP.Left + 150
    End Sub

    Private Sub Setup()
        Try
            PP.KH.PauseHook()
            Timer.Start()
            For Each s As String In [Enum].GetNames(GetType(eLanguages))
                Me.cbAudLang.Items.Add(s)
                Me.cbSubLang.Items.Add(s)
                Me.cbMenuLang.Items.Add(s)
            Next
            Me.cbAudLang.SelectedIndex = Me.cbAudLang.FindString(PP.Player.DefaultAudioLanguage.ToString)
            Me.cbSubLang.SelectedIndex = Me.cbSubLang.FindString(PP.Player.DefaultSubtitleLanguage.ToString)
            Me.cbMenuLang.SelectedIndex = Me.cbMenuLang.FindString(PP.Player.DefaultMenuLanguage.ToString)

            For Each s As String In [Enum].GetNames(GetType(ePreferredAspectRatio))
                Me.cbAspectRatio.Items.Add(s)
            Next
            Me.cbAspectRatio.SelectedIndex = Me.cbAspectRatio.FindString(PP.Player.DefaultAspectRatio.ToString)

            For Each s As String In [Enum].GetNames(GetType(eRegions))
                Me.cbRegion.Items.Add(s)
            Next
            Me.cbRegion.SelectedIndex = Me.cbRegion.FindString(PP.Player.Region.ToString)

            If PP.FeatureManagement.Features.FE_DVD_ParentalManagement Then
                Me.cbParentalCountry.Enabled = True
                Me.cbParentalLevel.Enabled = True
                For Each s As String In [Enum].GetNames(GetType(eCountries))
                    Me.cbParentalCountry.Items.Add(s)
                Next
                Me.cbParentalCountry.SelectedIndex = Me.cbParentalCountry.FindString(PP.CurrentConfig.ParentalCountry.ToString)

                For Each s As String In [Enum].GetNames(GetType(eParentalLevels))
                    Me.cbParentalLevel.Items.Add(s)
                Next
                Me.cbParentalLevel.SelectedIndex = Me.cbParentalLevel.FindString(PP.CurrentConfig.ParentalLevel.ToString)
            Else
                Me.cbParentalCountry.Enabled = False
                Me.cbParentalLevel.Enabled = False
            End If


        Catch ex As Exception
            PP.AddConsoleLine(eConsoleItemType.ERROR, "Problem with Setup(). Error: " & ex.Message)
        End Try
    End Sub

    Public Sub Destruct()
        PP.KH.UnpauseHook()
    End Sub

    Private Sub dlgPlayerDefaults_KeyUp(ByVal sender As Object, ByVal e As KeyEventArgs) Handles MyBase.KeyUp
        If Not AcceptKeys Then Exit Sub
        If e.KeyData = Keys.Enter Then
            Execute()
        ElseIf e.KeyData = Keys.Tab Then
            If CType(sender, Form).ActiveControl.Name.ToLower = "cbsublang" Then
                If Me.cbApplyAudToAll.Checked Then
                    Me.cbMenuLang.SelectedIndex = Me.cbMenuLang.FindStringExact(Me.cbAudLang.SelectedItem.ToString)
                    Me.cbSubLang.SelectedIndex = Me.cbSubLang.FindStringExact(Me.cbAudLang.SelectedItem.ToString)
                    Me.cbAspectRatio.Focus()
                End If
            End If
        ElseIf e.KeyData = Keys.Escape Then
            DialogResult = DialogResult.Cancel
        End If
    End Sub

    Private Sub Execute()
        Try
            If Me.cbAudLang.SelectedItem.ToString = "" Then
                XtraMessageBox.Show(Me.LookAndFeel, Me, "Please select an audio language.", My.Settings.APPLICATION_NAME, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
            If Me.cbSubLang.SelectedItem.ToString = "" Then
                XtraMessageBox.Show(Me.LookAndFeel, Me, "Please select a subtitle language.", My.Settings.APPLICATION_NAME, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
            If Me.cbMenuLang.SelectedItem.ToString = "" Then
                XtraMessageBox.Show(Me.LookAndFeel, Me, "Please select a menu language.", My.Settings.APPLICATION_NAME, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
            If Me.cbAspectRatio.SelectedItem.ToString = "" Then
                XtraMessageBox.Show(Me.LookAndFeel, Me, "Please select an aspect ratio.", My.Settings.APPLICATION_NAME, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
            If Me.cbRegion.SelectedItem.ToString = "" Then
                XtraMessageBox.Show(Me.LookAndFeel, Me, "Please select a region.", My.Settings.APPLICATION_NAME, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            Me.Hide()
            Me.Cursor = Cursors.WaitCursor

            If Not PP.Player.PlayState = ePlayState.SystemJP Then
                If Not PP.Player.EjectProject Then
                    XtraMessageBox.Show(Me.LookAndFeel, Me, "Failed to eject project. " & My.Settings.APPLICATION_NAME & " must exit.", My.Settings.APPLICATION_NAME, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Application.Exit()
                Else
                    'PP.Player = New cDVDPlayer(PP, PP.ForceMobileMode)
                    PP.Player = New cDVDPlayer(New cSMTForm(PP), PP.PreferredAVMode, PP.CurrentUserProfile.AppOptions.IntensityVideoScalingMode, PP.CurrentUserProfile.AppOptions.IntensityVideoResolution, My.Settings.DVD_NOTIFY_NONSEAMLESSCELL)
                End If
            End If

            If Me.cbApplyAudToAll.Checked Then
                PP.CurrentConfig.AudioLanguage = [Enum].Parse(GetType(eLanguages), Me.cbAudLang.SelectedItem.ToString)
                PP.CurrentConfig.SubtitleLanguage = [Enum].Parse(GetType(eLanguages), Me.cbAudLang.SelectedItem.ToString)
                PP.CurrentConfig.MenuLanguage = [Enum].Parse(GetType(eLanguages), Me.cbAudLang.SelectedItem.ToString)
            Else
                PP.CurrentConfig.AudioLanguage = [Enum].Parse(GetType(eLanguages), Me.cbAudLang.SelectedItem.ToString)
                PP.CurrentConfig.SubtitleLanguage = [Enum].Parse(GetType(eLanguages), Me.cbSubLang.SelectedItem.ToString)
                PP.CurrentConfig.MenuLanguage = [Enum].Parse(GetType(eLanguages), Me.cbMenuLang.SelectedItem.ToString)
            End If

            PP.CurrentConfig.AspectRatio = [Enum].Parse(GetType(ePreferredAspectRatio), Me.cbAspectRatio.SelectedItem.ToString)
            PP.CurrentConfig.Region = Me.cbRegion.SelectedIndex

            PP.CurrentConfig.ParentalCountry = [Enum].Parse(GetType(eCountries), Me.cbParentalCountry.SelectedItem.ToString)
            PP.CurrentConfig.ParentalLevel = [Enum].Parse(GetType(eParentalLevels), Me.cbParentalLevel.SelectedItem.ToString)

            If Me.cbOpenNew.Checked Then
                PP.Player.PlayState = ePlayState.SystemJP
                PP.SelectProject_WithDialog()
            Else
                PP.Player.InitializePlayer(PP.MostRecentProject, PP.PrepPlayerDefaults)
                If PP.Player.AVMode = eAVMode.DesktopVMR Then PP.Player.ShowViewer()
            End If

            PP.pgOptions.Refresh()

            DialogResult = DialogResult.OK
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            PP.AddConsoleLine(eConsoleItemType.ERROR, "Problem with Execute() in PDA. Error: " & ex.Message)
        End Try
    End Sub

    Private Sub btnApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApply.Click
        Execute()
    End Sub

    Private Sub dlgPlayerDefaults_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        If DialogResult <> DialogResult.OK Then
            DialogResult = DialogResult.Cancel
        End If
        Destruct()
    End Sub

    Private AcceptKeys As Boolean = False
    Private Sub Timer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer.Tick
        AcceptKeys = True
        Timer.Stop()
    End Sub

    'Private Sub cbAudLang_KeyUp(ByVal sender As Object, ByVal e As KeyEventArgs) Handles cbAudLang.KeyUp
    '    If Not AcceptKeys Then Exit Sub
    '    Dim t As Short = Me.cbAudLang.FindString(e.KeyData.ToString)
    '    If t > -1 Then
    '        Me.cbAudLang.SelectedIndex = t
    '    End If
    'End Sub

    'Private Sub cbSubLang_KeyUp(ByVal sender As Object, ByVal e As KeyEventArgs) Handles cbSubLang.KeyUp
    '    If Not AcceptKeys Then Exit Sub
    '    Dim t As Short = Me.cbSubLang.FindString(e.KeyData.ToString)
    '    If t > -1 Then
    '        Me.cbSubLang.SelectedIndex = t
    '    End If
    'End Sub

    'Private Sub cbMenuLang_KeyUp(ByVal sender As Object, ByVal e As KeyEventArgs) Handles cbMenuLang.KeyUp
    '    If Not AcceptKeys Then Exit Sub
    '    Dim t As Short = Me.cbMenuLang.FindString(e.KeyData.ToString)
    '    If t > -1 Then
    '        Me.cbMenuLang.SelectedIndex = t
    '    End If
    'End Sub

    'Private Sub cbAspectRatio_KeyUp(ByVal sender As Object, ByVal e As KeyEventArgs) Handles cbAspectRatio.KeyUp
    '    If Not AcceptKeys Then Exit Sub
    '    Dim t As Short = Me.cbAspectRatio.FindString(e.KeyData.ToString)
    '    If t > -1 Then
    '        Me.cbAspectRatio.SelectedIndex = t
    '    End If
    'End Sub

    'Private Sub cbRegion_KeyUp(ByVal sender As Object, ByVal e As KeyEventArgs) Handles cbRegion.KeyUp
    '    If Not AcceptKeys Then Exit Sub
    '    Dim t As Short = Me.cbRegion.FindString(e.KeyData.ToString)
    '    If t > -1 Then
    '        Me.cbRegion.SelectedIndex = t
    '    End If
    'End Sub

    'Private Sub cbAudioLang_KeyUp(ByVal sender As Object, ByVal e As KeyEventArgs) Handles cbAudLang.KeyUp
    '    If e.KeyData = Keys.Tab And Me.cbApplyAudToAll.Checked Then
    '        Me.cbAspectRatio.Focus()
    '    End If
    'End Sub

    Private Sub dlgPlayerDefaultAccelerator_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Dim b As Boolean
        b = Me.cbAudLang.Focus
        b = Me.cbApplyAudToAll.Focus()
        'Debug.WriteLine(b)
    End Sub

End Class
