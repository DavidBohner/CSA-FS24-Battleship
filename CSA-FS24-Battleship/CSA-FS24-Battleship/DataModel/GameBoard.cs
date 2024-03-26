using CSA_FS24_Battleship.DataModel.Ships;

namespace CSA_FS24_Battleship.DataModel;
public class GameBoard
{
    public readonly int Size;
    public readonly List<Ship> Ships;
    public BoardField[,] Fields; // 2D-Array für die Spielfelder

    public GameBoard(int size)
    {
        Size = size;
        
        // Generate Ships randomly
        Ships = new();
        
        GenerateFields();
    }

    public bool Target(int x, int y)
    {
        throw new NotImplementedException();
    }

    private void GenerateFields()
    {
        //creates a 2d-Array for the gameboardfields
        Fields = new BoardField[Size, Size];
        
        //Initialize each field with a new BoardField-Object
        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                Fields[i, j] = new BoardField();
            }
        }
    }
}