using System;
using System.Runtime.Caching;

namespace PhanMemQuanLyThiCong.Common.Helper
{
    public class MemoryCacheHelper
    {
        /// <summary>
        /// Get cache value by key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object GetValue(string key)
        {
            return MemoryCache.Default.Get(key);
        }

        /// <summary>
        /// Add a cache object with date expiration
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="absExpiration"></param>
        /// <returns></returns>
        public static bool Add(string key, object value, DateTimeOffset absExpiration)
        {
            return MemoryCache.Default.Add(key, value, absExpiration);
        }

        /// <summary>
        /// Add a cache object with date expiration
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="absExpiration"></param>
        /// <returns></returns>
        public static void Set(string key, object value, DateTimeOffset absExpiration)
        {
            MemoryCache.Default.Set(key, value, absExpiration);
        }

        /// <summary>
        /// Delete cache value from key
        /// </summary>
        /// <param name="key"></param>
        public static void Delete(string key)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            if (memoryCache.Contains(key))
            {
                memoryCache.Remove(key);
            }
        }

        public static bool CheckExistKey(string key)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            return memoryCache.Contains(key);
        }
    }
}