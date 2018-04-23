Imports DevExpress.Utils.Drawing
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraEditors.Drawing
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Drawing
Imports DevExpress.XtraGrid.Registrator
Imports DevExpress.XtraGrid.Skins
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Base.Handler
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Grid.Handler
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Drawing
Imports System.Linq
Imports System.Windows.Forms

Namespace dxExample
	<ToolboxItem(True)>
	Public Class CustomGrid
		Inherits GridControl

		Protected Overrides Function CreateDefaultView() As BaseView
			Return CreateView("CustomGridView")
		End Function

		Protected Overrides Sub RegisterAvailableViewsCore(ByVal collection As InfoCollection)
			MyBase.RegisterAvailableViewsCore(collection)
			collection.Add(New CustomGridViewInfoRegistrator())
		End Sub
	End Class

	Public Class CustomGridViewInfoRegistrator
		Inherits GridInfoRegistrator

		Public Overrides ReadOnly Property ViewName() As String
			Get
				Return "CustomGridView"
			End Get
		End Property
		Private ps As CustomGridSkinPaintStyle
		Public Overrides Function PaintStyleByLookAndFeel(ByVal lookAndFeel As DevExpress.LookAndFeel.UserLookAndFeel, ByVal name As String) As ViewPaintStyle
			If ps Is Nothing Then
				ps = New CustomGridSkinPaintStyle()
				PaintStyles.Add(ps)
			End If
			Return ps
		End Function
		Public Overrides Function CreateView(ByVal grid As GridControl) As BaseView
			Return New CustomGridView(grid)
		End Function

		Public Overrides Function CreateHandler(ByVal view As BaseView) As BaseViewHandler
			Return New CustomGridViewHandler(TryCast(view, CustomGridView))
		End Function
	End Class
	Public Class CustomGridSkinPaintStyle
		Inherits GridSkinPaintStyle

		Public Sub New()
		End Sub
		Public Overrides Function CreateElementsPainter(ByVal view As BaseView) As DevExpress.XtraGrid.Views.Grid.Drawing.GridElementsPainter
			Return New CustomGridSkinElementsPainter(view)
		End Function
	End Class

	Public Class CustomGridSkinElementsPainter
		Inherits GridSkinElementsPainter

		Public Sub New(ByVal view As BaseView)
			MyBase.New(view)
		End Sub
		Protected Overrides Function CreateFilterPanelPainter() As GridFilterPanelPainter
			Dim painter As GridFilterPanelPainter = New CustomGridFilterPanelPainter(View)
			Return painter
		End Function
	End Class
	Public Class CustomGridFilterPanelPainter
		Inherits SkinGridFilterPanelPainter

		Public Sub New(ByVal provider As GridView)
			MyBase.New(provider)
			View = TryCast(provider, CustomGridView)
			customButtonPainter = New SkinEditorButtonPainter(DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveLookAndFeel)
		End Sub
		Protected Overrides Function CalcButtonLocation(ByVal client As Rectangle, ByVal size As Size, ByVal isRight As Boolean) As Point
			Dim point As Point = MyBase.CalcButtonLocation(client, size, isRight)
			If point.X < 10 Then
				point.X += View.UpdateButtonsRects() ' set the indent
			End If

			Return point
		End Function

		Public Overrides Sub DrawObject(ByVal e As ObjectInfoArgs)
			MyBase.DrawObject(e)
			DrawCustomButtons(e.Cache)
		End Sub

		Public Property View() As CustomGridView
		Private customButtonPainter As SkinEditorButtonPainter
		Private Sub DrawCustomButtons(ByVal cache As GraphicsCache)
			For Each button As EditorButton In View.ButtonsStrore.Keys
				customButtonPainter.DrawObject(GetButtonInfoArgs(cache, View.ButtonsStrore(button),button))
			Next button
		End Sub

		Friend Function GetButtonInfoArgs(ByVal cache As GraphicsCache, ByVal args As EditorButtonObjectInfoArgs, ByVal button As EditorButton) As EditorButtonObjectInfoArgs
			args.Cache = cache
			Dim state As ObjectState = ObjectState.Normal
			If TypeOf button.Tag Is ObjectState Then
				state = CType(button.Tag, ObjectState)
			End If
			args.State = state
			Return args
		End Function
	End Class

	Public Class CustomGridView
		Inherits GridView

		Public Sub New()
			ButtonsStrore = New Dictionary(Of EditorButton, EditorButtonObjectInfoArgs)()
		End Sub

		Public Sub New(ByVal grid As GridControl)
			MyBase.New(grid)
		End Sub
		Protected Overrides ReadOnly Property ViewName() As String
			Get
				Return "CustomGridView"
			End Get
		End Property
		Friend ButtonsStrore As Dictionary(Of EditorButton, EditorButtonObjectInfoArgs)

		Public Sub AddButton(ByVal b As EditorButton)
			ButtonsStrore.Add(b, New EditorButtonObjectInfoArgs(b, New DevExpress.Utils.AppearanceObject()))
		End Sub

		Friend Function UpdateButtonsRects() As Integer
			Dim offset As Integer = 10
			For Each button As EditorButton In ButtonsStrore.Keys
                Dim y As Integer = ViewInfo.ClientBounds.Y + (ViewInfo.ClientBounds.Height - ((ViewInfo.FilterPanel.Bounds.Height \ 8) * 7))
				Dim x As Integer = offset
				offset += 5 + button.Width
                Dim buttonRect As New Rectangle(x, y, button.Width, (ViewInfo.FilterPanel.Bounds.Height \ 8) * 6)
				ButtonsStrore(button).Bounds = buttonRect
			Next button
			Return offset
		End Function

		Public Event CustomButtonClick As EventHandler
		#Region "OnCustomButtonClick"
		''' <summary>
		''' Triggers the CustomButtonClick event.
		''' </summary>
		Public Overridable Sub OnCustomButtonClick(ByVal b As EditorButton, ByVal ea As EventArgs)
			Dim handler As EventHandler = CustomButtonClickEvent
			If handler IsNot Nothing Then
				handler(b, ea)
			End If
		End Sub
		#End Region

		Private shouldReset As Boolean = False
		Friend Sub UpdateButtonState(ByVal state As ObjectState, ByVal point As Point)
			Dim hitInfo As GridHitInfo = CalcHitInfo(point)
			If hitInfo.HitTest <> GridHitTest.FilterPanel Then
				If shouldReset Then
					shouldReset = False
					If ResetButtonState() Then
						InvalidateFilterPanel()
					End If
				End If
				Return
			End If
			UpdateButtonStateCore(state, point)
			InvalidateFilterPanel()
			shouldReset = True
		End Sub


		Private Function ResetButtonState() As Boolean
			Dim res As Boolean = False
			For Each button As EditorButton In ButtonsStrore.Keys
                If button.Tag IsNot Nothing AndAlso CType(button.Tag, ObjectState) <> ObjectState.Normal Then
                    button.Tag = ObjectState.Normal
                    res = True
                End If
			Next button
			Return res
		End Function

		Private Sub UpdateButtonStateCore(ByVal state As ObjectState, ByVal point As Point)
			For Each button As EditorButton In ButtonsStrore.Keys
				button.Tag = ObjectState.Normal
				If ButtonsStrore(button).Bounds.Contains(point) Then
					button.Tag = state
                    If state = ObjectState.Pressed Then
                        OnCustomButtonClick(button, EventArgs.Empty)
                    End If
				End If
			Next button
		End Sub
	End Class

	Public Class CustomGridViewHandler
		Inherits GridHandler

		Public Sub New(ByVal view As GridView)
			MyBase.New(view)
		End Sub
		Public ReadOnly Property View() As CustomGridView
			Get
				Return TryCast(MyBase.View, CustomGridView)
			End Get
		End Property
		Protected Overrides Function OnMouseDown(ByVal ev As MouseEventArgs) As Boolean
			View.UpdateButtonState(ObjectState.Pressed,ev.Location)
			Return MyBase.OnMouseDown(ev)
		End Function
		Protected Overrides Function OnMouseMove(ByVal ev As MouseEventArgs) As Boolean
			View.UpdateButtonState(ObjectState.Hot,ev.Location)
			Return MyBase.OnMouseMove(ev)
		End Function
		Protected Overrides Sub OnMouseLeave(ByVal e As EventArgs)
			View.UpdateButtonState(ObjectState.Normal, Point.Empty)
			MyBase.OnMouseLeave(e)
		End Sub
		Protected Overrides Function OnMouseUp(ByVal ev As MouseEventArgs) As Boolean
			View.UpdateButtonState(ObjectState.Normal,ev.Location)
			Return MyBase.OnMouseUp(ev)
		End Function
	End Class
End Namespace
