Public Class Splash
    Dim T As New Timer
    Private Sub Splash_Load(sender As Object, e As EventArgs) Handles Me.Load
        Launcher.Enabled = False
        Launcher.Opacity = 0%
        Me.Location = New Point((My.Computer.Screen.WorkingArea.Width / 2) - (Me.Width / 2), (My.Computer.Screen.WorkingArea.Height / 2) - (Me.Height / 2))

        AddHandler T.Tick, AddressOf Close_form
        With T
            .Interval = 2000
            .Start()
        End With
    End Sub

    Sub Close_form()
        T.Stop()
        Me.Close()
        Launcher.Location = New Point((My.Computer.Screen.WorkingArea.Width / 2) - (Launcher.Width / 2), (My.Computer.Screen.WorkingArea.Height / 2) - (Launcher.Height / 2))
        Launcher.Opacity = 100%
        Launcher.Enabled = True
        Launcher.SplashScreenDone()
    End Sub
End Class
