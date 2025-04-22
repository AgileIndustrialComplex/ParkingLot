using Moq;
using ParkingLot.Interfaces;
using ParkingLot.Models;

namespace ParkingLot.Test;

public class ParkingLotTest
{
    private readonly Mock<IParkingSpotSelector> selectorMock;
    private readonly Mock<IParkingStorage> storageMock;
    private readonly ParkingLot parkingLot;

    public ParkingLotTest()
    {
        selectorMock = new Mock<IParkingSpotSelector>();
        storageMock = new Mock<IParkingStorage>();
        parkingLot = new ParkingLot([], storageMock.Object, selectorMock.Object);
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

        var actual = parkingLot.Park(vehicle);

        Assert.Equal(actual, spot);
        storageMock.Verify(x => x.Create(vehicle, spot), Times.Once);
    }

    [Fact]
    public void ParkingLot_GetParkingVehicle_ReturnsParkedVehicleForSpot()
    {
        var spot = new Mock<IParkingSpot>().Object;
        var vehicle = new Mock<IVehicle>().Object;
        storageMock.Setup(x => x.Get(spot)).Returns(vehicle);

        var actual = parkingLot.GetParkingVehicle(spot);
        Assert.Equal(vehicle, actual);
    }

    [Fact]
    public void ParkingLot_UnPark_RemovesVehicleFromStorage()
    {
        var spot = new Mock<IParkingSpot>().Object;
        var vehicle = new Mock<IVehicle>().Object;

        parkingLot.UnPark(vehicle);
        storageMock.Verify(x => x.Remove(vehicle), Times.Once);
    }
}