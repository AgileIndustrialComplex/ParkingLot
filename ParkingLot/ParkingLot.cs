using Microsoft.VisualBasic;
using ParkingLot.Interfaces;
using ParkingLot.Models;

namespace ParkingLot;

public class ParkingLot : IParkingLot
{
    private readonly IEnumerable<IParkingLevel> levels;

    private readonly Dictionary<IParkingSpot, IVehicle> parked = [];
    private readonly Dictionary<IVehicle, IParkingSpot> parkedInversed = [];

    public ParkingLot(IEnumerable<IParkingLevel> levels)
    {
        this.levels = levels;
    }

    public IVehicle GetParkingVehicle(IParkingSpot spot)
    {
        var exists = parked.TryGetValue(spot, out var vehicle);
        if (exists && vehicle is not null)
        {
            return vehicle;
        }
        if (!exists)
        {
            throw new InvalidOperationException("Parking spot not in parking lot");
        }

        throw new InvalidOperationException("Parking spot is not occupied");
    }

    public IParkingSpot Park(IVehicle vehicle)
    {
        foreach (var level in levels)
        {
            foreach (var row in level)
            {
                foreach (var spot in row)
                {
                    if (!parked.ContainsKey(spot) && CanPark(vehicle, spot))
                    {
                        parked[spot] = vehicle;
                        parkedInversed[vehicle] = spot;
                        return spot;
                    }
                }
            }
        }

        throw new InvalidOperationException("No free spots left.");
    }

    public bool CanPark(IVehicle vehicle, IParkingSpot spot)
    {
        if (vehicle is Car && spot is CarParkingSpot)
        {
            return true;
        }
        if (vehicle is Motorcycle && spot is MotorcycleParkingSpot)
        {
            return true;
        }
        if (vehicle is Motorcycle && spot is CarParkingSpot)
        {
            return true;
        }

        return false;
    }

    public void UnPark(IVehicle vehicle)
    {
        var exists = parkedInversed.TryGetValue(vehicle, out var spot);
        if (!exists)
        {
            throw new InvalidOperationException("Vehicle is not parked in the parking lot");
        }
        if (spot is null)
        {
            throw new InvalidOperationException("Invalid internal state, parking spot for vehicle is null");
        }

        parkedInversed.Remove(vehicle);
        parked.Remove(spot);
    }
}