using ParkingLot.Models;

namespace ParkingLot.Test;

public class ParkingRowTest
{
    [Fact]
    public void ParkingRow_Constructor_CreatesEmptyCollection()
    {
        var row = new ParkingRow([]);

        Assert.Empty(row);
    }
}