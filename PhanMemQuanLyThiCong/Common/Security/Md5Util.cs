using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Common.Security
{
    public class Md5Util
    {
        public static string Md5EnCrypt(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text
            md5.ComputeHash(Encoding.ASCII.GetBytes(text));

            //get hash result after compute it
            var result = md5.Hash;

            var strBuilder = new StringBuilder();
            for (var i = 0; i < result.Length; i++)
                //change it into 2 hexadecimal digits
                //for each byte
                strBuilder.Append(result[i].ToString("x2"));

            return strBuilder.ToString();
        }
    }
}
