using ParkingLot.Interfaces;

namespace ParkingLot;

public class ParkingLot : IParkingLot
{
    private readonly IEnumerable<IParkingLevel> levels;

    private readonly Dictionary<IParkingSpot, IVehicle> parked = [];

    public ParkingLot(IEnumerable<IParkingLevel> levels)
    {
        this.levels = levels;
    }

    public IVehicle GetParkingVehicle(IParkingSpot spot)
    {
        throw new NotImplementedException();
    }

    public IParkingSpot Park(IVehicle vehicle)
    {
        foreach (var level in levels)
        {
            foreach (var row in level)
            {
                foreach (var spot in row)
                {
                    if (!parked.ContainsKey(spot))
                    {
                        parked[spot] = vehicle;
                        return spot;
                    }
                }
            }
        }

        throw new InvalidOperationException("No free spots left.");
    }

    public void UnPark(IVehicle vehicle)
    {
        throw new NotImplementedException();
    }
}