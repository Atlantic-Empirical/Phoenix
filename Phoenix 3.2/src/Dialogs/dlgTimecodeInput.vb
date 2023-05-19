Imports SMT.Multimedia.Utility.Timecode
Imports SMT.Multimedia.Enums

Public Class dlgTimecodeInput
    Inherits DevExpress.XtraEditors.XtraForm

#Region " Windows Form Designer generated code "

    Private PP As Phoenix_Form
    Public Sub New(ByRef _PP As Phoenix_Form, ByVal MsgToUser As String)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        PP = _PP
        Me.lblMsg.Text = MsgToUser
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
    Friend WithEvents lblMsg As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnOK As DevExpress.XtraEditors.SimpleButton
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.txtTimeSearch_s = New System.Windows.Forms.TextBox
        Me.txtTimeSearch_m = New System.Windows.Forms.TextBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.txtTimeSearch_h = New System.Windows.Forms.TextBox
        Me.Timer = New System.Windows.Forms.Timer(Me.components)
        Me.btnOK = New DevExpress.XtraEditors.SimpleButton
        Me.lblMsg = New DevExpress.XtraEditors.LabelControl
        Me.SuspendLayout()
        '
        'txtTimeSearch_s
        '
        Me.txtTimeSearch_s.Location = New System.Drawing.Point(72, 31)
        Me.txtTimeSearch_s.Name = "txtTimeSearch_s"
        Me.txtTimeSearch_s.Size = New System.Drawing.Size(24, 20)
        Me.txtTimeSearch_s.TabIndex = 2
        Me.txtTimeSearch_s.Text = "s"
        Me.txtTimeSearch_s.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtTimeSearch_m
        '
        Me.txtTimeSearch_m.Location = New System.Drawing.Point(38, 31)
        Me.txtTimeSearch_m.Name = "txtTimeSearch_m"
        Me.txtTimeSearch_m.Size = New System.Drawing.Size(24, 20)
        Me.txtTimeSearch_m.TabIndex = 1
        Me.txtTimeSearch_m.Text = "m"
        Me.txtTimeSearch_m.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label15
        '
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(62, 31)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(8, 17)
        Me.Label15.TabIndex = 21
        Me.Label15.Text = ":"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label14
        '
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(29, 31)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(8, 17)
        Me.Label14.TabIndex = 19
        Me.Label14.Text = ":"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'txtTimeSearch_h
        '
        Me.txtTimeSearch_h.Location = New System.Drawing.Point(5, 31)
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
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(102, 29)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(45, 23)
        Me.btnOK.TabIndex = 27
        Me.btnOK.Text = "OK"
        '
        'lblMsg
        '
        Me.lblMsg.Location = New System.Drawing.Point(5, 12)
        Me.lblMsg.Name = "lblMsg"
        Me.lblMsg.Size = New System.Drawing.Size(8, 13)
        Me.lblMsg.TabIndex = 28
        Me.lblMsg.Text = "[]"
        '
        'dlgTimecodeInput
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        Me.ClientSize = New System.Drawing.Size(152, 57)
        Me.Controls.Add(Me.lblMsg)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.txtTimeSearch_h)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.txtTimeSearch_m)
        Me.Controls.Add(Me.txtTimeSearch_s)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.KeyPreview = True
        Me.Name = "dlgTimecodeInput"
        Me.ShowInTaskbar = False
        Me.Text = " Timecode Input"
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

    Public TIMECODE As cTimecode

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        If Not IsNumeric(Me.txtTimeSearch_h.Text) Then Me.txtTimeSearch_h.Text = 0
        If Not IsNumeric(Me.txtTimeSearch_m.Text) Then Me.txtTimeSearch_m.Text = 0
        If Not IsNumeric(Me.txtTimeSearch_s.Text) Then Me.txtTimeSearch_s.Text = 0
        Me.TIMECODE = New cTimecode(Me.txtTimeSearch_h.Text, Me.txtTimeSearch_m.Text, Me.txtTimeSearch_s.Text, 0, (PP.Player.CurrentVideoStandard = eVideoStandard.NTSC))
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub dlgTimesearchAccelerator_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        If Not DialogResult = DialogResult.OK Then
            DialogResult = DialogResult.Cancel
        End If
    End Sub

    Private Sub txtTimeSearch_h_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTimeSearch_h.TextChanged
        If Me.txtTimeSearch_h.Text = "h" Then Exit Sub
        If IsNumeric(Me.txtTimeSearch_h.Text) Then
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
        If IsNumeric(Me.txtTimeSearch_m.Text) Then
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
        If IsNumeric(Me.txtTimeSearch_s.Text) Then
            If Me.txtTimeSearch_s.Text > 99 Then
                Me.txtTimeSearch_s.Text = ""
                Exit Sub
            End If
        Else
            Me.txtTimeSearch_s.Text = ""
            Exit Sub
        End If
    End Sub

End Class
