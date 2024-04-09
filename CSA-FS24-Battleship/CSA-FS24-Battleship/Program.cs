using CSA_FS24_Battleship.DataModel;
using CSA_FS24_Battleship.DataModel.Ships;
using CSA_FS24_Battleship.GameLogic;
using Explorer700Library;

namespace CSA_FS24_Battleship;
class Program
{
    private static Explorer700 _explorer700 = new();
    static void Main(string[] args)
    {
        Console.WriteLine("Battleship game started...");
        
        //check if gameboard creates fields
        // GameBoard gb = new GameBoard(6);
        //
        // //check if ships are random
        // int ii = 1;
        // foreach (Ship ship in gb.Ships)
        // {
        //     Console.WriteLine("Ship " + ii + "\nSegments: ");
        //     foreach (ShipSegment segment in ship.Segments)
        //     {
        //         Console.WriteLine(segment.ToString());
        //     }
        //     ii++;
        // }

        GameManager gm = new();
        gm.GameSession();
    }
}