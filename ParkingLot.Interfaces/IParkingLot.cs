namespace ParkingLot.Interfaces;

// IParkingLot represents the interface for interaction with the parking lot
public interface IParkingLot
{
    // Park parks the vehicle in an appropriate spot (if found) and returns the spot
    // otherwise an error is thrown
    IParkingSpot Park(IVehicle vehicle);

    // UnPark removes the vehicle from the parking,
    // if vehicle is not parked an error is thrown
    void UnPark(IVehicle vehicle);
    
    // GetParkingVehicle returns a vehicle that is park at given spot
    // if no vehicle is parked on given spot or the spot is not in the
    // parking lot, an error is throw
    IVehicle GetParkingVehicle(IParkingSpot spot);
}