using System.Text.Json;
using RedisApi.Models;
using StackExchange.Redis;

namespace RedisApi.Data;

public class RedisPlatformRepo : IPlatformRepo
{
    private readonly IConnectionMultiplexer _redis;

    public RedisPlatformRepo(IConnectionMultiplexer redis)
    {
        _redis = redis;
    }

    public void CreatePlatform(Platform platform)
    {
        if (platform == null)
        {
            throw new ArgumentOutOfRangeException(nameof(platform));
        }

        var db = _redis.GetDatabase();
        var data = JsonSerializer.Serialize(platform);

        // db.StringSet(platform.Id, data);
        // db.SetAdd("platformSet", data);
        db.HashSet("hashplatform", new HashEntry[] { new HashEntry(platform.Id, data) });
    }

    public IEnumerable<Platform?>? GetAllPlatforms()
    {
        var db = _redis.GetDatabase();
        //var complateSet = db.SetMembers("platformSet");
        var complateHash = db.HashGetAll("hashplatform");
        if (complateHash.Length > 0)
        {
            var obj = Array.ConvertAll(complateHash, val => JsonSerializer.Deserialize<Platform>(val.Value)).ToList();
            return obj;
        }

        return null;
    }

    public Platform? GetPlatformById(string id)
    {
        var db = _redis.GetDatabase();

        //var platform = db.StringGet(id);
        var platform = db.HashGet("hashplatform",id);

        if (!string.IsNullOrWhiteSpace(platform))
        {
            return JsonSerializer.Deserialize<Platform>(platform);
        }

        return null;
    }
}