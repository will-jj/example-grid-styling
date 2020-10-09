﻿using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;


namespace DataGridStyle
{
    public class TableExporter
    {
        private static readonly string[] headings =
        {
            "name",
            "car",
            "class",
            "sector",
            "sector",
            "sector",
            "lap",
            "delta"
        };

        public static int TableToJpg(DataGridView dgv, Font formFont, string imageName, int width = 1024)
        {
            string html = ConvertDataGridViewToHTMLWithFormatting(dgv, formFont);
            return WriteImage(html, imageName, width);
        }
        // Inspired by pandas method and built upon: https://stackoverflow.com/questions/16008477/export-datagridview-to-html-page
        private static string ConvertDataGridViewToHTMLWithFormatting(DataGridView dgv, Font formFont)
        {
            string tableID = "timeTrialTable";
            StringBuilder sb = new StringBuilder();
            //create html & table
            sb.AppendLine(DGVStyle(tableID));
            sb.AppendLine((String.Format("<table id = \"{0}\"><thead>", tableID)));
            sb.AppendLine("<tr>");
            //create table header
            for (int i = 0; i < dgv.Columns.Count; i++)
            {
                sb.Append(DGVHeaderCellToHTMLWithFormatting(dgv, i));
                sb.Append(DGVCellFontAndValueToHTML(dgv.Columns[i].HeaderText, dgv.Columns[i].HeaderCell.Style.Font, formFont));
                sb.AppendLine("</th>");
            }
            sb.AppendLine("</tr>");
            sb.AppendLine("</thead>");
            //create table body
            for (int rowIndex = 0; rowIndex < dgv.Rows.Count; rowIndex++)
            {
                sb.AppendLine("<tr>");
                foreach (DataGridViewCell dgvc in dgv.Rows[rowIndex].Cells)
                {
                    sb.AppendLine(DGVCellToHTMLWithFormatting(dgv, rowIndex, dgvc.ColumnIndex, tableID));
                    string cellValue = dgvc.Value == null ? string.Empty : dgvc.Value.ToString();
                    sb.AppendLine(DGVCellFontAndValueToHTML(cellValue, dgvc.Style.Font, formFont));
                    sb.AppendLine("</td>");
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
            sb.Append(String.Format("<th class = \"col-{0}\"", headings[col]));
            sb.Append(DGVCellColorToHTML(dgv.Columns[col].HeaderCell.Style.ForeColor, dgv.Columns[col].HeaderCell.Style.BackColor));
            sb.Append(DGVCellAlignmentToHTML(dgv.Columns[col].HeaderCell.Style.Alignment));
            sb.Append(">");
            return sb.ToString();
        }

        private static string DGVCellToHTMLWithFormatting(DataGridView dgv, int row, int col, string tableID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<td");

            sb.Append(DGVCellClass(row, col, tableID));
            sb.Append(DGVCellColorToHTML(dgv.Rows[row].Cells[col].Style.ForeColor, dgv.Rows[row].Cells[col].Style.BackColor));
            sb.Append(DGVCellAlignmentToHTML(dgv.Rows[row].Cells[col].Style.Alignment));
            sb.Append(">");
            return sb.ToString();
        }

        private static string DGVCellClass(int row, int col, string tableID)
        {
            return String.Format(" class = \"row{0} col-{1}\" id = \"{2}row{0}_col-{1}\" ", row, headings[col], tableID);
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

        private static string DGVCellFontAndValueToHTML(string value, Font font, Font formFont)
        {
            //If no font has been set then assume its the default as someone would be expected in HTML or Excel
            if (font == null || font == formFont && !(font.Bold | font.Italic | font.Underline | font.Strikeout)) return value;
            StringBuilder sb = new StringBuilder();
            sb.Append(" ");
            if (font.Bold) sb.Append("<b>");
            if (font.Italic) sb.Append("<i>");
            if (font.Strikeout) sb.Append("<strike>");

            //The <u> element was deprecated in HTML 4.01. The new HTML 5 tag is: text-decoration: underline
            if (font.Underline) sb.Append("<u>");

            string size = string.Empty;
            if (font.Size != formFont.Size) size = "font-size: " + font.Size + "pt;";

            //The <font> tag is not supported in HTML5. Use CSS or a span instead. 
            if (font.FontFamily.Name != formFont.Name)
            {
                sb.Append("<span style=\"font-family: ");
                sb.Append(font.FontFamily.Name);
                sb.Append("; ");
                sb.Append(size);
                sb.Append("\">");
            }
            sb.Append(value);
            if (font.FontFamily.Name != formFont.Name) sb.Append("</span>");

            if (font.Underline) sb.Append("</u>");
            if (font.Strikeout) sb.Append("</strike>");
            if (font.Italic) sb.Append("</i>");
            if (font.Bold) sb.Append("</b>");

            return sb.ToString();
        }

        private static string DGVCellAlignmentToHTML(DataGridViewContentAlignment align)
        {
            if (align == DataGridViewContentAlignment.NotSet) return string.Empty;

            string horizontalAlignment = string.Empty;
            string verticalAlignment = string.Empty;
            CellAlignment(align, ref horizontalAlignment, ref verticalAlignment);
            StringBuilder sb = new StringBuilder();
            sb.Append(" align='");
            sb.Append(horizontalAlignment);
            sb.Append("' valign='");
            sb.Append(verticalAlignment);
            sb.Append("'");
            return sb.ToString();
        }

        private static void CellAlignment(DataGridViewContentAlignment align, ref string horizontalAlignment, ref string verticalAlignment)
        {
            switch (align)
            {
                case DataGridViewContentAlignment.MiddleRight:
                    horizontalAlignment = "right";
                    verticalAlignment = "middle";
                    break;
                case DataGridViewContentAlignment.MiddleLeft:
                    horizontalAlignment = "left";
                    verticalAlignment = "middle";
                    break;
                case DataGridViewContentAlignment.MiddleCenter:
                    horizontalAlignment = "centre";
                    verticalAlignment = "middle";
                    break;
                case DataGridViewContentAlignment.TopCenter:
                    horizontalAlignment = "centre";
                    verticalAlignment = "top";
                    break;
                case DataGridViewContentAlignment.BottomCenter:
                    horizontalAlignment = "centre";
                    verticalAlignment = "bottom";
                    break;
                case DataGridViewContentAlignment.TopLeft:
                    horizontalAlignment = "left";
                    verticalAlignment = "top";
                    break;
                case DataGridViewContentAlignment.BottomLeft:
                    horizontalAlignment = "left";
                    verticalAlignment = "bottom";
                    break;
                case DataGridViewContentAlignment.TopRight:
                    horizontalAlignment = "right";
                    verticalAlignment = "top";
                    break;
                case DataGridViewContentAlignment.BottomRight:
                    horizontalAlignment = "right";
                    verticalAlignment = "bottom";
                    break;

                default: //DataGridViewContentAlignment.NotSet
                    horizontalAlignment = "centre";
                    verticalAlignment = "middle";
                    break;
            }
        }
        //TODO: variables and not just a big string in the function
        private static string DGVStyle(string tableID)

        {

            return String.Format(@"
<meta charset=""UTF-8""/>
<html>
<head>
<style type=""text/css"">
[class^=""row0 col-name""]{{background-color: rgba(255, 217, 0, 0.61);}}
[class^=""row1 col-name""]{{background-color: rgb(151, 151, 151);}}
[class^=""row2 col-name""]{{background-color: rgb(205,127,50);}}
    
.col-name {{width: 35%;}}
.col-car  {{width: 14%;}}
.col-class {{
  width: 5%;
  text-align: center;
}}
.col-sector {{
  width: 10%;
  text-align: center;
}}

.col-lap {{
  width: 10%;
  text-align: center;
}}

.col-delta {{
  width: 6%;
  text-align: center;
}}
   
#{0} {{
    margin: 0;
          font-family: 'Roboto', sans-serif;
          border-collapse: collapse;
          border-spacing: 0;
          color: white;
          border: none;
          table-layout: fixed;
    }}    #{0} thead {{
          background-color: rgb(0, 0, 0);
border-bottom: 1px solid black;
    }}    #{0} tbody tr:nth-child(even) {{
          background-color: rgb(65, 62, 62);
    }}    #{0} tbody tr:nth-child(odd) {{
          background-color: #2c2b2b;
    }}    #{0} td {{
          padding: .5em;
    }}    #{0} th {{
          font-size: 115%;
          text-align: center;
    }}    #{0} caption {{
          caption-side: bottom;
    }}   

</style>
</head>
<body style = ""background-color:rgb(77, 77, 77);"" >", tableID);
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
