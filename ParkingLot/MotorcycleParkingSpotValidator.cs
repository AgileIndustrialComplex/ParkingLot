using ParkingLot.Interfaces;
using ParkingLot.Models;

namespace ParkingLot;

public class MotorcycleParkingLotValidator : IVehicleTypeParkingSpotValidator
{
    public Type VehicleType => typeof(Motorcycle);

    public bool CanPark(IVehicle vehicle, IParkingSpot spot)
    {
        IParkingSpotValidator.ThrowIfInvalidType<Motorcycle, MotorcycleParkingLotValidator>(vehicle);

        return true;
    }
}