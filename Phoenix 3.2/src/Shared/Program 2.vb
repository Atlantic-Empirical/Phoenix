Imports System.IO

Friend Class Program

    Private Sub New()
    End Sub

    <STAThread()> _
    Shared Sub Main()
        DevExpress.UserSkins.OfficeSkins.Register()
        DevExpress.Skins.SkinManager.EnableFormSkins()
        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)
        Application.Run(New Phoenix_Form())
    End Sub

End Class
