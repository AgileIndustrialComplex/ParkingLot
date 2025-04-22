namespace ParkingLot.Interfaces;

// IParkingSpotValidator check if vehicle can park on spot
public interface IParkingSpotValidator
{
    // CanPark determines if vehicle can park on spot
    bool CanPark(IVehicle vehicle, IParkingSpot spot);

    public static void ThrowIfInvalidType<T, S>(IVehicle vehicle)
        where T : IVehicle
        where S : IParkingSpotValidator
    {
        if (vehicle is not T)
        {
            var message = $"{typeof(S)} cannot be used with type: {vehicle.GetType().FullName}";
            throw new InvalidOperationException(message);
        }

    }
}