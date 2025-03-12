using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;

namespace PhanMemQuanLyThiCong.Common.Helper
{
    public static class CryptoHelper
    {
        private static readonly string FirstPass = "3";
        /// <summary>
        /// DecodeFrom64
        /// </summary>
        /// <param name="encodedData"></param>
        /// <returns></returns>
        public static string DecodeFrom64(string encodedData)
        {
            try
            {
                encodedData = encodedData.Remove(0, FirstPass.Length);
                var encodedDataAsBytes
                  = Convert.FromBase64String(encodedData);

                var returnValue =
                  Encoding.ASCII.GetString(encodedDataAsBytes);

                return returnValue;
            }
            catch (Exception ex)
            {
                throw new Exception("Exception DecodeBase64");
            }
        }

        /// <summary>
        /// Base64Encode
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            string textBase64 = Convert.ToBase64String(plainTextBytes);
            textBase64 = textBase64.Insert(0, FirstPass);
            return textBase64;
        }

        public static string Base64EncodeObject<T>(T obj)
        {
            return Base64Encode(JsonConvert.SerializeObject(obj));    
        }

        public static T Base64DecodeToObject<T>(string str)
        {
            string json = null;
            try
            {
                json = DecodeFrom64(str);
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch
            {
                return default(T);

            }

                
        }

        /// <summary>
        /// Compresses the string.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public static string CompressString(string text)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(text);
            var memoryStream = new MemoryStream();
            using (var gZipStream = new GZipStream(memoryStream, CompressionMode.Compress, true))
            {
                gZipStream.Write(buffer, 0, buffer.Length);
            }

            memoryStream.Position = 0;

            var compressedData = new byte[memoryStream.Length];
            memoryStream.Read(compressedData, 0, compressedData.Length);

            var gZipBuffer = new byte[compressedData.Length + 4];
            Buffer.BlockCopy(compressedData, 0, gZipBuffer, 4, compressedData.Length);
            Buffer.BlockCopy(BitConverter.GetBytes(buffer.Length), 0, gZipBuffer, 0, 4);
            return Convert.ToBase64String(gZipBuffer);
        }

        /// <summary>
        /// Decompresses the string.
        /// </summary>
        /// <param name="compressedText">The compressed text.</param>
        /// <returns></returns>
        public static string DecompressString(string compressedText)
        {
            byte[] gZipBuffer = Convert.FromBase64String(compressedText);
            using (var memoryStream = new MemoryStream())
            {
                int dataLength = BitConverter.ToInt32(gZipBuffer, 0);
                memoryStream.Write(gZipBuffer, 4, gZipBuffer.Length - 4);

                var buffer = new byte[dataLength];

                memoryStream.Position = 0;
                using (var gZipStream = new GZipStream(memoryStream, CompressionMode.Decompress))
                {
                    gZipStream.Read(buffer, 0, buffer.Length);
                }

                return Encoding.UTF8.GetString(buffer);
            }
        }

        /// <summary>
        /// Split by lengh size
        /// </summary>
        /// <param name="str"></param>
        /// <param name="chunkSize"></param>
        /// <returns></returns>
        public static List<string> Split(string str, int chunkSize)
        {
            List<string> lstDatas = new List<string>();
            int _countBlock = (int)Math.Ceiling((double)str.Length / chunkSize);
            for (int i = 0; i < _countBlock; i++)
            {
                if (i == _countBlock - 1) lstDatas.Add(str.Substring(i * chunkSize));
                else lstDatas.Add(str.Substring(i * chunkSize, chunkSize));
            }
            return lstDatas;
        }

        /// <summary>
        /// Truncate
        /// </summary>
        /// <param name="s"></param>
        /// <param name="i"></param>
        /// <param name="chunkSize"></param>
        /// <returns></returns>
        public static string Truncate(this string s, int i, int chunkSize)
        {

            if (s.Substring(i * chunkSize).Length > chunkSize)
                return s.Substring(i * chunkSize, chunkSize);
            return s.Substring(i * chunkSize);
        }
    }
}