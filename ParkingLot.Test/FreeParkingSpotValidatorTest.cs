using Moq;
using ParkingLot.Interfaces;

namespace ParkingLot.Test;

public class FreeParkingSpotValidatorTest
{
    private readonly Mock<IParkingStorage> storageMock;
    private readonly FreeParkingSpotValidator validator;

    public FreeParkingSpotValidatorTest()
    {
        storageMock = new Mock<IParkingStorage>();
        validator = new FreeParkingSpotValidator(storageMock.Object);
    }

    [Fact]
    public void FreeParkingSpotValidator_CanPark_ReturnsTrueForFreeSpot()
    {
        var vehicle = new Mock<IVehicle>().Object;
        var spot = new Mock<IParkingSpot>().Object;
        storageMock.Setup(x => x.Has(spot)).Returns(false);

        var actual = validator.CanPark(vehicle, spot);

        Assert.True(actual);
    }

    [Fact]
    public void FreeParkingSpotValidator_CanPark_ReturnsFalseForTakenSpot()
    {
        var vehicle = new Mock<IVehicle>().Object;
        var spot = new Mock<IParkingSpot>().Object;
        storageMock.Setup(x => x.Has(spot)).Returns(true);

        var actual = validator.CanPark(vehicle, spot);

        Assert.False(actual);
    }
}