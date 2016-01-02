<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class OKMsgboxWindow
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(OKMsgboxWindow))
        Me.Icons = New System.Windows.Forms.ImageList(Me.components)
        Me.SpaceTheme1 = New GhostGamerz.SpaceTheme()
        Me.SpaceButtonGloss5 = New GhostGamerz.SpaceButtonGloss()
        Me.IconImage = New System.Windows.Forms.PictureBox()
        Me.Body = New System.Windows.Forms.Label()
        Me.SpaceTheme1.SuspendLayout()
        CType(Me.IconImage, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Icons
        '
        Me.Icons.ImageStream = CType(resources.GetObject("Icons.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.Icons.TransparentColor = System.Drawing.Color.Transparent
        Me.Icons.Images.SetKeyName(0, "questionmark.png")
        Me.Icons.Images.SetKeyName(1, "warning.png")
        '
        'SpaceTheme1
        '
        Me.SpaceTheme1.BackColor = System.Drawing.Color.FromArgb(CType(CType(14, Byte), Integer), CType(CType(14, Byte), Integer), CType(CType(14, Byte), Integer))
        Me.SpaceTheme1.BorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.SpaceTheme1.Colors = New GhostGamerz.Bloom(-1) {}
        Me.SpaceTheme1.Controls.Add(Me.SpaceButtonGloss5)
        Me.SpaceTheme1.Controls.Add(Me.IconImage)
        Me.SpaceTheme1.Controls.Add(Me.Body)
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
        Me.SpaceTheme1.Sizable = False
        Me.SpaceTheme1.Size = New System.Drawing.Size(409, 111)
        Me.SpaceTheme1.SmartBounds = True
        Me.SpaceTheme1.TabIndex = 0
        Me.SpaceTheme1.Text = "Window Title"
        Me.SpaceTheme1.TitleForeColour = System.Drawing.Color.White
        Me.SpaceTheme1.TransparencyKey = System.Drawing.Color.DeepPink
        Me.SpaceTheme1.Transparent = False
        '
        'SpaceButtonGloss5
        '
        Me.SpaceButtonGloss5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SpaceButtonGloss5.BaseColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.SpaceButtonGloss5.ClickedColor = System.Drawing.Color.DeepSkyBlue
        Me.SpaceButtonGloss5.Colors = New GhostGamerz.Bloom(-1) {}
        Me.SpaceButtonGloss5.Customization = ""
        Me.SpaceButtonGloss5.Font = New System.Drawing.Font("Verdana", 8.0!)
        Me.SpaceButtonGloss5.HighlightColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(151, Byte), Integer), CType(CType(205, Byte), Integer))
        Me.SpaceButtonGloss5.Image = Nothing
        Me.SpaceButtonGloss5.Location = New System.Drawing.Point(309, 76)
        Me.SpaceButtonGloss5.Name = "SpaceButtonGloss5"
        Me.SpaceButtonGloss5.NoRounding = False
        Me.SpaceButtonGloss5.Size = New System.Drawing.Size(88, 23)
        Me.SpaceButtonGloss5.TabIndex = 17
        Me.SpaceButtonGloss5.Text = "Ok"
        Me.SpaceButtonGloss5.Transparent = False
        '
        'IconImage
        '
        Me.IconImage.BackColor = System.Drawing.Color.Transparent
        Me.IconImage.Location = New System.Drawing.Point(12, 31)
        Me.IconImage.Name = "IconImage"
        Me.IconImage.Size = New System.Drawing.Size(64, 67)
        Me.IconImage.TabIndex = 15
        Me.IconImage.TabStop = False
        '
        'Body
        '
        Me.Body.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Body.BackColor = System.Drawing.Color.Transparent
        Me.Body.ForeColor = System.Drawing.Color.White
        Me.Body.Location = New System.Drawing.Point(82, 31)
        Me.Body.MaximumSize = New System.Drawing.Size(312, 0)
        Me.Body.Name = "Body"
        Me.Body.Size = New System.Drawing.Size(312, 42)
        Me.Body.TabIndex = 14
        Me.Body.Text = "Body"
        '
        'OKMsgboxWindow
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(409, 111)
        Me.Controls.Add(Me.SpaceTheme1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "OKMsgboxWindow"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Alert"
        Me.TopMost = True
        Me.TransparencyKey = System.Drawing.Color.DeepPink
        Me.SpaceTheme1.ResumeLayout(False)
        CType(Me.IconImage, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SpaceTheme1 As SpaceTheme
    Friend WithEvents IconImage As System.Windows.Forms.PictureBox
    Friend WithEvents Body As System.Windows.Forms.Label
    Friend WithEvents Icons As System.Windows.Forms.ImageList
    Friend WithEvents SpaceButtonGloss5 As GhostGamerz.SpaceButtonGloss
End Class
