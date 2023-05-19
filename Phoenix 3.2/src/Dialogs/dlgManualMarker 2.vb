Imports SMT.Applications.Phoenix.Enums
Imports SMT.Applications.Phoenix.Classes
Imports SMT.Multimedia.Players.DVD.Enums
Imports DevExpress.XtraEditors
Imports SMT.Multimedia.DirectShow
Imports SMT.DotNet.AppConsole

Public Class dlgManualMarker
    Inherits DevExpress.XtraEditors.XtraForm

#Region " Windows Form Designer generated code "

    Private PP As Phoenix_Form
    Public Sub New(ByVal _pp As Phoenix_Form)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        PP = _pp
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
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cbTitles As System.Windows.Forms.ComboBox
    Friend WithEvents txtTimeSearch_h As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtTimeSearch_m As System.Windows.Forms.TextBox
    Friend WithEvents txtTimeSearch_s As System.Windows.Forms.TextBox
    Friend WithEvents txtMarkerName As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cbSetNames As System.Windows.Forms.ComboBox
    Friend WithEvents btnCreateMarker As System.Windows.Forms.Button
    Friend WithEvents llNewSet As System.Windows.Forms.LinkLabel
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtMarkerName = New System.Windows.Forms.TextBox
        Me.cbTitles = New System.Windows.Forms.ComboBox
        Me.txtTimeSearch_h = New System.Windows.Forms.TextBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.txtTimeSearch_m = New System.Windows.Forms.TextBox
        Me.txtTimeSearch_s = New System.Windows.Forms.TextBox
        Me.btnCreateMarker = New System.Windows.Forms.Button
        Me.cbSetNames = New System.Windows.Forms.ComboBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.llNewSet = New System.Windows.Forms.LinkLabel
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(4, 52)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(76, 16)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Title Number:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(4, 76)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(76, 16)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Time:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(4, 4)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(76, 16)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Marker Name:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtMarkerName
        '
        Me.txtMarkerName.Location = New System.Drawing.Point(80, 4)
        Me.txtMarkerName.Name = "txtMarkerName"
        Me.txtMarkerName.Size = New System.Drawing.Size(132, 20)
        Me.txtMarkerName.TabIndex = 0
        Me.txtMarkerName.Text = ""
        '
        'cbTitles
        '
        Me.cbTitles.BackColor = System.Drawing.SystemColors.Window
        Me.cbTitles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbTitles.Items.AddRange(New Object() {"1", "2", "3", "4", "5", "6", "7", "8", "10"})
        Me.cbTitles.Location = New System.Drawing.Point(80, 52)
        Me.cbTitles.Name = "cbTitles"
        Me.cbTitles.Size = New System.Drawing.Size(40, 21)
        Me.cbTitles.TabIndex = 2
        '
        'txtTimeSearch_h
        '
        Me.txtTimeSearch_h.Location = New System.Drawing.Point(80, 76)
        Me.txtTimeSearch_h.Name = "txtTimeSearch_h"
        Me.txtTimeSearch_h.Size = New System.Drawing.Size(24, 20)
        Me.txtTimeSearch_h.TabIndex = 3
        Me.txtTimeSearch_h.Text = "h"
        Me.txtTimeSearch_h.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label14
        '
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(104, 76)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(8, 16)
        Me.Label14.TabIndex = 32
        Me.Label14.Text = ":"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label15
        '
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(136, 76)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(8, 16)
        Me.Label15.TabIndex = 33
        Me.Label15.Text = ":"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'txtTimeSearch_m
        '
        Me.txtTimeSearch_m.Location = New System.Drawing.Point(112, 76)
        Me.txtTimeSearch_m.Name = "txtTimeSearch_m"
        Me.txtTimeSearch_m.Size = New System.Drawing.Size(24, 20)
        Me.txtTimeSearch_m.TabIndex = 4
        Me.txtTimeSearch_m.Text = "m"
        Me.txtTimeSearch_m.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtTimeSearch_s
        '
        Me.txtTimeSearch_s.Location = New System.Drawing.Point(144, 76)
        Me.txtTimeSearch_s.Name = "txtTimeSearch_s"
        Me.txtTimeSearch_s.Size = New System.Drawing.Size(24, 20)
        Me.txtTimeSearch_s.TabIndex = 5
        Me.txtTimeSearch_s.Text = "s"
        Me.txtTimeSearch_s.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'btnCreateMarker
        '
        Me.btnCreateMarker.Location = New System.Drawing.Point(84, 100)
        Me.btnCreateMarker.Name = "btnCreateMarker"
        Me.btnCreateMarker.Size = New System.Drawing.Size(128, 23)
        Me.btnCreateMarker.TabIndex = 6
        Me.btnCreateMarker.Text = "Create Marker"
        '
        'cbSetNames
        '
        Me.cbSetNames.BackColor = System.Drawing.SystemColors.Window
        Me.cbSetNames.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbSetNames.Items.AddRange(New Object() {"1", "2", "3", "4", "5", "6", "7", "8", "10"})
        Me.cbSetNames.Location = New System.Drawing.Point(80, 28)
        Me.cbSetNames.Name = "cbSetNames"
        Me.cbSetNames.Size = New System.Drawing.Size(100, 21)
        Me.cbSetNames.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(4, 28)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(76, 16)
        Me.Label4.TabIndex = 35
        Me.Label4.Text = "Set:"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'llNewSet
        '
        Me.llNewSet.Location = New System.Drawing.Point(184, 32)
        Me.llNewSet.Name = "llNewSet"
        Me.llNewSet.Size = New System.Drawing.Size(28, 16)
        Me.llNewSet.TabIndex = 37
        Me.llNewSet.TabStop = True
        Me.llNewSet.Text = "New"
        '
        'dlgManualMarker
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(214, 124)
        Me.Controls.Add(Me.llNewSet)
        Me.Controls.Add(Me.cbSetNames)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.btnCreateMarker)
        Me.Controls.Add(Me.cbTitles)
        Me.Controls.Add(Me.txtTimeSearch_h)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.txtTimeSearch_m)
        Me.Controls.Add(Me.txtTimeSearch_s)
        Me.Controls.Add(Me.txtMarkerName)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.KeyPreview = True
        Me.Name = "dlgManualMarker"
        Me.ShowInTaskbar = False
        Me.Text = " Manual Marker"
        Me.TopMost = True
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public NewMarker As cMarker
    Public SetName As String

    Private Sub dlgManualMarker_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        For s As Short = 1 To PP.Player.CurrentDVD.VMGM.GlobalTTs.Count
            Me.cbTitles.Items.Add(s)
        Next
        If PP.Player.CurrentDomain = DvdDomain.Title Then
            cbTitles.SelectedIndex = cbTitles.FindStringExact(PP.Player.CurrentTitle)
        Else
            cbTitles.SelectedIndex = 0
        End If

        Me.Top = PP.Top + 200
        Me.Left = PP.Left + 150

        Me.txtTimeSearch_h.Text = "h"
        Me.txtTimeSearch_m.Text = "m"
        Me.txtTimeSearch_s.Text = "s"
        PP.KH.PauseHook()

        Me.cbSetNames.Items.Clear()
        For Each MS As cMarkerSet In PP.CurrentMarkerCollection.MarkerSets
            Me.cbSetNames.Items.Add(MS.SetName)
        Next
        If Me.cbSetNames.Items.Count > 0 Then
            Me.cbSetNames.SelectedIndex = 0
        End If

    End Sub

    Private AcceptKeys As Boolean = True
    Private NextKeySwitchToAccept As Boolean = False
    Private Sub dlgPlayerDefaults_KeyUp(ByVal sender As Object, ByVal e As KeyEventArgs) Handles MyBase.KeyUp
        If Not AcceptKeys Then
            If NextKeySwitchToAccept Then
                AcceptKeys = True
            End If
            Exit Sub
        End If
        If e.KeyData = Keys.Enter Then
            Execute()
        End If
    End Sub

    Private Sub btnCreateMarker_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCreateMarker.Click
        Execute()
    End Sub

    Private Sub txtTimeSearch_h_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTimeSearch_h.TextChanged
        If Me.txtTimeSearch_h.Text = "h" Then Exit Sub
        If IsNumeric(Me.txtTimeSearch_h.Text) Then
            If Me.txtTimeSearch_h.Text > 99 Then
                Me.txtTimeSearch_h.Text = ""
                Exit Sub
            End If
            If Me.txtTimeSearch_h.Text.Length = 2 Then
                Me.txtTimeSearch_m.Focus()
            End If
        Else
            Me.txtTimeSearch_h.Text = ""
            Exit Sub
        End If
    End Sub

    Private Sub txtTimeSearch_m_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTimeSearch_m.TextChanged
        If Me.txtTimeSearch_m.Text = "m" Then Exit Sub
        If IsNumeric(Me.txtTimeSearch_m.Text) Then
            If Me.txtTimeSearch_m.Text > 99 Then
                Me.txtTimeSearch_m.Text = ""
                Exit Sub
            End If
            If Me.txtTimeSearch_m.Text.Length = 2 Then
                Me.txtTimeSearch_s.Focus()
            End If
        Else
            Me.txtTimeSearch_m.Text = ""
            Exit Sub
        End If
    End Sub

    Private Sub txtTimeSearch_s_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTimeSearch_s.TextChanged
        If Me.txtTimeSearch_s.Text = "s" Then Exit Sub
        If IsNumeric(Me.txtTimeSearch_s.Text) Then
            If Me.txtTimeSearch_s.Text > 99 Then
                Me.txtTimeSearch_s.Text = ""
                Exit Sub
            End If
            If Me.txtTimeSearch_s.Text.Length = 2 Then
                Me.btnCreateMarker.Focus()
            End If
        Else
            Me.txtTimeSearch_s.Text = ""
            Exit Sub
        End If
    End Sub

    Private Sub Execute()
        Try
            If Me.txtMarkerName.Text = "" Then
                XtraMessageBox.Show(Me.LookAndFeel, Me, "Invalid marker name.", My.Settings.APPLICATION_NAME, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            If Not IsNumeric(Me.txtTimeSearch_h.Text) Then
                Me.txtTimeSearch_h.Text = "0"
            End If
            If Not IsNumeric(Me.txtTimeSearch_m.Text) Then
                Me.txtTimeSearch_m.Text = "0"
            End If
            If Not IsNumeric(Me.txtTimeSearch_s.Text) Then
                Me.txtTimeSearch_s.Text = "0"
            End If

            NewMarker = New cMarker
            With NewMarker
                .ChapterNum = 0
                .Frames = 0
                .GOPTC = Nothing
                .Hours = Me.txtTimeSearch_h.Text
                .MarkerName = Me.txtMarkerName.Text
                .Minutes = Me.txtTimeSearch_m.Text
                .Seconds = Me.txtTimeSearch_s.Text
                .TimeCodeFlags = 0
                .TitleNum = Me.cbTitles.SelectedItem.ToString
            End With

            If Me.cbSetNames.SelectedIndex > -1 Then
                SetName = Me.cbSetNames.SelectedItem.ToString
            Else
                SetName = ""
            End If

            Me.DialogResult = DialogResult.OK
            Me.Close()
        Catch ex As Exception
            PP.AddConsoleLine(eConsoleItemType.ERROR, "Problem with Execute on ManualMarker dialog.", ex.Message)
        End Try
    End Sub

    Private Sub llNewSet_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llNewSet.LinkClicked
        Try
            AcceptKeys = False
            NextKeySwitchToAccept = True
            If PP.Markers_Set_New() Then
                Me.cbSetNames.Items.Clear()
                For Each MS As cMarkerSet In PP.CurrentMarkerCollection.MarkerSets
                    Me.cbSetNames.Items.Add(MS.SetName)
                Next
                Me.cbSetNames.SelectedIndex = Me.cbSetNames.Items.Count - 1
            Else
                XtraMessageBox.Show(Me.LookAndFeel, Me, "Failed to create new set in Manual Marker dialog.", My.Settings.APPLICATION_NAME, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
            PP.KH.PauseHook()
        Catch ex As Exception
            PP.AddConsoleLine(eConsoleItemType.ERROR, "Problem with NewSet in ManualMarker dialog.", ex.Message)
        End Try
    End Sub

    Private Sub dlgManualMarker_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        If Not Me.DialogResult = DialogResult.OK Then DialogResult = DialogResult.Cancel
        PP.KH.UnpauseHook()
    End Sub

End Class
