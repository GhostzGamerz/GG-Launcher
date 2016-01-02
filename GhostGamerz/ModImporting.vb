Imports System.IO

Module ModImporting

#Region "Mod Importing (from DayZ Launcher)"

    Private Sub ModImportButton_Click(sender As Object, e As EventArgs)
        'Dim yn As New ImportMaps
        'yn.ShowDialog()
    End Sub

    Sub Import()
        Dim msg As OKMsgboxWindow
        If Launcher.CheckForDownloadingMods(Launcher.ModsList) Then
            msg = New OKMsgboxWindow("Downloads active", 0, "Please wait until all downloads are completed.")
            msg.ShowDialog()
            Return
        End If
        Dim t As New Threading.Thread(Sub() importmods())
        t.IsBackground = True
        Enable(False, Launcher.UITabs)
        msg = New OKMsgboxWindow("Importing", 0, "Importing mods from DayZ Launcher, this may take a while. Do not close the launcher until completed.")
        SetText("Importing Mods", Launcher.modstatus)
        msg.ShowDialog()
        t.Start()
    End Sub

    Sub importmods()
        Dim copied As String = ""
        Dim failed As String = ""
        For Each dmod As ModContainer In Launcher.ModList
            If IO.Directory.Exists(Launcher.Mods + "\" + dmod.getDZLPrefix) Then
                If Not dmod.getDZLPrefix = "@DayZ" Then
                    SetText("Importing: " + dmod.getName, Launcher.modstatus)
                    If dmod.getArmaVersion = 2 Then
                        CopyMod(New DirectoryInfo(Launcher.Mods + "\" + dmod.getDZLPrefix), New DirectoryInfo(Launcher.Mods + "\" + dmod.getGGPrefix), True)
                    End If
                    If dmod.getArmaVersion = 3 Then
                        CopyMod(New DirectoryInfo(Launcher.A3Mods + "\" + dmod.getDZLPrefix), New DirectoryInfo(Launcher.Mods + "\" + dmod.getGGPrefix), True)
                    End If
                    If Not IO.Directory.Exists(Launcher.Mods + "\" + dmod.getGGPrefix) Or Not IO.Directory.Exists(Launcher.A3Mods + "\" + dmod.getGGPrefix) Then
                        failed += dmod.getName & ", "
                    End If
                    copied += dmod.getName & ", "
                End If
            End If
        Next
        Enable(True, Launcher.UITabs)
        SetText("Importing Done.", Launcher.modstatus)
        Launcher.ReloadMods()
        If Not failed = Nothing Then
            Dim msg As New OKMsgboxWindow("Mods failed", 1, "Some mods didnt import." & vbNewLine & failed)
            msg.ShowDialog()
        End If
    End Sub

    Public Sub CopyMod(ByVal base As DirectoryInfo, ByVal t As DirectoryInfo, ByVal overwrite As Boolean)
        For Each dir As DirectoryInfo In base.GetDirectories()
            CopyMod(dir, t.CreateSubdirectory(dir.Name), overwrite)
        Next

        For Each file As FileInfo In base.GetFiles
            Try
                file.CopyTo(Path.Combine(t.FullName, file.Name), overwrite)
            Catch ex As Exception

            End Try
        Next
    End Sub

#End Region

End Module
