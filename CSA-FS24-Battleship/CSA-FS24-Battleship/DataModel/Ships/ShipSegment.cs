using System.Runtime.InteropServices.JavaScript;

namespace CSA_FS24_Battleship.DataModel.Ships;

public class ShipSegment
{
    public int X { get; set; }
    public int Y { get; set; }
    public bool HasBeenHit { get; set; } = false;

    public ShipSegment(int x, int y)
    {
        X = x;
        Y = y;
    }

    public override string ToString()
    {
        return ("Position --> X: " + X + " & Y: " + Y + " & HasBeenHit: " + HasBeenHit.ToString());
    }
}