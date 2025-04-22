using System.Data;
using System.Data.SqlTypes;
using Microsoft.VisualBasic;
using ParkingLot.Interfaces;
using ParkingLot.Models;

namespace ParkingLot;

public class ParkingLot : IParkingLot
{
    private readonly IEnumerable<IParkingLevel> levels;
    private readonly IParkingStorage storage;
    private readonly IParkingSpotSelector selector;

    public ParkingLot(IEnumerable<IParkingLevel> levels, IParkingStorage storage, IParkingSpotSelector selector)
    {
        this.levels = levels;
        this.storage = storage;
        this.selector = selector;
    }

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

        throw new InvalidOperationException("No free spots left.");
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

    public void UnPark(IVehicle vehicle)
    {
        storage.Remove(vehicle);
    }
}