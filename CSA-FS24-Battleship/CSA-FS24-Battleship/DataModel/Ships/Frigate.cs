namespace CSA_FS24_Battleship.DataModel.Ships;

public class Frigate : Ship
{
    public Frigate(int x, int y, bool isVertical)
    {
        Segments = new List<ShipSegment>()
        {
            new ShipSegment(x, y)
        };
    }
}