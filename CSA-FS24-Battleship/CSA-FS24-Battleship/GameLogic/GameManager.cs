using CSA_FS24_Battleship.DataModel;
using Explorer700Library;
using Display = CSA_FS24_Battleship.ExplorerIO.Display;

namespace CSA_FS24_Battleship.GameLogic;

public class GameManager
{
    private static readonly int GameSize = 6;
    private GameBoard ComputerBoard = new(GameSize);
    private GameBoard PlayerBoard = new(GameSize);

    private static Explorer700 _explorer700;
    private Display Display;

    public GameManager()
    {
        _explorer700 = new();
        Display = new(_explorer700);
    }
    
    
    // cursor start
    int col = 0;
    int row = 0;
    

    public Winner GameTurn()
    {
        // Player turn
        bool madeMove = false;
        while (!madeMove)
        {
            madeMove = MakeMove(ComputerBoard);
        }

        bool playerWon = ComputerBoard.Target(col, row);
        Display.DrawGameField(ComputerBoard, col, row);

        if (playerWon)
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

        var shotResult = PlayerBoard.Target(compX, compY);

        var compResString = $"Comp shot at {compX}, {compY}";
        if (PlayerBoard.Fields[compX, compY].Ship != null)
        {
            compResString += $" and hit {PlayerBoard.Fields[compX, compY].Ship!.GetType()}";
            if (PlayerBoard.Fields[compX, compY].Ship!.IsSunk)
            {
                compResString += ", sinking it.";
            }
        }
        Console.WriteLine(compResString);
        
        if (shotResult)
        {
            return Winner.Computer;
        }

        return Winner.Undecided;
    }
    
    public void GameSession()
    {
        Display.DrawGameField(ComputerBoard, col, row);
        while (true)
        {
            var result = GameTurn();
            if (result != Winner.Undecided)
            {
                Console.WriteLine($"The winner is {result}");
                Display.DrawMessage(result == Winner.Player ? "Win!" : "Lose");
                break;
            }
        }
    }
    
    private bool MakeMove(GameBoard board)
    {
        bool moveMade = false;
        bool changed = false;
        Keys keys = _explorer700.Joystick.Keys;
        string key = "no key pressed";
        
        switch (keys)
        {
            case Keys.Left when row > 0:
                row--;
                changed = true;
                key = "Left";
                break;
            case Keys.Right when row < 6:
                row++;
                changed = true;
                key = "Right";
                break;
            case Keys.Up when col > 0:
                col--;
                changed = true;
                key = "Up";
                break;
            case Keys.Down when col < 6:
                col++;
                changed = true;
                key = "Down";
                break;
            case Keys.Center:// when CheckForValidMove(row, col, board, currentPlayer):
                // Valid move, place sign on board and switch to player
                // ComputerBoard.Target(col, row);
                changed = moveMade = true;
                key = "Center";
                break;
        }
    
        // Update the display
        if (changed)
        {
            Display.DrawGameField(board, col, row);
            
            string dataFile = "logs/data.txt";
            string content = $"Joystick: {key}: {DateTime.Now:dd.MM.yy hh:mm:ss}";
            using (StreamWriter sw = File.AppendText(dataFile))
            {
                sw.WriteLine(content);
            }	
            
            //try to prevent double input
            Thread.Sleep(200);
        }
    
        return moveMade;
    }
}
