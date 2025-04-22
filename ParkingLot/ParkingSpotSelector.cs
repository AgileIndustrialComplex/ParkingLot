using ParkingLot.Interfaces;

namespace ParkingLot;

public class ParkingSpotSelector(IEnumerable<IParkingSpotValidator> validators) : IParkingSpotSelector
{
    private readonly IEnumerable<IParkingSpotValidator> validators = validators;

    public IParkingSpot GetSpot(IVehicle vehicle, IEnumerable<IParkingSpot> spots)
    {
        foreach (var spot in spots)
        {
            if (validators.All(v => v.CanPark(vehicle, spot)))
            {
                return spot;
            }
        }

        throw new InvalidOperationException("No spot found");
    }

    public IEnumerable<IParkingSpotValidator> GetValidators(IVehicle vehicle)
    {
        return validators.Where(
            v => {
                if (v is IVehicleTypeParkingSpotValidator vt)
                {
                    return vehicle.GetType() == vt.VehicleType;
                }

                return true;
            }
        );
    }
}