namespace CSA_FS24_Battleship.DataModel.Ships;

public abstract class Ship
{
    public List<ShipSegment> Segments { get; set; } = new();
    public int Length => Segments.Count();
    public bool IsSunk => Segments.All(segment => segment.HasBeenHit);
    
    public Ship() { }
    public Ship(int x, int y, bool isVertical) { }

    public bool HasBeenSunk()
    {
        foreach (var segment in Segments)
        {
            if (!segment.HasBeenHit)
            {
                return false;
            }
        }

        return true;
    }
    
    public bool IsValid(int size)
    {
        // Check that none of its segments are out-of-bounds
        foreach (var segment in Segments)
        {
            if (!segment.IsValid(size))
            {
                return false;
            }
        }
        return true;
    }
}