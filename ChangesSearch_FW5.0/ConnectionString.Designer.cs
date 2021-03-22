
namespace ChangesSearch_DB
{
    partial class ConnectionString
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
            this.textBox_ConnectionString = new System.Windows.Forms.TextBox();
            this.Apply = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBox_ConnectionString
            // 
            this.textBox_ConnectionString.Location = new System.Drawing.Point(12, 37);
            this.textBox_ConnectionString.Name = "textBox_ConnectionString";
            this.textBox_ConnectionString.Size = new System.Drawing.Size(842, 31);
            this.textBox_ConnectionString.TabIndex = 1;
            this.textBox_ConnectionString.Text = "mongodb://mdbbim:Fr1n$3ur@172.16.153.56:27017/?authSource=admin";
            this.textBox_ConnectionString.TextChanged += new System.EventHandler(this.textBox_ConnectionString_TextChanged);
            // 
            // Apply
            // 
            this.Apply.Location = new System.Drawing.Point(12, 115);
            this.Apply.Name = "Apply";
            this.Apply.Size = new System.Drawing.Size(169, 45);
            this.Apply.TabIndex = 2;
            this.Apply.Text = "Apply";
            this.Apply.UseVisualStyleBackColor = true;
            this.Apply.Click += new System.EventHandler(this.Apply_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(326, 25);
            this.label1.TabIndex = 3;
            this.label1.Text = "Choose connection string or left default";
            // 
            // ConnectionString
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(866, 172);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Apply);
            this.Controls.Add(this.textBox_ConnectionString);
            this.Name = "ConnectionString";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ConnectionString";
            this.Load += new System.EventHandler(this.ConnectionString_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBox_ConnectionString;
        private System.Windows.Forms.Button Apply;
        private System.Windows.Forms.Label label1;
    }
}