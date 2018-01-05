using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageProcessor;
using StackExchange.Redis;
using System.Drawing;
using Newtonsoft.Json;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using NetTopologySuite.IO;
using GeoAPI.Geometries;
using GeoAPI;

namespace LogicalCacheLibrary
{
    public enum TileCheckMode
    {
        Whole,
        Strip,
        Exist
    }
    public class CheckTilesParameters { public string bundle_path; }
    public class ProcessTilesParameters { public string scrBundle; public string destBundle; public List<TileInfo> tiles; public Processor_conf config; }

    public class LogicalCache
    {
        const int BUNDLX_MAXIDX = 128;
        const int COMPACT_CACHE_HEADER_LENGTH = 64;
        const String BUNDLE_EXT = ".bundle";
        public int r, g, b = 0;
        public int h_max, w_max, h_min, w_min = 0;
        public string mask_name = "";
        public string cache_root_folder = "";
        public TileCheckMode check_mode = TileCheckMode.Whole;
        public Configuration config;
        public ConnectionMultiplexer redisConnection;
        private string redisConnectionString;



        public String BuildBundleFilePath(string pathToCacheRoot, int zoom, int row, int col)
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
            bundlePath.Append("L").Append(zoomStr).Append(Path.DirectorySeparatorChar).Append("R").Append(rowStr.ToString().ToLower())
                .Append("C").Append(colStr.ToString().ToLower()).Append(BUNDLE_EXT);

            return bundlePath.ToString();
        }

        public byte[] GetTileBytes(string bundleFile, int zoom, int row, int col)
        {
            int index = BUNDLX_MAXIDX * (row % BUNDLX_MAXIDX) + (col % BUNDLX_MAXIDX);
            using (FileStream source = File.OpenRead(bundleFile))
            {

                BinaryReader reader = new BinaryReader(source);

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

        //To-do
        public async void ProcessTiles(string SrcCacheRoot,string DestCacheRoot, string mask,string config)
        {
            Dictionary<string, List<TileInfo>> bundles = new Dictionary<string, List<TileInfo>>();
            if(mask!="")
            {
                IDatabase client = redisConnection.GetDatabase();
                if(client.KeyExists(mask))
                {
                    var masks = await client.SetMembersAsync(mask);
                    foreach ( var m in masks)
                    {
                        string[] componets = m.ToString().Split('/');
                        int level = int.Parse(componets[0].Substring(1));
                        int row = int.Parse(componets[1].Substring(1));
                        int column = int.Parse(componets[2].Substring(1));
                        string bundlepath =BuildBundleFilePath(SrcCacheRoot, level, row, column);
                        if(bundles.ContainsKey(bundlepath))
                        {
                            List<TileInfo> tiles_lst = bundles[bundlepath];
                            tiles_lst.Add(new TileInfo() { Level = level, Row = row, Column = column });
                        }
                        else
                        {
                            List<TileInfo> tiles_lst = new List<TileInfo>();
                            tiles_lst.Add(new TileInfo() { Level = level, Row = row, Column = column });
                            bundles.Add(bundlepath, tiles_lst);
                        }
                    }
                }
            }
            Processor_conf processor = JsonConvert.DeserializeObject<Processor_conf>(config);
            List<Task<string>> processList = new List<Task<string>>();
            foreach (string f in bundles.Keys)
            {
                string desBundle = DestCacheRoot + f.Substring(f.IndexOf(SrcCacheRoot)+SrcCacheRoot.Length);
                var paras = new ProcessTilesParameters { scrBundle = f , destBundle =desBundle, tiles = bundles[f]  ,config=processor};
                Task<string> processTileTask = Task.Factory.StartNew<string>((Object p) => ProcessBundle(p), paras);
                processList.Add(processTileTask);
            }
            Task.WaitAll(processList.ToArray());
            return;

        }

        private void InitCacheBundle(string path)
        {
            FileInfo fileInfo = new FileInfo(path);
            if (!fileInfo.Directory.Exists) fileInfo.Directory.Create();
            using (FileStream destination = File.Create(path))
            {
                BinaryWriter writer = new BinaryWriter(destination);
                byte[] header = new byte[COMPACT_CACHE_HEADER_LENGTH];
                int offset = 0;

                byte[] p1 = BitConverter.GetBytes(3);
                Buffer.BlockCopy(p1, 0, header, offset, 4);
                offset += 4;

                byte[] p2 = BitConverter.GetBytes(16384);
                Buffer.BlockCopy(p2, 0, header, offset, 4);
                offset += 4;

                byte[] p3 = BitConverter.GetBytes(0);
                Buffer.BlockCopy(p3, 0, header, offset, 4);
                offset += 4;

                byte[] p4 = BitConverter.GetBytes(5);
                Buffer.BlockCopy(p4, 0, header, offset, 4);
                offset += 4;

                byte[] p5 = BitConverter.GetBytes((long)0);
                Buffer.BlockCopy(p5, 0, header, offset, 8);
                offset += 8;

                byte[] p6 = BitConverter.GetBytes((long)0);
                Buffer.BlockCopy(p6, 0, header, offset, 8);
                offset += 8;

                byte[] p7 = BitConverter.GetBytes((long)40);
                Buffer.BlockCopy(p7, 0, header, offset, 8);
                offset += 8;

                byte[] p8 = BitConverter.GetBytes(20 + 131072);
                Buffer.BlockCopy(p8, 0, header, offset, 4);
                offset += 4;

                byte[] p9 = BitConverter.GetBytes(3);
                Buffer.BlockCopy(p9, 0, header, offset, 4);
                offset += 4;

                byte[] p10 = BitConverter.GetBytes(16);
                Buffer.BlockCopy(p10, 0, header, offset, 4);
                offset += 4;

                byte[] p11 = BitConverter.GetBytes(16384);
                Buffer.BlockCopy(p11, 0, header, offset, 4);
                offset += 4;

                byte[] p12 = BitConverter.GetBytes(5);
                Buffer.BlockCopy(p12, 0, header, offset, 4);
                offset += 4;

                byte[] p13 = BitConverter.GetBytes(131072);
                Buffer.BlockCopy(p13, 0, header, offset, 4);
                offset += 4;

                writer.Write(header);

                byte[] index = new byte[131072];
                writer.Write(index);

                writer.Close();
            }
        }

        public string ProcessBundle(Object paras)
        {
            var p = paras as ProcessTilesParameters;
            if(!File.Exists(p.destBundle))
            {
                InitCacheBundle(p.destBundle);
            }
            using (FileStream src = File.OpenRead(p.scrBundle))
            {
                BinaryReader reader = new BinaryReader(src, ASCIIEncoding.ASCII,true);
                using (FileStream dest = File.Open(p.destBundle,FileMode.Open,FileAccess.ReadWrite))
                {
                    BinaryWriter writer = new BinaryWriter(dest, ASCIIEncoding.ASCII, true);
                    foreach(var tile in p.tiles)
                    {
                        int index = BUNDLX_MAXIDX * (tile.Row % BUNDLX_MAXIDX) + (tile.Column % BUNDLX_MAXIDX);
                        src.Seek((index * 8) + COMPACT_CACHE_HEADER_LENGTH, SeekOrigin.Begin);

                        byte[] offsetAndSize = new byte[8];
                        reader.Read(offsetAndSize, 0, 8);

                        byte[] offsetBytes = new byte[8];
                        Buffer.BlockCopy(offsetAndSize, 0, offsetBytes, 0, 5);
                        long offset = BitConverter.ToUInt32(offsetBytes, 0);

                        byte[] sizeBytes = new byte[4];
                        Buffer.BlockCopy(offsetAndSize, 5, sizeBytes, 0, 3);
                        int size = BitConverter.ToInt32(sizeBytes, 0);

                        if (size != 0)
                        {
                            src.Seek(offset, SeekOrigin.Begin);
                            byte[] tile_src = new byte[size];
                            reader.Read(tile_src, 0, size);

                            ImageFactory imgfac = new ImageFactory(true);
                            imgfac.Load(tile_src);
                            using(MemoryStream stream  = new MemoryStream())
                            { 
                                imgfac.Save(stream);
                                byte[] tile_desc = stream.ToArray();
                                int size_dest = tile_desc.Length;
                                dest.Seek(8, SeekOrigin.Begin);
                                byte[] maxsizeBytes = new byte[4];
                                dest.Read(maxsizeBytes, 0, 4);
                                int maxsize = BitConverter.ToInt32(maxsizeBytes, 0);
                                maxsize = (maxsize < size_dest) ? size_dest : maxsize;
                                dest.Seek(8, SeekOrigin.Begin);
                                writer.Write(BitConverter.GetBytes(maxsize));

                                dest.Seek(24, SeekOrigin.Begin);
                                writer.Write(BitConverter.GetBytes(size_dest + dest.Length));

                                
                                byte[] offsetAndSize_desc = new byte[8];
                                Buffer.BlockCopy(BitConverter.GetBytes(size_dest), 0, offsetAndSize_desc, 5, 3);
                                long index_desc = dest.Length + 4;
                                byte[] indexByte_desc = BitConverter.GetBytes(index_desc);
                                Array.Resize<byte>(ref indexByte_desc, 5);
                                Buffer.BlockCopy(indexByte_desc, 0, offsetAndSize_desc, 0, 5);
                                dest.Seek((index * 8) + COMPACT_CACHE_HEADER_LENGTH, SeekOrigin.Begin);
                                writer.Write(offsetAndSize_desc);

                                dest.Seek(index_desc, SeekOrigin.Begin);
                                writer.Write(BitConverter.GetBytes(size_dest));
                                writer.Write(stream.ToArray());
                                Image img = Image.FromStream(stream);
                                string output = string.Format("L{0}R{1}C{2}", tile.Level, tile.Row, tile.Column);
                                img.Save(@"D:\Research\瓦片读取研究\test\" + output + ".png");
                            }
                        }
                       
                    }
                }
                reader.Close();
            }
            return "";
        }


        public void GetLRCFromBundleFilePath(string bundlefile, out int level, out int row, out int column)
        {
            level = 0;
            row = 0;
            column = 0;
            int index = bundlefile.LastIndexOf(Path.DirectorySeparatorChar);
            level = int.Parse(bundlefile.Substring(index - 2, 2));
            row = int.Parse(bundlefile.Substring(index + 2, 4), System.Globalization.NumberStyles.AllowHexSpecifier);
            column = int.Parse(bundlefile.Substring(index + 7, 4), System.Globalization.NumberStyles.AllowHexSpecifier);

        }

        public string CheckTiles(string pathToCacheRoot, TileCheckMode mode)
        {
            var zooms = Directory.EnumerateDirectories(pathToCacheRoot + @"\_alllayers\");
            List<Task<string>> checkList = new List<Task<string>>();
            this.check_mode = mode;
            DropMask(this.mask_name);
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
                    var paras = new CheckTilesParameters { bundle_path = f };
                    Task<string> checkTileTask = Task.Factory.StartNew<string>((Object p) => CheckBundle(p), paras);
                    checkList.Add(checkTileTask);
                }
            }
            Task.WaitAll(checkList.ToArray());
            StringBuilder result = new StringBuilder();
            foreach (var t in checkList)
            {
                if (t.Result != "")
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

                        if (check_mode == TileCheckMode.Exist)
                        {
                            IDatabase redisClient = redisConnection.GetDatabase();
                            string tileID = String.Format("L{0}/R{1}/C{2}", level, row + r, column + c);
                            redisClient.SetAddAsync(mask_name, tileID, CommandFlags.FireAndForget);
                            continue;
                        }
                        source.Seek(offset, SeekOrigin.Begin);
                        byte[] tile = new byte[size];
                        reader.Read(tile, 0, size);
                        if (check_mode == TileCheckMode.Whole)
                        {
                            if (CheckBlank(tile))
                            {
                                result.AppendLine(String.Format("L{0}/R{1}/C{2}", level, row + r, column + c));
                            }
                        }
                        else if (check_mode == TileCheckMode.Strip)
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

        private List<Tuple<int, int>> BandOverlap(List<List<Tuple<int, int>>> bands, int n)
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
                for (int i = 0; i < strips_n_1.Count; i++)
                {
                    for (int j = 0; j < strips_n.Count; j++)
                    {
                        if (strips_n_1[i].Item2 <= strips_n[j].Item1 || strips_n_1[i].Item1 >= strips_n[j].Item2)
                        {
                            continue;
                        }
                        int start = strips_n_1[i].Item1 >= strips_n[j].Item1 ? strips_n_1[i].Item1 : strips_n[j].Item1;
                        int end = strips_n_1[i].Item2 <= strips_n[j].Item2 ? strips_n_1[i].Item2 : strips_n[j].Item2;
                        strips_overlap.Add(new Tuple<int, int>(start, end));
                    }
                }
                return strips_overlap;
            }
        }


        public bool SaveImage(byte[] data, string filepath)
        {
            using (MemoryStream stream = new MemoryStream(data))
            {
                using (FileStream outStream = new FileStream(filepath, FileMode.OpenOrCreate))
                {
                    using (ImageFactory imgFac = new ImageFactory(preserveExifData: true))
                    {
                        imgFac.Load(data)
                              .Save(outStream);
                    }
                }
            }
            return true;
        }

        public void ConnectRegisterDB(string path)
        {
            redisConnectionString = path;
            redisConnection = ConnectionMultiplexer.Connect(path);
        }

        public void LoadConfig(string configuration)
        {
            using (StreamReader reader = new StreamReader(configuration))
            {
                config = JsonConvert.DeserializeObject<Configuration>(reader.ReadToEnd());
                ConnectRegisterDB(config.registerdb);
            }
        }

        public void GetTile(int level, int row, int column, Stream output)
        {
            //Check is in a mask
            //if in , fetech tiles by mask algorithem, then return image, then do processor
            foreach (var picker in config.pickers)
            {
                IDatabase redisClient = redisConnection.GetDatabase();
                string tileID = String.Format("L{0}/R{1}/C{2}", level, row, column);
                if (redisClient.SetContains(picker.mask, tileID))
                {
                    if (picker.type == "First")
                    {
                        foreach (var cache in config.caches)
                        {
                            if (redisClient.SetContains(cache.name, tileID))
                            {
                                string bundle = BuildBundleFilePath(cache.path, level, row, column);
                                byte[] tile = GetTileBytes(bundle, level, row, column);
                                ImageFactory imgfac = new ImageFactory();
                                imgfac.Load(tile);
                                ProcessTile(imgfac, picker.processors).Save(output);
                                return;
                            }
                        }
                    }
                    else if (picker.type == "Blend")
                    {
                        bool blend_flag = false;
                        ImageFactory imgfac = new ImageFactory();
                        for (int i = config.caches.Count - 1; i >= 0; i--)
                        {
                            var cache = config.caches[i];
                            if (redisClient.SetContains(cache.name, tileID))
                            {
                                string bundle = BuildBundleFilePath(cache.path, level, row, column);
                                byte[] tile = GetTileBytes(bundle, level, row, column);
                                Bitmap tile_image = new Bitmap(new MemoryStream(tile));
                                if (!blend_flag)
                                {
                                    imgfac.Load(tile);
                                    blend_flag = true;
                                }
                                else
                                {
                                    imgfac.Overlay(new ImageProcessor.Imaging.ImageLayer() { Image = tile_image });
                                }

                            }
                        }
                        ProcessTile(imgfac, picker.processors).Save(output);
                        return;
                    }
                }
                continue;
            }

            //if not in, return blank
            Bitmap bitmap = new Bitmap(256, 256);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.FillRectangle(Brushes.White, 0, 0, 256, 256);
            bitmap.Save(output, ImageFormat.Png);
        }

        private ImageFactory ProcessTile(ImageFactory tile, IList<Processor_conf> processors)
        {
            foreach (var processor in processors)
            {
                if (processor.type == "Watermark")
                {
                    tile = tile.Watermark(new ImageProcessor.Imaging.TextLayer() { Text = processor.parameters["text"] });
                }
            }
            return tile;
        }


        private static void MatrixBlend(string image1path, string image2path, byte alpha)
        {
            // for the matrix the range is 0.0 - 1.0
            float alphaNorm = (float)alpha / 255.0F;
            using (Bitmap image1 = (Bitmap)Bitmap.FromFile(image1path))
            {
                using (Bitmap image2 = (Bitmap)Bitmap.FromFile(image2path))
                {
                    // just change the alpha
                    ColorMatrix matrix = new ColorMatrix(new float[][]{
                new float[] {1F, 0, 0, 0, 0},
                new float[] {0, 1F, 0, 0, 0},
                new float[] {0, 0, 1F, 0, 0},
                new float[] {0, 0, 0, alphaNorm, 0},
                new float[] {0, 0, 0, 0, 1F}});

                    ImageAttributes imageAttributes = new ImageAttributes();
                    imageAttributes.SetColorMatrix(matrix);

                    using (Graphics g = Graphics.FromImage(image1))
                    {
                        g.CompositingMode = CompositingMode.SourceOver;
                        g.CompositingQuality = CompositingQuality.HighQuality;

                        g.DrawImage(image2,
                            new Rectangle(0, 0, image1.Width, image1.Height),
                            0,
                            0,
                            image2.Width,
                            image2.Height,
                            GraphicsUnit.Pixel,
                            imageAttributes);
                    }
                }
            }
        }

        public List<string> GetRegisterCaches()
        {
            List<string> keys = new List<string>();
            foreach (var key in redisConnection.GetServer(redisConnectionString).Keys())
            {
                keys.Add(key.ToString());
            }
            return keys;
        }

        public void BuildMask(string pathToCacheRoot, string shapefile, string maskName, string position,List<int> levels)
        {
            DropMask(maskName);
            CacheInfo cache = new CacheInfo();
            cache.LoadFromSchemaFile(pathToCacheRoot + @"\conf.xml");
            ShapefileReader reader = new ShapefileReader(shapefile);
            IGeometryCollection shps = reader.ReadAll();
            foreach (IGeometry shp in shps.Geometries)
            {
                foreach (var lod in cache.LODs.Reverse())
                {
                    if( !levels.Contains(lod.Value.Item1))
                    {
                        continue;
                    }
                    TileInfo tile_start = cache.GetTileInfoFromXY(lod.Key, shp.Envelope.Coordinates[0].X, shp.Envelope.Coordinates[0].Y);
                    TileInfo tile_end = cache.GetTileInfoFromXY(lod.Key, shp.Envelope.Coordinates[2].X, shp.Envelope.Coordinates[2].Y);
                    int level = tile_start.Level;
                    for (int x = tile_start.Column; x <= tile_end.Column; x++)
                    {
                        for (int y = tile_end.Row; y <= tile_start.Row; y++)
                        {
                            var loc = cache.GetTileEnvelop(level, y, x);
                            Envelope env = new Envelope(loc.Item1, loc.Item2, loc.Item3, loc.Item4);
                            IGeometryFactory geofac = GeometryServiceProvider.Instance.CreateGeometryFactory();
                            if (position == "Border")
                            {
                                if (shp.Boundary.Intersects(geofac.ToGeometry(env)))
                                {
                                    IDatabase redisClient = redisConnection.GetDatabase();
                                    string tileID = String.Format("L{0}/R{1}/C{2}", level, y, x);
                                    redisClient.SetAddAsync(maskName, tileID, CommandFlags.FireAndForget);
                                }
                            }
                            else if (position == "Interior")
                            {
                                if (shp.Contains(geofac.ToGeometry(env)))
                                {
                                    IDatabase redisClient = redisConnection.GetDatabase();
                                    string tileID = String.Format("L{0}/R{1}/C{2}", level, y, x);
                                    redisClient.SetAddAsync(maskName, tileID, CommandFlags.FireAndForget);
                                }
                            }

                        }
                    }

                }


            }

        }

        public void DropMask(string name)
        {
            foreach (var key in redisConnection.GetServer(this.redisConnectionString).Keys())
            {
                if (key.ToString() == name)
                {
                    IDatabase redisClient = redisConnection.GetDatabase();
                    redisClient.KeyDelete(key);
                }
            }
        }

    }
}
