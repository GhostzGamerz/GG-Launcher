Public Class DIRContainer

    Dim search() As String 'Folder to search for
    Dim alternativeSearch() As String 'Alternative
    Dim index As Integer
    Dim currentSearch As String

    Sub New(ByVal folder() As String, ByVal alternative() As String)
        search = folder
        alternativeSearch = alternative
    End Sub

    Sub setCurrentSearch(ByVal dir As String)
        currentSearch = dir
    End Sub

    Function getCurrentSearch() As String
        Return currentSearch
    End Function

    Function getSearch() As String()
        Return search
    End Function

    Function getAltSerach() As String()
        Return alternativeSearch
    End Function

    Function getIndex() As Integer
        Return index
    End Function

End Class
