using ParkingLot.Models;

namespace ParkingLot.Test;

public class CarParkingLotValidatorTest
{
    [Fact]
    public void CarParkingLotValidator_VehicleType_IsCar()
    {
        CarParkingLotValidator validator = new();
        Assert.Equal(typeof(Car), validator.VehicleType);
    }

    [Fact]
    public void CarParkingLotValidator_CanPark_ThrowsWhenUsedWithMotorcycle()
    {
        CarParkingLotValidator validator = new();
        var spot = new CarParkingSpot();
        var moto = new Motorcycle();

        Assert.Throws<InvalidOperationException>(() => validator.CanPark(moto, spot));
    }

    [Fact]
    public void CarParkingLotValidator_CanPark_ReturnsTrueForCar()
    {
        CarParkingLotValidator validator = new();
        var spot = new CarParkingSpot();
        var car = new Car();

        var actual = validator.CanPark(car, spot);

        Assert.True(actual);
    }

    [Fact]
    public void CarParkingLotValidator_CanPark_ReturnsFalseForMotorcycleParkingSpot()
    {
        CarParkingLotValidator validator = new();
        var spot = new MotorcycleParkingSpot();
        var car = new Car();

        var actual = validator.CanPark(car, spot);

        Assert.False(actual);
    }
}