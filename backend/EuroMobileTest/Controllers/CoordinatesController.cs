using EuroMobileTest.Core.Records;
using EuroMobileTest.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace EuroMobileTest.Controllers;

[Route("[controller]")]
[ApiController]
public class CoordinatesController : ControllerBase
{
    private readonly ICoordinatesService _coordinatesService;

    public CoordinatesController(ICoordinatesService coordinatesService)
    {
        _coordinatesService = coordinatesService;
    }

    [HttpGet]
    public ActionResult<Coordinates[]> GetRandomCoordinates(int count)
    {
        if (count < 1)
        {
            return BadRequest("Coordinates count must be more than 0");
        }

        return Ok(_coordinatesService.GenerateRandomCoordinates(count));
    }

    [HttpPost]
    public ActionResult<Distance> GetDistance(Coordinates[]? multipleCoordinates)
    {
        if (multipleCoordinates == null || multipleCoordinates.Length < 2)
        {
            return BadRequest(new Distance(0, 0));
        }

        return Ok(_coordinatesService.CalculateDistance(multipleCoordinates));
    }
}
