using EuroMobileTest.Core.Records;

namespace EuroMobileTest.Core.Services;

public interface ICoordinatesService
    {
        public Coordinates[] GenerateRandomCoordinates(int count);
        public Distance CalculateDistance(Coordinates[] coordinates);
    }
