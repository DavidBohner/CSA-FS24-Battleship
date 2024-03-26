using CSA_FS24_Battleship.DataModel.Ships;

namespace CSA_FS24_Battleship.DataModel;
public class GameBoard
{
    public readonly int Size;
    public readonly List<Ship> Ships;
    

    public GameBoard(int size)
    {
        Size = size;
        
        // Generate Ships
        Ships = new();
    }

    public bool Target(int x, int y)
    {
        throw new NotImplementedException();
    }
}