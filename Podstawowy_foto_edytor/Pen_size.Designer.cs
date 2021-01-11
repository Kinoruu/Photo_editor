namespace Podstawowy_foto_edytor
{
    partial class Pen_size
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
            this.Pen_size_size_textBox = new System.Windows.Forms.TextBox();
            this.Pen_size_size_label = new System.Windows.Forms.Label();
            this.Pen_size_OK_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Pen_size_size_textBox
            // 
            this.Pen_size_size_textBox.Location = new System.Drawing.Point(51, 24);
            this.Pen_size_size_textBox.Name = "Pen_size_size_textBox";
            this.Pen_size_size_textBox.Size = new System.Drawing.Size(41, 20);
            this.Pen_size_size_textBox.TabIndex = 0;
            this.Pen_size_size_textBox.Text = "4";
            // 
            // Pen_size_size_label
            // 
            this.Pen_size_size_label.AutoSize = true;
            this.Pen_size_size_label.Location = new System.Drawing.Point(20, 27);
            this.Pen_size_size_label.Name = "Pen_size_size_label";
            this.Pen_size_size_label.Size = new System.Drawing.Size(25, 13);
            this.Pen_size_size_label.TabIndex = 1;
            this.Pen_size_size_label.Text = "size";
            // 
            // Pen_size_OK_button
            // 
            this.Pen_size_OK_button.Location = new System.Drawing.Point(98, 24);
            this.Pen_size_OK_button.Name = "Pen_size_OK_button";
            this.Pen_size_OK_button.Size = new System.Drawing.Size(33, 20);
            this.Pen_size_OK_button.TabIndex = 2;
            this.Pen_size_OK_button.Text = "OK";
            this.Pen_size_OK_button.UseVisualStyleBackColor = true;
            this.Pen_size_OK_button.Click += new System.EventHandler(this.Pen_size_OK_button_Click);
            // 
            // Pen_size
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(177, 67);
            this.Controls.Add(this.Pen_size_OK_button);
            this.Controls.Add(this.Pen_size_size_label);
            this.Controls.Add(this.Pen_size_size_textBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Pen_size";
            this.Text = "Pen_size";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label Pen_size_size_label;
        private System.Windows.Forms.Button Pen_size_OK_button;
        public System.Windows.Forms.TextBox Pen_size_size_textBox;
    }
}