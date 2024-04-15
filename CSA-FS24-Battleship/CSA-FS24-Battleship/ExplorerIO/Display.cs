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
            screen.DrawString(board.Fields[i, 0] + GetPosSymbol(currentCoords, (i, 0)), new Font(FontFamily.GenericSansSerif, 10), GetColor(board.Fields[i, 0]), 0, i * 10);
            screen.DrawString(board.Fields[i, 1] + GetPosSymbol(currentCoords, (i, 1)), new Font(FontFamily.GenericSansSerif, 10), GetColor(board.Fields[i, 1]), 10, i * 10);
            screen.DrawString(board.Fields[i, 2] + GetPosSymbol(currentCoords, (i, 2)), new Font(FontFamily.GenericSansSerif, 10), GetColor(board.Fields[i, 2]), 20, i * 10);
            screen.DrawString(board.Fields[i, 3] + GetPosSymbol(currentCoords, (i, 3)), new Font(FontFamily.GenericSansSerif, 10), GetColor(board.Fields[i, 3]), 30, i * 10);
            screen.DrawString(board.Fields[i, 4] + GetPosSymbol(currentCoords, (i, 4)), new Font(FontFamily.GenericSansSerif, 10), GetColor(board.Fields[i, 4]), 40, i * 10);
            screen.DrawString(board.Fields[i, 5] + GetPosSymbol(currentCoords, (i, 5)), new Font(FontFamily.GenericSansSerif, 10), GetColor(board.Fields[i, 5]), 50, i * 10);
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