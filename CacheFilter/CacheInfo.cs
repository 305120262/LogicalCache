using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;

namespace CacheFilter
{
    class CacheInfo
    {
        public int OriginX { get; set; }
        public int OriginY { get; set; }
        public int TileRows { get; set; }
        public int TileColumns { get; set; }
        public SortedList<int,Tuple<int,int,double>> LODs { get; set; }

        public CacheInfo()
        {
            LODs = new SortedList<int, Tuple<int, int, double>>();
        }

        public TileInfo GetTileInfoFromXY(double scale, double x,double y)
        {
            TileInfo tile = new TileInfo();
            int i = 0;
            for (;i<LODs.Count;i++)
            {
                if (scale > LODs.Keys[i])
                    continue;
                else
                {
                    break;
                }
            }
            i = i == 0 ? 0 : i - 1;
            Tuple<int, int, double> lod = LODs[LODs.Keys[i]];
            tile.Column = (int) Math.Floor( (x-OriginX)/(TileColumns * lod.Item3));
            tile.Row = (int) Math.Floor ( (OriginY -y)/(TileRows * lod.Item3));
            tile.Level = lod.Item1;
            return tile;
            
        }

        public void LoadFromSchemaFile(string path)
        {
            XElement root = XElement.Load(path);
            XElement origin = root.Element("TileCacheInfo").Element("TileOrigin");
            OriginX = int.Parse(origin.Element("X").Value);
            OriginY = int.Parse(origin.Element("Y").Value);
            
            TileRows = int.Parse(root.Element("TileCacheInfo").Element("TileRows").Value);
            TileColumns = int.Parse(root.Element("TileCacheInfo").Element("TileCols").Value);

            IEnumerable<XElement> lods = root.Element("TileCacheInfo").Element("LODInfos").Elements("LODInfo");
            foreach(XElement lod in lods)
            {
                LODs.Add(int.Parse(lod.Element("Scale").Value), Tuple.Create(int.Parse(lod.Element("LevelID").Value), int.Parse(lod.Element("Scale").Value), double.Parse(lod.Element("Resolution").Value)));

            }

        }
    }
}
