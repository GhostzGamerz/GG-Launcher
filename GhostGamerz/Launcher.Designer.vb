<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Launcher
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Launcher))
        Me.UITheme = New GhostGamerz.SpaceTheme()
        Me.UIMinimizeBtn = New GhostGamerz.SpaceButton_Minimize()
        Me.UIMaximizeBtn = New GhostGamerz.SpaceButton_Maximize()
        Me.UICloseBtn = New GhostGamerz.SpaceButton_Close()
        Me.UITabs = New GhostGamerz.SpaceTabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.ServersPanel = New GhostGamerz.SpaceGroupBox()
        Me.ShowMapPlayers = New GhostGamerz.SpaceButtonGloss()
        Me.ServerListDown = New GhostGamerz.DirectionalButton()
        Me.ServerListUp = New GhostGamerz.DirectionalButton()
        Me.ServerScrollPanel = New System.Windows.Forms.Panel()
        Me.ChernoPanel = New GhostGamerz.SpacePanel()
        Me.ChernoDesc = New GhostGamerz.SpaceLabel()
        Me.ChernoImage = New System.Windows.Forms.PictureBox()
        Me.ChernoTitle = New GhostGamerz.SpaceLabel()
        Me.NapfPanel = New GhostGamerz.SpacePanel()
        Me.NapfDesc = New GhostGamerz.SpaceLabel()
        Me.NapfImage = New System.Windows.Forms.PictureBox()
        Me.NapfTitle = New GhostGamerz.SpaceLabel()
        Me.TaviPanel = New GhostGamerz.SpacePanel()
        Me.TaviDesc = New GhostGamerz.SpaceLabel()
        Me.TaviImage = New System.Windows.Forms.PictureBox()
        Me.TaviTitle = New GhostGamerz.SpaceLabel()
        Me.ServerPlay = New GhostGamerz.SpaceButtonGloss()
        Me.selectedMapLbl = New GhostGamerz.SpaceLabel()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.SpaceGroupBox1 = New GhostGamerz.SpaceGroupBox()
        Me.SpaceLabel1 = New GhostGamerz.SpaceLabel()
        Me.A3ParamsInput = New GhostGamerz.SpaceTextBox()
        Me.A3ModsDir = New GhostGamerz.SpaceButtonGloss()
        Me.A3ModsLoc = New GhostGamerz.SpaceTextBox()
        Me.SpaceLabel2 = New GhostGamerz.SpaceLabel()
        Me.A3DirBrowse = New GhostGamerz.SpaceButtonGloss()
        Me.A3AutoDetect = New GhostGamerz.SpaceButtonGloss()
        Me.SpaceLabel4 = New GhostGamerz.SpaceLabel()
        Me.Arma3Dir = New GhostGamerz.SpaceTextBox()
        Me.ModsGroupBox = New GhostGamerz.SpaceGroupBox()
        Me.ModImportButton = New GhostGamerz.SpaceButtonGloss()
        Me.modstatus = New GhostGamerz.SpaceLabel()
        Me.ModDownloadButton = New GhostGamerz.SpaceButtonGloss()
        Me.ModsList = New GhostGamerz.DoubleBufferedTreeview()
        Me.ArmaIILocGroupBox = New GhostGamerz.SpaceGroupBox()
        Me.ParamsLbl = New GhostGamerz.SpaceLabel()
        Me.Params = New GhostGamerz.SpaceTextBox()
        Me.ArmaIIModsBrowse = New GhostGamerz.SpaceButtonGloss()
        Me.ModsLoc = New GhostGamerz.SpaceTextBox()
        Me.ModsLocLbl = New GhostGamerz.SpaceLabel()
        Me.ArmaIIOABrowse = New GhostGamerz.SpaceButtonGloss()
        Me.ArmaIIBrowse = New GhostGamerz.SpaceButtonGloss()
        Me.AutoDetectModsButton = New GhostGamerz.SpaceButtonGloss()
        Me.ArmaOADirLbl = New GhostGamerz.SpaceLabel()
        Me.ArmaOADir = New GhostGamerz.SpaceTextBox()
        Me.ArmaDirLbl = New GhostGamerz.SpaceLabel()
        Me.ArmaDir = New GhostGamerz.SpaceTextBox()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.KillboardGroupBox = New GhostGamerz.SpaceGroupBox()
        Me.Killboard = New GhostGamerz.SpaceKillboard()
        Me.DirectionalButton2 = New GhostGamerz.DirectionalButton()
        Me.DirectionalButton1 = New GhostGamerz.DirectionalButton()
        Me.TabPage5 = New System.Windows.Forms.TabPage()
        Me.RulesGroupBox = New GhostGamerz.SpaceGroupBox()
        Me.Rules = New GhostGamerz.SpaceKillboard()
        Me.DirectionalButton3 = New GhostGamerz.DirectionalButton()
        Me.DirectionalButton4 = New GhostGamerz.DirectionalButton()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.SpaceKillboard1 = New GhostGamerz.SpaceKillboard()
        Me.UITheme.SuspendLayout()
        Me.UITabs.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.ServersPanel.SuspendLayout()
        Me.ServerScrollPanel.SuspendLayout()
        Me.ChernoPanel.SuspendLayout()
        CType(Me.ChernoImage, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.NapfPanel.SuspendLayout()
        CType(Me.NapfImage, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TaviPanel.SuspendLayout()
        CType(Me.TaviImage, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        Me.SpaceGroupBox1.SuspendLayout()
        Me.ModsGroupBox.SuspendLayout()
        Me.ArmaIILocGroupBox.SuspendLayout()
        Me.TabPage4.SuspendLayout()
        Me.KillboardGroupBox.SuspendLayout()
        Me.TabPage5.SuspendLayout()
        Me.RulesGroupBox.SuspendLayout()
        Me.SuspendLayout()
        '
        'UITheme
        '
        Me.UITheme.BackColor = System.Drawing.Color.FromArgb(CType(CType(14, Byte), Integer), CType(CType(14, Byte), Integer), CType(CType(14, Byte), Integer))
        Me.UITheme.BorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.UITheme.Colors = New GhostGamerz.Bloom(-1) {}
        Me.UITheme.Controls.Add(Me.UIMinimizeBtn)
        Me.UITheme.Controls.Add(Me.UIMaximizeBtn)
        Me.UITheme.Controls.Add(Me.UICloseBtn)
        Me.UITheme.Controls.Add(Me.UITabs)
        Me.UITheme.Customization = ""
        Me.UITheme.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UITheme.Font = New System.Drawing.Font("Verdana", 8.0!)
        Me.UITheme.Image = Nothing
        Me.UITheme.Location = New System.Drawing.Point(0, 0)
        Me.UITheme.MaxSize = New System.Drawing.Size(0, 0)
        Me.UITheme.MinimumSize = New System.Drawing.Size(482, 540)
        Me.UITheme.MinSize = New System.Drawing.Size(482, 540)
        Me.UITheme.Movable = True
        Me.UITheme.Name = "UITheme"
        Me.UITheme.NoRounding = False
        Me.UITheme.Sizable = True
        Me.UITheme.Size = New System.Drawing.Size(482, 593)
        Me.UITheme.SmartBounds = True
        Me.UITheme.TabIndex = 0
        Me.UITheme.Text = "[GG] Ghostz Gamerz Launcher"
        Me.UITheme.TitleForeColour = System.Drawing.Color.White
        Me.UITheme.TransparencyKey = System.Drawing.Color.DeepPink
        Me.UITheme.Transparent = False
        '
        'UIMinimizeBtn
        '
        Me.UIMinimizeBtn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UIMinimizeBtn.Colors = New GhostGamerz.Bloom(-1) {}
        Me.UIMinimizeBtn.Customization = ""
        Me.UIMinimizeBtn.Font = New System.Drawing.Font("Verdana", 8.0!)
        Me.UIMinimizeBtn.ForeColour = System.Drawing.Color.White
        Me.UIMinimizeBtn.Image = Nothing
        Me.UIMinimizeBtn.Location = New System.Drawing.Point(423, 3)
        Me.UIMinimizeBtn.Name = "UIMinimizeBtn"
        Me.UIMinimizeBtn.NoRounding = False
        Me.UIMinimizeBtn.Size = New System.Drawing.Size(19, 19)
        Me.UIMinimizeBtn.TabIndex = 4
        Me.UIMinimizeBtn.Text = "SpaceButton_Minimize1"
        Me.UIMinimizeBtn.Transparent = False
        '
        'UIMaximizeBtn
        '
        Me.UIMaximizeBtn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UIMaximizeBtn.Colors = New GhostGamerz.Bloom(-1) {}
        Me.UIMaximizeBtn.Customization = ""
        Me.UIMaximizeBtn.Font = New System.Drawing.Font("Verdana", 8.0!)
        Me.UIMaximizeBtn.ForeColour = System.Drawing.Color.White
        Me.UIMaximizeBtn.Image = Nothing
        Me.UIMaximizeBtn.Location = New System.Drawing.Point(441, 3)
        Me.UIMaximizeBtn.Name = "UIMaximizeBtn"
        Me.UIMaximizeBtn.NoRounding = False
        Me.UIMaximizeBtn.Size = New System.Drawing.Size(19, 19)
        Me.UIMaximizeBtn.TabIndex = 3
        Me.UIMaximizeBtn.Text = "SpaceButton_Maximize1"
        Me.UIMaximizeBtn.Transparent = False
        '
        'UICloseBtn
        '
        Me.UICloseBtn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UICloseBtn.Colors = New GhostGamerz.Bloom(-1) {}
        Me.UICloseBtn.Customization = ""
        Me.UICloseBtn.Font = New System.Drawing.Font("Verdana", 8.0!)
        Me.UICloseBtn.ForeColour = System.Drawing.Color.White
        Me.UICloseBtn.Image = Nothing
        Me.UICloseBtn.Location = New System.Drawing.Point(459, 3)
        Me.UICloseBtn.Name = "UICloseBtn"
        Me.UICloseBtn.NoRounding = False
        Me.UICloseBtn.Size = New System.Drawing.Size(19, 19)
        Me.UICloseBtn.TabIndex = 2
        Me.UICloseBtn.Text = "SpaceButton_Close1"
        Me.UICloseBtn.Transparent = False
        '
        'UITabs
        '
        Me.UITabs.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UITabs.Controls.Add(Me.TabPage1)
        Me.UITabs.Controls.Add(Me.TabPage2)
        Me.UITabs.Controls.Add(Me.TabPage4)
        Me.UITabs.Controls.Add(Me.TabPage5)
        Me.UITabs.Controls.Add(Me.TabPage3)
        Me.UITabs.Location = New System.Drawing.Point(12, 35)
        Me.UITabs.Name = "UITabs"
        Me.UITabs.SelectedIndex = 0
        Me.UITabs.Size = New System.Drawing.Size(458, 546)
        Me.UITabs.TabIndex = 1
        Me.UITabs.TabTextColor = System.Drawing.Color.White
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.Color.FromArgb(CType(CType(24, Byte), Integer), CType(CType(24, Byte), Integer), CType(CType(24, Byte), Integer))
        Me.TabPage1.Controls.Add(Me.ServersPanel)
        Me.TabPage1.Location = New System.Drawing.Point(4, 25)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(450, 517)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Severs"
        '
        'ServersPanel
        '
        Me.ServersPanel.ActiveTitleForeColour = System.Drawing.Color.White
        Me.ServersPanel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ServersPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(24, Byte), Integer), CType(CType(24, Byte), Integer), CType(CType(24, Byte), Integer))
        Me.ServersPanel.BorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.ServersPanel.Colors = New GhostGamerz.Bloom(-1) {}
        Me.ServersPanel.Controls.Add(Me.ShowMapPlayers)
        Me.ServersPanel.Controls.Add(Me.ServerListDown)
        Me.ServersPanel.Controls.Add(Me.ServerListUp)
        Me.ServersPanel.Controls.Add(Me.ServerScrollPanel)
        Me.ServersPanel.Controls.Add(Me.ServerPlay)
        Me.ServersPanel.Controls.Add(Me.selectedMapLbl)
        Me.ServersPanel.Customization = ""
        Me.ServersPanel.Font = New System.Drawing.Font("Verdana", 8.0!)
        Me.ServersPanel.Image = Nothing
        Me.ServersPanel.InactiveTitleForeColour = System.Drawing.Color.Red
        Me.ServersPanel.Location = New System.Drawing.Point(6, 6)
        Me.ServersPanel.Movable = True
        Me.ServersPanel.Name = "ServersPanel"
        Me.ServersPanel.NoRounding = False
        Me.ServersPanel.SettingsButton = True
        Me.ServersPanel.SettingsImage = Global.GhostGamerz.My.Resources.Resources.settings
        Me.ServersPanel.Sizable = True
        Me.ServersPanel.Size = New System.Drawing.Size(438, 505)
        Me.ServersPanel.SmartBounds = True
        Me.ServersPanel.TabIndex = 0
        Me.ServersPanel.Text = "Maps"
        Me.ServersPanel.TransparencyKey = System.Drawing.Color.Empty
        Me.ServersPanel.Transparent = False
        '
        'ShowMapPlayers
        '
        Me.ShowMapPlayers.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ShowMapPlayers.BaseColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.ShowMapPlayers.ClickedColor = System.Drawing.Color.DeepSkyBlue
        Me.ShowMapPlayers.Colors = New GhostGamerz.Bloom(-1) {}
        Me.ShowMapPlayers.Customization = ""
        Me.ShowMapPlayers.Font = New System.Drawing.Font("Verdana", 8.0!)
        Me.ShowMapPlayers.HighlightColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(151, Byte), Integer), CType(CType(205, Byte), Integer))
        Me.ShowMapPlayers.Image = Nothing
        Me.ShowMapPlayers.Location = New System.Drawing.Point(238, 467)
        Me.ShowMapPlayers.Name = "ShowMapPlayers"
        Me.ShowMapPlayers.NoRounding = False
        Me.ShowMapPlayers.Size = New System.Drawing.Size(88, 23)
        Me.ShowMapPlayers.TabIndex = 16
        Me.ShowMapPlayers.Text = "Players"
        Me.ShowMapPlayers.Transparent = False
        '
        'ServerListDown
        '
        Me.ServerListDown.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ServerListDown.BackColor = System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer))
        Me.ServerListDown.Direction = GhostGamerz.DirectionalButton.DirectionType.Down
        Me.ServerListDown.Location = New System.Drawing.Point(16, 446)
        Me.ServerListDown.Name = "ServerListDown"
        Me.ServerListDown.Size = New System.Drawing.Size(407, 15)
        Me.ServerListDown.TabIndex = 15
        '
        'ServerListUp
        '
        Me.ServerListUp.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ServerListUp.BackColor = System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer))
        Me.ServerListUp.Direction = GhostGamerz.DirectionalButton.DirectionType.Up
        Me.ServerListUp.Location = New System.Drawing.Point(16, 33)
        Me.ServerListUp.Name = "ServerListUp"
        Me.ServerListUp.Size = New System.Drawing.Size(407, 15)
        Me.ServerListUp.TabIndex = 14
        '
        'ServerScrollPanel
        '
        Me.ServerScrollPanel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ServerScrollPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer))
        Me.ServerScrollPanel.Controls.Add(Me.ChernoPanel)
        Me.ServerScrollPanel.Controls.Add(Me.NapfPanel)
        Me.ServerScrollPanel.Controls.Add(Me.TaviPanel)
        Me.ServerScrollPanel.Location = New System.Drawing.Point(16, 54)
        Me.ServerScrollPanel.Name = "ServerScrollPanel"
        Me.ServerScrollPanel.Size = New System.Drawing.Size(407, 386)
        Me.ServerScrollPanel.TabIndex = 13
        '
        'ChernoPanel
        '
        Me.ChernoPanel.Activated = True
        Me.ChernoPanel.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ChernoPanel.BorderColour = System.Drawing.Color.DeepSkyBlue
        Me.ChernoPanel.BorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.ChernoPanel.Colors = New GhostGamerz.Bloom(-1) {}
        Me.ChernoPanel.Controls.Add(Me.ChernoDesc)
        Me.ChernoPanel.Controls.Add(Me.ChernoImage)
        Me.ChernoPanel.Controls.Add(Me.ChernoTitle)
        Me.ChernoPanel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ChernoPanel.Customization = ""
        Me.ChernoPanel.Font = New System.Drawing.Font("Verdana", 8.0!)
        Me.ChernoPanel.Image = Nothing
        Me.ChernoPanel.Location = New System.Drawing.Point(3, 30)
        Me.ChernoPanel.Movable = True
        Me.ChernoPanel.Name = "ChernoPanel"
        Me.ChernoPanel.NoRounding = False
        Me.ChernoPanel.Sizable = True
        Me.ChernoPanel.Size = New System.Drawing.Size(402, 105)
        Me.ChernoPanel.SmartBounds = True
        Me.ChernoPanel.TabIndex = 10
        Me.ChernoPanel.Text = "SpacePanel1"
        Me.ChernoPanel.TransparencyKey = System.Drawing.Color.Empty
        Me.ChernoPanel.Transparent = False
        '
        'ChernoDesc
        '
        Me.ChernoDesc.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ChernoDesc.AutoSize = True
        Me.ChernoDesc.BackColor = System.Drawing.Color.Transparent
        Me.ChernoDesc.ForeColor = System.Drawing.Color.Gray
        Me.ChernoDesc.Location = New System.Drawing.Point(99, 33)
        Me.ChernoDesc.Name = "ChernoDesc"
        Me.ChernoDesc.Size = New System.Drawing.Size(35, 13)
        Me.ChernoDesc.TabIndex = 3
        Me.ChernoDesc.Text = "Desc"
        '
        'ChernoImage
        '
        Me.ChernoImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ChernoImage.Image = Global.GhostGamerz.My.Resources.Resources.Cherno
        Me.ChernoImage.Location = New System.Drawing.Point(13, 14)
        Me.ChernoImage.Name = "ChernoImage"
        Me.ChernoImage.Size = New System.Drawing.Size(79, 76)
        Me.ChernoImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.ChernoImage.TabIndex = 2
        Me.ChernoImage.TabStop = False
        '
        'ChernoTitle
        '
        Me.ChernoTitle.AutoSize = True
        Me.ChernoTitle.BackColor = System.Drawing.Color.Transparent
        Me.ChernoTitle.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChernoTitle.ForeColor = System.Drawing.Color.White
        Me.ChernoTitle.Location = New System.Drawing.Point(98, 16)
        Me.ChernoTitle.Name = "ChernoTitle"
        Me.ChernoTitle.Size = New System.Drawing.Size(74, 13)
        Me.ChernoTitle.TabIndex = 0
        Me.ChernoTitle.Text = "Chernarus"
        '
        'NapfPanel
        '
        Me.NapfPanel.Activated = False
        Me.NapfPanel.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.NapfPanel.BorderColour = System.Drawing.Color.DeepSkyBlue
        Me.NapfPanel.BorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.NapfPanel.Colors = New GhostGamerz.Bloom(-1) {}
        Me.NapfPanel.Controls.Add(Me.NapfDesc)
        Me.NapfPanel.Controls.Add(Me.NapfImage)
        Me.NapfPanel.Controls.Add(Me.NapfTitle)
        Me.NapfPanel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.NapfPanel.Customization = ""
        Me.NapfPanel.Font = New System.Drawing.Font("Verdana", 8.0!)
        Me.NapfPanel.Image = Nothing
        Me.NapfPanel.Location = New System.Drawing.Point(3, 252)
        Me.NapfPanel.Movable = True
        Me.NapfPanel.Name = "NapfPanel"
        Me.NapfPanel.NoRounding = False
        Me.NapfPanel.Sizable = True
        Me.NapfPanel.Size = New System.Drawing.Size(402, 105)
        Me.NapfPanel.SmartBounds = True
        Me.NapfPanel.TabIndex = 12
        Me.NapfPanel.Text = "SpacePanel3"
        Me.NapfPanel.TransparencyKey = System.Drawing.Color.Empty
        Me.NapfPanel.Transparent = False
        '
        'NapfDesc
        '
        Me.NapfDesc.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.NapfDesc.AutoSize = True
        Me.NapfDesc.BackColor = System.Drawing.Color.Transparent
        Me.NapfDesc.ForeColor = System.Drawing.Color.Gray
        Me.NapfDesc.Location = New System.Drawing.Point(99, 33)
        Me.NapfDesc.Name = "NapfDesc"
        Me.NapfDesc.Size = New System.Drawing.Size(35, 13)
        Me.NapfDesc.TabIndex = 3
        Me.NapfDesc.Text = "Desc"
        '
        'NapfImage
        '
        Me.NapfImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.NapfImage.Image = Global.GhostGamerz.My.Resources.Resources.napf
        Me.NapfImage.Location = New System.Drawing.Point(13, 14)
        Me.NapfImage.Name = "NapfImage"
        Me.NapfImage.Size = New System.Drawing.Size(79, 76)
        Me.NapfImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.NapfImage.TabIndex = 2
        Me.NapfImage.TabStop = False
        '
        'NapfTitle
        '
        Me.NapfTitle.AutoSize = True
        Me.NapfTitle.BackColor = System.Drawing.Color.Transparent
        Me.NapfTitle.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NapfTitle.ForeColor = System.Drawing.Color.White
        Me.NapfTitle.Location = New System.Drawing.Point(98, 16)
        Me.NapfTitle.Name = "NapfTitle"
        Me.NapfTitle.Size = New System.Drawing.Size(37, 13)
        Me.NapfTitle.TabIndex = 0
        Me.NapfTitle.Text = "Napf"
        '
        'TaviPanel
        '
        Me.TaviPanel.Activated = False
        Me.TaviPanel.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TaviPanel.BorderColour = System.Drawing.Color.DeepSkyBlue
        Me.TaviPanel.BorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.TaviPanel.Colors = New GhostGamerz.Bloom(-1) {}
        Me.TaviPanel.Controls.Add(Me.TaviDesc)
        Me.TaviPanel.Controls.Add(Me.TaviImage)
        Me.TaviPanel.Controls.Add(Me.TaviTitle)
        Me.TaviPanel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.TaviPanel.Customization = ""
        Me.TaviPanel.Font = New System.Drawing.Font("Verdana", 8.0!)
        Me.TaviPanel.Image = Nothing
        Me.TaviPanel.Location = New System.Drawing.Point(3, 141)
        Me.TaviPanel.Movable = True
        Me.TaviPanel.Name = "TaviPanel"
        Me.TaviPanel.NoRounding = False
        Me.TaviPanel.Sizable = True
        Me.TaviPanel.Size = New System.Drawing.Size(402, 105)
        Me.TaviPanel.SmartBounds = True
        Me.TaviPanel.TabIndex = 11
        Me.TaviPanel.Text = "SpacePanel2"
        Me.TaviPanel.TransparencyKey = System.Drawing.Color.Empty
        Me.TaviPanel.Transparent = False
        '
        'TaviDesc
        '
        Me.TaviDesc.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TaviDesc.AutoSize = True
        Me.TaviDesc.BackColor = System.Drawing.Color.Transparent
        Me.TaviDesc.ForeColor = System.Drawing.Color.Gray
        Me.TaviDesc.Location = New System.Drawing.Point(99, 33)
        Me.TaviDesc.Name = "TaviDesc"
        Me.TaviDesc.Size = New System.Drawing.Size(35, 13)
        Me.TaviDesc.TabIndex = 7
        Me.TaviDesc.Text = "Desc"
        '
        'TaviImage
        '
        Me.TaviImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TaviImage.Image = Global.GhostGamerz.My.Resources.Resources.tavi
        Me.TaviImage.Location = New System.Drawing.Point(13, 14)
        Me.TaviImage.Name = "TaviImage"
        Me.TaviImage.Size = New System.Drawing.Size(79, 76)
        Me.TaviImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.TaviImage.TabIndex = 2
        Me.TaviImage.TabStop = False
        '
        'TaviTitle
        '
        Me.TaviTitle.AutoSize = True
        Me.TaviTitle.BackColor = System.Drawing.Color.Transparent
        Me.TaviTitle.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TaviTitle.ForeColor = System.Drawing.Color.White
        Me.TaviTitle.Location = New System.Drawing.Point(98, 16)
        Me.TaviTitle.Name = "TaviTitle"
        Me.TaviTitle.Size = New System.Drawing.Size(59, 13)
        Me.TaviTitle.TabIndex = 0
        Me.TaviTitle.Text = "Taviana"
        '
        'ServerPlay
        '
        Me.ServerPlay.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ServerPlay.BaseColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.ServerPlay.ClickedColor = System.Drawing.Color.DeepSkyBlue
        Me.ServerPlay.Colors = New GhostGamerz.Bloom(-1) {}
        Me.ServerPlay.Customization = ""
        Me.ServerPlay.Font = New System.Drawing.Font("Verdana", 8.0!)
        Me.ServerPlay.HighlightColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(151, Byte), Integer), CType(CType(205, Byte), Integer))
        Me.ServerPlay.Image = Nothing
        Me.ServerPlay.Location = New System.Drawing.Point(332, 467)
        Me.ServerPlay.Name = "ServerPlay"
        Me.ServerPlay.NoRounding = False
        Me.ServerPlay.Size = New System.Drawing.Size(88, 23)
        Me.ServerPlay.TabIndex = 6
        Me.ServerPlay.Text = "Play"
        Me.ServerPlay.Transparent = False
        '
        'selectedMapLbl
        '
        Me.selectedMapLbl.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.selectedMapLbl.AutoSize = True
        Me.selectedMapLbl.BackColor = System.Drawing.Color.Transparent
        Me.selectedMapLbl.ForeColor = System.Drawing.Color.Gray
        Me.selectedMapLbl.Location = New System.Drawing.Point(13, 472)
        Me.selectedMapLbl.Name = "selectedMapLbl"
        Me.selectedMapLbl.Size = New System.Drawing.Size(119, 13)
        Me.selectedMapLbl.TabIndex = 4
        Me.selectedMapLbl.Text = "Map selected: None"
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.Color.FromArgb(CType(CType(24, Byte), Integer), CType(CType(24, Byte), Integer), CType(CType(24, Byte), Integer))
        Me.TabPage2.Controls.Add(Me.SpaceGroupBox1)
        Me.TabPage2.Controls.Add(Me.ModsGroupBox)
        Me.TabPage2.Controls.Add(Me.ArmaIILocGroupBox)
        Me.TabPage2.Location = New System.Drawing.Point(4, 25)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(450, 517)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Settings"
        '
        'SpaceGroupBox1
        '
        Me.SpaceGroupBox1.ActiveTitleForeColour = System.Drawing.Color.White
        Me.SpaceGroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SpaceGroupBox1.BorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.SpaceGroupBox1.Colors = New GhostGamerz.Bloom(-1) {}
        Me.SpaceGroupBox1.Controls.Add(Me.SpaceLabel1)
        Me.SpaceGroupBox1.Controls.Add(Me.A3ParamsInput)
        Me.SpaceGroupBox1.Controls.Add(Me.A3ModsDir)
        Me.SpaceGroupBox1.Controls.Add(Me.A3ModsLoc)
        Me.SpaceGroupBox1.Controls.Add(Me.SpaceLabel2)
        Me.SpaceGroupBox1.Controls.Add(Me.A3DirBrowse)
        Me.SpaceGroupBox1.Controls.Add(Me.A3AutoDetect)
        Me.SpaceGroupBox1.Controls.Add(Me.SpaceLabel4)
        Me.SpaceGroupBox1.Controls.Add(Me.Arma3Dir)
        Me.SpaceGroupBox1.Customization = ""
        Me.SpaceGroupBox1.Font = New System.Drawing.Font("Verdana", 8.0!)
        Me.SpaceGroupBox1.Image = Nothing
        Me.SpaceGroupBox1.InactiveTitleForeColour = System.Drawing.Color.Red
        Me.SpaceGroupBox1.Location = New System.Drawing.Point(6, 159)
        Me.SpaceGroupBox1.Movable = True
        Me.SpaceGroupBox1.Name = "SpaceGroupBox1"
        Me.SpaceGroupBox1.NoRounding = False
        Me.SpaceGroupBox1.SettingsButton = False
        Me.SpaceGroupBox1.SettingsImage = Nothing
        Me.SpaceGroupBox1.Sizable = True
        Me.SpaceGroupBox1.Size = New System.Drawing.Size(438, 118)
        Me.SpaceGroupBox1.SmartBounds = True
        Me.SpaceGroupBox1.TabIndex = 15
        Me.SpaceGroupBox1.Text = "ArmA III Location"
        Me.SpaceGroupBox1.TransparencyKey = System.Drawing.Color.Empty
        Me.SpaceGroupBox1.Transparent = False
        '
        'SpaceLabel1
        '
        Me.SpaceLabel1.AutoSize = True
        Me.SpaceLabel1.BackColor = System.Drawing.Color.Transparent
        Me.SpaceLabel1.ForeColor = System.Drawing.Color.White
        Me.SpaceLabel1.Location = New System.Drawing.Point(18, 87)
        Me.SpaceLabel1.Name = "SpaceLabel1"
        Me.SpaceLabel1.Size = New System.Drawing.Size(82, 13)
        Me.SpaceLabel1.TabIndex = 14
        Me.SpaceLabel1.Text = "Parameters: "
        '
        'A3ParamsInput
        '
        Me.A3ParamsInput.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.A3ParamsInput.Colors = New GhostGamerz.Bloom(-1) {}
        Me.A3ParamsInput.Customization = ""
        Me.A3ParamsInput.Font = New System.Drawing.Font("Verdana", 8.0!)
        Me.A3ParamsInput.ForeColour = System.Drawing.Color.White
        Me.A3ParamsInput.Image = Nothing
        Me.A3ParamsInput.Location = New System.Drawing.Point(103, 84)
        Me.A3ParamsInput.MaxLength = "32767"
        Me.A3ParamsInput.Multilined = False
        Me.A3ParamsInput.Name = "A3ParamsInput"
        Me.A3ParamsInput.NoRounding = False
        Me.A3ParamsInput.PasswordChar = Nothing
        Me.A3ParamsInput.Read_Only = False
        Me.A3ParamsInput.ScrollBars = GhostGamerz.SpaceTextBox.ScrollBar.None
        Me.A3ParamsInput.Size = New System.Drawing.Size(223, 20)
        Me.A3ParamsInput.TabIndex = 13
        Me.A3ParamsInput.Transparent = False
        Me.A3ParamsInput.UseSystemPasswordChar = False
        '
        'A3ModsDir
        '
        Me.A3ModsDir.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.A3ModsDir.BaseColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.A3ModsDir.ClickedColor = System.Drawing.Color.DeepSkyBlue
        Me.A3ModsDir.Colors = New GhostGamerz.Bloom(-1) {}
        Me.A3ModsDir.Customization = ""
        Me.A3ModsDir.Enabled = False
        Me.A3ModsDir.Font = New System.Drawing.Font("Verdana", 8.0!)
        Me.A3ModsDir.HighlightColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(151, Byte), Integer), CType(CType(205, Byte), Integer))
        Me.A3ModsDir.Image = Nothing
        Me.A3ModsDir.Location = New System.Drawing.Point(393, 58)
        Me.A3ModsDir.Name = "A3ModsDir"
        Me.A3ModsDir.NoRounding = False
        Me.A3ModsDir.Size = New System.Drawing.Size(27, 20)
        Me.A3ModsDir.TabIndex = 12
        Me.A3ModsDir.Text = "..."
        Me.A3ModsDir.Transparent = False
        '
        'A3ModsLoc
        '
        Me.A3ModsLoc.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.A3ModsLoc.Colors = New GhostGamerz.Bloom(-1) {}
        Me.A3ModsLoc.Customization = ""
        Me.A3ModsLoc.Enabled = False
        Me.A3ModsLoc.Font = New System.Drawing.Font("Verdana", 8.0!)
        Me.A3ModsLoc.ForeColour = System.Drawing.Color.White
        Me.A3ModsLoc.Image = Nothing
        Me.A3ModsLoc.Location = New System.Drawing.Point(103, 58)
        Me.A3ModsLoc.MaxLength = "32767"
        Me.A3ModsLoc.Multilined = False
        Me.A3ModsLoc.Name = "A3ModsLoc"
        Me.A3ModsLoc.NoRounding = False
        Me.A3ModsLoc.PasswordChar = Nothing
        Me.A3ModsLoc.Read_Only = False
        Me.A3ModsLoc.ScrollBars = GhostGamerz.SpaceTextBox.ScrollBar.None
        Me.A3ModsLoc.Size = New System.Drawing.Size(284, 20)
        Me.A3ModsLoc.TabIndex = 11
        Me.A3ModsLoc.Transparent = False
        Me.A3ModsLoc.UseSystemPasswordChar = False
        '
        'SpaceLabel2
        '
        Me.SpaceLabel2.AutoSize = True
        Me.SpaceLabel2.BackColor = System.Drawing.Color.Transparent
        Me.SpaceLabel2.Enabled = False
        Me.SpaceLabel2.ForeColor = System.Drawing.Color.White
        Me.SpaceLabel2.Location = New System.Drawing.Point(18, 61)
        Me.SpaceLabel2.Name = "SpaceLabel2"
        Me.SpaceLabel2.Size = New System.Drawing.Size(41, 13)
        Me.SpaceLabel2.TabIndex = 10
        Me.SpaceLabel2.Text = "Mods:"
        '
        'A3DirBrowse
        '
        Me.A3DirBrowse.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.A3DirBrowse.BaseColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.A3DirBrowse.ClickedColor = System.Drawing.Color.DeepSkyBlue
        Me.A3DirBrowse.Colors = New GhostGamerz.Bloom(-1) {}
        Me.A3DirBrowse.Customization = ""
        Me.A3DirBrowse.Font = New System.Drawing.Font("Verdana", 8.0!)
        Me.A3DirBrowse.HighlightColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(151, Byte), Integer), CType(CType(205, Byte), Integer))
        Me.A3DirBrowse.Image = Nothing
        Me.A3DirBrowse.Location = New System.Drawing.Point(393, 31)
        Me.A3DirBrowse.Name = "A3DirBrowse"
        Me.A3DirBrowse.NoRounding = False
        Me.A3DirBrowse.Size = New System.Drawing.Size(27, 20)
        Me.A3DirBrowse.TabIndex = 8
        Me.A3DirBrowse.Text = "..."
        Me.A3DirBrowse.Transparent = False
        '
        'A3AutoDetect
        '
        Me.A3AutoDetect.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.A3AutoDetect.BaseColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.A3AutoDetect.ClickedColor = System.Drawing.Color.DeepSkyBlue
        Me.A3AutoDetect.Colors = New GhostGamerz.Bloom(-1) {}
        Me.A3AutoDetect.Customization = ""
        Me.A3AutoDetect.Font = New System.Drawing.Font("Verdana", 8.0!)
        Me.A3AutoDetect.HighlightColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(151, Byte), Integer), CType(CType(205, Byte), Integer))
        Me.A3AutoDetect.Image = Nothing
        Me.A3AutoDetect.Location = New System.Drawing.Point(332, 82)
        Me.A3AutoDetect.Name = "A3AutoDetect"
        Me.A3AutoDetect.NoRounding = False
        Me.A3AutoDetect.Size = New System.Drawing.Size(88, 23)
        Me.A3AutoDetect.TabIndex = 7
        Me.A3AutoDetect.Text = "Auto Detect"
        Me.A3AutoDetect.Transparent = False
        '
        'SpaceLabel4
        '
        Me.SpaceLabel4.AutoSize = True
        Me.SpaceLabel4.BackColor = System.Drawing.Color.Transparent
        Me.SpaceLabel4.ForeColor = System.Drawing.Color.White
        Me.SpaceLabel4.Location = New System.Drawing.Point(18, 36)
        Me.SpaceLabel4.Name = "SpaceLabel4"
        Me.SpaceLabel4.Size = New System.Drawing.Size(63, 13)
        Me.SpaceLabel4.TabIndex = 1
        Me.SpaceLabel4.Text = "ArmA III:"
        '
        'Arma3Dir
        '
        Me.Arma3Dir.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Arma3Dir.Colors = New GhostGamerz.Bloom(-1) {}
        Me.Arma3Dir.Customization = ""
        Me.Arma3Dir.Font = New System.Drawing.Font("Verdana", 8.0!)
        Me.Arma3Dir.ForeColour = System.Drawing.Color.White
        Me.Arma3Dir.Image = Nothing
        Me.Arma3Dir.Location = New System.Drawing.Point(103, 32)
        Me.Arma3Dir.MaxLength = "32767"
        Me.Arma3Dir.Multilined = False
        Me.Arma3Dir.Name = "Arma3Dir"
        Me.Arma3Dir.NoRounding = False
        Me.Arma3Dir.PasswordChar = Nothing
        Me.Arma3Dir.Read_Only = False
        Me.Arma3Dir.ScrollBars = GhostGamerz.SpaceTextBox.ScrollBar.None
        Me.Arma3Dir.Size = New System.Drawing.Size(284, 20)
        Me.Arma3Dir.TabIndex = 0
        Me.Arma3Dir.Transparent = False
        Me.Arma3Dir.UseSystemPasswordChar = False
        '
        'ModsGroupBox
        '
        Me.ModsGroupBox.ActiveTitleForeColour = System.Drawing.Color.White
        Me.ModsGroupBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ModsGroupBox.BorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.ModsGroupBox.Colors = New GhostGamerz.Bloom(-1) {}
        Me.ModsGroupBox.Controls.Add(Me.ModImportButton)
        Me.ModsGroupBox.Controls.Add(Me.modstatus)
        Me.ModsGroupBox.Controls.Add(Me.ModDownloadButton)
        Me.ModsGroupBox.Controls.Add(Me.ModsList)
        Me.ModsGroupBox.Customization = ""
        Me.ModsGroupBox.Font = New System.Drawing.Font("Verdana", 8.0!)
        Me.ModsGroupBox.Image = Nothing
        Me.ModsGroupBox.InactiveTitleForeColour = System.Drawing.Color.Red
        Me.ModsGroupBox.Location = New System.Drawing.Point(7, 283)
        Me.ModsGroupBox.Movable = True
        Me.ModsGroupBox.Name = "ModsGroupBox"
        Me.ModsGroupBox.NoRounding = False
        Me.ModsGroupBox.SettingsButton = True
        Me.ModsGroupBox.SettingsImage = Global.GhostGamerz.My.Resources.Resources.settings
        Me.ModsGroupBox.Sizable = True
        Me.ModsGroupBox.Size = New System.Drawing.Size(438, 228)
        Me.ModsGroupBox.SmartBounds = True
        Me.ModsGroupBox.TabIndex = 1
        Me.ModsGroupBox.Text = "ArmA Mods"
        Me.ModsGroupBox.TransparencyKey = System.Drawing.Color.Empty
        Me.ModsGroupBox.Transparent = False
        '
        'ModImportButton
        '
        Me.ModImportButton.BaseColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.ModImportButton.ClickedColor = System.Drawing.Color.DeepSkyBlue
        Me.ModImportButton.Colors = New GhostGamerz.Bloom(-1) {}
        Me.ModImportButton.Customization = ""
        Me.ModImportButton.Font = New System.Drawing.Font("Verdana", 8.0!)
        Me.ModImportButton.HighlightColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(151, Byte), Integer), CType(CType(205, Byte), Integer))
        Me.ModImportButton.Image = Nothing
        Me.ModImportButton.Location = New System.Drawing.Point(20, 192)
        Me.ModImportButton.Name = "ModImportButton"
        Me.ModImportButton.NoRounding = False
        Me.ModImportButton.Size = New System.Drawing.Size(88, 23)
        Me.ModImportButton.TabIndex = 13
        Me.ModImportButton.Text = "Import Mods"
        Me.ModImportButton.Transparent = False
        '
        'modstatus
        '
        Me.modstatus.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.modstatus.AutoSize = True
        Me.modstatus.BackColor = System.Drawing.Color.Transparent
        Me.modstatus.ForeColor = System.Drawing.Color.Gray
        Me.modstatus.Location = New System.Drawing.Point(113, 197)
        Me.modstatus.Name = "modstatus"
        Me.modstatus.Size = New System.Drawing.Size(0, 13)
        Me.modstatus.TabIndex = 12
        Me.modstatus.Tag = ""
        Me.modstatus.Visible = False
        '
        'ModDownloadButton
        '
        Me.ModDownloadButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ModDownloadButton.BaseColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.ModDownloadButton.ClickedColor = System.Drawing.Color.DeepSkyBlue
        Me.ModDownloadButton.Colors = New GhostGamerz.Bloom(-1) {}
        Me.ModDownloadButton.Customization = ""
        Me.ModDownloadButton.Font = New System.Drawing.Font("Verdana", 8.0!)
        Me.ModDownloadButton.HighlightColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(151, Byte), Integer), CType(CType(205, Byte), Integer))
        Me.ModDownloadButton.Image = Nothing
        Me.ModDownloadButton.Location = New System.Drawing.Point(332, 192)
        Me.ModDownloadButton.Name = "ModDownloadButton"
        Me.ModDownloadButton.NoRounding = False
        Me.ModDownloadButton.Size = New System.Drawing.Size(88, 23)
        Me.ModDownloadButton.TabIndex = 10
        Me.ModDownloadButton.Text = "Download"
        Me.ModDownloadButton.Transparent = False
        '
        'ModsList
        '
        Me.ModsList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ModsList.BackColor = System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer))
        Me.ModsList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ModsList.ForeColor = System.Drawing.Color.Gray
        Me.ModsList.Location = New System.Drawing.Point(20, 35)
        Me.ModsList.Name = "ModsList"
        Me.ModsList.Size = New System.Drawing.Size(400, 151)
        Me.ModsList.TabIndex = 0
        '
        'ArmaIILocGroupBox
        '
        Me.ArmaIILocGroupBox.ActiveTitleForeColour = System.Drawing.Color.White
        Me.ArmaIILocGroupBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ArmaIILocGroupBox.BorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.ArmaIILocGroupBox.Colors = New GhostGamerz.Bloom(-1) {}
        Me.ArmaIILocGroupBox.Controls.Add(Me.ParamsLbl)
        Me.ArmaIILocGroupBox.Controls.Add(Me.Params)
        Me.ArmaIILocGroupBox.Controls.Add(Me.ArmaIIModsBrowse)
        Me.ArmaIILocGroupBox.Controls.Add(Me.ModsLoc)
        Me.ArmaIILocGroupBox.Controls.Add(Me.ModsLocLbl)
        Me.ArmaIILocGroupBox.Controls.Add(Me.ArmaIIOABrowse)
        Me.ArmaIILocGroupBox.Controls.Add(Me.ArmaIIBrowse)
        Me.ArmaIILocGroupBox.Controls.Add(Me.AutoDetectModsButton)
        Me.ArmaIILocGroupBox.Controls.Add(Me.ArmaOADirLbl)
        Me.ArmaIILocGroupBox.Controls.Add(Me.ArmaOADir)
        Me.ArmaIILocGroupBox.Controls.Add(Me.ArmaDirLbl)
        Me.ArmaIILocGroupBox.Controls.Add(Me.ArmaDir)
        Me.ArmaIILocGroupBox.Customization = ""
        Me.ArmaIILocGroupBox.Font = New System.Drawing.Font("Verdana", 8.0!)
        Me.ArmaIILocGroupBox.Image = Nothing
        Me.ArmaIILocGroupBox.InactiveTitleForeColour = System.Drawing.Color.Red
        Me.ArmaIILocGroupBox.Location = New System.Drawing.Point(6, 6)
        Me.ArmaIILocGroupBox.Movable = True
        Me.ArmaIILocGroupBox.Name = "ArmaIILocGroupBox"
        Me.ArmaIILocGroupBox.NoRounding = False
        Me.ArmaIILocGroupBox.SettingsButton = False
        Me.ArmaIILocGroupBox.SettingsImage = Nothing
        Me.ArmaIILocGroupBox.Sizable = True
        Me.ArmaIILocGroupBox.Size = New System.Drawing.Size(438, 147)
        Me.ArmaIILocGroupBox.SmartBounds = True
        Me.ArmaIILocGroupBox.TabIndex = 0
        Me.ArmaIILocGroupBox.Text = "ArmA II Location"
        Me.ArmaIILocGroupBox.TransparencyKey = System.Drawing.Color.Empty
        Me.ArmaIILocGroupBox.Transparent = False
        '
        'ParamsLbl
        '
        Me.ParamsLbl.AutoSize = True
        Me.ParamsLbl.BackColor = System.Drawing.Color.Transparent
        Me.ParamsLbl.ForeColor = System.Drawing.Color.White
        Me.ParamsLbl.Location = New System.Drawing.Point(18, 113)
        Me.ParamsLbl.Name = "ParamsLbl"
        Me.ParamsLbl.Size = New System.Drawing.Size(82, 13)
        Me.ParamsLbl.TabIndex = 14
        Me.ParamsLbl.Text = "Parameters: "
        '
        'Params
        '
        Me.Params.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Params.Colors = New GhostGamerz.Bloom(-1) {}
        Me.Params.Customization = ""
        Me.Params.Font = New System.Drawing.Font("Verdana", 8.0!)
        Me.Params.ForeColour = System.Drawing.Color.White
        Me.Params.Image = Nothing
        Me.Params.Location = New System.Drawing.Point(103, 110)
        Me.Params.MaxLength = "32767"
        Me.Params.Multilined = False
        Me.Params.Name = "Params"
        Me.Params.NoRounding = False
        Me.Params.PasswordChar = Nothing
        Me.Params.Read_Only = False
        Me.Params.ScrollBars = GhostGamerz.SpaceTextBox.ScrollBar.None
        Me.Params.Size = New System.Drawing.Size(223, 20)
        Me.Params.TabIndex = 13
        Me.Params.Transparent = False
        Me.Params.UseSystemPasswordChar = False
        '
        'ArmaIIModsBrowse
        '
        Me.ArmaIIModsBrowse.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ArmaIIModsBrowse.BaseColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.ArmaIIModsBrowse.ClickedColor = System.Drawing.Color.DeepSkyBlue
        Me.ArmaIIModsBrowse.Colors = New GhostGamerz.Bloom(-1) {}
        Me.ArmaIIModsBrowse.Customization = ""
        Me.ArmaIIModsBrowse.Enabled = False
        Me.ArmaIIModsBrowse.Font = New System.Drawing.Font("Verdana", 8.0!)
        Me.ArmaIIModsBrowse.HighlightColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(151, Byte), Integer), CType(CType(205, Byte), Integer))
        Me.ArmaIIModsBrowse.Image = Nothing
        Me.ArmaIIModsBrowse.Location = New System.Drawing.Point(393, 83)
        Me.ArmaIIModsBrowse.Name = "ArmaIIModsBrowse"
        Me.ArmaIIModsBrowse.NoRounding = False
        Me.ArmaIIModsBrowse.Size = New System.Drawing.Size(27, 20)
        Me.ArmaIIModsBrowse.TabIndex = 12
        Me.ArmaIIModsBrowse.Text = "..."
        Me.ArmaIIModsBrowse.Transparent = False
        '
        'ModsLoc
        '
        Me.ModsLoc.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ModsLoc.Colors = New GhostGamerz.Bloom(-1) {}
        Me.ModsLoc.Customization = ""
        Me.ModsLoc.Enabled = False
        Me.ModsLoc.Font = New System.Drawing.Font("Verdana", 8.0!)
        Me.ModsLoc.ForeColour = System.Drawing.Color.White
        Me.ModsLoc.Image = Nothing
        Me.ModsLoc.Location = New System.Drawing.Point(103, 84)
        Me.ModsLoc.MaxLength = "32767"
        Me.ModsLoc.Multilined = False
        Me.ModsLoc.Name = "ModsLoc"
        Me.ModsLoc.NoRounding = False
        Me.ModsLoc.PasswordChar = Nothing
        Me.ModsLoc.Read_Only = False
        Me.ModsLoc.ScrollBars = GhostGamerz.SpaceTextBox.ScrollBar.None
        Me.ModsLoc.Size = New System.Drawing.Size(284, 20)
        Me.ModsLoc.TabIndex = 11
        Me.ModsLoc.Transparent = False
        Me.ModsLoc.UseSystemPasswordChar = False
        '
        'ModsLocLbl
        '
        Me.ModsLocLbl.AutoSize = True
        Me.ModsLocLbl.BackColor = System.Drawing.Color.Transparent
        Me.ModsLocLbl.Enabled = False
        Me.ModsLocLbl.ForeColor = System.Drawing.Color.White
        Me.ModsLocLbl.Location = New System.Drawing.Point(18, 87)
        Me.ModsLocLbl.Name = "ModsLocLbl"
        Me.ModsLocLbl.Size = New System.Drawing.Size(41, 13)
        Me.ModsLocLbl.TabIndex = 10
        Me.ModsLocLbl.Text = "Mods:"
        '
        'ArmaIIOABrowse
        '
        Me.ArmaIIOABrowse.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ArmaIIOABrowse.BaseColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.ArmaIIOABrowse.ClickedColor = System.Drawing.Color.DeepSkyBlue
        Me.ArmaIIOABrowse.Colors = New GhostGamerz.Bloom(-1) {}
        Me.ArmaIIOABrowse.Customization = ""
        Me.ArmaIIOABrowse.Font = New System.Drawing.Font("Verdana", 8.0!)
        Me.ArmaIIOABrowse.HighlightColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(151, Byte), Integer), CType(CType(205, Byte), Integer))
        Me.ArmaIIOABrowse.Image = Nothing
        Me.ArmaIIOABrowse.Location = New System.Drawing.Point(393, 57)
        Me.ArmaIIOABrowse.Name = "ArmaIIOABrowse"
        Me.ArmaIIOABrowse.NoRounding = False
        Me.ArmaIIOABrowse.Size = New System.Drawing.Size(27, 20)
        Me.ArmaIIOABrowse.TabIndex = 9
        Me.ArmaIIOABrowse.Text = "..."
        Me.ArmaIIOABrowse.Transparent = False
        '
        'ArmaIIBrowse
        '
        Me.ArmaIIBrowse.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ArmaIIBrowse.BaseColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.ArmaIIBrowse.ClickedColor = System.Drawing.Color.DeepSkyBlue
        Me.ArmaIIBrowse.Colors = New GhostGamerz.Bloom(-1) {}
        Me.ArmaIIBrowse.Customization = ""
        Me.ArmaIIBrowse.Font = New System.Drawing.Font("Verdana", 8.0!)
        Me.ArmaIIBrowse.HighlightColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(151, Byte), Integer), CType(CType(205, Byte), Integer))
        Me.ArmaIIBrowse.Image = Nothing
        Me.ArmaIIBrowse.Location = New System.Drawing.Point(393, 31)
        Me.ArmaIIBrowse.Name = "ArmaIIBrowse"
        Me.ArmaIIBrowse.NoRounding = False
        Me.ArmaIIBrowse.Size = New System.Drawing.Size(27, 20)
        Me.ArmaIIBrowse.TabIndex = 8
        Me.ArmaIIBrowse.Text = "..."
        Me.ArmaIIBrowse.Transparent = False
        '
        'AutoDetectModsButton
        '
        Me.AutoDetectModsButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AutoDetectModsButton.BaseColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.AutoDetectModsButton.ClickedColor = System.Drawing.Color.DeepSkyBlue
        Me.AutoDetectModsButton.Colors = New GhostGamerz.Bloom(-1) {}
        Me.AutoDetectModsButton.Customization = ""
        Me.AutoDetectModsButton.Font = New System.Drawing.Font("Verdana", 8.0!)
        Me.AutoDetectModsButton.HighlightColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(151, Byte), Integer), CType(CType(205, Byte), Integer))
        Me.AutoDetectModsButton.Image = Nothing
        Me.AutoDetectModsButton.Location = New System.Drawing.Point(332, 108)
        Me.AutoDetectModsButton.Name = "AutoDetectModsButton"
        Me.AutoDetectModsButton.NoRounding = False
        Me.AutoDetectModsButton.Size = New System.Drawing.Size(88, 23)
        Me.AutoDetectModsButton.TabIndex = 7
        Me.AutoDetectModsButton.Text = "Auto Detect"
        Me.AutoDetectModsButton.Transparent = False
        '
        'ArmaOADirLbl
        '
        Me.ArmaOADirLbl.AutoSize = True
        Me.ArmaOADirLbl.BackColor = System.Drawing.Color.Transparent
        Me.ArmaOADirLbl.ForeColor = System.Drawing.Color.White
        Me.ArmaOADirLbl.Location = New System.Drawing.Point(18, 61)
        Me.ArmaOADirLbl.Name = "ArmaOADirLbl"
        Me.ArmaOADirLbl.Size = New System.Drawing.Size(79, 13)
        Me.ArmaOADirLbl.TabIndex = 3
        Me.ArmaOADirLbl.Text = "ArmA II OA:"
        '
        'ArmaOADir
        '
        Me.ArmaOADir.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ArmaOADir.Colors = New GhostGamerz.Bloom(-1) {}
        Me.ArmaOADir.Customization = ""
        Me.ArmaOADir.Font = New System.Drawing.Font("Verdana", 8.0!)
        Me.ArmaOADir.ForeColour = System.Drawing.Color.White
        Me.ArmaOADir.Image = Nothing
        Me.ArmaOADir.Location = New System.Drawing.Point(103, 58)
        Me.ArmaOADir.MaxLength = "32767"
        Me.ArmaOADir.Multilined = False
        Me.ArmaOADir.Name = "ArmaOADir"
        Me.ArmaOADir.NoRounding = False
        Me.ArmaOADir.PasswordChar = Nothing
        Me.ArmaOADir.Read_Only = False
        Me.ArmaOADir.ScrollBars = GhostGamerz.SpaceTextBox.ScrollBar.None
        Me.ArmaOADir.Size = New System.Drawing.Size(284, 20)
        Me.ArmaOADir.TabIndex = 2
        Me.ArmaOADir.Transparent = False
        Me.ArmaOADir.UseSystemPasswordChar = False
        '
        'ArmaDirLbl
        '
        Me.ArmaDirLbl.AutoSize = True
        Me.ArmaDirLbl.BackColor = System.Drawing.Color.Transparent
        Me.ArmaDirLbl.ForeColor = System.Drawing.Color.White
        Me.ArmaDirLbl.Location = New System.Drawing.Point(18, 36)
        Me.ArmaDirLbl.Name = "ArmaDirLbl"
        Me.ArmaDirLbl.Size = New System.Drawing.Size(58, 13)
        Me.ArmaDirLbl.TabIndex = 1
        Me.ArmaDirLbl.Text = "ArmA II:"
        '
        'ArmaDir
        '
        Me.ArmaDir.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ArmaDir.Colors = New GhostGamerz.Bloom(-1) {}
        Me.ArmaDir.Customization = ""
        Me.ArmaDir.Font = New System.Drawing.Font("Verdana", 8.0!)
        Me.ArmaDir.ForeColour = System.Drawing.Color.White
        Me.ArmaDir.Image = Nothing
        Me.ArmaDir.Location = New System.Drawing.Point(103, 32)
        Me.ArmaDir.MaxLength = "32767"
        Me.ArmaDir.Multilined = False
        Me.ArmaDir.Name = "ArmaDir"
        Me.ArmaDir.NoRounding = False
        Me.ArmaDir.PasswordChar = Nothing
        Me.ArmaDir.Read_Only = False
        Me.ArmaDir.ScrollBars = GhostGamerz.SpaceTextBox.ScrollBar.None
        Me.ArmaDir.Size = New System.Drawing.Size(284, 20)
        Me.ArmaDir.TabIndex = 0
        Me.ArmaDir.Transparent = False
        Me.ArmaDir.UseSystemPasswordChar = False
        '
        'TabPage4
        '
        Me.TabPage4.BackColor = System.Drawing.Color.FromArgb(CType(CType(24, Byte), Integer), CType(CType(24, Byte), Integer), CType(CType(24, Byte), Integer))
        Me.TabPage4.Controls.Add(Me.KillboardGroupBox)
        Me.TabPage4.Location = New System.Drawing.Point(4, 25)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage4.Size = New System.Drawing.Size(450, 517)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "Killboard"
        '
        'KillboardGroupBox
        '
        Me.KillboardGroupBox.ActiveTitleForeColour = System.Drawing.Color.White
        Me.KillboardGroupBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.KillboardGroupBox.BorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.KillboardGroupBox.Colors = New GhostGamerz.Bloom(-1) {}
        Me.KillboardGroupBox.Controls.Add(Me.Killboard)
        Me.KillboardGroupBox.Controls.Add(Me.DirectionalButton2)
        Me.KillboardGroupBox.Controls.Add(Me.DirectionalButton1)
        Me.KillboardGroupBox.Customization = ""
        Me.KillboardGroupBox.Font = New System.Drawing.Font("Verdana", 8.0!)
        Me.KillboardGroupBox.Image = Nothing
        Me.KillboardGroupBox.InactiveTitleForeColour = System.Drawing.Color.Red
        Me.KillboardGroupBox.Location = New System.Drawing.Point(3, 3)
        Me.KillboardGroupBox.Movable = True
        Me.KillboardGroupBox.Name = "KillboardGroupBox"
        Me.KillboardGroupBox.NoRounding = False
        Me.KillboardGroupBox.SettingsButton = True
        Me.KillboardGroupBox.SettingsImage = Global.GhostGamerz.My.Resources.Resources.refresh1
        Me.KillboardGroupBox.Sizable = True
        Me.KillboardGroupBox.Size = New System.Drawing.Size(444, 508)
        Me.KillboardGroupBox.SmartBounds = True
        Me.KillboardGroupBox.TabIndex = 1
        Me.KillboardGroupBox.Text = "Killboard"
        Me.KillboardGroupBox.TransparencyKey = System.Drawing.Color.Empty
        Me.KillboardGroupBox.Transparent = False
        '
        'Killboard
        '
        Me.Killboard.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Killboard.Items = CType(resources.GetObject("Killboard.Items"), System.Collections.ArrayList)
        Me.Killboard.Location = New System.Drawing.Point(17, 31)
        Me.Killboard.Name = "Killboard"
        Me.Killboard.Size = New System.Drawing.Size(388, 466)
        Me.Killboard.TabIndex = 3
        Me.Killboard.VScrollValue = 0
        '
        'DirectionalButton2
        '
        Me.DirectionalButton2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DirectionalButton2.BackColor = System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer))
        Me.DirectionalButton2.Direction = GhostGamerz.DirectionalButton.DirectionType.Down
        Me.DirectionalButton2.Location = New System.Drawing.Point(411, 472)
        Me.DirectionalButton2.Name = "DirectionalButton2"
        Me.DirectionalButton2.Size = New System.Drawing.Size(25, 25)
        Me.DirectionalButton2.TabIndex = 2
        '
        'DirectionalButton1
        '
        Me.DirectionalButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DirectionalButton1.BackColor = System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer))
        Me.DirectionalButton1.Direction = GhostGamerz.DirectionalButton.DirectionType.Up
        Me.DirectionalButton1.Location = New System.Drawing.Point(411, 31)
        Me.DirectionalButton1.Name = "DirectionalButton1"
        Me.DirectionalButton1.Size = New System.Drawing.Size(25, 25)
        Me.DirectionalButton1.TabIndex = 1
        '
        'TabPage5
        '
        Me.TabPage5.BackColor = System.Drawing.Color.FromArgb(CType(CType(24, Byte), Integer), CType(CType(24, Byte), Integer), CType(CType(24, Byte), Integer))
        Me.TabPage5.Controls.Add(Me.RulesGroupBox)
        Me.TabPage5.Location = New System.Drawing.Point(4, 25)
        Me.TabPage5.Name = "TabPage5"
        Me.TabPage5.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage5.Size = New System.Drawing.Size(450, 517)
        Me.TabPage5.TabIndex = 4
        Me.TabPage5.Text = "Rules"
        '
        'RulesGroupBox
        '
        Me.RulesGroupBox.ActiveTitleForeColour = System.Drawing.Color.White
        Me.RulesGroupBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RulesGroupBox.BorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.RulesGroupBox.Colors = New GhostGamerz.Bloom(-1) {}
        Me.RulesGroupBox.Controls.Add(Me.Rules)
        Me.RulesGroupBox.Controls.Add(Me.DirectionalButton3)
        Me.RulesGroupBox.Controls.Add(Me.DirectionalButton4)
        Me.RulesGroupBox.Customization = ""
        Me.RulesGroupBox.Font = New System.Drawing.Font("Verdana", 8.0!)
        Me.RulesGroupBox.Image = Nothing
        Me.RulesGroupBox.InactiveTitleForeColour = System.Drawing.Color.Red
        Me.RulesGroupBox.Location = New System.Drawing.Point(3, 3)
        Me.RulesGroupBox.Movable = True
        Me.RulesGroupBox.Name = "RulesGroupBox"
        Me.RulesGroupBox.NoRounding = False
        Me.RulesGroupBox.SettingsButton = True
        Me.RulesGroupBox.SettingsImage = Global.GhostGamerz.My.Resources.Resources.refresh1
        Me.RulesGroupBox.Sizable = True
        Me.RulesGroupBox.Size = New System.Drawing.Size(444, 508)
        Me.RulesGroupBox.SmartBounds = True
        Me.RulesGroupBox.TabIndex = 2
        Me.RulesGroupBox.Text = "Rules"
        Me.RulesGroupBox.TransparencyKey = System.Drawing.Color.Empty
        Me.RulesGroupBox.Transparent = False
        '
        'Rules
        '
        Me.Rules.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Rules.Items = CType(resources.GetObject("Rules.Items"), System.Collections.ArrayList)
        Me.Rules.Location = New System.Drawing.Point(17, 31)
        Me.Rules.Name = "Rules"
        Me.Rules.Size = New System.Drawing.Size(388, 466)
        Me.Rules.TabIndex = 3
        Me.Rules.VScrollValue = 0
        '
        'DirectionalButton3
        '
        Me.DirectionalButton3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DirectionalButton3.BackColor = System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer))
        Me.DirectionalButton3.Direction = GhostGamerz.DirectionalButton.DirectionType.Down
        Me.DirectionalButton3.Location = New System.Drawing.Point(411, 472)
        Me.DirectionalButton3.Name = "DirectionalButton3"
        Me.DirectionalButton3.Size = New System.Drawing.Size(25, 25)
        Me.DirectionalButton3.TabIndex = 2
        '
        'DirectionalButton4
        '
        Me.DirectionalButton4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DirectionalButton4.BackColor = System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer))
        Me.DirectionalButton4.Direction = GhostGamerz.DirectionalButton.DirectionType.Up
        Me.DirectionalButton4.Location = New System.Drawing.Point(411, 31)
        Me.DirectionalButton4.Name = "DirectionalButton4"
        Me.DirectionalButton4.Size = New System.Drawing.Size(25, 25)
        Me.DirectionalButton4.TabIndex = 1
        '
        'TabPage3
        '
        Me.TabPage3.BackColor = System.Drawing.Color.FromArgb(CType(CType(24, Byte), Integer), CType(CType(24, Byte), Integer), CType(CType(24, Byte), Integer))
        Me.TabPage3.Location = New System.Drawing.Point(4, 25)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(450, 517)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "About"
        '
        'SpaceKillboard1
        '
        Me.SpaceKillboard1.Items = CType(resources.GetObject("SpaceKillboard1.Items"), System.Collections.ArrayList)
        Me.SpaceKillboard1.Location = New System.Drawing.Point(0, 0)
        Me.SpaceKillboard1.Name = "SpaceKillboard1"
        Me.SpaceKillboard1.Size = New System.Drawing.Size(200, 100)
        Me.SpaceKillboard1.TabIndex = 0
        Me.SpaceKillboard1.VScrollValue = 0
        '
        'Launcher
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(482, 593)
        Me.Controls.Add(Me.UITheme)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimumSize = New System.Drawing.Size(482, 540)
        Me.Name = "Launcher"
        Me.Text = "Launcher"
        Me.TransparencyKey = System.Drawing.Color.DeepPink
        Me.UITheme.ResumeLayout(False)
        Me.UITabs.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.ServersPanel.ResumeLayout(False)
        Me.ServersPanel.PerformLayout()
        Me.ServerScrollPanel.ResumeLayout(False)
        Me.ChernoPanel.ResumeLayout(False)
        Me.ChernoPanel.PerformLayout()
        CType(Me.ChernoImage, System.ComponentModel.ISupportInitialize).EndInit()
        Me.NapfPanel.ResumeLayout(False)
        Me.NapfPanel.PerformLayout()
        CType(Me.NapfImage, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TaviPanel.ResumeLayout(False)
        Me.TaviPanel.PerformLayout()
        CType(Me.TaviImage, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.SpaceGroupBox1.ResumeLayout(False)
        Me.SpaceGroupBox1.PerformLayout()
        Me.ModsGroupBox.ResumeLayout(False)
        Me.ModsGroupBox.PerformLayout()
        Me.ArmaIILocGroupBox.ResumeLayout(False)
        Me.ArmaIILocGroupBox.PerformLayout()
        Me.TabPage4.ResumeLayout(False)
        Me.KillboardGroupBox.ResumeLayout(False)
        Me.TabPage5.ResumeLayout(False)
        Me.RulesGroupBox.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents UITheme As GhostGamerz.SpaceTheme
    Friend WithEvents ServersPanel As GhostGamerz.SpaceGroupBox
    Friend WithEvents UITabs As GhostGamerz.SpaceTabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents selectedMapLbl As GhostGamerz.SpaceLabel
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents UIMinimizeBtn As GhostGamerz.SpaceButton_Minimize
    Friend WithEvents UIMaximizeBtn As GhostGamerz.SpaceButton_Maximize
    Friend WithEvents UICloseBtn As GhostGamerz.SpaceButton_Close
    Friend WithEvents ArmaIILocGroupBox As GhostGamerz.SpaceGroupBox
    Friend WithEvents ServerPlay As GhostGamerz.SpaceButtonGloss
    Friend WithEvents ArmaOADirLbl As GhostGamerz.SpaceLabel
    Private WithEvents ArmaOADir As GhostGamerz.SpaceTextBox
    Friend WithEvents ArmaDirLbl As GhostGamerz.SpaceLabel
    Private WithEvents ArmaDir As GhostGamerz.SpaceTextBox
    Friend WithEvents AutoDetectModsButton As GhostGamerz.SpaceButtonGloss
    Friend WithEvents ArmaIIOABrowse As GhostGamerz.SpaceButtonGloss
    Friend WithEvents ArmaIIBrowse As GhostGamerz.SpaceButtonGloss
    Friend WithEvents ModsGroupBox As GhostGamerz.SpaceGroupBox
    Friend WithEvents ModsList As GhostGamerz.DoubleBufferedTreeview
    Friend WithEvents ModDownloadButton As GhostGamerz.SpaceButtonGloss
    Friend WithEvents ArmaIIModsBrowse As GhostGamerz.SpaceButtonGloss
    Private WithEvents ModsLoc As GhostGamerz.SpaceTextBox
    Friend WithEvents ModsLocLbl As GhostGamerz.SpaceLabel
    Friend WithEvents modstatus As GhostGamerz.SpaceLabel
    Friend WithEvents ParamsLbl As GhostGamerz.SpaceLabel
    Private WithEvents Params As GhostGamerz.SpaceTextBox
    Friend WithEvents ChernoPanel As GhostGamerz.SpacePanel
    Friend WithEvents ChernoDesc As GhostGamerz.SpaceLabel
    Friend WithEvents ChernoImage As System.Windows.Forms.PictureBox
    Friend WithEvents ChernoTitle As GhostGamerz.SpaceLabel
    Friend WithEvents TaviPanel As GhostGamerz.SpacePanel
    Friend WithEvents TaviDesc As GhostGamerz.SpaceLabel
    Friend WithEvents TaviImage As System.Windows.Forms.PictureBox
    Friend WithEvents TaviTitle As GhostGamerz.SpaceLabel
    Friend WithEvents NapfPanel As GhostGamerz.SpacePanel
    Friend WithEvents NapfDesc As GhostGamerz.SpaceLabel
    Friend WithEvents NapfImage As System.Windows.Forms.PictureBox
    Friend WithEvents NapfTitle As GhostGamerz.SpaceLabel
    Friend WithEvents ServerScrollPanel As System.Windows.Forms.Panel
    Friend WithEvents ServerListUp As GhostGamerz.DirectionalButton
    Friend WithEvents ServerListDown As GhostGamerz.DirectionalButton
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents KillboardGroupBox As GhostGamerz.SpaceGroupBox
    Friend WithEvents DirectionalButton2 As GhostGamerz.DirectionalButton
    Friend WithEvents DirectionalButton1 As GhostGamerz.DirectionalButton
    Friend WithEvents SpaceKillboard1 As GhostGamerz.SpaceKillboard
    Friend WithEvents Killboard As GhostGamerz.SpaceKillboard
    Friend WithEvents TabPage5 As System.Windows.Forms.TabPage
    Friend WithEvents RulesGroupBox As GhostGamerz.SpaceGroupBox
    Friend WithEvents Rules As GhostGamerz.SpaceKillboard
    Friend WithEvents DirectionalButton3 As GhostGamerz.DirectionalButton
    Friend WithEvents DirectionalButton4 As GhostGamerz.DirectionalButton
    Friend WithEvents ShowMapPlayers As GhostGamerz.SpaceButtonGloss
    Friend WithEvents SpaceGroupBox1 As GhostGamerz.SpaceGroupBox
    Friend WithEvents SpaceLabel1 As GhostGamerz.SpaceLabel
    Private WithEvents A3ParamsInput As GhostGamerz.SpaceTextBox
    Friend WithEvents A3ModsDir As GhostGamerz.SpaceButtonGloss
    Private WithEvents A3ModsLoc As GhostGamerz.SpaceTextBox
    Friend WithEvents SpaceLabel2 As GhostGamerz.SpaceLabel
    Friend WithEvents A3DirBrowse As GhostGamerz.SpaceButtonGloss
    Friend WithEvents A3AutoDetect As GhostGamerz.SpaceButtonGloss
    Friend WithEvents SpaceLabel4 As GhostGamerz.SpaceLabel
    Private WithEvents Arma3Dir As GhostGamerz.SpaceTextBox
    Friend WithEvents ModImportButton As GhostGamerz.SpaceButtonGloss
End Class
