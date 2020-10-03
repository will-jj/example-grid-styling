using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;


namespace DataGridStyle
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        public void StyleTable(DataGridView table, int fontSize = 20)
        {
            table.AutoSize = true;

            table.EnableHeadersVisualStyles = false;
            table.ColumnHeadersDefaultCellStyle.BackColor = Color.Black;
            table.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            table.BorderStyle = BorderStyle.None;
            table.CellBorderStyle = DataGridViewCellBorderStyle.None;
            table.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            table.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            table.RowHeadersVisible = false;
            table.AllowUserToAddRows = false;
            table.BackgroundColor = SystemColors.ControlLightLight;
            table.DefaultCellStyle.Font = new Font("Tahoma", fontSize);
            table.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", fontSize + 3);

            table.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            table.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;


        }



        /// <summary>
        /// Style the datagridview rows with alternating back colours  
        /// </summary>
        /// <param name="table">DataGridView table to colour</param>
        /// <param name="evenColour">HTML string for even row back colour (default white)</param>
        /// <param name="oddColour">HTML string for odd row back colour (default grey)</param>
        public void ColourRows(DataGridView table, string evenColour = "#fff", string oddColour = "#f5f5f5")
        {

            //Can alternatively set the padding on form load using instead of iterating through rows:
            //dataGridViewTTable.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#f5f5f5");
            //dataGridViewTTable.DefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#fff");

            foreach (DataGridViewRow row in table.Rows)
            {
                if (row.Index % 2 == 0)
                {
                    row.DefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml(evenColour);
                }
                else
                {
                    row.DefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml(oddColour);
                }

            }


        }
        /// <summary>
        /// Add padding to the rows in the DataGridView table
        /// </summary>
        /// <param name="table">DataGridView table to add padding to</param>
        public void PadRows(DataGridView table, int left = 10, int top = 10, int right = 10, int bottom = 10)
        {

            Padding generalPadding = new Padding(left, top, right, bottom);
            //Padding nameColumns = new Padding(0, 10, 0, 10);
            table.RowTemplate.DefaultCellStyle.Padding = generalPadding;
            table.ColumnHeadersDefaultCellStyle.Padding = generalPadding;

            //Can alternatively set the padding on form load using instead of iterating through rows:
            //Padding newPadding = new Padding(30, 10, 10, 10);
            //table.RowTemplate.DefaultCellStyle.Padding = newPadding;

            foreach (DataGridViewRow row in table.Rows)
            {
                row.DefaultCellStyle.Padding = generalPadding;

            }
            //table.Columns[0].DefaultCellStyle.Padding = nameColumns;
            //table.Columns[1].DefaultCellStyle.Padding = nameColumns;

        }

        /// <summary>
        /// Populate DatagridView with CSV data
        /// </summary>
        /// <param name="filePath">path to CSV data</param>
        public void ReadCSV(string filePath)
        {

            // Creating the columns
            var headers = File.ReadLines(filePath).Take(1)
                .SelectMany(x => x.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                .ToList();
            int col = 0;
            foreach (var header in headers)
            {
                dataGridViewTTable.Columns.Add(new DataGridViewTextBoxColumn());
                dataGridViewTTable.Columns[col].HeaderText = header;
                col = col + 1;


            }

            // Adding the rows
            var lines = File.ReadLines(filePath).Skip(1)
                .Select(x => x.Split(','))
                .ToList();
            lines.RemoveAt(lines.Count - 1);
            lines.ForEach(line => dataGridViewTTable.Rows.Add(line));



        }

        /// <summary>
        /// Convert to bitmap and save taken from:
        /// https://www.aspsnippets.com/Articles/Convert-Export-DataGridView-to-Bitmap-PNG-Image-in-Windows-Forms-WinForms-Application-using-C-and-VBNet.aspx
        /// </summary>
        private void SaveDataTTable()
        {
            //dataGridViewTTable.Width = 1000;
            //Resize DataGridView to full height.
            dataGridViewTTable.CurrentCell = null;
            int height = dataGridViewTTable.Height;
            dataGridViewTTable.Height = dataGridViewTTable.RowCount * dataGridViewTTable.RowTemplate.Height;

            //Create a Bitmap and draw the DataGridView on it.
            Bitmap bitmap = new Bitmap(this.dataGridViewTTable.Width, this.dataGridViewTTable.Height);
            dataGridViewTTable.DrawToBitmap(bitmap, new Rectangle(0, 0, this.dataGridViewTTable.Width, this.dataGridViewTTable.Height));

            //Resize DataGridView back to original height.  
            // not working
            dataGridViewTTable.Height = height;


            //Save the Bitmap to folder.
            bitmap.Save("../../../DataGridView.png");
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveDataTTable();

        }



        private void buttonLoadandStyle_Click(object sender, EventArgs e)
        {
            // I know there are better ways to point to this file
            ReadCSV("../../../test.csv");
            //ColourRows(dataGridViewTTable);
            //PadRows(dataGridViewTTable);

        }



        private void Form1_Load(object sender, EventArgs e)
        {
            StyleTable(dataGridViewTTable);
            Padding generalPadding = new Padding(10, 10, 10, 10);
            dataGridViewTTable.RowTemplate.DefaultCellStyle.Padding = generalPadding;
            dataGridViewTTable.ColumnHeadersDefaultCellStyle.Padding = generalPadding;
            dataGridViewTTable.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#f5f5f5");
            dataGridViewTTable.DefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#fff");
        }
    }
}
