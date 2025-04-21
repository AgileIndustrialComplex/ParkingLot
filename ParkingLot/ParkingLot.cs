using System.Data;
using Microsoft.VisualBasic;
using ParkingLot.Interfaces;
using ParkingLot.Models;

namespace ParkingLot;

public class ParkingLot : IParkingLot
{
    private readonly IEnumerable<IParkingLevel> levels;

    private readonly Dictionary<IParkingSpot, IVehicle> parked = [];
    private readonly Dictionary<IVehicle, IParkingSpot> parkedInversed = [];
    private readonly Dictionary<Type, IParkingSpotValidator> validators;

    public ParkingLot(IEnumerable<IParkingLevel> levels, IEnumerable<IParkingSpotValidator> validators)
    {
        this.levels = levels;
        this.validators = validators.Select(v => KeyValuePair.Create(v.VehicleType, v)).ToDictionary();
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
        var validator = GetValidator(vehicle);
        foreach (var level in levels)
        {
            foreach (var row in level)
            {
                foreach (var spot in row)
                {
                    if (!parked.ContainsKey(spot) && validator.CanPark(vehicle, spot))
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

    public IParkingSpotValidator GetValidator(IVehicle vehicle)
    {
        if (validators.TryGetValue(vehicle.GetType(), out var validator))
        {
            return validator;
        }

        throw new InvalidOperationException($"No validator found for type: {vehicle.GetType()}");
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