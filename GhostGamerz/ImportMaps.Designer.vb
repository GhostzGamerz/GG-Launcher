<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ImportMaps
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ImportMaps))
        Me.SpaceTheme1 = New GhostGamerz.SpaceTheme()
        Me.SpaceButtonGloss1 = New GhostGamerz.SpaceButtonGloss()
        Me.SpaceButtonGloss2 = New GhostGamerz.SpaceButtonGloss()
        Me.SpaceLabel1 = New GhostGamerz.SpaceLabel()
        Me.SpaceTheme1.SuspendLayout()
        Me.SuspendLayout()
        '
        'SpaceTheme1
        '
        Me.SpaceTheme1.BackColor = System.Drawing.Color.FromArgb(CType(CType(14, Byte), Integer), CType(CType(14, Byte), Integer), CType(CType(14, Byte), Integer))
        Me.SpaceTheme1.BorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.SpaceTheme1.Colors = New GhostGamerz.Bloom(-1) {}
        Me.SpaceTheme1.Controls.Add(Me.SpaceButtonGloss1)
        Me.SpaceTheme1.Controls.Add(Me.SpaceButtonGloss2)
        Me.SpaceTheme1.Controls.Add(Me.SpaceLabel1)
        Me.SpaceTheme1.Customization = ""
        Me.SpaceTheme1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SpaceTheme1.Font = New System.Drawing.Font("Verdana", 8.0!)
        Me.SpaceTheme1.Image = Nothing
        Me.SpaceTheme1.Location = New System.Drawing.Point(0, 0)
        Me.SpaceTheme1.MaxSize = New System.Drawing.Size(0, 0)
        Me.SpaceTheme1.MinSize = New System.Drawing.Size(0, 0)
        Me.SpaceTheme1.Movable = True
        Me.SpaceTheme1.Name = "SpaceTheme1"
        Me.SpaceTheme1.NoRounding = False
        Me.SpaceTheme1.Sizable = True
        Me.SpaceTheme1.Size = New System.Drawing.Size(313, 121)
        Me.SpaceTheme1.SmartBounds = True
        Me.SpaceTheme1.TabIndex = 0
        Me.SpaceTheme1.Text = "Import Mods"
        Me.SpaceTheme1.TitleForeColour = System.Drawing.Color.White
        Me.SpaceTheme1.TransparencyKey = System.Drawing.Color.DeepPink
        Me.SpaceTheme1.Transparent = False
        '
        'SpaceButtonGloss1
        '
        Me.SpaceButtonGloss1.BaseColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.SpaceButtonGloss1.ClickedColor = System.Drawing.Color.DeepSkyBlue
        Me.SpaceButtonGloss1.Colors = New GhostGamerz.Bloom(-1) {}
        Me.SpaceButtonGloss1.Customization = ""
        Me.SpaceButtonGloss1.Font = New System.Drawing.Font("Verdana", 8.0!)
        Me.SpaceButtonGloss1.HighlightColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(151, Byte), Integer), CType(CType(205, Byte), Integer))
        Me.SpaceButtonGloss1.Image = Nothing
        Me.SpaceButtonGloss1.Location = New System.Drawing.Point(119, 87)
        Me.SpaceButtonGloss1.Name = "SpaceButtonGloss1"
        Me.SpaceButtonGloss1.NoRounding = False
        Me.SpaceButtonGloss1.Size = New System.Drawing.Size(88, 23)
        Me.SpaceButtonGloss1.TabIndex = 8
        Me.SpaceButtonGloss1.Text = "Yes"
        Me.SpaceButtonGloss1.Transparent = False
        '
        'SpaceButtonGloss2
        '
        Me.SpaceButtonGloss2.BaseColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.SpaceButtonGloss2.ClickedColor = System.Drawing.Color.DeepSkyBlue
        Me.SpaceButtonGloss2.Colors = New GhostGamerz.Bloom(-1) {}
        Me.SpaceButtonGloss2.Customization = ""
        Me.SpaceButtonGloss2.Font = New System.Drawing.Font("Verdana", 8.0!)
        Me.SpaceButtonGloss2.HighlightColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(151, Byte), Integer), CType(CType(205, Byte), Integer))
        Me.SpaceButtonGloss2.Image = Nothing
        Me.SpaceButtonGloss2.Location = New System.Drawing.Point(213, 87)
        Me.SpaceButtonGloss2.Name = "SpaceButtonGloss2"
        Me.SpaceButtonGloss2.NoRounding = False
        Me.SpaceButtonGloss2.Size = New System.Drawing.Size(88, 23)
        Me.SpaceButtonGloss2.TabIndex = 7
        Me.SpaceButtonGloss2.Text = "No"
        Me.SpaceButtonGloss2.Transparent = False
        '
        'SpaceLabel1
        '
        Me.SpaceLabel1.BackColor = System.Drawing.Color.Transparent
        Me.SpaceLabel1.ForeColor = System.Drawing.Color.White
        Me.SpaceLabel1.Location = New System.Drawing.Point(12, 32)
        Me.SpaceLabel1.Name = "SpaceLabel1"
        Me.SpaceLabel1.Size = New System.Drawing.Size(289, 52)
        Me.SpaceLabel1.TabIndex = 0
        Me.SpaceLabel1.Text = "Are you sure you want to import DayZ launcher mods into GhostzGamerz Launcher? Th" & _
    "is may take some time."
        '
        'ImportMaps
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(313, 121)
        Me.Controls.Add(Me.SpaceTheme1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "ImportMaps"
        Me.Text = "ImportMaps"
        Me.TransparencyKey = System.Drawing.Color.DeepPink
        Me.SpaceTheme1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SpaceTheme1 As GhostGamerz.SpaceTheme
    Friend WithEvents SpaceLabel1 As GhostGamerz.SpaceLabel
    Friend WithEvents SpaceButtonGloss2 As GhostGamerz.SpaceButtonGloss
    Friend WithEvents SpaceButtonGloss1 As GhostGamerz.SpaceButtonGloss
End Class
