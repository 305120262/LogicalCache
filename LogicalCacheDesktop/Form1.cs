﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;
using LogicalCacheLibrary;

//参考 https://github.com/Esri/raster-tiles-compactcache/blob/master/CompactCacheV2.md

namespace LogicalCacheDesktop
{

    public partial class Form1 : Form
    {
        LogicalCache logCache = new LogicalCache();

        public Form1()
        {
            InitializeComponent();
        }

        //Export Tile
        private void button1_Click(object sender, EventArgs e)
        {
            int level=0, row=0, col=0;
            if (scale_tbx.Text!="" && x_tbx.Text != "" && y_tbx.Text != "")
            {
                TileInfo tile = logCache.schema.GetTileInfoFromXY(double.Parse(scale_tbx.Text), double.Parse(x_tbx.Text), double.Parse(y_tbx.Text));
                level = tile.Level;
                row = tile.Row;
                col = tile.Column;
            }
            else if(level_tbx.Text!="" && row_tbx.Text!="" && column_tbx.Text!="")
            {
                level = int.Parse(level_tbx.Text);
                row = int.Parse(row_tbx.Text);
                col = int.Parse(column_tbx.Text);
            }
            else if (tileurl_tbx.Text != "")
            {
                Match match = Regex.Match(tileurl_tbx.Text, @"\d+\/\d+\/\d+$");
                string[] components = match.Value.Split('/');
                int[] components_int = Array.ConvertAll<string, int>(components, new Converter<string, int>(Str2int));
                level = components_int[0];
                row = components_int[1];
                col = components_int[2];
            }

            byte[] data = logCache.GetTileBytes( level, row, col);
            if (data == null)
            {
                MessageBox.Show("Tile Not Exist!");
                return;
            }
            //Save to file
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "*.jpeg|jpeg";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                logCache.SaveImage(data, dlg.FileName);
                MessageBox.Show("OK");
            }

        }

        private int Str2int(string s)
        {
            return int.Parse(s);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                this.cacheRoot_tbx.Text = dlg.SelectedPath;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "*.txt|text";
            dlg.DefaultExt = "txt";
            dlg.FileName = "result.txt";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                this.progressBar1.Style = ProgressBarStyle.Marquee;
                logCache.r = int.Parse(r_tbx.Text);
                logCache.g = int.Parse(g_tbx.Text);
                logCache.b = int.Parse(b_tbx.Text);
                
                var result = logCache.CheckTiles(this.cacheRoot_tbx.Text, TileCheckMode.Whole);
                this.progressBar1.Style = ProgressBarStyle.Blocks;
                using (StreamWriter writer = new StreamWriter(dlg.FileName))
                {
                    writer.WriteLine(result);
                    writer.Flush();
                    MessageBox.Show("Completed.");
                }

            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "*.txt|text";
            dlg.DefaultExt = "txt";
            dlg.FileName = "result.txt";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                this.progressBar1.Style = ProgressBarStyle.Marquee;
                logCache.h_max = int.Parse(strip_h_max_tbx.Text);
                logCache.w_max = int.Parse(strip_w_max_tbx.Text);
                logCache.h_min = int.Parse(strip_h_min_tbx.Text);
                logCache.w_min = int.Parse(strip_w_min_tbx.Text);
                var result = logCache.CheckTiles(this.cacheRoot_tbx.Text, TileCheckMode.Strip);
                this.progressBar1.Style = ProgressBarStyle.Blocks;
                using (StreamWriter writer = new StreamWriter(dlg.FileName))
                {
                    writer.WriteLine(result);
                    writer.Flush();
                    MessageBox.Show("Completed.");
                }

            }
        }

        //Preview Tile
        private void button5_Click(object sender, EventArgs e)
        {
            int level = 0, row = 0, col = 0;
            if (scale_tbx.Text != "" && x_tbx.Text != "" && y_tbx.Text != "")
            {
                TileInfo tile = logCache.schema.GetTileInfoFromXY(double.Parse(scale_tbx.Text), double.Parse(x_tbx.Text), double.Parse(y_tbx.Text));
                level = tile.Level;
                row = tile.Row;
                col = tile.Column;

                this.level_tbx.Text = level.ToString();
                this.row_tbx.Text = row.ToString();
                this.column_tbx.Text = col.ToString();
            }
            else if (level_tbx.Text != "" && row_tbx.Text != "" && column_tbx.Text != "")
            {
                level = int.Parse(level_tbx.Text);
                row = int.Parse(row_tbx.Text);
                col = int.Parse(column_tbx.Text);
            }
            else if (tileurl_tbx.Text != "")
            {
                Match match = Regex.Match(tileurl_tbx.Text, @"\d+\/\d+\/\d+$");
                string[] components = match.Value.Split('/');
                int[] components_int = Array.ConvertAll<string, int>(components, new Converter<string, int>(Str2int));
                level = components_int[0];
                row = components_int[1];
                col = components_int[2];
            }
            byte[] data = logCache.GetTileBytes( level, row, col);
            if(data==null)
            {
                MessageBox.Show("Tile Not Exist!");
                return;
            }
            //Preview in picturebox
            this.tile_picbox.Image = Image.FromStream(new MemoryStream(data));
        }

        //Check Tile
        private void button6_Click(object sender, EventArgs e)
        {
            logCache.mask_name = this.maskName_tbx.Text;
            logCache.CheckTiles(this.maskName_tbx.Text, TileCheckMode.Exist);
            MessageBox.Show("Completed.");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            maskName_lstbox.DataSource = logCache.GetMasks();
        }

        //Connect Mask DB
        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                logCache.ConnectRegisterDB(this.redisSvr_tbx.Text);
                MessageBox.Show("Connected.");
                maskName_lstbox.DataSource = logCache.GetMasks();
                maskName_cbx.DataSource = logCache.GetMasks();
            }
            catch
            { }
        }

        //Build Mask
        private void button8_Click(object sender, EventArgs e)
        {
            List<int> levels = new List<int>();
            if (maskLevels_tbx.Text!="")
            {
                levels= new List<string>(maskLevels_tbx.Text.Split(',')).ConvertAll(new Converter<string, int>(Str2int));
            }
            logCache.BuildMask(this.maskShape_tbx.Text, this.maskName_tbx.Text, this.maskPosition_cbx.Text,levels);
            MessageBox.Show("Completed.");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "*.shp|Shapefile";
            if(dlg.ShowDialog()==DialogResult.OK)
            {
                this.maskShape_tbx.Text = dlg.FileName;
            }
        }

        //Process Tiles
        private void button7_Click(object sender, EventArgs e)
        {
            logCache.ProcessTiles(cacheRoot_tbx.Text,destCacheRoot_tbx.Text,maskName_cbx.Text,processor_tbx.Text);
        }

        //Set Current Cache
        private void cacheRoot_tbx_TextChanged(object sender, EventArgs e)
        {
            logCache.SetCurrentCache(this.cacheRoot_tbx.Text);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            foreach(var i in maskName_lstbox.SelectedItems)
            {
                logCache.DropMask(i.ToString());
            }
            maskName_lstbox.DataSource = logCache.GetMasks();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            logCache.SetCurrentCache(this.cacheRoot_tbx.Text);
        }
    }
}
