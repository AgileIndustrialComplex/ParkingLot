using Moq;
using ParkingLot.Interfaces;
using ParkingLot.Models;

namespace ParkingLot.Test;

public class MotorcycleParkingLotValidatorTest
{
    [Fact]
    public void MotorcycleParkingLotValidator_VehicleType_IsMotorcycle()
    {
        MotorcycleParkingLotValidator validator = new();
        Assert.Equal(typeof(Motorcycle), validator.VehicleType);
    }

    [Fact]
    public void MotorcycleParkingLotValidator_CanPark_ThrowsWhenUsedWithCar()
    {
        MotorcycleParkingLotValidator validator = new();
        var spot = new MotorcycleParkingSpot();
        var car = new Car();

        Assert.Throws<InvalidOperationException>(() => validator.CanPark(car, spot));
    }

    [Fact]
    public void MotorcycleParkingLotValidator_CanPark_ReturnsTrueForCarParkingSpot()
    {
        MotorcycleParkingLotValidator validator = new();
        var spot = new CarParkingSpot();
        var moto = new Motorcycle();

        var actual = validator.CanPark(moto, spot);

        Assert.True(actual);
    }

    [Fact]
    public void MotorcycleParkingLotValidator_CanPark_ReturnsTrueForMotorcycleParkingSpot()
    {
        MotorcycleParkingLotValidator validator = new();
        var spot = new CarParkingSpot();
        var car = new Motorcycle();

        var actual = validator.CanPark(car, spot);

        Assert.True(actual);
    }

    [Fact]
    public void MotorcycleParkingLotValidator_CanPark_ThrowsForInvalidType()
    {
        MotorcycleParkingLotValidator validator = new();
        var spot = new Mock<IParkingSpot>().Object;
        var vehicle = new Mock<IVehicle>().Object;

        Assert.Throws<InvalidOperationException>(() => validator.CanPark(vehicle, spot));
    }
}