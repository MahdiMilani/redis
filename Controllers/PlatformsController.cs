using Microsoft.AspNetCore.Mvc;
using RedisApi.Data;
using RedisApi.Models;

namespace RedisApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlatformsController : ControllerBase
{
    private readonly IPlatformRepo _platformRepo;
    public PlatformsController(IPlatformRepo platformRepo)
    {
        _platformRepo = platformRepo;
    }

    [HttpGet("{id}", Name = nameof(GetPlatformById))]
    public ActionResult<Platform> GetPlatformById(string id)
    {
        var platfrom = _platformRepo.GetPlatformById(id);

        if (null != platfrom)
        {
            return Ok(platfrom);
        }

        return NotFound();
    }

    [HttpPost]
    public ActionResult<Platform> CreatePlatform(Platform platform)
    {
        _platformRepo.CreatePlatform(platform);
        return CreatedAtRoute(nameof(GetPlatformById), new { Id = platform.Id }, platform);
    }

    [HttpGet]
    public ActionResult<IEnumerable<Platform>> GetAllPlatforms()
    {
        return Ok(_platformRepo.GetAllPlatforms());
    }
}