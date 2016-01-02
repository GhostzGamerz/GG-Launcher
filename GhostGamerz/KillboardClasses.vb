Module KillboardClasses

    Dim classesArray As WeaponClassContainer() = {
        New WeaponClassContainer("", "")
    }

    Function parseClassname(ByVal killLine As String) As String
        For Each w As WeaponClassContainer In classesArray
            If killLine.ToLower.Contains(w.getBaseClass) Then
                Return killLine.ToLower.Replace(w.getBaseClass, w.getRealName)
            End If
        Next
        Return killLine
    End Function

End Module

Class WeaponClassContainer
    Dim baseclass As String
    Dim realname As String

    Sub New(ByVal bc As String, ByVal rn As String)
        baseclass = bc.ToLower
        realname = rn.ToLower
    End Sub

    Function getBaseClass() As String
        Return baseclass
    End Function

    Function getRealName() As String
        Return realname
    End Function
End Class
