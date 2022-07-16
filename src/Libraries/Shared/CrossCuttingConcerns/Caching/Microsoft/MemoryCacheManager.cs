using System.Collections.Concurrent;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace Shared.CrossCuttingConcerns.Caching.Microsoft;

  public class MemoryCacheManager : ICacheManager
    {
        private readonly IMemoryCache _cache;
        protected readonly ConcurrentDictionary<string, bool> AllKeys;

        public MemoryCacheManager(IServiceProvider serviceProvider)
        {
            _cache = serviceProvider.GetService<IMemoryCache>();
            AllKeys = new ConcurrentDictionary<string, bool>();
        }


        #region Utilities
        /// <summary>
        /// Add key to dictionary
        /// </summary>
        /// <param name="key">Key of cached item</param>
        /// <returns>Itself key</returns>
        protected string AddKey(string key)
        {
            AllKeys.TryAdd(key, true);
            return key;
        }
        /// <summary>
        /// Remove key from dictionary
        /// </summary>
        /// <param name="key">Key of cached item</param>
        /// <returns>Itself key</returns>
        protected string RemoveKey(string key)
        {
            TryRemoveKey(key);
            return key;
        }


        public IList<string> GetKeys() => AllKeys.Keys.ToList();


        /// <summary>
        /// Try to remove a key from dictionary, or mark a key as not existing in cache
        /// </summary>
        /// <param name="key">Key of cached item</param>
        protected void TryRemoveKey(string key)
        {
            //try to remove key from dictionary
            if (!AllKeys.TryRemove(key, out _))
                //if not possible to remove key from dictionary, then try to mark key as not existing in cache
                AllKeys.TryUpdate(key, false, true);
        }
        #endregion

        public T GetCache<T>(string key)
        {
            return _cache.Get<T>(key);
        }

        public object GetCache(string key)
        {
            return _cache.Get(key);
        }

        public bool IsAddedCache(string key)
        {
            return _cache.TryGetValue(key, out _);
        }
        public void AddCache(string key, object data, int duration)
        {
            _cache.Set(AddKey(key), data, TimeSpan.FromMinutes(duration));
        }
        public void RemoveCache(string key)
        {
            _cache.Remove(RemoveKey(key));
        }

        public void RemoveCacheByPattern(string pattern)
        {
            
            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var keysToRemove = AllKeys.Where(d => regex.IsMatch(d.Key)).Select(d => d).ToList();

            foreach (var key in keysToRemove)
            {
                RemoveCache(key.Key);
            }
        }
    }