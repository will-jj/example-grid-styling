using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;


namespace DataGridStyle
{
    public class TableExporter
    {
        private static readonly string[] timeTrialHeadings =
        {
            "position",
            "car",
            "class",
             "name",
            "sector",
            "sector",
            "sector",
            "lap",
            "delta"
        };

        public enum TableType
        {
            timeTrialTable,
            leagueResults
        }

        public static int TableToJpg(DataGridView dgv, string imageName, int width = 1024, TableType tableType = TableType.timeTrialTable)
        {
            string html = ConvertDataGridViewToHTMLWithFormatting(dgv, tableType);
            return WriteImage(html, imageName, width);
        }
        // Inspired by pandas method and built upon: https://stackoverflow.com/questions/16008477/export-datagridview-to-html-page
        private static string ConvertDataGridViewToHTMLWithFormatting(DataGridView dgv, TableType tableType)
        {
            string tableID = "timeTrialTable";
            StringBuilder sb = new StringBuilder();
            //create html & table
            sb.AppendLine("<meta charset = \"UTF-8\"/>");
            sb.AppendLine("<html>");
            sb.AppendLine("<style type = \"text/css\">");
            sb.AppendLine(DGVStyle(tableType));
            sb.AppendLine("</style>");
            sb.AppendLine("</head>");
            sb.AppendLine("<body style = \"background-color:rgb(77, 77, 77);\" >");
            sb.AppendLine("<table>");
            sb.AppendLine("<thead>");
            sb.AppendLine("<tr>");
            //create table header
            for (int i = 0; i < dgv.Columns.Count; i++)
            {
                if (i == 0)
                {
                    sb.AppendLine("<th class=\"blank position\"></th>");
                }
                else
                {
                    sb.Append(DGVHeaderCellToHTMLWithFormatting(dgv, i));
                    sb.Append(DGVCellFontAndValueToHTML(dgv.Columns[i].HeaderText));
                    sb.AppendLine("</th>");
                }
            }
            sb.AppendLine("</tr>");
            sb.AppendLine("</thead>");
            //create table body
            for (int rowIndex = 0; rowIndex < dgv.Rows.Count; rowIndex++)
            {

                sb.AppendLine("<tr>");
                foreach (DataGridViewCell dgvc in dgv.Rows[rowIndex].Cells)
                {
                    string cellValue = dgvc.Value == null ? string.Empty : dgvc.Value.ToString();

                    if (dgvc.ColumnIndex == 0)
                    {
                        sb.AppendLine(String.Format("<th class = \"position row{0}\">{1}</th>", rowIndex, cellValue));
                    }
                    else
                    {
                        sb.AppendLine(DGVCellToHTMLWithFormatting(dgv, rowIndex, dgvc.ColumnIndex, tableID));
                        sb.AppendLine(DGVCellFontAndValueToHTML(cellValue));
                        sb.AppendLine("</td>");
                    }
                }
                sb.AppendLine("</tr>");
            }
            //table footer & end of html file
            sb.AppendLine("</table></body></html>");
            return sb.ToString();
        }

        //TODO: Add more cell styles described here: https://msdn.microsoft.com/en-us/library/1yef90x0(v=vs.110).aspx
        private static string DGVHeaderCellToHTMLWithFormatting(DataGridView dgv, int col)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(String.Format("<th class = \"col-{0}\"", timeTrialHeadings[col]));
            sb.Append(DGVCellColorToHTML(dgv.Columns[col].HeaderCell.Style.ForeColor, dgv.Columns[col].HeaderCell.Style.BackColor));
            sb.Append(">");
            return sb.ToString();
        }

        private static string DGVCellToHTMLWithFormatting(DataGridView dgv, int row, int col, string tableID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<td");

            sb.Append(DGVCellClass(row, col, tableID));
            sb.Append(DGVCellColorToHTML(dgv.Rows[row].Cells[col].Style.ForeColor, dgv.Rows[row].Cells[col].Style.BackColor));
            sb.Append(">");
            return sb.ToString();
        }

        private static string DGVCellClass(int row, int col, string tableID)
        {
            return String.Format(" class = \"row{0} col-{1}\" id = \"{2}row{0}_col-{1}\" ", row, timeTrialHeadings[col], tableID);
        }

        private static string DGVCellColorToHTML(Color foreColor, Color backColor)
        {
            if (foreColor.Name == "0" && backColor.Name == "0") return string.Empty;

            StringBuilder sb = new StringBuilder();
            sb.Append(" style=\"");
            if (foreColor.Name != "0" && backColor.Name != "0")
            {
                sb.Append("color:#");
                sb.Append(foreColor.R.ToString("X2") + foreColor.G.ToString("X2") + foreColor.B.ToString("X2"));
                sb.Append("; background-color:#");
                sb.Append(backColor.R.ToString("X2") + backColor.G.ToString("X2") + backColor.B.ToString("X2"));
            }
            else if (foreColor.Name != "0" && backColor.Name == "0")
            {
                sb.Append("color:#");
                sb.Append(foreColor.R.ToString("X2") + foreColor.G.ToString("X2") + foreColor.B.ToString("X2"));
            }
            else //if (foreColor.Name == "0" &&  backColor.Name != "0")
            {
                sb.Append("background-color:#");
                sb.Append(backColor.R.ToString("X2") + backColor.G.ToString("X2") + backColor.B.ToString("X2"));
            }

            sb.Append(";\"");
            return sb.ToString();
        }

        private static string DGVCellFontAndValueToHTML(string value)
        {
            return value;

        }

        //TODO: variables and not just a big string in the function
        // Variable tableID not really needed for a page with only one table 
        // If it needs to be reintroduced just use a unique item like {{tableID}} and search and replace
        private static string DGVStyle(TableType tableType)

        {
            string style;
            switch (tableType)
            {
                case TableType.timeTrialTable:
                    {
                        style = DataGridStyle.Properties.Resources.timeTrialTableStyle;
                        break;
                    }
                case TableType.leagueResults:
                    {
                        style = DataGridStyle.Properties.Resources.leagueResultsTableStyle;
                        break;
                    }
                default: throw new System.ArgumentException("No matching style found", "Styling"); ;
            }
            return style;


        }

        // could replace use of the exe with use of the DLL
        private static int WriteImage(string html, string imageName, int width)
        {
            string inFileName;
            int exitCode = -1;
            inFileName = "./table.html ";
            File.WriteAllText(inFileName, html);
            string arguments = String.Format("-q --quality 100 --width {0} {1} {2}", width, inFileName, imageName);

            try
            {
                using (Process myProcess = new Process())
                {
                    myProcess.StartInfo.UseShellExecute = false;
                    myProcess.StartInfo.FileName = "./wkhtmltoimage.exe";
                    myProcess.StartInfo.CreateNoWindow = true;
                    myProcess.StartInfo.Arguments = arguments;
                    myProcess.Start();
                    myProcess.WaitForExit(10000);
                    exitCode = myProcess.ExitCode;
                    myProcess.Close();
                }
            }
            catch (Exception e)
            {
                // Tell Jose it is broken
                Console.WriteLine(e.Message);

            }


            // delete the html file
            File.Delete(inFileName);
            return exitCode;
        }

    }
}
