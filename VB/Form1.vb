Imports DevExpress.XtraEditors.Controls
Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Drawing
Imports System.IO
Imports System.Linq
Imports System.Windows.Forms

Namespace dxExample
	Partial Public Class Form1
		Inherits DevExpress.XtraEditors.XtraForm

		Public Sub New()
			InitializeComponent()
			customGrid1.DataSource = GetData(10)
			customGridView1.ActiveFilterString = "[ID] Between (3, 6)"
			AddHandler customGridView1.CustomButtonClick, AddressOf customGridView1_CustomButtonClick
			AddCustomButtonsToFilterPanel()
		End Sub

		Private Sub AddCustomButtonsToFilterPanel()
			Dim b As New EditorButton(ButtonPredefines.Glyph) With {.Caption = "Custom Button", .Width = 80}
			customGridView1.AddButton(b)

			b = New EditorButton(ButtonPredefines.Glyph) With {.Caption = "Custom Filter Button", .Width = 125}
			b.Image = Image.FromFile(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName & "\Filter_16x16.png")
			b.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft
			customGridView1.AddButton(b)
		End Sub

		Private Sub customGridView1_CustomButtonClick(ByVal sender As Object, ByVal e As EventArgs)
			Dim b As EditorButton = TryCast(sender, EditorButton)
			Console.WriteLine(String.Format("{0} is clicked",b.Caption))
		End Sub

		Private Function GetData(ByVal rows As Integer) As DataTable
			Dim dt As New DataTable()
			dt.Columns.Add("ID", GetType(Integer))
			dt.Columns.Add("Info", GetType(String))
			dt.Columns.Add("Value", GetType(Decimal))
			dt.Columns.Add("Date", GetType(Date))
			dt.Columns.Add("Type", GetType(String))
			For i As Integer = 0 To rows - 1
				dt.Rows.Add(i, "Info" & i, 3.37 * i, Date.Now.AddDays(i), "Type " & i Mod 3)
			Next i
			Return dt
		End Function

	End Class
End Namespace
