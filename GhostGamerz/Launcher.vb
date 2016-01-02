Imports System.Runtime.InteropServices
Imports System.ServiceProcess
Imports System.Net
Imports System.Net.Sockets
Imports ICSharpCode.SharpZipLib.Zip
Imports System.IO
Imports ICSharpCode.SharpZipLib.Core
Imports System.Reflection
Imports System.Text.RegularExpressions

Public Class Launcher

    Dim version As Double = "0.71"

    Dim selectedMapName As String
    Dim selectedTab As Integer = 0

    Dim MapScrollPosition As Integer = 0
    Dim MapScrollPositionMax As Integer = 0

    Public A2 As String
    Public A2OA As String
    Public Mods As String  'Using A2OA to keep it simple
    Public Args As String

    Public A3 As String
    Public A3Mods As String
    Public A3Args As String

    Dim MapType As String = "A2"

    Dim serverAddress As String = "192.99.46.157"
    Dim baseServer As String = serverAddress + "/server_status/"

    'Mod name, gg launcher prefix, dayz launcher prefix, Download Link 
    Public ModList As ModContainer() = {
        New ModContainer("Chernarus", "@DayZ", "@DayZ", 2, Nothing),
        New ModContainer("Taviana", "@Taviana", "@Taviana", 2, "http://ghostzgamerz.com/downloads/A2MODS/@Taviana.zip"),
        New ModContainer("Epoch", "@DayZ_Epoch1051", "@DayZ_Epoch1051", 2, "http://ghostzgamerz.com/downloads/A2MODS/@DayzEpoch.zip"),
        New ModContainer("Overwatch", "@DayzOverwatch", "@DayzOverwatch", 2, "http://ghostzgamerz.com/downloads/A2MODS/@DayzOverwatch.zip"),
        New ModContainer("Exile", "@Exile", "@Exile", 3, "http://ghostzgamerz.com/downloads/A3MODS/@Exile.zip"),
        New ModContainer("RHS : RU", "@RHSAFRF", "@RHSAFRF", 4, "http://ghostzgamerz.com/downloads/A3MODS/@RHSAFRF.zip"),
        New ModContainer("RHS : US", "@RHSUSAF", "@RHSUSAF", 5, "http://ghostzgamerz.com/downloads/A3MODS/@RHSUSAF.zip"),
        New ModContainer("AllInArmaTerrainPack", "@AllInArmaTerrainPack", "@AllInArmaTerrainPack", 6, "http://ghostzgamerz.com/downloads/A3MODS/@AllInArmaTerrainPack.zip")
    }

    Public ArmaLocationArray As New DIRContainer(New String() {"program Files", "Steam", "steamapps", "common"}, New String() {"program Files (x86)", "Steam", "steamapps", "common"})

    'Main Method
    Shared Sub New()
        'Load the dependencies from within memory
        LoadDLLs()
    End Sub

    Function LocateArma(ByVal drive As String, ByVal container As DIRContainer, ByVal index As Integer)
        If IO.Directory.Exists(drive & container.getSearch(container.getIndex)) Then
            'locate here, use recursion

        End If
    End Function

    Private Sub Launcher_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Splash.Show()

        Dim CheckVerThread As New Threading.Thread(Sub() Updater())
        CheckVerThread.IsBackground = True
        CheckVerThread.Start()

        loadSettings()

        Dim LoadDataThread As New Threading.Thread(Sub() loadData())
        LoadDataThread.IsBackground = True
        LoadDataThread.Start()

        LoadMods()
    End Sub

#Region "Updater"
    Sub Updater()
        Try
            Dim WC As New Net.WebClient
            WC.Proxy = Nothing
            Dim updateServer As Double = WC.DownloadString(baseServer & "launcher/update.txt")
            If Not updateServer <= version Then
                Dim update As New OKMsgboxWindow("New Version", 0, "There is a newer version of the launcher. Go to the GhostzGamerz website to download it.")
                update.ShowDialog()
                End
            End If
        Catch ex As Exception
            Dim update As New OKMsgboxWindow("Error", 1, "Couldn't connect to the update server.")
            update.ShowDialog()
            'End
        End Try
    End Sub
#End Region

    Sub loadData()
        Dim WC As New Net.WebClient
        WC.Proxy = Nothing

        'Load webserver data here
        ClearControls(ServerScrollPanel)
        SetAutoScrollMinSize(New Size(0, 0), ServerScrollPanel)
        SetAutoScroll(False, ServerScrollPanel)

        Dim l As New SpaceLabel
        l.Text = "Loading, Please wait..."
        l.AutoSize = True
        AddControl(l, ServerScrollPanel)
        SetLocation(New Point((GetSize(ServerScrollPanel).Width / 2) - (l.Width / 2), 10), l)

        'loadMaps(WC.DownloadString(baseServer & "serverdata.php"))
    End Sub

    Sub loadMaps(ByVal rawdata As String)
        SetText("Map selected: None", selectedMapLbl)
        ClearControls(ServerScrollPanel)

        Dim i As Integer = 0
        Dim Maps() As String = rawdata.Split(New String() {"[serverDetails]"}, StringSplitOptions.None)

        Dim ContentHeight As Integer = 0

        For Each MapData As String In Maps
            If i > 0 Then
                Dim gametype As String = parseDivValue("lgametype", MapData)
                If gametype = MapType Then
                    Dim ServPan As New SpaceServerItem
                    SetTitle(parseDivValue("ltitle", MapData), ServPan)
                    SetText(parseDivValue("ldesc", MapData), ServPan)
                    SetServerImage(loadServerImage(parseDivValue("serverImage", MapData)), ServPan)
                    SetStatus(parseDivValue("status", MapData), ServPan)
                    SetPlayers(parseDivValue("players", MapData), ServPan)
                    SetMaxPlayers(parseDivValue("maxPlayers", MapData), ServPan)
                    SetClientMods(parseDivValue("clientmods", MapData), ServPan)
                    SetGameType(parseDivValue("lgametype", MapData), ServPan)
                    SetArmaVersion(parseDivValue("lgametype", MapData), ServPan)
                    SetIP(parseDivValue("serverIP", MapData), ServPan)
                    SetPort(parseDivValue("serverPort", MapData), ServPan)
                    SetPassword(parseDivValue("pass", MapData), ServPan)
                    SetBorderColor(Color.DeepSkyBlue, ServPan)
                    SetSize(New Size(ServerScrollPanel.Width, 105), ServPan)
                    SetLocation(New Point(0, (ServerScrollPanel.Controls.Count * (105 + 5))), ServPan)
                    SetAnchor(AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Top, ServPan)
                    ContentHeight += (ServerScrollPanel.Controls.Count * (105 + 5))
                    SetCursor(Cursors.Hand, ServPan)
                    AddHandler ServPan.Click, AddressOf SwitchServer
                    AddControl(ServPan, ServerScrollPanel)
                End If
            End If
            i += 1
        Next

        'Set scroll container size
        SetAutoScrollMinSize(New Size(0, ContentHeight), ServerScrollPanel)
        MapScrollPositionMax = ContentHeight
        SetAutoScroll(False, ServerScrollPanel)
    End Sub

    Sub LoadMods()

        ClearNodes(ModsList)

        Dim installed As New ServerTreeNode
        installed.Text = "Installed Mods"
        installed.IsMod = False
        AddNode(installed, ModsList)

        Dim downloads As New ServerTreeNode
        downloads.Text = "Downloads"
        downloads.IsMod = False
        AddNode(downloads, ModsList)

        For Each dmod As ModContainer In ModList
            Dim SelectedModPath As String = Nothing
            If dmod.getArmaVersion() = 2 Then
                SelectedModPath = Mods
            End If
            If dmod.getArmaVersion() = 3 Then
                SelectedModPath = A3Mods
            End If

            If IO.Directory.Exists(SelectedModPath + "\" + dmod.getGGPrefix) Then
                Dim n As New ServerTreeNode
                n.Text = dmod.getName
                n.ForeColor = Color.DeepSkyBlue
                n.Downloadable = False
                n.Downloading = False
                n.IsMod = True
                n.RawMod = dmod.getGGPrefix
                AddNodeToNode(installed, n, ModsList)
            Else
                Dim n As New ServerTreeNode
                n.Text = dmod.getName
                n.ModName = dmod.getName
                n.DownloadLink = dmod.getDownloadLink
                n.Downloadable = True
                n.Downloading = False
                n.IsMod = False
                n.RawMod = dmod.getGGPrefix
                AddNodeToNode(downloads, n, ModsList)
            End If
        Next
    End Sub

    Sub SwitchServer(ByVal sender As Object, ByVal e As EventArgs)
        Dim s As SpaceServerItem = CType(sender, SpaceServerItem)
        UpdateMaps(s)
    End Sub

    Function parseDivValue(ByVal divclass As String, ByVal rawdata As String) As String
        Try
            Dim line As String() = rawdata.Split(New String() {"[" & divclass & "]"}, StringSplitOptions.None)
            Dim line2 As String() = line(1).Split(New String() {"[/" & divclass & "]"}, StringSplitOptions.None)
            Return line2(0)
        Catch ex As Exception
            Return Nothing 'If cant find class, return nothing.
        End Try
    End Function

    Function loadServerImage(ByVal link As String) As Image
        Return New System.Drawing.Bitmap(New IO.MemoryStream(New System.Net.WebClient().DownloadData(link)))
    End Function

    Sub loadSettings()
        Dim settingsFile As String = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) & "/Settings.ini"
        If IO.File.Exists(settingsFile) Then
            Dim sr As New StreamReader(settingsFile)
            Dim settings As String = sr.ReadToEnd()
            A2 = parseDivValue("A2", settings)
            A2OA = parseDivValue("A2OA", settings)
            Mods = parseDivValue("MODS", settings)
            Args = parseDivValue("ARGS", settings)
            A3 = parseDivValue("A3", settings)
            A3Args = parseDivValue("A3ARGS", settings)
            A3Mods = parseDivValue("A3MODS", settings)
        Else
            Dim sw As StreamWriter
            Dim fs As FileStream = Nothing
            sw = File.AppendText(settingsFile)
            sw.WriteLine("[A2][/A2]")
            sw.WriteLine("[A2OA][/A2OA]")
            sw.WriteLine("[MODS][/MODS]")
            sw.WriteLine("[ARGS][/ARGS]")
            sw.WriteLine("[A3][/A3]")
            sw.WriteLine("[A3MODS][/A3MODS]")
            sw.WriteLine("[A3ARGS][/A3ARGS]")
            sw.Close()
            A2 = ""
            A2OA = ""
            Mods = ""
            Args = ""
            A3 = ""
            A3Args = ""
            A3Mods = ""
        End If

        ArmaDir.Text = A2
        ArmaOADir.Text = A2OA
        ModsLoc.Text = Mods
        Params.Text = Args
        Arma3Dir.Text = A3
        A3ModsLoc.Text = A3Mods
        A3ParamsInput.Text = A3Args
    End Sub

    Sub saveSettings()
        Try
            Dim settingsFile As String = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) & "/Settings.ini"
            Dim sw As New StreamWriter(New FileStream(settingsFile, FileMode.Truncate))
            Dim fs As FileStream = Nothing
            sw.WriteLine("[A2]" & A2 & "[/A2]")
            sw.WriteLine("[A2OA]" & A2OA & "[/A2OA]")
            sw.WriteLine("[MODS]" & Mods & "[/MODS]")
            sw.WriteLine("[ARGS]" & Args & "[/ARGS]")
            sw.WriteLine("[A3]" & A3 & "[/A3]")
            sw.WriteLine("[A3MODS]" & A3Mods & "[/A3MODS]")
            sw.WriteLine("[A3ARGS]" & A3Args & "[/A3ARGS]")
            sw.Close()
        Catch ex As Exception

        End Try
    End Sub

    Sub SplashScreenDone()
        Me.BringToFront()
    End Sub

    Private Sub startarma(ByVal p As String, ByVal armaVersion As Integer)
        If armaVersion = 2 Then
            saveSettings()
            Dim startInfo As New ProcessStartInfo
            startInfo.FileName = A2OA + "\" + "ArmA2OA_BE.exe"
            startInfo.Arguments = p
            Process.Start(startInfo)
        End If
        If armaVersion = 3 Then
            saveSettings()
            Dim startInfo As New ProcessStartInfo
            startInfo.FileName = A3 + "\" + "arma3battleye.exe"
            startInfo.Arguments = p
            Process.Start(startInfo)
        End If
    End Sub

    Private Sub CloseLauncher(sender As Object, e As EventArgs) Handles UICloseBtn.Click
        saveSettings()
        End
    End Sub

    Sub UpdateMaps(ByVal i As SpaceServerItem)
        For Each map As SpaceServerItem In ServerScrollPanel.Controls
            If map Is i Then
                map.Activated = True
                selectedMapLbl.Text = "Map selected: " & map.Title
            Else
                map.Activated = False
            End If
        Next
    End Sub

#Region "NodeCheckbox"

    Private Const TVIF_STATE As Integer = &H8
    Private Const TVIS_STATEIMAGEMASK As Integer = &HF000
    Private Const TV_FIRST As Integer = &H1100
    Private Const TVM_SETITEM As Integer = TV_FIRST + 63

    <StructLayout(LayoutKind.Sequential)>
    Public Structure TVITEM
        Public mask As Integer
        Public hItem As IntPtr
        Public state As Integer
        Public stateMask As Integer
        <MarshalAs(UnmanagedType.LPTStr)> Public lpszText As String
        Public cchTextMax As Integer
        Public iImage As Integer
        Public iSelectedImage As Integer
        Public cChildren As Integer
        Public lParam As IntPtr
    End Structure

    Private Declare Auto Function SendMessage Lib "User32.dll" (ByVal hwnd As IntPtr, ByVal msg As Integer, ByVal wParam As IntPtr, ByRef lParam As TVITEM) As Integer

    Private Sub HideRootCheckBox(ByVal node As TreeNode)
        Dim tvi As New TVITEM
        tvi.hItem = node.Handle
        tvi.mask = TVIF_STATE
        tvi.stateMask = TVIS_STATEIMAGEMASK
        tvi.state = 0
        SendMessage(ModsList.Handle, TVM_SETITEM, IntPtr.Zero, tvi)
    End Sub

#End Region

    Private Sub UITabs_SelectedIndexChanged(sender As Object, e As EventArgs) Handles UITabs.SelectedIndexChanged
        If UITabs.SelectedTab.Text = "About" Then
            UITabs.SelectedIndex = selectedTab
            Dim msg As New OKMsgboxWindow("About", 0, "GhostzGamerz launcher v" & version & " designed in .NET" + vbNewLine + "GhostGamerz © 2015" + vbNewLine + vbNewLine + "Credit to Devvo")
            msg.Height += 10
            msg.ShowDialog()
        End If
        selectedTab = UITabs.SelectedIndex
    End Sub


    Function CheckForProcess(ByVal p As String) As Boolean
        Dim found As Boolean = False
        For Each pro As Process In Process.GetProcesses()
            If pro.ProcessName.StartsWith(p) Then
                found = True
            End If
        Next
        Return found
    End Function

    Sub EndProcess(ByVal p As String)
        For Each pro As Process In Process.GetProcesses()
            If pro.ProcessName.StartsWith(p) Then
                pro.Kill()
            End If
        Next
    End Sub

    Private Sub SpaceButtonGloss2_Click(sender As Object, e As EventArgs) Handles ServerPlay.Click
        If A2 = Nothing Or A2OA = Nothing Or Mods = Nothing Then
            Dim msg As New OKMsgboxWindow("Settings missing", 1, "Go to the settings tab and locate ArmA, ArmA 2 and Mods.")
            msg.ShowDialog()
            Return
        End If
        If CheckForProcess("ArmA2OA") Or CheckForProcess("arma3launcher") Or CheckForProcess("arma3") Then
            Dim msg As New AlreadyRunning()
            msg.ShowDialog()
        Else
            LaunchMap()
        End If
    End Sub

    Sub LaunchMapAndEndProcess()
        EndProcess("ArmA2OA")
        EndProcess("arma3launcher")
        EndProcess("arma3")
        LaunchMap()
    End Sub

    Sub LaunchMap()
        For Each m As SpaceServerItem In ServerScrollPanel.Controls
            If m.Activated Then
                If m.ArmaVersion = "A2" Then
                    Dim A2ClientMods As String = parseClientMods(m.ClientMods)
                    If m.Password = Nothing Then
                        startarma("0 0 -connect=" & m.ServerIP & " -port=" & m.ServerPort & " " + Chr(34) + "-mod=" & A2ClientMods & Chr(34) & " " & Args, 2)
                    Else
                        startarma("0 0 -connect=" & m.ServerIP & " -port=" & m.ServerPort & " " + Chr(34) + "-mod=" & A2ClientMods & Chr(34) & " " & Chr(34) & "-password=" & m.Password & Chr(34) & Args, 2)
                    End If
                End If
                If m.ArmaVersion = "A3" Then
                    Dim A3ClientMods As String = parseClientMods(m.ClientMods)
                    If m.Password = Nothing Then
                        startarma("0 1 -connect=" & m.ServerIP & " -port=" & m.ServerPort & " " + Chr(34) + "-mod=" & A3ClientMods & Chr(34) & " " & Args, 3)
                    Else
                        startarma("0 1-connect=" & m.ServerIP & " -port=" & m.ServerPort & " " + Chr(34) + "-mod=" & A3ClientMods & Chr(34) & " " & Chr(34) & "-password=" & m.Password & Chr(34) & Args, 3)
                    End If
                End If
            End If
        Next
    End Sub

    Function parseClientMods(ByVal rawModsList As String) As String
        Dim outputA2 As String = rawModsList.Replace("[#A2]", A2)
        Dim outputA2OA As String = outputA2.Replace("[#MODS]", Mods)
        Dim outputA3 As String = outputA2OA.Replace("[#A3]", A3)
        Dim outputA3Mods As String = outputA3.Replace("[#A3MODS]", A3Mods)
        Return outputA3Mods
    End Function

    Private Sub SpaceButtonGloss5_Click(sender As Object, e As EventArgs) Handles ModDownloadButton.Click
        Dim i As ServerTreeNode = SelectedNode(ModsList)
        If i Is Nothing Then
            Return
        End If

        Dim modname As String = ModsList.SelectedNode.Text

        Dim downloadlink As String = i.DownloadLink
        If i.RawMod = "@DayZ" Then
            Dim msg As New OKMsgboxWindow("@DayZ", 1, "Use Steam for the @DayZ files.")
            msg.ShowDialog()
            Return
        End If

        Dim t As New Threading.Thread(Sub() DownloadMod(modname, downloadlink, i))
        t.IsBackground = True
        t.Start()
    End Sub

    Private Sub SpaceButtonGloss3_Click(sender As Object, e As EventArgs) Handles ArmaIIBrowse.Click
        Dim fbd As New FolderBrowserDialog
        fbd.ShowDialog()
        ArmaDir.Text = fbd.SelectedPath
        A2 = fbd.SelectedPath
        saveSettings()
    End Sub

    Private Sub ArmaDir_TextChanged(sender As Object, e As EventArgs) Handles ArmaDir.TextChanged
        A2 = ArmaDir.Text
    End Sub

    Private Sub SpaceButtonGloss4_Click(sender As Object, e As EventArgs) Handles ArmaIIOABrowse.Click
        Dim fbd As New FolderBrowserDialog
        fbd.ShowDialog()
        ArmaOADir.Text = fbd.SelectedPath
        A2OA = fbd.SelectedPath
        saveSettings()
    End Sub

    Private Sub ArmaOADir_TextChanged(sender As Object, e As EventArgs) Handles ArmaOADir.TextChanged
        A2OA = ArmaOADir.Text
    End Sub

    Private Sub SpaceButtonGloss6_Click(sender As Object, e As EventArgs) Handles ArmaIIModsBrowse.Click
        Dim fbd As New FolderBrowserDialog
        fbd.ShowDialog()
        ModsLoc.Text = fbd.SelectedPath
        Mods = fbd.SelectedPath
        saveSettings()
    End Sub

    Private Sub SpaceTextBox1_TextChanged(sender As Object, e As EventArgs) Handles ModsLoc.TextChanged
        Mods = ModsLoc.Text
        LoadMods()
    End Sub

    Private Sub SpaceTextBox2_TextChanged(sender As Object, e As EventArgs) Handles Params.TextChanged
        Args = Params.Text
    End Sub

    Private Sub SpaceButtonGloss1_Click(sender As Object, e As EventArgs) Handles AutoDetectModsButton.Click
        'If IO.Directory.Exists("C:\Program Files (x86)\Steam\steamapps\common\Arma 2") Then
        '    ArmaDir.Text = "C:\Program Files (x86)\Steam\steamapps\common\Arma 2"
        '    ArmaOADir.Text = "C:\Program Files (x86)\Steam\steamapps\common\Arma 2 Operation Arrowhead"
        '    ModsLoc.Text = "C:\Program Files (x86)\Steam\steamapps\common\Arma 2 Operation Arrowhead"
        '    Return
        'End If
        Dim t As New Threading.Thread(Sub() LoadArma2Locations())
        t.IsBackground = True
        t.Start()

    End Sub

    Sub LoadArma2Locations()
        SetText("Finding...", ArmaDir)
        SetText("Finding...", ArmaOADir)
        SetText("Finding...", ModsLoc)
        For Each drive As IO.DriveInfo In My.Computer.FileSystem.Drives
            If drive.DriveType = IO.DriveType.Fixed Then

                Dim scan As String = ScanDrive(New DirectoryInfo(drive.Name), drive.Name, 2)
                If Not scan = Nothing And Not scan = "Couldnt Auto Detect." Then

                    A2 = scan
                    A2OA = scan & " operation arrowhead"
                    Mods = scan & " operation arrowhead"

                    SetText(scan, ArmaDir)
                    SetText(scan & " operation arrowhead", ArmaOADir)
                    SetText(scan & " operation arrowhead", ModsLoc)

                    SetText(A2, ArmaDir)
                    SetText(A2OA, ArmaOADir)
                    SetText(Mods, ModsLoc)

                    saveSettings()
                    Return
                Else
                    SetText("Error.", ArmaDir)
                    SetText("Error.", ArmaOADir)
                    SetText("Error.", ModsLoc)
                End If
            End If
        Next
    End Sub

    Sub LoadArma3Locations()
        SetText("Finding...", Arma3Dir)
        SetText("Finding...", A3ModsLoc)
        For Each drive As IO.DriveInfo In My.Computer.FileSystem.Drives
            If drive.DriveType = IO.DriveType.Fixed Then

                Dim scan As String = ScanDrive(New DirectoryInfo(drive.Name), drive.Name, 3)
                If Not scan = Nothing And Not scan = "Couldnt Auto Detect." Then

                    A3 = scan
                    A3Mods = scan

                    SetText(scan, Arma3Dir)
                    SetText(scan, A3ModsLoc)

                    SetText(A3, Arma3Dir)
                    SetText(A3Mods, A3ModsLoc)

                    saveSettings()
                    Return
                Else
                    SetText("Error.", Arma3Dir)
                    SetText("Error.", A3ModsLoc)
                End If
            End If
        Next
    End Sub

    Function ScanDrive(ByVal DIR As DirectoryInfo, ByVal drive As String, ByVal armaVersion As Integer) As String '0 Not found, 5 pf, 10 pf86
        Dim PF As Boolean = False
        Dim x86 As Boolean = Nothing
        Dim steam As Boolean = False
        For Each drivefolder As DirectoryInfo In DIR.GetDirectories()

            If drivefolder.Name.ToLower = "program files" Then 'determine if the drive contains program files
                PF = True
                If drivefolder.Name.ToLower = "program files (x86)" Then 'determine if 64/32 bit layout.
                    x86 = True
                End If
            End If


            If Not x86 = True Then
                Try
                    For Each pffolder As DirectoryInfo In New DirectoryInfo(drive & "program files (x86)").GetDirectories
                        If pffolder.Name.ToLower = "steam" Then
                            steam = True
                            For Each steamfolder As DirectoryInfo In New DirectoryInfo(drive & "program files (x86)/steam").GetDirectories
                                If steamfolder.Name.ToLower = "steamapps" Then
                                    For Each steamappsfolder As DirectoryInfo In New DirectoryInfo(drive & "program files (x86)/steam/steamapps").GetDirectories
                                        If steamappsfolder.Name.ToLower = "common" Then
                                            For Each steamcommonfolder As DirectoryInfo In New DirectoryInfo(drive & "program files (x86)/steam/steamapps/common").GetDirectories
                                                If armaVersion = 2 Then
                                                    If steamcommonfolder.Name.ToLower = "arma 2" Then
                                                        Return drive & "program files (x86)/steam/steamapps/common/arma 2" 'found
                                                    End If
                                                End If
                                                If armaVersion = 3 Then
                                                    If steamcommonfolder.Name.ToLower = "arma 3" Then
                                                        Return drive & "program files (x86)/steam/steamapps/common/arma 3" 'found
                                                    End If
                                                End If
                                            Next
                                        End If
                                    Next
                                End If
                            Next
                        End If
                    Next
                Catch ex As Exception
                    Return "Couldnt Auto Detect."
                End Try
            Else
                Try
                    For Each pffolder As DirectoryInfo In New DirectoryInfo(drive & "program files").GetDirectories
                        If pffolder.Name.ToLower = "steam" Then
                            steam = True
                            For Each steamfolder As DirectoryInfo In New DirectoryInfo(drive & "program files/steam").GetDirectories
                                If steamfolder.Name.ToLower = "steamapps" Then
                                    For Each steamappsfolder As DirectoryInfo In New DirectoryInfo(drive & "program files/steam/steamapps").GetDirectories
                                        If steamappsfolder.Name.ToLower = "common" Then
                                            For Each steamcommonfolder As DirectoryInfo In New DirectoryInfo(drive & "program files/steam/steamapps/common").GetDirectories
                                                If armaVersion = 2 Then
                                                    If steamcommonfolder.Name.ToLower = "arma 2" Then
                                                        Return drive & "program files (x86)/steam/steamapps/common/arma 2" 'found
                                                    End If
                                                End If
                                                If armaVersion = 3 Then
                                                    If steamcommonfolder.Name.ToLower = "arma 3" Then
                                                        Return drive & "program files (x86)/steam/steamapps/common/arma 3" 'found
                                                    End If
                                                End If
                                            Next
                                        End If
                                    Next
                                End If
                            Next
                        End If
                    Next
                Catch ex As Exception
                    Return "Couldnt Auto Detect."
                End Try
            End If

        Next
        If PF = False Then
            Return "Couldnt Auto Detect." 'No program files found.
        End If
        Return scanx86(DIR, drive, armaVersion)
    End Function

    Function scanx86(ByVal DIR As DirectoryInfo, ByVal drive As String, ByVal armaVersion As Integer) As String
        Dim steam As Boolean = False
        Try
            For Each pffolder As DirectoryInfo In New DirectoryInfo(drive & "program files").GetDirectories
                If pffolder.Name.ToLower = "steam" Then
                    steam = True
                    For Each steamfolder As DirectoryInfo In New DirectoryInfo(drive & "program files/steam").GetDirectories
                        If steamfolder.Name.ToLower = "steamapps" Then
                            For Each steamappsfolder As DirectoryInfo In New DirectoryInfo(drive & "program files/steam/steamapps").GetDirectories
                                If steamappsfolder.Name.ToLower = "common" Then
                                    For Each steamcommonfolder As DirectoryInfo In New DirectoryInfo(drive & "program files/steam/steamapps/common").GetDirectories
                                        If armaVersion = 2 Then
                                            If steamcommonfolder.Name.ToLower = "arma 2" Then
                                                Return drive & "program files (x86)/steam/steamapps/common/arma 2" 'found
                                            End If
                                        End If
                                        If armaVersion = 3 Then
                                            If steamcommonfolder.Name.ToLower = "arma 3" Then
                                                Return drive & "program files (x86)/steam/steamapps/common/arma 3" 'found
                                            End If
                                        End If
                                    Next
                                End If
                            Next
                        End If
                    Next
                End If
            Next
        Catch ex As Exception
            Return "Couldnt Auto Detect."
        End Try
    End Function

    Private Sub SpaceButton_Maximize1_Click(sender As Object, e As EventArgs) Handles UIMaximizeBtn.Click
        If Me.WindowState = FormWindowState.Normal Then
            Me.WindowState = FormWindowState.Maximized
        Else
            Me.WindowState = FormWindowState.Normal
        End If
    End Sub

    Private Sub SpaceButton_Minimize1_Click(sender As Object, e As EventArgs) Handles UIMinimizeBtn.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub ServersPanel_SettingsClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles ServersPanel.SettingsClick
        Dim SettingsMenu As New SpaceContextMenu(New Size(200, 200))
        Dim ArmaIIServerButton As New SpaceMenuItem("ArmA II", New Size(100, 23))
        Dim ArmaIIIServerButton As New SpaceMenuItem("ArmA III", New Size(100, 23))
        AddHandler ArmaIIServerButton.Click, AddressOf ChangeMapType_A2
        AddHandler ArmaIIIServerButton.Click, AddressOf ChangeMapType_A3
        SettingsMenu.Items.Add(ArmaIIServerButton)
        SettingsMenu.Items.Add(ArmaIIIServerButton)
        SettingsMenu.Show(New Point(Me.Location.X + ServersPanel.Location.X + ServersPanel.Width - SettingsMenu.Width - 16, Me.Location.Y + ServersPanel.Location.Y + 60))
    End Sub

    Sub ChangeMapType_A2()
        MapType = "A2"
        Dim LoadDataThread As New Threading.Thread(Sub() loadData())
        LoadDataThread.IsBackground = True
        LoadDataThread.Start()
    End Sub
    Sub ChangeMapType_A3()
        MapType = "A3"
        Dim LoadDataThread As New Threading.Thread(Sub() loadData())
        LoadDataThread.IsBackground = True
        LoadDataThread.Start()
    End Sub

    Sub DownloadMod(ByVal modname As String, ByVal modLink As String, ByVal node As ServerTreeNode)
        If node.Downloading = False Then
            If node.Complete Then
                Dim msg As New OKMsgboxWindow("Already installed", 0, "This mod is already installed.")
                msg.ShowDialog()
                Return
            End If
            If node.Downloadable Then
                Dim wfd As New ModDownloader
                AddHandler wfd.AmountDownloadedChanged, AddressOf progressChanged
                AddHandler wfd.FileDownloadFailed, AddressOf failed
                AddHandler wfd.FileDownloadComplete, AddressOf downloadComplete
                node.Downloading = True
                If node.ArmaVersion = 2 Then
                    wfd.DownloadFileWithProgress(modLink, Mods & "/" & "GG" & node.ModName & ".zip", node)
                End If
                If node.ArmaVersion = 3 Then
                    wfd.DownloadFileWithProgress(modLink, A3Mods & "/" & "GG" & node.ModName & ".zip", node)
                End If
            End If
        Else
            Dim msg As New OKMsgboxWindow("Already downloading", 0, "This mod is already currently downloading.")
            msg.ShowDialog()
        End If
    End Sub

    Sub failed(ByVal ex As Exception)
        Dim msg As New OKMsgboxWindow("Error", 1, "Download failed.")
        msg.ShowDialog()
    End Sub

    Sub progressChanged(ByVal iNewProgress As Long, ByVal TotalSize As Long, ByVal obj As ServerTreeNode)
        SetModText(obj.ModName & " - Downloading: " & FormatFileSize(iNewProgress) & "/" & FormatFileSize(TotalSize), obj)
    End Sub

    Sub downloadComplete(ByVal zipfile As String, ByVal node As ServerTreeNode)
        SetModText(node.ModName & " - Extracting...", node)
        UnZipMod(zipfile, Nothing, Mods, node)
    End Sub

    Public Shared Function FormatFileSize(ByVal Size As Long) As String
        Try
            Dim KB As Integer = 1024
            Dim MB As Integer = KB * KB
            If Size < KB Then
                Return (Size.ToString("D") & " bytes")
            Else
                Select Case Size / KB
                    Case Is < 1000
                        Return (Size / KB).ToString("N") & "KB"
                    Case Is < 1000000
                        Return (Size / MB).ToString("N") & "MB"
                    Case Is < 10000000
                        Return (Size / MB / KB).ToString("N") & "GB"
                End Select
            End If
        Catch ex As Exception
            Return Size.ToString
        End Try
        Return Nothing
    End Function

    Public Sub UnZipMod(archiveFilenameIn As String, password As String, outFolder As String, ByVal node As ServerTreeNode)
        Dim zf As ZipFile = Nothing
        Try
            Dim fs As FileStream = File.OpenRead(archiveFilenameIn)
            zf = New ZipFile(fs)

            'AES encrypted entries are handled automatically
            If Not String.IsNullOrEmpty(password) Then
                zf.Password = password
            End If

            For Each zipEntry As ZipEntry In zf
                If Not zipEntry.IsFile Then
                    ' No zip files within mod download so ignore.
                    Continue For
                End If

                Dim entryFileName As String = zipEntry.Name

                Dim buffer As Byte() = New Byte(4095) {}
                ' 4K is optimum
                Dim zipStream As Stream = zf.GetInputStream(zipEntry)

                'Chnage file name output if needed, to avoid conflict with other files on disk.
                Dim fullZipToPath As String = Path.Combine(outFolder, entryFileName)
                Dim directoryName As String = Path.GetDirectoryName(fullZipToPath)
                If directoryName.Length > 0 Then
                    Directory.CreateDirectory(directoryName)
                End If

                ' Unzip file in buffered chunks to conserve RAM.
                Using streamWriter As FileStream = File.Create(fullZipToPath)
                    StreamUtils.Copy(zipStream, streamWriter, buffer)
                End Using
            Next
        Catch ex As Exception
            SetDownloading(False, node, ModsList)
            SetModText(node.ModName & " - Failed.", node)
        Finally
            If zf IsNot Nothing Then
                zf.IsStreamOwner = True
                ' Makes close also shut the underlying stream
                ' Ensure we release resources
                zf.Close()
            End If

            IO.File.Delete(archiveFilenameIn) 'Delete zip

            SetComplete(True, node, ModsList) 'set as complete
            SetDownloading(False, node, ModsList) 'set downloading false
            SetModText(node.ModName & " - Done.", node) 'Reload mods here.
        End Try

        Dim downloadingCount As Integer = 0

        For Each n As ServerTreeNode In GetNodes(ModsList) 'Check to see if any nodes are downloading
            For Each cn As ServerTreeNode In n.Nodes
                If cn.Downloading = True Then
                    downloadingCount += 1 'create a count of nodes that are downloading
                End If
            Next
        Next

        If downloadingCount = 0 Then 'if nodes are downloading then dont load mods.
            LoadMods()
        End If
    End Sub

    Function CheckForDownloadingMods(ByVal control As TreeView)
        Dim downloadingCount As Integer = 0

        For Each n As ServerTreeNode In GetNodes(control) 'Check to see if any nodes are downloading
            For Each cn As ServerTreeNode In n.Nodes
                If cn.Downloading = True Then
                    downloadingCount += 1 'create a count of nodes that are downloading
                End If
            Next
        Next

        If downloadingCount = 0 Then 'if nodes are downloading then dont load mods.
            Return False
        Else
            Return True
        End If
    End Function

    Private Sub ModsGroupBox_SettingsClick(sender As Object, e As EventArgs) Handles ModsGroupBox.SettingsClick
        Dim SettingsMenu As New SpaceContextMenu(New Size(200, 200))
        Dim ReloadModsButton As New SpaceMenuItem("Reload", New Size(100, 23))
        AddHandler ReloadModsButton.Click, AddressOf ReloadMods_Click
        SettingsMenu.Items.Add(ReloadModsButton)
        SettingsMenu.Show(New Point(Me.Location.X + ServersPanel.Location.X + ServersPanel.Width - SettingsMenu.Width - 115, Me.Location.Y + ModsGroupBox.Location.Y + 60))
    End Sub

    Sub ReloadMods_Click()
        Dim t As New Threading.Thread(Sub() ReloadMods())
        t.IsBackground = True
        t.Start()
    End Sub

    Sub ReloadMods()
        If Not CheckForDownloadingMods(ModsList) Then
            LoadMods()
        Else
            Dim msg As New OKMsgboxWindow("Downloads active", 1, "Mods will be reloaded once all downloads are complete.")
            msg.ShowDialog()
        End If
    End Sub

    Private Sub ServerListDown_Click(sender As Object, e As EventArgs) Handles ServerListDown.Click
        MapScrollPosition += 110
        If MapScrollPosition > MapScrollPositionMax Then
            MapScrollPosition = MapScrollPositionMax
        End If
        ServerScrollPanel.AutoScrollPosition = New Point(0, MapScrollPosition)
    End Sub

    Private Sub ServerListUp_Click(sender As Object, e As EventArgs) Handles ServerListUp.Click
        MapScrollPosition -= 110
        If MapScrollPosition < 0 Then
            MapScrollPosition = 0
        End If
        ServerScrollPanel.AutoScrollPosition = New Point(0, MapScrollPosition)
    End Sub

    Private Sub ServersPanel_SettingsClick(sender As Object, e As EventArgs) Handles ServersPanel.SettingsClick

    End Sub

    Private Sub KillboardGroupBox_SettingsClick(sender As Object, e As EventArgs) Handles KillboardGroupBox.SettingsClick
        Dim uKillboardThread As New Threading.Thread(Sub() UpdateKillboard())
        uKillboardThread.IsBackground = True
        uKillboardThread.Start()
    End Sub

    Sub UpdateKillboard()
        Try
            ClearKillboard(Killboard)

            AddKillboardItem(Killboard, "Loading, Please wait...")
            UpdateControl(Killboard)

            Dim WC As New Net.WebClient
            WC.Proxy = Nothing

            Dim content As String = WC.DownloadString("http://" + serverAddress + "/launcherkillboard.php")
            Dim lines() As String = content.Split(New String() {"PKILL:"}, StringSplitOptions.None)

            Dim removeMapLoc As String = Regex.Replace(content, "\w \d-\d-\w:\d ", "")
            Dim removeUID As String = Regex.Replace(removeMapLoc, "\(\d\d\d\d\d\d\d\d\d\d\d\d\d\d\d\d\d\)", "")
            Dim removeRemoteB As String = Regex.Replace(removeUID, "(REMOTE) ", "")
            Dim removeRemote As String = Regex.Replace(removeRemoteB, "REMOTE ", "")

            Dim substrings() As String = Regex.Split(removeRemote, "\d+:\d+:\d+ PKILL: ")
            Dim index As Integer = 0

            ClearKillboard(Killboard)

            For Each match As String In substrings
                If Not index = 0 Then
                    AddKillboardItem(Killboard, match)
                End If
                index += 1
            Next
            UpdateControl(Killboard)
        Catch ex As Exception
            Dim msg As New OKMsgboxWindow("Error", 1, ex.Message)
            msg.ShowDialog()
            ClearKillboard(Killboard)
            AddKillboardItem(Killboard, "Failed to load.")
            UpdateControl(Killboard)
        End Try
    End Sub

    Private Sub DirectionalButton2_Click(sender As Object, e As EventArgs) Handles DirectionalButton2.Click
        Killboard.VScrollValue += 5
    End Sub

    Private Sub DirectionalButton1_Click(sender As Object, e As EventArgs) Handles DirectionalButton1.Click
        Killboard.VScrollValue -= 5
    End Sub

    'Create a GDI panel with a property of a string array
    'for each item in array drawstring(item)
    'use scroll panel method from maps panel on GDI killboard panel.
    '
    'Scratch that, the GDI drawstring wont allow for scrolling as the offset wont change
    'Create a new label control each line and add to the panel.

    Private Sub RulesGroupBox_SettingsClick(sender As Object, e As EventArgs) Handles RulesGroupBox.SettingsClick
        Dim uRulesThread As New Threading.Thread(Sub() updateRules())
        uRulesThread.IsBackground = True
        uRulesThread.Start()
    End Sub

    Sub updateRules()
        Try
            ClearKillboard(Rules)

            AddKillboardItem(Rules, "Loading, Please wait...")
            UpdateControl(Rules)

            Dim WC As New Net.WebClient
            WC.Proxy = Nothing

            Dim content As String = WC.DownloadString("http://" + serverAddress + "/rules.php")
            Dim lines() As String = content.Split(New String() {"<br>"}, StringSplitOptions.None)

            Dim index As Integer = 0

            ClearKillboard(Rules)

            For Each match As String In lines
                AddKillboardItem(Rules, match)
                index += 1
            Next
            UpdateControl(Rules)
        Catch ex As Exception
            ClearKillboard(Rules)
            AddKillboardItem(Rules, "Failed to load.")
            UpdateControl(Rules)
        End Try
    End Sub

    Private Sub DirectionalButton3_Click(sender As Object, e As EventArgs) Handles DirectionalButton3.Click
        Rules.VScrollValue += 5
    End Sub

    Private Sub DirectionalButton4_Click(sender As Object, e As EventArgs) Handles DirectionalButton4.Click
        Rules.VScrollValue -= 5
    End Sub

    Private Sub A3DirBrowse_Click(sender As Object, e As EventArgs) Handles A3DirBrowse.Click
        Dim fbd As New FolderBrowserDialog
        fbd.ShowDialog()
        Arma3Dir.Text = fbd.SelectedPath
        A3 = fbd.SelectedPath
        saveSettings()
    End Sub

    Private Sub A3ModsDir_Click(sender As Object, e As EventArgs) Handles A3ModsDir.Click
        Dim fbd As New FolderBrowserDialog
        fbd.ShowDialog()
        A3ModsLoc.Text = fbd.SelectedPath
        A3Mods = fbd.SelectedPath
        saveSettings()
    End Sub

    Private Sub A3Params_TextChanged(sender As Object, e As EventArgs) Handles A3ParamsInput.TextChanged
        A3Args = Params.Text
    End Sub

    Private Sub A3AutoDetect_Click(sender As Object, e As EventArgs) Handles A3AutoDetect.Click
        Dim t As New Threading.Thread(Sub() LoadArma3Locations())
        t.IsBackground = True
        t.Start()
    End Sub

    Private Sub ModsList_Click(sender As Object, e As MouseEventArgs) Handles ModsList.Click
        If e.Button = Windows.Forms.MouseButtons.Right Then
            Dim modItem As ServerTreeNode = CType(ModsList.SelectedNode, ServerTreeNode)
            If Not IsNothing(modItem) Then
                If modItem.IsMod Then
                    Dim SettingsMenu As New SpaceContextMenu(New Size(200, 200))
                    Dim VerifyModsButton As New SpaceMenuItem("Verify", New Size(100, 23))
                    Dim DeleteModsButton As New SpaceMenuItem("Delete", New Size(100, 23))
                    AddHandler VerifyModsButton.Click, AddressOf WorkInProgress
                    AddHandler DeleteModsButton.Click, AddressOf WorkInProgress
                    SettingsMenu.Items.Add(VerifyModsButton)
                    SettingsMenu.Items.Add(DeleteModsButton)
                    SettingsMenu.Show(New Point(Me.Location.X + UITabs.Location.X + ModsGroupBox.Location.X + e.X + 25, Me.Location.Y + UITabs.Location.Y + ModsGroupBox.Location.Y + e.Y + 8))
                End If
            End If
        End If
    End Sub

    Sub WorkInProgress()
        Dim msg As New OKMsgboxWindow("Work in progress", 1, "Work in progress")
        msg.ShowDialog()
    End Sub

    Private Sub ModImportButton_Click(sender As Object, e As EventArgs) Handles ModImportButton.Click
        Dim im As New ImportMaps
        im.ShowDialog()
    End Sub

    Private Sub ShowMapPlayers_Click(sender As Object, e As EventArgs) Handles ShowMapPlayers.Click
        WorkInProgress()
    End Sub

    Private Sub ModsList_Click(sender As Object, e As EventArgs) Handles ModsList.Click

    End Sub
End Class


