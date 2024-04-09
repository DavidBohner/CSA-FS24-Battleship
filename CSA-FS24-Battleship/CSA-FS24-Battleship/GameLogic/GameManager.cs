using CSA_FS24_Battleship.DataModel;
using CSA_FS24_Battleship.ExplorerIO;

namespace CSA_FS24_Battleship.GameLogic;

public class GameManager
{
    private static readonly int GameSize = 6;
    private GameBoard ComputerBoard = new(GameSize);
    private GameBoard PlayerBoard = new(GameSize);

    private Display Display = new();

    public Winner GameTurn()
    {
        // Player turn
        if (ComputerBoard.Target(1, 1))
        {
            return Winner.Player;
        }
        // Computer turn
        int compX, compY;
        do
        {
            compX = new Random().Next(GameSize);
            compY = new Random().Next(GameSize);
        } while (PlayerBoard.Fields[compX, compY].WasShot);
        Console.WriteLine($"Comp shot at {compX}, {compY}.");
        if (PlayerBoard.Target(compX, compY))
        {
            return Winner.Computer;
        }

        return Winner.Undecided;
    }
    
    public void GameSession()
    {
        //tmp
        Display.DrawGameField();
        while (true)
        {
            var result = GameTurn();
            if (result != Winner.Undecided)
            {
                Console.WriteLine($"The winner is {result}");
                return;
            }
        }
    }
}
