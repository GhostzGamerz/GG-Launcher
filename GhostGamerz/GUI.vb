Imports System, System.Collections
Imports System.Drawing, System.Drawing.Drawing2D
Imports System.ComponentModel, System.Windows.Forms

'------------------
'Creator: Kieran Devlin
'Site: N/a
'Created: 18/12/2011
'Version: 1.0.0
'Theme Base: 1.5.3
'------------------



#Region "Theme Base"
Imports System.IO, System.Collections.Generic
Imports System.Runtime.InteropServices
Imports System.Drawing.Imaging
Imports System.Drawing.Text

MustInherit Class ThemeContainer153
    Inherits ContainerControl

#Region "Themebase"

#Region " Initialization "

    Protected G As Graphics, B As Bitmap

    Sub New()
        SetStyle(DirectCast(139270, ControlStyles), True)

        _ImageSize = Size.Empty
        Font = New Font("Verdana", 8S)

        MeasureBitmap = New Bitmap(1, 1)
        MeasureGraphics = Graphics.FromImage(MeasureBitmap)

        DrawRadialPath = New GraphicsPath

        InvalidateCustimization() 'Remove?
    End Sub

    Protected NotOverridable Overrides Sub OnHandleCreated(ByVal e As EventArgs)
        InvalidateCustimization()
        ColorHook()

        If Not _LockWidth = 0 Then Width = _LockWidth
        If Not _LockHeight = 0 Then Height = _LockHeight
        If Not _ControlMode Then MyBase.Dock = DockStyle.Fill

        Transparent = _Transparent
        If _Transparent AndAlso _BackColor Then BackColor = Color.Transparent

        MyBase.OnHandleCreated(e)
    End Sub

    Protected NotOverridable Overrides Sub OnParentChanged(ByVal e As EventArgs)
        MyBase.OnParentChanged(e)

        If Parent Is Nothing Then Return
        _IsParentForm = TypeOf Parent Is Form

        If Not _ControlMode Then
            InitializeMessages()

            If _IsParentForm Then
                ParentForm.FormBorderStyle = _BorderStyle
                ParentForm.TransparencyKey = _TransparencyKey
            End If

            Parent.BackColor = BackColor
        End If

        OnCreation()
    End Sub

#End Region


    Protected NotOverridable Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        If Width = 0 OrElse Height = 0 Then Return

        If _Transparent AndAlso _ControlMode Then
            PaintHook()
            e.Graphics.DrawImage(B, 0, 0)
        Else
            G = e.Graphics
            PaintHook()
        End If
    End Sub


#Region " Size Handling "

    Private Frame As Rectangle
    Protected NotOverridable Overrides Sub OnSizeChanged(ByVal e As EventArgs)
        If _Movable AndAlso Not _ControlMode Then
            Frame = New Rectangle(7, 7, Width - 14, _Header - 7)
        End If

        InvalidateBitmap()
        Invalidate()

        MyBase.OnSizeChanged(e)
    End Sub

    Protected Overrides Sub SetBoundsCore(ByVal x As Integer, ByVal y As Integer, ByVal width As Integer, ByVal height As Integer, ByVal specified As BoundsSpecified)
        If Not _LockWidth = 0 Then width = _LockWidth
        If Not _LockHeight = 0 Then height = _LockHeight
        MyBase.SetBoundsCore(x, y, width, height, specified)
    End Sub

#End Region

#Region " State Handling "

    Protected State As MouseState
    Private Sub SetState(ByVal current As MouseState)
        State = current
        Invalidate()
    End Sub

    Protected Overrides Sub OnMouseMove(ByVal e As MouseEventArgs)
        If Not (_IsParentForm AndAlso ParentForm.WindowState = FormWindowState.Maximized) Then
            If _Sizable AndAlso Not _ControlMode Then InvalidateMouse()
        End If

        MyBase.OnMouseMove(e)
    End Sub

    Protected Overrides Sub OnEnabledChanged(ByVal e As EventArgs)
        If Enabled Then SetState(MouseState.None) Else SetState(MouseState.Block)
        MyBase.OnEnabledChanged(e)
    End Sub

    Protected Overrides Sub OnMouseEnter(ByVal e As EventArgs)
        SetState(MouseState.Over)
        MyBase.OnMouseEnter(e)
    End Sub

    Protected Overrides Sub OnMouseUp(ByVal e As MouseEventArgs)
        SetState(MouseState.Over)
        MyBase.OnMouseUp(e)
    End Sub

    Protected Overrides Sub OnMouseLeave(ByVal e As EventArgs)
        SetState(MouseState.None)

        If GetChildAtPoint(PointToClient(MousePosition)) IsNot Nothing Then
            If _Sizable AndAlso Not _ControlMode Then
                Cursor = Cursors.Default
                Previous = 0
            End If
        End If

        MyBase.OnMouseLeave(e)
    End Sub

    Protected Overrides Sub OnMouseDown(ByVal e As MouseEventArgs)
        If e.Button = Windows.Forms.MouseButtons.Left Then SetState(MouseState.Down)

        If Not (_IsParentForm AndAlso ParentForm.WindowState = FormWindowState.Maximized OrElse _ControlMode) Then
            If _Movable AndAlso Frame.Contains(e.Location) Then
                Capture = False
                WM_LMBUTTONDOWN = True
                DefWndProc(Messages(0))
            ElseIf _Sizable AndAlso Not Previous = 0 Then
                Capture = False
                WM_LMBUTTONDOWN = True
                DefWndProc(Messages(Previous))
            End If
        End If

        MyBase.OnMouseDown(e)
    End Sub

    Private WM_LMBUTTONDOWN As Boolean
    Protected Overrides Sub WndProc(ByRef m As Message)
        MyBase.WndProc(m)

        If WM_LMBUTTONDOWN AndAlso m.Msg = 513 Then
            WM_LMBUTTONDOWN = False

            SetState(MouseState.Over)
            If Not _SmartBounds Then Return

            If IsParentMdi Then
                CorrectBounds(New Rectangle(Point.Empty, Parent.Parent.Size))
            Else
                CorrectBounds(Screen.FromControl(Parent).WorkingArea)
            End If
        End If
    End Sub

    Private GetIndexPoint As Point
    Private B1, B2, B3, B4 As Boolean
    Private Function GetIndex() As Integer
        GetIndexPoint = PointToClient(MousePosition)
        B1 = GetIndexPoint.X < 7
        B2 = GetIndexPoint.X > Width - 7
        B3 = GetIndexPoint.Y < 7
        B4 = GetIndexPoint.Y > Height - 7

        If B1 AndAlso B3 Then Return 4
        If B1 AndAlso B4 Then Return 7
        If B2 AndAlso B3 Then Return 5
        If B2 AndAlso B4 Then Return 8
        If B1 Then Return 1
        If B2 Then Return 2
        If B3 Then Return 3
        If B4 Then Return 6
        Return 0
    End Function

    Private Current, Previous As Integer
    Private Sub InvalidateMouse()
        Current = GetIndex()
        If Current = Previous Then Return

        Previous = Current
        Select Case Previous
            Case 0
                Cursor = Cursors.Default
            Case 1, 2
                Cursor = Cursors.SizeWE
            Case 3, 6
                Cursor = Cursors.SizeNS
            Case 4, 8
                Cursor = Cursors.SizeNWSE
            Case 5, 7
                Cursor = Cursors.SizeNESW
        End Select
    End Sub

    Private Messages(8) As Message
    Private Sub InitializeMessages()
        Messages(0) = Message.Create(Parent.Handle, 161, New IntPtr(2), IntPtr.Zero)
        For I As Integer = 1 To 8
            Messages(I) = Message.Create(Parent.Handle, 161, New IntPtr(I + 9), IntPtr.Zero)
        Next
    End Sub

    Private Sub CorrectBounds(ByVal bounds As Rectangle)
        If Parent.Width > bounds.Width Then Parent.Width = bounds.Width
        If Parent.Height > bounds.Height Then Parent.Height = bounds.Height

        Dim X As Integer = Parent.Location.X
        Dim Y As Integer = Parent.Location.Y

        If X < bounds.X Then X = bounds.X
        If Y < bounds.Y Then Y = bounds.Y

        Dim Width As Integer = bounds.X + bounds.Width
        Dim Height As Integer = bounds.Y + bounds.Height

        If X + Parent.Width > Width Then X = Width - Parent.Width
        If Y + Parent.Height > Height Then Y = Height - Parent.Height

        Parent.Location = New Point(X, Y)
    End Sub

#End Region


#Region " Base Properties "

    Overrides Property Dock As DockStyle
        Get
            Return MyBase.Dock
        End Get
        Set(ByVal value As DockStyle)
            If Not _ControlMode Then Return
            MyBase.Dock = value
        End Set
    End Property

    Private _BackColor As Boolean
    <Category("Misc")> _
    Overrides Property BackColor() As Color
        Get
            Return MyBase.BackColor
        End Get
        Set(ByVal value As Color)
            If value = MyBase.BackColor Then Return

            If Not IsHandleCreated AndAlso _ControlMode AndAlso value = Color.Transparent Then
                _BackColor = True
                Return
            End If

            MyBase.BackColor = value
            If Parent IsNot Nothing Then
                If Not _ControlMode Then Parent.BackColor = value
                ColorHook()
            End If
        End Set
    End Property

    Overrides Property MinimumSize As Size
        Get
            Return MyBase.MinimumSize
        End Get
        Set(ByVal value As Size)
            MyBase.MinimumSize = value
            If Parent IsNot Nothing Then Parent.MinimumSize = value
        End Set
    End Property

    Overrides Property MaximumSize As Size
        Get
            Return MyBase.MaximumSize
        End Get
        Set(ByVal value As Size)
            MyBase.MaximumSize = value
            If Parent IsNot Nothing Then Parent.MaximumSize = value
        End Set
    End Property

    Overrides Property Text() As String
        Get
            Return MyBase.Text
        End Get
        Set(ByVal value As String)
            MyBase.Text = value
            Invalidate()
        End Set
    End Property

    Overrides Property Font() As Font
        Get
            Return MyBase.Font
        End Get
        Set(ByVal value As Font)
            MyBase.Font = value
            Invalidate()
        End Set
    End Property

    <Browsable(False), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
    Overrides Property ForeColor() As Color
        Get
            Return Color.Empty
        End Get
        Set(ByVal value As Color)
        End Set
    End Property
    <Browsable(False), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
    Overrides Property BackgroundImage() As Image
        Get
            Return Nothing
        End Get
        Set(ByVal value As Image)
        End Set
    End Property
    <Browsable(False), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
    Overrides Property BackgroundImageLayout() As ImageLayout
        Get
            Return ImageLayout.None
        End Get
        Set(ByVal value As ImageLayout)
        End Set
    End Property

#End Region

#Region " Public Properties "

    Private _SmartBounds As Boolean = True
    Property SmartBounds() As Boolean
        Get
            Return _SmartBounds
        End Get
        Set(ByVal value As Boolean)
            _SmartBounds = value
        End Set
    End Property

    Private _Movable As Boolean = True
    Property Movable() As Boolean
        Get
            Return _Movable
        End Get
        Set(ByVal value As Boolean)
            _Movable = value
        End Set
    End Property

    Private _Sizable As Boolean = True
    Property Sizable() As Boolean
        Get
            Return _Sizable
        End Get
        Set(ByVal value As Boolean)
            _Sizable = value
        End Set
    End Property

    Private _TransparencyKey As Color
    Property TransparencyKey() As Color
        Get
            If _IsParentForm AndAlso Not _ControlMode Then Return ParentForm.TransparencyKey Else Return _TransparencyKey
        End Get
        Set(ByVal value As Color)
            If value = _TransparencyKey Then Return
            _TransparencyKey = value

            If _IsParentForm AndAlso Not _ControlMode Then
                ParentForm.TransparencyKey = value
                ColorHook()
            End If
        End Set
    End Property

    Private _BorderStyle As FormBorderStyle
    Property BorderStyle() As FormBorderStyle
        Get
            If _IsParentForm AndAlso Not _ControlMode Then Return ParentForm.FormBorderStyle Else Return _BorderStyle
        End Get
        Set(ByVal value As FormBorderStyle)
            _BorderStyle = value

            If _IsParentForm AndAlso Not _ControlMode Then
                ParentForm.FormBorderStyle = value

                If Not value = FormBorderStyle.None Then
                    Movable = False
                    Sizable = False
                End If
            End If
        End Set
    End Property

    Private _NoRounding As Boolean
    Property NoRounding() As Boolean
        Get
            Return _NoRounding
        End Get
        Set(ByVal v As Boolean)
            _NoRounding = v
            Invalidate()
        End Set
    End Property

    Private _Image As Image
    Property Image() As Image
        Get
            Return _Image
        End Get
        Set(ByVal value As Image)
            If value Is Nothing Then _ImageSize = Size.Empty Else _ImageSize = value.Size

            _Image = value
            Invalidate()
        End Set
    End Property

    Private Items As New Dictionary(Of String, Color)
    Property Colors() As Bloom()
        Get
            Dim T As New List(Of Bloom)
            Dim E As Dictionary(Of String, Color).Enumerator = Items.GetEnumerator

            While E.MoveNext
                T.Add(New Bloom(E.Current.Key, E.Current.Value))
            End While

            Return T.ToArray
        End Get
        Set(ByVal value As Bloom())
            For Each B As Bloom In value
                If Items.ContainsKey(B.Name) Then Items(B.Name) = B.Value
            Next

            InvalidateCustimization()
            ColorHook()
            Invalidate()
        End Set
    End Property

    Private _Customization As String
    Property Customization() As String
        Get
            Return _Customization
        End Get
        Set(ByVal value As String)
            If value = _Customization Then Return

            Dim Data As Byte()
            Dim Items As Bloom() = Colors

            Try
                Data = Convert.FromBase64String(value)
                For I As Integer = 0 To Items.Length - 1
                    Items(I).Value = Color.FromArgb(BitConverter.ToInt32(Data, I * 4))
                Next
            Catch
                Return
            End Try

            _Customization = value

            Colors = Items
            ColorHook()
            Invalidate()
        End Set
    End Property

    Private _Transparent As Boolean
    Property Transparent() As Boolean
        Get
            Return _Transparent
        End Get
        Set(ByVal value As Boolean)
            _Transparent = value
            If Not (IsHandleCreated OrElse _ControlMode) Then Return

            If Not value AndAlso Not BackColor.A = 255 Then
                Throw New Exception("Unable to change value to false while a transparent BackColor is in use.")
            End If

            SetStyle(ControlStyles.Opaque, Not value)
            SetStyle(ControlStyles.SupportsTransparentBackColor, value)

            InvalidateBitmap()
            Invalidate()
        End Set
    End Property

#End Region

#Region " Private Properties "

    Private _ImageSize As Size
    Protected ReadOnly Property ImageSize() As Size
        Get
            Return _ImageSize
        End Get
    End Property

    Private _IsParentForm As Boolean
    Protected ReadOnly Property IsParentForm As Boolean
        Get
            Return _IsParentForm
        End Get
    End Property

    Protected ReadOnly Property IsParentMdi As Boolean
        Get
            If Parent Is Nothing Then Return False
            Return Parent.Parent IsNot Nothing
        End Get
    End Property

    Private _LockWidth As Integer
    Protected Property LockWidth() As Integer
        Get
            Return _LockWidth
        End Get
        Set(ByVal value As Integer)
            _LockWidth = value
            If Not LockWidth = 0 AndAlso IsHandleCreated Then Width = LockWidth
        End Set
    End Property

    Private _LockHeight As Integer
    Protected Property LockHeight() As Integer
        Get
            Return _LockHeight
        End Get
        Set(ByVal value As Integer)
            _LockHeight = value
            If Not LockHeight = 0 AndAlso IsHandleCreated Then Height = LockHeight
        End Set
    End Property

    Private _Header As Integer = 24
    Protected Property Header() As Integer
        Get
            Return _Header
        End Get
        Set(ByVal v As Integer)
            _Header = v

            If Not _ControlMode Then
                Frame = New Rectangle(7, 7, Width - 14, v - 7)
                Invalidate()
            End If
        End Set
    End Property

    Private _ControlMode As Boolean
    Protected Property ControlMode() As Boolean
        Get
            Return _ControlMode
        End Get
        Set(ByVal v As Boolean)
            _ControlMode = v

            Transparent = _Transparent
            If _Transparent AndAlso _BackColor Then BackColor = Color.Transparent

            InvalidateBitmap()
            Invalidate()
        End Set
    End Property

#End Region


#Region " Property Helpers "

    Protected Function GetPen(ByVal name As String) As Pen
        Return New Pen(Items(name))
    End Function
    Protected Function GetPen(ByVal name As String, ByVal width As Single) As Pen
        Return New Pen(Items(name), width)
    End Function

    Protected Function GetBrush(ByVal name As String) As SolidBrush
        Return New SolidBrush(Items(name))
    End Function

    Protected Function GetColor(ByVal name As String) As Color
        Return Items(name)
    End Function

    Protected Sub SetColor(ByVal name As String, ByVal value As Color)
        If Items.ContainsKey(name) Then Items(name) = value Else Items.Add(name, value)
    End Sub
    Protected Sub SetColor(ByVal name As String, ByVal r As Byte, ByVal g As Byte, ByVal b As Byte)
        SetColor(name, Color.FromArgb(r, g, b))
    End Sub
    Protected Sub SetColor(ByVal name As String, ByVal a As Byte, ByVal r As Byte, ByVal g As Byte, ByVal b As Byte)
        SetColor(name, Color.FromArgb(a, r, g, b))
    End Sub
    Protected Sub SetColor(ByVal name As String, ByVal a As Byte, ByVal value As Color)
        SetColor(name, Color.FromArgb(a, value))
    End Sub

    Private Sub InvalidateBitmap()
        If _Transparent AndAlso _ControlMode Then
            If Width = 0 OrElse Height = 0 Then Return
            B = New Bitmap(Width, Height, PixelFormat.Format32bppPArgb)
            G = Graphics.FromImage(B)
        Else
            G = Nothing
            B = Nothing
        End If
    End Sub

    Private Sub InvalidateCustimization()
        Dim M As New MemoryStream(Items.Count * 4)

        For Each B As Bloom In Colors
            M.Write(BitConverter.GetBytes(B.Value.ToArgb), 0, 4)
        Next

        M.Close()
        _Customization = Convert.ToBase64String(M.ToArray)
    End Sub

#End Region


#Region " User Hooks "

    Protected MustOverride Sub ColorHook()
    Protected MustOverride Sub PaintHook()

    Protected Overridable Sub OnCreation()
    End Sub

#End Region


#Region " Offset "

    Private OffsetReturnRectangle As Rectangle
    Protected Function Offset(ByVal r As Rectangle, ByVal amount As Integer) As Rectangle
        OffsetReturnRectangle = New Rectangle(r.X + amount, r.Y + amount, r.Width - (amount * 2), r.Height - (amount * 2))
        Return OffsetReturnRectangle
    End Function

    Private OffsetReturnSize As Size
    Protected Function Offset(ByVal s As Size, ByVal amount As Integer) As Size
        OffsetReturnSize = New Size(s.Width + amount, s.Height + amount)
        Return OffsetReturnSize
    End Function

    Private OffsetReturnPoint As Point
    Protected Function Offset(ByVal p As Point, ByVal amount As Integer) As Point
        OffsetReturnPoint = New Point(p.X + amount, p.Y + amount)
        Return OffsetReturnPoint
    End Function

#End Region

#Region " Center "

    Private CenterReturn As Point

    Protected Function Center(ByVal p As Rectangle, ByVal c As Rectangle) As Point
        CenterReturn = New Point((p.Width \ 2 - c.Width \ 2) + p.X + c.X, (p.Height \ 2 - c.Height \ 2) + p.Y + c.Y)
        Return CenterReturn
    End Function
    Protected Function Center(ByVal p As Rectangle, ByVal c As Size) As Point
        CenterReturn = New Point((p.Width \ 2 - c.Width \ 2) + p.X, (p.Height \ 2 - c.Height \ 2) + p.Y)
        Return CenterReturn
    End Function

    Protected Function Center(ByVal child As Rectangle) As Point
        Return Center(Width, Height, child.Width, child.Height)
    End Function
    Protected Function Center(ByVal child As Size) As Point
        Return Center(Width, Height, child.Width, child.Height)
    End Function
    Protected Function Center(ByVal childWidth As Integer, ByVal childHeight As Integer) As Point
        Return Center(Width, Height, childWidth, childHeight)
    End Function

    Protected Function Center(ByVal p As Size, ByVal c As Size) As Point
        Return Center(p.Width, p.Height, c.Width, c.Height)
    End Function

    Protected Function Center(ByVal pWidth As Integer, ByVal pHeight As Integer, ByVal cWidth As Integer, ByVal cHeight As Integer) As Point
        CenterReturn = New Point(pWidth \ 2 - cWidth \ 2, pHeight \ 2 - cHeight \ 2)
        Return CenterReturn
    End Function

#End Region

#Region " Measure "

    Private MeasureBitmap As Bitmap
    Private MeasureGraphics As Graphics 'TODO: Potential issues during multi-threading.

    Protected Function Measure() As Size
        Return MeasureGraphics.MeasureString(Text, Font, Width).ToSize
    End Function
    Protected Function Measure(ByVal text As String) As Size
        Return MeasureGraphics.MeasureString(text, Font, Width).ToSize
    End Function

#End Region


#Region " DrawPixel "

    Private DrawPixelBrush As SolidBrush

    Protected Sub DrawPixel(ByVal c1 As Color, ByVal x As Integer, ByVal y As Integer)
        If _Transparent Then
            B.SetPixel(x, y, c1)
        Else
            DrawPixelBrush = New SolidBrush(c1)
            G.FillRectangle(DrawPixelBrush, x, y, 1, 1)
        End If
    End Sub

#End Region

#Region " DrawCorners "

    Private DrawCornersBrush As SolidBrush

    Protected Sub DrawCorners(ByVal c1 As Color, ByVal offset As Integer)
        DrawCorners(c1, 0, 0, Width, Height, offset)
    End Sub
    Protected Sub DrawCorners(ByVal c1 As Color, ByVal r1 As Rectangle, ByVal offset As Integer)
        DrawCorners(c1, r1.X, r1.Y, r1.Width, r1.Height, offset)
    End Sub
    Protected Sub DrawCorners(ByVal c1 As Color, ByVal x As Integer, ByVal y As Integer, ByVal width As Integer, ByVal height As Integer, ByVal offset As Integer)
        DrawCorners(c1, x + offset, y + offset, width - (offset * 2), height - (offset * 2))
    End Sub

    Protected Sub DrawCorners(ByVal c1 As Color)
        DrawCorners(c1, 0, 0, Width, Height)
    End Sub
    Protected Sub DrawCorners(ByVal c1 As Color, ByVal r1 As Rectangle)
        DrawCorners(c1, r1.X, r1.Y, r1.Width, r1.Height)
    End Sub
    Protected Sub DrawCorners(ByVal c1 As Color, ByVal x As Integer, ByVal y As Integer, ByVal width As Integer, ByVal height As Integer)
        If _NoRounding Then Return

        If _Transparent Then
            B.SetPixel(x, y, c1)
            B.SetPixel(x + (width - 1), y, c1)
            B.SetPixel(x, y + (height - 1), c1)
            B.SetPixel(x + (width - 1), y + (height - 1), c1)
        Else
            DrawCornersBrush = New SolidBrush(c1)
            G.FillRectangle(DrawCornersBrush, x, y, 1, 1)
            G.FillRectangle(DrawCornersBrush, x + (width - 1), y, 1, 1)
            G.FillRectangle(DrawCornersBrush, x, y + (height - 1), 1, 1)
            G.FillRectangle(DrawCornersBrush, x + (width - 1), y + (height - 1), 1, 1)
        End If
    End Sub

#End Region

#Region " DrawBorders "

    Protected Sub DrawBorders(ByVal p1 As Pen, ByVal offset As Integer)
        DrawBorders(p1, 0, 0, Width, Height, offset)
    End Sub
    Protected Sub DrawBorders(ByVal p1 As Pen, ByVal r As Rectangle, ByVal offset As Integer)
        DrawBorders(p1, r.X, r.Y, r.Width, r.Height, offset)
    End Sub
    Protected Sub DrawBorders(ByVal p1 As Pen, ByVal x As Integer, ByVal y As Integer, ByVal width As Integer, ByVal height As Integer, ByVal offset As Integer)
        DrawBorders(p1, x + offset, y + offset, width - (offset * 2), height - (offset * 2))
    End Sub

    Protected Sub DrawBorders(ByVal p1 As Pen)
        DrawBorders(p1, 0, 0, Width, Height)
    End Sub
    Protected Sub DrawBorders(ByVal p1 As Pen, ByVal r As Rectangle)
        DrawBorders(p1, r.X, r.Y, r.Width, r.Height)
    End Sub
    Protected Sub DrawBorders(ByVal p1 As Pen, ByVal x As Integer, ByVal y As Integer, ByVal width As Integer, ByVal height As Integer)
        G.DrawRectangle(p1, x, y, width - 1, height - 1)
    End Sub

#End Region

#Region " DrawText "

    Private DrawTextPoint As Point
    Private DrawTextSize As Size

    Protected Sub DrawText(ByVal b1 As Brush, ByVal a As HorizontalAlignment, ByVal x As Integer, ByVal y As Integer, ByVal STR As String)
        DrawText(b1, STR, a, x, y)
    End Sub
    Protected Sub DrawText(ByVal b1 As Brush, ByVal text As String, ByVal a As HorizontalAlignment, ByVal x As Integer, ByVal y As Integer)
        If text.Length = 0 Then Return

        DrawTextSize = Measure(text)
        DrawTextPoint = New Point(Width \ 2 - DrawTextSize.Width \ 2, Header \ 2 - DrawTextSize.Height \ 2)

        Select Case a
            Case HorizontalAlignment.Left
                G.DrawString(text, Font, b1, x, DrawTextPoint.Y + y)
            Case HorizontalAlignment.Center
                G.DrawString(text, Font, b1, DrawTextPoint.X + x, DrawTextPoint.Y + y)
            Case HorizontalAlignment.Right
                G.DrawString(text, Font, b1, Width - DrawTextSize.Width - x, DrawTextPoint.Y + y)
        End Select
    End Sub

    Protected Sub DrawText(ByVal b1 As Brush, ByVal p1 As Point)
        If Text.Length = 0 Then Return
        G.DrawString(Text, Font, b1, p1)
    End Sub
    Protected Sub DrawText(ByVal b1 As Brush, ByVal x As Integer, ByVal y As Integer)
        If Text.Length = 0 Then Return
        G.DrawString(Text, Font, b1, x, y)
    End Sub

#End Region

#Region " DrawImage "

    Private DrawImagePoint As Point

    Protected Sub DrawImage(ByVal a As HorizontalAlignment, ByVal x As Integer, ByVal y As Integer)
        DrawImage(_Image, a, x, y)
    End Sub
    Protected Sub DrawImage(ByVal image As Image, ByVal a As HorizontalAlignment, ByVal x As Integer, ByVal y As Integer)
        If image Is Nothing Then Return
        DrawImagePoint = New Point(Width \ 2 - image.Width \ 2, Header \ 2 - image.Height \ 2)

        Select Case a
            Case HorizontalAlignment.Left
                G.DrawImage(image, x, DrawImagePoint.Y + y, image.Width, image.Height)
            Case HorizontalAlignment.Center
                G.DrawImage(image, DrawImagePoint.X + x, DrawImagePoint.Y + y, image.Width, image.Height)
            Case HorizontalAlignment.Right
                G.DrawImage(image, Width - image.Width - x, DrawImagePoint.Y + y, image.Width, image.Height)
        End Select
    End Sub

    Protected Sub DrawImage(ByVal p1 As Point)
        DrawImage(_Image, p1.X, p1.Y)
    End Sub
    Protected Sub DrawImage(ByVal x As Integer, ByVal y As Integer)
        DrawImage(_Image, x, y)
    End Sub

    Protected Sub DrawImage(ByVal image As Image, ByVal p1 As Point)
        DrawImage(image, p1.X, p1.Y)
    End Sub
    Protected Sub DrawImage(ByVal image As Image, ByVal x As Integer, ByVal y As Integer)
        If image Is Nothing Then Return
        G.DrawImage(image, x, y, image.Width, image.Height)
    End Sub

#End Region

#Region " DrawGradient "

    Private DrawGradientBrush As LinearGradientBrush
    Private DrawGradientRectangle As Rectangle

    Protected Sub DrawGradient(ByVal blend As ColorBlend, ByVal x As Integer, ByVal y As Integer, ByVal width As Integer, ByVal height As Integer)
        DrawGradientRectangle = New Rectangle(x, y, width, height)
        DrawGradient(blend, DrawGradientRectangle)
    End Sub
    Protected Sub DrawGradient(ByVal blend As ColorBlend, ByVal x As Integer, ByVal y As Integer, ByVal width As Integer, ByVal height As Integer, ByVal angle As Single)
        DrawGradientRectangle = New Rectangle(x, y, width, height)
        DrawGradient(blend, DrawGradientRectangle, angle)
    End Sub

    Protected Sub DrawGradient(ByVal blend As ColorBlend, ByVal r As Rectangle)
        DrawGradientBrush = New LinearGradientBrush(r, Color.Empty, Color.Empty, 90.0F)
        DrawGradientBrush.InterpolationColors = blend
        G.FillRectangle(DrawGradientBrush, r)
    End Sub
    Protected Sub DrawGradient(ByVal blend As ColorBlend, ByVal r As Rectangle, ByVal angle As Single)
        DrawGradientBrush = New LinearGradientBrush(r, Color.Empty, Color.Empty, angle)
        DrawGradientBrush.InterpolationColors = blend
        G.FillRectangle(DrawGradientBrush, r)
    End Sub


    Protected Sub DrawGradient(ByVal c1 As Color, ByVal c2 As Color, ByVal x As Integer, ByVal y As Integer, ByVal width As Integer, ByVal height As Integer)
        DrawGradientRectangle = New Rectangle(x, y, width, height)
        DrawGradient(c1, c2, DrawGradientRectangle)
    End Sub
    Protected Sub DrawGradient(ByVal c1 As Color, ByVal c2 As Color, ByVal x As Integer, ByVal y As Integer, ByVal width As Integer, ByVal height As Integer, ByVal angle As Single)
        DrawGradientRectangle = New Rectangle(x, y, width, height)
        DrawGradient(c1, c2, DrawGradientRectangle, angle)
    End Sub

    Protected Sub DrawGradient(ByVal c1 As Color, ByVal c2 As Color, ByVal r As Rectangle)
        DrawGradientBrush = New LinearGradientBrush(r, c1, c2, 90.0F)
        G.FillRectangle(DrawGradientBrush, r)
    End Sub
    Protected Sub DrawGradient(ByVal c1 As Color, ByVal c2 As Color, ByVal r As Rectangle, ByVal angle As Single)
        DrawGradientBrush = New LinearGradientBrush(r, c1, c2, angle)
        G.FillRectangle(DrawGradientBrush, r)
    End Sub

#End Region

#Region " DrawRadial "

    Private DrawRadialPath As GraphicsPath
    Private DrawRadialBrush1 As PathGradientBrush
    Private DrawRadialBrush2 As LinearGradientBrush
    Private DrawRadialRectangle As Rectangle

    Sub DrawRadial(ByVal blend As ColorBlend, ByVal x As Integer, ByVal y As Integer, ByVal width As Integer, ByVal height As Integer)
        DrawRadialRectangle = New Rectangle(x, y, width, height)
        DrawRadial(blend, DrawRadialRectangle, width \ 2, height \ 2)
    End Sub
    Sub DrawRadial(ByVal blend As ColorBlend, ByVal x As Integer, ByVal y As Integer, ByVal width As Integer, ByVal height As Integer, ByVal center As Point)
        DrawRadialRectangle = New Rectangle(x, y, width, height)
        DrawRadial(blend, DrawRadialRectangle, center.X, center.Y)
    End Sub
    Sub DrawRadial(ByVal blend As ColorBlend, ByVal x As Integer, ByVal y As Integer, ByVal width As Integer, ByVal height As Integer, ByVal cx As Integer, ByVal cy As Integer)
        DrawRadialRectangle = New Rectangle(x, y, width, height)
        DrawRadial(blend, DrawRadialRectangle, cx, cy)
    End Sub

    Sub DrawRadial(ByVal blend As ColorBlend, ByVal r As Rectangle)
        DrawRadial(blend, r, r.Width \ 2, r.Height \ 2)
    End Sub
    Sub DrawRadial(ByVal blend As ColorBlend, ByVal r As Rectangle, ByVal center As Point)
        DrawRadial(blend, r, center.X, center.Y)
    End Sub
    Sub DrawRadial(ByVal blend As ColorBlend, ByVal r As Rectangle, ByVal cx As Integer, ByVal cy As Integer)
        DrawRadialPath.Reset()
        DrawRadialPath.AddEllipse(r.X, r.Y, r.Width - 1, r.Height - 1)

        DrawRadialBrush1 = New PathGradientBrush(DrawRadialPath)
        DrawRadialBrush1.CenterPoint = New Point(r.X + cx, r.Y + cy)
        DrawRadialBrush1.InterpolationColors = blend

        If G.SmoothingMode = SmoothingMode.AntiAlias Then
            G.FillEllipse(DrawRadialBrush1, r.X + 1, r.Y + 1, r.Width - 3, r.Height - 3)
        Else
            G.FillEllipse(DrawRadialBrush1, r)
        End If
    End Sub


    Protected Sub DrawRadial(ByVal c1 As Color, ByVal c2 As Color, ByVal x As Integer, ByVal y As Integer, ByVal width As Integer, ByVal height As Integer)
        DrawRadialRectangle = New Rectangle(x, y, width, height)
        DrawRadial(c1, c2, DrawGradientRectangle)
    End Sub
    Protected Sub DrawRadial(ByVal c1 As Color, ByVal c2 As Color, ByVal x As Integer, ByVal y As Integer, ByVal width As Integer, ByVal height As Integer, ByVal angle As Single)
        DrawRadialRectangle = New Rectangle(x, y, width, height)
        DrawRadial(c1, c2, DrawGradientRectangle, angle)
    End Sub

    Protected Sub DrawRadial(ByVal c1 As Color, ByVal c2 As Color, ByVal r As Rectangle)
        DrawRadialBrush2 = New LinearGradientBrush(r, c1, c2, 90.0F)
        G.FillRectangle(DrawGradientBrush, r)
    End Sub
    Protected Sub DrawRadial(ByVal c1 As Color, ByVal c2 As Color, ByVal r As Rectangle, ByVal angle As Single)
        DrawRadialBrush2 = New LinearGradientBrush(r, c1, c2, angle)
        G.FillEllipse(DrawGradientBrush, r)
    End Sub

#End Region

#End Region

End Class

MustInherit Class ThemeControl153
    Inherits Control

#Region "Themebase"

#Region " Initialization "

    Protected G As Graphics, B As Bitmap

    Sub New()
        SetStyle(DirectCast(139270, ControlStyles), True)

        _ImageSize = Size.Empty
        Font = New Font("Verdana", 8S)

        MeasureBitmap = New Bitmap(1, 1)
        MeasureGraphics = Graphics.FromImage(MeasureBitmap)

        DrawRadialPath = New GraphicsPath

        InvalidateCustimization() 'Remove?
    End Sub

    Protected NotOverridable Overrides Sub OnHandleCreated(ByVal e As EventArgs)
        InvalidateCustimization()
        ColorHook()

        If Not _LockWidth = 0 Then Width = _LockWidth
        If Not _LockHeight = 0 Then Height = _LockHeight

        Transparent = _Transparent
        If _Transparent AndAlso _BackColor Then BackColor = Color.Transparent

        MyBase.OnHandleCreated(e)
    End Sub

    Protected NotOverridable Overrides Sub OnParentChanged(ByVal e As EventArgs)
        If Parent IsNot Nothing Then OnCreation()
        MyBase.OnParentChanged(e)
    End Sub

#End Region


    Protected NotOverridable Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        If Width = 0 OrElse Height = 0 Then Return

        If _Transparent Then
            PaintHook()
            e.Graphics.DrawImage(B, 0, 0)
        Else
            G = e.Graphics
            PaintHook()
        End If
    End Sub


#Region " Size Handling "

    Protected NotOverridable Overrides Sub OnSizeChanged(ByVal e As EventArgs)
        If _Transparent Then
            InvalidateBitmap()
        End If

        Invalidate()
        MyBase.OnSizeChanged(e)
    End Sub

    Protected Overrides Sub SetBoundsCore(ByVal x As Integer, ByVal y As Integer, ByVal width As Integer, ByVal height As Integer, ByVal specified As BoundsSpecified)
        If Not _LockWidth = 0 Then width = _LockWidth
        If Not _LockHeight = 0 Then height = _LockHeight
        MyBase.SetBoundsCore(x, y, width, height, specified)
    End Sub

#End Region

#Region " State Handling "

    Private InPosition As Boolean
    Protected Overrides Sub OnMouseEnter(ByVal e As EventArgs)
        InPosition = True
        SetState(MouseState.Over)
        MyBase.OnMouseEnter(e)
    End Sub

    Protected Overrides Sub OnMouseUp(ByVal e As MouseEventArgs)
        If InPosition Then SetState(MouseState.Over)
        MyBase.OnMouseUp(e)
    End Sub

    Protected Overrides Sub OnMouseDown(ByVal e As MouseEventArgs)
        If e.Button = Windows.Forms.MouseButtons.Left Then SetState(MouseState.Down)
        MyBase.OnMouseDown(e)
    End Sub

    Protected Overrides Sub OnMouseLeave(ByVal e As EventArgs)
        InPosition = False
        SetState(MouseState.None)
        MyBase.OnMouseLeave(e)
    End Sub

    Protected Overrides Sub OnEnabledChanged(ByVal e As EventArgs)
        If Enabled Then SetState(MouseState.None) Else SetState(MouseState.Block)
        MyBase.OnEnabledChanged(e)
    End Sub

    Protected State As MouseState
    Private Sub SetState(ByVal current As MouseState)
        State = current
        Invalidate()
    End Sub

#End Region


#Region " Base Properties "

    <Browsable(False), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
    Overrides Property ForeColor() As Color
        Get
            Return Color.Empty
        End Get
        Set(ByVal value As Color)
        End Set
    End Property
    <Browsable(False), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
    Overrides Property BackgroundImage() As Image
        Get
            Return Nothing
        End Get
        Set(ByVal value As Image)
        End Set
    End Property
    <Browsable(False), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
    Overrides Property BackgroundImageLayout() As ImageLayout
        Get
            Return ImageLayout.None
        End Get
        Set(ByVal value As ImageLayout)
        End Set
    End Property

    Overrides Property Text() As String
        Get
            Return MyBase.Text
        End Get
        Set(ByVal value As String)
            MyBase.Text = value
            Invalidate()
        End Set
    End Property
    Overrides Property Font() As Font
        Get
            Return MyBase.Font
        End Get
        Set(ByVal value As Font)
            MyBase.Font = value
            Invalidate()
        End Set
    End Property

    Private _BackColor As Boolean
    <Category("Misc")> _
    Overrides Property BackColor() As Color
        Get
            Return MyBase.BackColor
        End Get
        Set(ByVal value As Color)
            If Not IsHandleCreated AndAlso value = Color.Transparent Then
                _BackColor = True
                Return
            End If

            MyBase.BackColor = value
            If Parent IsNot Nothing Then ColorHook()
        End Set
    End Property

#End Region

#Region " Public Properties "

    Private _NoRounding As Boolean
    Property NoRounding() As Boolean
        Get
            Return _NoRounding
        End Get
        Set(ByVal v As Boolean)
            _NoRounding = v
            Invalidate()
        End Set
    End Property

    Private _Image As Image
    Property Image() As Image
        Get
            Return _Image
        End Get
        Set(ByVal value As Image)
            If value Is Nothing Then
                _ImageSize = Size.Empty
            Else
                _ImageSize = value.Size
            End If

            _Image = value
            Invalidate()
        End Set
    End Property

    Private _Transparent As Boolean
    Property Transparent() As Boolean
        Get
            Return _Transparent
        End Get
        Set(ByVal value As Boolean)
            _Transparent = value
            If Not IsHandleCreated Then Return

            If Not value AndAlso Not BackColor.A = 255 Then
                Throw New Exception("Unable to change value to false while a transparent BackColor is in use.")
            End If

            SetStyle(ControlStyles.Opaque, Not value)
            SetStyle(ControlStyles.SupportsTransparentBackColor, value)

            If value Then InvalidateBitmap() Else B = Nothing
            Invalidate()
        End Set
    End Property

    Private Items As New Dictionary(Of String, Color)
    Property Colors() As Bloom()
        Get
            Dim T As New List(Of Bloom)
            Dim E As Dictionary(Of String, Color).Enumerator = Items.GetEnumerator

            While E.MoveNext
                T.Add(New Bloom(E.Current.Key, E.Current.Value))
            End While

            Return T.ToArray
        End Get
        Set(ByVal value As Bloom())
            For Each B As Bloom In value
                If Items.ContainsKey(B.Name) Then Items(B.Name) = B.Value
            Next

            InvalidateCustimization()
            ColorHook()
            Invalidate()
        End Set
    End Property

    Private _Customization As String
    Property Customization() As String
        Get
            Return _Customization
        End Get
        Set(ByVal value As String)
            If value = _Customization Then Return

            Dim Data As Byte()
            Dim Items As Bloom() = Colors

            Try
                Data = Convert.FromBase64String(value)
                For I As Integer = 0 To Items.Length - 1
                    Items(I).Value = Color.FromArgb(BitConverter.ToInt32(Data, I * 4))
                Next
            Catch
                Return
            End Try

            _Customization = value

            Colors = Items
            ColorHook()
            Invalidate()
        End Set
    End Property

#End Region

#Region " Private Properties "

    Private _ImageSize As Size
    Protected ReadOnly Property ImageSize() As Size
        Get
            Return _ImageSize
        End Get
    End Property

    Private _LockWidth As Integer
    Protected Property LockWidth() As Integer
        Get
            Return _LockWidth
        End Get
        Set(ByVal value As Integer)
            _LockWidth = value
            If Not LockWidth = 0 AndAlso IsHandleCreated Then Width = LockWidth
        End Set
    End Property

    Private _LockHeight As Integer
    Protected Property LockHeight() As Integer
        Get
            Return _LockHeight
        End Get
        Set(ByVal value As Integer)
            _LockHeight = value
            If Not LockHeight = 0 AndAlso IsHandleCreated Then Height = LockHeight
        End Set
    End Property

#End Region


#Region " Property Helpers "

    Protected Function GetPen(ByVal name As String) As Pen
        Return New Pen(Items(name))
    End Function
    Protected Function GetPen(ByVal name As String, ByVal width As Single) As Pen
        Return New Pen(Items(name), width)
    End Function

    Protected Function GetBrush(ByVal name As String) As SolidBrush
        Return New SolidBrush(Items(name))
    End Function

    Protected Function GetColor(ByVal name As String) As Color
        Return Items(name)
    End Function

    Protected Sub SetColor(ByVal name As String, ByVal value As Color)
        If Items.ContainsKey(name) Then Items(name) = value Else Items.Add(name, value)
    End Sub
    Protected Sub SetColor(ByVal name As String, ByVal r As Byte, ByVal g As Byte, ByVal b As Byte)
        SetColor(name, Color.FromArgb(r, g, b))
    End Sub
    Protected Sub SetColor(ByVal name As String, ByVal a As Byte, ByVal r As Byte, ByVal g As Byte, ByVal b As Byte)
        SetColor(name, Color.FromArgb(a, r, g, b))
    End Sub
    Protected Sub SetColor(ByVal name As String, ByVal a As Byte, ByVal value As Color)
        SetColor(name, Color.FromArgb(a, value))
    End Sub

    Private Sub InvalidateBitmap()
        If Width = 0 OrElse Height = 0 Then Return
        B = New Bitmap(Width, Height, PixelFormat.Format32bppPArgb)
        G = Graphics.FromImage(B)
    End Sub

    Private Sub InvalidateCustimization()
        Dim M As New MemoryStream(Items.Count * 4)

        For Each B As Bloom In Colors
            M.Write(BitConverter.GetBytes(B.Value.ToArgb), 0, 4)
        Next

        M.Close()
        _Customization = Convert.ToBase64String(M.ToArray)
    End Sub

#End Region


#Region " User Hooks "

    Protected MustOverride Sub ColorHook()
    Protected MustOverride Sub PaintHook()

    Protected Overridable Sub OnCreation()
    End Sub

#End Region


#Region " Offset "

    Private OffsetReturnRectangle As Rectangle
    Protected Function Offset(ByVal r As Rectangle, ByVal amount As Integer) As Rectangle
        OffsetReturnRectangle = New Rectangle(r.X + amount, r.Y + amount, r.Width - (amount * 2), r.Height - (amount * 2))
        Return OffsetReturnRectangle
    End Function

    Private OffsetReturnSize As Size
    Protected Function Offset(ByVal s As Size, ByVal amount As Integer) As Size
        OffsetReturnSize = New Size(s.Width + amount, s.Height + amount)
        Return OffsetReturnSize
    End Function

    Private OffsetReturnPoint As Point
    Protected Function Offset(ByVal p As Point, ByVal amount As Integer) As Point
        OffsetReturnPoint = New Point(p.X + amount, p.Y + amount)
        Return OffsetReturnPoint
    End Function

#End Region

#Region " Center "

    Private CenterReturn As Point

    Protected Function Center(ByVal p As Rectangle, ByVal c As Rectangle) As Point
        CenterReturn = New Point((p.Width \ 2 - c.Width \ 2) + p.X + c.X, (p.Height \ 2 - c.Height \ 2) + p.Y + c.Y)
        Return CenterReturn
    End Function
    Protected Function Center(ByVal p As Rectangle, ByVal c As Size) As Point
        CenterReturn = New Point((p.Width \ 2 - c.Width \ 2) + p.X, (p.Height \ 2 - c.Height \ 2) + p.Y)
        Return CenterReturn
    End Function

    Protected Function Center(ByVal child As Rectangle) As Point
        Return Center(Width, Height, child.Width, child.Height)
    End Function
    Protected Function Center(ByVal child As Size) As Point
        Return Center(Width, Height, child.Width, child.Height)
    End Function
    Protected Function Center(ByVal childWidth As Integer, ByVal childHeight As Integer) As Point
        Return Center(Width, Height, childWidth, childHeight)
    End Function

    Protected Function Center(ByVal p As Size, ByVal c As Size) As Point
        Return Center(p.Width, p.Height, c.Width, c.Height)
    End Function

    Protected Function Center(ByVal pWidth As Integer, ByVal pHeight As Integer, ByVal cWidth As Integer, ByVal cHeight As Integer) As Point
        CenterReturn = New Point(pWidth \ 2 - cWidth \ 2, pHeight \ 2 - cHeight \ 2)
        Return CenterReturn
    End Function

#End Region

#Region " Measure "

    Private MeasureBitmap As Bitmap
    Private MeasureGraphics As Graphics 'TODO: Potential issues during multi-threading.

    Protected Function Measure() As Size
        Return MeasureGraphics.MeasureString(Text, Font, Width).ToSize
    End Function
    Protected Function Measure(ByVal text As String) As Size
        Return MeasureGraphics.MeasureString(text, Font, Width).ToSize
    End Function

#End Region


#Region " DrawPixel "

    Private DrawPixelBrush As SolidBrush

    Protected Sub DrawPixel(ByVal c1 As Color, ByVal x As Integer, ByVal y As Integer)
        If _Transparent Then
            B.SetPixel(x, y, c1)
        Else
            DrawPixelBrush = New SolidBrush(c1)
            G.FillRectangle(DrawPixelBrush, x, y, 1, 1)
        End If
    End Sub

#End Region

#Region " DrawCorners "

    Private DrawCornersBrush As SolidBrush

    Protected Sub DrawCorners(ByVal c1 As Color, ByVal offset As Integer)
        DrawCorners(c1, 0, 0, Width, Height, offset)
    End Sub
    Protected Sub DrawCorners(ByVal c1 As Color, ByVal r1 As Rectangle, ByVal offset As Integer)
        DrawCorners(c1, r1.X, r1.Y, r1.Width, r1.Height, offset)
    End Sub
    Protected Sub DrawCorners(ByVal c1 As Color, ByVal x As Integer, ByVal y As Integer, ByVal width As Integer, ByVal height As Integer, ByVal offset As Integer)
        DrawCorners(c1, x + offset, y + offset, width - (offset * 2), height - (offset * 2))
    End Sub

    Protected Sub DrawCorners(ByVal c1 As Color)
        DrawCorners(c1, 0, 0, Width, Height)
    End Sub
    Protected Sub DrawCorners(ByVal c1 As Color, ByVal r1 As Rectangle)
        DrawCorners(c1, r1.X, r1.Y, r1.Width, r1.Height)
    End Sub
    Protected Sub DrawCorners(ByVal c1 As Color, ByVal x As Integer, ByVal y As Integer, ByVal width As Integer, ByVal height As Integer)
        If _NoRounding Then Return

        If _Transparent Then
            B.SetPixel(x, y, c1)
            B.SetPixel(x + (width - 1), y, c1)
            B.SetPixel(x, y + (height - 1), c1)
            B.SetPixel(x + (width - 1), y + (height - 1), c1)
        Else
            DrawCornersBrush = New SolidBrush(c1)
            G.FillRectangle(DrawCornersBrush, x, y, 1, 1)
            G.FillRectangle(DrawCornersBrush, x + (width - 1), y, 1, 1)
            G.FillRectangle(DrawCornersBrush, x, y + (height - 1), 1, 1)
            G.FillRectangle(DrawCornersBrush, x + (width - 1), y + (height - 1), 1, 1)
        End If
    End Sub

#End Region

#Region " DrawBorders "

    Protected Sub DrawBorders(ByVal p1 As Pen, ByVal offset As Integer)
        DrawBorders(p1, 0, 0, Width, Height, offset)
    End Sub
    Protected Sub DrawBorders(ByVal p1 As Pen, ByVal r As Rectangle, ByVal offset As Integer)
        DrawBorders(p1, r.X, r.Y, r.Width, r.Height, offset)
    End Sub
    Protected Sub DrawBorders(ByVal p1 As Pen, ByVal x As Integer, ByVal y As Integer, ByVal width As Integer, ByVal height As Integer, ByVal offset As Integer)
        DrawBorders(p1, x + offset, y + offset, width - (offset * 2), height - (offset * 2))
    End Sub

    Protected Sub DrawBorders(ByVal p1 As Pen)
        DrawBorders(p1, 0, 0, Width, Height)
    End Sub
    Protected Sub DrawBorders(ByVal p1 As Pen, ByVal r As Rectangle)
        DrawBorders(p1, r.X, r.Y, r.Width, r.Height)
    End Sub
    Protected Sub DrawBorders(ByVal p1 As Pen, ByVal x As Integer, ByVal y As Integer, ByVal width As Integer, ByVal height As Integer)
        G.DrawRectangle(p1, x, y, width - 1, height - 1)
    End Sub

#End Region

#Region " DrawText "

    Private DrawTextPoint As Point
    Private DrawTextSize As Size

    Protected Sub DrawText(ByVal b1 As Brush, ByVal a As HorizontalAlignment, ByVal x As Integer, ByVal y As Integer)
        DrawText(b1, Text, a, x, y)
    End Sub
    Protected Sub DrawText(ByVal b1 As Brush, ByVal text As String, ByVal a As HorizontalAlignment, ByVal x As Integer, ByVal y As Integer)
        If text.Length = 0 Then Return

        DrawTextSize = Measure(text)
        DrawTextPoint = Center(DrawTextSize)

        Select Case a
            Case HorizontalAlignment.Left
                G.DrawString(text, Font, b1, x, DrawTextPoint.Y + y)
            Case HorizontalAlignment.Center
                G.DrawString(text, Font, b1, DrawTextPoint.X + x, DrawTextPoint.Y + y)
            Case HorizontalAlignment.Right
                G.DrawString(text, Font, b1, Width - DrawTextSize.Width - x, DrawTextPoint.Y + y)
        End Select
    End Sub

    Protected Sub DrawText(ByVal b1 As Brush, ByVal p1 As Point)
        If Text.Length = 0 Then Return
        G.DrawString(Text, Font, b1, p1)
    End Sub
    Protected Sub DrawText(ByVal b1 As Brush, ByVal x As Integer, ByVal y As Integer)
        If Text.Length = 0 Then Return
        G.DrawString(Text, Font, b1, x, y)
    End Sub

#End Region

#Region " DrawImage "

    Private DrawImagePoint As Point

    Protected Sub DrawImage(ByVal a As HorizontalAlignment, ByVal x As Integer, ByVal y As Integer)
        DrawImage(_Image, a, x, y)
    End Sub
    Protected Sub DrawImage(ByVal image As Image, ByVal a As HorizontalAlignment, ByVal x As Integer, ByVal y As Integer)
        If image Is Nothing Then Return
        DrawImagePoint = Center(image.Size)

        Select Case a
            Case HorizontalAlignment.Left
                G.DrawImage(image, x, DrawImagePoint.Y + y, image.Width, image.Height)
            Case HorizontalAlignment.Center
                G.DrawImage(image, DrawImagePoint.X + x, DrawImagePoint.Y + y, image.Width, image.Height)
            Case HorizontalAlignment.Right
                G.DrawImage(image, Width - image.Width - x, DrawImagePoint.Y + y, image.Width, image.Height)
        End Select
    End Sub

    Protected Sub DrawImage(ByVal p1 As Point)
        DrawImage(_Image, p1.X, p1.Y)
    End Sub
    Protected Sub DrawImage(ByVal x As Integer, ByVal y As Integer)
        DrawImage(_Image, x, y)
    End Sub

    Protected Sub DrawImage(ByVal image As Image, ByVal p1 As Point)
        DrawImage(image, p1.X, p1.Y)
    End Sub
    Protected Sub DrawImage(ByVal image As Image, ByVal x As Integer, ByVal y As Integer)
        If image Is Nothing Then Return
        G.DrawImage(image, x, y, image.Width, image.Height)
    End Sub

#End Region

#Region " DrawGradient "

    Private DrawGradientBrush As LinearGradientBrush
    Private DrawGradientRectangle As Rectangle

    Protected Sub DrawGradient(ByVal blend As ColorBlend, ByVal x As Integer, ByVal y As Integer, ByVal width As Integer, ByVal height As Integer)
        DrawGradientRectangle = New Rectangle(x, y, width, height)
        DrawGradient(blend, DrawGradientRectangle)
    End Sub
    Protected Sub DrawGradient(ByVal blend As ColorBlend, ByVal x As Integer, ByVal y As Integer, ByVal width As Integer, ByVal height As Integer, ByVal angle As Single)
        DrawGradientRectangle = New Rectangle(x, y, width, height)
        DrawGradient(blend, DrawGradientRectangle, angle)
    End Sub

    Protected Sub DrawGradient(ByVal blend As ColorBlend, ByVal r As Rectangle)
        DrawGradientBrush = New LinearGradientBrush(r, Color.Empty, Color.Empty, 90.0F)
        DrawGradientBrush.InterpolationColors = blend
        G.FillRectangle(DrawGradientBrush, r)
    End Sub
    Protected Sub DrawGradient(ByVal blend As ColorBlend, ByVal r As Rectangle, ByVal angle As Single)
        DrawGradientBrush = New LinearGradientBrush(r, Color.Empty, Color.Empty, angle)
        DrawGradientBrush.InterpolationColors = blend
        G.FillRectangle(DrawGradientBrush, r)
    End Sub


    Protected Sub DrawGradient(ByVal c1 As Color, ByVal c2 As Color, ByVal x As Integer, ByVal y As Integer, ByVal width As Integer, ByVal height As Integer)
        DrawGradientRectangle = New Rectangle(x, y, width, height)
        DrawGradient(c1, c2, DrawGradientRectangle)
    End Sub
    Protected Sub DrawGradient(ByVal c1 As Color, ByVal c2 As Color, ByVal x As Integer, ByVal y As Integer, ByVal width As Integer, ByVal height As Integer, ByVal angle As Single)
        DrawGradientRectangle = New Rectangle(x, y, width, height)
        DrawGradient(c1, c2, DrawGradientRectangle, angle)
    End Sub

    Protected Sub DrawGradient(ByVal c1 As Color, ByVal c2 As Color, ByVal r As Rectangle)
        DrawGradientBrush = New LinearGradientBrush(r, c1, c2, 90.0F)
        G.FillRectangle(DrawGradientBrush, r)
    End Sub
    Protected Sub DrawGradient(ByVal c1 As Color, ByVal c2 As Color, ByVal r As Rectangle, ByVal angle As Single)
        DrawGradientBrush = New LinearGradientBrush(r, c1, c2, angle)
        G.FillRectangle(DrawGradientBrush, r)
    End Sub

#End Region

#Region " DrawRadial "

    Private DrawRadialPath As GraphicsPath
    Private DrawRadialBrush1 As PathGradientBrush
    Private DrawRadialBrush2 As LinearGradientBrush
    Private DrawRadialRectangle As Rectangle

    Sub DrawRadial(ByVal blend As ColorBlend, ByVal x As Integer, ByVal y As Integer, ByVal width As Integer, ByVal height As Integer)
        DrawRadialRectangle = New Rectangle(x, y, width, height)
        DrawRadial(blend, DrawRadialRectangle, width \ 2, height \ 2)
    End Sub
    Sub DrawRadial(ByVal blend As ColorBlend, ByVal x As Integer, ByVal y As Integer, ByVal width As Integer, ByVal height As Integer, ByVal center As Point)
        DrawRadialRectangle = New Rectangle(x, y, width, height)
        DrawRadial(blend, DrawRadialRectangle, center.X, center.Y)
    End Sub
    Sub DrawRadial(ByVal blend As ColorBlend, ByVal x As Integer, ByVal y As Integer, ByVal width As Integer, ByVal height As Integer, ByVal cx As Integer, ByVal cy As Integer)
        DrawRadialRectangle = New Rectangle(x, y, width, height)
        DrawRadial(blend, DrawRadialRectangle, cx, cy)
    End Sub

    Sub DrawRadial(ByVal blend As ColorBlend, ByVal r As Rectangle)
        DrawRadial(blend, r, r.Width \ 2, r.Height \ 2)
    End Sub
    Sub DrawRadial(ByVal blend As ColorBlend, ByVal r As Rectangle, ByVal center As Point)
        DrawRadial(blend, r, center.X, center.Y)
    End Sub
    Sub DrawRadial(ByVal blend As ColorBlend, ByVal r As Rectangle, ByVal cx As Integer, ByVal cy As Integer)
        DrawRadialPath.Reset()
        DrawRadialPath.AddEllipse(r.X, r.Y, r.Width - 1, r.Height - 1)

        DrawRadialBrush1 = New PathGradientBrush(DrawRadialPath)
        DrawRadialBrush1.CenterPoint = New Point(r.X + cx, r.Y + cy)
        DrawRadialBrush1.InterpolationColors = blend

        If G.SmoothingMode = SmoothingMode.AntiAlias Then
            G.FillEllipse(DrawRadialBrush1, r.X + 1, r.Y + 1, r.Width - 3, r.Height - 3)
        Else
            G.FillEllipse(DrawRadialBrush1, r)
        End If
    End Sub


    Protected Sub DrawRadial(ByVal c1 As Color, ByVal c2 As Color, ByVal x As Integer, ByVal y As Integer, ByVal width As Integer, ByVal height As Integer)
        DrawRadialRectangle = New Rectangle(x, y, width, height)
        DrawRadial(c1, c2, DrawRadialRectangle)
    End Sub
    Protected Sub DrawRadial(ByVal c1 As Color, ByVal c2 As Color, ByVal x As Integer, ByVal y As Integer, ByVal width As Integer, ByVal height As Integer, ByVal angle As Single)
        DrawRadialRectangle = New Rectangle(x, y, width, height)
        DrawRadial(c1, c2, DrawRadialRectangle, angle)
    End Sub

    Protected Sub DrawRadial(ByVal c1 As Color, ByVal c2 As Color, ByVal r As Rectangle)
        DrawRadialBrush2 = New LinearGradientBrush(r, c1, c2, 90.0F)
        G.FillEllipse(DrawRadialBrush2, r)
    End Sub
    Protected Sub DrawRadial(ByVal c1 As Color, ByVal c2 As Color, ByVal r As Rectangle, ByVal angle As Single)
        DrawRadialBrush2 = New LinearGradientBrush(r, c1, c2, angle)
        G.FillEllipse(DrawRadialBrush2, r)
    End Sub

#End Region
#End Region

End Class

Enum MouseState As Byte
    None = 0
    Over = 1
    Down = 2
    Block = 3
End Enum

Structure Bloom

    Public _Name As String
    ReadOnly Property Name() As String
        Get
            Return _Name
        End Get
    End Property

    Private _Value As Color
    Property Value() As Color
        Get
            Return _Value
        End Get
        Set(ByVal value As Color)
            _Value = value
        End Set
    End Property

    Sub New(ByVal name As String, ByVal value As Color)
        _Name = name
        _Value = value
    End Sub
End Structure

#End Region



Class SpaceTheme
    Inherits ThemeContainer153

#Region "Theme"

    Sub New()
        Header = 29
        TransparencyKey = Color.DeepPink
        MinimumSize = Me.MinimumSize
        MaximumSize = Me.MaximumSize
        Me.DoubleBuffered = True
        TitleForeColourVal = Color.White
        BackColor = Color.FromArgb(14, 14, 14)
    End Sub

    Protected Overrides Sub ColorHook()

    End Sub

    Private TitleForeColourVal As Color
    Property MinSize As Size
    Property MaxSize As Size

    <System.ComponentModel.Description("Change the font colour of the title text.")> _
    Public Property TitleForeColour() As Color
        Get
            Return TitleForeColourVal
        End Get
        Set(ByVal TitleForeColour As Color)
            TitleForeColourVal = TitleForeColour
            Invalidate()
        End Set
    End Property

    Protected Overrides Sub PaintHook()
        If MinSize <> New Size(0, 0) Or MaxSize <> New Size(0, 0) Then
            Me.ParentForm.MinimumSize = MinSize
            Me.ParentForm.MaximumSize = MaxSize
        End If

        G.Clear(Color.FromArgb(255, 14, 14, 14))

        Dim P As New Pen(Color.FromArgb(255, Color.Black))
        G.DrawLine(P, 32, 1, 56, 25) 'diagonal line left corner
        G.DrawLine(P, Width - 67, 1, Width - 91, 25) 'diagonal line left corner


        G.DrawLine(P, 56, 25, Width - 56, 25)
        G.DrawLine(P, 33, 1, Width - 67, 1)
        G.DrawLine(P, 3, 0, 31, 0)
        G.DrawLine(P, Width - 4, 0, Width - 67, 0)
        G.DrawLine(P, Width, 25, Width - 55, 25)
        G.DrawLine(P, 0, 25, 55, 25)
        G.DrawLine(P, 0, 3, 0, 24)
        G.DrawLine(P, Width - 1, 3, Width - 1, 24)
        G.DrawLine(P, 3, 0, 0, 3)
        DrawPixel(Color.Black, Width - 2, 2)
        DrawPixel(Color.Black, Width - 3, 1)


        'colors
        Dim GrayLine As New Pen(Color.FromArgb(100, 100, 100))
        G.DrawLine(GrayLine, 34, 2, Width - 69, 2)

        Dim DarkGray As New Pen(Color.FromArgb(60, 60, 60))
        G.DrawLine(DarkGray, 35, 3, Width - 70, 3)
        G.DrawLine(DarkGray, 36, 4, Width - 71, 4)
        G.DrawLine(DarkGray, 37, 5, Width - 72, 5)
        G.DrawLine(DarkGray, 38, 6, Width - 73, 6)
        G.DrawLine(DarkGray, 39, 7, Width - 74, 7)
        G.DrawLine(DarkGray, 40, 8, Width - 75, 8)
        G.DrawLine(DarkGray, 41, 9, Width - 76, 9)
        G.DrawLine(DarkGray, 42, 10, Width - 77, 10)
        G.DrawLine(DarkGray, 43, 11, Width - 78, 11)

        Dim DGray As New Pen(Color.FromArgb(22, 22, 22))
        G.DrawLine(DGray, 44, 12, Width - 79, 12)
        G.DrawLine(DGray, 45, 13, Width - 80, 13)
        G.DrawLine(DGray, 46, 14, Width - 81, 14)
        G.DrawLine(DGray, 47, 15, Width - 82, 15)
        G.DrawLine(DGray, 48, 16, Width - 83, 16)
        G.DrawLine(DGray, 49, 17, Width - 84, 17)
        G.DrawLine(DGray, 50, 18, Width - 85, 18)
        G.DrawLine(DGray, 51, 19, Width - 86, 19)
        G.DrawLine(DGray, 52, 20, Width - 87, 20)
        G.DrawLine(DGray, 53, 21, Width - 88, 21)
        G.DrawLine(DGray, 54, 22, Width - 89, 22)
        G.DrawLine(DGray, 55, 23, Width - 90, 23)
        G.DrawLine(DGray, 56, 24, Width - 91, 24)

        Dim Gray2 As New Pen(Color.FromArgb(70, 70, 70))
        G.DrawLine(Gray2, 3, 1, 31, 1)
        G.DrawLine(Gray2, 31, 2, 32, 2)
        G.DrawLine(Gray2, 32, 3, 33, 3)
        G.DrawLine(Gray2, 33, 4, 34, 4)
        G.DrawLine(Gray2, 34, 5, 35, 5)
        G.DrawLine(Gray2, 35, 6, 36, 6)
        G.DrawLine(Gray2, 36, 7, 37, 7)
        G.DrawLine(Gray2, 37, 8, 38, 8)
        G.DrawLine(Gray2, 38, 9, 39, 9)
        G.DrawLine(Gray2, 39, 10, 40, 10)
        G.DrawLine(Gray2, 2, 2, 4, 2)

        Dim Gray3 As New Pen(Color.FromArgb(41, 41, 41))
        G.DrawLine(Gray3, 40, 11, 41, 11)
        G.DrawLine(Gray3, 41, 12, 42, 12)
        G.DrawLine(Gray3, 42, 13, 43, 13)
        G.DrawLine(Gray3, 43, 14, 44, 14)
        G.DrawLine(Gray3, 44, 15, 45, 15)
        G.DrawLine(Gray3, 45, 16, 46, 16)
        G.DrawLine(Gray3, 46, 17, 47, 17)
        G.DrawLine(Gray3, 47, 18, 48, 18)
        G.DrawLine(Gray3, 48, 19, 49, 19)
        G.DrawLine(Gray3, 49, 20, 50, 20)
        G.DrawLine(Gray3, 50, 21, 51, 21)
        G.DrawLine(Gray3, 51, 22, 52, 22)
        G.DrawLine(Gray3, 52, 23, 53, 23)
        G.DrawLine(Gray3, 53, 24, 54, 24)

        Dim RGB56 As New Pen(Color.FromArgb(56, 56, 56))
        G.DrawLine(RGB56, 4, 2, 30, 2)

        Dim RGB67 As New Pen(Color.FromArgb(67, 67, 67))
        G.DrawLine(RGB67, 1, 3, 2, 3)

        'gradient
        DrawPixel(Color.FromArgb(66, 66, 66), 1, 4)
        DrawPixel(Color.FromArgb(64, 64, 64), 1, 5)
        DrawPixel(Color.FromArgb(62, 62, 62), 1, 6)
        DrawPixel(Color.FromArgb(60, 60, 60), 1, 7)
        DrawPixel(Color.FromArgb(58, 58, 58), 1, 8)
        DrawPixel(Color.FromArgb(57, 57, 57), 1, 9)
        DrawPixel(Color.FromArgb(55, 55, 55), 1, 10)
        DrawPixel(Color.FromArgb(41, 41, 41), 1, 11)
        DrawPixel(Color.FromArgb(39, 39, 39), 1, 12)
        DrawPixel(Color.FromArgb(38, 38, 38), 1, 13)
        DrawPixel(Color.FromArgb(36, 36, 26), 1, 14)
        DrawPixel(Color.FromArgb(35, 35, 35), 1, 15)
        DrawPixel(Color.FromArgb(33, 33, 33), 1, 16)
        DrawPixel(Color.FromArgb(31, 31, 31), 1, 17)
        DrawPixel(Color.FromArgb(29, 29, 29), 1, 18)
        DrawPixel(Color.FromArgb(27, 27, 27), 1, 19)
        DrawPixel(Color.FromArgb(25, 25, 25), 1, 20)
        DrawPixel(Color.FromArgb(24, 24, 24), 1, 21)
        DrawPixel(Color.FromArgb(23, 23, 23), 1, 22)

        Dim RGB21 As New Pen(Color.FromArgb(21, 21, 21))
        G.DrawLine(RGB21, 1, 24, 52, 24)

        Dim RGB55 As New Pen(Color.FromArgb(55, 55, 55))
        G.DrawLine(RGB55, 4, 3, 31, 3)
        DrawPixel(Color.FromArgb(54, 54, 54), 3, 3)

        Dim RGB54 As New Pen(Color.FromArgb(54, 54, 54))
        G.DrawLine(RGB55, 2, 4, 32, 4)

        Dim RGB52 As New Pen(Color.FromArgb(52, 52, 52))
        G.DrawLine(RGB52, 2, 5, 33, 5)

        Dim RGB50 As New Pen(Color.FromArgb(50, 50, 50))
        G.DrawLine(RGB50, 2, 6, 34, 6)

        Dim RGB48 As New Pen(Color.FromArgb(48, 48, 48))
        G.DrawLine(RGB48, 2, 7, 35, 7)

        Dim RGB46 As New Pen(Color.FromArgb(46, 46, 46))
        G.DrawLine(RGB46, 2, 8, 36, 8)

        Dim RGB45 As New Pen(Color.FromArgb(45, 45, 45))
        G.DrawLine(RGB45, 2, 9, 37, 9)

        Dim RGB43 As New Pen(Color.FromArgb(43, 43, 43))
        G.DrawLine(RGB43, 2, 10, 38, 10)

        Dim RGB28 As New Pen(Color.FromArgb(28, 28, 28))
        G.DrawLine(RGB28, 2, 11, 39, 11)

        Dim RGB26 As New Pen(Color.FromArgb(26, 26, 26))
        G.DrawLine(RGB26, 2, 12, 40, 12)

        Dim RGB24 As New Pen(Color.FromArgb(24, 24, 24))
        G.DrawLine(RGB24, 2, 13, 41, 13)

        Dim RGB22 As New Pen(Color.FromArgb(22, 22, 22))
        G.DrawLine(RGB22, 2, 14, 42, 14)

        Dim RGB21v2 As New Pen(Color.FromArgb(21, 21, 21))
        G.DrawLine(RGB21v2, 2, 15, 43, 15)

        Dim RGB19 As New Pen(Color.FromArgb(19, 19, 19))
        G.DrawLine(RGB19, 2, 16, 44, 16)

        Dim RGB17 As New Pen(Color.FromArgb(17, 17, 17))
        G.DrawLine(RGB17, 2, 17, 45, 17)

        Dim RGB15 As New Pen(Color.FromArgb(15, 15, 15))
        G.DrawLine(RGB15, 2, 18, 46, 18)

        Dim RGB13 As New Pen(Color.FromArgb(13, 13, 13))
        G.DrawLine(RGB13, 2, 19, 47, 19)

        Dim RGB11 As New Pen(Color.FromArgb(11, 11, 11))
        G.DrawLine(RGB11, 2, 20, 48, 20)

        Dim RGB10 As New Pen(Color.FromArgb(10, 10, 10))
        G.DrawLine(RGB10, 2, 21, 49, 21)
        G.DrawLine(RGB10, 2, 22, 50, 22)

        Dim RGB8 As New Pen(Color.FromArgb(8, 8, 8))
        G.DrawLine(RGB10, 2, 23, 51, 23)

        Dim Trans As New Pen(Color.FromArgb(255, Color.DeepPink))
        G.DrawLine(Trans, 32, 0, Width - 67, 0)
        G.DrawLine(Trans, Width + 2, 5, Width - 2, 0)
        G.DrawLine(Trans, Width + 1, 5, Width - 1, 0)
        G.DrawLine(Trans, Width + 3, 5, Width - 3, 0)

        G.DrawLine(Trans, 2, 0, 0, 2)
        G.DrawLine(Trans, 1, 0, 0, 1)
        DrawPixel(Color.DeepPink, 0, 0)


        'Borders
        Dim RGB66 As New Pen(Color.FromArgb(66, 66, 66))
        G.DrawLine(RGB66, 1, Height - 2, 1, 26)
        G.DrawLine(RGB66, 2, Height - 2, Width, Height - 2)
        G.DrawLine(RGB66, Width - 2, Height, Width - 2, 26)

        Dim RGBBlack As New Pen(Color.Black)
        G.DrawLine(RGBBlack, 0, Height, 0, 26)
        G.DrawLine(RGBBlack, 1, Height - 1, Width, Height - 1)
        G.DrawLine(RGBBlack, Width - 1, Height, Width - 1, 26)

        G.DrawLine(RGBBlack, Width - 6, Height - 3, Width - 6, 26)
        G.DrawLine(RGBBlack, 5, Height - 3, 5, 26)
        G.DrawLine(RGBBlack, 2, Height - 6, Width - 3, Height - 6)

        Dim RGB45v2 As New Pen(Color.FromArgb(45, 45, 45))
        G.DrawLine(RGB45v2, 2, Height - 3, 2, 26)
        G.DrawLine(RGB45v2, 3, Height - 3, 3, 26)
        G.DrawLine(RGB45v2, 4, Height - 3, 4, 26)

        G.DrawLine(RGB45v2, 2, Height - 3, Width - 3, Height - 3)
        G.DrawLine(RGB45v2, 2, Height - 4, Width - 3, Height - 4)
        G.DrawLine(RGB45v2, 2, Height - 5, Width - 3, Height - 5)

        G.DrawLine(RGB45v2, Width - 3, Height - 3, Width - 3, 26)
        G.DrawLine(RGB45v2, Width - 4, Height - 3, Width - 4, 26)
        G.DrawLine(RGB45v2, Width - 5, Height - 3, Width - 5, 26)

        'Right Top corner
        Dim RGB68 As New Pen(Color.FromArgb(68, 68, 68))
        G.DrawLine(RGB68, Width - 66, 2, Width - 67, 2)

        G.DrawLine(RGB67, Width - 67, 3, Width - 68, 3)

        G.DrawLine(RGB66, Width - 68, 4, Width - 69, 4)

        Dim RGB64 As New Pen(Color.FromArgb(64, 64, 64))
        G.DrawLine(RGB64, Width - 69, 5, Width - 70, 5)

        Dim RGB62 As New Pen(Color.FromArgb(62, 62, 62))
        G.DrawLine(RGB62, Width - 70, 6, Width - 71, 6)

        Dim RGB60 As New Pen(Color.FromArgb(60, 60, 60))
        G.DrawLine(RGB60, Width - 71, 7, Width - 72, 7)

        Dim RGB58 As New Pen(Color.FromArgb(58, 58, 58))
        G.DrawLine(RGB58, Width - 72, 8, Width - 73, 8)

        Dim RGB57 As New Pen(Color.FromArgb(57, 57, 57))
        G.DrawLine(RGB57, Width - 73, 9, Width - 74, 9)

        G.DrawLine(RGB55, Width - 74, 10, Width - 75, 10)

        Dim RGB41 As New Pen(Color.FromArgb(41, 41, 41))
        G.DrawLine(RGB41, Width - 75, 11, Width - 76, 11)

        Dim RGB39 As New Pen(Color.FromArgb(39, 39, 39))
        G.DrawLine(RGB39, Width - 76, 12, Width - 77, 12)

        Dim RGB38 As New Pen(Color.FromArgb(38, 38, 38))
        G.DrawLine(RGB38, Width - 77, 13, Width - 78, 13)

        Dim RGB36 As New Pen(Color.FromArgb(36, 36, 36))
        G.DrawLine(RGB36, Width - 78, 14, Width - 79, 14)

        Dim RGB35 As New Pen(Color.FromArgb(35, 35, 35))
        G.DrawLine(RGB35, Width - 79, 15, Width - 80, 15)

        Dim RGB33 As New Pen(Color.FromArgb(33, 33, 33))
        G.DrawLine(RGB33, Width - 80, 16, Width - 81, 16)

        Dim RGB31 As New Pen(Color.FromArgb(31, 31, 31))
        G.DrawLine(RGB36, Width - 81, 17, Width - 82, 17)

        Dim RGB29 As New Pen(Color.FromArgb(29, 29, 29))
        G.DrawLine(RGB29, Width - 82, 18, Width - 83, 18)

        Dim RGB27 As New Pen(Color.FromArgb(27, 27, 27))
        G.DrawLine(RGB27, Width - 83, 19, Width - 84, 19)

        Dim RGB25 As New Pen(Color.FromArgb(25, 25, 25))
        G.DrawLine(RGB25, Width - 84, 20, Width - 85, 20)

        G.DrawLine(RGB24, Width - 85, 21, Width - 86, 21)

        Dim RGB23 As New Pen(Color.FromArgb(23, 23, 23))
        G.DrawLine(RGB23, Width - 86, 22, Width - 87, 22)

        G.DrawLine(RGB8, Width - 86, 23, Width - 3, 23)

        Dim RGB21v3 As New Pen(Color.FromArgb(21, 21, 21))
        G.DrawLine(RGB21v3, Width - 88, 24, Width - 3, 24)

        Dim RGB70 As New Pen(Color.FromArgb(70, 70, 70))
        G.DrawLine(RGB70, Width - 4, 1, Width - 66, 1) '<------ top light gray line

        G.DrawLine(RGB68, Width - 3, 2, Width - 4, 2)
        G.DrawLine(RGB67, Width - 2, 3, Width - 3, 3)

        'Pixels for right top corner

        DrawPixel(Color.FromArgb(66, 66, 66), Width - 2, 4)
        DrawPixel(Color.FromArgb(64, 64, 64), Width - 2, 5)
        DrawPixel(Color.FromArgb(62, 62, 62), Width - 2, 6)
        DrawPixel(Color.FromArgb(60, 60, 60), Width - 2, 7)
        DrawPixel(Color.FromArgb(58, 58, 58), Width - 2, 8)
        DrawPixel(Color.FromArgb(57, 57, 57), Width - 2, 9)
        DrawPixel(Color.FromArgb(41, 41, 41), Width - 2, 10)
        DrawPixel(Color.FromArgb(39, 39, 39), Width - 2, 11)
        DrawPixel(Color.FromArgb(38, 38, 38), Width - 2, 12)
        DrawPixel(Color.FromArgb(36, 36, 36), Width - 2, 13)
        DrawPixel(Color.FromArgb(35, 35, 35), Width - 2, 14)
        DrawPixel(Color.FromArgb(33, 33, 33), Width - 2, 15)
        DrawPixel(Color.FromArgb(31, 31, 31), Width - 2, 16)
        DrawPixel(Color.FromArgb(29, 29, 29), Width - 2, 17)
        DrawPixel(Color.FromArgb(27, 27, 27), Width - 2, 18)
        DrawPixel(Color.FromArgb(25, 25, 25), Width - 2, 19)
        DrawPixel(Color.FromArgb(25, 25, 25), Width - 2, 19)
        DrawPixel(Color.FromArgb(24, 24, 24), Width - 2, 20)
        DrawPixel(Color.FromArgb(23, 23, 23), Width - 2, 21)
        DrawPixel(Color.FromArgb(23, 23, 23), Width - 2, 22)

        'gradient for top right corner

        G.DrawLine(RGB56, Width - 65, 2, Width - 5, 2)
        G.DrawLine(RGB55, Width - 66, 3, Width - 4, 3)
        G.DrawLine(RGB54, Width - 67, 4, Width - 3, 4)
        G.DrawLine(RGB52, Width - 68, 5, Width - 3, 5)
        G.DrawLine(RGB50, Width - 69, 6, Width - 3, 6)
        G.DrawLine(RGB48, Width - 70, 7, Width - 3, 7)
        G.DrawLine(RGB46, Width - 71, 8, Width - 3, 8)
        G.DrawLine(RGB45, Width - 72, 9, Width - 3, 9)
        G.DrawLine(RGB43, Width - 73, 10, Width - 3, 10)
        G.DrawLine(RGB28, Width - 74, 11, Width - 3, 11)
        G.DrawLine(RGB26, Width - 75, 12, Width - 3, 12)
        G.DrawLine(RGB24, Width - 76, 13, Width - 3, 13)
        G.DrawLine(RGB22, Width - 77, 14, Width - 3, 14)
        G.DrawLine(RGB21, Width - 78, 15, Width - 3, 15)
        G.DrawLine(RGB19, Width - 79, 16, Width - 3, 16)
        G.DrawLine(RGB17, Width - 80, 17, Width - 3, 17)
        G.DrawLine(RGB15, Width - 81, 18, Width - 3, 18)
        G.DrawLine(RGB13, Width - 82, 19, Width - 3, 19)
        G.DrawLine(RGB11, Width - 83, 20, Width - 3, 20)
        G.DrawLine(RGB10, Width - 84, 21, Width - 3, 21)
        G.DrawLine(RGB10, Width - 85, 22, Width - 3, 22)

        DrawText(New Drawing.SolidBrush(Color.FromArgb(255, TitleForeColourVal)), HorizontalAlignment.Center, -19, -1, Me.Text)
    End Sub
#End Region

End Class





Class SpaceConsole
    Inherits ThemeControl153

#Region "Console"

    Private WithEvents Textbx As New RichTextBox
    Sub New()
        Textbx.BorderStyle = BorderStyle.None
        Textbx.Location = New Point(4, 3)
        Controls.Add(Textbx)
        ForeColourVal = Color.White
        Textbx.ForeColor = ForeColourVal
        Textbx.ScrollBars = ScrollBarsVal
        Textbx.MaxLength = 2147483647
    End Sub

    Private ForeColourVal As Color
    Private ScrollBarsVal As ScrollBar = 0

    <System.ComponentModel.Description("Change the font colour of the text.")> _
    Public Property ForeColour() As Color
        Get
            Return ForeColourVal
        End Get
        Set(ByVal ForeColour As Color)
            ForeColourVal = ForeColour
            Textbx.ForeColor = ForeColourVal
            Invalidate()
        End Set
    End Property

    <System.ComponentModel.Description("Change what type of scrolling function the textbox is allowed.")> _
    Public Property ScrollBars() As ScrollBar
        Get
            Return ScrollBarsVal
        End Get
        Set(ByVal ScrollBars As ScrollBar)
            ScrollBarsVal = ScrollBars
            Invalidate()
        End Set
    End Property

    Protected Overrides Sub ColorHook()

    End Sub
    Property Read_Only As Boolean = False

    Enum ScrollBar As Byte
        None = 0
        Vertical = 1
        Horizontal = 2
        Both = 3
    End Enum

    Protected Overrides Sub PaintHook()
        G.Clear(Color.FromArgb(14, 14, 14))
        Textbx.BackColor = Color.FromArgb(14, 14, 14)

        Dim RGB28 As New Pen(Color.FromArgb(28, 28, 28))
        G.DrawRectangle(RGB28, 0, 0, Width - 1, Height - 1)

        Dim RGB3 As New Pen(Color.FromArgb(3, 3, 3))
        G.DrawRectangle(RGB3, 1, 1, Width - 3, Height - 3)

        Textbx.Size = New Size(Me.Width - 8, Textbx.Height - 4)
        Textbx.ReadOnly = Read_Only

        Textbx.Height = Me.Height - 6
        Textbx.Width = Me.Width - 7
        LockHeight = 0


        ScrollBarsState()

    End Sub

    Sub GetFoc() Handles Textbx.GotFocus

    End Sub

    Sub LostFoc() Handles Textbx.LostFocus

    End Sub

    Sub ScrollBarsState()
        If ScrollBarsVal = "1" Then
            Textbx.ScrollBars = Windows.Forms.ScrollBars.Vertical
            Textbx.Location = New Point(4, 3)
        Else
            If ScrollBarsVal = "2" Then
                Textbx.ScrollBars = Windows.Forms.ScrollBars.Horizontal
                Textbx.Location = New Point(4, 3)
            Else
                If ScrollBarsVal = "3" Then
                    Textbx.ScrollBars = Windows.Forms.ScrollBars.Both
                    Textbx.Location = New Point(4, 3)
                Else
                    'If ScrollBarsVal = "0" Then
                    Textbx.ScrollBars = Windows.Forms.ScrollBars.None
                    Textbx.Location = New Point(4, 3)
                End If
            End If
        End If
    End Sub

    Sub TextBox_TextChanged() Handles Textbx.TextChanged
        Text = Textbx.Text
    End Sub

    Sub PropertyTextChanged() Handles MyBase.TextChanged
        Textbx.Text = Text
    End Sub

#End Region

End Class



Class SpaceButton
    Inherits ThemeControl153

#Region "Button"

    Sub New()
        Size = New Point(88, 23)
        ForeColourVal = Color.White
    End Sub

    Private ForeColourVal As Color

    <System.ComponentModel.Description("Change the font colour of the text.")> _
    Public Property ForeColour() As Color
        Get
            Return ForeColourVal
        End Get
        Set(ByVal ForeColour As Color)
            ForeColourVal = ForeColour
            Invalidate()
        End Set
    End Property

    Protected Overrides Sub ColorHook()

    End Sub

    Protected Overrides Sub PaintHook()
        G.FillRectangle(New SolidBrush(Color.FromArgb(75, 75, 75)), 0, 0, Width, Height)

        DrawBorders(Pens.Black, 0)
        DrawCorners(Color.FromArgb(24, 24, 24), ClientRectangle)

        Dim RGB56 As New Pen(Color.FromArgb(56, 56, 56))
        G.DrawLine(RGB56, 2, 1, Width - 2, 1)
        DrawPixel(Color.FromArgb(35, 35, 35), 1, 1)
        DrawPixel(Color.FromArgb(35, 35, 35), Width - 2, 1)


        DrawGradient(Color.FromArgb(50, 50, 50), Color.FromArgb(26, 26, 26), 1, 1, 1, Height - 2)
        DrawGradient(Color.FromArgb(50, 50, 50), Color.FromArgb(26, 26, 26), Width - 2, 1, 1, Height - 3)

        Dim RGB21 As New Pen(Color.FromArgb(21, 21, 21))
        G.DrawLine(RGB21, 1, Height - 2, Width - 2, Height - 2)

        DrawGradient(Color.FromArgb(38, 38, 38), Color.FromArgb(14, 14, 14), 2, 2, Width - 4, Height - 4)

        If Me.Enabled = True Then
            DrawText(New Drawing.SolidBrush(Color.FromArgb(255, ForeColourVal)), HorizontalAlignment.Center, -1, 0)
        Else
            DrawText(New Drawing.SolidBrush(Color.Red), HorizontalAlignment.Center, -1, 0)
        End If

        If State = MouseState.Over Then
            Dim RGB91 As New Pen(Color.FromArgb(91, 91, 91))
            DrawGradient(Color.FromArgb(145, 145, 145), Color.FromArgb(145, 145, 145), 1, 2, 1, Height \ 2)
            G.DrawLine(RGB91, 1, Height \ 2, 1, Height - 3)

            DrawGradient(Color.FromArgb(145, 145, 145), Color.FromArgb(145, 145, 145), Width - 2, 2, 1, Height \ 2)
            G.DrawLine(RGB91, Width - 2, Height \ 2, Width - 2, Height - 3)

            G.DrawLine(RGB91, 2, Height - 2, Width - 3, Height - 2)

            Dim RGB145 As New Pen(Color.FromArgb(145, 145, 145))
            G.DrawLine(RGB145, 2, 1, Width - 3, 1)
        ElseIf State = MouseState.Down Then



            DrawGradient(Color.FromArgb(14, 14, 14), Color.FromArgb(38, 38, 38), 2, 2, Width - 4, Height - 4)
            If Me.Enabled = True Then
                DrawText(New Drawing.SolidBrush(Color.FromArgb(255, ForeColourVal)), HorizontalAlignment.Center, -2, 0)
            Else
                DrawText(New Drawing.SolidBrush(Color.Red), HorizontalAlignment.Center, -2, 0)
            End If

        Else
            'normal
        End If



        If State = MouseState.Down Then
            'when down
        Else
            'when up text related
        End If

    End Sub
#End Region

End Class





Class SpaceButtonGloss
    Inherits ThemeControl153

#Region "GlossButton"

    Sub New()
        Size = New Point(88, 23)
    End Sub

    Property BaseColor As Color = Color.FromArgb(0, 0, 0) 'Black
    Property HighlightColor As Color = Color.FromArgb(255, 100, 0) 'Orange
    Property ClickedColor As Color = Color.FromArgb(180, 50, 0) 'Dark Orange

    Protected Overrides Sub ColorHook()

    End Sub

    Protected Overrides Sub PaintHook()
        G.FillRectangle(New SolidBrush(BaseColor), 0, 0, Width, Height)

        Dim RGB29 As New Pen(Color.FromArgb(29, 29, 29))
        DrawBorders(RGB29, 0)

        DrawGradient(Color.FromArgb(124, 255, 255, 255), Color.FromArgb(0, 0, 0, 0), 1, 1, Me.Width, Me.Height / 1.8)
        Dim White As New Pen(Color.FromArgb(124, 255, 255, 255))

        Dim path As New GraphicsPath()
        Dim HalfHeight As Integer = Me.Height / 2.2
        Dim HalfWidth As Integer = ((Me.Width * 1.2) - Me.Width) * -1
        Dim EndWidth As Integer = Me.Width * 1.4
        path.AddEllipse(HalfWidth, HalfHeight, EndWidth, Me.Height * 2)
        Dim pthGrBrush As New PathGradientBrush(path)
        pthGrBrush.CenterColor = Color.FromArgb(144, 255, 255, 255)
        Dim colors As Color() = {Color.Transparent}
        pthGrBrush.SurroundColors = colors
        G.FillEllipse(pthGrBrush, HalfWidth, HalfHeight, EndWidth, Me.Height * 2)

        DrawCorners(Color.FromArgb(255, 24, 24, 24), ClientRectangle)

        If State = MouseState.Over Then
            G.Clear(Color.Black)
            G.FillRectangle(New SolidBrush(HighlightColor), 0, 0, Width, Height)

            DrawBorders(RGB29, 0)

            DrawGradient(Color.FromArgb(124, 255, 255, 255), Color.FromArgb(0, 0, 0, 0), 1, 1, Me.Width, Me.Height / 1.8)

            path.AddEllipse(HalfWidth, HalfHeight, EndWidth, Me.Height * 2)
            pthGrBrush.CenterColor = Color.FromArgb(144, 255, 255, 255)
            pthGrBrush.SurroundColors = colors
            G.FillEllipse(pthGrBrush, HalfWidth, HalfHeight, EndWidth, Me.Height * 2)

            DrawCorners(Color.FromArgb(255, 24, 24, 24), ClientRectangle)

        ElseIf State = MouseState.Down Then
            G.Clear(Color.Black)
            G.FillRectangle(New SolidBrush(ClickedColor), 0, 0, Width, Height)

            DrawBorders(RGB29, 0)

            DrawGradient(Color.FromArgb(124, 255, 255, 255), Color.FromArgb(0, 0, 0, 0), 1, 1, Me.Width, Me.Height / 1.8)

            path.AddEllipse(HalfWidth, HalfHeight, EndWidth, Me.Height * 2)
            pthGrBrush.CenterColor = Color.FromArgb(144, 255, 255, 255)
            pthGrBrush.SurroundColors = colors
            G.FillEllipse(pthGrBrush, HalfWidth, HalfHeight, EndWidth, Me.Height * 2)

            DrawCorners(Color.FromArgb(255, 24, 24, 24), ClientRectangle)

            DrawText(Brushes.White, HorizontalAlignment.Center, 0, 0)
        Else
            'normal
        End If



        If State = MouseState.Down Then
            'when down
        Else
            'when up text related
        End If

        DrawText(Brushes.White, HorizontalAlignment.Center, 0, 0)
    End Sub
#End Region

End Class





Class SpaceButton_ControlButton
    Inherits ThemeControl153

#Region "Control Button"

    Sub New()
        Size = New Point(19, 19)
        ForeColourVal = Color.White
    End Sub

    Private ForeColourVal As Color
    Private IconVal As Image
    Private ShowIconAlwaysVal As Boolean

    <System.ComponentModel.Description("Change the font colour of the text.")> _
    Public Property ForeColour() As Color
        Get
            Return ForeColourVal
        End Get
        Set(ByVal ForeColour As Color)
            ForeColourVal = ForeColour
            Invalidate()
        End Set
    End Property

    <System.ComponentModel.Description("Changes the image of the icon.")> _
    Public Property Icon As Image
        Get
            Return IconVal
        End Get
        Set(ByVal Icon As Image)
            IconVal = Icon
            Invalidate()
        End Set
    End Property

    <System.ComponentModel.Description("Changes the image of the icon.")> _
    Public Property ShowIconAlways As Boolean
        Get
            Return ShowIconAlwaysVal
        End Get
        Set(ByVal ShowIconAlways As Boolean)
            ShowIconAlwaysVal = ShowIconAlways
            Invalidate()
        End Set
    End Property

    Protected Overrides Sub ColorHook()

    End Sub

    Protected Overrides Sub PaintHook()
        Size = New Point(19, 19)

        G.FillRectangle(New SolidBrush(Color.FromArgb(75, 75, 75)), 0, 0, Width, Height)

        DrawBorders(Pens.Black, 0)

        Dim RGB56 As New Pen(Color.FromArgb(56, 56, 56))
        G.DrawLine(RGB56, 2, 1, Width - 2, 1)
        DrawPixel(Color.FromArgb(35, 35, 35), 1, 1)
        DrawPixel(Color.FromArgb(35, 35, 35), Width - 2, 1)


        DrawGradient(Color.FromArgb(50, 50, 50), Color.FromArgb(26, 26, 26), 1, 1, 1, Height - 2)
        DrawGradient(Color.FromArgb(50, 50, 50), Color.FromArgb(26, 26, 26), Width - 2, 1, 1, Height - 3)

        Dim RGB21 As New Pen(Color.FromArgb(21, 21, 21))
        G.DrawLine(RGB21, 1, Height - 2, Width - 2, Height - 2)

        DrawGradient(Color.FromArgb(38, 38, 38), Color.FromArgb(14, 14, 14), 2, 2, 15, 15)

        If State = MouseState.Over Then

            DrawGradient(Color.FromArgb(145, 145, 145), Color.FromArgb(145, 145, 145), 1, 1, 1, Height \ 2)
            DrawGradient(Color.FromArgb(91, 91, 91), Color.FromArgb(91, 91, 91), 1, Height \ 2, 1, Height - 10)

            DrawGradient(Color.FromArgb(145, 145, 145), Color.FromArgb(145, 145, 145), Width - 2, 1, 1, Height \ 2)
            DrawGradient(Color.FromArgb(91, 91, 91), Color.FromArgb(91, 91, 91), Width - 2, Height \ 2, 1, Height - 10)

            Dim RGB91 As New Pen(Color.FromArgb(91, 91, 91))
            G.DrawLine(RGB91, 2, Height - 2, Width - 3, Height - 2)

            Dim RGB145 As New Pen(Color.FromArgb(145, 145, 145))
            G.DrawLine(RGB145, 2, 1, Width - 3, 1)

            Dim RGB255 As New Pen(Color.FromArgb(255, 255, 255))
            If ShowIconAlways = False Then
                G.DrawImage(Icon, 2, 2, 16, 16)
            End If


        ElseIf State = MouseState.Down Then

            DrawGradient(Color.FromArgb(14, 14, 14), Color.FromArgb(38, 38, 38), 2, 2, Width - 4, Height - 4)
            Dim RGB255 As New Pen(Color.FromArgb(255, 255, 255))
            If ShowIconAlways = False Then
                G.DrawImage(Icon, 2, 2, 16, 16)
            End If

        Else
            'normal
        End If

        If State = MouseState.Down Then
            'when down
        Else
            'when up text related
        End If

        If ShowIconAlways = True Then
            G.DrawImage(Icon, 2, 2, 16, 16)
        End If

    End Sub
#End Region

End Class





Class SpaceButton_Close
    Inherits ThemeControl153

#Region "Close"

    Sub New()
        Size = New Point(19, 19)
        ForeColourVal = Color.White
    End Sub

    Private ForeColourVal As Color

    <System.ComponentModel.Description("Change the font colour of the text.")> _
    Public Property ForeColour() As Color
        Get
            Return ForeColourVal
        End Get
        Set(ByVal ForeColour As Color)
            ForeColourVal = ForeColour
            Invalidate()
        End Set
    End Property

    Protected Overrides Sub ColorHook()

    End Sub

    Protected Overrides Sub PaintHook()
        Size = New Point(19, 19)

        G.FillRectangle(New SolidBrush(Color.FromArgb(75, 75, 75)), 0, 0, Width, Height)

        DrawBorders(Pens.Black, 0)

        Dim RGB56 As New Pen(Color.FromArgb(56, 56, 56))
        G.DrawLine(RGB56, 2, 1, Width - 2, 1)
        DrawPixel(Color.FromArgb(35, 35, 35), 1, 1)
        DrawPixel(Color.FromArgb(35, 35, 35), Width - 2, 1)


        DrawGradient(Color.FromArgb(50, 50, 50), Color.FromArgb(26, 26, 26), 1, 1, 1, Height - 2)
        DrawGradient(Color.FromArgb(50, 50, 50), Color.FromArgb(26, 26, 26), Width - 2, 1, 1, Height - 3)

        Dim RGB21 As New Pen(Color.FromArgb(21, 21, 21))
        G.DrawLine(RGB21, 1, Height - 2, Width - 2, Height - 2)

        DrawGradient(Color.FromArgb(38, 38, 38), Color.FromArgb(14, 14, 14), 2, 2, 15, 15)

        Dim RGB100 As New Pen(Color.FromArgb(60, 60, 60))
        G.DrawLine(RGB100, 5, 5, Width - 6, Height - 6)
        G.DrawLine(RGB100, 5, 6, Width - 7, Height - 6)
        G.DrawLine(RGB100, 6, 5, Width - 6, Height - 7)

        G.DrawLine(RGB100, Width - 6, 5, 5, Height - 6)
        G.DrawLine(RGB100, Width - 6, 6, 6, Height - 6)
        G.DrawLine(RGB100, Width - 7, 5, 5, Height - 7)

        If State = MouseState.Over Then

            DrawGradient(Color.FromArgb(145, 145, 145), Color.FromArgb(145, 145, 145), 1, 1, 1, Height \ 2)
            DrawGradient(Color.FromArgb(91, 91, 91), Color.FromArgb(91, 91, 91), 1, Height \ 2, 1, Height - 10)

            DrawGradient(Color.FromArgb(145, 145, 145), Color.FromArgb(145, 145, 145), Width - 2, 1, 1, Height \ 2)
            DrawGradient(Color.FromArgb(91, 91, 91), Color.FromArgb(91, 91, 91), Width - 2, Height \ 2, 1, Height - 10)

            Dim RGB91 As New Pen(Color.FromArgb(91, 91, 91))
            G.DrawLine(RGB91, 2, Height - 2, Width - 3, Height - 2)

            Dim RGB145 As New Pen(Color.FromArgb(145, 145, 145))
            G.DrawLine(RGB145, 2, 1, Width - 3, 1)

            Dim RGB255 As New Pen(Color.FromArgb(255, 255, 255))
            G.DrawLine(RGB255, 5, 5, Width - 6, Height - 6)
            G.DrawLine(RGB255, 5, 6, Width - 7, Height - 6)
            G.DrawLine(RGB255, 6, 5, Width - 6, Height - 7)

            G.DrawLine(RGB255, Width - 6, 5, 5, Height - 6)
            G.DrawLine(RGB255, Width - 6, 6, 6, Height - 6)
            G.DrawLine(RGB255, Width - 7, 5, 5, Height - 7)

        ElseIf State = MouseState.Down Then

            DrawGradient(Color.FromArgb(14, 14, 14), Color.FromArgb(38, 38, 38), 2, 2, Width - 4, Height - 4)
            Dim RGB255 As New Pen(Color.FromArgb(255, 255, 255))
            G.DrawLine(RGB255, 5, 5, Width - 6, Height - 6)
            G.DrawLine(RGB255, 5, 6, Width - 7, Height - 6)
            G.DrawLine(RGB255, 6, 5, Width - 6, Height - 7)

            G.DrawLine(RGB255, Width - 6, 5, 5, Height - 6)
            G.DrawLine(RGB255, Width - 6, 6, 6, Height - 6)
            G.DrawLine(RGB255, Width - 7, 5, 5, Height - 7)

        Else
            'normal
        End If

        If State = MouseState.Down Then
            'when down
        Else
            'when up text related
        End If

    End Sub
#End Region

End Class





Class SpaceButton_Maximize
    Inherits ThemeControl153

#Region "Maximize"

    Sub New()
        Size = New Point(19, 19)
        ForeColourVal = Color.White
    End Sub

    Private ForeColourVal As Color

    <System.ComponentModel.Description("Change the font colour of the text.")> _
    Public Property ForeColour() As Color
        Get
            Return ForeColourVal
        End Get
        Set(ByVal ForeColour As Color)
            ForeColourVal = ForeColour
            Invalidate()
        End Set
    End Property

    Protected Overrides Sub ColorHook()

    End Sub

    Protected Overrides Sub PaintHook()
        Size = New Point(19, 19)

        G.FillRectangle(New SolidBrush(Color.FromArgb(75, 75, 75)), 0, 0, Width, Height)

        DrawBorders(Pens.Black, 0)

        Dim RGB56 As New Pen(Color.FromArgb(56, 56, 56))
        G.DrawLine(RGB56, 2, 1, Width - 2, 1)
        DrawPixel(Color.FromArgb(35, 35, 35), 1, 1)
        DrawPixel(Color.FromArgb(35, 35, 35), Width - 2, 1)


        DrawGradient(Color.FromArgb(50, 50, 50), Color.FromArgb(26, 26, 26), 1, 1, 1, Height - 2)
        DrawGradient(Color.FromArgb(50, 50, 50), Color.FromArgb(26, 26, 26), Width - 2, 1, 1, Height - 3)

        Dim RGB21 As New Pen(Color.FromArgb(21, 21, 21))
        G.DrawLine(RGB21, 1, Height - 2, Width - 2, Height - 2)

        DrawGradient(Color.FromArgb(38, 38, 38), Color.FromArgb(14, 14, 14), 2, 2, 15, 15)

        Dim RGB255 As New Pen(Color.FromArgb(255, 255, 255))

        Dim RGB60 As New Pen(Color.FromArgb(60, 60, 60))

        G.DrawLine(RGB60, 4, 4, Width - 5, 4) '<--- Line For Icon
        G.DrawLine(RGB60, 4, 5, Width - 5, 5) '<--- Line For Icon
        G.DrawLine(RGB60, 4, 6, Width - 5, 6) '<--- Line For Icon

        G.DrawLine(RGB60, 4, 5, 4, Height - 5) '<--- Line For Icon
        G.DrawLine(RGB60, 4, Height - 5, Width - 5, Height - 5) '<--- Line For Icon
        G.DrawLine(RGB60, Width - 5, 5, Width - 5, Height - 5) '<--- Line For Icon

        If State = MouseState.Over Then

            DrawGradient(Color.FromArgb(145, 145, 145), Color.FromArgb(145, 145, 145), 1, 1, 1, Height \ 2)
            DrawGradient(Color.FromArgb(91, 91, 91), Color.FromArgb(91, 91, 91), 1, Height \ 2, 1, Height - 10)

            DrawGradient(Color.FromArgb(145, 145, 145), Color.FromArgb(145, 145, 145), Width - 2, 1, 1, Height \ 2)
            DrawGradient(Color.FromArgb(91, 91, 91), Color.FromArgb(91, 91, 91), Width - 2, Height \ 2, 1, Height - 10)

            Dim RGB91 As New Pen(Color.FromArgb(91, 91, 91))
            G.DrawLine(RGB91, 2, Height - 2, Width - 3, Height - 2)

            Dim RGB145 As New Pen(Color.FromArgb(145, 145, 145))
            G.DrawLine(RGB145, 2, 1, Width - 3, 1)

            G.DrawLine(RGB255, 4, 4, Width - 5, 4) '<--- Line For Icon
            G.DrawLine(RGB255, 4, 5, Width - 5, 5) '<--- Line For Icon
            G.DrawLine(RGB255, 4, 6, Width - 5, 6) '<--- Line For Icon

            G.DrawLine(RGB255, 4, 5, 4, Height - 5) '<--- Line For Icon
            G.DrawLine(RGB255, 4, Height - 5, Width - 5, Height - 5) '<--- Line For Icon
            G.DrawLine(RGB255, Width - 5, 5, Width - 5, Height - 5) '<--- Line For Icon

        ElseIf State = MouseState.Down Then

            DrawGradient(Color.FromArgb(14, 14, 14), Color.FromArgb(38, 38, 38), 2, 2, Width - 4, Height - 4)

            G.DrawLine(RGB255, 4, 4, Width - 5, 4) '<--- Line For Icon
            G.DrawLine(RGB255, 4, 5, Width - 5, 5) '<--- Line For Icon
            G.DrawLine(RGB255, 4, 6, Width - 5, 6) '<--- Line For Icon

            G.DrawLine(RGB255, 4, 5, 4, Height - 5) '<--- Line For Icon
            G.DrawLine(RGB255, 4, Height - 5, Width - 5, Height - 5) '<--- Line For Icon
            G.DrawLine(RGB255, Width - 5, 5, Width - 5, Height - 5) '<--- Line For Icon

        Else
            'normal
        End If



        If State = MouseState.Down Then
            'when down
        Else
            'when up text related
        End If

    End Sub
#End Region

End Class





Class SpaceButton_Minimize
    Inherits ThemeControl153

#Region "Minimize"

    Sub New()
        Size = New Point(19, 19)
        ForeColourVal = Color.White
    End Sub

    Private ForeColourVal As Color

    <System.ComponentModel.Description("Change the font colour of the text.")> _
    Public Property ForeColour() As Color
        Get
            Return ForeColourVal
        End Get
        Set(ByVal ForeColour As Color)
            ForeColourVal = ForeColour
            Invalidate()
        End Set
    End Property

    Protected Overrides Sub ColorHook()

    End Sub

    Protected Overrides Sub PaintHook()
        Size = New Point(19, 19)

        G.FillRectangle(New SolidBrush(Color.FromArgb(75, 75, 75)), 0, 0, Width, Height)

        DrawBorders(Pens.Black, 0)

        Dim RGB56 As New Pen(Color.FromArgb(56, 56, 56))
        G.DrawLine(RGB56, 2, 1, Width - 2, 1)
        DrawPixel(Color.FromArgb(35, 35, 35), 1, 1)
        DrawPixel(Color.FromArgb(35, 35, 35), Width - 2, 1)


        DrawGradient(Color.FromArgb(50, 50, 50), Color.FromArgb(26, 26, 26), 1, 1, 1, Height - 2)
        DrawGradient(Color.FromArgb(50, 50, 50), Color.FromArgb(26, 26, 26), Width - 2, 1, 1, Height - 3)

        Dim RGB21 As New Pen(Color.FromArgb(21, 21, 21))
        G.DrawLine(RGB21, 1, Height - 2, Width - 2, Height - 2)

        DrawGradient(Color.FromArgb(38, 38, 38), Color.FromArgb(14, 14, 14), 2, 2, 15, 15)

        Dim RGB255 As New Pen(Color.FromArgb(255, 255, 255))

        Dim RGB60 As New Pen(Color.FromArgb(60, 60, 60))

        G.DrawLine(RGB60, 4, Height - 5, Width - 5, Height - 5) '<--- Line For Icon
        G.DrawLine(RGB60, 4, Height - 6, Width - 5, Height - 6)

        If State = MouseState.Over Then

            DrawGradient(Color.FromArgb(145, 145, 145), Color.FromArgb(145, 145, 145), 1, 1, 1, Height \ 2)
            DrawGradient(Color.FromArgb(91, 91, 91), Color.FromArgb(91, 91, 91), 1, Height \ 2, 1, Height - 10)

            DrawGradient(Color.FromArgb(145, 145, 145), Color.FromArgb(145, 145, 145), Width - 2, 1, 1, Height \ 2)
            DrawGradient(Color.FromArgb(91, 91, 91), Color.FromArgb(91, 91, 91), Width - 2, Height \ 2, 1, Height - 10)

            Dim RGB91 As New Pen(Color.FromArgb(91, 91, 91))
            G.DrawLine(RGB91, 2, Height - 2, Width - 3, Height - 2)

            Dim RGB145 As New Pen(Color.FromArgb(145, 145, 145))
            G.DrawLine(RGB145, 2, 1, Width - 3, 1)

            G.DrawLine(RGB255, 4, Height - 5, Width - 5, Height - 5) '<--- Line For Icon
            G.DrawLine(RGB255, 4, Height - 6, Width - 5, Height - 6)

        ElseIf State = MouseState.Down Then

            DrawGradient(Color.FromArgb(14, 14, 14), Color.FromArgb(38, 38, 38), 2, 2, Width - 4, Height - 4)

            G.DrawLine(RGB255, 4, Height - 5, Width - 5, Height - 5)
            G.DrawLine(RGB255, 4, Height - 6, Width - 5, Height - 6)

        Else
            'normal
        End If



        If State = MouseState.Down Then
            'when down
        Else
            'when up text related
        End If

    End Sub
#End Region

End Class





Class SpaceProgressBar
    Inherits ThemeControl153

#Region "ProgressBar"
    Private Blend As ColorBlend


    Sub New()
        Blend = New ColorBlend
        Blend.Colors = New Color() {Color.FromArgb(39, 39, 39), Color.FromArgb(64, 64, 64), Color.FromArgb(64, 64, 64), Color.FromArgb(39, 39, 39)}
        Blend.Positions = New Single() {0.0F, 0.4F, 0.6F, 1.0F}
        Me.Text = Progress & "%"
    End Sub

    Protected Overrides Sub OnCreation()
        If Not DesignMode Then
            Dim T As New Threading.Thread(AddressOf MoveGlow)
            T.IsBackground = True
            T.Start()
            Text = "0%"
        End If
    End Sub

    Private GlowPosition As Single = -1.0F
    Private Sub MoveGlow()
        While True
            GlowPosition += 0.01F
            If GlowPosition >= 1.0F Then GlowPosition = -1.0F
            Invalidate()
            Threading.Thread.Sleep(25)
        End While
    End Sub

    Private _Value As Integer
    Property Value() As Integer
        Get
            Return _Value
        End Get
        Set(ByVal value As Integer)
            If value > _Maximum Then value = _Maximum
            If value < 0 Then value = 0

            _Value = value
            Text = value & "%"
            Invalidate()
        End Set
    End Property

    Private _Maximum As Integer = 100
    Property Maximum() As Integer
        Get
            Return _Maximum
        End Get
        Set(ByVal value As Integer)
            If value < 1 Then value = 1
            If _Value > value Then _Value = value

            _Maximum = value
            Invalidate()
        End Set
    End Property

    Sub Increment(ByVal amount As Integer)
        Value += amount
    End Sub

    Protected Overrides Sub ColorHook()

    End Sub

    Property DrawPercent As Boolean = False

    Private Progress As Integer
    Protected Overrides Sub PaintHook()
        DrawBorders(New Pen(Color.FromArgb(32, 32, 32)), 1)
        G.FillRectangle(New SolidBrush(Color.FromArgb(50, 50, 50)), 0, 0, Width, 8)

        DrawGradient(Color.FromArgb(8, 8, 8), Color.FromArgb(23, 23, 23), 2, 2, Width - 4, Height - 4, 90.0F)

        Progress = CInt((_Value / _Maximum) * Width)

        If Not Progress = 0 Then
            G.SetClip(New Rectangle(3, 3, Progress - 6, Height - 6))
            G.FillRectangle(New SolidBrush(Color.FromArgb(39, 39, 39)), 0, 0, Progress, Height)

            DrawGradient(Blend, CInt(GlowPosition * Progress), 0, Progress, Height, 0.0F)
            DrawBorders(New Pen(Color.FromArgb(15, Color.White)), 3, 3, Progress - 6, Height - 6)

            G.FillRectangle(New SolidBrush(Color.FromArgb(13, Color.White)), 3, 3, Width - 6, 5)

            G.ResetClip()

        End If

        If DrawPercent = True Then
            DrawText(Brushes.White, HorizontalAlignment.Center, 0, 0)
        Else

        End If

        DrawBorders(Pens.Black, 2)
        DrawBorders(Pens.Black)
    End Sub
#End Region

End Class





Class SpaceGroupBox
    Inherits ThemeContainer153

#Region "Group Box"

    Sub New()
        ControlMode = True
        Header = 26
        Me.Text = Me.Measure.ToString
        ActiveTitleForeColourVal = Color.White
        InactiveTitleForeColourVal = Color.Red
        Me.AutoScroll = False
        Me.VerticalScroll.Enabled = True
    End Sub

    Private ActiveTitleForeColourVal As Color
    Private InactiveTitleForeColourVal As Color

    <System.ComponentModel.Description("Change the font colour of the title text when the control is enabled.")> _
    Public Property ActiveTitleForeColour() As Color
        Get
            Return ActiveTitleForeColourVal
        End Get
        Set(ByVal ActiveTitleForeColour As Color)
            ActiveTitleForeColourVal = ActiveTitleForeColour
            Invalidate()
        End Set
    End Property

    <System.ComponentModel.Description("Change the font colour of the title text when the control is disabled.")> _
    Public Property InactiveTitleForeColour() As Color
        Get
            Return InactiveTitleForeColourVal
        End Get
        Set(ByVal InactiveTitleForeColour As Color)
            InactiveTitleForeColourVal = InactiveTitleForeColour
            Invalidate()
        End Set
    End Property

    Private SettingsButtonBool As Boolean = False
    <System.ComponentModel.Description("Enable Settings button.")> _
    Public Property SettingsButton() As Boolean
        Get
            Return SettingsButtonBool
        End Get
        Set(ByVal SettingsBool As Boolean)
            SettingsButtonBool = SettingsBool
            Invalidate()
        End Set
    End Property

    Private SImage As Image
    <System.ComponentModel.Description("Enable Settings button.")> _
    Public Property SettingsImage() As Image
        Get
            Return SImage
        End Get
        Set(ByVal SettingImg As Image)
            SImage = SettingImg
            Invalidate()
        End Set
    End Property

    Protected Overrides Sub ColorHook()

    End Sub

    Private SettingsBounds As New Rectangle()
    Public Event SettingsClick As EventHandler

    Protected Overridable Sub OnSettingsClick(ByVal e As System.Windows.Forms.MouseEventArgs)
        If SettingsBounds.Contains(e.Location) Then
            RaiseEvent SettingsClick(Me, e)
        End If
    End Sub




    Protected Overrides Sub OnMouseUp(ByVal e As System.Windows.Forms.MouseEventArgs)
        If SettingsButtonBool Then OnSettingsClick(e)
        MyBase.OnMouseUp(e)
    End Sub

    Protected Overrides Sub PaintHook()
        G.Clear(Me.BackColor)

        DrawGradient(Color.FromArgb(20, 20, 20), Color.FromArgb(20, 20, 20), 2, 14, Width - 2, Height - 2)

        Dim RGB32 As New Pen(Color.FromArgb(32, 32, 32))
        G.DrawLine(RGB32, 1, 14, Width - 1, 14)
        G.DrawLine(RGB32, 1, 15, 1, Height - 1)
        G.DrawLine(RGB32, 2, Height - 2, Width - 2, Height - 2)
        G.DrawLine(RGB32, Width - 2, Height - 3, Width - 2, 15)

        Dim RGB0 As New Pen(Color.FromArgb(0, 0, 0))
        G.DrawLine(RGB0, 1, 13, Width - 2, 13)
        G.DrawLine(RGB0, 0, Height - 2, 0, 14)
        G.DrawLine(RGB0, Width - 1, Height - 2, Width - 1, 14)
        G.DrawLine(RGB0, 1, Height - 1, Width - 2, Height - 1)

        DrawGradient(Color.FromArgb(20, 20, 20), Color.FromArgb(20, 20, 20), 8, 0, Me.Measure.Width + 13, 26) ' <------ Gredient Box

        G.DrawLine(RGB0, 8, 0, Me.Measure.Width + 21, 0)
        G.DrawLine(RGB0, 8, 25, 8, 0)
        G.DrawLine(RGB0, 8, 25, Me.Measure.Width + 21, 25)
        G.DrawLine(RGB0, Me.Measure.Width + 21, 25, Me.Measure.Width + 21, 0)

        G.DrawLine(RGB32, 9, 1, Me.Measure.Width + 20, 1)
        G.DrawLine(RGB32, 9, 24, 9, 1)
        G.DrawLine(RGB32, 9, 24, Me.Measure.Width + 20, 24)
        G.DrawLine(RGB32, Me.Measure.Width + 20, 24, Me.Measure.Width + 20, 2)

        Dim xoffset As Integer = -8

        If SettingsButtonBool Then
            DrawGradient(Color.FromArgb(20, 20, 20), Color.FromArgb(20, 20, 20), Me.Width - 25 + xoffset, 0, 25, 25)
            G.DrawLine(RGB0, Me.Width - 25 + xoffset, 0, Me.Width + xoffset, 0)
            G.DrawLine(RGB0, Me.Width - 25 + xoffset, 25, Me.Width - 25 + xoffset, 0)
            G.DrawLine(RGB0, Me.Width - 25 + xoffset, 25, Me.Width + xoffset, 25)
            G.DrawLine(RGB0, Me.Width + xoffset, 25, Me.Width + xoffset, 0)

            G.DrawLine(RGB32, Me.Width - 24 + xoffset, 1, Me.Width - 1 + xoffset, 1)
            G.DrawLine(RGB32, Me.Width - 24 + xoffset, 24, Me.Width - 24 + xoffset, 1)
            G.DrawLine(RGB32, Me.Width - 24 + xoffset, 24, Me.Width - 1 + xoffset, 24)
            G.DrawLine(RGB32, Me.Width - 1 + xoffset, 24, Me.Width - 1 + xoffset, 2)
            SettingsBounds.Location = New Point(Me.Width - 25 + xoffset, 0)
            SettingsBounds.Size = New Size(25, 25)
            If Not SImage Is Nothing Then
                G.DrawImage(SImage, New Point(Me.Width - 22 + xoffset, 3))
            End If
        End If

        If Me.Enabled Then
            DrawText(New Drawing.SolidBrush(Color.FromArgb(255, ActiveTitleForeColourVal)), HorizontalAlignment.Left, 14, 0, Me.Text)
        Else
            DrawText(New Drawing.SolidBrush(Color.FromArgb(255, InactiveTitleForeColourVal)), HorizontalAlignment.Left, 14, 0, Me.Text)
        End If

    End Sub
#End Region

End Class





Class SpaceGroupTabBox
    Inherits TabControl

#Region "Group Tab Box"
    Private LightBlack As Color = Color.FromArgb(24, 24, 24)
    Private LighterBlack As Color = Color.FromArgb(24, 24, 24)
    Private DrawGradientBrush, DrawGradientBrush2 As LinearGradientBrush
    Private _ControlBColor As Color
    Public Property TabTextColor() As Color
        Get
            Return _ControlBColor
        End Get
        Set(ByVal v As Color)
            _ControlBColor = v
            Invalidate()
        End Set
    End Property

    Sub New()
        MyBase.New()
        SetStyle(ControlStyles.AllPaintingInWmPaint Or _
        ControlStyles.ResizeRedraw Or _
        ControlStyles.UserPaint Or _
        ControlStyles.DoubleBuffer, True)
        TabTextColor = Color.White
    End Sub
    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        Dim g As Graphics = e.Graphics
        MyBase.OnPaint(e)
        g.Clear(Color.FromArgb(20, 20, 20))
        Dim r2 As New Rectangle(0, 0, Width - 1, 11)
        Dim r3 As New Rectangle(0, 0, Width - 1, 11)
        Dim ItemBounds As Rectangle
        Dim TextBrush As New SolidBrush(Color.Empty)
        Dim TabBrush As New SolidBrush(Color.Lime)
        g.FillRectangle(New SolidBrush(Color.FromArgb(24, 24, 24)), New Rectangle(0, 1, Width - 0, Height - 2))
        DrawGradientBrush2 = New LinearGradientBrush(r3, Color.FromArgb(50, Color.White), Color.FromArgb(0, Color.White), 90S)
        g.FillRectangle(DrawGradientBrush2, r2)
        For TabItemIndex As Integer = 0 To Me.TabCount - 1
            ItemBounds = Me.GetTabRect(TabItemIndex)

            If CBool(TabItemIndex And 1) Then
                TabBrush.Color = Color.Transparent
            Else
                TabBrush.Color = Color.Transparent
            End If
            g.FillRectangle(TabBrush, ItemBounds)
            Dim BorderPen As Pen
            If TabItemIndex = SelectedIndex Then
                BorderPen = New Pen(Color.Transparent, 1)
            Else
                BorderPen = New Pen(Color.Transparent, 1)
            End If
            g.DrawRectangle(BorderPen, New Rectangle(ItemBounds.Location.X + 3, ItemBounds.Location.Y + 1, ItemBounds.Width - 8, ItemBounds.Height - 4))
            BorderPen.Dispose()
            Dim sf As New StringFormat
            sf.LineAlignment = StringAlignment.Center
            sf.Alignment = StringAlignment.Center

            If Me.SelectedIndex = TabItemIndex Then
                TextBrush.Color = TabTextColor
            Else
                TextBrush.Color = Color.Gray
            End If
            g.DrawString( _
            Me.TabPages(TabItemIndex).Text, _
            Me.Font, TextBrush, _
            RectangleF.op_Implicit(Me.GetTabRect(TabItemIndex)), sf)
            Try
                Me.TabPages(TabItemIndex).BackColor = Color.FromArgb(24, 24, 24)
            Catch
            End Try
        Next
        Try
            For Each tabpg As TabPage In Me.TabPages
                tabpg.BorderStyle = BorderStyle.None
            Next
        Catch
        End Try
        g.DrawRectangle(New Pen(New SolidBrush(Color.FromArgb(255, Color.Black))), 0, 0, Width - 1, Height - 1)
        g.DrawRectangle(New Pen(New SolidBrush(LighterBlack)), New Rectangle(3, 24, Width - 5, Height - 28))
        g.DrawLine(New Pen(New SolidBrush(Color.FromArgb(255, Color.Black))), 1, 23, Width - 2, 23)

    End Sub
#End Region

End Class





Class SpacePanel
    Inherits ThemeContainer153

#Region "Panel"

    Sub New()
        ControlMode = True
        Header = 26
    End Sub

    Private _Value As Color
    Property BorderColour() As Color
        Get
            Return _Value
        End Get
        Set(ByVal value As Color)
            _Value = value
            Invalidate()
        End Set
    End Property

    Private _Activated As Boolean = False
    Property Activated() As Boolean
        Get
            Return _Activated
        End Get
        Set(ByVal value As Boolean)
            _Activated = value
            Invalidate()
        End Set
    End Property

    Protected Overrides Sub ColorHook()

    End Sub

    Protected Overrides Sub PaintHook()
        G.Clear(Color.FromArgb(14, 14, 14))

        DrawGradient(Color.FromArgb(20, 20, 20), Color.FromArgb(20, 20, 20), 1, 1, Width - 2, Height - 2)

        Dim RGB32 As New Pen(Color.FromArgb(32, 32, 32))
        G.DrawLine(RGB32, 2, 1, Width - 1, 1)
        G.DrawLine(RGB32, 1, 1, 1, Height - 1)
        G.DrawLine(RGB32, 2, Height - 2, Width - 2, Height - 2)
        G.DrawLine(RGB32, Width - 2, Height - 3, Width - 2, 1)

        Dim RGB0 As Pen
        If _Activated Then
            RGB0 = New Pen(_Value)
        Else
            RGB0 = New Pen(Color.FromArgb(0, 0, 0))
        End If
        G.DrawLine(RGB0, 0, 0, Width - 1, 0)
        G.DrawLine(RGB0, 0, Height - 1, 0, 1)
        G.DrawLine(RGB0, 1, Height - 1, Width - 1, Height - 1)
        G.DrawLine(RGB0, Width - 1, Height - 1, Width - 1, 1)

    End Sub
#End Region

End Class





Class SpaceTabControl
    Inherits TabControl

#Region "TabControl"
    Private LightBlack As Color = Color.FromArgb(24, 24, 24)
    Private LighterBlack As Color = Color.FromArgb(24, 24, 24)
    Private DrawGradientBrush, DrawGradientBrush2 As LinearGradientBrush
    Private _ControlBColor As Color
    Public Property TabTextColor() As Color
        Get
            Return _ControlBColor
        End Get
        Set(ByVal v As Color)
            _ControlBColor = v
            Invalidate()
        End Set
    End Property

    Sub New()
        MyBase.New()
        SetStyle(ControlStyles.AllPaintingInWmPaint Or _
        ControlStyles.ResizeRedraw Or _
        ControlStyles.UserPaint Or _
        ControlStyles.DoubleBuffer, True)
        TabTextColor = Color.White
    End Sub
    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        MyBase.OnPaint(e)
        Dim r2 As New Rectangle(0, 0, Width - 1, 11)
        Dim r3 As New Rectangle(0, 0, Width - 1, 11)
        Dim ItemBounds As Rectangle
        Dim TextBrush As New SolidBrush(Color.Empty)
        Dim TabBrush As New SolidBrush(Color.Lime)
        e.Graphics.FillRectangle(New SolidBrush(Color.FromArgb(24, 24, 24)), New Rectangle(0, 1, Width - 0, Height - 2))
        DrawGradientBrush2 = New LinearGradientBrush(r3, Color.FromArgb(50, Color.White), Color.FromArgb(0, Color.White), 90S)
        e.Graphics.FillRectangle(DrawGradientBrush2, r2)
        For TabItemIndex As Integer = 0 To Me.TabCount - 1
            ItemBounds = Me.GetTabRect(TabItemIndex)

            If CBool(TabItemIndex And 1) Then
                TabBrush.Color = Color.Transparent
            Else
                TabBrush.Color = Color.Transparent
            End If
            e.Graphics.FillRectangle(TabBrush, ItemBounds)
            Dim BorderPen As Pen
            If TabItemIndex = SelectedIndex Then
                BorderPen = New Pen(Color.Transparent, 1)
            Else
                BorderPen = New Pen(Color.Transparent, 1)
            End If
            e.Graphics.DrawRectangle(BorderPen, New Rectangle(ItemBounds.Location.X + 3, ItemBounds.Location.Y + 1, ItemBounds.Width - 8, ItemBounds.Height - 4))
            BorderPen.Dispose()
            Dim sf As New StringFormat
            sf.LineAlignment = StringAlignment.Center
            sf.Alignment = StringAlignment.Center

            If Me.SelectedIndex = TabItemIndex Then
                TextBrush.Color = TabTextColor
            Else
                TextBrush.Color = Color.Gray
            End If
            e.Graphics.DrawString( _
            Me.TabPages(TabItemIndex).Text, _
            Me.Font, TextBrush, _
            RectangleF.op_Implicit(Me.GetTabRect(TabItemIndex)), sf)
            Try
                Me.TabPages(TabItemIndex).BackColor = Color.FromArgb(24, 24, 24)
            Catch
            End Try
        Next
        Try
            For Each tabpg As TabPage In Me.TabPages
                tabpg.BorderStyle = BorderStyle.None
            Next
        Catch
        End Try
        e.Graphics.DrawRectangle(New Pen(New SolidBrush(Color.FromArgb(255, Color.Black))), 0, 0, Width - 1, Height - 1)
        e.Graphics.DrawRectangle(New Pen(New SolidBrush(LighterBlack)), New Rectangle(3, 24, Width - 5, Height - 28))
        e.Graphics.DrawLine(New Pen(New SolidBrush(Color.FromArgb(255, Color.Black))), 1, 23, Width - 2, 23)

    End Sub
#End Region

End Class





Class SpaceSeperator
    Inherits ThemeControl153

#Region "Seperator"

    Private _Orientation As Orientation
    Property Orientation() As Orientation
        Get
            Return _Orientation
        End Get
        Set(ByVal value As Orientation)
            _Orientation = value

            Invalidate()
        End Set
    End Property

    Sub New()
        Transparent = True
        BackColor = Color.Transparent

        LockHeight = 5
    End Sub

    Protected Overrides Sub ColorHook()

    End Sub

    Protected Overrides Sub PaintHook()
        G.Clear(BackColor)

        Dim BL1, BL2 As New ColorBlend
        BL1.Positions = New Single() {0.0F, 0.15F, 0.85F, 1.0F}
        BL2.Positions = New Single() {0.0F, 0.15F, 0.5F, 0.85F, 1.0F}

        BL1.Colors = New Color() {Color.Transparent, Color.Black, Color.Black, Color.Transparent}
        BL2.Colors = New Color() {Color.Transparent, Color.FromArgb(35, 35, 35), Color.FromArgb(45, 45, 45), Color.FromArgb(35, 35, 35), Color.Transparent}

        Dim RGB32 As New Pen(Color.FromArgb(32, 32, 32))
        Dim RGB3 As New Pen(Color.FromArgb(3, 3, 3))

        If _Orientation = Windows.Forms.Orientation.Vertical Then
            G.DrawLine(RGB3, 1, 1, Width, 1)
            G.DrawLine(RGB32, 1, 2, Width, 2)
        Else
            G.DrawLine(RGB3, 1, 1, Width, 1)
            G.DrawLine(RGB32, 1, 2, Width, 2)
        End If

    End Sub
#End Region

End Class





Class SpaceTextBox
    Inherits ThemeControl153

#Region "TextBox"

    Private WithEvents Textbx As New TextBox
    Sub New()
        Textbx.TextAlign = HorizontalAlignment.Left
        Textbx.BorderStyle = BorderStyle.None
        Textbx.Location = New Point(4, 3)
        Controls.Add(Textbx)
        ForeColourVal = Color.White
        Textbx.ForeColor = ForeColourVal
        Multilined = False
        ScrollBarsVal = 0
    End Sub

    Private ForeColourVal As Color
    Private ScrollBarsVal As ScrollBar

    <System.ComponentModel.Description("Change the font colour of the text.")> _
    Public Property ForeColour() As Color
        Get
            Return ForeColourVal
        End Get
        Set(ByVal ForeColour As Color)
            ForeColourVal = ForeColour
            Textbx.ForeColor = ForeColourVal
            'Invalidate()
        End Set
    End Property

    <System.ComponentModel.Description("Change what type of scrolling function the textbox is allowed.")> _
    Public Property ScrollBars() As ScrollBar
        Get
            Return ScrollBarsVal
        End Get
        Set(ByVal ScrollBars As ScrollBar)
            ScrollBarsVal = ScrollBars
            Invalidate()
        End Set
    End Property

    Protected Overrides Sub ColorHook()

    End Sub

    Property PasswordChar As String
    Property UseSystemPasswordChar As Boolean = False
    Property MaxLength As String = 32767
    Property Multilined As Boolean = False
    Property Read_Only As Boolean = False

    Enum ScrollBar As Byte
        None = 0
        Vertical = 1
        Horizontal = 2
        Both = 3
    End Enum

    Protected Overrides Sub PaintHook()
        G.Clear(Color.FromArgb(14, 14, 14))
        Textbx.BackColor = Color.FromArgb(14, 14, 14)

        Dim RGB28 As New Pen(Color.FromArgb(28, 28, 28))
        G.DrawRectangle(RGB28, 0, 0, Width - 1, Height - 1)

        Dim RGB3 As New Pen(Color.FromArgb(3, 3, 3))
        G.DrawRectangle(RGB3, 1, 1, Width - 3, Height - 3)

        Textbx.Size = New Size(Me.Width - 8, Textbx.Height - 4)
        Textbx.PasswordChar = PasswordChar
        Textbx.UseSystemPasswordChar = UseSystemPasswordChar
        Textbx.MaxLength = MaxLength
        Textbx.ReadOnly = Read_Only

        If Multilined = True Then
            Textbx.Multiline = Multilined
            Textbx.Height = Me.Height - 6
            Textbx.Width = Me.Width - 7
            LockHeight = 0
        Else
            ScrollBarsVal = "0"
            Textbx.Multiline = Multilined
            LockHeight = 20
        End If

        ScrollBarsState()

    End Sub

    Sub GetFoc() Handles Textbx.GotFocus

    End Sub

    Sub LostFoc() Handles Textbx.LostFocus

    End Sub

    Sub ScrollBarsState()
        If ScrollBarsVal = "1" And Multilined = True Then
            Textbx.ScrollBars = Windows.Forms.ScrollBars.Vertical
            Textbx.Location = New Point(4, 3)
        Else
            If ScrollBarsVal = "2" And Multilined = True Then
                Textbx.ScrollBars = Windows.Forms.ScrollBars.Horizontal
                Textbx.Location = New Point(4, 3)
            Else
                If ScrollBarsVal = "3" And Multilined = True Then
                    Textbx.ScrollBars = Windows.Forms.ScrollBars.Both
                    Textbx.Location = New Point(4, 3)
                Else
                    'If ScrollBarsVal = "0" And Multilined = True Then
                    Textbx.ScrollBars = Windows.Forms.ScrollBars.None
                    Textbx.Location = New Point(4, 3)
                End If
            End If
        End If
    End Sub

    Sub TextBox_TextChanged() Handles Textbx.TextChanged
        Text = Textbx.Text
    End Sub

    Sub PropertyTextChanged() Handles MyBase.TextChanged
        Textbx.Text = Text
    End Sub

#End Region

End Class


Class SpacePlaceHolder
    Inherits ThemeControl153

#Region "Placeholder"

    Private WithEvents Textbx As New TextBox
    Sub New()
        Textbx.TextAlign = HorizontalAlignment.Left
        Textbx.BorderStyle = BorderStyle.None
        Textbx.Location = New Point(4, 3)
        Controls.Add(Textbx)
        ForeColourVal = Color.White
        Textbx.ForeColor = ForeColourVal
        Multilined = False
        ScrollBarsVal = 0
    End Sub

    Private ForeColourVal As Color
    Private ScrollBarsVal As ScrollBar

    <System.ComponentModel.Description("Change the font colour of the text.")> _
    Public Property ForeColour() As Color
        Get
            Return ForeColourVal
        End Get
        Set(ByVal ForeColour As Color)
            ForeColourVal = ForeColour
            Textbx.ForeColor = ForeColourVal
            'Invalidate()
        End Set
    End Property

    <System.ComponentModel.Description("Change what type of scrolling function the textbox is allowed.")> _
    Public Property ScrollBars() As ScrollBar
        Get
            Return ScrollBarsVal
        End Get
        Set(ByVal ScrollBars As ScrollBar)
            ScrollBarsVal = ScrollBars
            Invalidate()
        End Set
    End Property

    Protected Overrides Sub ColorHook()

    End Sub

    Property PasswordChar As String
    Property UseSystemPasswordChar As Boolean = False
    Property MaxLength As String = 32767
    Property Multilined As Boolean = False
    Property Read_Only As Boolean = False
    Property Placeholder As String
    Public Active As Boolean
    Dim initial As Integer = 0
    Property Foc As Boolean

    Enum ScrollBar As Byte
        None = 0
        Vertical = 1
        Horizontal = 2
        Both = 3
    End Enum

    Protected Overrides Sub PaintHook()
        G.Clear(Color.FromArgb(14, 14, 14))
        Textbx.BackColor = Color.FromArgb(14, 14, 14)

        Dim RGB28 As New Pen(Color.FromArgb(28, 28, 28))
        G.DrawRectangle(RGB28, 0, 0, Width - 1, Height - 1)

        Dim RGB3 As New Pen(Color.FromArgb(3, 3, 3))
        G.DrawRectangle(RGB3, 1, 1, Width - 3, Height - 3)

        Textbx.Size = New Size(Me.Width - 8, Textbx.Height - 4)
        Textbx.PasswordChar = PasswordChar
        Textbx.UseSystemPasswordChar = UseSystemPasswordChar
        Textbx.MaxLength = MaxLength
        Textbx.ReadOnly = Read_Only

        If Multilined = True Then
            Textbx.Multiline = Multilined
            Textbx.Height = Me.Height - 6
            Textbx.Width = Me.Width - 7
            LockHeight = 0
        Else
            ScrollBarsVal = "0"
            Textbx.Multiline = Multilined
            LockHeight = 20
        End If

        Me.ForeColour = Textbx.ForeColor

        If initial = 0 Then
            Textbx.ForeColor = Color.Gray
            initial = 1
            Active = False
        End If

        ScrollBarsState()

    End Sub

    Sub PlaceholderEnable()
        Textbx.Text = Placeholder
        Textbx.ForeColor = Color.Gray
        Me.ForeColour = Color.Gray
    End Sub

    Sub PlaceholderDisabled()
        Textbx.Text = Nothing
        Textbx.ForeColor = Color.White
        Me.ForeColour = Color.White
    End Sub

    Sub Sel() Handles Textbx.GotFocus
        Active = True
        If Textbx.Text = Placeholder Then
            PlaceholderDisabled()
        End If
    End Sub

    Sub UnSel() Handles Textbx.LostFocus
        Active = False
        If Textbx.Text = Nothing Then
            PlaceholderEnable()
        End If
    End Sub

    Sub ScrollBarsState()
        If ScrollBarsVal = "1" And Multilined = True Then
            Textbx.ScrollBars = Windows.Forms.ScrollBars.Vertical
            Textbx.Location = New Point(4, 3)
        Else
            If ScrollBarsVal = "2" And Multilined = True Then
                Textbx.ScrollBars = Windows.Forms.ScrollBars.Horizontal
                Textbx.Location = New Point(4, 3)
            Else
                If ScrollBarsVal = "3" And Multilined = True Then
                    Textbx.ScrollBars = Windows.Forms.ScrollBars.Both
                    Textbx.Location = New Point(4, 3)
                Else
                    'If ScrollBarsVal = "0" And Multilined = True Then
                    Textbx.ScrollBars = Windows.Forms.ScrollBars.None
                    Textbx.Location = New Point(4, 3)
                End If
            End If
        End If
    End Sub

    Sub TextBox_TextChanged() Handles Textbx.TextChanged
        Text = Textbx.Text
    End Sub

    Sub PropertyTextChanged() Handles MyBase.TextChanged
        Textbx.Text = Text
    End Sub

#End Region

End Class




Class SpaceCheckBox
    Inherits ThemeControl153

#Region "Check Box"
    Private _CheckedState As Boolean

    Public Property CheckedState() As Boolean
        Get
            Return _CheckedState
        End Get
        Set(ByVal v As Boolean)
            _CheckedState = v
            Invalidate()
        End Set
    End Property
    Sub New()
        Size = New Size(119, 16)
        MinimumSize = New Size(16, 16)
        MaximumSize = New Size(600, 16)
        CheckedState = False
        Me.BackColor = Color.FromArgb(20, 20, 20)
    End Sub
    Protected Overrides Sub ColorHook()

    End Sub

    Protected Overrides Sub OnTextChanged(ByVal e As System.EventArgs)
        MyBase.OnTextChanged(e)
        Dim textSize As Integer
        textSize = Me.CreateGraphics.MeasureString(Text, Font).Width
        Me.Width = 20 + textSize
        Invalidate()
    End Sub

    Protected Overrides Sub PaintHook()
        Dim FontColor As Color
        FontColor = Color.White

        Me.Height = 16
        Me.Width = Me.Measure.Width.ToString + 20

        G.Clear(Me.BackColor)
        Select Case CheckedState
            Case True
                G.Clear(Me.BackColor)
                G.DrawLine(Pens.Black, 0, 1, 0, 14)
                G.DrawLine(Pens.Black, 1, 15, 15, 15)
                G.DrawLine(Pens.Black, 15, 1, 15, 14)
                G.DrawLine(Pens.Black, 1, 0, 14, 0)

                G.DrawLine(Pens.Black, 2, 2, 2, 13)
                G.DrawLine(Pens.Black, 2, 2, 13, 2)
                G.DrawLine(Pens.Black, 2, 13, 13, 13)
                G.DrawLine(Pens.Black, 13, 13, 13, 2)

                'outline
                Dim RGB54 As New Pen(Color.FromArgb(54, 54, 54))
                G.DrawLine(RGB54, 1, 1, 14, 1)

                DrawPixel(Color.FromArgb(50, 50, 50), 1, 2)
                DrawPixel(Color.FromArgb(50, 50, 50), 14, 2)

                DrawPixel(Color.FromArgb(48, 48, 48), 1, 3)
                DrawPixel(Color.FromArgb(48, 48, 48), 14, 3)

                DrawPixel(Color.FromArgb(44, 44, 44), 1, 4)
                DrawPixel(Color.FromArgb(44, 44, 44), 14, 4)

                DrawPixel(Color.FromArgb(40, 40, 40), 1, 5)
                DrawPixel(Color.FromArgb(40, 40, 40), 14, 5)

                DrawPixel(Color.FromArgb(37, 37, 37), 1, 6)
                DrawPixel(Color.FromArgb(37, 37, 37), 14, 6)

                DrawPixel(Color.FromArgb(34, 34, 34), 1, 7)
                DrawPixel(Color.FromArgb(34, 34, 34), 14, 7)

                Dim RGB26 As New Pen(Color.FromArgb(26, 26, 26))
                G.DrawLine(RGB26, 1, 8, 1, 14)
                G.DrawLine(RGB26, 1, 14, 14, 14)
                G.DrawLine(RGB26, 14, 14, 14, 8)

                Dim RGB43 As New Pen(Color.FromArgb(43, 43, 43))
                G.DrawLine(RGB43, 3, 3, 12, 3)
                G.DrawLine(RGB43, 3, 4, 12, 4)
                G.DrawLine(RGB43, 3, 5, 12, 5)

                Dim RGB42 As New Pen(Color.FromArgb(42, 42, 42))
                G.DrawLine(RGB42, 3, 6, 12, 6)
                G.DrawLine(RGB42, 3, 7, 12, 7)

                Dim RGB38 As New Pen(Color.FromArgb(38, 38, 38))
                G.DrawLine(RGB38, 3, 8, 12, 8)

                Dim RGB41 As New Pen(Color.FromArgb(41, 41, 41))
                G.DrawLine(RGB41, 3, 9, 12, 9)

                Dim RGB44 As New Pen(Color.FromArgb(44, 44, 44))
                G.DrawLine(RGB44, 3, 10, 12, 10)

                Dim RGB48 As New Pen(Color.FromArgb(48, 48, 48))
                G.DrawLine(RGB48, 3, 11, 12, 11)

                Dim RGB50 As New Pen(Color.FromArgb(50, 50, 50))
                G.DrawLine(RGB50, 3, 12, 12, 12)

            Case False
                G.Clear(Me.BackColor)
                G.DrawLine(Pens.Black, 0, 1, 0, 14)
                G.DrawLine(Pens.Black, 1, 15, 15, 15)
                G.DrawLine(Pens.Black, 15, 1, 15, 14)
                G.DrawLine(Pens.Black, 1, 0, 14, 0)

                G.DrawLine(Pens.Black, 2, 2, 2, 13)
                G.DrawLine(Pens.Black, 2, 2, 13, 2)
                G.DrawLine(Pens.Black, 2, 13, 13, 13)
                G.DrawLine(Pens.Black, 13, 13, 13, 2)

                'outline
                Dim RGB54 As New Pen(Color.FromArgb(54, 54, 54))
                G.DrawLine(RGB54, 1, 1, 14, 1)

                DrawPixel(Color.FromArgb(50, 50, 50), 1, 2)
                DrawPixel(Color.FromArgb(50, 50, 50), 14, 2)

                DrawPixel(Color.FromArgb(48, 48, 48), 1, 3)
                DrawPixel(Color.FromArgb(48, 48, 48), 14, 3)

                DrawPixel(Color.FromArgb(44, 44, 44), 1, 4)
                DrawPixel(Color.FromArgb(44, 44, 44), 14, 4)

                DrawPixel(Color.FromArgb(40, 40, 40), 1, 5)
                DrawPixel(Color.FromArgb(40, 40, 40), 14, 5)

                DrawPixel(Color.FromArgb(37, 37, 37), 1, 6)
                DrawPixel(Color.FromArgb(37, 37, 37), 14, 6)

                DrawPixel(Color.FromArgb(34, 34, 34), 1, 7)
                DrawPixel(Color.FromArgb(34, 34, 34), 14, 7)

                Dim RGB26 As New Pen(Color.FromArgb(26, 26, 26))
                G.DrawLine(RGB26, 1, 8, 1, 14)
                G.DrawLine(RGB26, 1, 14, 14, 14)
                G.DrawLine(RGB26, 14, 14, 14, 8)

                'gradiant
                Dim RGB9 As New Pen(Color.FromArgb(9, 9, 9))
                G.DrawLine(RGB9, 3, 3, 12, 3)

                Dim RGB10 As New Pen(Color.FromArgb(10, 10, 10))
                G.DrawLine(RGB10, 3, 4, 12, 4)
                G.DrawLine(RGB10, 3, 5, 12, 5)

                Dim RGB11 As New Pen(Color.FromArgb(11, 11, 11))
                G.DrawLine(RGB11, 3, 6, 12, 6)

                Dim RGB12 As New Pen(Color.FromArgb(12, 12, 12))
                G.DrawLine(RGB12, 3, 7, 12, 7)
                G.DrawLine(RGB12, 3, 8, 12, 8)

                Dim RGB13 As New Pen(Color.FromArgb(13, 13, 13))
                G.DrawLine(RGB13, 3, 9, 12, 9)

                Dim RGB14 As New Pen(Color.FromArgb(14, 14, 14))
                G.DrawLine(RGB14, 3, 10, 12, 10)
                G.DrawLine(RGB14, 3, 11, 12, 11)

                Dim RGB15 As New Pen(Color.FromArgb(15, 15, 15))
                G.DrawLine(RGB15, 3, 12, 12, 12)

        End Select
        DrawPixel(Me.BackColor, 0, 0)
        DrawPixel(Me.BackColor, 0, Me.Height - 1)
        DrawPixel(Me.BackColor, 15, Me.Height - 1)
        DrawPixel(Me.BackColor, 15, 0)
        DrawText(Brushes.White, HorizontalAlignment.Left, 17, 0)
    End Sub
    Sub changeCheck() Handles Me.Click
        Select Case CheckedState
            Case True
                CheckedState = False
            Case False
                CheckedState = True
        End Select
    End Sub
#End Region

End Class




Class SpaceRadioButton
    Inherits ThemeControl153

#Region "Radio Button"

    Sub New()
        LockHeight = 16

        SetColor("Gloss1", 38, Color.White)
        SetColor("Gloss2", 5, Color.White)
        SetColor("Checked1", Color.Transparent)
        SetColor("Checked2", 40, Color.White)
        SetColor("Unchecked1", 8, 8, 8)
        SetColor("Unchecked2", 16, 16, 16)
        SetColor("Glow", 5, Color.White)
        SetColor("Text", Color.White)
        SetColor("InnerOutline", Color.Black)
        SetColor("OuterOutline", 15, Color.White)
    End Sub

    Protected Overrides Sub ColorHook()
        C1 = GetColor("Gloss1")
        C2 = GetColor("Gloss2")
        C3 = GetColor("Checked1")
        C4 = GetColor("Checked2")
        C5 = GetColor("Unchecked1")
        C6 = GetColor("Unchecked2")

        B1 = New SolidBrush(GetColor("Glow"))
        B2 = New SolidBrush(GetColor("Text"))

        P1 = New Pen(GetColor("InnerOutline"))
        P2 = New Pen(GetColor("OuterOutline"))
    End Sub

    Private C1, C2, C3, C4, C5, C6 As Color
    Private P1, P2 As Pen
    Private B1, B2 As SolidBrush

    Private R1, R2 As Rectangle
    Private G1 As LinearGradientBrush

    Protected Overrides Sub PaintHook()
        G.Clear(BackColor)

        G.SmoothingMode = SmoothingMode.HighQuality
        R1 = New Rectangle(4, 2, _Field - 8, (_Field \ 2) - 1)
        R2 = New Rectangle(4, 2, _Field - 8, (_Field \ 2))

        G1 = New LinearGradientBrush(R2, C1, C2, 90S)
        G.FillEllipse(G1, R1)

        R1 = New Rectangle(2, 2, _Field - 4, _Field - 4)

        If _Checked Then
            G1 = New LinearGradientBrush(R1, C3, C4, 90S)
        Else
            G1 = New LinearGradientBrush(R1, C5, C6, 90S)
        End If
        G.FillEllipse(G1, R1)

        If State = MouseState.Over Then
            R1 = New Rectangle(2, 2, _Field - 4, _Field - 4)
            G.FillEllipse(B1, R1)
        End If

        DrawText(B2, HorizontalAlignment.Left, _Field + 3, 0)

        G.DrawEllipse(P1, 2, 2, _Field - 4, _Field - 4)
        G.DrawEllipse(P2, 1, 1, _Field - 2, _Field - 2)

    End Sub

    Private _Field As Integer = 16
    Property Field() As Integer
        Get
            Return _Field
        End Get
        Set(ByVal value As Integer)
            If value < 4 Then Return
            _Field = value
            LockHeight = value
            Invalidate()
        End Set
    End Property

    Private _Checked As Boolean
    Property Checked() As Boolean
        Get
            Return _Checked
        End Get
        Set(ByVal value As Boolean)
            _Checked = value
            InvalidateControls()
            RaiseEvent CheckedChanged(Me)
            Invalidate()
        End Set
    End Property

    Protected Overrides Sub OnMouseDown(ByVal e As System.Windows.Forms.MouseEventArgs)
        If Not _Checked Then Checked = True
        MyBase.OnMouseDown(e)
    End Sub

    Event CheckedChanged(ByVal sender As Object)

    Protected Overrides Sub OnCreation()
        InvalidateControls()
    End Sub

    Private Sub InvalidateControls()
        If Not IsHandleCreated OrElse Not _Checked Then Return

        For Each C As Control In Parent.Controls
            If C IsNot Me AndAlso TypeOf C Is SpaceRadioButton Then
                DirectCast(C, SpaceRadioButton).Checked = False
            End If
        Next
    End Sub

#End Region

End Class




Class SpaceComboBox
    Inherits ComboBox

#Region "Combo Box"
    Sub New()
        MyBase.New()
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.ResizeRedraw Or ControlStyles.UserPaint Or ControlStyles.DoubleBuffer, True)
        DrawMode = Windows.Forms.DrawMode.OwnerDrawFixed
        ItemHeight = 16
        BackColor = Color.FromArgb(30, 30, 30)
        DropDownStyle = ComboBoxStyle.DropDownList
    End Sub

    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        If Not DropDownStyle = ComboBoxStyle.DropDownList Then DropDownStyle = ComboBoxStyle.DropDownList
        Dim B As New Bitmap(Width, Height)
        Dim G As Graphics = Graphics.FromImage(B)

        G.Clear(Color.FromArgb(14, 14, 14))
        Dim GradientBrush As LinearGradientBrush = New LinearGradientBrush(New Rectangle(0, 0, Width, Height / 5 * 2), Color.FromArgb(20, 0, 0, 0), Color.FromArgb(24, 24, 24), 90.0F)
        G.FillRectangle(GradientBrush, New Rectangle(0, 0, Width, Height / 5 * 2))

        Dim S1 As Integer = G.MeasureString("Item", Font).Height
        If SelectedIndex <> -1 Then
            G.DrawString(Items(SelectedIndex), Font, New SolidBrush(Color.White), 4, Height \ 2 - S1 \ 2)
        Else
            If Not Items Is Nothing And Items.Count > 0 Then
                G.DrawString(Items(0), Font, New SolidBrush(Color.White), 4, Height \ 2 - S1 \ 2)
            Else
                G.DrawString("Items", Font, New SolidBrush(Color.White), 4, Height \ 2 - S1 \ 2)
            End If
        End If

        Dim RGB14 As New Pen(Color.FromArgb(14, 14, 14))
        G.DrawRectangle(RGB14, 0, 0, Width - 1, Height - 1)
        G.DrawRectangle(New Pen(Color.FromArgb(90, 90, 90)), 1, 1, Width - 3, Height - 3)
        G.DrawLine(New Pen(Color.FromArgb(90, 90, 90)), Width - 25, 1, Width - 25, Height - 3)
        G.DrawLine(Pens.Black, Width - 24, 0, Width - 24, Height)
        G.DrawLine(New Pen(Color.FromArgb(90, 90, 90)), Width - 23, 1, Width - 23, Height - 3)

        G.FillPolygon(Brushes.Black, Triangle(New Point(Width - 14, Height \ 2), New Size(5, 3)))
        G.FillPolygon(Brushes.White, Triangle(New Point(Width - 15, Height \ 2 - 1), New Size(5, 3)))

        e.Graphics.DrawImage(B.Clone, 0, 0)
        G.Dispose() : B.Dispose()
    End Sub

    Protected Overrides Sub OnDrawItem(ByVal e As DrawItemEventArgs)
        If e.Index < 0 Then Exit Sub
        Dim rect As New Rectangle()
        rect.X = e.Bounds.X
        rect.Y = e.Bounds.Y
        rect.Width = e.Bounds.Width - 2
        rect.Height = e.Bounds.Height - 1

        e.DrawBackground()
        If e.State = 785 Or e.State = 17 Then
            e.Graphics.FillRectangle(New SolidBrush(Color.FromArgb(14, 14, 14)), e.Bounds)
            e.Graphics.DrawString(Me.Items(e.Index).ToString(), e.Font, Brushes.White, e.Bounds.X, e.Bounds.Y)
        Else
            e.Graphics.FillRectangle(New SolidBrush(Color.FromArgb(14, 14, 14)), e.Bounds)
            e.Graphics.DrawString(Me.Items(e.Index).ToString(), e.Font, Brushes.Gray, e.Bounds.X, e.Bounds.Y)
        End If
        MyBase.OnDrawItem(e)
    End Sub

    Public Function Triangle(ByVal Location As Point, ByVal Size As Size) As Point()
        Dim ReturnPoints(0 To 3) As Point
        ReturnPoints(0) = Location
        ReturnPoints(1) = New Point(Location.X + Size.Width, Location.Y)
        ReturnPoints(2) = New Point(Location.X + Size.Width \ 2, Location.Y + Size.Height)
        ReturnPoints(3) = Location

        Return ReturnPoints
    End Function

    Private Sub GhostComboBox_DropDown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.DropDown

    End Sub

    Private Sub GhostComboBox_DropDownClosed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.DropDownClosed
        DropDownStyle = ComboBoxStyle.Simple
        Application.DoEvents()
        DropDownStyle = ComboBoxStyle.DropDownList
    End Sub

    Private Sub GhostCombo_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.TextChanged
        Invalidate()
    End Sub
#End Region

End Class




Class SpaceLabel
    Inherits Label

#Region "Label"

    Sub New()
        Me.BackColor = Color.Transparent
        Me.ForeColor = Color.White
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)
    End Sub

#End Region

End Class




Class DoubleBufferedTreeview
    Inherits TreeView

#Region "DoubleBuffered Treeview"
    Sub New()
        SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
        Me.DoubleBuffered = True
    End Sub
#End Region

End Class



Class SpaceSplashScreen
    Inherits ThemeContainer153

#Region "SplashScreen"

    Sub New()
        TransparencyKey = Color.DeepPink
        Me.DoubleBuffered = True
    End Sub

    Dim CustomFont As New CustomFont(My.Resources.Champagne___Limousines)

    Protected Overrides Sub PaintHook()
        G.Clear(Color.FromArgb(24, 24, 24))

        DrawGradient(Color.FromArgb(48, 48, 48), Color.FromArgb(14, 14, 14), 0, 0, Width, Height)

        'Border
        Dim RGBBlack As New Pen(Color.Black)
        G.DrawLine(RGBBlack, 0, 0, 0, Height)
        G.DrawLine(RGBBlack, Width - 1, 0, Width - 1, Height)
        G.DrawLine(RGBBlack, 0, Height - 1, Width, Height - 1)
        G.DrawLine(RGBBlack, 0, 0, Width - 1, 0)

        Dim xpos As Integer = (ParentForm.Width / 2) - (TextRenderer.MeasureText("GhostzGamerz", New Font(CustomFont.Font, 49)).Width / 2)
        Dim ypos As Integer = (ParentForm.Height / 2) - (TextRenderer.MeasureText("GhostzGamerz", New Font(CustomFont.Font, 49)).Height / 2)
        'MsgBox(xpos & "/" & ypos)
        Dim xpos2 As Integer = (ParentForm.Width / 2) - (TextRenderer.MeasureText("Developed by Kieran Devlin © 2015", New Font(CustomFont.Font, 14)).Width / 2)
        Dim ypos2 As Integer = (ParentForm.Height - (TextRenderer.MeasureText("Developed by Kieran Devlin © 2015", New Font(CustomFont.Font, 14)).Height / 2)) - 15
        'MsgBox(TextRenderer.MeasureText("Developed by Kieran Devlin © 2015", New Font(CustomFont.Font, 14)).Width)

        G.DrawString("Ghostz", New Font(CustomFont.Font, 49), Brushes.DeepSkyBlue, New Point(15, 67))
        G.DrawString("Gamerz", New Font(CustomFont.Font, 49), Brushes.Gray, New Point(180, 67))
        G.DrawString("Developed by Devvo © 2015", New Font(CustomFont.Font, 8), Brushes.White, New Point(140, 176))
    End Sub

    Protected Overrides Sub ColorHook()

    End Sub

#End Region

End Class




Class SpaceContextMenu
    Inherits ContextMenuStrip

#Region "Context Menu"

    Sub New(ByVal size As Size)
        AutoSize = True
        Me.Size = size
        Margin = Nothing
        Padding = Nothing
        ShowImageMargin = False
        ShowCheckMargin = False
        ShowItemToolTips = False
    End Sub

    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        Me.Size = Size
        Dim g As Graphics = e.Graphics


        g.Clear(Color.FromArgb(20, 20, 20))
        MyBase.OnPaint(e)

        Dim RGB32 As New Pen(Color.FromArgb(32, 32, 32))
        g.DrawLine(RGB32, 2, 1, Width - 1, 1)
        g.DrawLine(RGB32, 1, 1, 1, Height - 1)
        g.DrawLine(RGB32, 2, Height - 2, Width - 2, Height - 2)
        g.DrawLine(RGB32, Width - 2, Height - 3, Width - 2, 1)

        Dim RGB0 As Pen
        RGB0 = New Pen(Color.FromArgb(0, 0, 0))

        g.DrawLine(RGB0, 0, 0, Width - 1, 0)
        g.DrawLine(RGB0, 0, Height - 1, 0, 1)
        g.DrawLine(RGB0, 1, Height - 1, Width - 1, Height - 1)
        g.DrawLine(RGB0, Width - 1, Height - 1, Width - 1, 1)

        'Dim index As Integer = 0
        'For Each i As ToolStripMenuItem In Items
        '    g.DrawString(i.Text, Me.Font, Brushes.White, New Point(5, 4 + index * 21))
        '    index += 1
        'Next

    End Sub

#End Region

End Class





Class SpaceMenuItem
    Inherits ToolStripItem

#Region "Context Menu Item"

    Sub New(ByVal text As String, ByVal s As Size, Optional ByVal HC As Color = Nothing, Optional ByVal OC As Color = Nothing)
        Me.Text = text
        AutoSize = False
        Margin = New System.Windows.Forms.Padding(10, 2, -10, 2)

        Size = s
        If HC = Nothing Then
            HC = Color.DeepSkyBlue
        End If
        If OC = Nothing Then
            OC = Color.FromArgb(0, 151, 205)
        End If
        HighlightColor = HC
        MouseOverColor = OC
    End Sub

    Private mEnter As Boolean = False
    Private mDown As Boolean = False

    Private HighlightColor As Color
    Private MouseOverColor As Color

    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        Dim g As Graphics = e.Graphics

        g.FillRectangle(New SolidBrush(Color.Black), 0, 0, Width, Height)

        Dim RGB29 As New Pen(Color.FromArgb(29, 29, 29))
        g.DrawRectangle(RGB29, 0, 0, Width - 1, Height - 1)

        'DrawGradient(Color.FromArgb(124, 255, 255, 255), Color.FromArgb(0, 0, 0, 0), 1, 1, Me.Width, Me.Height / 1.8)

        Dim DrawGradientBrush As LinearGradientBrush = New LinearGradientBrush(New Rectangle(1, 1, Me.Width, Me.Height / 1.8), Color.FromArgb(124, 255, 255, 255), Color.FromArgb(0, 0, 0, 0), 90.0F)
        g.FillRectangle(DrawGradientBrush, New Rectangle(1, 1, Me.Width, Me.Height / 1.8))

        Dim White As New Pen(Color.FromArgb(124, 255, 255, 255))

        Dim path As New GraphicsPath()
        Dim HalfHeight As Integer = Me.Height / 2.2
        Dim HalfWidth As Integer = ((Me.Width * 1.2) - Me.Width) * -1
        Dim EndWidth As Integer = Me.Width * 1.4
        path.AddEllipse(HalfWidth, HalfHeight, EndWidth, Me.Height * 2)
        Dim pthGrBrush As New PathGradientBrush(path)
        pthGrBrush.CenterColor = Color.FromArgb(144, 255, 255, 255)
        Dim colors As Color() = {Color.Transparent}
        pthGrBrush.SurroundColors = colors
        g.FillEllipse(pthGrBrush, HalfWidth, HalfHeight, EndWidth, Me.Height * 2)

        If mEnter Then
            If mDown Then
                g.Clear(Color.Black)
                g.FillRectangle(New SolidBrush(MouseOverColor), 0, 0, Width, Height)

                g.DrawRectangle(RGB29, 0, 0, Width - 1, Height - 1)

                Dim DrawGradientBrush_over As LinearGradientBrush = New LinearGradientBrush(New Rectangle(1, 1, Me.Width, Me.Height / 1.8), Color.FromArgb(124, 255, 255, 255), Color.FromArgb(0, 0, 0, 0), 90.0F)
                g.FillRectangle(DrawGradientBrush_over, New Rectangle(1, 1, Me.Width, Me.Height / 1.8))

                path.AddEllipse(HalfWidth, HalfHeight, EndWidth, Me.Height * 2)
                pthGrBrush.CenterColor = Color.FromArgb(144, 255, 255, 255)
                pthGrBrush.SurroundColors = colors
                g.FillEllipse(pthGrBrush, HalfWidth, HalfHeight, EndWidth, Me.Height * 2)

                g.DrawString(Me.Text, Me.Font, Brushes.White, New Point((Me.Width / 2) - (g.MeasureString(Me.Text, Me.Font).Width / 2), (Me.Height / 2) - (g.MeasureString(Me.Text, Me.Font).Height / 2)))
            Else


                g.Clear(Color.Black)
                g.FillRectangle(New SolidBrush(HighlightColor), 0, 0, Width, Height)

                g.DrawRectangle(RGB29, 0, 0, Width - 1, Height - 1)

                Dim DrawGradientBrush_over As LinearGradientBrush = New LinearGradientBrush(New Rectangle(1, 1, Me.Width, Me.Height / 1.8), Color.FromArgb(124, 255, 255, 255), Color.FromArgb(0, 0, 0, 0), 90.0F)
                g.FillRectangle(DrawGradientBrush_over, New Rectangle(1, 1, Me.Width, Me.Height / 1.8))

                path.AddEllipse(HalfWidth, HalfHeight, EndWidth, Me.Height * 2)
                pthGrBrush.CenterColor = Color.FromArgb(144, 255, 255, 255)
                pthGrBrush.SurroundColors = colors
                g.FillEllipse(pthGrBrush, HalfWidth, HalfHeight, EndWidth, Me.Height * 2)

                g.DrawString(Me.Text, Me.Font, Brushes.White, New Point((Me.Width / 2) - (g.MeasureString(Me.Text, Me.Font).Width / 2), (Me.Height / 2) - (g.MeasureString(Me.Text, Me.Font).Height / 2)))
            End If
        Else
            g.DrawString(Me.Text, Me.Font, Brushes.White, New Point((Me.Width / 2) - (g.MeasureString(Me.Text, Me.Font).Width / 2), (Me.Height / 2) - (g.MeasureString(Me.Text, Me.Font).Height / 2)))
        End If
    End Sub

    Protected Overrides Sub OnMouseEnter(e As EventArgs)
        mEnter = True
        Invalidate()
    End Sub

    Protected Overrides Sub OnMouseLeave(e As EventArgs)
        mEnter = False
        Invalidate()
    End Sub

    Protected Overrides Sub OnMouseUp(e As MouseEventArgs)
        mDown = False
        Invalidate()
    End Sub

    Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
        mDown = True
        Invalidate()
    End Sub

#End Region

End Class





Public Class SpaceServerItem
    Inherits Panel

#Region "Server Item"

    Sub New()
        Text = "Body text"
        DoubleBuffered = True 'reduce flickering while resizing
    End Sub

    Private _Value As Color
    Property BorderColour() As Color
        Get
            Return _Value
        End Get
        Set(ByVal value As Color)
            _Value = value
            Invalidate()
        End Set
    End Property

    Private _Activated As Boolean = False
    Property Activated() As Boolean
        Get
            Return _Activated
        End Get
        Set(ByVal value As Boolean)
            _Activated = value
            Invalidate()
        End Set
    End Property

    Private _Title As String = "Title"
    Property Title() As String
        Get
            Return _Title
        End Get
        Set(ByVal value As String)
            _Title = value
            Invalidate()
        End Set
    End Property

    Private _Image As Image
    Property ServerImage() As Image
        Get
            Return _Image
        End Get
        Set(ByVal value As Image)
            _Image = value
            Invalidate()
        End Set
    End Property

    Private _ClientMods As String
    Property ClientMods() As String
        Get
            Return _ClientMods
        End Get
        Set(ByVal value As String)
            _ClientMods = value
            Invalidate()
        End Set
    End Property

    Private _Players As Integer
    Property Players() As Integer
        Get
            Return _Players
        End Get
        Set(ByVal value As Integer)
            _Players = value
            Invalidate()
        End Set
    End Property

    Private _MaxPlayers As Integer
    Property MaxPlayers() As Integer
        Get
            Return _MaxPlayers
        End Get
        Set(ByVal value As Integer)
            _MaxPlayers = value
            Invalidate()
        End Set
    End Property

    Private _Status As String
    Property Status() As String
        Get
            Return _Status
        End Get
        Set(ByVal value As String)
            _Status = value
            Invalidate()
        End Set
    End Property

    Private _GameType As String
    Property GameType() As String
        Get
            Return _GameType
        End Get
        Set(ByVal value As String)
            _GameType = value
            Invalidate()
        End Set
    End Property

    Private _ServerIP As String
    Property ServerIP() As String
        Get
            Return _ServerIP
        End Get
        Set(ByVal value As String)
            _ServerIP = value
            Invalidate()
        End Set
    End Property

    Private _ServerPort As Integer
    Property ServerPort() As Integer
        Get
            Return _ServerPort
        End Get
        Set(ByVal value As Integer)
            _ServerPort = value
            Invalidate()
        End Set
    End Property

    Private _Password As Integer
    Property Password() As Integer
        Get
            Return _Password
        End Get
        Set(ByVal value As Integer)
            _Password = value
            Invalidate()
        End Set
    End Property

    Private _ArmaVersion As String
    Property ArmaVersion() As String
        Get
            Return _ArmaVersion
        End Get
        Set(ByVal value As String)
            _ArmaVersion = value
            Invalidate()
        End Set
    End Property

    Protected Overrides Sub SetBoundsCore(x As Integer, y As Integer, width As Integer, height As Integer, specified As BoundsSpecified)
        If (specified And BoundsSpecified.Height) = 0 OrElse height = 105 Then
            MyBase.SetBoundsCore(x, y, width, 105, specified)
        Else
            Return
        End If
    End Sub


    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        Dim G As Graphics = e.Graphics

        If Me.DesignMode Then
            SetBoundsCore(Me.Location.X, Me.Location.Y, Me.Width, 105, 105)
        End If

        G.Clear(Color.FromArgb(14, 14, 14))

        G.FillRectangle(New SolidBrush(Color.FromArgb(20, 20, 20)), New Rectangle(1, 1, Width - 2, Height - 2))

        Dim RGB32 As New Pen(Color.FromArgb(32, 32, 32))
        G.DrawLine(RGB32, 2, 1, Width - 1, 1)
        G.DrawLine(RGB32, 1, 1, 1, Height - 1)
        G.DrawLine(RGB32, 2, Height - 2, Width - 2, Height - 2)
        G.DrawLine(RGB32, Width - 2, Height - 3, Width - 2, 1)

        Dim RGB0 As Pen
        If _Activated Then
            RGB0 = New Pen(_Value)
        Else
            RGB0 = New Pen(Color.FromArgb(0, 0, 0))
        End If
        G.DrawLine(RGB0, 0, 0, Width - 1, 0)
        G.DrawLine(RGB0, 0, Height - 1, 0, 1)
        G.DrawLine(RGB0, 1, Height - 1, Width - 1, Height - 1)
        G.DrawLine(RGB0, Width - 1, Height - 1, Width - 1, 1)

        If Not _Image Is Nothing Then
            If Not _Image.Width = 79 And Not _Image.Height = 76 Then
                G.DrawString("Error: Size must be 79x76.", Me.Font, Brushes.Red, 13, 14)
            Else
                G.DrawImage(_Image, New Point(13, 14))
            End If
        Else
            G.DrawString("Error: No image avalible.", Me.Font, Brushes.Red, 13, 14)
        End If

        G.DrawRectangle(Pens.Gray, New Rectangle(13, 14, 79, 76))

        Dim SF As New StringFormat
        G.DrawString(_Title, New Font(Me.Font, FontStyle.Bold), Brushes.White, New Rectangle(98, 16, Me.Width - 98, Me.Height), SF)
        G.DrawString(Text, New Font(Me.Font, FontStyle.Regular), Brushes.Gray, New Rectangle(98, 30, Me.Width - 98 - 7, Me.Height - 29 - 21), SF)

        'Body region bounds
        'G.DrawRectangle(Pens.Beige, New Rectangle(98, 30, Me.Width - 98 - 7, Me.Height - 29 - 21))

        Dim statusBrush As Brush = Brushes.White
        If Status.ToLower = "online" Then
            statusBrush = Brushes.Lime
        End If
        If Status.ToLower = "offline" Then
            statusBrush = Brushes.Red
        End If
        If Status.ToLower = "restarting" Then
            statusBrush = Brushes.Orange
        End If

        'Server Status
        G.DrawString("Status: ", New Font(Me.Font, FontStyle.Regular), Brushes.White, New Rectangle(98, Me.Height - 20, Me.Width - 98, Me.Height), SF)
        G.DrawString(Status, New Font(Me.Font, FontStyle.Regular), statusBrush, New Rectangle(98 + G.MeasureString("Status: ", Me.Font).Width, Me.Height - 20, Me.Width - 98, Me.Height), SF)


        Dim playersBrush As Brush = Brushes.White
        If Players / MaxPlayers < 0.5 Then
            playersBrush = Brushes.Lime
        End If
        If Players / MaxPlayers >= 0.5 Then
            playersBrush = Brushes.Orange
        End If
        If Players / MaxPlayers = 1 Then
            playersBrush = Brushes.Red
        End If

            'Server player count
        G.DrawString("Players: ", New Font(Me.Font, FontStyle.Regular), Brushes.White, New Rectangle(98 + 102, Me.Height - 20, Me.Width - 98, Me.Height), SF)
        G.DrawString(Players & "/" & MaxPlayers, New Font(Me.Font, FontStyle.Regular), playersBrush, New Rectangle(98 + 102 + G.MeasureString("Players: ", Me.Font).Width, Me.Height - 20, Me.Width - 98, Me.Height), SF)

    End Sub

#End Region

End Class




Class SpaceScrollBar
    Inherits Panel

#Region "ScrollBar"

    Sub New()
        DoubleBuffered = True
    End Sub

    Private _value As Integer
    Public Property Value() As Integer
        Get
            Return _value
        End Get
        Set(ByVal v As Integer)
            _value = v
            Invalidate()
        End Set
    End Property

    Private mEnter As Boolean = False
    Private mDown As Boolean = False

    Private topButton_hover As Boolean = False
    Private bottomButton_hover As Boolean = False

    Protected Overrides Sub OnMouseMove(e As MouseEventArgs)
        mEnter = True
        If New Rectangle(0, 0, Width - 1, 20).Contains(e.Location) Then
            topButton_hover = True
        Else
            topButton_hover = False
        End If
        Invalidate()
    End Sub

    Protected Overrides Sub OnMouseLeave(e As EventArgs)
        mEnter = False
        Invalidate()
    End Sub

    Protected Overrides Sub OnMouseUp(e As MouseEventArgs)
        mDown = False
        Invalidate()
    End Sub

    Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
        mDown = True
        Invalidate()
    End Sub


    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        Dim g As Graphics = e.Graphics
        'g.DrawRectangle(Pens.Red, New Rectangle(0, 0, Width - 1, 20))



        g.FillRectangle(New SolidBrush(Color.Black), 0, 0, Width - 1, 20)

        Dim RGB29 As New Pen(Color.FromArgb(29, 29, 29))
        g.DrawRectangle(RGB29, 0, 0, Width - 1, 20)

        'DrawGradient(Color.FromArgb(124, 255, 255, 255), Color.FromArgb(0, 0, 0, 0), 1, 1, Me.Width, Me.Height / 1.8)

        Dim DrawGradientBrush As LinearGradientBrush = New LinearGradientBrush(New Rectangle(1, 1, Width - 1, 20 / 1.8), Color.FromArgb(124, 255, 255, 255), Color.FromArgb(0, 0, 0, 0), 90.0F)
        g.FillRectangle(DrawGradientBrush, New Rectangle(1, 1, Width - 1, 20 / 1.8))

        Dim White As New Pen(Color.FromArgb(124, 255, 255, 255))

        Dim path As New GraphicsPath()
        Dim HalfHeight As Integer = 1 / 2.2
        Dim HalfWidth As Integer = ((Me.Width * 1.2) - Me.Width) * -1
        Dim EndWidth As Integer = Me.Width * 1.4
        path.AddEllipse(HalfWidth, HalfHeight, EndWidth, 20 * 1)
        Dim pthGrBrush As New PathGradientBrush(path)
        pthGrBrush.CenterColor = Color.FromArgb(144, 255, 255, 255)
        Dim colors As Color() = {Color.Transparent}
        pthGrBrush.SurroundColors = colors
        g.FillEllipse(pthGrBrush, HalfWidth, HalfHeight, EndWidth, 20 * 2)




        g.DrawRectangle(Pens.BlueViolet, New Rectangle(0, (((Height - 1) / 100) * Value) + 21, Width - 1, Height - 43))

        g.DrawRectangle(Pens.Red, New Rectangle(0, Height - 21, Width - 1, 20))






        'MyBase.OnPaint(e)
    End Sub

#End Region

End Class


Public Class ServerTreeNode
    Inherits TreeNode

#Region "ServerTreeNode"

    Private _DownloadLink As String
    Property DownloadLink() As String
        Get
            Return _DownloadLink
        End Get
        Set(ByVal value As String)
            _DownloadLink = value
        End Set
    End Property

    Private _Downloadable As Boolean = False
    Property Downloadable() As Boolean
        Get
            Return _Downloadable
        End Get
        Set(ByVal value As Boolean)
            _Downloadable = value
        End Set
    End Property

    Private _Complete As Boolean = False
    Property Complete() As Boolean
        Get
            Return _Complete
        End Get
        Set(ByVal value As Boolean)
            _Complete = value
        End Set
    End Property

    Private _ModName As String
    Property ModName() As String
        Get
            Return _ModName
        End Get
        Set(ByVal value As String)
            _ModName = value
        End Set
    End Property

    Private _Downloading As Boolean = False
    Property Downloading() As Boolean
        Get
            Return _Downloading
        End Get
        Set(ByVal value As Boolean)
            _Downloading = value
        End Set
    End Property

    Private _RawMod As String = ""
    Property RawMod() As String
        Get
            Return _RawMod
        End Get
        Set(ByVal value As String)
            _RawMod = value
        End Set
    End Property

    Private _IsMod As Boolean = False
    Property IsMod() As Boolean
        Get
            Return _IsMod
        End Get
        Set(ByVal value As Boolean)
            _IsMod = value
        End Set
    End Property

    Private _ArmaVersion As Integer
    Property ArmaVersion() As Integer
        Get
            Return _ArmaVersion
        End Get
        Set(ByVal value As Integer)
            _ArmaVersion = value
        End Set
    End Property

#End Region

End Class



Class DirectionalButton
    Inherits Panel

#Region "DirectionalButton"

    Sub New()
        DoubleBuffered = True 'reduce flickering while resizing
    End Sub

    Enum DirectionType
        Up
        Down
    End Enum

    Private _mEnter As Boolean = False

    Private _Direction As DirectionType = DirectionType.Down
    Property Direction() As DirectionType
        Get
            Return _Direction
        End Get
        Set(ByVal value As DirectionType)
            _Direction = value
        End Set
    End Property

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        Dim g As Graphics = e.Graphics
        g.Clear(Me.BackColor)


        'Dim path As New GraphicsPath()
        'Dim HalfHeight As Integer = Me.Height / 5
        'Dim HalfWidth As Integer = ((Me.Width * 1.2) - Me.Width) * -1
        'Dim EndWidth As Integer = Me.Width * 1.4
        'path.AddEllipse(HalfWidth, HalfHeight, EndWidth, Me.Height * 2)
        'Dim pthGrBrush As New PathGradientBrush(path)
        'pthGrBrush.CenterColor = Color.FromArgb(144, 255, 255, 255)
        'Dim colors As Color() = {Color.Transparent}
        'pthGrBrush.SurroundColors = colors
        'g.FillEllipse(pthGrBrush, HalfWidth, HalfHeight, EndWidth, Me.Height * 2)

        Dim col As Pen
        If _mEnter Then
            col = Pens.DeepSkyBlue
        Else
            col = Pens.White
        End If

        If _Direction = DirectionType.Down Then
            g.DrawLine(col, New Point((Me.Width / 2) - 10, (Me.Height / 2) - 5), New Point((Me.Width / 2), (Me.Height / 2) + 5))
            g.DrawLine(col, New Point((Me.Width / 2), (Me.Height / 2) + 5), New Point((Me.Width / 2) + 10, (Me.Height / 2) - 5))
        Else
            g.DrawLine(col, New Point((Me.Width / 2) - 10, (Me.Height / 2) + 5), New Point((Me.Width / 2), (Me.Height / 2) - 5))
            g.DrawLine(col, New Point((Me.Width / 2), (Me.Height / 2) - 5), New Point((Me.Width / 2) + 10, (Me.Height / 2) + 5))
        End If

    End Sub

    Protected Overrides Sub OnMouseEnter(e As EventArgs)
        _mEnter = True
        Invalidate()
    End Sub

    Protected Overrides Sub OnMouseLeave(e As EventArgs)
        _mEnter = False
        Invalidate()
    End Sub

#End Region

End Class

Class SpaceListBox
    Inherits ListBox

#Region "ListBox"

    Sub New()
        DoubleBuffered = True 'reduce flickering while resizing
    End Sub

#End Region

End Class



Class SpaceKillboard
    Inherits Panel

#Region "KillBoard"

    Sub New()
        DoubleBuffered = True
        'Items.Add("HELLO")
        'Items.Add("<col=&H780000FF>HELLO")
    End Sub

    Private _text As New ArrayList
    Property Items() As ArrayList
        Get
            Return _text
        End Get
        Set(value As ArrayList)
            _text = value
            Invalidate()
        End Set
    End Property

    Private _vScrollVal As Integer
    Property VScrollValue() As Integer
        Get
            Return _vScrollVal
        End Get
        Set(ByVal value As Integer)
            _vScrollVal = value
            Invalidate()
        End Set
    End Property

    Public renderTotal As Integer

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)
        Dim G As Graphics = e.Graphics
        G.Clear(Color.FromArgb(14, 14, 14))

        G.FillRectangle(New SolidBrush(Color.FromArgb(20, 20, 20)), 1, 1, Width - 2, Height - 2)

        Dim RGB32 As New Pen(Color.FromArgb(32, 32, 32))
        G.DrawLine(RGB32, 2, 1, Width - 1, 1)
        G.DrawLine(RGB32, 1, 1, 1, Height - 1)
        G.DrawLine(RGB32, 2, Height - 2, Width - 2, Height - 2)
        G.DrawLine(RGB32, Width - 2, Height - 3, Width - 2, 1)

        Dim RGB0 As New Pen(Color.FromArgb(0, 0, 0))
        G.DrawLine(RGB0, 0, 0, Width - 1, 0)
        G.DrawLine(RGB0, 0, Height - 1, 0, 1)
        G.DrawLine(RGB0, 1, Height - 1, Width - 1, Height - 1)
        G.DrawLine(RGB0, Width - 1, Height - 1, Width - 1, 1)

        Me.Controls.Clear()
        Dim i As Integer = 0
        Dim padding As Integer = 5
        Dim linespacing As Integer = 2

        renderTotal = Height / (G.MeasureString("test", Me.Font).Height + linespacing)


        For itemIndex As Integer = VScrollValue To renderTotal + VScrollValue
            If VScrollValue < 0 Then
                itemIndex = 0
                VScrollValue = 0
            End If
            If Not itemIndex >= Items.Count Then
                Dim SF As New StringFormat
                If Items(itemIndex).ToString.StartsWith("<col=") Then
                    Dim line As String = Items(itemIndex)
                    'G.DrawRectangle(Pens.Blue, New Rectangle(padding, (((i) * (G.MeasureString(Items(itemIndex), Me.Font).Height + linespacing)) + padding), Me.Width - 7, G.MeasureString("A", Me.Font).Height)) 'G.MeasureString(Items(itemIndex), Me.Font).Height
                    G.DrawString(line.Replace("<col=" & GetColorData(Items(itemIndex)) & ">", ""), Me.Font, New SolidBrush(parseLineColour(Items(itemIndex))), New Rectangle(padding, (((i) * (G.MeasureString("A", Me.Font).Height + linespacing)) + padding), Me.Width - 7, G.MeasureString(Items(itemIndex), Me.Font).Height), SF)
                Else
                    'G.DrawRectangle(Pens.Blue, New Rectangle(padding, (((i) * (G.MeasureString("A", Me.Font).Height + linespacing)) + padding), Me.Width - 7, G.MeasureString("A", Me.Font).Height)) 'G.MeasureString(Items(itemIndex), Me.Font).Height
                    G.DrawString(Items(itemIndex), Me.Font, Brushes.White, New Rectangle(padding, (((i) * (G.MeasureString("A", Me.Font).Height + linespacing)) + padding), Me.Width - 7, G.MeasureString("A", Me.Font).Height), SF) 'G.MeasureString(Items(itemIndex), Me.Font).Height
                End If
            End If
            i += 1
        Next
    End Sub

    Function parseLineColour(ByVal rawdata As String) As Color 'TODO
        Try
            Dim line As String() = rawdata.Split(New String() {"<col="}, StringSplitOptions.None)
            Dim line2 As String() = line(1).Split(New String() {">"}, StringSplitOptions.None)
            Dim codes As String() = line2(0).Split(New String() {","}, StringSplitOptions.None)
            Dim val As Color = Color.FromArgb(codes(0), codes(1), codes(2))
            Return val
        Catch ex As Exception
            Dim val As Color = Color.FromArgb(255, 255, 255)
            Return val
        End Try
    End Function

    Function GetColorData(ByVal rawdata As String) As String 'TODO
        Try
            Dim line As String() = rawdata.Split(New String() {"<col="}, StringSplitOptions.None)
            Dim line2 As String() = line(1).Split(New String() {">"}, StringSplitOptions.None)
            Return line2(0)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Protected Overrides Sub OnInvalidated(e As InvalidateEventArgs)
        MyBase.OnInvalidated(e)
    End Sub
#End Region

End Class