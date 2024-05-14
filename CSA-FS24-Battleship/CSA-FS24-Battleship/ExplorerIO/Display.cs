using System.Drawing;
using CSA_FS24_Battleship.DataModel;
using Explorer700Library;

namespace CSA_FS24_Battleship.ExplorerIO;

public class Display
{
    private readonly Explorer700 _explorer700;

    public Display(Explorer700 explorer700)
    {
        _explorer700 = explorer700;
    }

    public void DrawGameField(GameBoard board, int col, int row)
    {
        var screen = _explorer700.Display.Graphics;

        DrawBoard(screen, board, (col, row));
    }
    
    private void DrawBoard(Graphics screen, GameBoard board, (int row, int col) currentCoords)
    {
        screen.Clear(Color.Black);

        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                screen.DrawString(board.Fields[i, j] + GetPosSymbol(currentCoords, (i, j)), new Font(FontFamily.GenericSansSerif, 10), GetColor(board.Fields[i, j]), j * 10, i * 10);
            }
        }
        
        _explorer700.Display.Update();
    }

    public void DrawMessage(string message)
    {
        _explorer700.Display.Graphics.DrawString(message, new Font(FontFamily.GenericSansSerif, 10), Brushes.White, 70, 20);
        _explorer700.Display.Update();
    }
    
    private static String GetPosSymbol((int row, int col) currentPosition, (int row, int col) currentCoords)
    {
        if (currentPosition.col == currentCoords.col && currentPosition.row == currentCoords.row)
            return "I";
        else
            return "";
    }

    private Brush GetColor(BoardField boardField)
    {
        if (boardField.Ship?.HasBeenSunk() ?? false)
        {
            return Brushes.Red;
        }
        else
        {
            return Brushes.White;
        }
    }
}