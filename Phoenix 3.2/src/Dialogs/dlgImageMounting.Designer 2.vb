<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgImageMounting

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(dlgImageMounting))
        Me.txtMountingInstructios = New System.Windows.Forms.TextBox
        Me.btnRunUtility = New DevExpress.XtraEditors.SimpleButton
        Me.SuspendLayout()
        '
        'txtMountingInstructios
        '
        Me.txtMountingInstructios.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.txtMountingInstructios.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtMountingInstructios.Location = New System.Drawing.Point(3, 2)
        Me.txtMountingInstructios.Multiline = True
        Me.txtMountingInstructios.Name = "txtMountingInstructios"
        Me.txtMountingInstructios.ReadOnly = True
        Me.txtMountingInstructios.Size = New System.Drawing.Size(367, 349)
        Me.txtMountingInstructios.TabIndex = 5
        Me.txtMountingInstructios.TabStop = False
        Me.txtMountingInstructios.Text = resources.GetString("txtMountingInstructios.Text")
        '
        'btnRunUtility
        '
        Me.btnRunUtility.Location = New System.Drawing.Point(82, 357)
        Me.btnRunUtility.Name = "btnRunUtility"
        Me.btnRunUtility.Size = New System.Drawing.Size(172, 23)
        Me.btnRunUtility.TabIndex = 1
        Me.btnRunUtility.Text = "Run Image Mount Utility"
        '
        'dlgImageMounting
        '
        Me.ClientSize = New System.Drawing.Size(372, 385)
        Me.Controls.Add(Me.btnRunUtility)
        Me.Controls.Add(Me.txtMountingInstructios)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.LookAndFeel.SkinName = "Office 2007 Black"
        Me.Name = "dlgImageMounting"
        Me.Text = "Image Mount"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtMountingInstructios As System.Windows.Forms.TextBox
    Friend WithEvents btnRunUtility As DevExpress.XtraEditors.SimpleButton
End Class
