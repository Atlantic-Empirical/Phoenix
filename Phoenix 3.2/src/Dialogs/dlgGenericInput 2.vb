
Public Class dlgGenericInput

    Public Sub New(ByRef nPP As Phoenix_Form, ByVal Caption As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.Text = Caption
        PP = nPP
    End Sub

    Public USER_INPUT As String
    Private PP As Phoenix_Form

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Me.USER_INPUT = Me.txtEntryField.Text
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub dlgGenericInput_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Not Me.DialogResult = DialogResult.OK Then
            Me.DialogResult = DialogResult.Cancel
        End If
    End Sub

    Private Sub dlgGenericInput_KeyUp(ByVal sender As Object, ByVal e As KeyEventArgs) Handles Me.KeyUp
        If e.KeyCode = Keys.Enter Then
            Me.btnOK_Click(sender, e)
        End If
    End Sub

End Class
