Imports System.Reflection

Module DLLLoader

    Sub LoadDLLs()
        AddHandler AppDomain.CurrentDomain.AssemblyResolve, AddressOf AssemblyReslover

    End Sub

    Private Function AssemblyReslover(ByVal sender As Object, ByVal e As ResolveEventArgs) As Assembly
        Dim ra As Assembly
        ra = Assembly.Load(My.Resources.ICSharpCode_SharpZipLib)
        Return ra
    End Function

End Module
