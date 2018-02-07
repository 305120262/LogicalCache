using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;

namespace LogicalCacheLibrary
{
    public class CacheInfo
    {
        public CacheSchema Schema { get; set; }
        public string Root { get; }

        public CacheInfo(string root)
        {
            Schema = new CacheSchema(root + "/Conf.xml");
            Root = root;
            
        }


    }
}
