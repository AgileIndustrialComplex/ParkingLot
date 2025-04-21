using System.Collections;
using ParkingLot.Interfaces;

namespace ParkingLot.Models;

public class ParkingRow(IEnumerable<IParkingSpot> spots) : IParkingRow
{
    private readonly IEnumerable<IParkingSpot> spots = spots;

    public IEnumerator<IParkingSpot> GetEnumerator()
    {
        foreach (var spot in spots)
        {
            yield return spot;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}