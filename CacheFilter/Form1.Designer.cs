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
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(50, 118);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tileurl_tbx
            // 
            this.tileurl_tbx.Location = new System.Drawing.Point(50, 21);
            this.tileurl_tbx.Name = "tileurl_tbx";
            this.tileurl_tbx.Size = new System.Drawing.Size(575, 21);
            this.tileurl_tbx.TabIndex = 1;
            this.tileurl_tbx.Text = "https://sde.esrigz.com:6443/arcgis/rest/services/refdemo/ImageServer/tile/0/218/1" +
    "72";
            // 
            // cacheRoot_tbx
            // 
            this.cacheRoot_tbx.Location = new System.Drawing.Point(50, 68);
            this.cacheRoot_tbx.Name = "cacheRoot_tbx";
            this.cacheRoot_tbx.Size = new System.Drawing.Size(575, 21);
            this.cacheRoot_tbx.TabIndex = 2;
            this.cacheRoot_tbx.Text = "D:\\Research\\瓦片读取研究\\refdemo_ImageServer\\_alllayers";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(665, 155);
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
    }
}

