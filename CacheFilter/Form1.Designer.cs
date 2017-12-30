namespace CacheFilter
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
            this.button1 = new System.Windows.Forms.Button();
            this.tileurl_tbx = new System.Windows.Forms.TextBox();
            this.cacheRoot_tbx = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.x_tbx = new System.Windows.Forms.TextBox();
            this.y_tbx = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.scale_tbx = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.r_tbx = new System.Windows.Forms.TextBox();
            this.g_tbx = new System.Windows.Forms.TextBox();
            this.b_tbx = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.strip_h_min_tbx = new System.Windows.Forms.TextBox();
            this.strip_w_min_tbx = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.strip_h_max_tbx = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.strip_w_max_tbx = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.level_tbx = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label16 = new System.Windows.Forms.Label();
            this.row_tbx = new System.Windows.Forms.TextBox();
            this.column_tbx = new System.Windows.Forms.TextBox();
            this.tile_picbox = new System.Windows.Forms.PictureBox();
            this.button5 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button6 = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cacheName_tbx = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.redisSvr_tbx = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.tile_picbox)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(915, 303);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Export";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tileurl_tbx
            // 
            this.tileurl_tbx.Location = new System.Drawing.Point(82, 123);
            this.tileurl_tbx.Name = "tileurl_tbx";
            this.tileurl_tbx.Size = new System.Drawing.Size(575, 21);
            this.tileurl_tbx.TabIndex = 1;
            this.tileurl_tbx.Text = "https://sde.esrigz.com:6443/arcgis/rest/services/refdemo/ImageServer/tile/0/218/1" +
    "72";
            // 
            // cacheRoot_tbx
            // 
            this.cacheRoot_tbx.Location = new System.Drawing.Point(58, 21);
            this.cacheRoot_tbx.Name = "cacheRoot_tbx";
            this.cacheRoot_tbx.Size = new System.Drawing.Size(575, 21);
            this.cacheRoot_tbx.TabIndex = 2;
            this.cacheRoot_tbx.Text = "D:\\AppDatas\\arcgisserver\\directories\\arcgiscache\\WorldMap\\Layers";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(639, 21);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 4;
            this.button3.Text = "Browse";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(203, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(11, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "X";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(363, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "Y";
            // 
            // x_tbx
            // 
            this.x_tbx.Location = new System.Drawing.Point(233, 33);
            this.x_tbx.Name = "x_tbx";
            this.x_tbx.Size = new System.Drawing.Size(100, 21);
            this.x_tbx.TabIndex = 7;
            // 
            // y_tbx
            // 
            this.y_tbx.Location = new System.Drawing.Point(395, 33);
            this.y_tbx.Name = "y_tbx";
            this.y_tbx.Size = new System.Drawing.Size(100, 21);
            this.y_tbx.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(40, 126);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "URL";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "Cache";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(28, 37);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 12);
            this.label5.TabIndex = 11;
            this.label5.Text = "Scale";
            // 
            // scale_tbx
            // 
            this.scale_tbx.Location = new System.Drawing.Point(82, 33);
            this.scale_tbx.Name = "scale_tbx";
            this.scale_tbx.Size = new System.Drawing.Size(100, 21);
            this.scale_tbx.TabIndex = 12;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(584, 286);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(130, 23);
            this.button2.TabIndex = 13;
            this.button2.Text = "Check Blank Tiles";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(31, 34);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(23, 12);
            this.label6.TabIndex = 14;
            this.label6.Text = "Red";
            // 
            // r_tbx
            // 
            this.r_tbx.Location = new System.Drawing.Point(82, 30);
            this.r_tbx.Name = "r_tbx";
            this.r_tbx.Size = new System.Drawing.Size(100, 21);
            this.r_tbx.TabIndex = 15;
            this.r_tbx.Text = "115";
            // 
            // g_tbx
            // 
            this.g_tbx.Location = new System.Drawing.Point(243, 30);
            this.g_tbx.Name = "g_tbx";
            this.g_tbx.Size = new System.Drawing.Size(100, 21);
            this.g_tbx.TabIndex = 16;
            this.g_tbx.Text = "223";
            // 
            // b_tbx
            // 
            this.b_tbx.Location = new System.Drawing.Point(404, 30);
            this.b_tbx.Name = "b_tbx";
            this.b_tbx.Size = new System.Drawing.Size(100, 21);
            this.b_tbx.TabIndex = 17;
            this.b_tbx.Text = "255";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(192, 34);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 12);
            this.label7.TabIndex = 18;
            this.label7.Text = "Green";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(353, 34);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 12);
            this.label8.TabIndex = 19;
            this.label8.Text = "Blue";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(31, 71);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(71, 12);
            this.label9.TabIndex = 20;
            this.label9.Text = "strip_h_min";
            // 
            // strip_h_min_tbx
            // 
            this.strip_h_min_tbx.Location = new System.Drawing.Point(113, 67);
            this.strip_h_min_tbx.Name = "strip_h_min_tbx";
            this.strip_h_min_tbx.Size = new System.Drawing.Size(100, 21);
            this.strip_h_min_tbx.TabIndex = 21;
            this.strip_h_min_tbx.Text = "250";
            // 
            // strip_w_min_tbx
            // 
            this.strip_w_min_tbx.Location = new System.Drawing.Point(115, 104);
            this.strip_w_min_tbx.Name = "strip_w_min_tbx";
            this.strip_w_min_tbx.Size = new System.Drawing.Size(100, 21);
            this.strip_w_min_tbx.TabIndex = 22;
            this.strip_w_min_tbx.Text = "2";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(30, 108);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(71, 12);
            this.label10.TabIndex = 23;
            this.label10.Text = "strip_w_min";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(584, 334);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(130, 23);
            this.button4.TabIndex = 24;
            this.button4.Text = "Check Strip Tiles";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // strip_h_max_tbx
            // 
            this.strip_h_max_tbx.Location = new System.Drawing.Point(306, 67);
            this.strip_h_max_tbx.Name = "strip_h_max_tbx";
            this.strip_h_max_tbx.Size = new System.Drawing.Size(100, 21);
            this.strip_h_max_tbx.TabIndex = 25;
            this.strip_h_max_tbx.Text = "256";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(224, 71);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(71, 12);
            this.label11.TabIndex = 26;
            this.label11.Text = "strip_h_max";
            // 
            // strip_w_max_tbx
            // 
            this.strip_w_max_tbx.Location = new System.Drawing.Point(304, 104);
            this.strip_w_max_tbx.Name = "strip_w_max_tbx";
            this.strip_w_max_tbx.Size = new System.Drawing.Size(100, 21);
            this.strip_w_max_tbx.TabIndex = 27;
            this.strip_w_max_tbx.Text = "50";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(224, 108);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(71, 12);
            this.label12.TabIndex = 28;
            this.label12.Text = "strip_w_max";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(29, 84);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(35, 12);
            this.label13.TabIndex = 29;
            this.label13.Text = "Level";
            // 
            // level_tbx
            // 
            this.level_tbx.Location = new System.Drawing.Point(82, 80);
            this.level_tbx.Name = "level_tbx";
            this.level_tbx.Size = new System.Drawing.Size(100, 21);
            this.level_tbx.TabIndex = 30;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(191, 84);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(23, 12);
            this.label14.TabIndex = 31;
            this.label14.Text = "Row";
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar1.Location = new System.Drawing.Point(0, 547);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(1054, 23);
            this.progressBar1.TabIndex = 33;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(350, 84);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(41, 12);
            this.label16.TabIndex = 35;
            this.label16.Text = "Column";
            // 
            // row_tbx
            // 
            this.row_tbx.Location = new System.Drawing.Point(233, 80);
            this.row_tbx.Name = "row_tbx";
            this.row_tbx.Size = new System.Drawing.Size(100, 21);
            this.row_tbx.TabIndex = 36;
            // 
            // column_tbx
            // 
            this.column_tbx.Location = new System.Drawing.Point(395, 80);
            this.column_tbx.Name = "column_tbx";
            this.column_tbx.Size = new System.Drawing.Size(100, 21);
            this.column_tbx.TabIndex = 37;
            // 
            // tile_picbox
            // 
            this.tile_picbox.Location = new System.Drawing.Point(758, 21);
            this.tile_picbox.Name = "tile_picbox";
            this.tile_picbox.Size = new System.Drawing.Size(256, 256);
            this.tile_picbox.TabIndex = 38;
            this.tile_picbox.TabStop = false;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(788, 303);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 39;
            this.button5.Text = "Preview";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tileurl_tbx);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.column_tbx);
            this.groupBox1.Controls.Add(this.x_tbx);
            this.groupBox1.Controls.Add(this.row_tbx);
            this.groupBox1.Controls.Add(this.y_tbx);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.scale_tbx);
            this.groupBox1.Controls.Add(this.level_tbx);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Location = new System.Drawing.Point(9, 60);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(681, 165);
            this.groupBox1.TabIndex = 40;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Location";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.g_tbx);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.r_tbx);
            this.groupBox2.Controls.Add(this.b_tbx);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.strip_w_max_tbx);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.strip_h_min_tbx);
            this.groupBox2.Controls.Add(this.strip_h_max_tbx);
            this.groupBox2.Controls.Add(this.strip_w_min_tbx);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Location = new System.Drawing.Point(13, 243);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(551, 153);
            this.groupBox2.TabIndex = 41;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Check Tile Settings";
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(584, 442);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(130, 23);
            this.button6.TabIndex = 42;
            this.button6.Text = "Load to Logical";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cacheName_tbx);
            this.groupBox3.Controls.Add(this.label17);
            this.groupBox3.Controls.Add(this.redisSvr_tbx);
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Location = new System.Drawing.Point(13, 415);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(551, 108);
            this.groupBox3.TabIndex = 43;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Logical Cache Builder";
            // 
            // cacheName_tbx
            // 
            this.cacheName_tbx.Location = new System.Drawing.Point(108, 70);
            this.cacheName_tbx.Name = "cacheName_tbx";
            this.cacheName_tbx.Size = new System.Drawing.Size(187, 21);
            this.cacheName_tbx.TabIndex = 3;
            this.cacheName_tbx.Text = "WorldMap";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(25, 73);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(65, 12);
            this.label17.TabIndex = 2;
            this.label17.Text = "Cache Name";
            // 
            // redisSvr_tbx
            // 
            this.redisSvr_tbx.Location = new System.Drawing.Point(107, 27);
            this.redisSvr_tbx.Name = "redisSvr_tbx";
            this.redisSvr_tbx.Size = new System.Drawing.Size(397, 21);
            this.redisSvr_tbx.TabIndex = 1;
            this.redisSvr_tbx.Text = "127.0.0.1:6379";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(24, 32);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(77, 12);
            this.label15.TabIndex = 0;
            this.label15.Text = "Redis Server";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1054, 570);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.tile_picbox);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.cacheRoot_tbx);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "ArcGIS Compact CacheV2 ";
            ((System.ComponentModel.ISupportInitialize)(this.tile_picbox)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox tileurl_tbx;
        private System.Windows.Forms.TextBox cacheRoot_tbx;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox x_tbx;
        private System.Windows.Forms.TextBox y_tbx;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox scale_tbx;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox r_tbx;
        private System.Windows.Forms.TextBox g_tbx;
        private System.Windows.Forms.TextBox b_tbx;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox strip_h_min_tbx;
        private System.Windows.Forms.TextBox strip_w_min_tbx;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox strip_h_max_tbx;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox strip_w_max_tbx;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox level_tbx;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox row_tbx;
        private System.Windows.Forms.TextBox column_tbx;
        private System.Windows.Forms.PictureBox tile_picbox;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox cacheName_tbx;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox redisSvr_tbx;
        private System.Windows.Forms.Label label15;
    }
}

