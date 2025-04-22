namespace ParkingLot.Interfaces;

public interface IParkingSpotSelector
{
    IParkingSpot GetSpot(IVehicle vehicle, IEnumerable<IParkingSpot> spots);
}