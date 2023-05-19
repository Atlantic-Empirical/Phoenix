Imports SMT.Multimedia.Players.DVD.Enums
Imports SMT.Applications.Phoenix.Enums
Imports DevExpress.XtraEditors
Imports SMT.DotNet.AppConsole

Public Class dlgCHTTSearchAccelerator
    Inherits DevExpress.XtraEditors.XtraForm

#Region " Windows Form Designer generated code "

    Private PP As Phoenix_Form
    Private Type As eSearchTypes
    Public Sub New(ByRef _PP As Phoenix_Form, ByVal nSearchType As eSearchTypes)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        PP = _PP
        Me.Type = nSearchType
        Me.txtCH.Text = ""
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
    Friend WithEvents Timer As System.Windows.Forms.Timer
    Friend WithEvents btnGo As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents txtCH As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.txtCH = New System.Windows.Forms.TextBox
        Me.Timer = New System.Windows.Forms.Timer(Me.components)
        Me.btnGo = New DevExpress.XtraEditors.SimpleButton
        Me.SuspendLayout()
        '
        'txtCH
        '
        Me.txtCH.Enabled = False
        Me.txtCH.Location = New System.Drawing.Point(8, 9)
        Me.txtCH.Name = "txtCH"
        Me.txtCH.Size = New System.Drawing.Size(24, 20)
        Me.txtCH.TabIndex = 1
        Me.txtCH.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Timer
        '
        Me.Timer.Interval = 180
        '
        'btnGo
        '
        Me.btnGo.Location = New System.Drawing.Point(36, 7)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(45, 23)
        Me.btnGo.TabIndex = 28
        Me.btnGo.Text = "Go"
        '
        'dlgCHTTSearchAccelerator
        '
        Me.AcceptButton = Me.btnGo
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        Me.ClientSize = New System.Drawing.Size(84, 34)
        Me.Controls.Add(Me.btnGo)
        Me.Controls.Add(Me.txtCH)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.KeyPreview = True
        Me.Name = "dlgCHTTSearchAccelerator"
        Me.ShowInTaskbar = False
        Me.Text = "[]"
        Me.TopMost = True
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private MSGDisplayed As Boolean = False
    'Private AcceptKeys As Boolean = False
    Private NextKeySwitchToAccept As Boolean = False
    Private KeyIsDown As Boolean = False

    Private Sub dlgTimesearchAccelerator_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Select Case Me.Type
            Case eSearchTypes.Chapter
                Me.Text = "CH Search"
                If Not PP.Player.CurrentUserOperations(1) Then
                    If XtraMessageBox.Show(Me.LookAndFeel, Me, "Chapter seach UOP is prohibited.", My.Settings.APPLICATION_NAME, MessageBoxButtons.OK, MessageBoxIcon.Exclamation) Then
                        Me.DialogResult = DialogResult.OK
                        Me.Close()
                        Exit Sub
                    End If
                End If
            Case eSearchTypes.Title
                Me.Text = "TT Search"
                If Not PP.Player.CurrentUserOperations(2) Then
                    If XtraMessageBox.Show(Me.LookAndFeel, Me, "Title seach UOP is prohibited.", My.Settings.APPLICATION_NAME, MessageBoxButtons.OK, MessageBoxIcon.Exclamation) = DialogResult.OK Then
                        Me.DialogResult = DialogResult.OK
                        Me.Close()
                        Exit Sub
                    End If
                End If
            Case eSearchTypes.SubtitleStream
                Me.Text = "Sub Stream"
                If Not PP.Player.CurrentUserOperations(21) Then
                    If XtraMessageBox.Show(Me.LookAndFeel, Me, "Subtitle change UOP (21) is prohibited.", My.Settings.APPLICATION_NAME, MessageBoxButtons.OK, MessageBoxIcon.Exclamation) = DialogResult.OK Then
                        Me.DialogResult = DialogResult.OK
                        Me.Close()
                        Exit Sub
                    End If
                End If
            Case eSearchTypes.AudioStream
                Me.Text = "Audio Stream"
                If Not PP.Player.CurrentUserOperations(20) Then
                    If XtraMessageBox.Show(Me.LookAndFeel, Me, "Audio change UOP (20) is prohibited.", My.Settings.APPLICATION_NAME, MessageBoxButtons.OK, MessageBoxIcon.Exclamation) = DialogResult.OK Then
                        Me.DialogResult = DialogResult.OK
                        Me.Close()
                        Exit Sub
                    End If
                End If
        End Select

        Me.Left = PP.Left
        Me.Top = PP.Top
        PP.KH.PauseHook()
        Timer.Start()
    End Sub

    Private Sub dlgTimesearchAccelerator_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        If Not DialogResult = DialogResult.OK Then
            DialogResult = DialogResult.Cancel
        End If
        PP.KH.UnpauseHook()
    End Sub

    Private Sub btnTimeSearchGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGo.Click
        Execute()
    End Sub

    Private Sub dlgPlayerDefaults_KeyUp(ByVal sender As Object, ByVal e As KeyEventArgs) Handles MyBase.KeyUp
        If Not KeyIsDown Then Exit Sub
        If MSGDisplayed Then Exit Sub
        'If Not AcceptKeys Then
        '    If NextKeySwitchToAccept Then
        '        AcceptKeys = True
        '    End If
        '    Exit Sub
        'End If
        Select Case e.KeyData
            Case Keys.Escape
                DialogResult = DialogResult.Cancel
                Me.Close()
            Case Keys.Enter
                Execute()
                'Case Keys.D0, Keys.D1, Keys.D2, Keys.D3, Keys.D4, Keys.D5, Keys.D6, Keys.D7, Keys.D8, Keys.D9, Keys.NumPad0, Keys.NumPad1, Keys.NumPad2, Keys.NumPad3, Keys.NumPad4, Keys.NumPad5, Keys.NumPad6, Keys.NumPad7, Keys.NumPad8, Keys.NumPad9

                'Case Else
                '    e.Handled = True
                '    Me.txtCH.Text = ""
                '    Exit Sub
        End Select
    End Sub

    Private Sub Timer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer.Tick
        ' AcceptKeys = True
        Me.txtCH.Text = ""
        Me.txtCH.Enabled = True
        txtCH.Focus()
        Timer.Stop()
    End Sub

    Private Sub Execute()
        Me.txtCH.Text = Me.txtCH.Text.Trim

        If Not IsNumeric(Me.txtCH.Text) Then
            Me.txtCH.Text = ""
            Beep()
            Exit Sub
        End If

        Select Case Me.Type
            Case eSearchTypes.Chapter
                If Not IsNumeric(Me.txtCH.Text) Then GoTo chInvalid
                If Me.txtCH.Text < 1 Then GoTo CHInvalid
                If Me.txtCH.Text > PP.Player.ChaptersInCurrentTitle Then GoTo chInvalid
                GoTo CHValid
CHInvalid:
                MSGDisplayed = True

                If XtraMessageBox.Show(Me.LookAndFeel, Me, "Invalid value. Try again?", My.Settings.APPLICATION_NAME, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = MsgBoxResult.Yes Then
                    Me.txtCH.Text = ""
                    Me.txtCH.Focus()
                    Exit Sub
                Else
                    Me.Close()
                    Exit Sub
                End If
                MSGDisplayed = False
CHValid:
                Try
                    If CByte(Me.txtCH.Text) > PP.Player.ChaptersInCurrentTitle Then
                        XtraMessageBox.Show(Me.LookAndFeel, Me, "Chapter " & Me.txtCH.Text & " is not available in current title.", My.Settings.APPLICATION_NAME, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        DialogResult = DialogResult.Abort
                    Else
                        PP.Player.PlayChapter(Me.txtCH.Text, False)
                        DialogResult = DialogResult.OK
                    End If
                Catch ex As Exception
                    PP.AddConsoleLine(eConsoleItemType.ERROR, "problem with chapter search. error: " & ex.Message)
                Finally
                    PP.KH.UnpauseHook()
                    Me.Close()
                End Try

            Case eSearchTypes.Title
                If Not IsNumeric(Me.txtCH.Text) Then GoTo ttInvalid
                If Me.txtCH.Text < 1 Then GoTo CHInvalid
                If Me.txtCH.Text > PP.Player.GlobalTTCount Then GoTo ttInvalid
                GoTo ttValid
TTInvalid:
                MSGDisplayed = True
                If XtraMessageBox.Show(Me.LookAndFeel, Me, "Invalid value. Try again?", My.Settings.APPLICATION_NAME, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = MsgBoxResult.Yes Then
                    Me.txtCH.Text = ""
                    Me.txtCH.Focus()
                    Exit Sub
                Else
                    Me.Close()
                    Exit Sub
                End If
                MSGDisplayed = False
TTValid:
                Try
                    If CByte(Me.txtCH.Text) > PP.Player.GlobalTTCount Then
                        XtraMessageBox.Show(Me.LookAndFeel, Me, "Invalid title. There are " & PP.Player.GlobalTTCount & " titles in the current project.", My.Settings.APPLICATION_NAME, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        DialogResult = DialogResult.Abort
                    Else
                        PP.Player.PlayTitle(Me.txtCH.Text)
                        DialogResult = DialogResult.OK
                    End If
                Catch ex As Exception
                    PP.AddConsoleLine(eConsoleItemType.ERROR, "Problem with Execute(). Error: " & ex.Message)
                Finally
                    PP.KH.UnpauseHook()
                    Me.Close()
                End Try

            Case eSearchTypes.SubtitleStream
                Try
                    If CByte(Me.txtCH.Text) > PP.Player.SubtitleStreamCount OrElse Not PP.Player.IsSubStreamEnabled(Me.txtCH.Text) Then
                        XtraMessageBox.Show(Me.LookAndFeel, Me, "Invalid stream. Either the requested stream is not enabled in the current title or is greater than the number of streams.", My.Settings.APPLICATION_NAME, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        DialogResult = DialogResult.Abort
                    Else
                        PP.Player.SetSubtitleStream(Me.txtCH.Text, False, True)
                        DialogResult = DialogResult.OK
                    End If
                Catch ex As Exception
                    PP.AddConsoleLine(eConsoleItemType.ERROR, "Problem with Execute(). Error: " & ex.Message)
                Finally
                    PP.KH.UnpauseHook()
                    Me.Close()
                End Try

            Case eSearchTypes.AudioStream
                Try
                    If CByte(Me.txtCH.Text) > PP.Player.AudioStreamCount Then
                        XtraMessageBox.Show(Me.LookAndFeel, Me, "Invalid stream. There are " & PP.Player.AudioStreamCount & " audio streams in the current title.", My.Settings.APPLICATION_NAME, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        DialogResult = DialogResult.Abort
                    Else
                        PP.Player.SetAudioStream(Me.txtCH.Text)
                        DialogResult = DialogResult.OK
                    End If
                Catch ex As Exception
                    PP.AddConsoleLine(eConsoleItemType.ERROR, "Problem with Execute(). Error: " & ex.Message)
                Finally
                    PP.KH.UnpauseHook()
                    Me.Close()
                End Try

        End Select
    End Sub

    'Private Sub txtTimeSearch_s_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGo.Click
    '    If IsNumeric(Me.txtCH.Text) Then
    '        If Me.txtCH.Text > 99 Then
    '            Me.txtCH.Text = ""
    '            Exit Sub
    '        End If
    '        If Me.txtCH.Text.Length = 2 Then
    '            Me.btnGo.Focus()
    '        End If
    '    Else
    '        Me.txtCH.Text = ""
    '        Exit Sub
    '    End If
    'End Sub

End Class
