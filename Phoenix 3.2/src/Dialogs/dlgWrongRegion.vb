Imports SMT.Multimedia.Players.DVD.Enums

Public Class dlgWrongRegion
    Inherits DevExpress.XtraEditors.XtraForm

#Region " Windows Form Designer generated code "

    Public PlayerRegion As Byte
    Public DVDRegions() As Boolean
    Friend WithEvents btnExecute As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents cbRegions As DevExpress.XtraEditors.ComboBoxEdit
    Private PP As Phoenix_Form

    Public Sub New(ByVal nPlayerRegion As Byte, ByVal nDVDRegions() As Boolean, ByRef _PP As Phoenix_Form)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        PlayerRegion = nPlayerRegion
        DVDRegions = nDVDRegions
        PP = _PP
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
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblPlayerRegion As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblDVDRegion As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.lblPlayerRegion = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.lblDVDRegion = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.btnExecute = New DevExpress.XtraEditors.SimpleButton
        Me.cbRegions = New DevExpress.XtraEditors.ComboBoxEdit
        CType(Me.cbRegions.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(4, 4)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(215, 22)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Incompatible DVD Region"
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(4, 26)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(127, 17)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Emulator is set to region "
        '
        'lblPlayerRegion
        '
        Me.lblPlayerRegion.Location = New System.Drawing.Point(123, 26)
        Me.lblPlayerRegion.Margin = New System.Windows.Forms.Padding(0)
        Me.lblPlayerRegion.Name = "lblPlayerRegion"
        Me.lblPlayerRegion.Size = New System.Drawing.Size(119, 17)
        Me.lblPlayerRegion.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(4, 43)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(100, 17)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "DVD is region "
        '
        'lblDVDRegion
        '
        Me.lblDVDRegion.Location = New System.Drawing.Point(70, 41)
        Me.lblDVDRegion.Name = "lblDVDRegion"
        Me.lblDVDRegion.Size = New System.Drawing.Size(172, 17)
        Me.lblDVDRegion.TabIndex = 4
        Me.lblDVDRegion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(4, 63)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(132, 18)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "Select new player region:"
        '
        'btnExecute
        '
        Me.btnExecute.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExecute.Appearance.Options.UseFont = True
        Me.btnExecute.Location = New System.Drawing.Point(7, 86)
        Me.btnExecute.Name = "btnExecute"
        Me.btnExecute.Size = New System.Drawing.Size(235, 23)
        Me.btnExecute.TabIndex = 8
        Me.btnExecute.Text = "RESTART"
        '
        'cbRegions
        '
        Me.cbRegions.Location = New System.Drawing.Point(136, 60)
        Me.cbRegions.Name = "cbRegions"
        Me.cbRegions.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cbRegions.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.cbRegions.Size = New System.Drawing.Size(106, 20)
        Me.cbRegions.TabIndex = 9
        '
        'dlgWrongRegion
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        Me.ClientSize = New System.Drawing.Size(247, 114)
        Me.Controls.Add(Me.lblDVDRegion)
        Me.Controls.Add(Me.lblPlayerRegion)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cbRegions)
        Me.Controls.Add(Me.btnExecute)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "dlgWrongRegion"
        Me.ShowInTaskbar = False
        Me.Text = " Player Region"
        Me.TopMost = True
        CType(Me.cbRegions.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub dlgWrongRegion_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim DVDRegionsStr As String = ""
        Dim tName As String
        For i As Byte = 0 To 7
            If DVDRegions(i) Then
                tName = [Enum].GetName(GetType(eRegions), i)
                Me.cbRegions.Properties.Items.Add(tName)
                DVDRegionsStr &= tName & ", "
            End If
        Next
        If Me.cbRegions.Properties.Items.Count = 1 Then Me.cbRegions.Enabled = False 'disable dropdown if there is only one item

        If DVDRegionsStr <> "" Then DVDRegionsStr = Microsoft.VisualBasic.Left(DVDRegionsStr, DVDRegionsStr.Length - 2)

        Me.lblDVDRegion.Text = DVDRegionsStr.ToLower
        Me.cbRegions.SelectedIndex = 0
        Me.lblPlayerRegion.Text = [Enum].GetName(GetType(eRegions), PlayerRegion - 1).ToLower
        Me.Top = PP.Top + 50
        Me.Left = PP.Left + 150
        PP.KH.PauseHook()
    End Sub

    Private Sub dlgWrongRegion_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyData = Keys.Enter Then
            Me.Execute()
        End If
    End Sub

    Private Sub btnExecute_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExecute.Click
        Execute()
    End Sub

    Private Sub dlgWrongRegion_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        If Me.DialogResult <> DialogResult.OK Then Me.DialogResult = DialogResult.Cancel
        PP.KH.UnpauseHook()
    End Sub

    Private Sub Execute()
        PP.CurrentConfig.Region = [Enum].Parse(GetType(eRegions), Me.cbRegions.SelectedItem.ToString)
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

End Class
