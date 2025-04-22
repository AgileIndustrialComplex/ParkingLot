using ParkingLot.Interfaces;

namespace ParkingLot;

public class FreeParkingSpotValidator(IParkingStorage storage) : IParkingSpotValidator
{
    private readonly IParkingStorage storage = storage;

    public bool CanPark(IVehicle _, IParkingSpot spot)
    {
        return !storage.Has(spot);
    }
}