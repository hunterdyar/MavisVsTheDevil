using Raylib_cs;

namespace MavisVsTheDevil.Animation;

public static class Utility
{
    public static Color ColorLerp(Color a, Color b, float t)
    {
        return new Color(
             Single.Lerp((float)a.R/255f,(float)b.R/255f,t),
            Single.Lerp((float)a.G/255f,(float)b.G/255f,t),
            Single.Lerp((float)a.B/255f,(float)b.B/255f,t),
            Single.Lerp((float)a.A/255f,(float)b.A/255f,t)
            );
    }
}