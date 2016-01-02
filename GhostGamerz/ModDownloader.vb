Imports System.Net
Imports System.IO

Public Class ModDownloader
    Public Event AmountDownloadedChanged(ByVal iNewProgress As Long, ByVal TotalSize As Long, ByVal obj As TreeNode)
    Public Event FileDownloadSizeObtained(ByVal iFileSize As Long)
    Public Event FileDownloadComplete(ByVal zipfile As String, ByVal node As ServerTreeNode)
    Public Event FileDownloadFailed(ByVal ex As Exception)

    Private mCurrentFile As String = String.Empty
    Private FileDownloadSize As Long = 0
    Private lastUpdate As Long = 0

    Public ReadOnly Property CurrentFile() As String
        Get
            Return mCurrentFile
        End Get
    End Property

    Private Function GetFileName(ByVal URL As String) As String
        Try
            Return URL.Substring(URL.LastIndexOf("/") + 1)
        Catch ex As Exception
            Return URL
        End Try
    End Function

    Public Function DownloadFileWithProgress(ByVal URL As String, ByVal Location As String, ByVal obj As TreeNode) As Boolean
        Dim FS As FileStream = Nothing
        Try
            mCurrentFile = GetFileName(URL)
            Dim wRemote As WebRequest
            Dim bBuffer As Byte()
            ReDim bBuffer(256) '256
            Dim iBytesRead As Integer
            Dim iTotalBytesRead As Integer

            FS = New FileStream(Location, FileMode.Create, FileAccess.Write)
            wRemote = WebRequest.Create(URL)
            Dim myWebResponse As WebResponse = wRemote.GetResponse
            FileDownloadSize = myWebResponse.ContentLength
            RaiseEvent FileDownloadSizeObtained(myWebResponse.ContentLength)
            Dim sChunks As Stream = myWebResponse.GetResponseStream
            Do
                iBytesRead = sChunks.Read(bBuffer, 0, 256) '256
                FS.Write(bBuffer, 0, iBytesRead)
                iTotalBytesRead += iBytesRead
                If myWebResponse.ContentLength < iTotalBytesRead Then
                    RaiseEvent AmountDownloadedChanged(myWebResponse.ContentLength, FileDownloadSize, obj)
                Else
                    If (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds - lastUpdate > 1 Then
                        lastUpdate = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                        RaiseEvent AmountDownloadedChanged(iTotalBytesRead, FileDownloadSize, obj)
                    End If
                End If
            Loop While Not iBytesRead = 0
            sChunks.Close()
            FS.Close()
            RaiseEvent FileDownloadComplete(Location, obj)
            Return True
        Catch ex As Exception
            If Not (FS Is Nothing) Then
                FS.Close()
                FS = Nothing
            End If
            RaiseEvent FileDownloadFailed(ex)
            Return False
        End Try
    End Function
End Class

