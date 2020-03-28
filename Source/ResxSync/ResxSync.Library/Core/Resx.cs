using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace ResxSync.Library.Core
{
    public class Resx
    {
        public Resx()
        {
            KVPs = new Dictionary<string, string>();
        }

        public Resx(string resxFilePath)
        {
            KVPs = new Dictionary<string, string>();

            using (ResXResourceReader resx = new ResXResourceReader(resxFilePath))
            {
                foreach (DictionaryEntry entry in resx)
                {
                    KVPs.Add(entry.Key.ToString(), entry.Value.ToString());
                }
            }
        }

        public Dictionary<string, string> KVPs;
    }
}
