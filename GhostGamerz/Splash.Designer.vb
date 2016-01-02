<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Splash
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Splash))
        Me.SpaceSplashScreen1 = New GhostGamerz.SpaceSplashScreen()
        Me.SuspendLayout()
        '
        'SpaceSplashScreen1
        '
        Me.SpaceSplashScreen1.BorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.SpaceSplashScreen1.Colors = New GhostGamerz.Bloom(-1) {}
        Me.SpaceSplashScreen1.Customization = ""
        Me.SpaceSplashScreen1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SpaceSplashScreen1.Font = New System.Drawing.Font("Verdana", 8.0!)
        Me.SpaceSplashScreen1.Image = Nothing
        Me.SpaceSplashScreen1.Location = New System.Drawing.Point(0, 0)
        Me.SpaceSplashScreen1.Movable = True
        Me.SpaceSplashScreen1.Name = "SpaceSplashScreen1"
        Me.SpaceSplashScreen1.NoRounding = False
        Me.SpaceSplashScreen1.Sizable = False
        Me.SpaceSplashScreen1.Size = New System.Drawing.Size(403, 261)
        Me.SpaceSplashScreen1.SmartBounds = True
        Me.SpaceSplashScreen1.TabIndex = 0
        Me.SpaceSplashScreen1.Text = "SpaceSplashScreen1"
        Me.SpaceSplashScreen1.TransparencyKey = System.Drawing.Color.DeepPink
        Me.SpaceSplashScreen1.Transparent = False
        '
        'Splash
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(403, 261)
        Me.Controls.Add(Me.SpaceSplashScreen1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Splash"
        Me.Opacity = 0.8R
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "GhostzGamerz"
        Me.TopMost = True
        Me.TransparencyKey = System.Drawing.Color.DeepPink
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SpaceSplashScreen1 As GhostGamerz.SpaceSplashScreen

End Class
