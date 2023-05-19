
Public Module mNavUpgrader

    Public Sub InstallNav_XP()
        Try
            Dim CommonFilesPath As String = Environment.GetFolderPath(Environment.SpecialFolder.CommonProgramFiles)
            Dim InstallerPath As String = CommonFilesPath & "\SMT Shared\Installation\DVDRuntime_XP.exe"
            Process.Start(InstallerPath)
        Catch ex As Exception
            'Throw New Exception("Problem with InstallNav_XP(). Error: " & ex.Message, ex)
            Debug.Write("Problem with InstallNav_XP(). Error: " & ex.Message)
        End Try
    End Sub

    Public Sub InstallNav_Vista()
        Try
            Dim CommonFilesPath As String = Environment.GetFolderPath(Environment.SpecialFolder.CommonProgramFiles)
            Dim InstallerPath As String = CommonFilesPath & "\SMT Shared\Installation\DVDRuntime_Vista.exe"
            Process.Start(InstallerPath)
        Catch ex As Exception
            'Throw New Exception("Problem with InstallNav_Vista(). Error: " & ex.Message, ex)
            Debug.WriteLine("Problem with InstallNav_Vista(). Error: " & ex.Message)
        End Try
    End Sub

End Module
