namespace CSA_FS24_Battleship.DataModel.Ships;

public class Destroyer : Ship
{
    public Destroyer(int x, int y, bool isVertical) : base(x, y, isVertical)
    {
        int x2 = isVertical ? x : x + 1;
        int y2 = isVertical ? y + 1 : y;
        Segments = new List<ShipSegment>()
        {
            new ShipSegment(x, y),
            new ShipSegment(x2, y2),
        };
    }
}