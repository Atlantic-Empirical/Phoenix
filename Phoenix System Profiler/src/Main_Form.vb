Imports SMT.Multimedia.GraphConstruction
Imports System.IO

Public Class Main_Form

    Private ReadOnly Property PROGRAM_FILES() As String
        Get
            Return Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) & "\"
        End Get
    End Property

    Private ReadOnly Property COMMON_FILES() As String
        Get
            Return Environment.GetFolderPath(Environment.SpecialFolder.CommonProgramFiles) & "\"
        End Get
    End Property

    Private ReadOnly Property SYSTEM_FILES() As String
        Get
            Return Environment.GetFolderPath(Environment.SpecialFolder.System) & "\"
        End Get
    End Property

    Private Sub btnTest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTest.Click
        TestFilters()
        FileVersions()
        MSRuntimes()
        ProgramDir()
    End Sub

    Private Sub TestFilters()
        Try
            Dim GB As New cSMTGraph(Me.Handle)

            Try
                If GB.AddAMTC Then
                    Me.lblAMTC.Text = "True"
                    Me.lblAMTC.ForeColor = Color.Black
                Else
                    Me.lblAMTC.Text = "False"
                    Me.lblAMTC.ForeColor = Color.Red
                End If
            Catch ex As Exception
                Me.lblAMTC.Text = "False"
                Me.lblAMTC.ForeColor = Color.Red
            End Try

            Try
                If GB.AddNVidiaAudioDecoder(False) Then
                    Me.lblAudDec.Text = "True"
                    Me.lblAudDec.ForeColor = Color.Black
                Else
                    Me.lblAudDec.Text = "False"
                    Me.lblAudDec.ForeColor = Color.Red
                End If
            Catch ex As Exception
                Me.lblAudDec.Text = "False"
                Me.lblAudDec.ForeColor = Color.Red
            End Try

            Try
                If GB.AddNVidiaVideoDecoder(False, False) Then
                    Me.lblVidDec.Text = "True"
                    Me.lblVidDec.ForeColor = Color.Black
                Else
                    Me.lblVidDec.Text = "True"
                    Me.lblVidDec.ForeColor = Color.Red
                End If
            Catch ex As Exception
                Me.lblVidDec.Text = "True"
                Me.lblVidDec.ForeColor = Color.Red
            End Try

            Try
                If GB.AddDeckLinkAudio Then
                    Me.lblDLA.Text = "True"
                    Me.lblDLA.ForeColor = Color.Black
                Else
                    Me.lblDLA.Text = "False"
                    Me.lblDLA.ForeColor = Color.Red
                End If
            Catch ex As Exception
                Me.lblDLA.Text = "False"
                Me.lblDLA.ForeColor = Color.Red
            End Try

            Try
                If GB.AddDeckLinkVideo Then
                    Me.lblDLV.Text = "True"
                    Me.lblDLV.ForeColor = Color.Black
                Else
                    Me.lblDLV.Text = "False"
                    Me.lblDLV.ForeColor = Color.Red
                End If
            Catch ex As Exception
                Me.lblDLV.Text = "False"
                Me.lblDLV.ForeColor = Color.Red
            End Try

            Try
                If GB.AddKeystoneOmni() Then
                    Me.lblKeystone.Text = "True"
                    Me.lblKeystone.ForeColor = Color.Black
                Else
                    Me.lblKeystone.Text = "True"
                    Me.lblKeystone.ForeColor = Color.Red
                End If
            Catch ex As Exception
                Me.lblKeystone.Text = "True"
                Me.lblKeystone.ForeColor = Color.Red
            End Try

            Try
                If GB.AddL21G Then
                    Me.lblL21G.Text = "True"
                    Me.lblL21G.ForeColor = Color.Black
                Else
                    Me.lblL21G.Text = "False"
                    Me.lblL21G.ForeColor = Color.Red
                End If
            Catch ex As Exception
                Me.lblL21G.Text = "False"
                Me.lblL21G.ForeColor = Color.Red
            End Try

            Try
                If GB.AddDVDNav Then
                    Me.lblNavigator.Text = "True"
                    Me.lblNavigator.ForeColor = Color.Black
                Else
                    Me.lblNavigator.Text = "False"
                    Me.lblNavigator.ForeColor = Color.Red
                End If
            Catch ex As Exception
                Me.lblNavigator.Text = "False"
                Me.lblNavigator.ForeColor = Color.Red
            End Try

            If File.Exists(COMMON_FILES & "SMT Shared\Filters\dac.ax") Then
                Me.lblDAC.Text = "True"
                Me.lblDAC.ForeColor = Color.Black
            Else
                Me.lblDAC.Text = "False"
                Me.lblDAC.ForeColor = Color.Red
            End If

            Try
                If GB.AddNullVideoSource Then
                    Me.lblNVS.Text = "True"
                    Me.lblNVS.ForeColor = Color.Black
                Else
                    Me.lblNVS.Text = "False"
                    Me.lblNVS.ForeColor = Color.Red
                End If
            Catch ex As Exception
                Me.lblNVS.Text = "False"
                Me.lblNVS.ForeColor = Color.Red
            End Try

            Try
                If GB.AddMCE_MP2A Then
                    Me.lblMCMPGDEC.Text = "True"
                    Me.lblMCMPGDEC.ForeColor = Color.Black
                Else
                    Me.lblMCMPGDEC.Text = "False"
                    Me.lblMCMPGDEC.ForeColor = Color.Red
                End If
            Catch ex As Exception
                Me.lblMCMPGDEC.Text = "False"
                Me.lblMCMPGDEC.ForeColor = Color.Red
            End Try

            Try
                If GB.AddMCE_DMXA Then
                    Me.lblMCMPGDMX.Text = "True"
                    Me.lblMCMPGDMX.ForeColor = Color.Black
                Else
                    Me.lblMCMPGDMX.Text = "False"
                    Me.lblMCMPGDMX.ForeColor = Color.Red
                End If
            Catch ex As Exception
                Me.lblMCMPGDMX.Text = "False"
                Me.lblMCMPGDMX.ForeColor = Color.Red
            End Try

            Try
                If GB.AddMCE_ImgSiz Then
                    Me.lblMCE_IMZ.Text = "True"
                    Me.lblMCE_IMZ.ForeColor = Color.Black
                Else
                    Me.lblMCE_IMZ.Text = "False"
                    Me.lblMCE_IMZ.ForeColor = Color.Red
                End If
            Catch ex As Exception
                Me.lblMCE_IMZ.Text = "False"
                Me.lblMCE_IMZ.ForeColor = Color.Red
            End Try


            'GB.DestroyGraph()
            'GB = Nothing
        Catch ex As Exception
            MsgBox("Problem in TestFilters(). Error: " & ex.Message)
        End Try
    End Sub

    Private Sub FileVersions()
        Try
            Dim FVI As FileVersionInfo
            If File.Exists(COMMON_FILES & "SMT Shared\Filters\keystone_omni.ax") Then
                FVI = FileVersionInfo.GetVersionInfo(COMMON_FILES & "SMT Shared\Filters\keystone_omni.ax")
                Me.lblKeystoneVersion.Text = FVI.FileVersion
                Me.lblKeystoneVersion.ForeColor = Color.Black
                'If FVI.FileVersion <> "1.1.2.0" Then
                If FVI.FileMajorPart = 2 And FVI.FileMinorPart = 0 Then

                Else
                    Me.lblKeystoneVersion.ForeColor = Color.Red
                End If
            Else
                Me.lblKeystoneVersion.Text = "NOT PRESENT"
                Me.lblKeystoneVersion.ForeColor = Color.Red
            End If

            If File.Exists(PROGRAM_FILES & "Blackmagic Design\Blackmagic DeckLink\decklink.dll") Then
                FVI = FileVersionInfo.GetVersionInfo(PROGRAM_FILES & "Blackmagic Design\Blackmagic DeckLink\decklink.dll")
                Me.lblDecklinkVersion.Text = FVI.ProductVersion
                Me.lblDecklinkVersion.ForeColor = Color.Black
                If FVI.ProductVersion <> "6.4" Then
                    Me.lblDecklinkVersion.ForeColor = Color.Red
                End If
            Else
                Me.lblDecklinkVersion.Text = "NOT INSTALLED"
                Me.lblDecklinkVersion.ForeColor = Color.Red
            End If
            If File.Exists(SYSTEM_FILES & "qdvd.dll") Then
                FVI = FileVersionInfo.GetVersionInfo(SYSTEM_FILES & "qdvd.dll")
                If FVI.ProductVersion <> "6.05.2600.2831" Then
                    Me.lblNavVersion.Text = "WRONG VERSION"
                    Me.lblNavVersion.ForeColor = Color.Red
                Else
                    Me.lblNavVersion.Text = "OK"
                    Me.lblNavVersion.ForeColor = Color.Black
                End If
            Else
                Me.lblNavVersion.Text = "NOT PRESENT"
                Me.lblNavVersion.ForeColor = Color.Red
            End If
        Catch ex As Exception
            MsgBox("Problem with FileVersions(). Error: " & ex.Message)
        End Try
    End Sub

    Private Sub MSRuntimes()
        Try
            If File.Exists(SYSTEM_FILES & "MSVCP71.dll") Then
                Me.lblMSVCP71.Text = "True"
                Me.lblMSVCP71.ForeColor = Color.Black
            Else
                Me.lblMSVCP71.Text = "False"
                Me.lblMSVCP71.ForeColor = Color.Red
            End If

            If File.Exists(SYSTEM_FILES & "MSVCR71.dll") Then
                Me.lblMSVCR71.Text = "True"
                Me.lblMSVCR71.ForeColor = Color.Black
            Else
                Me.lblMSVCR71.Text = "False"
                Me.lblMSVCR71.ForeColor = Color.Red
            End If

            If File.Exists(SYSTEM_FILES & "MSVCRT.dll") Then
                Me.lblMSVCRT.Text = "True"
                Me.lblMSVCRT.ForeColor = Color.Black
            Else
                Me.lblMSVCRT.Text = "False"
                Me.lblMSVCRT.ForeColor = Color.Red
            End If

            Me.lblMSVCR80.Text = "True" 'this app wouldn't run if the dll weren't where expected (in the SxS dir) "C:\WINDOWS\WinSxS\x86_Microsoft.VC80.CRT_1fc8b3b9a1e18e3b_8.0.50727.1378_x-ww_5c7e3652" on this machine.

        Catch ex As Exception
            MsgBox("Problem with MSRuntimes(). Error: " & ex.Message)
        End Try
    End Sub

    Private Sub ProgramDir()
        Try
            Dim PG As String = PROGRAM_FILES & "SMT\Phoenix\"

            If File.Exists(PG & "DevExpress.Data.v7.2.dll") Then
                Me.lblDevExpress.Text = "True"
                Me.lblDevExpress.ForeColor = Color.Black
            Else
                Me.lblDevExpress.Text = "False"
                Me.lblDevExpress.ForeColor = Color.Red
            End If

            If File.Exists(PG & "ModuleConfig.dll") Then
                Me.lblModuleConfig.Text = "True"
                Me.lblModuleConfig.ForeColor = Color.Black
            Else
                Me.lblModuleConfig.Text = "False"
                Me.lblModuleConfig.ForeColor = Color.Red
            End If

            If File.Exists(PG & "NvSharedLib.dll") Then
                Me.lblNvSharedLib.Text = "True"
                Me.lblNvSharedLib.ForeColor = Color.Black
            Else
                Me.lblNvSharedLib.Text = "False"
                Me.lblNvSharedLib.ForeColor = Color.Red
            End If

            If File.Exists(PG & "SecureUpdate.dll") Then
                Me.lblSecureUpdate.Text = "True"
                Me.lblSecureUpdate.ForeColor = Color.Black
            Else
                Me.lblSecureUpdate.Text = "False"
                Me.lblSecureUpdate.ForeColor = Color.Red
            End If

            If File.Exists(PG & "SentinelKeyW.dll") Then
                Me.lblSentinelKeyW.Text = "True"
                Me.lblSentinelKeyW.ForeColor = Color.Black
            Else
                Me.lblSentinelKeyW.Text = "False"
                Me.lblSentinelKeyW.ForeColor = Color.Red
            End If

            If File.Exists(PG & "Phoenix.exe") Then
                Me.lblPhoenix.Text = "True"
                Me.lblPhoenix.ForeColor = Color.Black
            Else
                Me.lblPhoenix.Text = "False"
                Me.lblPhoenix.ForeColor = Color.Red
            End If

            If File.Exists(PG & "SMT.Common.dll") Then
                Me.lblSMTCommon.Text = "True"
                Me.lblSMTCommon.ForeColor = Color.Black
            Else
                Me.lblSMTCommon.Text = "False"
                Me.lblSMTCommon.ForeColor = Color.Red
            End If

            If File.Exists(PG & "SMT.MediaResources.dll") Then
                Me.lblMediaResources.Text = "True"
                Me.lblMediaResources.ForeColor = Color.Black
            Else
                Me.lblMediaResources.Text = "False"
                Me.lblMediaResources.ForeColor = Color.Red
            End If

            If File.Exists(PG & "SMT_IMMT.exe") Then
                Me.lblIMMT.Text = "True"
                Me.lblIMMT.ForeColor = Color.Black
            Else
                Me.lblIMMT.Text = "False"
                Me.lblIMMT.ForeColor = Color.Red
            End If

            If File.Exists(PG & "Win32Security.dll") Then
                Me.lblWin32Sec.Text = "True"
                Me.lblWin32Sec.ForeColor = Color.Black
            Else
                Me.lblWin32Sec.Text = "False"
                Me.lblWin32Sec.ForeColor = Color.Red
            End If

        Catch ex As Exception
            MsgBox("Problem with ProgramDir(). Error: " & ex.Message)
        End Try
    End Sub

End Class
