using ParkingLot.Interfaces;
using ParkingLot.Models;

namespace ParkingLot;

public class MotorcycleParkingLotValidator : IParkingSpotValidator
{
    public Type VehicleType => typeof(Motorcycle);

    public bool CanPark(IVehicle vehicle, IParkingSpot spot)
    {
        if (vehicle is not Motorcycle)
        {
            var typeName = vehicle.GetType().ToString();
            throw new InvalidOperationException($"CarParkingLotValidator cannot be used with type: {typeName}");
        }
        if (spot is CarParkingSpot)
        {
            return true;
        }
        if (spot is MotorcycleParkingSpot)
        {
            return true;
        }

        return false;
    }
}