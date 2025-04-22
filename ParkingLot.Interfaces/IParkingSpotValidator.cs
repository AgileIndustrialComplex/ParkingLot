namespace ParkingLot.Interfaces;

// IParkingSpotValidator check if vehicle can park on spot
public interface IParkingSpotValidator
{
    // CanPark determines if vehicle can park on spot
    bool CanPark(IVehicle vehicle, IParkingSpot spot);
}