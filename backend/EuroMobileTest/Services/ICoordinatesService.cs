using EuroMobileTest.Contracts;

namespace EuroMobileTest.Services
{
    public interface ICoordinatesService
    {
        public Coordinates[] GenerateRandomCoordinates(int count);
        public Distance CalculateDistance(Coordinates[] coordinates);
    }
}
