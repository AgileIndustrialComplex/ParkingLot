using ParkingLot.Interfaces;
using ParkingLot.Models;

namespace ParkingLot;

public class CarParkingLotValidator : IVehicleTypeParkingSpotValidator
{
    public Type VehicleType => typeof(Car);

    public bool CanPark(IVehicle vehicle, IParkingSpot spot)
    {
        IParkingSpotValidator.ThrowIfInvalidType<Car, CarParkingLotValidator>(vehicle);

        if (spot is CarParkingSpot)
        {
            return true;
        }

        return false;
    }
}