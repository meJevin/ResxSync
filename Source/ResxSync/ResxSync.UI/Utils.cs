using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResxSync.UI
{
    public static class Utils
    {
        public static string DummyResxDirectory = @"..\..\..\ResxSync.Library.Tests\Dummy\";

        public static string SelectFile(List<string> extensions = null)
        {
            #region Get file
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Resx|*.resx";
            ofd.RestoreDirectory = true;

            var res = ofd.ShowDialog();
            if (!res.HasValue || res.Value != true) return null;
            #endregion

            return ofd.FileName;
        }
    }
}
