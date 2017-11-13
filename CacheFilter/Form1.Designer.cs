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
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(24, 156);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "导出";
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
            this.cacheRoot_tbx.Text = "D:\\Research\\瓦片读取研究\\refdemo_ImageServer";
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(724, 268);
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
    }
}

