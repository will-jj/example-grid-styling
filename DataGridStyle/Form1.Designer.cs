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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewTTable
            // 
            this.dataGridViewTTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTTable.Location = new System.Drawing.Point(65, 49);
            this.dataGridViewTTable.Name = "dataGridViewTTable";
            this.dataGridViewTTable.Size = new System.Drawing.Size(712, 603);
            this.dataGridViewTTable.TabIndex = 0;
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(630, 658);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(147, 23);
            this.buttonSave.TabIndex = 1;
            this.buttonSave.Text = "Save and Display";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonLoadandStyle
            // 
            this.buttonLoadandStyle.Location = new System.Drawing.Point(556, 658);
            this.buttonLoadandStyle.Name = "buttonLoadandStyle";
            this.buttonLoadandStyle.Size = new System.Drawing.Size(68, 23);
            this.buttonLoadandStyle.TabIndex = 2;
            this.buttonLoadandStyle.Text = "Load";
            this.buttonLoadandStyle.UseVisualStyleBackColor = true;
            this.buttonLoadandStyle.Click += new System.EventHandler(this.buttonLoadandStyle_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.Window;
            this.pictureBox1.Location = new System.Drawing.Point(815, 51);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(650, 600);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1506, 689);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.buttonLoadandStyle);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.dataGridViewTTable);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Text = "TT Table";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewTTable;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonLoadandStyle;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

