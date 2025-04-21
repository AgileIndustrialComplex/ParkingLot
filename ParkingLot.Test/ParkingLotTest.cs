using ParkingLot.Models;

namespace ParkingLot.Test;

public class ParkingLotTest
{
    [Fact]
    public void ParkingLot_Park_ReturnsParkingSpot()
    {
        CarParkingSpot spot = new();
        ParkingLevel[] levels =
        [
            new([
                new ParkingRow([
                    spot
                ])
            ])
        ];

        ParkingLot parkingLot = new(levels, [new CarParkingLotValidator()]);
        Car vehicle = new();

        var actual = parkingLot.Park(vehicle);

        Assert.Equal(actual, spot);
    }

    [Fact]
    public void ParkingLot_Park_ThrowsWhenThereAreNoParkingSpots()
    {
        ParkingLevel[] levels = [new([new ParkingRow([])])];

        ParkingLot parkingLot = new(levels, [new CarParkingLotValidator()]);
        Car vehicle = new();

        Assert.Throws<InvalidOperationException>(() => parkingLot.Park(vehicle));
    }

    [Fact]
    public void ParkingLot_Park_ThrowsWhenAllSpotsAreTaken()
    {
        ParkingLevel[] levels = [new([new ParkingRow([
            new CarParkingSpot(), new CarParkingSpot()
        ])])];

        ParkingLot parkingLot = new(levels, [new CarParkingLotValidator()]);

        _ = parkingLot.Park(new Car());
        _ = parkingLot.Park(new Car());

        Assert.Throws<InvalidOperationException>(() => parkingLot.Park(new Car()));
    }

    [Fact]
    public void ParkingLot_Park_CarsCannotParkOnMotorcycleSpots()
    {
        ParkingLevel[] levels = [new([new ParkingRow([
            new MotorcycleParkingSpot()
        ])])];

        ParkingLot parkingLot = new(levels, [new CarParkingLotValidator()]);

        Assert.Throws<InvalidOperationException>(() => parkingLot.Park(new Car()));
    }

    [Fact]
    public void ParkingLot_Park_MotorcyclesCanParkOnCarSpots()
    {
        var spot = new CarParkingSpot();
        ParkingLevel[] levels = [new([new ParkingRow([spot])])];

        ParkingLot parkingLot = new(levels, [new MotorcycleParkingLotValidator()]);

        var actual = parkingLot.Park(new Motorcycle());

        Assert.Equal(spot, actual);
    }

    [Fact]
    public void ParkingLot_GetParkingVehicle_ReturnsParkedVehicleForSpot()
    {
        ParkingLevel[] levels = [new([new ParkingRow([new CarParkingSpot()])])];

        ParkingLot parkingLot = new(levels, [new CarParkingLotValidator()]);

        var expected = new Car();
        var spot = parkingLot.Park(expected);

        var actual = parkingLot.GetParkingVehicle(spot);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ParkingLot_GetParkingVehicle_ThrowsForParkingSpotNotInParkingLot()
    {
        ParkingLevel[] levels = [new([new ParkingRow([new CarParkingSpot()])])];

        ParkingLot parkingLot = new(levels, [new CarParkingLotValidator()]);

        var car = new Car();
        var spot = parkingLot.Park(car);

        Assert.Throws<InvalidOperationException>(() => parkingLot.GetParkingVehicle(new CarParkingSpot()));
    }

    [Fact]
    public void ParkingLot_GetParkingVehicle_ThrowsForFreeParkingSpot()
    {
        CarParkingSpot spot = new();
        ParkingLevel[] levels = [new([new ParkingRow([spot])])];

        ParkingLot parkingLot = new(levels, [new CarParkingLotValidator()]);

        Assert.Throws<InvalidOperationException>(() => parkingLot.GetParkingVehicle(spot));
    }

    [Fact]
    public void ParkingLot_UnPark_ThrowsForVehicleNotInParkingLot()
    {
        ParkingLevel[] levels = [new([new ParkingRow([new CarParkingSpot()])])];

        ParkingLot parkingLot = new(levels, [new CarParkingLotValidator()]);

        Assert.Throws<InvalidOperationException>(() => parkingLot.UnPark(new Car()));
    }

    [Fact]
    public void ParkingLot_UnPark_RemovesCarFromParkingLot()
    {
        ParkingLevel[] levels = [new([new ParkingRow([new CarParkingSpot()])])];

        ParkingLot parkingLot = new(levels, [new CarParkingLotValidator()]);
        var car = new Car();
        var spot = parkingLot.Park(car);
        parkingLot.UnPark(car);

        Assert.Throws<InvalidOperationException>(() => parkingLot.GetParkingVehicle(spot));
    }

    [Fact]
    public void ParkingLot_GetValidator_ThrowsWhenNoValidatorFound()
    {
        ParkingLevel[] levels = [new([new ParkingRow([new CarParkingSpot()])])];

        ParkingLot parkingLot = new(levels, [new CarParkingLotValidator()]);
        var car = new Car();

        Assert.Throws<InvalidOperationException>(() => parkingLot.GetValidator(new Motorcycle()));
    }
}