using Raylib_cs;

namespace MavisVsTheDevil.Animation;

public static class Utility
{
    public static Color ColorLerp(Color a, Color b, float t)
    {
        return new Color(
             Single.Lerp(a.R,b.R,t),
            Single.Lerp(a.G,b.G,t),
            Single.Lerp(a.B,b.B,t),
            Single.Lerp(a.A,b.A,t)
            );
    }
}