namespace CSA_FS24_Battleship.DataModel.Ships;

public abstract class Ship
{
    public List<ShipSegment> Segments { get; set; } = new();
    public int Length => Segments.Count();
    public bool IsSunk => Segments.All(segment => segment.HasBeenHit);
    
    public Ship() { }
    public Ship(int x, int y, bool isVertical) { }
    
    public bool IsValid()
    {
        // TODO: Data validity checks
        /*
         * 1. All of its segments are adjoining
         * 2. None of its segments are overlapping
         * 3. None of its segments are out-of-bounds
         */
        return true;
    }
}