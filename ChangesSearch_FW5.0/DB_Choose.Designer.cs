
namespace ChangesSearch_DB
{
    partial class DB_Choose
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
            this.label1 = new System.Windows.Forms.Label();
            this.Apply = new System.Windows.Forms.Button();
            this.comboBox_DBList = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 15);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(403, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Choose the database you are going to work with:";
            // 
            // Apply
            // 
            this.Apply.Location = new System.Drawing.Point(289, 130);
            this.Apply.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Apply.Name = "Apply";
            this.Apply.Size = new System.Drawing.Size(107, 38);
            this.Apply.TabIndex = 1;
            this.Apply.Text = "Apply";
            this.Apply.UseVisualStyleBackColor = true;
            this.Apply.Click += new System.EventHandler(this.Apply_Click);
            // 
            // comboBox_DBList
            // 
            this.comboBox_DBList.FormattingEnabled = true;
            this.comboBox_DBList.Location = new System.Drawing.Point(17, 63);
            this.comboBox_DBList.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.comboBox_DBList.Name = "comboBox_DBList";
            this.comboBox_DBList.Size = new System.Drawing.Size(270, 33);
            this.comboBox_DBList.TabIndex = 2;
            this.comboBox_DBList.SelectedIndexChanged += new System.EventHandler(this.comboBox_DBList_SelectedIndexChanged);
            // 
            // DB_Choose
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(424, 203);
            this.Controls.Add(this.comboBox_DBList);
            this.Controls.Add(this.Apply);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "DB_Choose";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.DB_Choose_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Apply;
        private System.Windows.Forms.ComboBox comboBox_DBList;
    }
}