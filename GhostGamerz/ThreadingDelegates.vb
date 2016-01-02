Module ThreadingDelegates

#Region "Threading Delegates"

    Delegate Sub SetTextDelegate(ByVal Text As String, ByVal Control As Control)
    Sub SetText(ByVal Text As String, ByVal Control As Control)
        If Control.InvokeRequired = True Then
            Control.Invoke(New SetTextDelegate(AddressOf SetText), Text, Control)
        Else
            Control.Text = Text
        End If
    End Sub

    Delegate Sub AddNodeDelegate(ByVal Node As ServerTreeNode, ByVal Control As TreeView)
    Sub AddNode(ByVal Node As ServerTreeNode, ByVal Control As TreeView)
        If Control.InvokeRequired = True Then
            Control.Invoke(New AddNodeDelegate(AddressOf AddNode), Node, Control)
        Else
            Control.Nodes.Add(Node)
        End If
    End Sub

    Delegate Sub AddNodeToNodeDelegate(ByVal BaseNode As ServerTreeNode, ByVal Node As ServerTreeNode, ByVal tv As TreeView)
    Sub AddNodeToNode(ByVal BaseNode As ServerTreeNode, ByVal Node As ServerTreeNode, ByVal tv As TreeView)
        If tv.InvokeRequired = True Then
            tv.Invoke(New AddNodeToNodeDelegate(AddressOf AddNodeToNode), BaseNode, Node, tv)
        Else
            BaseNode.Nodes.Add(Node)
        End If
    End Sub

    Delegate Sub ClearNodesDelegate(ByVal Control As TreeView)
    Sub ClearNodes(ByVal Control As TreeView)
        If Control.InvokeRequired = True Then
            Control.Invoke(New ClearNodesDelegate(AddressOf ClearNodes), Control)
        Else
            Control.Nodes.Clear()
        End If
    End Sub

    Delegate Sub SetGameTypeDelegate(ByVal gt As String, ByVal Control As SpaceServerItem)
    Sub SetGameType(ByVal gt As String, ByVal Control As SpaceServerItem)
        If Control.InvokeRequired = True Then
            Control.Invoke(New SetGameTypeDelegate(AddressOf SetGameType), gt, Control)
        Else
            Control.GameType = gt
        End If
    End Sub

    Delegate Sub SetArmaVersionDelegate(ByVal ArmaVersion As String, ByVal Control As SpaceServerItem)
    Sub SetArmaVersion(ByVal ArmaVersion As String, ByVal Control As SpaceServerItem)
        If Control.InvokeRequired = True Then
            Control.Invoke(New SetArmaVersionDelegate(AddressOf SetArmaVersion), ArmaVersion, Control)
        Else
            Control.ArmaVersion = ArmaVersion
        End If
    End Sub

    Delegate Sub SetServerPasswordDelegate(ByVal pass As String, ByVal Control As SpaceServerItem)
    Sub SetPassword(ByVal pass As String, ByVal Control As SpaceServerItem)
        If Control.InvokeRequired = True Then
            Control.Invoke(New SetServerPasswordDelegate(AddressOf SetPassword), pass, Control)
        Else
            Control.GameType = pass
        End If
    End Sub

    Delegate Sub SetIPDelegate(ByVal ip As String, ByVal Control As SpaceServerItem)
    Sub SetIP(ByVal ip As String, ByVal Control As SpaceServerItem)
        If Control.InvokeRequired = True Then
            Control.Invoke(New SetIPDelegate(AddressOf SetIP), ip, Control)
        Else
            Control.ServerIP = ip
        End If
    End Sub

    Delegate Sub SetPortDelegate(ByVal port As Integer, ByVal Control As SpaceServerItem)
    Sub SetPort(ByVal port As Integer, ByVal Control As SpaceServerItem)
        If Control.InvokeRequired = True Then
            Control.Invoke(New SetPortDelegate(AddressOf SetPort), port, Control)
        Else
            Control.ServerPort = port
        End If
    End Sub

    Delegate Sub SetClientModsDelegate(ByVal Mods As String, ByVal Control As SpaceServerItem)
    Sub SetClientMods(ByVal Mods As String, ByVal Control As SpaceServerItem)
        If Control.InvokeRequired = True Then
            Control.Invoke(New SetClientModsDelegate(AddressOf SetClientMods), Mods, Control)
        Else
            Control.ClientMods = Mods
        End If
    End Sub

    Delegate Sub SetPlayersDelegate(ByVal p As Integer, ByVal Control As SpaceServerItem)
    Sub SetPlayers(ByVal p As Integer, ByVal Control As SpaceServerItem)
        If Control.InvokeRequired = True Then
            Control.Invoke(New SetPlayersDelegate(AddressOf SetPlayers), p, Control)
        Else
            Control.Players = p
        End If
    End Sub

    Delegate Sub GetNodesDelegate(ByVal Control As TreeView)
    Function GetNodes(ByVal Control As TreeView) As TreeNodeCollection
        If Control.InvokeRequired = True Then
            Control.Invoke(New GetNodesDelegate(AddressOf GetNodes), Control)
            Return Control.Nodes
        Else
            Return Control.Nodes
        End If
    End Function

    Delegate Sub GetNodesFromNodeDelegate(ByVal node As ServerTreeNode, ByVal tv As TreeView)
    Function GetNodesFromNode(ByVal node As ServerTreeNode, ByVal tv As TreeView) As TreeNodeCollection
        If tv.InvokeRequired = True Then
            tv.Invoke(New GetNodesFromNodeDelegate(AddressOf GetNodesFromNode), node, tv)
            Return node.Nodes
        Else
            Return node.Nodes
        End If
    End Function

    Delegate Sub SetMaxPlayersDelegate(ByVal p As Integer, ByVal Control As SpaceServerItem)
    Sub SetMaxPlayers(ByVal p As Integer, ByVal Control As SpaceServerItem)
        If Control.InvokeRequired = True Then
            Control.Invoke(New SetMaxPlayersDelegate(AddressOf SetMaxPlayers), p, Control)
        Else
            Control.MaxPlayers = p
        End If
    End Sub

    Delegate Sub SetStatusDelegate(ByVal Text As String, ByVal Control As SpaceServerItem)
    Sub SetStatus(ByVal Text As String, ByVal Control As SpaceServerItem)
        If Control.InvokeRequired = True Then
            Control.Invoke(New SetStatusDelegate(AddressOf SetStatus), Text, Control)
        Else
            Control.Status = Text
        End If
    End Sub

    Delegate Sub GetSizeDelegate(ByVal Control As Control)
    Function GetSize(ByVal Control As Control) As Size
        If Control.InvokeRequired = True Then
            Control.Invoke(New GetSizeDelegate(AddressOf GetSize), Control)
            Return Control.Size
        Else
            Return Control.Size
        End If
    End Function

    Delegate Sub SetAutoScrollMinSizeDelegate(ByVal s As Size, ByVal Control As Panel)
    Sub SetAutoScrollMinSize(ByVal s As Size, ByVal Control As Panel)
        If Control.InvokeRequired = True Then
            Control.Invoke(New SetAutoScrollMinSizeDelegate(AddressOf SetAutoScrollMinSize), s, Control)
        Else
            Control.AutoScrollMinSize = s
        End If
    End Sub

    Delegate Sub SetAutoScrollDelegate(ByVal s As Boolean, ByVal Control As Panel)
    Sub SetAutoScroll(ByVal s As Boolean, ByVal Control As Panel)
        If Control.InvokeRequired = True Then
            Control.Invoke(New SetAutoScrollDelegate(AddressOf SetAutoScroll), s, Control)
        Else
            Control.AutoScroll = s
        End If
    End Sub

    Delegate Sub SetDownloadingDelegate(ByVal s As Boolean, ByVal Control As ServerTreeNode, ByVal tv As TreeView)
    Sub SetDownloading(ByVal s As Boolean, ByVal Control As ServerTreeNode, ByVal tv As TreeView)
        If tv.InvokeRequired = True Then
            tv.Invoke(New SetDownloadingDelegate(AddressOf SetDownloading), s, Control, tv)
        Else
            Control.Downloading = s
        End If
    End Sub

    Delegate Sub SetCompleteDelegate(ByVal s As Boolean, ByVal Control As ServerTreeNode, ByVal tv As TreeView)
    Sub SetComplete(ByVal s As Boolean, ByVal Control As ServerTreeNode, ByVal tv As TreeView)
        If tv.InvokeRequired = True Then
            tv.Invoke(New SetCompleteDelegate(AddressOf SetComplete), s, Control, tv)
        Else
            Control.Complete = s
        End If
    End Sub

    Delegate Sub AddControlDelegate(ByVal Child As Control, ByVal Control As Control)
    Sub AddControl(ByVal Child As Control, ByVal Control As Control)
        If Control.InvokeRequired = True Then
            Control.Invoke(New AddControlDelegate(AddressOf AddControl), Child, Control)
        Else
            Control.Controls.Add(Child)
        End If
    End Sub

    Delegate Sub ClearControlsDelegate(ByVal Control As Control)
    Sub ClearControls(ByVal Control As Control)
        If Control.InvokeRequired = True Then
            Control.Invoke(New ClearControlsDelegate(AddressOf ClearControls), Control)
        Else
            Control.Controls.Clear()
        End If
    End Sub

    Delegate Sub SetSizeDelegate(ByVal s As Size, ByVal Control As Control)
    Sub SetSize(ByVal s As Size, ByVal Control As Control)
        If Control.InvokeRequired = True Then
            Control.Invoke(New SetSizeDelegate(AddressOf SetSize), s, Control)
        Else
            Control.Size = s
        End If
    End Sub

    Delegate Sub SetCursorDelegate(ByVal s As Cursor, ByVal Control As Control)
    Sub SetCursor(ByVal s As Cursor, ByVal Control As Control)
        If Control.InvokeRequired = True Then
            Control.Invoke(New SetCursorDelegate(AddressOf SetCursor), s, Control)
        Else
            Control.Cursor = s
        End If
    End Sub

    Delegate Sub SetAnchorDelegate(ByVal a As AnchorStyles, ByVal Control As Control)
    Sub SetAnchor(ByVal a As AnchorStyles, ByVal Control As Control)
        If Control.InvokeRequired = True Then
            Control.Invoke(New SetAnchorDelegate(AddressOf SetAnchor), a, Control)
        Else
            Control.Anchor = a
        End If
    End Sub

    Delegate Sub SetLocationDelegate(ByVal l As Point, ByVal Control As Control)
    Sub SetLocation(ByVal l As Point, ByVal Control As Control)
        If Control.InvokeRequired = True Then
            Control.Invoke(New SetLocationDelegate(AddressOf SetLocation), l, Control)
        Else
            Control.Location = l
        End If
    End Sub

    Delegate Sub SetBorderColourDelegate(ByVal col As Color, ByVal Control As SpaceServerItem)
    Sub SetBorderColor(ByVal col As Color, ByVal Control As SpaceServerItem)
        If Control.InvokeRequired = True Then
            Control.Invoke(New SetBorderColourDelegate(AddressOf SetBorderColor), col, Control)
        Else
            Control.BorderColour = col
        End If
    End Sub

    Delegate Sub SetServerImageDelegate(ByVal img As Image, ByVal Control As SpaceServerItem)
    Sub SetServerImage(ByVal img As Image, ByVal Control As SpaceServerItem)
        If Control.InvokeRequired = True Then
            Control.Invoke(New SetServerImageDelegate(AddressOf SetServerImage), img, Control)
        Else
            Control.ServerImage = img
        End If
    End Sub

    Delegate Sub SetTitleDelegate(ByVal Title As String, ByVal Control As SpaceServerItem)
    Sub SetTitle(ByVal Title As String, ByVal Control As SpaceServerItem)
        If Control.InvokeRequired = True Then
            Control.Invoke(New SetTitleDelegate(AddressOf SetTitle), Title, Control)
        Else
            Control.Title = Title
        End If
    End Sub

    Delegate Sub SetModTextDelegate(ByVal Text As String, ByVal Control As ServerTreeNode)
    Sub SetModText(ByVal Text As String, ByVal Control As ServerTreeNode)
        If Control.TreeView.InvokeRequired = True Then
            Control.TreeView.Invoke(New SetModTextDelegate(AddressOf SetModText), Text, Control)
        Else
            Control.Text = Text
        End If
    End Sub

    Delegate Sub EnableDelegate(ByVal data As Boolean, ByVal Control As Control)
    Sub Enable(ByVal data As Boolean, ByVal Control As Control)
        If Control.InvokeRequired = True Then
            Control.Invoke(New EnableDelegate(AddressOf Enable), data, Control)
        Else
            Control.Enabled = data
        End If
    End Sub

    Delegate Sub selectedNodeDelegate(ByVal Control As TreeView)
    Function SelectedNode(ByVal Control As TreeView) As ServerTreeNode
        If Control.InvokeRequired = True Then
            Control.Invoke(New selectedNodeDelegate(AddressOf SelectedNode), Control)
        Else
            Return CType(Control.SelectedNode, ServerTreeNode)
        End If
        Return Nothing
    End Function

    Delegate Sub treeViewBeginUpdateDelegate(ByVal Control As ServerTreeNode)
    Sub TreeViewBeginUpdate(ByVal Control As ServerTreeNode)
        If Control.TreeView.InvokeRequired = True Then
            Control.TreeView.Invoke(New treeViewBeginUpdateDelegate(AddressOf TreeViewBeginUpdate), Control)
        Else
            Control.TreeView.BeginUpdate()
        End If
    End Sub

    Delegate Sub treeViewEndUpdateDelegate(ByVal Control As ServerTreeNode)
    Sub TreeViewEndUpdate(ByVal Control As ServerTreeNode)
        If Control.TreeView.InvokeRequired = True Then
            Control.TreeView.Invoke(New treeViewEndUpdateDelegate(AddressOf TreeViewEndUpdate), Control)
        Else
            Control.TreeView.EndUpdate()
        End If
    End Sub

    Delegate Sub getTreeviewDelegate(ByVal Control As TreeView)
    Function GetTreeView(ByVal Control As TreeView)
        If Control.InvokeRequired = True Then
            Control.Invoke(New getTreeviewDelegate(AddressOf GetTreeView), Control)
        Else
            Return Control
        End If
        Return Nothing
    End Function

    Delegate Sub AddKillboardItemDelegate(ByVal Control As SpaceKillboard, ByVal item As String)
    Function AddKillboardItem(ByVal Control As SpaceKillboard, ByVal item As String)
        If Control.InvokeRequired = True Then
            Control.Invoke(New AddKillboardItemDelegate(AddressOf AddKillboardItem), Control, item)
        Else
            Control.Items.Add(item)
        End If
        Return Nothing
    End Function

    Delegate Sub UpdateControlDelegate(ByVal Control As SpaceKillboard)
    Function UpdateControl(ByVal Control As SpaceKillboard)
        If Control.InvokeRequired = True Then
            Control.Invoke(New UpdateControlDelegate(AddressOf UpdateControl), Control)
        Else
            Control.Invalidate()
        End If
        Return Nothing
    End Function

    Delegate Sub ClearKillboardDelegate(ByVal Control As SpaceKillboard)
    Function ClearKillboard(ByVal Control As SpaceKillboard)
        If Control.InvokeRequired = True Then
            Control.Invoke(New ClearKillboardDelegate(AddressOf ClearKillboard), Control)
        Else
            Control.Items.Clear()
        End If
        Return Nothing
    End Function

#End Region

End Module
