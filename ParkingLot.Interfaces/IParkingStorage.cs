namespace ParkingLot.Interfaces;

// IParkingStorage tracks currently parked cars in the parking lot
public interface IParkingStorage
{
    // Create makes a new entry into the storage, throws if vehicle or spot
    // are already in the storage
    void Create(IVehicle vehicle, IParkingSpot spot);
    
    // Remove deletes the entry associated with vehicle
    void Remove(IVehicle vehicle);

    // Remove deletes the entry associated with parking spot
    void Remove(IParkingSpot spot);
    
    // Has checks if vehicle exists in storage
    bool Has(IVehicle vehicle);
    
    // Has checks if spot exists in storage
    bool Has(IParkingSpot spot);

    // Get fetches vehicle associated with parking spot 
    IVehicle Get(IParkingSpot spot);

    // Get fetches parking spot associated with vehicle 
    IParkingSpot Get(IVehicle vehicle);
}