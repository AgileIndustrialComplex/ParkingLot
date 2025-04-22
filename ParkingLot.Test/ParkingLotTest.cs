using Moq;
using ParkingLot.Interfaces;
using ParkingLot.Models;

namespace ParkingLot.Test;

public class ParkingLotTest
{
    private readonly Mock<IParkingSpotValidator> validatorMock;
    private readonly Mock<IParkingSpotSelector> selectorMock;
    private readonly Mock<IParkingStorage> storageMock;

    public ParkingLotTest()
    {
        validatorMock = new Mock<IParkingSpotValidator>();
        validatorMock.Setup(x => x.CanPark(It.IsAny<IVehicle>(), It.IsAny<IParkingSpot>())).Returns(true);

        selectorMock = new Mock<IParkingSpotSelector>();

        storageMock = new Mock<IParkingStorage>();
    }

    [Fact]
    public void ParkingLot_Park_ReturnsParkingSpot()
    {
        var spot = new Mock<IParkingSpot>().Object;
        var vehicle = new Mock<IVehicle>().Object;
        selectorMock
            .Setup(x => x.GetSpot(
                It.IsAny<IVehicle>(),
                It.IsAny<IEnumerable<IParkingSpot>>()))
            .Returns(spot);


        ParkingLot parkingLot = new([], storageMock.Object, selectorMock.Object);

        var actual = parkingLot.Park(vehicle);

        Assert.Equal(actual, spot);
    }

    [Fact]
    public void ParkingLot_GetParkingVehicle_ReturnsParkedVehicleForSpot()
    {
        CarParkingSpot spot = new();
        var expected = new Car();
        storageMock.Setup(x => x.Get(spot)).Returns(expected);

        ParkingLot parkingLot = new([], storageMock.Object, selectorMock.Object);

        var actual = parkingLot.GetParkingVehicle(spot);
        Assert.Equal(expected, actual);
    }
}