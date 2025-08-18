using Raylib_cs;

namespace MavisVsTheDevil.Animation;

/// <summary>
/// Given a value between 0 and 1, a tween is evaluated.
/// </summary>
public abstract class TweenBase
{
    public abstract void Evaluate(float t);
}

public abstract class PropertyTween<T> : TweenBase
{
    public Action<T> OnEval;

    protected PropertyTween(Action<T> onEval)
    {
        OnEval = onEval;
    }

    public T Value => _value;
    protected T _value;
}


public class ColorTween : PropertyTween<Color>
{
    private Color _start;
    private Color _end;
    public ColorTween(Action<Color> eval, Color start, Color end) : base(eval)
    {
        _start = start;
        _end = end;
    }

    public override void Evaluate(float t)
    {
        _value = Utility.ColorLerp(_start, _end, t);
        OnEval?.Invoke(_value);
    }
}


public class FloatTween : PropertyTween<float>
{
    private float _start;
    private float _end;
    public FloatTween(Action<float> eval, float start, float end) : base(eval)
    {
        _start = start;
        _end = end;
    }

    public override void Evaluate(float t)
    {
        _value = Single.Lerp(_start, _end, t);
        OnEval?.Invoke(_value);
    }
}

public class AlphaTween : PropertyTween<Color>
{
    private readonly Color _diffuse;
    private bool _fadeIn;

    public AlphaTween(Action<Color> eval, Color color, bool fadeIn) : base(eval)
    {
        _diffuse = color;
        _fadeIn = fadeIn;
    }

    public override void Evaluate(float t)
    {
        if (_fadeIn)
        {
            _value = new Color(_diffuse.R, _diffuse.G, _diffuse.B, t);
        }
        else
        {
            _value = new Color(_diffuse.R, _diffuse.G, _diffuse.B, t);
        }

        OnEval?.Invoke(_value);
    }
}

public class IntTween : PropertyTween<int>
{
    private int _start;
    private int _end;
    public IntTween(Action<int> onEval, int start, int end) : base(onEval)
    {
        _start = start;
        _end = end;
    }
    public IntTween(Action<int> onEval, int end) : base(onEval)
    {
        _start = 0;
        _end = end;
    }

    public override void Evaluate(float t)
    {
        //todo: does this work for negative start values? or any negative values?
        float c = _end-_start;
        int index = (int)MathF.Floor(t * c);
        float remainder = (t * c) % 1;
        _value = _start + index;
        OnEval?.Invoke(_value);
    }
}

public class IndexTween<T> : PropertyTween<T>
{
    private IList<T> _values;
    
    public IndexTween(Action<T> onEval, IList<T> items) : base(onEval)
    {
        _values = items;
    }
    public override void Evaluate(float t)
    {
        //todo: does this work for negative start values? or any negative values?
        float c = _values.Count;
        int index = (int)MathF.Floor((t * c - Single.Epsilon));
        float remainder = (t * c) % 1;
        _value = _values[index];
        OnEval?.Invoke(_value);
    }
}

public class NopTween : TweenBase
{
    public override void Evaluate(float t)
    {
        
    }
}