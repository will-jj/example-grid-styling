namespace DataGridStyle
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridViewTTable = new System.Windows.Forms.DataGridView();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonLoadandStyle = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTTable)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewTTable
            // 
            this.dataGridViewTTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTTable.Location = new System.Drawing.Point(65, 49);
            this.dataGridViewTTable.Name = "dataGridViewTTable";
            this.dataGridViewTTable.Size = new System.Drawing.Size(1516, 848);
            this.dataGridViewTTable.TabIndex = 0;
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(1506, 903);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 1;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonLoadandStyle
            // 
            this.buttonLoadandStyle.Location = new System.Drawing.Point(1318, 903);
            this.buttonLoadandStyle.Name = "buttonLoadandStyle";
            this.buttonLoadandStyle.Size = new System.Drawing.Size(173, 23);
            this.buttonLoadandStyle.TabIndex = 2;
            this.buttonLoadandStyle.Text = "Load and Style";
            this.buttonLoadandStyle.UseVisualStyleBackColor = true;
            this.buttonLoadandStyle.Click += new System.EventHandler(this.buttonLoadandStyle_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1593, 938);
            this.Controls.Add(this.buttonLoadandStyle);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.dataGridViewTTable);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTTable)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewTTable;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonLoadandStyle;
    }
}

