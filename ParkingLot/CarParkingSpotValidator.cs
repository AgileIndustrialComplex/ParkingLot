using ParkingLot.Interfaces;
using ParkingLot.Models;

namespace ParkingLot;

public class CarParkingLotValidator : IParkingSpotValidator
{
    public Type VehicleType => typeof(Car);

    public bool CanPark(IVehicle vehicle, IParkingSpot spot)
    {
        if (vehicle is not Car)
        {
            var typeName = vehicle.GetType().ToString();
            throw new InvalidOperationException($"CarParkingLotValidator cannot be used with type: {typeName}");
        }
        if (spot is CarParkingSpot)
        {
            return true;
        }

        return false;
    }
}