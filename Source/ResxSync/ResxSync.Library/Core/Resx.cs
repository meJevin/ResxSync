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
        List<Resx> _resxFiles;

        // Copy from above with extra KVPs that are missing
        List<Resx> _resxFilesSynced;

        public ResxSyncer()
        {
            _resxFiles = new List<Resx>();
            _resxFilesSynced = new List<Resx>();
        }

        public void Add(Resx rFile)
        {
            _resxFiles.Add(rFile);

            Resx synced = new Resx();
        }

        public void Remove(Resx rFile)
        {

        }
    }
}
