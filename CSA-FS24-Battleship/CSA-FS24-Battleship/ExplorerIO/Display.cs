using System.Drawing;
using Explorer700Library;

namespace CSA_FS24_Battleship.ExplorerIO;

public class Display
{
    private static readonly Explorer700 Explorer700 = new();
    
    public void DrawGameField()
    {
        var screen = Explorer700.Display.Graphics;
        Pen pen = new(Color.White);
        screen.Clear(Color.Black);
        screen.DrawRectangles(pen, new Rectangle[] { new(0, 0, 1, 1) });
    }
}