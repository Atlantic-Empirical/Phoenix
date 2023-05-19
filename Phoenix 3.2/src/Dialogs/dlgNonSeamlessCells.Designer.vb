<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgNonSeamlessCells

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim SuperToolTip1 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(dlgNonSeamlessCells))
        Me.lbNSCs = New DevExpress.XtraEditors.ListBoxControl
        Me.cbConfirmedLayerbreak = New DevExpress.XtraEditors.CheckEdit
        Me.btnOK = New DevExpress.XtraEditors.SimpleButton
        Me.llTEST = New System.Windows.Forms.LinkLabel
        CType(Me.lbNSCs, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbConfirmedLayerbreak.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lbNSCs
        '
        Me.lbNSCs.Location = New System.Drawing.Point(12, 12)
        Me.lbNSCs.Name = "lbNSCs"
        Me.lbNSCs.Size = New System.Drawing.Size(605, 274)
        Me.lbNSCs.SuperTip = SuperToolTip1
        Me.lbNSCs.TabIndex = 0
        '
        'cbConfirmedLayerbreak
        '
        Me.cbConfirmedLayerbreak.Location = New System.Drawing.Point(13, 295)
        Me.cbConfirmedLayerbreak.Name = "cbConfirmedLayerbreak"
        Me.cbConfirmedLayerbreak.Properties.Caption = "Confirmed Layerbreak"
        Me.cbConfirmedLayerbreak.Size = New System.Drawing.Size(125, 18)
        Me.cbConfirmedLayerbreak.TabIndex = 1
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(542, 295)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 23)
        Me.btnOK.TabIndex = 2
        Me.btnOK.Text = "OK"
        '
        'llTEST
        '
        Me.llTEST.AutoSize = True
        Me.llTEST.Location = New System.Drawing.Point(446, 304)
        Me.llTEST.Name = "llTEST"
        Me.llTEST.Size = New System.Drawing.Size(31, 13)
        Me.llTEST.TabIndex = 3
        Me.llTEST.TabStop = True
        Me.llTEST.Text = "TEST"
        Me.llTEST.Visible = False
        '
        'dlgNonSeamlessCells
        '
        Me.ClientSize = New System.Drawing.Size(629, 325)
        Me.Controls.Add(Me.llTEST)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.cbConfirmedLayerbreak)
        Me.Controls.Add(Me.lbNSCs)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "dlgNonSeamlessCells"
        Me.ShowIcon = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.Text = "Non-Seamless Cells"
        CType(Me.lbNSCs, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbConfirmedLayerbreak.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lbNSCs As DevExpress.XtraEditors.ListBoxControl
    Friend WithEvents cbConfirmedLayerbreak As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents btnOK As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents llTEST As System.Windows.Forms.LinkLabel
End Class
