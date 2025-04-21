namespace ParkingLot.Interfaces;

// IParkingSpotValidator check if vehicle can park on spot
public interface IParkingSpotValidator
{
    // VehicleType is the vehicle the validator is working with
    Type VehicleType { get; }
    
    // CanPark determines if vehicle can park on spot
    bool CanPark(IVehicle vehicle, IParkingSpot spot);
}