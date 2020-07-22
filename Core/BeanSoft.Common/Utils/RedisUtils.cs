using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Core.Utils
{
    public class RedisUtils
    {
        //private static string conectionString = "192.168.137.152:6379";
        //static RedisUtils()
        //{
        //    RedisUtils.lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
        //    {
        //        return ConnectionMultiplexer.Connect(conectionString);
        //    });
        //}

        //private static Lazy<ConnectionMultiplexer> lazyConnection;

        //public static ConnectionMultiplexer Connection {
        //    get
        //    {
        //        return lazyConnection.Value;
        //    }
        //}

        //public static void SetCacheData(IDistributedCache distributedCache, IConfiguration configuration, object result, string key)
        //{
        //    var expireMinute = !string.IsNullOrEmpty(configuration["CachingExpireMinute"]) ? int.Parse(configuration["CachingExpireMinute"]) : 1;
        //    var data = JsonConvert.SerializeObject(result);
        //    var expireTime = new DistributedCacheEntryOptions { SlidingExpiration = TimeSpan.FromMinutes(expireMinute) };
        //    distributedCache.SetStringAsync(key, data, expireTime);
        //}

        //public static void SetCacheData(IDistributedCache distributedCache, int expireMinute, object result, string key)
        //{
        //    var data = JsonConvert.SerializeObject(result);
        //    var expireTime = new DistributedCacheEntryOptions { SlidingExpiration = TimeSpan.FromMinutes(expireMinute) };
        //    distributedCache.SetStringAsync(key, data, expireTime);
        //}

        //public static void SetCacheData( object result, string key)
        //{
        //    var cache = Connection.GetDatabase();
        //    if (cache.StringGet(key).ToString() != null)
        //    {
        //        cache.KeyDelete(key);
        //    }
        //    cache.StringSet(key, JsonConvert.SerializeObject(result));
        //}
        //public static string GetCacheData(string key)
        //{
        //    var cache = Connection.GetDatabase();
        //    return cache.StringGet(key);
        //}

        //public static void RemoveCache(IDistributedCache distributedCache, string key)
        //{
        //    distributedCache.Remove(key);
        //}

        //public static void SetData<T>(string key, T data)
        //{
        //    using (var redis = ConnectionMultiplexer.Connect(conectionString))
        //    {
        //        IDatabase db = redis.GetDatabase();
        //        db.StringSet(key, JsonConvert.SerializeObject(data));
        //        redis.Close();
        //    }
        //}

        //public static T GetData<T>(string key)
        //{
        //    using (var redis = ConnectionMultiplexer.Connect(conectionString))
        //    {
        //        try
        //        {
        //            IDatabase db = redis.GetDatabase();
        //            var res = db.StringGet(key);

        //            redis.Close();
        //            if (res.IsNull)
        //                return default(T);
        //            else
        //                return JsonConvert.DeserializeObject<T>(res);
        //        }
        //        catch
        //        {
        //            return default(T);
        //        }

        //    }
        //}
    }
}
