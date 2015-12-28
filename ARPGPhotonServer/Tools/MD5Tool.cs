using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace ARPGPhotonServer.Tools
{
    class MD5Tool
    {
        public static string GetMD5(string str)
        {
            var strBytes = Encoding.UTF8.GetBytes(str);
            var md5 = new MD5CryptoServiceProvider();
            var outPutBytes = md5.ComputeHash(strBytes);
            return BitConverter.ToString(outPutBytes).Replace("-", "");
        }
    }
}
