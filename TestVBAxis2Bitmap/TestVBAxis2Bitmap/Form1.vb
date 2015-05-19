Public Class Form1

    Dim bmp0 As Bitmap = Nothing
    Public imgOriginalSize(2) As Integer
    Public ZoomFactor As Double = 100

    Public listLabelX, listLabelY As New List(Of Label)
    Public listTickV, listTickH As New List(Of PictureBox)
    Public picMaxX, picMaxY, picHoverX, picHoverY As New PictureBox
    Public picRubberTop, picRubberBottom, picRubberLeft, picRubberRight As New PictureBox
    Public lblHoverX, lblHoverY As New Label

    Public dX As Integer = 1
    Public dY As Double = 0.033
    Public minX As Integer = 15
    Public maxX As Integer = 1000 + dX
    Public minY As Double = 0
    Public maxY As Double = 5 ' dY

    Public dLabelX As Integer = 50
    Public dLabelY As Integer = 50

    ' public variable for panning
    Public fPan As Boolean = False
    Public panStartMouse As Point
    Public panStartHScroll, panStartVScroll As Integer

    'public variable for axis scaling
    Public fScaleX As Boolean = False
    Public fScaleY As Boolean = False
    Public scaleMouse0 As Point

    Public Sub Image_Axis_Initialize(ByVal picbox As PictureBox, ByVal panelX As Panel, ByVal panelY As Panel, ByVal panelImage As Panel, ByVal panelGap As Panel,
                                     ByVal listLabelX As List(Of Label), ByVal listLabelY As List(Of Label),
                                     ByVal listTickV As List(Of PictureBox), ByVal listTickH As List(Of PictureBox),
                                     ByVal minX As Double, ByVal maxX As Double, ByVal minY As Double, ByVal maxY As Double)

        listLabelX.Clear() : listTickV.Clear() ': listGridV.Clear()
        listLabelX.Clear() : listTickH.Clear() ': listGridH.Clear()

        ' create list of labels
        For i As Integer = 0 To Math.Floor(imgOriginalSize(0) / dLabelX) '- 1
            listLabelX.Add(New Label)
            listTickV.Add(New PictureBox)
        Next

        'Dim lastleft As Integer = 0
        For i As Integer = 0 To Math.Floor(imgOriginalSize(1) / dLabelY) - 1
            listLabelY.Add(New Label)
            listTickH.Add(New PictureBox)
        Next

        ' auto-adjust Y-axis panel width based on last Y-value label position
        Dim dummy As New Label
        With dummy
            .Parent = panelY : .AutoSize = True
            .Text = Math.Round(minY + ((listLabelY.Count - 1) * ((maxY - minY) / imgOriginalSize(1))), 3)
            .Left = 5 : .Hide()

            panelY.Parent.Width = .Left + .Width + 15
        End With
        dummy.Dispose()

        ' show X-axis label and tick
        For i As Integer = 0 To listLabelX.Count - 1
            Dim val0 As Double = minX + ((i * dLabelX) * ((maxX - minX) / picbox.Width))

            With listTickV.Item(i)
                .Parent = panelX
                .BackColor = Color.Black : .Height = 10 : .Width = 1
                .Top = panelX.Height - .Height : .Left = (i * dLabelX) + panelY.Width
                .Show() : .Update()
            End With

            With listLabelX.Item(i)
                .Parent = panelX : .AutoSize = True
                .Text = Math.Floor(val0)
                .Top = 10 : .Left = Math.Round(listTickV.Item(i).Left - (.Width / 2))
                .Show() : .Update()
            End With
        Next

        ' show Y-axis label and tick
        For i As Integer = 0 To listLabelY.Count - 1
            With listTickH.Item(i)
                .Parent = panelY
                .BackColor = Color.Black : .Height = 1 : .Width = 10
                .Top = (i * dLabelY) + panelGap.Height
                .Left = panelY.Width - .Width
                .Show() : .Update()
            End With

            With listLabelY.Item(i)
                .Parent = panelY : .AutoSize = True
                .Text = Math.Round(minY + (i * ((maxY - minY) / picbox.Height)), 3)
                .Left = listTickH.Item(i).Left - .Width
                .Top = Math.Round(listTickH.Item(i).Top - (.Height / 2))
                .Show() : .Update()
            End With
        Next

        ' show maximum axis indicator
        Dim pixPtrace As Integer = Math.Floor(picbox.Width / (maxX - minX))
        If pixPtrace < 1 Then pixPtrace = 1

        With picMaxX
            .Parent = panelX : .BringToFront()
            .BackColor = Color.Red
            .Height = panelX.Height : .Width = pixPtrace
            .Top = 0
            .Left = imgOriginalSize(0) - pixPtrace + picbox.Left + 1 + panelY.Width
            .Show()
            .Update()
        End With

        With picMaxY
            .Parent = panelY : .BringToFront()
            .BackColor = Color.Red
            .Height = 1 : .Width = panelY.Width
            .Top = (imgOriginalSize(1) - 1 + picbox.Top) + panelGap.Height
            .Left = 0
            .Show()
            .Update()
        End With

        ' prepare value-on-hover
        With picHoverX
            .Parent = panelX : .BringToFront()
            .BackColor = Color.Green
            .Height = panelX.Height : .Width = 1
            .Top = 0 : .Left = -1
            .Show() : .Update()
        End With

        With lblHoverX
            .Parent = panelX : .BringToFront() : .AutoSize = True
            .BackColor = Color.Green : .ForeColor = Color.White
            .Top = 10
        End With

        With picHoverY
            .Parent = panelY : .BringToFront()
            .BackColor = Color.Green
            .Height = 1 : .Width = panelY.Width
            .Top = -1 : .Left = 0
            .Show() : .Update()
        End With

        With lblHoverY
            .Parent = panelY : .BringToFront() : .AutoSize = True
            .BackColor = Color.Green : .ForeColor = Color.White
        End With

        ' prepare grid lines
        Dim gv As New Bitmap(1, 6)
        Dim ggv As Graphics = Graphics.FromImage(gv)
        ggv.DrawLine(Pens.Black, 0, 0, 0, 2)
        ggv.DrawLine(Pens.White, 0, 3, 0, 5)
        ggv.Dispose()

        Dim gh As New Bitmap(6, 1)
        Dim ggh As Graphics = Graphics.FromImage(gh)
        ggh.DrawLine(Pens.Black, 0, 0, 2, 0)
        ggh.DrawLine(Pens.White, 3, 0, 5, 0)
        ggh.Dispose()

    End Sub

    Public Sub Image_Axis_Update(ByVal picbox As PictureBox, ByVal panelX As Panel, ByVal panelY As Panel, ByVal panelImage As Panel, ByVal panelGap As Panel,
                                 ByVal listLabelX As List(Of Label), ByVal listLabelY As List(Of Label),
                                 ByVal listTickV As List(Of PictureBox), ByVal listTickH As List(Of PictureBox))

        ' move maximum X-axis and Y-axis indicator
        Dim pixPtrace As Integer = Math.Floor(picbox.Width / (maxX - minX))
        If pixPtrace < 1 Then pixPtrace = 1

        With picMaxX
            .Width = pixPtrace
            .Left = picbox.Width - pixPtrace + picbox.Left + 1 + panelY.Width
            .Update()
        End With

        With picMaxY
            .Top = (picbox.Height - 1 + picbox.Top) + panelGap.Height
            .Update()
        End With

        ' update X-axis label
        For i As Integer = 0 To listLabelX.Count - 1
            Dim val0 As Double = minX + (((i * dLabelX) - picbox.Left) * ((maxX - minX) / picbox.Width))

            With listTickV.Item(i)
                .Height = 10 : .Width = 1
                If .Left >= picMaxX.Left Then .BackColor = Color.Red Else .BackColor = Color.Black
                .Update()
            End With

            With listLabelX.Item(i)
                .Text = Math.Floor(val0)
                .Left = Math.Round((listTickV.Item(i).Left + listTickV.Item(i).Width / 2) - (.Width / 2))
                .ForeColor = listTickV.Item(i).BackColor
                .Update()
            End With
        Next

        ' update Y-axis label
        For i As Integer = 0 To listLabelY.Count - 1
            Dim val0 As Double = minY + (((i * dLabelY) - picbox.Top) * ((maxY - minY) / picbox.Height))

            With listTickH.Item(i)
                .Height = 1 : .Width = 10
                If .Top >= picMaxY.Top Then .BackColor = Color.Red Else .BackColor = Color.Black
                .Update()
            End With

            With listLabelY.Item(i)
                .Text = Math.Round(val0, 3)
                .Left = listTickH.Item(i).Left - .Width
                .ForeColor = listTickH.Item(i).BackColor
                .Update()
            End With
        Next

        panelImage.Invalidate()
        panelImage.Update()

    End Sub

    Public Sub Image_Axis_StretchShrink(ByVal picbox As PictureBox, panelX As Panel, ByVal panelY As Panel, ByVal panelImage As Panel)
        ' for X-axis stretch/shrink
        If fScaleX = True Then
            Dim scaleMouse1 As New Point(panelX.PointToClient(MousePosition))

            ' translate mouse shift to the amount of size change
            Dim scl0 As Double = (scaleMouse1.X - scaleMouse0.X) * 3
            imgOriginalSize(0) = imgOriginalSize(0) + scl0

            ' resize in X-direction
            picbox.Width = imgOriginalSize(0)
            picbox.Update()

            scaleMouse0 = scaleMouse1
        End If

        ' for Y-axis stretch/shrink
        If fScaleY = True Then
            Dim scaleMouse1 As New Point(panelY.PointToClient(MousePosition))

            ' translate mouse shift to the amount of size change
            Dim scl1 As Double = (scaleMouse1.Y - scaleMouse0.Y) * 3
            imgOriginalSize(1) = imgOriginalSize(1) + scl1

            ' resize in Y-direction
            picbox.Height = imgOriginalSize(1)
            picbox.Update()

            scaleMouse0 = scaleMouse1
        End If

        panelImage.Invalidate()
        panelImage.Update()

    End Sub

    Public Sub Image_Axis_SetToDefault(ByVal picbox As PictureBox, ByVal panelImage As Panel)
        imgOriginalSize(0) = picbox.Image.Width
        imgOriginalSize(1) = picbox.Image.Height

        With picbox
            .SizeMode = PictureBoxSizeMode.StretchImage
            .Width = imgOriginalSize(0)
            .Height = imgOriginalSize(1)
            .Update()
        End With

        panelImage.HorizontalScroll.Value = 0
        panelImage.VerticalScroll.Value = 0
        panelImage.Update()

    End Sub

    Public Sub Image_ValueOnHover(ByVal panelGap As Panel, ByVal panelY As Panel, ByVal picbox As PictureBox,
                                  ByVal minX As Double, ByVal maxX As Double, ByVal minY As Double, ByVal maxY As Double)

        ' calculate pointer in resized image
        Dim scaled As Point
        scaled.X = (imgOriginalSize(0) * picbox.PointToClient(MousePosition).X / picbox.Width)
        scaled.Y = (imgOriginalSize(1) * picbox.PointToClient(MousePosition).Y / picbox.Height)

        ' scale mouse position to unscaled axis and calculate selected trace
        Dim unscTrace As Double = minX + (scaled.X * (maxX - minX) / imgOriginalSize(0))
        Dim unscFloor As Integer = Math.Floor(unscTrace)
        Dim unscCeiln As Integer
        If Math.Ceiling(unscTrace) = unscFloor Then unscCeiln = unscFloor + 1 Else unscCeiln = Math.Ceiling(unscTrace)

        ' translate selected trace back to its pixel position
        Dim pixlFloor As Double = (unscFloor - minX) * imgOriginalSize(0) / (maxX - minX)
        Dim pixlCeiln As Double = (unscCeiln - minX) * imgOriginalSize(0) / (maxX - minX)
        Dim pixpTrace As Double = pixlCeiln - pixlFloor

        ' scale pixel position to current image size
        Dim scalFloor As Double = pixlFloor * picbox.Width / imgOriginalSize(0)
        Dim scalCeiln As Double = pixlCeiln * picbox.Width / imgOriginalSize(0)
        Dim scalPxTrc As Double = scalCeiln - scalFloor
        If scalPxTrc < 1 Then scalPxTrc = 1

        With picHoverX
            .Width = Math.Round(scalPxTrc)
            .Left = Math.Round(scalFloor) + 1 + panelY.Width + picbox.Left
            .Invalidate() : Update()
        End With

        With lblHoverX
            .Text = unscFloor
            .Left = (picHoverX.Left + picHoverX.Width / 2) - (.Width / 2)
            .Invalidate() : Update()
        End With

        With picHoverY
            .Top = picbox.PointToClient(MousePosition).Y + panelGap.Height + picbox.Top
            .Invalidate()
        End With

        With lblHoverY
            .Text = Math.Round(minY + (scaled.Y * ((maxY - minY) / imgOriginalSize(1))), 3)
            .Top = Math.Round(picbox.PointToClient(MousePosition).Y + panelGap.Height + picbox.Top - .Height / 2)
            .Left = panelY.Width - .Width - 10
            .Invalidate()
        End With

    End Sub

    Public Sub Image_Pan(ByVal panelImage As Panel)
        Dim panEndMouse As Point = panelImage.PointToClient(MousePosition)
        Dim panShiftMouse As Point = panEndMouse - panStartMouse

        Dim panEndHScroll As Integer = panStartHScroll - panShiftMouse.X
        If panEndHScroll <= 0 Then panEndHScroll = 0
        If panEndHScroll >= panelImage.HorizontalScroll.Maximum Then panEndHScroll = panelImage.HorizontalScroll.Maximum

        Dim panEndVScroll As Integer = panStartVScroll - panShiftMouse.Y
        If panEndVScroll <= 0 Then panEndVScroll = 0
        If panEndVScroll >= panelImage.VerticalScroll.Maximum Then panEndVScroll = panelImage.VerticalScroll.Maximum

        If panelImage.HorizontalScroll.Visible = True Then panelImage.HorizontalScroll.Value = panEndHScroll
        If panelImage.VerticalScroll.Visible = True Then panelImage.VerticalScroll.Value = panEndVScroll

        panelImage.Invalidate()
        panelImage.Update()
    End Sub

    Public Sub Image_MouseCenteredZoom(ByVal zoomfactor As Integer, ByVal picbox As PictureBox, ByVal panelImage As Panel)

        ' save initial parameter for mouse centered zoom
        'Dim picStart As New Point(picbox.PointToClient(MousePosition))
        Dim picSizeStart As New Point(picbox.Width, picbox.Height)

        ' zoom image
        Dim picNewSize As New Point(imgOriginalSize(0) * (zoomfactor / 100), imgOriginalSize(1) * (zoomfactor / 100))
        With picbox
            .SizeMode = PictureBoxSizeMode.StretchImage
            .Width = picNewSize.X
            .Height = picNewSize.Y
        End With

        panelImage.Invalidate()

        ' mouse centered zoom
        Dim picPosShift As New Point(picStart.X - (picStart.X / picSizeStart.X) * picNewSize.X, picStart.Y - (picStart.Y / picSizeStart.Y) * picNewSize.Y)
        Dim picPosEnd As New Point(picbox.Left + picPosShift.X, picbox.Top + picPosShift.Y)

        Dim panEndHScroll As Integer = -picPosEnd.X
        If panEndHScroll <= 0 Then panEndHScroll = 0
        If panEndHScroll >= Panel5.HorizontalScroll.Maximum Then panEndHScroll = Panel5.HorizontalScroll.Maximum

        Dim panEndVScroll As Integer = -picPosEnd.Y
        If panEndVScroll <= 0 Then panEndVScroll = 0
        If panEndVScroll >= Panel5.VerticalScroll.Maximum Then panEndVScroll = Panel5.VerticalScroll.Maximum

        panelImage.HorizontalScroll.Value = panEndHScroll
        panelImage.VerticalScroll.Value = panEndVScroll

        panelImage.Update()

    End Sub

#Region "PICBOX INTERACTION"

    Private Sub picBox_MouseDown(sender As Object, e As MouseEventArgs) Handles picBox.MouseDown
        ' if left mouse is clicked, activate pan mode
        If MouseButtons = Windows.Forms.MouseButtons.Left Then
            fPan = True
            panStartMouse = Panel5.PointToClient(MousePosition)

            If Panel5.HorizontalScroll.Visible = True Then panStartHScroll = Panel5.HorizontalScroll.Value
            If Panel5.VerticalScroll.Visible = True Then panStartVScroll = Panel5.VerticalScroll.Value
        End If
    End Sub

    Private Sub picBox_MouseMove(sender As Object, e As MouseEventArgs) Handles picBox.MouseMove
        picBox.Focus()
        Image_ValueOnHover(Panel7, Panel2, picBox, minX, maxX, minY, maxY)

        If fPan = True Then
            Image_Pan(Panel5)
            Image_Axis_Update(picBox, Panel4, Panel2, Panel5, Panel7, listLabelX, listLabelY, listTickV, listTickH)
        End If

    End Sub

    Private Sub picBox_MouseUp(sender As Object, e As MouseEventArgs) Handles picBox.MouseUp
        fPan = False
    End Sub

    Private Sub picBox_MouseWheel(sender As Object, e As MouseEventArgs) Handles picBox.MouseWheel

        ZoomFactor = ZoomFactor + 10 * (e.Delta / 120)
        If ZoomFactor < 10 Then ZoomFactor = 10
        If ZoomFactor > 500 Then ZoomFactor = 500

        Image_MouseCenteredZoom(ZoomFactor, picBox, Panel5)
        Image_Axis_Update(picBox, Panel4, Panel2, Panel5, Panel7, listLabelX, listLabelY, listTickV, listTickH)

        'Me.Text = ZoomFactor

    End Sub
#End Region

#Region "PANEL-X MOUSE OPERATION"
    Private Sub Panel4_MouseDown(sender As Object, e As MouseEventArgs) Handles Panel4.MouseDown
        If MouseButtons = Windows.Forms.MouseButtons.Left Then
            fScaleX = True
            scaleMouse0 = Panel4.PointToClient(MousePosition)
        End If
    End Sub

    Private Sub Panel4_MouseMove(sender As Object, e As MouseEventArgs) Handles Panel4.MouseMove
        Image_Axis_StretchShrink(picBox, Panel4, Panel2, Panel5)
        Image_Axis_Update(picBox, Panel4, Panel2, Panel5, Panel7, listLabelX, listLabelY, listTickV, listTickH)
    End Sub

    Private Sub Panel4_MouseUp(sender As Object, e As MouseEventArgs) Handles Panel4.MouseUp
        fScaleX = False
    End Sub
#End Region

#Region "PANEL-Y MOUSE OPERATION"
    Private Sub Panel2_MouseDown(sender As Object, e As MouseEventArgs) Handles Panel2.MouseDown
        If MouseButtons = Windows.Forms.MouseButtons.Left Then
            fScaleY = True
            scaleMouse0 = Panel2.PointToClient(MousePosition)
        End If
    End Sub

    Private Sub Panel2_MouseMove(sender As Object, e As MouseEventArgs) Handles Panel2.MouseMove
        Image_Axis_StretchShrink(picBox, Panel4, Panel2, Panel5)
        Image_Axis_Update(picBox, Panel4, Panel2, Panel5, Panel7, listLabelX, listLabelY, listTickV, listTickH)
    End Sub

    Private Sub Panel2_MouseUp(sender As Object, e As MouseEventArgs) Handles Panel2.MouseUp
        fScaleY = False
    End Sub
#End Region

#Region "MISC"
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        bmp0 = New Bitmap("segybitmap.bmp")
        imgOriginalSize = {bmp0.Width, bmp0.Height}
        picBox.SizeMode = PictureBoxSizeMode.AutoSize

        picBox.SizeMode = PictureBoxSizeMode.StretchImage
        picBox.Width = imgOriginalSize(0)
        picBox.Height = imgOriginalSize(1)
        picBox.Image = bmp0

        Image_Axis_Initialize(picBox, Panel4, Panel2, Panel5, Panel7, listLabelX, listLabelY,
                              listTickV, listTickH, minX, maxX, minY, maxY)

    End Sub

    Private Sub Panel5_Scroll(sender As Object, e As ScrollEventArgs) Handles Panel5.Scroll
        Image_Axis_Update(picBox, Panel4, Panel2, Panel5, Panel7, listLabelX, listLabelY, listTickV, listTickH)
    End Sub

    Private Sub Form1_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        Image_Axis_Update(picBox, Panel4, Panel2, Panel5, Panel7, listLabelX, listLabelY, listTickV, listTickH)
    End Sub
#End Region

    Private Sub butReset_Click(sender As Object, e As EventArgs) Handles butReset.Click
        Image_Axis_SetToDefault(picBox, Panel5)
        Image_Axis_Update(picBox, Panel4, Panel2, Panel5, Panel7, listLabelX, listLabelY, listTickV, listTickH)
    End Sub
End Class
