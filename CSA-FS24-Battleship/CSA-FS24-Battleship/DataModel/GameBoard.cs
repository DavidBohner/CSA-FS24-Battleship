using CSA_FS24_Battleship.DataModel.Ships;

namespace CSA_FS24_Battleship.DataModel;
public class GameBoard
{
    public readonly int Size;
    public List<Ship> Ships;
    public BoardField[,] Fields; // 2D-Array für die Spielfelder

    public GameBoard(int size)
    {
        Size = size;
        GenerateFields();
        CreateShips();
    }
   

    public bool Target(int x, int y)
    {
        if (x < 0 || x >= Size || y < 0 || y >= Size)
        {
            // Check that the coordinates are within the playing field
            Console.WriteLine("Invalid coordinates!");
            throw new Exception("Target out of Game-board.");
        }

        BoardField targetField = Fields[x, y];
        targetField.WasShot = true; // Set the What attribute to true for the BoardField at the given coordinates
        
        
        foreach (var segment in from ship in Ships from segment in ship.Segments where segment.X == x && segment.Y == y select segment)
        {
            segment.HasBeenHit = true;
        }

        return IsGameOver();
    }

    public bool IsGameOver()
    {
        foreach (var ship in Ships)
        {
            if (!ship.HasBeenSunk())
            {
                return false;
            }
        }

        return true;
    }

    public bool IsValid()
    {
        // Bounds check
        foreach (var ship in Ships)
        {
            if (!ship.IsValid(Size))
            {
                Console.WriteLine($"{ship.GetType()} failed bounds check.");
                return false;
            }
        }
        
        // Check for overlapping segments
        foreach (var ship in Ships)
        {
            foreach (var segment in ship.Segments)
            {
                var overlappingSegment = Ships
                    .Where(otherShip => ship != otherShip)
                    .SelectMany(otherShip => otherShip.Segments)
                    .FirstOrDefault(otherSegment => otherSegment.X == segment.X && otherSegment.Y == segment.Y);

                if (overlappingSegment != null)
                {
                    Console.WriteLine($"Segment overlap at {segment.X}, {segment.Y} between {ship.GetType()} and {overlappingSegment.GetType()}");
                    return false;
                }
            }
        }
        Console.WriteLine("Validity Check succeeded!");
        return true;
    }

    private void CreateShips()
    {
        do
        {
            Ships = new();
            Ships.Add(new Frigate(GenerateRandomCoordinate(), GenerateRandomCoordinate(), GenerateRandomAlignment()));
            Ships.Add(new Destroyer(GenerateRandomCoordinate(), GenerateRandomCoordinate(), GenerateRandomAlignment()));
            Ships.Add(new Cruiser(GenerateRandomCoordinate(), GenerateRandomCoordinate(), GenerateRandomAlignment()));
        } while (!IsValid());
    }
    
    private int GenerateRandomCoordinate()
    {
        return new Random().Next(Size);
    }
    
    private bool GenerateRandomAlignment()
    {
        return new Random().Next(2) == 0;
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