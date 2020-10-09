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
                col = col + 1;

            }

            // Adding the rows
            var lines = File.ReadLines(filePath).Skip(1)
                .Select(x => x.Split(','))
                .ToList();
            lines.RemoveAt(lines.Count - 1);
            lines.ForEach(line => dataGridViewTTable.Rows.Add(line));

        }






        private void ColourSector()
        {

            int S1 = 4;
            var min = dataGridViewTTable.Rows.Cast<DataGridViewRow>().Min(r => Convert.ToDouble(r.Cells[S1].Value));
            var max = dataGridViewTTable.Rows.Cast<DataGridViewRow>().Max(r => Convert.ToDouble(r.Cells[S1].Value));
            min = 1;
            Color purpleSectorColour = new Color();
            purpleSectorColour = Color.FromArgb(127, 4, 158);

            for (int i = 0; i < dataGridViewTTable.Rows.Count; i++)
            {
                Double value = Double.NaN;
                if (dataGridViewTTable.Rows[i].Cells[S1].Value != null)
                {
                    value = Convert.ToDouble(dataGridViewTTable.Rows[i].Cells[S1].Value.ToString());
                }
               
                if (value == min)
                {
                    dataGridViewTTable.Rows[i].Cells[S1].Style.ForeColor = Color.White;
                    dataGridViewTTable.Rows[i].Cells[S1].Style.BackColor = purpleSectorColour;
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
            // I know there are better ways to point to this file
            ReadCSV("../../../testModified.csv");
            ColourSector();
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

    }






}
