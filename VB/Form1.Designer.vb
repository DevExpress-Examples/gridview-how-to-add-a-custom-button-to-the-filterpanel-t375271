Namespace dxExample
	Partial Public Class Form1
		''' <summary>
		''' Required designer variable.
		''' </summary>
		Private components As System.ComponentModel.IContainer = Nothing

		''' <summary>
		''' Clean up any resources being used.
		''' </summary>
		''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		Protected Overrides Sub Dispose(ByVal disposing As Boolean)
			If disposing AndAlso (components IsNot Nothing) Then
				components.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub

		#Region "Windows Form Designer generated code"

		''' <summary>
		''' Required method for Designer support - do not modify
		''' the contents of this method with the code editor.
		''' </summary>
		Private Sub InitializeComponent()
			Me.customGrid1 = New dxExample.CustomGrid()
			Me.customGridView1 = New dxExample.CustomGridView()
			Me.defaultLookAndFeel1 = New DevExpress.LookAndFeel.DefaultLookAndFeel()
			DirectCast(Me.customGrid1, System.ComponentModel.ISupportInitialize).BeginInit()
			DirectCast(Me.customGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.SuspendLayout()
			' 
			' customGrid1
			' 
			Me.customGrid1.Dock = System.Windows.Forms.DockStyle.Fill
			Me.customGrid1.Location = New System.Drawing.Point(0, 0)
			Me.customGrid1.MainView = Me.customGridView1
			Me.customGrid1.Name = "customGrid1"
			Me.customGrid1.Size = New System.Drawing.Size(643, 455)
			Me.customGrid1.TabIndex = 0
			Me.customGrid1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() { Me.customGridView1})
			' 
			' customGridView1
			' 
			Me.customGridView1.GridControl = Me.customGrid1
			Me.customGridView1.Name = "customGridView1"
			' 
			' defaultLookAndFeel1
			' 
			Me.defaultLookAndFeel1.EnableBonusSkins = True
			Me.defaultLookAndFeel1.LookAndFeel.SkinName = "Office 2010 Blue"
			' 
			' Form1
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.ClientSize = New System.Drawing.Size(643, 455)
			 Me.Controls.Add(Me.customGrid1)
			Me.Name = "Form1"
			Me.Text = "Form1"
			DirectCast(Me.customGrid1, System.ComponentModel.ISupportInitialize).EndInit()
			DirectCast(Me.customGridView1, System.ComponentModel.ISupportInitialize).EndInit()
			Me.ResumeLayout(False)

		End Sub

		#End Region

		Private customGrid1 As CustomGrid
		Private customGridView1 As CustomGridView
		Private defaultLookAndFeel1 As DevExpress.LookAndFeel.DefaultLookAndFeel

	End Class
End Namespace

