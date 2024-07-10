using System.ComponentModel.DataAnnotations;

namespace EuroMobileTest.Core.Records;

public record Coordinates
{
    public Coordinates(double latitude, double longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }

    [Required]
    [Range(-90, 90)]
    public double Latitude { get; init; }

    [Required]
    [Range(-180, 180)]
    public double Longitude { get; init; }
}