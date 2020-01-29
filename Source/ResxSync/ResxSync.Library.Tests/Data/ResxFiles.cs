using ResxSync.Library.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResxSync.Library.Tests.Data
{
    public static class ResxFiles
    {
        public static List<Resx> Data;

        static ResxFiles()
        {
            Data = new List<Resx>();
        }
    }
}
