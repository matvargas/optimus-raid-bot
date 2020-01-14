using Microsoft.VisualStudio.TestTools.UnitTesting;
using RaidBot.Engine.Utility.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Engine.Utility.Security.Tests
{
    [TestClass()]
    public class CryptographyTests
    {
        [TestMethod()]
        public void EncryptTest()
        {
            Cryptography.Encrypt(new sbyte[] { 2, 4, 69, -45, 98, -56, 103, 75, -23, 10, 61, 107, -31, -4, -5, -52, -70, -81, 75, -101, -45, 90, -58, 45, 117, 19, -107, -80, -123, 111, -60, 81, 50, 126, -67, -119, 116, 105, 101, -14, -57, -125, 5, -71, 24, 6, -34, 125, -85, 47, -14, -48, 77, -101, 121, 109, 94, -43, 103, 16, 77, 49, -117, -50, 59, -14, -109, 76, -7, -119, 116, -9, 5, 41, 84, 85, -63, 66, -123, -106, 48,  }, "joix56diSxf0NR1[%=QyAgfLYW5.'d;Z", "acorbeau1", "Asyade123581321");
        }
    }
}