using DevExpress.XtraEditors.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace dxExample
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        public Form1()
        {
            InitializeComponent();
            customGrid1.DataSource = GetData(10);
            customGridView1.ActiveFilterString = "[ID] Between (3, 6)";
            customGridView1.CustomButtonClick += customGridView1_CustomButtonClick;
            AddCustomButtonsToFilterPanel();
        }

        private void AddCustomButtonsToFilterPanel()
        {
            EditorButton b = new EditorButton(ButtonPredefines.Glyph) { Caption = "Custom Button", Width = 80 };
            customGridView1.AddButton(b);

            b = new EditorButton(ButtonPredefines.Glyph) { Caption = "Custom Filter Button", Width = 125 };
            b.Image = Image.FromFile(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + @"\Filter_16x16.png");
            b.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            customGridView1.AddButton(b);
        }

        void customGridView1_CustomButtonClick(object sender, EventArgs e)
        {
            EditorButton b = sender as EditorButton;
            Console.WriteLine(string.Format("{0} is clicked",b.Caption));
        }

        DataTable GetData(int rows)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Info", typeof(string));
            dt.Columns.Add("Value", typeof(decimal));
            dt.Columns.Add("Date", typeof(DateTime));
            dt.Columns.Add("Type", typeof(string));
            for (int i = 0; i < rows; i++)
            {
                dt.Rows.Add(i, "Info" + i, 3.37 * i, DateTime.Now.AddDays(i), "Type " + i % 3);
            }
            return dt;
        }

    }
}
