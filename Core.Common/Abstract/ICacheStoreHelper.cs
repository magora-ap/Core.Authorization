using System;
using System.Collections.Generic;

namespace Core.Authorization.Common.Abstract
{
    public interface ICacheStoreHelper<T>
    {
        T this[string key] { get; }

        void Add(string key, T value, TimeSpan expirationTime);
        void CreateOrUpdate(string key, T value, TimeSpan expirationTime);

        void Remove(string key);
        void ClearRegion(string region);

        bool ContainsKey(string key);

        void Clear();
        bool ContainsKeysFind(string pattern);

        IEnumerable<T> GetList(string key);
    }
}
