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
    private readonly Dictionary<Type, IParkingSpotValidator> validators;

    public ParkingLot(IEnumerable<IParkingLevel> levels, IEnumerable<IParkingSpotValidator> validators, IParkingStorage storage)
    {
        this.levels = levels;
        this.storage = storage;
        this.validators = validators.Select(v => KeyValuePair.Create(v.VehicleType, v)).ToDictionary();
    }

    public IVehicle GetParkingVehicle(IParkingSpot spot)
    {
        var vehicle = storage.Get(spot);
        return vehicle;
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
                    if (!storage.Has(spot) && validator.CanPark(vehicle, spot))
                    {
                        storage.Create(vehicle, spot);
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
        storage.Remove(vehicle);
    }
}