Imports System.Drawing.Text
Imports System.Runtime.InteropServices

Public NotInheritable Class CustomFont
    Implements IDisposable

    Private fontCollection As New PrivateFontCollection()
    Private fontPtr As IntPtr

#Region "Constructor"
    Public Sub New(ByVal fontData() As Byte)
        fontPtr = Marshal.AllocHGlobal(fontData.Length)
        Marshal.Copy(fontData, 0, fontPtr, fontData.Length)
        fontCollection.AddMemoryFont(fontPtr, fontData.Length)
    End Sub
#End Region

#Region "Destructor"
    Public Sub Dispose() Implements IDisposable.Dispose
        Marshal.FreeHGlobal(fontPtr)
        fontCollection.Dispose()

        GC.SuppressFinalize(Me)
    End Sub

    Protected Overrides Sub Finalize()
        Marshal.FreeHGlobal(fontPtr)
    End Sub
#End Region

#Region "Properties"
    Public ReadOnly Property Font() As FontFamily
        Get
            Return fontCollection.Families(0)
        End Get
    End Property
#End Region

End Class