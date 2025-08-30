using System.Numerics;
using MavisVsTheDevil.Panels;
using Raylib_cs;

namespace MavisVsTheDevil;

public static class DrawUtility
{
    public static void DrawLineCentered(string text, int totalWidth, int y,int fontSize, Color color, int xShift = 0)
    {
        Font font = Program.terminalFont;
         if (fontSize > 60)
        {
            font = Program.headerFont;
        }else if (fontSize >= GameTitleIdleScreen.FontHeight)
        {
            font = Program.titleFont;
        }
        var tx = Raylib.MeasureTextEx(Program.terminalFont, text, fontSize, 0);
        int x = (int)(totalWidth - tx.X) / 2;
        Raylib.DrawTextEx(font, text, new Vector2(x + xShift, y), fontSize, 0, color);
    }
}