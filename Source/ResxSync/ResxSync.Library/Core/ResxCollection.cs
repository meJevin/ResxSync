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
        public Dictionary<string, Dictionary<string, string>> ResxFiles;

        // All encountered keys and their values according to file
        public Dictionary<string, Dictionary<string, string>> KeyValues;

        public ResxCollection()
        {
            ResxFiles = new Dictionary<string, Dictionary<string, string>>();

            KeyValues = new Dictionary<string, Dictionary<string, string>>();
        }

        public void Add(string filePath)
        {
            string resxName = Path.GetFileNameWithoutExtension(filePath);

            if (ResxFiles.ContainsKey(resxName))
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

                    if (KeyValues.ContainsKey(key))
                    {
                        // Already encountered, add file, values KVP
                        KeyValues[key].Add(resxName, value);
                    }
                    else
                    {
                        // New key. Add and populate
                        Dictionary<string, string> values = new Dictionary<string, string>();

                        foreach (var resxFile in ResxFiles.Keys)
                        {
                            values.Add(resxFile, null);
                        }

                        values.Add(resxName, value);

                        KeyValues.Add(key, values);
                    }
                }

                // Add nulls to keys that weren't in this file
                foreach (var key in KeyValues.Keys.Except(keyValuePairs.Keys))
                {
                    KeyValues[key].Add(resxName, null);
                }
            }

            ResxFiles.Add(resxName, keyValuePairs);
        }

        public void Remove(string resxName)
        {
            var resxToRemove = ResxFiles[resxName];

            var allKeys = KeyValues.Keys.ToList();
            foreach (var key in allKeys)
            {
                KeyValues[key].Remove(resxName);

                if (KeyValues[key].Count == 0 || KeyValues[key].Values.All(val => val == null))
                {
                    KeyValues.Remove(key);
                }
            }

            ResxFiles.Remove(resxName);
        }

        public Dictionary<string, Dictionary<string, string>> GetUnsynced()
        {
            Dictionary<string, Dictionary<string, string>> result = new Dictionary<string, Dictionary<string, string>>();

            foreach (var keyAndItsValues in KeyValues)
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
