using Moq;
using ParkingLot.Interfaces;
using ParkingLot.Models;

namespace ParkingLot.Test;

public class ParkingLotTest
{
    private readonly Mock<IParkingSpotValidator> validatorMock;
    private readonly Mock<IParkingStorage> storageMock;

    public ParkingLotTest()
    {
        validatorMock = new Mock<IParkingSpotValidator>();
        validatorMock.Setup(x => x.VehicleType).Returns(typeof(Car));
        validatorMock.Setup(x => x.CanPark(It.IsAny<IVehicle>(), It.IsAny<IParkingSpot>())).Returns(true);

        storageMock = new Mock<IParkingStorage>();
    }

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



        ParkingLot parkingLot = new(levels, [validatorMock.Object], storageMock.Object);
        Car vehicle = new();

        var actual = parkingLot.Park(vehicle);

        Assert.Equal(actual, spot);
    }

    [Fact]
    public void ParkingLot_Park_ThrowsWhenThereAreNoParkingSpots()
    {
        ParkingLevel[] levels = [new([new ParkingRow([])])];

        ParkingLot parkingLot = new(levels, [validatorMock.Object], storageMock.Object);
        Car vehicle = new();

        Assert.Throws<InvalidOperationException>(() => parkingLot.Park(vehicle));
    }

    [Fact]
    public void ParkingLot_Park_ThrowsWhenAllSpotsAreTaken()
    {
        ParkingLevel[] levels = [new([new ParkingRow([
            new CarParkingSpot(), new CarParkingSpot()
        ])])];

        storageMock.Setup(x => x.Has(It.IsAny<IParkingSpot>())).Returns(true);

        ParkingLot parkingLot = new(levels, [validatorMock.Object], storageMock.Object);

        Assert.Throws<InvalidOperationException>(() => parkingLot.Park(new Car()));
    }

    [Fact]
    public void ParkingLot_Park_CarsCannotParkOnMotorcycleSpots()
    {
        ParkingLevel[] levels = [new([new ParkingRow([
            new MotorcycleParkingSpot()
        ])])];

        validatorMock.Setup(x => x.CanPark(It.IsAny<IVehicle>(), It.IsAny<IParkingSpot>())).Returns(false);

        ParkingLot parkingLot = new(levels, [validatorMock.Object], storageMock.Object);

        Assert.Throws<InvalidOperationException>(() => parkingLot.Park(new Car()));
    }

    [Fact]
    public void ParkingLot_Park_MotorcyclesCanParkOnCarSpots()
    {
        var spot = new CarParkingSpot();
        ParkingLevel[] levels = [new([new ParkingRow([spot])])];

        validatorMock.Setup(x => x.VehicleType).Returns(typeof(Motorcycle));

        ParkingLot parkingLot = new(levels, [validatorMock.Object], storageMock.Object);

        var actual = parkingLot.Park(new Motorcycle());

        Assert.Equal(spot, actual);
    }

    [Fact]
    public void ParkingLot_GetParkingVehicle_ReturnsParkedVehicleForSpot()
    {
        CarParkingSpot spot = new();
        var expected = new Car();
        storageMock.Setup(x => x.Get(spot)).Returns(expected);

        ParkingLot parkingLot = new([], [validatorMock.Object], storageMock.Object);

        var actual = parkingLot.GetParkingVehicle(spot);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ParkingLot_GetValidator_ThrowsWhenNoValidatorFound()
    {
        ParkingLevel[] levels = [new([new ParkingRow([new CarParkingSpot()])])];

        ParkingLot parkingLot = new(levels, [validatorMock.Object], storageMock.Object);
        var car = new Car();

        Assert.Throws<InvalidOperationException>(() => parkingLot.GetValidator(new Motorcycle()));
    }
}