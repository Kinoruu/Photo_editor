namespace Podstawowy_foto_edytor
{
    partial class Extraction
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Extraction));
            this.e_R_label = new System.Windows.Forms.Label();
            this.e_R_textBox = new System.Windows.Forms.TextBox();
            this.e_G_textBox = new System.Windows.Forms.TextBox();
            this.e_G_label = new System.Windows.Forms.Label();
            this.e_B_textBox = new System.Windows.Forms.TextBox();
            this.e_B_label = new System.Windows.Forms.Label();
            this.e_from_to = new System.Windows.Forms.Label();
            this.e_rgb_ok_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // e_R_label
            // 
            resources.ApplyResources(this.e_R_label, "e_R_label");
            this.e_R_label.Name = "e_R_label";
            // 
            // e_R_textBox
            // 
            resources.ApplyResources(this.e_R_textBox, "e_R_textBox");
            this.e_R_textBox.Name = "e_R_textBox";
            // 
            // e_G_textBox
            // 
            resources.ApplyResources(this.e_G_textBox, "e_G_textBox");
            this.e_G_textBox.Name = "e_G_textBox";
            // 
            // e_G_label
            // 
            resources.ApplyResources(this.e_G_label, "e_G_label");
            this.e_G_label.Name = "e_G_label";
            // 
            // e_B_textBox
            // 
            resources.ApplyResources(this.e_B_textBox, "e_B_textBox");
            this.e_B_textBox.Name = "e_B_textBox";
            // 
            // e_B_label
            // 
            resources.ApplyResources(this.e_B_label, "e_B_label");
            this.e_B_label.Name = "e_B_label";
            // 
            // e_from_to
            // 
            resources.ApplyResources(this.e_from_to, "e_from_to");
            this.e_from_to.Name = "e_from_to";
            // 
            // e_rgb_ok_button
            // 
            resources.ApplyResources(this.e_rgb_ok_button, "e_rgb_ok_button");
            this.e_rgb_ok_button.Name = "e_rgb_ok_button";
            this.e_rgb_ok_button.UseVisualStyleBackColor = true;
            this.e_rgb_ok_button.Click += new System.EventHandler(this.e_rgb_ok_button_Click);
            // 
            // Extraction
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.e_rgb_ok_button);
            this.Controls.Add(this.e_from_to);
            this.Controls.Add(this.e_B_textBox);
            this.Controls.Add(this.e_B_label);
            this.Controls.Add(this.e_G_textBox);
            this.Controls.Add(this.e_G_label);
            this.Controls.Add(this.e_R_textBox);
            this.Controls.Add(this.e_R_label);
            this.Name = "Extraction";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label e_R_label;
        private System.Windows.Forms.Label e_G_label;
        private System.Windows.Forms.Label e_B_label;
        private System.Windows.Forms.Label e_from_to;
        public System.Windows.Forms.TextBox e_R_textBox;
        public System.Windows.Forms.TextBox e_G_textBox;
        public System.Windows.Forms.TextBox e_B_textBox;
        private System.Windows.Forms.Button e_rgb_ok_button;
    }
}