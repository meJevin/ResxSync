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

    public class SyncKey
    {
        public SyncKey()
        {
            Owners = new List<Resx>();
        }

        public string Key;

        // Where this key is present
        public List<Resx> Owners;
    }


    public class ResxSyncer
    {
        // Loaded from actual files
        public List<Resx> LoadedResx;

        public Dictionary<string, SyncKey> SyncKeys;

        public ResxSyncer()
        {
            LoadedResx = new List<Resx>();
            SyncKeys = new Dictionary<string, SyncKey>();
        }

        public void Add(Resx rFile)
        {
            LoadedResx.Add(rFile);

            foreach (var key in rFile.KVPs.Keys)
            {
                if (!SyncKeys.ContainsKey(key))
                {
                    // New key!

                    SyncKey sKey = new SyncKey()
                    {
                        Key = key,
                    };

                    sKey.Owners.Add(rFile);

                    SyncKeys.Add(key, sKey);
                }
                else
                {
                    // Old key!

                    SyncKeys[key].Owners.Add(rFile);
                }
            }
        }

        public void Remove(Resx rFile)
        {
            foreach (var key in rFile.KVPs.Keys)
            {
                SyncKeys[key].Owners.Remove(rFile);

                if (SyncKeys[key].Owners.Count == 0)
                {
                    SyncKeys.Remove(key);
                }
            }

            LoadedResx.Remove(rFile);
        }

        public List<string> CommonKeys()
        {
            return
                SyncKeys.Where(kvp => kvp.Value.Owners.Count == LoadedResx.Count)
                .Select(kvp => kvp.Key)
                .ToList();
        }

        public List<string> UniqueTo(Resx rFile)
        {
            return
                SyncKeys.Where((kvp) =>
                {
                    var owners = kvp.Value.Owners;

                    return (owners.Count == 1) && (owners[0] == rFile);
                })
                .Select(kvp => kvp.Key)
                .ToList();
        }
    }
}
