using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ResxSync.Library.Core;

namespace ResxSync.Library.Tests.Basic
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void AllKeys()
        {
            Resx file1 = new Resx(@"C:\Users\Michael\Desktop\Dev\Personal\ResxSync\Source\ResxSync\ResxSync.Library.Tests\Dummy\1.resx");
            Resx file2 = new Resx(@"C:\Users\Michael\Desktop\Dev\Personal\ResxSync\Source\ResxSync\ResxSync.Library.Tests\Dummy\2.resx");
            Resx file3 = new Resx(@"C:\Users\Michael\Desktop\Dev\Personal\ResxSync\Source\ResxSync\ResxSync.Library.Tests\Dummy\3.resx");

            ResxSyncer syncer = new ResxSyncer();
            syncer.Add(file1);
            syncer.Add(file2);

            syncer.AllKeys();
        }
    }
}
