using ParkingLot.Interfaces;

namespace ParkingLot;

public class ParkingLot(IEnumerable<IParkingLevel> levels, IParkingStorage storage, IParkingSpotSelector selector) : IParkingLot
{
    private readonly IEnumerable<IParkingLevel> levels = levels;
    private readonly IParkingStorage storage = storage;
    private readonly IParkingSpotSelector selector = selector;

    public IVehicle GetParkingVehicle(IParkingSpot spot)
    {
        var vehicle = storage.Get(spot);
        return vehicle;
    }

    public IParkingSpot Park(IVehicle vehicle)
    {
        var spot = selector.GetSpot(vehicle, GetParkingSpots());
        storage.Create(vehicle, spot);
        return spot;
    }

    public void UnPark(IVehicle vehicle)
    {
        storage.Remove(vehicle);
    }

    private IEnumerable<IParkingSpot> GetParkingSpots()
    {
        foreach (var level in levels)
        {
            foreach (var row in level)
            {
                foreach (var spot in row)
                {
                    yield return spot;
                }
            }
        }
    }
}