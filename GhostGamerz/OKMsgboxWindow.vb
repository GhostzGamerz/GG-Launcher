Public Class OKMsgboxWindow

    Sub New(ByVal title As String, ByVal icon As Integer, ByVal data As String)
        InitializeComponent()
        Me.Location = New Point((Launcher.Location.X + (Launcher.Width / 2)) - (Me.Width / 2), (Launcher.Location.Y + (Launcher.Height / 2)) - (Me.Height / 2))
        Me.TopMost = True
        SpaceTheme1.Text = title
        IconImage.Image = Icons.Images(icon)
        Body.Text = data
        Body.Location = New Point(82, 31)
        Body.Size = New Size(312, 42)
    End Sub

#Region "Flicker Reduction"
    Protected Overrides ReadOnly Property CreateParams() As CreateParams
        Get
            Dim params As CreateParams = MyBase.CreateParams
            params.ExStyle = params.ExStyle Or &H2000000
            Return params
        End Get
    End Property
#End Region

    Private Sub SpaceButtonGloss5_Click(sender As Object, e As EventArgs) Handles SpaceButtonGloss5.Click
        Me.Close()
    End Sub
End Class