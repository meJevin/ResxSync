using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResxSync.Library.Core
{
    public class ResxSyncer
    {
        // Loaded from actual files
        public List<Resx> LoadedResx;

        private Dictionary<string, SyncKey> _syncKeys;

        public List<SyncKey> SyncKeys
        {
            get
            {
                return _syncKeys.Values.ToList();
            }
        }

        public ResxSyncer()
        {
            LoadedResx = new List<Resx>();
            _syncKeys = new Dictionary<string, SyncKey>();
        }

        public void Add(Resx rFile)
        {
            LoadedResx.Add(rFile);

            foreach (var key in rFile.KVPs.Keys)
            {
                if (!_syncKeys.ContainsKey(key))
                {
                    // New key!

                    SyncKey sKey = new SyncKey()
                    {
                        Key = key,
                    };

                    sKey.Owners.Add(rFile);

                    _syncKeys.Add(key, sKey);
                }
                else
                {
                    // Old key!

                    _syncKeys[key].Owners.Add(rFile);
                }
            }
        }

        public void Remove(Resx rFile)
        {
            foreach (var key in rFile.KVPs.Keys)
            {
                _syncKeys[key].Owners.Remove(rFile);

                if (_syncKeys[key].Owners.Count == 0)
                {
                    _syncKeys.Remove(key);
                }
            }

            LoadedResx.Remove(rFile);
        }

        public bool ContainsKey(string key)
        {
            return _syncKeys.ContainsKey(key);
        }

        public List<string> CommonKeys()
        {
            return
                _syncKeys.Where(kvp => kvp.Value.Owners.Count == LoadedResx.Count)
                .Select(kvp => kvp.Key)
                .ToList();
        }

        public List<string> UniqueTo(Resx rFile)
        {
            return
                _syncKeys.Where((kvp) =>
                {
                    var owners = kvp.Value.Owners;

                    return (owners.Count == 1) && (owners[0] == rFile);
                })
                .Select(kvp => kvp.Key)
                .ToList();
        }
    }
}
