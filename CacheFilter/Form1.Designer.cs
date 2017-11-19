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
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(639, 105);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Export";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tileurl_tbx
            // 
            this.tileurl_tbx.Location = new System.Drawing.Point(58, 107);
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
            this.button3.Text = "浏览";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(178, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(11, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "X";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(338, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "Y";
            // 
            // x_tbx
            // 
            this.x_tbx.Location = new System.Drawing.Point(208, 59);
            this.x_tbx.Name = "x_tbx";
            this.x_tbx.Size = new System.Drawing.Size(100, 21);
            this.x_tbx.TabIndex = 7;
            // 
            // y_tbx
            // 
            this.y_tbx.Location = new System.Drawing.Point(370, 59);
            this.y_tbx.Name = "y_tbx";
            this.y_tbx.Size = new System.Drawing.Size(100, 21);
            this.y_tbx.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 110);
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
            this.label5.Location = new System.Drawing.Point(3, 63);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 12);
            this.label5.TabIndex = 11;
            this.label5.Text = "Scale";
            // 
            // scale_tbx
            // 
            this.scale_tbx.Location = new System.Drawing.Point(57, 59);
            this.scale_tbx.Name = "scale_tbx";
            this.scale_tbx.Size = new System.Drawing.Size(100, 21);
            this.scale_tbx.TabIndex = 12;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(515, 144);
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
            this.label6.Location = new System.Drawing.Point(4, 150);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(23, 12);
            this.label6.TabIndex = 14;
            this.label6.Text = "Red";
            // 
            // r_tbx
            // 
            this.r_tbx.Location = new System.Drawing.Point(55, 146);
            this.r_tbx.Name = "r_tbx";
            this.r_tbx.Size = new System.Drawing.Size(100, 21);
            this.r_tbx.TabIndex = 15;
            this.r_tbx.Text = "254";
            // 
            // g_tbx
            // 
            this.g_tbx.Location = new System.Drawing.Point(216, 146);
            this.g_tbx.Name = "g_tbx";
            this.g_tbx.Size = new System.Drawing.Size(100, 21);
            this.g_tbx.TabIndex = 16;
            this.g_tbx.Text = "254";
            // 
            // b_tbx
            // 
            this.b_tbx.Location = new System.Drawing.Point(377, 146);
            this.b_tbx.Name = "b_tbx";
            this.b_tbx.Size = new System.Drawing.Size(100, 21);
            this.b_tbx.TabIndex = 17;
            this.b_tbx.Text = "254";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(165, 150);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 12);
            this.label7.TabIndex = 18;
            this.label7.Text = "Green";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(326, 150);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 12);
            this.label8.TabIndex = 19;
            this.label8.Text = "Blue";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(4, 187);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(71, 12);
            this.label9.TabIndex = 20;
            this.label9.Text = "strip_h_min";
            // 
            // strip_h_min_tbx
            // 
            this.strip_h_min_tbx.Location = new System.Drawing.Point(86, 183);
            this.strip_h_min_tbx.Name = "strip_h_min_tbx";
            this.strip_h_min_tbx.Size = new System.Drawing.Size(100, 21);
            this.strip_h_min_tbx.TabIndex = 21;
            this.strip_h_min_tbx.Text = "250";
            // 
            // strip_w_min_tbx
            // 
            this.strip_w_min_tbx.Location = new System.Drawing.Point(88, 220);
            this.strip_w_min_tbx.Name = "strip_w_min_tbx";
            this.strip_w_min_tbx.Size = new System.Drawing.Size(100, 21);
            this.strip_w_min_tbx.TabIndex = 22;
            this.strip_w_min_tbx.Text = "2";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 224);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(71, 12);
            this.label10.TabIndex = 23;
            this.label10.Text = "strip_w_min";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(515, 224);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(130, 23);
            this.button4.TabIndex = 24;
            this.button4.Text = "Check Strip Tiles";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // strip_h_max_tbx
            // 
            this.strip_h_max_tbx.Location = new System.Drawing.Point(279, 183);
            this.strip_h_max_tbx.Name = "strip_h_max_tbx";
            this.strip_h_max_tbx.Size = new System.Drawing.Size(100, 21);
            this.strip_h_max_tbx.TabIndex = 25;
            this.strip_h_max_tbx.Text = "256";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(197, 187);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(71, 12);
            this.label11.TabIndex = 26;
            this.label11.Text = "strip_h_max";
            // 
            // strip_w_max_tbx
            // 
            this.strip_w_max_tbx.Location = new System.Drawing.Point(277, 220);
            this.strip_w_max_tbx.Name = "strip_w_max_tbx";
            this.strip_w_max_tbx.Size = new System.Drawing.Size(100, 21);
            this.strip_w_max_tbx.TabIndex = 27;
            this.strip_w_max_tbx.Text = "50";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(197, 224);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(71, 12);
            this.label12.TabIndex = 28;
            this.label12.Text = "strip_w_max";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(720, 303);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.strip_w_max_tbx);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.strip_h_max_tbx);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.strip_w_min_tbx);
            this.Controls.Add(this.strip_h_min_tbx);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.b_tbx);
            this.Controls.Add(this.g_tbx);
            this.Controls.Add(this.r_tbx);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.scale_tbx);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.y_tbx);
            this.Controls.Add(this.x_tbx);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.cacheRoot_tbx);
            this.Controls.Add(this.tileurl_tbx);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "ArcGIS Compact CacheV2 ";
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
    }
}

