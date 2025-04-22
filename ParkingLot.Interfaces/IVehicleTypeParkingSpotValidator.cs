namespace ParkingLot.Interfaces;

public interface IVehicleTypeParkingSpotValidator : IParkingSpotValidator
{
    public Type VehicleType { get; set; }
}