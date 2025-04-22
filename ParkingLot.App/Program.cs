using ParkingLot;
using ParkingLot.Interfaces;
using ParkingLot.Models;

ParkingLevel[] levels = [
    new(
        [
            new ParkingRow(
                [
                    new MotorcycleParkingSpot()
                ]
            )
        ]
    ),
    new(
        [
            new ParkingRow(
                [
                    new CarParkingSpot()
                ]
            )
        ]
    )
];

ParkingStorage storage = new();
ParkingSpotSelector selector = new([
    new MotorcycleParkingLotValidator(),
    new CarParkingLotValidator(),
    new FreeParkingSpotValidator(storage),
]);
ParkingLot.ParkingLot parkingLot = new(levels, storage, selector);

Motorcycle moto = new();
Motorcycle anotherMoto = new();

// Motorcycle can be parked on motorcycle spot
var spot = parkingLot.Park(moto);
Console.WriteLine($"{moto} parked on {spot}");

// it is not possible to park the same motorcycle twice
try
{
    _ = parkingLot.Park(moto);
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"Could not park motorcycle: {ex.Message}");
}

// Motorcycle can be parked on car spot
var anotherSpot = parkingLot.Park(anotherMoto);
Console.WriteLine($"{anotherMoto} parked on {anotherSpot}");

var car = new Car();
// It is not possible to park a vehicle when parking lot is full
try
{
    _ = parkingLot.Park(car);
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"Could not park car: {ex.Message}");
}

// You can fetch the parking spot by vehicle
var parkedOn = parkingLot.GetParkingVehicle(anotherSpot);
Console.WriteLine($"Found vehicle {parkedOn} parked on spot");

// You can unpark the vehicle
parkingLot.UnPark(parkedOn);
Console.WriteLine($"Unparked vehicle {parkedOn}");

// There's one spot free now
var freedSpot = parkingLot.Park(car);
Console.WriteLine($"Parked {car} on {freedSpot}");

// Car cannot park on motorcycle parking spot
parkingLot.UnPark(moto);
var anotherCar = new Car();
try
{
    _ = parkingLot.Park(anotherCar);
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"Could not park car: {ex.Message}");
}

// It is easy to extend functionality
ParkingLevel[] newLevels = [
    new(
        [
            new ParkingRow
            (
                [
                    new TruckSpot(),
                    new TruckSpot(),
                    new CarParkingSpot(),
                ]
            )
        ]
    )
];
ParkingStorage newStorage = new();
ParkingSpotSelector newSelector = new([
    new MotorcycleParkingLotValidator(),
    new CarParkingLotValidator(),
    new TruckValidator(),
    new FreeParkingSpotValidator(newStorage),
]);
ParkingLot.ParkingLot newParkingLot = new(newLevels, newStorage, newSelector);

Truck truck = new();
var truckSpot = newParkingLot.Park(truck);
Console.WriteLine($"{truck} parked at {truckSpot}");

var carSpot = newParkingLot.Park(moto);
Console.WriteLine($"{moto} parked at {carSpot}");

try
{
    _ = parkingLot.Park(car);
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"Could not park car: {ex.Message}");
}


class Truck : IVehicle {}
class TruckSpot : IParkingSpot {}
class TruckValidator : IVehicleTypeParkingSpotValidator
{
    public Type VehicleType => typeof(Truck);

    public bool CanPark(IVehicle vehicle, IParkingSpot spot)
    {
        IParkingSpotValidator.ThrowIfInvalidType<Truck, TruckValidator>(vehicle);
        return spot is TruckSpot;
    }
}