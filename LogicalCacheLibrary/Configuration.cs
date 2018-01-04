using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalCacheLibrary
{
    public class Configuration
    {
        public IList<Cache_conf> caches;
        public IList<Picker_conf> pickers;
        public string registerdb;
    }

    public class Cache_conf
    {
        public string name;
        public string path;
    }

    public class Picker_conf
    {
        public string mask;
        public string type;
        public Dictionary<string, string> parameters;
        public IList<Processor_conf> processors;
    }

    public class Processor_conf
    {
        public string type;
        public Dictionary<string,string> parameters;
    }

}
