Imports SMT.Multimedia.Formats.DVD.IFO
Imports DevExpress.XtraEditors
Imports SMT.Multimedia.Players.DVD.Enums
Imports SMT.DotNet.AppConsole

Public Class dlgLayerbreakSelector
    Inherits DevExpress.XtraEditors.XtraForm

#Region " Windows Form Designer generated code "

    Private PP As Phoenix_Form
    Public Sub New(ByVal nP As Phoenix_Form)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        PP = nP
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
    Friend WithEvents cbProjectIsDualLayer As System.Windows.Forms.CheckBox
    Friend WithEvents cblLayerbreaks As System.Windows.Forms.CheckedListBox
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents cmLayerbreak As System.Windows.Forms.ContextMenu
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.cbProjectIsDualLayer = New System.Windows.Forms.CheckBox
        Me.cblLayerbreaks = New System.Windows.Forms.CheckedListBox
        Me.btnOK = New System.Windows.Forms.Button
        Me.cmLayerbreak = New System.Windows.Forms.ContextMenu
        Me.SuspendLayout()
        '
        'cbProjectIsDualLayer
        '
        Me.cbProjectIsDualLayer.Checked = True
        Me.cbProjectIsDualLayer.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbProjectIsDualLayer.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbProjectIsDualLayer.Location = New System.Drawing.Point(4, 96)
        Me.cbProjectIsDualLayer.Name = "cbProjectIsDualLayer"
        Me.cbProjectIsDualLayer.Size = New System.Drawing.Size(216, 16)
        Me.cbProjectIsDualLayer.TabIndex = 0
        Me.cbProjectIsDualLayer.Text = "Project is dual layer"
        '
        'cblLayerbreaks
        '
        Me.cblLayerbreaks.CheckOnClick = True
        Me.cblLayerbreaks.Location = New System.Drawing.Point(0, 0)
        Me.cblLayerbreaks.Name = "cblLayerbreaks"
        Me.cblLayerbreaks.Size = New System.Drawing.Size(448, 94)
        Me.cblLayerbreaks.TabIndex = 1
        '
        'btnOK
        '
        Me.btnOK.DialogResult = DialogResult.OK
        Me.btnOK.Location = New System.Drawing.Point(368, 96)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.TabIndex = 0
        Me.btnOK.Text = "OK"
        '
        'frmLayerbreakSelector
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(450, 212)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.cblLayerbreaks)
        Me.Controls.Add(Me.cbProjectIsDualLayer)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmLayerbreakSelector"
        Me.ShowInTaskbar = False
        Me.Text = " Select Layerbreak"
        Me.TopMost = True
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub cbProjectIsDualLayer_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbProjectIsDualLayer.CheckedChanged
        If CType(sender, CheckBox).Checked Then
            Me.cblLayerbreaks.Enabled = True
        Else
            Me.cblLayerbreaks.Enabled = False
        End If
    End Sub

    Public Sub SetupLayerbreaks(ByVal LBs As colNonSeamlessCells)
        For i As Short = 0 To LBs.Count - 1
            If LBs(i).CandidateLayerbreak Then
                Me.cblLayerbreaks.Items.Add(LBs.Item(i))
            End If
        Next
        Me.cblLayerbreaks.SetItemChecked(0, True)
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Try
            If Not Me.cbProjectIsDualLayer.Checked Then
                PP.Player.Layerbreak = Nothing
                Exit Sub
            End If

            PP.Player.Layerbreak = CType(Me.cblLayerbreaks.CheckedItems.Item(0), cNonSeamlessCell)
            PP.lblLBCh.Text = CType(Me.cblLayerbreaks.CheckedItems.Item(0), cNonSeamlessCell).PTT
            PP.lblLBTT.Text = CType(Me.cblLayerbreaks.CheckedItems.Item(0), cNonSeamlessCell).GTTn
            PP.lblLBTC.Text = Replace(CType(Me.cblLayerbreaks.CheckedItems.Item(0), cNonSeamlessCell).LBTC, "/ 30fps", "", 1, -1, CompareMethod.Text)
            'NSC.ConfirmedLayerbreak = True
            PP.Player.NonSeamlessCells.LayerbreakConfirmed = True

            ManualClose = True
        Catch ex As Exception
            PP.AddConsoleLine(eConsoleItemType.ERROR, "Problem with selected layerbreak. Error: " & ex.Message)
        End Try
    End Sub

    Private Sub cblLayerbreaks_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cblLayerbreaks.SelectedIndexChanged
        If Me.cblLayerbreaks.CheckedItems.Count > 1 Then
            XtraMessageBox.Show(Me.LookAndFeel, Me, "Please choose only one layerbreak.", My.Settings.APPLICATION_NAME, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            For i As Short = 0 To Me.cblLayerbreaks.Items.Count - 1
                Me.cblLayerbreaks.SetItemChecked(i, False)
            Next
        End If
    End Sub

    Public ManualClose As Boolean = False
    Private Sub frmLayerbreakSelector_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        'If Not ManualClose Then
        '    e.Cancel = True
        'End If
    End Sub

    Private Sub frmLayerbreakSelector_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Left = PP.Left + 30
        Me.Top = PP.Top + 200
    End Sub

End Class
