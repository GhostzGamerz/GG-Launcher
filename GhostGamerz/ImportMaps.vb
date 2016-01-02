Public Class ImportMaps

    Sub New()
        InitializeComponent()
        Me.Location = New Point((Launcher.Location.X + (Launcher.Width / 2)) - (Me.Width / 2), (Launcher.Location.Y + (Launcher.Height / 2)) - (Me.Height / 2))
        Me.TopMost = True
    End Sub

    Private Sub SpaceButtonGloss2_Click(sender As Object, e As EventArgs) Handles SpaceButtonGloss2.Click
        Me.Close()
    End Sub

    Private Sub SpaceButtonGloss1_Click(sender As Object, e As EventArgs) Handles SpaceButtonGloss1.Click
        ModImporting.Import()
        Me.Close()
    End Sub

    Private Sub SpaceLabel1_Click(sender As Object, e As EventArgs)

    End Sub
End Class