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

//https://github.com/Esri/raster-tiles-compactcache/blob/master/CompactCacheV2.md

namespace CacheFilter
{
    public partial class Form1 : Form
    {
        const int BUNDLX_MAXIDX = 128;
        const int COMPACT_CACHE_HEADER_LENGTH = 64;
        const String BUNDLE_EXT = ".bundle";

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Match match = Regex.Match(tileurl_tbx.Text, @"\d+\/\d+\/\d+$");
            string[] components = match.Value.Split('/');
            int[] components_int = Array.ConvertAll<string, int>(components, new Converter<string, int>(Str2int));
            int level = components_int[0];
            int row = components_int[1];
            int col = components_int[2];
            string bundleFilePath = BuildBundleFilePath(cacheRoot_tbx.Text, level,row,col);
            byte[] data = GetTileBytes(bundleFilePath,level,row,col);
            SaveImage(data, 256, 256);
        }

        private int Str2int(string s)
        {
            return int.Parse(s);
        }

        protected String BuildBundleFilePath(string pathToCacheRoot, int zoom, int row, int col)
        {
            StringBuilder bundlePath = new StringBuilder(pathToCacheRoot);

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
            // col and row are inverted for 10.3 caches
            int index = BUNDLX_MAXIDX * (row % BUNDLX_MAXIDX) + (col % BUNDLX_MAXIDX);

            using (FileStream source = File.OpenRead(bundleFile))
            {
                
                BinaryReader reader = new BinaryReader(source);

                source.Seek(0, SeekOrigin.Begin);
                byte[] ver = new byte[4];
                reader.Read(ver, 0, 4);
                int vernumber = BitConverter.ToInt32(ver,0);

                source.Seek((index * 8) + COMPACT_CACHE_HEADER_LENGTH, SeekOrigin.Begin);

                byte[] offsetAndSize = new byte[8];
                reader.Read(offsetAndSize ,0, 8);

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

        void SaveImage(byte[] data,int rows, int cols)
        {
            MemoryStream stream = new MemoryStream(data);
            File.WriteAllBytes("D:/test.jpeg", data);
            MessageBox.Show("OK");
        }
    }
}
