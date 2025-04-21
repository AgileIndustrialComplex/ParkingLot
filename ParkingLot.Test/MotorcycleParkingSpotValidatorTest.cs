using ParkingLot.Models;

namespace ParkingLot.Test;

public class MotorcycleParkingLotValidatorTest
{
    [Fact]
    public void MotorcycleParkingLotValidator_VehicleType_IsCar()
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
    public void MotorcycleParkingLotValidator_CanPark_ReturnsTrueForCar()
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
}