Imports SMT.Multimedia.Formats.DVD.IFO
Imports SMT.Multimedia.Formats.DVD.VOB
Imports SMT.Multimedia.Players.DVD.Enums
Imports SMT.Multimedia.Utility.Timecode
Imports SMT.Multimedia.Enums
Imports SMT.DotNet.AppConsole

Public Class dlgNonSeamlessCells
    Inherits DevExpress.XtraEditors.XtraForm

    Private PP As Phoenix_Form
    Public Sub New(ByVal nPP As Phoenix_Form)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        PP = nPP
    End Sub

    Private Sub dlgNonSeamlessCells_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.lbNSCs.Items.Clear()
            For Each NSC As cNonSeamlessCell In PP.Player.NonSeamlessCells
                Me.lbNSCs.Items.Add(NSC)
            Next
        Catch ex As Exception
            PP.AddConsoleLine(eConsoleItemType.ERROR, "Problem with dlgNonSeamlessCells_Load(). Error: " & ex.Message)
        End Try
    End Sub

    Private Sub dlgNonSeamlessCells_DrawItem(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.ListBoxDrawItemEventArgs) Handles lbNSCs.DrawItem
        Dim lb As DevExpress.XtraEditors.ListBoxControl = CType(sender, DevExpress.XtraEditors.ListBoxControl)
        For i As Short = 0 To PP.Player.NonSeamlessCells.Count - 1
            If PP.Player.NonSeamlessCells(i).ConfirmedLayerbreak Then
                If i = e.Index Then
                    e.Appearance.BackColor = Color.LightBlue
                    Exit Sub
                End If
            End If
            If PP.Player.NonSeamlessCells(i).CandidateLayerbreak Then
                If i = e.Index Then
                    e.Appearance.BackColor = Color.Pink
                    Exit Sub
                End If
            End If
        Next
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Me.Close()
    End Sub

    Private Sub cbConfirmedLayerbreak_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbConfirmedLayerbreak.CheckedChanged
        If Me.cbConfirmedLayerbreak.Checked Then
            If Me.lbNSCs.SelectedIndex >= 0 Or Me.lbNSCs.SelectedIndex <= PP.Player.NonSeamlessCells.Count - 1 Then
                PP.Player.NonSeamlessCells.Item(Me.lbNSCs.SelectedIndex).ConfirmedLayerbreak = True
                PP.Player.NonSeamlessCells.LayerbreakConfirmed = True

                'Dim dlg As dlgTimecodeInput = New dlgTimecodeInput(PP, "Layerbreak source timecode")
                'If dlg.ShowDialog = DialogResult.OK Then
                '    PP.Player.NonSeamlessCells.Item(Me.lbNSCs.SelectedIndex).SourceTimeCode = dlg.TIMECODE
                'End If

            End If
            Me.lbNSCs.Refresh()
        Else
            If Me.lbNSCs.SelectedIndex >= 0 Or Me.lbNSCs.SelectedIndex <= PP.Player.NonSeamlessCells.Count - 1 Then
                PP.Player.NonSeamlessCells.Item(Me.lbNSCs.SelectedIndex).ConfirmedLayerbreak = False
            End If
            Me.lbNSCs.Refresh()
        End If
    End Sub

    Private Sub lbNSCs_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lbNSCs.MouseDoubleClick
        Try
            Dim NSC As cNonSeamlessCell = CType(Me.lbNSCs.SelectedItem, cNonSeamlessCell)
            Debug.WriteLine("NSC time: " & NSC.tcLB.ToString_NoFrames)
            Dim tTC As New cTimecode(NSC.tcLB.Hours, NSC.tcLB.Minutes, NSC.tcLB.Seconds, NSC.tcLB.Frames, PP.Player.CurrentVideoStandard = eVideoStandard.NTSC)
            tTC.Subtract(0, 0, 5, 0)
            PP.Player.PlayAtTimeInTitle(tTC.DVDTimeCode, NSC.GTTn)
        Catch ex As Exception
            PP.AddConsoleLine(eConsoleItemType.ERROR, "Problem with lbNSCs_MouseDoubleClick(). Error: " & ex.Message)
        End Try
    End Sub

    Private Sub llTEST_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llTEST.LinkClicked
        Try
            Dim VOBU_SA As UInt32 = PP.Player.NonSeamlessCells.Item(Me.lbNSCs.SelectedIndex).VOBU_SA
            'vobu_sa now contains the sector offset to the VOBU that ends with the NSC
            'this sector * 2048 = the offset in bytes with byte 0 being the first byte of VTS_0X_1 (x is the current vts number)


        Catch ex As Exception
            MsgBox("Problem with Test(). Error: " & ex.Message)
        End Try
    End Sub

End Class
