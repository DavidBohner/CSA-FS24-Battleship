using CSA_FS24_Battleship.DataModel;
using CSA_FS24_Battleship.DataModel.Ships;
using CSA_FS24_Battleship.GameLogic;
using Explorer700Library;

namespace CSA_FS24_Battleship;
class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Battleship game started...");

            GameManager gm = new();
            gm.GameSession();
        }
    }
}