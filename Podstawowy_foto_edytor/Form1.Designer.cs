namespace Podstawowy_foto_edytor
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
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.None_button = new System.Windows.Forms.Button();
            this.Red_Bar = new System.Windows.Forms.TrackBar();
            this.Green_Bar = new System.Windows.Forms.TrackBar();
            this.Blue_Bar = new System.Windows.Forms.TrackBar();
            this.Red_Name_label = new System.Windows.Forms.Label();
            this.Green_Name_label = new System.Windows.Forms.Label();
            this.Blue_Name_label = new System.Windows.Forms.Label();
            this.Contrast_Bar = new System.Windows.Forms.TrackBar();
            this.Brightness_Bar = new System.Windows.Forms.TrackBar();
            this.Brightness_Name_label = new System.Windows.Forms.Label();
            this.Contrast_Name_label = new System.Windows.Forms.Label();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.Red_Value_label = new System.Windows.Forms.Label();
            this.Green_Value_label = new System.Windows.Forms.Label();
            this.Blue_Value_label = new System.Windows.Forms.Label();
            this.Brightness_Value_label = new System.Windows.Forms.Label();
            this.Contrast_Value_label = new System.Windows.Forms.Label();
            this.Copyrights_label = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.openImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openImageToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.filtersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grayscaleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sepiaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.blurToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.negativeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.edgeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mirrorsInvertsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mirrorLeftSideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mirrorRightSideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mirrorTopSideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mirrorBottomSideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.invertHorizontalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.invertVerticalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.drawingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statisticsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.histogramToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.resizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nearestNeighbourToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.biLinearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.biCubicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Red_Bar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Green_Bar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Blue_Bar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Contrast_Bar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Brightness_Bar)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox
            // 
            this.pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox.Location = new System.Drawing.Point(116, 27);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(892, 505);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            this.pictureBox.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // None_button
            // 
            this.None_button.Location = new System.Drawing.Point(14, 600);
            this.None_button.Name = "None_button";
            this.None_button.Size = new System.Drawing.Size(69, 73);
            this.None_button.TabIndex = 1;
            this.None_button.Text = "None";
            this.None_button.UseVisualStyleBackColor = true;
            this.None_button.Click += new System.EventHandler(this.button1_Click);
            // 
            // Red_Bar
            // 
            this.Red_Bar.Location = new System.Drawing.Point(145, 556);
            this.Red_Bar.Maximum = 100;
            this.Red_Bar.Name = "Red_Bar";
            this.Red_Bar.Size = new System.Drawing.Size(741, 45);
            this.Red_Bar.TabIndex = 11;
            this.Red_Bar.ValueChanged += new System.EventHandler(this.trackBar1_ValueChanged);
            // 
            // Green_Bar
            // 
            this.Green_Bar.Location = new System.Drawing.Point(145, 607);
            this.Green_Bar.Maximum = 100;
            this.Green_Bar.Name = "Green_Bar";
            this.Green_Bar.Size = new System.Drawing.Size(741, 45);
            this.Green_Bar.TabIndex = 12;
            this.Green_Bar.ValueChanged += new System.EventHandler(this.trackBar2_ValueChanged);
            // 
            // Blue_Bar
            // 
            this.Blue_Bar.Location = new System.Drawing.Point(145, 658);
            this.Blue_Bar.Maximum = 100;
            this.Blue_Bar.Name = "Blue_Bar";
            this.Blue_Bar.Size = new System.Drawing.Size(741, 45);
            this.Blue_Bar.TabIndex = 13;
            this.Blue_Bar.ValueChanged += new System.EventHandler(this.trackBar3_ValueChanged);
            // 
            // Red_Name_label
            // 
            this.Red_Name_label.AutoSize = true;
            this.Red_Name_label.Location = new System.Drawing.Point(113, 556);
            this.Red_Name_label.Name = "Red_Name_label";
            this.Red_Name_label.Size = new System.Drawing.Size(27, 13);
            this.Red_Name_label.TabIndex = 14;
            this.Red_Name_label.Text = "Red";
            // 
            // Green_Name_label
            // 
            this.Green_Name_label.AutoSize = true;
            this.Green_Name_label.Location = new System.Drawing.Point(113, 609);
            this.Green_Name_label.Name = "Green_Name_label";
            this.Green_Name_label.Size = new System.Drawing.Size(36, 13);
            this.Green_Name_label.TabIndex = 15;
            this.Green_Name_label.Text = "Green";
            // 
            // Blue_Name_label
            // 
            this.Blue_Name_label.AutoSize = true;
            this.Blue_Name_label.Location = new System.Drawing.Point(111, 660);
            this.Blue_Name_label.Name = "Blue_Name_label";
            this.Blue_Name_label.Size = new System.Drawing.Size(28, 13);
            this.Blue_Name_label.TabIndex = 16;
            this.Blue_Name_label.Text = "Blue";
            // 
            // Contrast_Bar
            // 
            this.Contrast_Bar.Location = new System.Drawing.Point(65, 55);
            this.Contrast_Bar.Maximum = 100;
            this.Contrast_Bar.Name = "Contrast_Bar";
            this.Contrast_Bar.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.Contrast_Bar.Size = new System.Drawing.Size(45, 477);
            this.Contrast_Bar.TabIndex = 19;
            this.Contrast_Bar.Value = 25;
            this.Contrast_Bar.ValueChanged += new System.EventHandler(this.trackBar5_ValueChanged);
            // 
            // Brightness_Bar
            // 
            this.Brightness_Bar.Location = new System.Drawing.Point(14, 55);
            this.Brightness_Bar.Maximum = 100;
            this.Brightness_Bar.Name = "Brightness_Bar";
            this.Brightness_Bar.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.Brightness_Bar.Size = new System.Drawing.Size(45, 477);
            this.Brightness_Bar.TabIndex = 20;
            this.Brightness_Bar.ValueChanged += new System.EventHandler(this.trackBar6_ValueChanged);
            // 
            // Brightness_Name_label
            // 
            this.Brightness_Name_label.AutoSize = true;
            this.Brightness_Name_label.Location = new System.Drawing.Point(2, 39);
            this.Brightness_Name_label.Name = "Brightness_Name_label";
            this.Brightness_Name_label.Size = new System.Drawing.Size(56, 13);
            this.Brightness_Name_label.TabIndex = 21;
            this.Brightness_Name_label.Text = "Brightness";
            // 
            // Contrast_Name_label
            // 
            this.Contrast_Name_label.AutoSize = true;
            this.Contrast_Name_label.Location = new System.Drawing.Point(62, 39);
            this.Contrast_Name_label.Name = "Contrast_Name_label";
            this.Contrast_Name_label.Size = new System.Drawing.Size(46, 13);
            this.Contrast_Name_label.TabIndex = 22;
            this.Contrast_Name_label.Text = "Contrast";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Red_Value_label
            // 
            this.Red_Value_label.AutoSize = true;
            this.Red_Value_label.Location = new System.Drawing.Point(892, 556);
            this.Red_Value_label.Name = "Red_Value_label";
            this.Red_Value_label.Size = new System.Drawing.Size(13, 13);
            this.Red_Value_label.TabIndex = 24;
            this.Red_Value_label.Text = "0";
            // 
            // Green_Value_label
            // 
            this.Green_Value_label.AutoSize = true;
            this.Green_Value_label.Location = new System.Drawing.Point(892, 609);
            this.Green_Value_label.Name = "Green_Value_label";
            this.Green_Value_label.Size = new System.Drawing.Size(13, 13);
            this.Green_Value_label.TabIndex = 25;
            this.Green_Value_label.Text = "0";
            // 
            // Blue_Value_label
            // 
            this.Blue_Value_label.AutoSize = true;
            this.Blue_Value_label.Location = new System.Drawing.Point(892, 660);
            this.Blue_Value_label.Name = "Blue_Value_label";
            this.Blue_Value_label.Size = new System.Drawing.Size(13, 13);
            this.Blue_Value_label.TabIndex = 26;
            this.Blue_Value_label.Text = "0";
            // 
            // Brightness_Value_label
            // 
            this.Brightness_Value_label.AutoSize = true;
            this.Brightness_Value_label.Location = new System.Drawing.Point(7, 544);
            this.Brightness_Value_label.Name = "Brightness_Value_label";
            this.Brightness_Value_label.Size = new System.Drawing.Size(13, 13);
            this.Brightness_Value_label.TabIndex = 27;
            this.Brightness_Value_label.Text = "0";
            // 
            // Contrast_Value_label
            // 
            this.Contrast_Value_label.AutoSize = true;
            this.Contrast_Value_label.Location = new System.Drawing.Point(62, 544);
            this.Contrast_Value_label.Name = "Contrast_Value_label";
            this.Contrast_Value_label.Size = new System.Drawing.Size(13, 13);
            this.Contrast_Value_label.TabIndex = 28;
            this.Contrast_Value_label.Text = "0";
            // 
            // Copyrights_label
            // 
            this.Copyrights_label.AutoSize = true;
            this.Copyrights_label.Location = new System.Drawing.Point(849, 696);
            this.Copyrights_label.Name = "Copyrights_label";
            this.Copyrights_label.Size = new System.Drawing.Size(159, 13);
            this.Copyrights_label.TabIndex = 30;
            this.Copyrights_label.Text = "© Copyrights Jakub Siejak 2020";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openImageToolStripMenuItem,
            this.filtersToolStripMenuItem,
            this.mirrorsInvertsToolStripMenuItem,
            this.drawingToolStripMenuItem,
            this.helpToolStripMenuItem,
            this.statisticsToolStripMenuItem,
            this.resizeToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1020, 24);
            this.menuStrip1.TabIndex = 34;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // openImageToolStripMenuItem
            // 
            this.openImageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveImageToolStripMenuItem,
            this.openImageToolStripMenuItem1});
            this.openImageToolStripMenuItem.Name = "openImageToolStripMenuItem";
            this.openImageToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.openImageToolStripMenuItem.Text = "File";
            // 
            // saveImageToolStripMenuItem
            // 
            this.saveImageToolStripMenuItem.Name = "saveImageToolStripMenuItem";
            this.saveImageToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.saveImageToolStripMenuItem.Text = "Save image";
            this.saveImageToolStripMenuItem.Click += new System.EventHandler(this.saveImageToolStripMenuItem_Click);
            // 
            // openImageToolStripMenuItem1
            // 
            this.openImageToolStripMenuItem1.Name = "openImageToolStripMenuItem1";
            this.openImageToolStripMenuItem1.Size = new System.Drawing.Size(139, 22);
            this.openImageToolStripMenuItem1.Text = "Open image";
            this.openImageToolStripMenuItem1.Click += new System.EventHandler(this.openImageToolStripMenuItem1_Click);
            // 
            // filtersToolStripMenuItem
            // 
            this.filtersToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.grayscaleToolStripMenuItem,
            this.sepiaToolStripMenuItem,
            this.blurToolStripMenuItem,
            this.negativeToolStripMenuItem,
            this.edgeToolStripMenuItem});
            this.filtersToolStripMenuItem.Name = "filtersToolStripMenuItem";
            this.filtersToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.filtersToolStripMenuItem.Text = "Filters";
            // 
            // grayscaleToolStripMenuItem
            // 
            this.grayscaleToolStripMenuItem.Name = "grayscaleToolStripMenuItem";
            this.grayscaleToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.grayscaleToolStripMenuItem.Text = "Grayscale";
            this.grayscaleToolStripMenuItem.Click += new System.EventHandler(this.grayscaleToolStripMenuItem_Click);
            // 
            // sepiaToolStripMenuItem
            // 
            this.sepiaToolStripMenuItem.Name = "sepiaToolStripMenuItem";
            this.sepiaToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.sepiaToolStripMenuItem.Text = "Sepia";
            this.sepiaToolStripMenuItem.Click += new System.EventHandler(this.sepiaToolStripMenuItem_Click);
            // 
            // blurToolStripMenuItem
            // 
            this.blurToolStripMenuItem.Name = "blurToolStripMenuItem";
            this.blurToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.blurToolStripMenuItem.Text = "Blur";
            this.blurToolStripMenuItem.Click += new System.EventHandler(this.blurToolStripMenuItem_Click);
            // 
            // negativeToolStripMenuItem
            // 
            this.negativeToolStripMenuItem.Name = "negativeToolStripMenuItem";
            this.negativeToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.negativeToolStripMenuItem.Text = "Negative";
            this.negativeToolStripMenuItem.Click += new System.EventHandler(this.negativeToolStripMenuItem_Click);
            // 
            // edgeToolStripMenuItem
            // 
            this.edgeToolStripMenuItem.Name = "edgeToolStripMenuItem";
            this.edgeToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.edgeToolStripMenuItem.Text = "Edge(black on white)";
            this.edgeToolStripMenuItem.Click += new System.EventHandler(this.edgeToolStripMenuItem_Click);
            // 
            // mirrorsInvertsToolStripMenuItem
            // 
            this.mirrorsInvertsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mirrorLeftSideToolStripMenuItem,
            this.mirrorRightSideToolStripMenuItem,
            this.mirrorTopSideToolStripMenuItem,
            this.mirrorBottomSideToolStripMenuItem,
            this.invertHorizontalToolStripMenuItem,
            this.invertVerticalToolStripMenuItem});
            this.mirrorsInvertsToolStripMenuItem.Name = "mirrorsInvertsToolStripMenuItem";
            this.mirrorsInvertsToolStripMenuItem.Size = new System.Drawing.Size(97, 20);
            this.mirrorsInvertsToolStripMenuItem.Text = "Mirrors/Inverts";
            // 
            // mirrorLeftSideToolStripMenuItem
            // 
            this.mirrorLeftSideToolStripMenuItem.Name = "mirrorLeftSideToolStripMenuItem";
            this.mirrorLeftSideToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.mirrorLeftSideToolStripMenuItem.Text = "Mirror left side";
            this.mirrorLeftSideToolStripMenuItem.Click += new System.EventHandler(this.mirrorLeftSideToolStripMenuItem_Click);
            // 
            // mirrorRightSideToolStripMenuItem
            // 
            this.mirrorRightSideToolStripMenuItem.Name = "mirrorRightSideToolStripMenuItem";
            this.mirrorRightSideToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.mirrorRightSideToolStripMenuItem.Text = "Mirror right side";
            this.mirrorRightSideToolStripMenuItem.Click += new System.EventHandler(this.mirrorRightSideToolStripMenuItem_Click);
            // 
            // mirrorTopSideToolStripMenuItem
            // 
            this.mirrorTopSideToolStripMenuItem.Name = "mirrorTopSideToolStripMenuItem";
            this.mirrorTopSideToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.mirrorTopSideToolStripMenuItem.Text = "Mirror top side";
            this.mirrorTopSideToolStripMenuItem.Click += new System.EventHandler(this.mirrorTopSideToolStripMenuItem_Click);
            // 
            // mirrorBottomSideToolStripMenuItem
            // 
            this.mirrorBottomSideToolStripMenuItem.Name = "mirrorBottomSideToolStripMenuItem";
            this.mirrorBottomSideToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.mirrorBottomSideToolStripMenuItem.Text = "Mirror bottom side";
            this.mirrorBottomSideToolStripMenuItem.Click += new System.EventHandler(this.mirrorBottomSideToolStripMenuItem_Click);
            // 
            // invertHorizontalToolStripMenuItem
            // 
            this.invertHorizontalToolStripMenuItem.Name = "invertHorizontalToolStripMenuItem";
            this.invertHorizontalToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.invertHorizontalToolStripMenuItem.Text = "Invert horizontal";
            this.invertHorizontalToolStripMenuItem.Click += new System.EventHandler(this.invertHorizontalToolStripMenuItem_Click);
            // 
            // invertVerticalToolStripMenuItem
            // 
            this.invertVerticalToolStripMenuItem.Name = "invertVerticalToolStripMenuItem";
            this.invertVerticalToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.invertVerticalToolStripMenuItem.Text = "Invert vertical";
            this.invertVerticalToolStripMenuItem.Click += new System.EventHandler(this.invertVerticalToolStripMenuItem_Click);
            // 
            // drawingToolStripMenuItem
            // 
            this.drawingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.colorToolStripMenuItem});
            this.drawingToolStripMenuItem.Name = "drawingToolStripMenuItem";
            this.drawingToolStripMenuItem.Size = new System.Drawing.Size(63, 20);
            this.drawingToolStripMenuItem.Text = "Drawing";
            // 
            // colorToolStripMenuItem
            // 
            this.colorToolStripMenuItem.Name = "colorToolStripMenuItem";
            this.colorToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.colorToolStripMenuItem.Text = "Color";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // statisticsToolStripMenuItem
            // 
            this.statisticsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.histogramToolStripMenuItem});
            this.statisticsToolStripMenuItem.Name = "statisticsToolStripMenuItem";
            this.statisticsToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.statisticsToolStripMenuItem.Text = "Statistics";
            // 
            // histogramToolStripMenuItem
            // 
            this.histogramToolStripMenuItem.Name = "histogramToolStripMenuItem";
            this.histogramToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.histogramToolStripMenuItem.Text = "Histogram";
            this.histogramToolStripMenuItem.Click += new System.EventHandler(this.histogramToolStripMenuItem_Click);
            // 
            // resizeToolStripMenuItem
            // 
            this.resizeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nearestNeighbourToolStripMenuItem,
            this.biLinearToolStripMenuItem,
            this.biCubicToolStripMenuItem});
            this.resizeToolStripMenuItem.Name = "resizeToolStripMenuItem";
            this.resizeToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
            this.resizeToolStripMenuItem.Text = "Resize";
            // 
            // nearestNeighbourToolStripMenuItem
            // 
            this.nearestNeighbourToolStripMenuItem.Name = "nearestNeighbourToolStripMenuItem";
            this.nearestNeighbourToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.nearestNeighbourToolStripMenuItem.Text = "Nearest neighbour";
            // 
            // biLinearToolStripMenuItem
            // 
            this.biLinearToolStripMenuItem.Name = "biLinearToolStripMenuItem";
            this.biLinearToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.biLinearToolStripMenuItem.Text = "BiLinear";
            // 
            // biCubicToolStripMenuItem
            // 
            this.biCubicToolStripMenuItem.Name = "biCubicToolStripMenuItem";
            this.biCubicToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.biCubicToolStripMenuItem.Text = "BiCubic";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1020, 718);
            this.Controls.Add(this.Copyrights_label);
            this.Controls.Add(this.Contrast_Value_label);
            this.Controls.Add(this.Brightness_Value_label);
            this.Controls.Add(this.Blue_Value_label);
            this.Controls.Add(this.Green_Value_label);
            this.Controls.Add(this.Red_Value_label);
            this.Controls.Add(this.Contrast_Name_label);
            this.Controls.Add(this.Brightness_Name_label);
            this.Controls.Add(this.Brightness_Bar);
            this.Controls.Add(this.Contrast_Bar);
            this.Controls.Add(this.Blue_Name_label);
            this.Controls.Add(this.Green_Name_label);
            this.Controls.Add(this.Red_Name_label);
            this.Controls.Add(this.Blue_Bar);
            this.Controls.Add(this.Green_Bar);
            this.Controls.Add(this.Red_Bar);
            this.Controls.Add(this.None_button);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Red_Bar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Green_Bar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Blue_Bar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Contrast_Bar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Brightness_Bar)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Button None_button;
        private System.Windows.Forms.TrackBar Red_Bar;
        private System.Windows.Forms.TrackBar Green_Bar;
        private System.Windows.Forms.TrackBar Blue_Bar;
        private System.Windows.Forms.Label Red_Name_label;
        private System.Windows.Forms.Label Green_Name_label;
        private System.Windows.Forms.Label Blue_Name_label;
        private System.Windows.Forms.TrackBar Contrast_Bar;
        private System.Windows.Forms.TrackBar Brightness_Bar;
        private System.Windows.Forms.Label Brightness_Name_label;
        private System.Windows.Forms.Label Contrast_Name_label;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label Red_Value_label;
        private System.Windows.Forms.Label Green_Value_label;
        private System.Windows.Forms.Label Blue_Value_label;
        private System.Windows.Forms.Label Brightness_Value_label;
        private System.Windows.Forms.Label Contrast_Value_label;
        private System.Windows.Forms.Label Copyrights_label;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem openImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openImageToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem filtersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem drawingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem grayscaleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sepiaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem blurToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem negativeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem edgeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mirrorsInvertsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mirrorLeftSideToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mirrorRightSideToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mirrorTopSideToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mirrorBottomSideToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem invertHorizontalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem invertVerticalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem colorToolStripMenuItem;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.ToolStripMenuItem statisticsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem histogramToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nearestNeighbourToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem biLinearToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem biCubicToolStripMenuItem;
    }
}

