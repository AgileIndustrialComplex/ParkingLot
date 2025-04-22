namespace ParkingLot.Interfaces;

// IParkingSpotSelector selects a spot given vehicle can park on
public interface IParkingSpotSelector
{
    // GetSpot selects a spot given vehicle can park on
    IParkingSpot GetSpot(IVehicle vehicle, IEnumerable<IParkingSpot> spots);
}
