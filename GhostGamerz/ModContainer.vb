Public Class ModContainer

    Dim Name As String 'Mod name
    Dim ggp As String 'GG prefix
    Dim dzlp As String 'Dayz Launcher prefix
    Dim Download As String 'Link to the mod file
    Dim AVersion As Integer 'Version of arma the mod belongs to

    Sub New(ByVal modname As String, ByVal ggprefix As String, ByVal dzlprefix As String, ByVal armaVersion As Integer, Optional ByVal DownloadLink As String = Nothing)
        Name = modname
        ggp = ggprefix
        dzlp = dzlprefix
        AVersion = armaVersion
        Download = DownloadLink
    End Sub

    Function getName() As String
        Return Name
    End Function

    Function getGGPrefix() As String
        Return ggp
    End Function

    Function getDZLPrefix() As String
        Return dzlp
    End Function

    Function getArmaVersion() As Integer
        Return AVersion
    End Function

    Function getDownloadLink() As String
        Return Download
    End Function

End Class
