using CSA_FS24_Battleship.DataModel.Ships;

namespace CSA_FS24_Battleship.DataModel;

public class BoardField
{
    public bool WasShot { get; set; } = false;
    public Ship? Ship;
    public ShipSegment? ShipSegment;

    public override string ToString()
    {
        if (ShipSegment?.HasBeenHit ?? false)
        {
            return "O";
        }
        else if (WasShot)
        {
            return "X";
        }
        else
        {
            return "-";
        }
    }
}