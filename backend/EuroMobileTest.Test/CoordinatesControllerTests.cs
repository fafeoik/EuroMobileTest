using EuroMobileTest.Core.Records;
using System.Net.Http.Json;
using System.Net;
using EuroMobileTest.Core.Extensions;
using Newtonsoft.Json;

namespace EuroMobileTest.Test;

public class CoordinatesControllerTests
{
    [Fact]
    public async Task GetCoordinatesWithCountGreaterThanOne()
    {
        await using CoordinatesApiApplication app = new CoordinatesApiApplication();
        using HttpClient httpClient = app.CreateClient();

        using HttpResponseMessage response = await httpClient.GetAsync("/coordinates?count=2");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetCoordinatesWithCountLessThanOne()
    {
        await using CoordinatesApiApplication app = new CoordinatesApiApplication();
        using HttpClient httpClient = app.CreateClient();

        using HttpResponseMessage response = await httpClient.GetAsync("/coordinates?count=0");

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task PostDistanceWithLengthGreaterThanOne()
    {
        await using CoordinatesApiApplication app = new CoordinatesApiApplication();
        using HttpClient httpClient = app.CreateClient();

        Coordinates[] coordinates =
        {
            new Coordinates ( 54.544532, 118.954033 ),
            new Coordinates ( -19.282392,1.231232 )
        };

        HttpResponseMessage response = await httpClient.PostAsJsonAsync("/coordinates", coordinates);

        string responseContent = await response.Content.ReadAsStringAsync();
        Dictionary<string, double>? distanceData = JsonConvert.DeserializeObject<Dictionary<string, double>>(responseContent);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.True(distanceData?["meters"] > 0);
        Assert.True(distanceData?["miles"] > 0);
    }

    [Fact]
    public async Task PostDistanceWithLengthLessOne()
    {
        await using CoordinatesApiApplication app = new CoordinatesApiApplication();
        using HttpClient httpClient = app.CreateClient();

        Coordinates[] coordinates = Array.Empty<Coordinates>();

        HttpResponseMessage response = await httpClient.PostAsJsonAsync("/coordinates", coordinates);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal("{\"meters\":0,\"miles\":0}", await response.Content.ReadAsStringAsync());
    }

    [Fact]
    public async Task PostDistanceWithLengthOne()
    {
        await using CoordinatesApiApplication app = new CoordinatesApiApplication();
        using HttpClient httpClient = app.CreateClient();

        Random random = new Random();

        double latitude = Math.Round(random.NextDouble(-90, 90), 6);
        double longitude = Math.Round(random.NextDouble(-180, 180), 6);

        Coordinates[] coordinates =
        {
            new Coordinates (latitude, longitude )
        };

        HttpResponseMessage response = await httpClient.PostAsJsonAsync("/coordinates", coordinates);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal("{\"meters\":0,\"miles\":0}", await response.Content.ReadAsStringAsync());
    }

    [Fact]
    public async Task PostDistanceWithNullBody()
    {
        await using CoordinatesApiApplication app = new CoordinatesApiApplication();
        using HttpClient httpClient = app.CreateClient();

        Coordinates[]? coordinates = null;

        HttpResponseMessage response = await httpClient.PostAsJsonAsync("/coordinates", coordinates);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal("{\"meters\":0,\"miles\":0}", await response.Content.ReadAsStringAsync());
    }
}