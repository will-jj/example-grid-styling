using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace DataGridStyle
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
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
                dataGridViewTTable.Columns[col].Name = header;
                col = col + 1;

            }

            // Adding the rows
            var lines = File.ReadLines(filePath).Skip(1)
                .Select(x => x.Split(','))
                .ToList();

            lines.ForEach(line => dataGridViewTTable.Rows.Add(line));

        }


        private void ColourMinValue(string columnID)
        {

            var min = dataGridViewTTable.Rows.Cast<DataGridViewRow>().Min(r => Convert.ToDouble(r.Cells[columnID].Value));
            var max = dataGridViewTTable.Rows.Cast<DataGridViewRow>().Max(r => Convert.ToDouble(r.Cells[columnID].Value));
            Color purpleSectorColour = new Color();
            purpleSectorColour = Color.FromArgb(127, 4, 158);

            for (int i = 0; i < dataGridViewTTable.Rows.Count; i++)
            {
                Double value = Double.NaN;
                if (dataGridViewTTable.Rows[i].Cells[columnID].Value != null)
                {
                    value = Convert.ToDouble(dataGridViewTTable.Rows[i].Cells[columnID].Value.ToString());
                }
               
                if (value == min)
                {
                    dataGridViewTTable.Rows[i].Cells[columnID].Style.ForeColor = Color.White;
                    dataGridViewTTable.Rows[i].Cells[columnID].Style.BackColor = purpleSectorColour;
                    break;
                }
                if (value == max)
                {
                    //dataGridViewTTable.Rows[i].DefaultCellStyle.ForeColor = Color.Green;
                }


            }
        }

        private void buttonLoadandStyle_Click(object sender, EventArgs e)
        {
            ColourMinValue("S2");
            buttonLoadandStyle.Enabled = false;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            string imageName = "./table.jpg";
            int exitCode = TableExporter.TableToJpg(dataGridViewTTable, imageName);
            if (exitCode == 0)
            {
                pictureBox1.ImageLocation = imageName;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridViewTTable.AllowUserToAddRows = false;
        }
    }






}
