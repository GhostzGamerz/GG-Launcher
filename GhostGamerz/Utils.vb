Module Utils

    Function parseLineColour(ByVal rawdata As String) As String
        Try
            Dim line As String() = rawdata.Split(New String() {"<col="}, StringSplitOptions.None)
            Dim line2 As String() = line(1).Split(New String() {">"}, StringSplitOptions.None)
            Return line2(0)
        Catch ex As Exception
            Return Nothing 'If cant find class, return nothing.
        End Try
    End Function

End Module
