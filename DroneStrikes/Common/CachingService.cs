using System;
using System.IO;
using System.Threading.Tasks;

using Windows.Storage;
using Windows.Storage.FileProperties;

using Newtonsoft.Json;

namespace DroneStrikes.Common
{
    public class CachingService
    {
        private const string CacheFileNameFormat = "Cache_{0}_Data.json";

        public async Task<CacheItem> IsAvailable(string key, int cachePeriodInMinutes)
        {
            return await ContainsFileAsync(GenerateCacheFileName(key), cachePeriodInMinutes);
        }

        private async Task<CacheItem> ContainsFileAsync(string filename, int cachePeriodInMinutes)
        {
            try
            {
                StorageFile file = await ApplicationData.Current.LocalFolder.GetFileAsync(filename);
                BasicProperties properties = await file.GetBasicPropertiesAsync();

                bool isExpired = false;
                TimeSpan difference = new DateTimeOffset(DateTime.Now) - properties.DateModified;
                if (difference.TotalMinutes > cachePeriodInMinutes)
                {
                    isExpired = true;
                }

                return new CacheItem
                {
                    IsAvailable = true,
                    IsExpired = isExpired
                };
            }
            catch (FileNotFoundException ex)
            {
                return new CacheItem
                {
                    IsAvailable = false
                };
            }
        }

        public async Task<T> LoadCachedData<T>(string key, int cachePeriodInMinutes) where T : class
        {
            CacheItem cacheItem = await IsAvailable(key, cachePeriodInMinutes);
            if (cacheItem.IsAvailable && !cacheItem.IsExpired)
            {
                StorageFolder localFolder = ApplicationData.Current.LocalFolder;
                StorageFile cacheStorageFile = await localFolder.GetFileAsync(GenerateCacheFileName(key));
                using (Stream cacheStream = await cacheStorageFile.OpenStreamForReadAsync())
                {
                    using (var reader = new StreamReader(cacheStream))
                    {
                        string cacheDataString = await reader.ReadToEndAsync();
                        T cacheData = await JsonConvert.DeserializeObjectAsync<T>(cacheDataString);
                        return cacheData;
                    }
                }
            }
            return null;
        }

        public async void StoreCachedData<T>(string key, T dataToCache) where T : class
        {
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            StorageFile cacheStorageFile = await localFolder.CreateFileAsync(GenerateCacheFileName(key), CreationCollisionOption.ReplaceExisting);
            using (Stream cacheStream = await cacheStorageFile.OpenStreamForWriteAsync())
            {
                using (var writer = new StreamWriter(cacheStream))
                {
                    string cacheDataString = JsonConvert.SerializeObject(dataToCache);
                    await writer.WriteAsync(cacheDataString);
                }
            }
        }

        private static string GenerateCacheFileName(string key)
        {
            return string.Format(CacheFileNameFormat, key);
        }

        public async void Clear(string cacheKey)
        {
            CacheItem cacheItem = await ContainsFileAsync(GenerateCacheFileName(cacheKey), 1);
            if (cacheItem.IsAvailable)
            {
                StorageFolder localFolder = ApplicationData.Current.LocalFolder;
                StorageFile cacheStorageFile = await localFolder.GetFileAsync(GenerateCacheFileName(cacheKey));
                await cacheStorageFile.DeleteAsync();
            }
        }
    }
}
