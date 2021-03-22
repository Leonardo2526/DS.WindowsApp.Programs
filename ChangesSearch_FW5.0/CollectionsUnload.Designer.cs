
namespace ChangesSearch_DB
{
    partial class CollectionsUnload
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
            this.comboBox_Collections = new System.Windows.Forms.ComboBox();
            this.Apply = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // comboBox_Collections
            // 
            this.comboBox_Collections.FormattingEnabled = true;
            this.comboBox_Collections.Location = new System.Drawing.Point(17, 70);
            this.comboBox_Collections.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.comboBox_Collections.Name = "comboBox_Collections";
            this.comboBox_Collections.Size = new System.Drawing.Size(384, 33);
            this.comboBox_Collections.TabIndex = 0;
            this.comboBox_Collections.SelectedIndexChanged += new System.EventHandler(this.comboBox_Collections_SelectedIndexChanged);
            // 
            // Apply
            // 
            this.Apply.Location = new System.Drawing.Point(296, 167);
            this.Apply.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Apply.Name = "Apply";
            this.Apply.Size = new System.Drawing.Size(107, 38);
            this.Apply.TabIndex = 1;
            this.Apply.Text = "Apply";
            this.Apply.UseVisualStyleBackColor = true;
            this.Apply.Click += new System.EventHandler(this.Apply_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 22);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "Chose report:";
            // 
            // CollectionsUnload
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(426, 247);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Apply);
            this.Controls.Add(this.comboBox_Collections);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "CollectionsUnload";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.CollectionsUnload_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox_Collections;
        private System.Windows.Forms.Button Apply;
        private System.Windows.Forms.Label label1;
    }
}