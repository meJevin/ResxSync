using System;
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
                foreach (KeyValuePair<string, string> entry in resx)
                {
                    KVPs.Add(entry.Key, entry.Value);
                }
            }
        }

        public Dictionary<string, string> KVPs;
    }

    public class ResxSyncer
    {
        // Loaded from actual files
        public List<Resx> LoadedResx;

        public ResxSyncer()
        {
            LoadedResx = new List<Resx>();
        }

        public void Add(Resx rFile)
        {
            LoadedResx.Add(rFile);
        }

        public void Remove(Resx rFile)
        {
            LoadedResx.Remove(rFile);
        }

        public void AllKeys()
        {
            LoadedResx.Select(resx => resx.KVPs.Keys).Distinct();
        }

        public void MissingValues(Resx file)
        {

        }
    }
}
