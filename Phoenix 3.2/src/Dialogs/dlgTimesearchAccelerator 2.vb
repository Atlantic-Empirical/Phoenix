Imports SMT.Multimedia.DirectShow
Imports SMT.Multimedia.Players.DVD.Enums
Imports SMT.Applications.Phoenix.Enums
Imports DevExpress.XtraEditors
Imports SMT.DotNet.AppConsole

Public Class dlgTimesearchAccelerator
    Inherits DevExpress.XtraEditors.XtraForm

#Region " Windows Form Designer generated code "

    Private PP As Phoenix_Form
    Public Sub New(ByRef _PP As Phoenix_Form)
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
    Friend WithEvents txtTimeSearch_s As System.Windows.Forms.TextBox
    Friend WithEvents txtTimeSearch_m As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtTimeSearch_h As System.Windows.Forms.TextBox
    Friend WithEvents Timer As System.Windows.Forms.Timer
    Friend WithEvents cbTitles As System.Windows.Forms.ComboBox
    Friend WithEvents btnTimeSearchGo As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents Label16 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.txtTimeSearch_s = New System.Windows.Forms.TextBox
        Me.txtTimeSearch_m = New System.Windows.Forms.TextBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.txtTimeSearch_h = New System.Windows.Forms.TextBox
        Me.Timer = New System.Windows.Forms.Timer(Me.components)
        Me.cbTitles = New System.Windows.Forms.ComboBox
        Me.Label16 = New System.Windows.Forms.Label
        Me.btnTimeSearchGo = New DevExpress.XtraEditors.SimpleButton
        Me.SuspendLayout()
        '
        'txtTimeSearch_s
        '
        Me.txtTimeSearch_s.Location = New System.Drawing.Point(71, 9)
        Me.txtTimeSearch_s.Name = "txtTimeSearch_s"
        Me.txtTimeSearch_s.Size = New System.Drawing.Size(24, 20)
        Me.txtTimeSearch_s.TabIndex = 2
        Me.txtTimeSearch_s.Text = "s"
        Me.txtTimeSearch_s.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtTimeSearch_m
        '
        Me.txtTimeSearch_m.Location = New System.Drawing.Point(37, 9)
        Me.txtTimeSearch_m.Name = "txtTimeSearch_m"
        Me.txtTimeSearch_m.Size = New System.Drawing.Size(24, 20)
        Me.txtTimeSearch_m.TabIndex = 1
        Me.txtTimeSearch_m.Text = "m"
        Me.txtTimeSearch_m.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label15
        '
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(61, 9)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(8, 17)
        Me.Label15.TabIndex = 21
        Me.Label15.Text = ":"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label14
        '
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(28, 9)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(8, 17)
        Me.Label14.TabIndex = 19
        Me.Label14.Text = ":"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'txtTimeSearch_h
        '
        Me.txtTimeSearch_h.Location = New System.Drawing.Point(4, 9)
        Me.txtTimeSearch_h.Name = "txtTimeSearch_h"
        Me.txtTimeSearch_h.Size = New System.Drawing.Size(24, 20)
        Me.txtTimeSearch_h.TabIndex = 0
        Me.txtTimeSearch_h.Text = "h"
        Me.txtTimeSearch_h.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Timer
        '
        Me.Timer.Interval = 200
        '
        'cbTitles
        '
        Me.cbTitles.BackColor = System.Drawing.SystemColors.Window
        Me.cbTitles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbTitles.Items.AddRange(New Object() {"1", "2", "3", "4", "5", "6", "7", "8", "10"})
        Me.cbTitles.Location = New System.Drawing.Point(114, 61)
        Me.cbTitles.Name = "cbTitles"
        Me.cbTitles.Size = New System.Drawing.Size(40, 21)
        Me.cbTitles.TabIndex = 3
        Me.cbTitles.TabStop = False
        '
        'Label16
        '
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(94, 61)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(20, 20)
        Me.Label16.TabIndex = 26
        Me.Label16.Text = "TT"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.BottomRight
        '
        'btnTimeSearchGo
        '
        Me.btnTimeSearchGo.Location = New System.Drawing.Point(101, 7)
        Me.btnTimeSearchGo.Name = "btnTimeSearchGo"
        Me.btnTimeSearchGo.Size = New System.Drawing.Size(45, 23)
        Me.btnTimeSearchGo.TabIndex = 27
        Me.btnTimeSearchGo.Text = "Go"
        '
        'dlgTimesearchAccelerator
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        Me.ClientSize = New System.Drawing.Size(151, 33)
        Me.Controls.Add(Me.btnTimeSearchGo)
        Me.Controls.Add(Me.cbTitles)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.txtTimeSearch_h)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.txtTimeSearch_m)
        Me.Controls.Add(Me.txtTimeSearch_s)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.KeyPreview = True
        Me.Name = "dlgTimesearchAccelerator"
        Me.ShowInTaskbar = False
        Me.Text = " Search Accelerator"
        Me.TopMost = True
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private Sub txtTimeSearch_h_GotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTimeSearch_h.GotFocus, txtTimeSearch_m.GotFocus, txtTimeSearch_s.GotFocus
        Dim tb As TextBox = CType(sender, TextBox)
        tb.Text = ""
        Me.ActiveControl = tb
    End Sub

    Private Sub btnTimeSearchGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTimeSearchGo.Click
        Execute()
    End Sub

    Private Sub Execute()
        Try
            If Not IsNumeric(Me.txtTimeSearch_h.Text) Then
                Me.txtTimeSearch_h.Text = "0"
            End If
            If Not IsNumeric(Me.txtTimeSearch_m.Text) Then
                Me.txtTimeSearch_m.Text = "0"
            End If
            If Not IsNumeric(Me.txtTimeSearch_s.Text) Then
                Me.txtTimeSearch_s.Text = "0"
            End If

            Dim TC As DvdTimeCode
            TC.bHours = Me.txtTimeSearch_h.Text
            TC.bMinutes = Me.txtTimeSearch_m.Text
            TC.bSeconds = Me.txtTimeSearch_s.Text
            TC.bFrames = 1

            If Not PP.Player.PlayAtTime(TC, False) Then
                AcceptKeys = False
                XtraMessageBox.Show(Me.LookAndFeel, Me, "Invalid time/title search value(s).", My.Settings.APPLICATION_NAME, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Me.txtTimeSearch_h.Text = ""
                Me.txtTimeSearch_m.Text = ""
                Me.txtTimeSearch_s.Text = ""
                Me.txtTimeSearch_h.Focus()
                Me.cbTitles.SelectedIndex = 0
                NextKeySwitchToAccept = True
                Exit Sub
            End If
            Debug.WriteLine("Unpaused")
            PP.KH.UnpauseHook()
            DialogResult = DialogResult.OK
        Catch ex As Exception
            PP.AddConsoleLine(eConsoleItemType.ERROR, "problem with go time search . error: " & ex.Message)
        End Try
    End Sub

    Private NextKeySwitchToAccept As Boolean = False

    Private Sub dlgPlayerDefaults_KeyUp(ByVal sender As Object, ByVal e As KeyEventArgs) Handles MyBase.KeyUp
        If Not AcceptKeys Then
            If NextKeySwitchToAccept Then
                AcceptKeys = True
            End If
            Exit Sub
        End If
        If e.KeyData = Keys.Enter Then
            Execute()
        End If
        If e.KeyData = Keys.Escape Then
            DialogResult = DialogResult.Cancel
        End If
    End Sub

    Private AcceptKeys As Boolean = False
    Private Sub Timer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer.Tick
        AcceptKeys = True
        Timer.Stop()
    End Sub

    Private Sub dlgTimesearchAccelerator_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        If Not DialogResult = DialogResult.OK Then
            DialogResult = DialogResult.Cancel
        End If
        PP.KH.UnpauseHook()
    End Sub

    Private Sub dlgTimesearchAccelerator_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If PP.Player.CurrentUserOperations(0) Then
            Me.cbTitles.Items.Clear()

            If PP.Player.CurrentUserOperations(2) Then
                For s As Short = 1 To PP.Player.CurrentDVD.VMGM.GlobalTTs.Count
                    Me.cbTitles.Items.Add(s)
                Next
                If PP.Player.CurrentDomain = DvdDomain.Title Then
                    cbTitles.SelectedIndex = cbTitles.FindStringExact(PP.Player.CurrentTitle)
                Else
                    cbTitles.SelectedIndex = 0
                End If

                Me.Top = PP.Top + 50
                Me.Left = PP.Left + 150

            Else
                Me.cbTitles.Items.Add(PP.lblEXDB_CurrentTitle.Text)
                Me.cbTitles.Text = PP.lblEXDB_CurrentTitle.Text
                Me.cbTitles.Enabled = False
            End If

            Me.txtTimeSearch_h.Text = "h"
            Me.txtTimeSearch_m.Text = "m"
            Me.txtTimeSearch_s.Text = "s"

            'If PP.Player.LastTimeSearch.bHours = Nothing Then
            '    Me.txtTimeSearch_h.Text = "h"
            '    Me.txtTimeSearch_m.Text = "m"
            '    Me.txtTimeSearch_s.Text = "s"
            'Else
            '    Me.txtTimeSearch_h.Text = PP.Player.LastTimeSearch.bHours
            '    Me.txtTimeSearch_m.Text = PP.Player.LastTimeSearch.bMinutes
            '    Me.txtTimeSearch_s.Text = PP.Player.LastTimeSearch.bSeconds
            '    Me.btnTimeSearchGo.TabIndex = 0
            '    Me.txtTimeSearch_h.TabIndex = 1
            '    Me.txtTimeSearch_m.TabIndex = 2
            '    Me.txtTimeSearch_s.TabIndex = 3
            '    ActiveControl = Me.btnTimeSearchGo
            '    Me.btnTimeSearchGo.Focus()
            'End If

            PP.KH.PauseHook()
            Timer.Start()
        Else
            If XtraMessageBox.Show(Me.LookAndFeel, Me, "Time/Title Search is prohibited by UOP 0.", My.Settings.APPLICATION_NAME, MessageBoxButtons.OK, MessageBoxIcon.Exclamation) = MsgBoxResult.Ok Then
                Me.Close()
            End If
        End If
    End Sub

    Private Sub txtTimeSearch_h_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTimeSearch_h.TextChanged
        If Me.txtTimeSearch_h.Text = "h" Then Exit Sub
        If IsNumeric(Me.txtTimeSearch_h.Text) AndAlso Me.txtTimeSearch_h.Text >= 0 Then
            If Me.txtTimeSearch_h.Text > 99 Then
                Me.txtTimeSearch_h.Text = ""
                Exit Sub
            End If
            If Me.txtTimeSearch_h.Text.Length = 2 Then
                Me.txtTimeSearch_m.Focus()
            End If
        Else
            Me.txtTimeSearch_h.Text = ""
            Exit Sub
        End If
    End Sub

    Private Sub txtTimeSearch_m_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTimeSearch_m.TextChanged
        If Me.txtTimeSearch_m.Text = "m" Then Exit Sub
        If IsNumeric(Me.txtTimeSearch_m.Text) AndAlso Me.txtTimeSearch_m.Text >= 0 Then
            If Me.txtTimeSearch_m.Text > 99 Then
                Me.txtTimeSearch_m.Text = ""
                Exit Sub
            End If
            If Me.txtTimeSearch_m.Text.Length = 2 Then
                Me.txtTimeSearch_s.Focus()
            End If
        Else
            Me.txtTimeSearch_m.Text = ""
            Exit Sub
        End If
    End Sub

    Private Sub txtTimeSearch_s_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTimeSearch_s.TextChanged
        If Me.txtTimeSearch_s.Text = "s" Then Exit Sub
        If IsNumeric(Me.txtTimeSearch_s.Text) AndAlso Me.txtTimeSearch_s.Text >= 0 Then
            If Me.txtTimeSearch_s.Text > 99 Then
                Me.txtTimeSearch_s.Text = ""
                Exit Sub
            End If
            If Me.txtTimeSearch_s.Text.Length = 2 Then
                Me.cbTitles.Focus()
            End If
        Else
            Me.txtTimeSearch_s.Text = ""
            Exit Sub
        End If
    End Sub

End Class
