using CacheManager.Core;
using Core.Authorization.Common.Abstract;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Core.Authorization.Common.Models.Helpers;
using StackExchange.Redis;

namespace Core.Authorization.Bll.Helpers.Cache
{
    public class RedisCacheStore<T> : ICacheStoreHelper<T> where T : class
    {
        private ICacheManager<string> CacheManager { get; }
        private ConnectionMultiplexer RedisConnection { get; }
        private IServer RedisServer { get; }
        private IDatabase RedisDatabase { get; }
        public RedisCacheStore()
        {
            CacheManager = CacheFactory.Build<string>(settings =>
            {
                settings
                    .WithRedisConfiguration("redis", config =>
                    {
                        config.WithAllowAdmin()
                            .WithDatabase(0)
                            
                            .WithEndpoint(ConfigurationHelper.RedisServer, ConfigurationHelper.RedisPort);
                    })
                    .WithMaxRetries(100)
                    .WithRetryTimeout(50)
                    .WithJsonSerializer()
                    .WithRedisBackplane("redis")
                    .WithRedisCacheHandle("redis", true);
            });
            RedisConnection = ConnectionMultiplexer.Connect($"{ConfigurationHelper.RedisServer}:{ConfigurationHelper.RedisPort}");
            RedisServer = RedisConnection.GetServer(ConfigurationHelper.RedisServer, ConfigurationHelper.RedisPort);
            RedisDatabase = RedisConnection.GetDatabase();
        }

        public T this[string key] => CacheManager.Get(key) != null ? JsonConvert.DeserializeObject<T>(CacheManager.Get(key)) : null;

        public void Add(string key, T value, TimeSpan expirationTime)
        {
            CacheManager.Add(key, JsonConvert.SerializeObject(value));
            CacheManager.Expire(key, ExpirationMode.Absolute, expirationTime);
        }

        public void Remove(string key)
        {
            CacheManager.Remove(key);

        }

        public bool ContainsKey(string key)
        {
            return CacheManager.Get(key) != null;
        }

        public void Clear()
        {
            CacheManager.Clear();
        }

        public bool ContainsKeysFind(string region)
        {
            var keys = RedisServer.Keys(0, region);
            return keys.Any();
        }

        public void CreateOrUpdate(string key, T value, TimeSpan expirationTime)
        {
            if (CacheManager[key] != null) CacheManager.Remove(key);
            Add(key, value, expirationTime);
        }

        public void ClearRegion(string region)
        {
            var keys = RedisServer.Keys(0, region);
            foreach (var key in keys)
            {
                CacheManager.Remove(key);
            }
        }

        public IEnumerable<T> GetList(string key)
        {

            var length = RedisDatabase.ListLength(key);
            if (length == 0) yield break;
            var result = RedisDatabase.ListRange(key, 0, length);
            foreach (var item in result)
            {
                yield return Deserialize<T>(item);
            }
        }
        private T Deserialize<T>(string serialized)
        {
            return JsonConvert.DeserializeObject<T>(serialized);
        }

    }
}
