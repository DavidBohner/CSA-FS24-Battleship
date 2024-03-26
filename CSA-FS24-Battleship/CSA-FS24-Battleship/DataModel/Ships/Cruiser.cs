namespace CSA_FS24_Battleship.DataModel.Ships;

public class Cruiser : Ship
{
    public Cruiser(int x, int y, bool isVertical)
    {
        int x2 = isVertical ? x : x + 1;
        int x3 = isVertical ? x : x + 2;
        int y2 = isVertical ? y + 1 : y;
        int y3 = isVertical ? y + 2 : y;
        Segments = new List<ShipSegment>()
        {
            new ShipSegment(x, y),
            new ShipSegment(x2, y2),
            new ShipSegment(x3, y3),
        };
    }
}