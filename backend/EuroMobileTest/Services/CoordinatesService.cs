using EuroMobileTest.Contracts;
using EuroMobileTest.Extensions;

namespace EuroMobileTest.Services
{
    public class CoordinatesService : ICoordinatesService
    {
        public Coordinates[] GenerateRandomCoordinates(int count)
        {
            Coordinates[] coordinates = new Coordinates[count];

            Random random = new Random();

            for (int i = 0; i < coordinates.Length; i++)
            {
                double latitude = Math.Round(random.NextDouble(-90, 90), 6);
                double longitude = Math.Round(random.NextDouble(-180, 180), 6);

                coordinates[i] = new Coordinates(latitude, longitude);
            }

            return coordinates;
        }

        public Distance CalculateDistance(Coordinates[] coordinates)
        {
            double distanceMeters = 0;

            for (int i = 0; i < coordinates.Length - 1; i++)
            {
                distanceMeters += CalculateDistaneInMeters(coordinates[i], coordinates[i + 1]);
            }

            double distanceMiles = distanceMeters / 1609.34;

            return new Distance(Math.Round(distanceMeters, 3), Math.Round(distanceMiles, 3));
        }

        private double CalculateDistaneInMeters(Coordinates coordinate1, Coordinates coordinate2)
        {
            const double earthRadiusMeters = 6371000;

            double latitudeRadians1 = coordinate1.Latitude * (Math.PI / 180);
            double latitudeRadians2 = coordinate2.Latitude * (Math.PI / 180);
            double deltaLatitude = (coordinate2.Latitude - coordinate1.Latitude) * (Math.PI / 180);
            double deltaLongitude = (coordinate2.Longitude - coordinate1.Longitude) * (Math.PI / 180);

            double centralAngle = Math.Sin(deltaLatitude / 2) * Math.Sin(deltaLatitude / 2) +
                       Math.Cos(latitudeRadians1) * Math.Cos(latitudeRadians2) *
                       Math.Sin(deltaLongitude / 2) * Math.Sin(deltaLongitude / 2);
            double angularDistance = 2 * Math.Atan2(Math.Sqrt(centralAngle), Math.Sqrt(1 - centralAngle));

            return earthRadiusMeters * angularDistance;
        }
    }
}
