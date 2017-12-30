using System;
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
using ImageProcessor;
using StackExchange.Redis;

//参考 https://github.com/Esri/raster-tiles-compactcache/blob/master/CompactCacheV2.md

namespace CacheFilter
{
    enum TileCheckMode
    {
        Whole,
        Strip,
        Exist
    }
    public partial class Form1 : Form
    {
        const int BUNDLX_MAXIDX = 128;
        const int COMPACT_CACHE_HEADER_LENGTH = 64;
        const String BUNDLE_EXT = ".bundle";
        ConnectionMultiplexer redisConnection;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int level=0, row=0, col=0;
            if (scale_tbx.Text!="" && x_tbx.Text != "" && y_tbx.Text != "")
            {
                CacheInfo cache = new CacheInfo();
                cache.LoadFromSchemaFile(this.cacheRoot_tbx.Text + @"\conf.xml");
                TileInfo tile = cache.GetTileInfoFromXY(double.Parse(scale_tbx.Text), double.Parse(x_tbx.Text), double.Parse(y_tbx.Text));
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
            string bundleFilePath = BuildBundleFilePath(cacheRoot_tbx.Text, level, row, col);
            byte[] data = GetTileBytes(bundleFilePath, level, row, col);
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
                SaveImage(data, dlg.FileName);
            }

        }

        private int Str2int(string s)
        {
            return int.Parse(s);
        }

        protected String BuildBundleFilePath(string pathToCacheRoot, int zoom, int row, int col)
        {
            StringBuilder bundlePath = new StringBuilder(pathToCacheRoot + @"\_alllayers");

            int baseRow = (row / BUNDLX_MAXIDX) * BUNDLX_MAXIDX;
            int baseCol = (col / BUNDLX_MAXIDX) * BUNDLX_MAXIDX;

            String zoomStr = zoom.ToString();
            if (zoomStr.Length < 2)
                zoomStr = "0" + zoomStr;

            StringBuilder rowStr = new StringBuilder(baseRow.ToString("X"));
            StringBuilder colStr = new StringBuilder(baseCol.ToString("X"));

            // column and rows are at least 4 characters long
            const int padding = 4;

            while (colStr.Length < padding)
                colStr.Insert(0, "0");

            while (rowStr.Length < padding)
                rowStr.Insert(0, "0");

            if (!pathToCacheRoot.EndsWith(Path.DirectorySeparatorChar.ToString()))
                bundlePath.Append(Path.DirectorySeparatorChar);
            bundlePath.Append("L").Append(zoomStr).Append(Path.DirectorySeparatorChar).Append("R").Append(rowStr)
                .Append("C").Append(colStr).Append(BUNDLE_EXT);

            return bundlePath.ToString();
        }

        byte[] GetTileBytes(string bundleFile, int zoom, int row, int col)
        {
            int index = BUNDLX_MAXIDX * (row % BUNDLX_MAXIDX) + (col % BUNDLX_MAXIDX);

            using (FileStream source = File.OpenRead(bundleFile))
            {

                BinaryReader reader = new BinaryReader(source);

                source.Seek(0, SeekOrigin.Begin);
                byte[] ver = new byte[4];
                reader.Read(ver, 0, 4);
                int vernumber = BitConverter.ToInt32(ver, 0);

                source.Seek((index * 8) + COMPACT_CACHE_HEADER_LENGTH, SeekOrigin.Begin);

                byte[] offsetAndSize = new byte[8];
                reader.Read(offsetAndSize, 0, 8);

                byte[] offsetBytes = new byte[8];
                Buffer.BlockCopy(offsetAndSize, 0, offsetBytes, 0, 5);
                long offset = BitConverter.ToUInt32(offsetBytes, 0);

                byte[] sizeBytes = new byte[4];
                Buffer.BlockCopy(offsetAndSize, 5, sizeBytes, 0, 3);
                int size = BitConverter.ToInt32(sizeBytes, 0);

                if (size == 0)
                    return null;

                source.Seek(offset, SeekOrigin.Begin);
                byte[] tile = new byte[size];
                reader.Read(tile, 0, size);
                return tile;
            }

        }

        void SaveImage(byte[] data, string filepath)
        {
            using (MemoryStream stream = new MemoryStream(data))
            {
                using (FileStream outStream = new FileStream(filepath, FileMode.OpenOrCreate))
                {
                    using (ImageFactory imgFac = new ImageFactory(preserveExifData: true))
                    {
                        imgFac.Load(data)
                              //.Watermark(new ImageProcessor.Imaging.TextLayer { Text = "Hello", FontSize = 20, FontColor = Color.White, Opacity = 60 })
                              .Save(outStream);
                    }
                }
            }
            MessageBox.Show("完成");
        }

        void CreateCacheBundleFromTemplate(string dst, string src)
        {
            using (FileStream destination = File.Create(dst))
            {
                using (FileStream source = File.OpenRead(src))
                {
                    BinaryReader reader = new BinaryReader(source);
                    BinaryWriter writer = new BinaryWriter(destination);
                    source.Seek(0, SeekOrigin.Begin);
                    byte[] header = new byte[COMPACT_CACHE_HEADER_LENGTH];
                    reader.Read(header, 0, COMPACT_CACHE_HEADER_LENGTH);
                    writer.Write(header);

                }
            }
        }

        //To-do
        private void ReplaceBundleTiles(Object paras)
        {
            var p = paras as ReplaceBundleTilesParameters;
            using (FileStream dest = File.OpenWrite(p.destBundle))
            {
                using (FileStream src = File.OpenRead(p.scrBundle))
                {
                    BinaryReader reader = new BinaryReader(src, ASCIIEncoding.ASCII, true);
                    for (int r = 0; r < 128; r++)
                    {
                        for (int c = 0; c < 128; c++)
                        {
                            int index = r * 128 + c;
                            src.Seek((index * 8) + COMPACT_CACHE_HEADER_LENGTH, SeekOrigin.Begin);

                            byte[] offsetAndSize = new byte[8];
                            reader.Read(offsetAndSize, 0, 8);

                            byte[] offsetBytes = new byte[8];
                            Buffer.BlockCopy(offsetAndSize, 0, offsetBytes, 0, 5);
                            long offset = BitConverter.ToUInt32(offsetBytes, 0);

                            byte[] sizeBytes = new byte[4];
                            Buffer.BlockCopy(offsetAndSize, 5, sizeBytes, 0, 3);
                            int size = BitConverter.ToInt32(sizeBytes, 0);
                        }
                    }
                }
            }
        }
        private class ReplaceBundleTilesParameters { public string scrBundle; public string destBundle; public List<TileInfo> tiles; }



        private void button3_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                this.cacheRoot_tbx.Text = dlg.SelectedPath;
            }
        }

        private void GetLRCFromBundleFilePath(string bundlefile, out int level, out int row, out int column)
        {
            level = 0;
            row = 0;
            column = 0;
            int index = bundlefile.LastIndexOf(Path.DirectorySeparatorChar);
            level = int.Parse(bundlefile.Substring(index - 2, 2));
            row = int.Parse(bundlefile.Substring(index + 2, 4), System.Globalization.NumberStyles.AllowHexSpecifier);
            column = int.Parse(bundlefile.Substring(index + 7, 4), System.Globalization.NumberStyles.AllowHexSpecifier);

        }

        private class CheckTilesParameters { public string bundle_path; public TileCheckMode check_mode; }

        private string CheckTiles(string pathToCacheRoot, TileCheckMode mode)
        {
            var zooms = Directory.EnumerateDirectories(pathToCacheRoot + @"\_alllayers\");
            List<Task<string>> checkList = new List<Task<string>>();
            foreach (string z in zooms)
            {
                //if(this.level_tbx.Text!="")
                //{
                //    var zoom = int.Parse(z.Substring(z.Length-2,2));
                //    var l = int.Parse(this.level_tbx.Text);
                //    if(l!=zoom)
                //    {
                //        continue;
                //    }
                //}
                
                var files = Directory.EnumerateFiles(z);
                foreach (string f in files)
                {
                    var paras = new CheckTilesParameters { bundle_path = f, check_mode = mode };
                    Task<string> CheckTileTask = Task.Factory.StartNew<string>((Object p) => CheckBundle(p),paras);
                    checkList.Add(CheckTileTask);
                 }
            }
            Task.WaitAll(checkList.ToArray());
            StringBuilder result = new StringBuilder();
            foreach(var t in checkList)
            {
                if(t.Result!="")
                {
                    result.AppendLine(t.Result);
                }
            }
            return result.ToString();
        }


        private string CheckBundle(Object paras)
        {
            var p = paras as CheckTilesParameters;
            var f = p.bundle_path;
            var mode = p.check_mode;
            StringBuilder result = new StringBuilder();
            int level;
            int row;
            int column;
            GetLRCFromBundleFilePath(f, out level, out row, out column);
            using (FileStream source = File.OpenRead(f))
            {
                BinaryReader reader = new BinaryReader(source, ASCIIEncoding.ASCII, true);
                for (int r = 0; r < 128; r++)
                {
                    for (int c = 0; c < 128; c++)
                    {
                        int index = r * 128 + c;
                        source.Seek((index * 8) + COMPACT_CACHE_HEADER_LENGTH, SeekOrigin.Begin);

                        byte[] offsetAndSize = new byte[8];
                        reader.Read(offsetAndSize, 0, 8);

                        byte[] offsetBytes = new byte[8];
                        Buffer.BlockCopy(offsetAndSize, 0, offsetBytes, 0, 5);
                        long offset = BitConverter.ToUInt32(offsetBytes, 0);

                        byte[] sizeBytes = new byte[4];
                        Buffer.BlockCopy(offsetAndSize, 5, sizeBytes, 0, 3);
                        int size = BitConverter.ToInt32(sizeBytes, 0);

                        if (size == 0)
                            continue;

                        if (mode == TileCheckMode.Exist)
                        {
                            IDatabase redisClient = redisConnection.GetDatabase();
                            string tileID = String.Format("L{0}/R{1}/C{2}", level, row + r, column + c);
                            redisClient.SetAddAsync(this.cacheName_tbx.Text, tileID, CommandFlags.FireAndForget);
                            continue;
                        }
                        source.Seek(offset, SeekOrigin.Begin);
                        byte[] tile = new byte[size];
                        reader.Read(tile, 0, size);
                        if (mode == TileCheckMode.Whole)
                        {
                            if (CheckBlank(tile))
                            {
                                result.AppendLine(String.Format("L{0}/R{1}/C{2}", level, row + r, column + c));
                            }
                        }
                        else if (mode == TileCheckMode.Strip)
                        {
                            if (CheckStrip(tile))
                            {
                                result.AppendLine(String.Format("L{0}/R{1}/C{2}", level, row + r, column + c));
                            }
                        }
                    }
                }
            }
            return result.ToString();
        }


        private bool CheckBlank(byte[] data)
        {
            using (ImageFactory imgFac = new ImageFactory(preserveExifData: true))
            {
                imgFac.Load(data);
                using (Bitmap map = new Bitmap(imgFac.Image))
                {
                    int r = int.Parse(r_tbx.Text);
                    int g = int.Parse(g_tbx.Text);
                    int b = int.Parse(b_tbx.Text);

                    for (int x = 0; x < imgFac.Image.Width; x++)
                    {
                        for (int y = 0; y < imgFac.Image.Height; y++)
                        {
                            if (map.GetPixel(x, y) != Color.FromArgb(r, g, b))
                            {
                                return false;
                            }
                        }
                    }
                    return true;
                }

            }

        }

        private bool CheckStrip(byte[] data)
        {
            using (ImageFactory imgFac = new ImageFactory(preserveExifData: true))
            {
                imgFac.Load(data);
                using (Bitmap map = new Bitmap(imgFac.Image))
                {
                    int r = int.Parse(r_tbx.Text);
                    int g = int.Parse(g_tbx.Text);
                    int b = int.Parse(b_tbx.Text);

                    int h_max = int.Parse(strip_h_max_tbx.Text);
                    int w_max = int.Parse(strip_w_max_tbx.Text);
                    int h_min = int.Parse(strip_h_min_tbx.Text);
                    int w_min = int.Parse(strip_w_min_tbx.Text);


                    bool addband_flag = false;// is band begin
                    List<List<Tuple<int, int>>> bands = new List<List<Tuple<int, int>>>();
                    for (int x = 0; x < imgFac.Image.Width; x++)
                    {
                        int head = -1;
                        int tail = -1;
                        bool flag = false;//is strip begin
                        List<Tuple<int, int>> strips = new List<Tuple<int, int>>();
                        for (int y = 0; y < imgFac.Image.Height; y++)
                        {
                            if (map.GetPixel(x, y) != Color.FromArgb(r, g, b))
                            {
                                if (flag)
                                {
                                    int length = tail - head + 1;
                                    if (length >= h_min)
                                    {
                                        strips.Add(new Tuple<int, int>(head, tail));
                                        addband_flag = true;
                                    }
                                    flag = false;
                                }
                                continue;
                            }
                            else
                            {
                                if (!flag)
                                {
                                    flag = true;
                                    head = y;
                                    tail = y;
                                }
                                else
                                {
                                    tail = y;
                                }
                            }
                        }
                        if (flag)
                        {
                            int length = tail - head + 1;
                            if (length >= h_min)
                            {
                                strips.Add(new Tuple<int, int>(head, tail));
                                addband_flag = true;
                            }
                        }

                        if (addband_flag)
                        {
                            addband_flag = false;
                            bands.Add(strips);
                        }
                        else
                        {
                            //Assert
                            if (bands.Count >= w_min)
                            {
                                bool over_w_flag = false;
                                bool w_flag = false;// is found strip
                                bool w_once_flag = false;
                                int w_counter = w_min;
                                while (w_counter <= bands.Count)
                                {
                                    List<Tuple<int, int>> overlap_strips = BandOverlap(bands, w_counter);
                                    foreach (var s in overlap_strips)
                                    {
                                        int l = s.Item2 - s.Item1 + 1;
                                        if (l >= h_min && l <= h_max)
                                        {
                                            w_flag = true;
                                            w_once_flag = true;
                                            break;
                                        }
                                    }

                                    if (w_flag)
                                    {
                                        w_counter++;
                                        if (w_counter > w_max && w_counter <= bands.Count)
                                        {
                                            over_w_flag = true;
                                            bands.Clear();
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        if (w_once_flag && w_counter <= w_max)
                                        {
                                            w_flag = true;
                                            break;
                                        }
                                        else
                                        {
                                            bands.RemoveAt(0);
                                        }
                                    }

                                }
                                if (!over_w_flag && w_flag)
                                {
                                    return true;
                                }
                                bands.Clear();
                            }
                        }

                    }
                    if (addband_flag)
                    {
                        //Assert
                        if (bands.Count >= w_min)
                        {
                            bool over_w_flag = false;
                            bool w_flag = false;// is found strip
                            bool w_once_flag = false;
                            int w_counter = w_min;
                            while (w_counter <= bands.Count)
                            {
                                List<Tuple<int, int>> overlap_strips = BandOverlap(bands, w_counter);
                                foreach (var s in overlap_strips)
                                {
                                    int l = s.Item2 - s.Item1 + 1;
                                    if (l >= h_min && l <= h_max)
                                    {
                                        w_flag = true;
                                        w_once_flag = true;
                                        break;
                                    }
                                }

                                if (w_flag)
                                {
                                    w_counter++;
                                    if (w_counter > w_max && w_counter <= bands.Count)
                                    {
                                        over_w_flag = true;
                                        bands.Clear();
                                        break;
                                    }
                                }
                                else
                                {
                                    if (w_once_flag && w_counter <= w_max)
                                    {
                                        w_flag = true;
                                        break;
                                    }
                                    else
                                    {
                                        bands.RemoveAt(0);
                                    }
                                }

                            }
                            if (!over_w_flag && w_flag)
                            {
                                return true;
                            }
                            bands.Clear();
                        }

                    }

                    return false;
                }
            }
        }

        private List<Tuple<int,int>> BandOverlap(List<List<Tuple<int, int>>> bands, int n)
        {
            if (n == 1)
            {
                return bands[0];
            }
            else
            {
                List<Tuple<int, int>> strips_overlap = new List<Tuple<int, int>>();
                List<Tuple<int, int>> strips_n = bands[n - 1];
                List<Tuple<int, int>> strips_n_1 = BandOverlap(bands, n - 1);
                for(int i=0;i<strips_n_1.Count;i++)
                {
                    for(int j=0;j<strips_n.Count;j++)
                    {
                        if(strips_n_1[i].Item2<=strips_n[j].Item1 || strips_n_1[i].Item1>=strips_n[j].Item2)
                        {
                            continue;
                        }
                        int start = strips_n_1[i].Item1 >= strips_n[j].Item1?strips_n_1[i].Item1: strips_n[j].Item1;
                        int end = strips_n_1[i].Item2 <= strips_n[j].Item2 ? strips_n_1[i].Item2 : strips_n[j].Item2;
                        strips_overlap.Add(new Tuple<int, int>(start, end));
                    }
                }
                return strips_overlap;
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
                
                var result = CheckTiles(this.cacheRoot_tbx.Text, TileCheckMode.Whole);
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
                var result = CheckTiles(this.cacheRoot_tbx.Text, TileCheckMode.Strip);
                this.progressBar1.Style = ProgressBarStyle.Blocks;
                using (StreamWriter writer = new StreamWriter(dlg.FileName))
                {
                    writer.WriteLine(result);
                    writer.Flush();
                    MessageBox.Show("Completed.");
                }

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int level = 0, row = 0, col = 0;
            if (scale_tbx.Text != "" && x_tbx.Text != "" && y_tbx.Text != "")
            {
                CacheInfo cache = new CacheInfo();
                cache.LoadFromSchemaFile(this.cacheRoot_tbx.Text + @"\conf.xml");
                TileInfo tile = cache.GetTileInfoFromXY(double.Parse(scale_tbx.Text), double.Parse(x_tbx.Text), double.Parse(y_tbx.Text));
                level = tile.Level;
                row = tile.Row;
                col = tile.Column;
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
            string bundleFilePath = BuildBundleFilePath(cacheRoot_tbx.Text, level, row, col);
            byte[] data = GetTileBytes(bundleFilePath, level, row, col);
            if(data==null)
            {
                MessageBox.Show("Tile Not Exist!");
                return;
            }
            //Preview in picturebox
            this.tile_picbox.Image = Image.FromStream(new MemoryStream(data));
        }

        private void button6_Click(object sender, EventArgs e)
        {
            redisConnection = ConnectionMultiplexer.Connect(this.redisSvr_tbx.Text);
            CheckTiles(this.cacheRoot_tbx.Text, TileCheckMode.Exist);
            MessageBox.Show("Completed.");
        }
    }
}
