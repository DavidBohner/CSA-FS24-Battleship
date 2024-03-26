using CSA_FS24_Battleship.DataModel;
using CSA_FS24_Battleship.DataModel.Ships;
using Explorer700Library;

namespace CSA_FS24_Battleship;
class Program
{
    private static Explorer700 _explorer700 = new();
    static void Main(string[] args)
    {
        Console.WriteLine("Battleship game started...");
        
        //check if gameboard crates fields
        GameBoard gb = new GameBoard(6);

        int i = 1;
        foreach (BoardField field in gb.Fields)
        {
            Console.WriteLine(i);
            i++;
        }
        
        //check if ships are random
        int ii = 1;
        foreach (Ship ship in gb.Ships)
        {
            Console.WriteLine("Ship " + ii + "\nSegments: ");
            foreach (ShipSegment segment in ship.Segments)
            {
                Console.WriteLine(segment.ToString());
            }
            ii++;
        }

        //shoot on every field
        for (int iii = 0; iii < gb.Size; iii++)
        {
            for (int j = 0; j < gb.Size; j++)
            {
                Console.WriteLine("Shot on: " + iii + "/" + j + " --> " + gb.Target(iii, j).ToString());
            }
        }
    }
}