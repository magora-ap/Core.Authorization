using System;
using Newtonsoft.Json;

namespace Core.Authorization.Common.Models
{
    public class HashModel
    {
        public Guid Id { get; set; }
        public Guid CreateUserId { get; set; }
        public int TypeId { get; set; }
        public string Info { get; set; }
        public T GetDataModel<T>() where T : new()
        {
            return !string.IsNullOrEmpty(Info)
                ? JsonConvert.DeserializeObject<T>(Info)
                : new T();
        }

        public void SetDataModel<T>(T model) where T : new()
        {
            Info = JsonConvert.SerializeObject(model);
        }
        public DateTime CreateTimestamp { get; set; }
        public DateTime? DeleteTimestamp { get; set; }
        public bool IsActive { get; set; }
    }
}
