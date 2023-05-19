Imports SMT.DotNet.Reflection
Imports SMT.Multimedia.Players.DVD.Enums
Imports SMT.DotNet.AppConsole

Public Class dlgImageMounting
    Inherits DevExpress.XtraEditors.XtraForm

    Private PP As Phoenix_Form
    Public Sub New(ByRef nPP As Phoenix_Form)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        PP = nPP
    End Sub

    Private Sub btnRunUtility_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRunUtility.Click
        Try
            Process.Start(GetExePath() & "SMT_IMMT.exe")
        Catch ex As Exception
            PP.AddConsoleLine(eConsoleItemType.ERROR, "Problem with start image mount tool. Error: " & ex.Message, Nothing)
        Finally
            Me.Close()
        End Try
    End Sub

End Class