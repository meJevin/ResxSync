using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResxSync.Library.Core
{

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
}
