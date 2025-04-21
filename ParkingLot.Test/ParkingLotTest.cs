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

        ParkingLot parkingLot = new(levels);
        Car vehicle = new();

        var actual = parkingLot.Park(vehicle);

        Assert.Equal(actual, spot);
    }

    [Fact]
    public void ParkingLot_Park_ThrowsWhenThereAreNoParkingSpots()
    {
        ParkingLevel[] levels = [new([new ParkingRow([])])];

        ParkingLot parkingLot = new(levels);
        Car vehicle = new();

        Assert.Throws<InvalidOperationException>(() => parkingLot.Park(vehicle));
    }

    [Fact]
    public void ParkingLot_Park_ThrowsWhenAllSpotsAreTaken()
    {
        ParkingLevel[] levels = [new([new ParkingRow([
            new CarParkingSpot(), new CarParkingSpot()
        ])])];

        ParkingLot parkingLot = new(levels);

        _ = parkingLot.Park(new Car());
        _ = parkingLot.Park(new Car());

        Assert.Throws<InvalidOperationException>(() => parkingLot.Park(new Car()));
    }
}