using RedisApi.Models;

namespace RedisApi.Data;

public interface IPlatformRepo
{
    void CreatePlatform(Platform platform);
    Platform? GetPlatformById(string id);
    IEnumerable<Platform?>? GetAllPlatforms();
}