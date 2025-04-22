using System.ComponentModel.DataAnnotations;
using Moq;
using ParkingLot.Interfaces;

namespace ParkingLot.Test;

public class ParkingSpotSelectorTest
{
    private readonly Mock<IParkingSpotValidator> validatorMock;
    private readonly ParkingSpotSelector selector;
    public ParkingSpotSelectorTest()
    {
        validatorMock = new Mock<IParkingSpotValidator>();
        selector = new ParkingSpotSelector([validatorMock.Object]);
    }

    [Fact]
    public void ParkingSpotSelector_GetSpot_ReturnsSpotForVehicle()
    {
        var vehicle = new Mock<IVehicle>().Object;
        var spot = new Mock<IParkingSpot>().Object;

        validatorMock.Setup(x => x.CanPark(vehicle, spot)).Returns(true);

        var actual = selector.GetSpot(vehicle, [spot]);

        Assert.Equal(spot, actual);
    }

    [Fact]
    public void ParkingSpotSelector_GetSpot_ReturnsFirstFreeSpot()
    {
        var vehicle = new Mock<IVehicle>().Object;
        var freeSpot = new Mock<IParkingSpot>().Object;
        var takenSpot = new Mock<IParkingSpot>().Object;

        validatorMock.Setup(x => x.CanPark(vehicle, takenSpot)).Returns(false);
        validatorMock.Setup(x => x.CanPark(vehicle, freeSpot)).Returns(true);

        var actual = selector.GetSpot(vehicle, [takenSpot, freeSpot]);

        Assert.Equal(freeSpot, actual);
    }

    [Fact]
    public void ParkingSpotSelector_GetSpot_ThrowsForNoSpots()
    {
        var vehicle = new Mock<IVehicle>().Object;

        Assert.Throws<InvalidOperationException>(() => selector.GetSpot(vehicle, []));
    }

    [Fact]
    public void ParkingSpotSelector_GetSpot_ThrowsForNoFreeSpots()
    {
        var vehicle = new Mock<IVehicle>().Object;
        var taken1 = new Mock<IParkingSpot>().Object;
        var taken0 = new Mock<IParkingSpot>().Object;

        validatorMock.Setup(x => x.CanPark(vehicle, taken0)).Returns(false);
        validatorMock.Setup(x => x.CanPark(vehicle, taken1)).Returns(false);

        Assert.Throws<InvalidOperationException>(() => selector.GetSpot(vehicle, [taken0, taken1]));
    }
}