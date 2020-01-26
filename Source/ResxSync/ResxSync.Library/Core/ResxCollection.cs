using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Resources;
using System.Collections;
using System.IO;

namespace ResxSync.Library.Core
{
    public class ResxCollection
    {
        // Loaded .resx files and their KVPs
        public Dictionary<string, Dictionary<string, string>> _resxFiles;

        // All encountered keys and their values according to file
        public Dictionary<string, Dictionary<string, string>> _keysAndTheirValues;

        public ResxCollection()
        {
            _resxFiles = new Dictionary<string, Dictionary<string, string>>();

            _keysAndTheirValues = new Dictionary<string, Dictionary<string, string>>();
        }

        public void Add(string filePath)
        {
            string resxName = Path.GetFileNameWithoutExtension(filePath);

            if (_resxFiles.ContainsKey(resxName))
            {
                return;
            }

            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            using (ResXResourceReader resx = new ResXResourceReader(filePath))
            {
                foreach (DictionaryEntry entry in resx)
                {
                    var key = entry.Key.ToString();
                    var value = entry.Value.ToString();

                    keyValuePairs.Add(key, value);

                    if (_keysAndTheirValues.ContainsKey(key))
                    {
                        // Already encountered, add file, values KVP
                        _keysAndTheirValues[key].Add(resxName, value);
                    }
                    else
                    {
                        // New key. Add and populate
                        Dictionary<string, string> values = new Dictionary<string, string>();

                        foreach (var resxFile in _resxFiles.Keys)
                        {
                            values.Add(resxFile, null);
                        }

                        values.Add(resxName, value);

                        _keysAndTheirValues.Add(key, values);
                    }
                }

                // Add nulls to keys that weren't in this file
                foreach (var key in _keysAndTheirValues.Keys.Except(keyValuePairs.Keys))
                {
                    _keysAndTheirValues[key].Add(resxName, null);
                }
            }

            _resxFiles.Add(resxName, keyValuePairs);
        }

        public void Remove(string resxName)
        {
            var resxToRemove = _resxFiles[resxName];

            var allKeys = _keysAndTheirValues.Keys.ToList();
            foreach (var key in allKeys)
            {
                _keysAndTheirValues[key].Remove(resxName);

                if (_keysAndTheirValues[key].Count == 0 || _keysAndTheirValues[key].Values.All(val => val == null))
                {
                    _keysAndTheirValues.Remove(key);
                }
            }

            _resxFiles.Remove(resxName);
        }

        public Dictionary<string, Dictionary<string, string>> GetUnsynced()
        {
            Dictionary<string, Dictionary<string, string>> result = new Dictionary<string, Dictionary<string, string>>();

            foreach (var keyAndItsValues in _keysAndTheirValues)
            {
                if (keyAndItsValues.Value.Values.Contains(null))
                {
                    result.Add(keyAndItsValues.Key, keyAndItsValues.Value);
                }
            }

            return result;
        }
    }
}
