using System.Collections;
using ParkingLot.Interfaces;

namespace ParkingLot.Models;

public class ParkingLevel(IEnumerable<IParkingRow> rows) : IParkingLevel
{
    private readonly IEnumerable<IParkingRow> rows = rows;

    public IEnumerator<IParkingRow> GetEnumerator()
    {
        foreach (var row in rows)
        {
            yield return row;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}