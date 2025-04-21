using Microsoft.VisualBasic;
using ParkingLot.Interfaces;

namespace ParkingLot;

public class ParkingStorage : IParkingStorage
{
    private readonly Dictionary<IVehicle, IParkingSpot> vehicleToSpot = [];
    private readonly Dictionary<IParkingSpot, IVehicle> spotToVehicle = [];

    public void Create(IVehicle vehicle, IParkingSpot spot)
    {
        if (vehicleToSpot.ContainsKey(vehicle))
        {
            throw new InvalidOperationException("Given vehicle does not exists");
        }
        if (spotToVehicle.ContainsKey(spot))
        {
            throw new InvalidOperationException("Given parking spot does not exist");
        }

        vehicleToSpot[vehicle] = spot;
        spotToVehicle[spot] = vehicle;
    }

    public IVehicle Get(IParkingSpot spot)
    {
        if (spotToVehicle.TryGetValue(spot, out var vehicle))
        {
            return vehicle;
        }

        throw new InvalidOperationException("Given parking spot does not exist");
    }

    public IParkingSpot Get(IVehicle vehicle)
    {
        if (vehicleToSpot.TryGetValue(vehicle, out var spot))
        {
            return spot;
        }

        throw new InvalidOperationException("Given vehicle does not exist");
    }

    public bool Has(IVehicle vehicle)
    {
        var exists = vehicleToSpot.ContainsKey(vehicle);
        return exists;
    }

    public bool Has(IParkingSpot spot)
    {
        var exists = spotToVehicle.ContainsKey(spot);
        return exists;
    }

    public void Remove(IVehicle vehicle)
    {
        if (vehicleToSpot.TryGetValue(vehicle, out var spot))
        {
            vehicleToSpot.Remove(vehicle);
            spotToVehicle.Remove(spot);
            return;
        }

        throw new InvalidOperationException("Given vehicle does not exist");
    }

    public void Remove(IParkingSpot spot)
    {
        if (spotToVehicle.TryGetValue(spot, out var vehicle))
        {
            vehicleToSpot.Remove(vehicle);
            spotToVehicle.Remove(spot);
            return;
        }

        throw new InvalidOperationException("Given parking spot does not exist");
    }
}