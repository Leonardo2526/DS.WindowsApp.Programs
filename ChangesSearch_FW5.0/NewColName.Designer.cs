
namespace ChangesSearch_DB
{
    partial class Form_NewColName
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
            this.NewColName = new System.Windows.Forms.Label();
            this.textBox_NewColName = new System.Windows.Forms.TextBox();
            this.Apply = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // NewColName
            // 
            this.NewColName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.NewColName.AutoSize = true;
            this.NewColName.Location = new System.Drawing.Point(17, 23);
            this.NewColName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.NewColName.Name = "NewColName";
            this.NewColName.Size = new System.Drawing.Size(232, 25);
            this.NewColName.TabIndex = 0;
            this.NewColName.Text = "Enter a new collection name";
            this.NewColName.Click += new System.EventHandler(this.label1_Click);
            // 
            // textBox_NewColName
            // 
            this.textBox_NewColName.Location = new System.Drawing.Point(279, 18);
            this.textBox_NewColName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox_NewColName.Name = "textBox_NewColName";
            this.textBox_NewColName.Size = new System.Drawing.Size(215, 31);
            this.textBox_NewColName.TabIndex = 1;
            this.textBox_NewColName.TextChanged += new System.EventHandler(this.textBox_NewColName_TextChanged);
            // 
            // Apply
            // 
            this.Apply.Location = new System.Drawing.Point(294, 82);
            this.Apply.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Apply.Name = "Apply";
            this.Apply.Size = new System.Drawing.Size(181, 38);
            this.Apply.TabIndex = 2;
            this.Apply.Text = "Apply";
            this.Apply.UseVisualStyleBackColor = true;
            this.Apply.Click += new System.EventHandler(this.Apply_Click);
            // 
            // Form_NewColName
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(570, 190);
            this.Controls.Add(this.Apply);
            this.Controls.Add(this.textBox_NewColName);
            this.Controls.Add(this.NewColName);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form_NewColName";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label NewColName;
        private System.Windows.Forms.Button Apply;
        public System.Windows.Forms.TextBox textBox_NewColName;
    }
}