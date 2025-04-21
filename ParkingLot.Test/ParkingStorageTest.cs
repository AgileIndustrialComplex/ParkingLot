using System.Data.Common;
using Microsoft.VisualBasic;
using ParkingLot.Models;

namespace ParkingLot.Test;

public class ParkingStorageTest
{
    [Fact]
    public void ParkingStroageTest_Create_Get_MakesANewEntry()
    {
        ParkingStorage storage = new();
        CarParkingSpot spot = new();
        Car car = new();

        storage.Create(car, spot);

        var actualCar = storage.Get(spot);
        var actualSpot = storage.Get(car);

        Assert.Equal(car, actualCar);
        Assert.Equal(spot, actualSpot);
    }

    [Fact]
    public void ParkingStroageTest_Create_ThrowsForExistingVehicle()
    {
        ParkingStorage storage = new();
        CarParkingSpot spot = new();
        Car car = new();

        storage.Create(car, spot);

        Assert.Throws<InvalidOperationException>(() => storage.Create(car, new CarParkingSpot()));
    }

    [Fact]
    public void ParkingStroageTest_Create_ThrowsForExistingParkingSpot()
    {
        ParkingStorage storage = new();
        CarParkingSpot spot = new();
        Car car = new();

        storage.Create(car, spot);

        Assert.Throws<InvalidOperationException>(() => storage.Create(new Car(), spot));
    }

    [Fact]
    public void ParkingStroageTest_Get_ThrowsForNonExistentVehicle()
    {
        ParkingStorage storage = new();

        Assert.Throws<InvalidOperationException>(() => storage.Get(new Car()));
    }

    [Fact]
    public void ParkingStroageTest_Get_ThrowsForNonExistentParkingSpot()
    {
        ParkingStorage storage = new();

        Assert.Throws<InvalidOperationException>(() => storage.Get(new CarParkingSpot()));
    }

    [Fact]
    public void ParkingStroageTest_Has_ResturnsTrueForExistsingVehicle()
    {
        ParkingStorage storage = new();
        Car car = new();

        storage.Create(car, new CarParkingSpot());
        var actual = storage.Has(car);

        Assert.True(actual);
    }

    [Fact]
    public void ParkingStroageTest_Has_ResturnsFalseForNonExistsingVehicle()
    {
        ParkingStorage storage = new();
        Car car = new();

        storage.Create(car, new CarParkingSpot());
        var actual = storage.Has(new Car());

        Assert.False(actual);
    }

    [Fact]
    public void ParkingStroageTest_Has_ResturnsTrueForExistsingParkingSpot()
    {
        ParkingStorage storage = new();
        CarParkingSpot spot = new();

        storage.Create(new Car(), spot);
        var actual = storage.Has(spot);

        Assert.True(actual);
    }

    [Fact]
    public void ParkingStroageTest_Has_ResturnsFalseForNonExistsingParkingSpot()
    {
        ParkingStorage storage = new();

        storage.Create(new Car(), new CarParkingSpot());
        var actual = storage.Has(new CarParkingSpot());

        Assert.False(actual);
    }

    [Fact]
    public void ParkingStroageTest_Create_Remove_Has_ReturnsFalseForRemovedVehicle()
    {
        ParkingStorage storage = new();

        var car = new Car();
        var spot = new CarParkingSpot();
        storage.Create(car, spot);
        storage.Remove(car);

        Assert.False(storage.Has(car));
        Assert.False(storage.Has(spot));
    }

    [Fact]
    public void ParkingStroageTest_Create_Remove_Has_ReturnsFalseForRemovedParkingSpot()
    {
        ParkingStorage storage = new();

        var car = new Car();
        var spot = new CarParkingSpot();
        storage.Create(car, spot);
        storage.Remove(spot);

        Assert.False(storage.Has(car));
        Assert.False(storage.Has(spot));
    }

    [Fact]
    public void ParkingStroageTest_Remove_ThrowsForNonExistentVehicle()
    {
        ParkingStorage storage = new();


        Assert.Throws<InvalidOperationException>(() => storage.Remove(new Car()));
    }

    [Fact]
    public void ParkingStroageTest_Remove_ThrowsForNonExistentParkingSpot()
    {
        ParkingStorage storage = new();


        Assert.Throws<InvalidOperationException>(() => storage.Remove(new CarParkingSpot()));
    }
}