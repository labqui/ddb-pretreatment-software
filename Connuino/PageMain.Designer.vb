<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class PageMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PageMain))
        Me.abortar = New System.Windows.Forms.Button()
        Me.ComboPortas = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TrackBar1 = New System.Windows.Forms.TrackBar()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.TimerRead = New System.Windows.Forms.Timer(Me.components)
        Me.StatusField = New System.Windows.Forms.TextBox()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.CheckBox2 = New System.Windows.Forms.CheckBox()
        CType(Me.TrackBar1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'abortar
        '
        Me.abortar.Location = New System.Drawing.Point(12, 77)
        Me.abortar.Name = "abortar"
        Me.abortar.Size = New System.Drawing.Size(93, 33)
        Me.abortar.TabIndex = 10
        Me.abortar.Text = "Reiniciar"
        Me.abortar.UseVisualStyleBackColor = True
        '
        'ComboPortas
        '
        Me.ComboPortas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboPortas.FormattingEnabled = True
        Me.ComboPortas.Location = New System.Drawing.Point(12, 50)
        Me.ComboPortas.Name = "ComboPortas"
        Me.ComboPortas.Size = New System.Drawing.Size(193, 21)
        Me.ComboPortas.TabIndex = 8
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(9, 34)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(35, 13)
        Me.Label7.TabIndex = 8
        Me.Label7.Text = "Porta:"
        '
        'TrackBar1
        '
        Me.TrackBar1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.TrackBar1.Location = New System.Drawing.Point(0, 209)
        Me.TrackBar1.Maximum = 255
        Me.TrackBar1.Name = "TrackBar1"
        Me.TrackBar1.Size = New System.Drawing.Size(347, 45)
        Me.TrackBar1.SmallChange = 25
        Me.TrackBar1.TabIndex = 43
        Me.TrackBar1.TickFrequency = 5
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(111, 77)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(97, 33)
        Me.Button1.TabIndex = 44
        Me.Button1.Text = "Ler"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'TimerRead
        '
        '
        'StatusField
        '
        Me.StatusField.Location = New System.Drawing.Point(12, 131)
        Me.StatusField.Name = "StatusField"
        Me.StatusField.Size = New System.Drawing.Size(314, 20)
        Me.StatusField.TabIndex = 46
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(12, 168)
        Me.ProgressBar1.Maximum = 1023
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(314, 25)
        Me.ProgressBar1.Step = 5
        Me.ProgressBar1.TabIndex = 47
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Location = New System.Drawing.Point(263, 86)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(47, 17)
        Me.CheckBox1.TabIndex = 48
        Me.CheckBox1.Text = "LED"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'CheckBox2
        '
        Me.CheckBox2.AutoSize = True
        Me.CheckBox2.Location = New System.Drawing.Point(214, 50)
        Me.CheckBox2.Name = "CheckBox2"
        Me.CheckBox2.Size = New System.Drawing.Size(69, 17)
        Me.CheckBox2.TabIndex = 49
        Me.CheckBox2.Text = "Conectar"
        Me.CheckBox2.UseVisualStyleBackColor = True
        '
        'PageMain
        '
        Me.AccessibleRole = System.Windows.Forms.AccessibleRole.MenuBar
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.ClientSize = New System.Drawing.Size(347, 254)
        Me.Controls.Add(Me.CheckBox2)
        Me.Controls.Add(Me.CheckBox1)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.StatusField)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.TrackBar1)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.abortar)
        Me.Controls.Add(Me.ComboPortas)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "PageMain"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Text = "Gerenciador"
        CType(Me.TrackBar1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents abortar As System.Windows.Forms.Button
    Friend WithEvents Label7 As Label
    Friend WithEvents TrackBar1 As TrackBar
    Friend WithEvents Button1 As Button
    Friend WithEvents TimerRead As Timer
    Friend WithEvents ProgressBar1 As ProgressBar
    Public WithEvents StatusField As TextBox
    Friend WithEvents CheckBox1 As CheckBox
    Friend WithEvents CheckBox2 As CheckBox
    Public WithEvents ComboPortas As ComboBox
End Class
