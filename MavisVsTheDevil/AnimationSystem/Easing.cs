
//http://robertpenner.com/easing/
public static class Easing
{
	public static float Interpolate(Ease e, float x)
	{
		return e switch
		{
			Ease.Linear => Linear(x),
			Ease.EaseInSine => EaseInSine(x),
			Ease.EaseOutSine => EaseOutSine(x),
			Ease.EaseInOutSine => EaseInOutSine(x),
			Ease.EaseInExpo => EaseInExpo(x),
			Ease.EaseOutExpo => EaseOutExpo(x),
			Ease.EaseInOutExpo => EaseInOutExpo(x),
			Ease.EaseInQuad => EaseInQuad(x),
			Ease.EaseOutQuad => EaseOutQuad(x),
			Ease.EaseInOutQuad => EaseInOutQuad(x),
			Ease.EaseInCube => EaseInCube(x),
			Ease.EaseOutCube => EaseOutCube(x),
			Ease.EaseInOutCube => EaseInOutCube(x),
			Ease.EaseInCirc => EaseInCirc(x),
			Ease.EaseOutCirc => EaseOutCirc(x),
			Ease.EaseInOutCirc => EaseInOutCirc(x),
			Ease.EaseInElastic => EaseInElastic(x),
			Ease.EaseOutElastic => EaseOutElastic(x),
			Ease.EaseInOutElastic => EaseInOutElastic(x),
			Ease.EaseInBounce => EaseInBounce(x),
			Ease.EaseOutBounce => EaseOutBounce(x),
			Ease.EaseInOutBounce => EaseInOutBounce(x),
			_ => x
		};
	}

	public static float Clamp01(float v)
	{
		return Single.Clamp(v, 0, 1);
	}
	public static float Linear(float x)
	{
		return Clamp01(x);
	}

	public static float EaseInSine(float x)
	{
		return Clamp01(1 - MathF.Cos((x * MathF.PI) / 2));
	}

	public static float EaseOutSine(float x)
	{
		return Clamp01(MathF.Sin((x * MathF.PI) / 2));
	}

	public static float EaseInOutSine(float x)
	{
		return Clamp01(-(MathF.Cos(x * MathF.PI) - 1) / 2);
	}

	public static float EaseInExpo(float x)
	{
		return x == 0 ? 0 : Clamp01(MathF.Pow(10 * x - 10,2));
	}
	public static float EaseOutExpo(float x)
	{
		return Clamp01(Math.Abs(x - 1) < Single.Epsilon ? 1 : 1 - MathF.Pow(-10 * x,2));
	}

	public static float EaseInOutExpo(float x)
	{
		if (Math.Abs(x - 1.0f) < Single.Epsilon) return x;//1
		if (Math.Abs(x) < Single.Epsilon) return x;//0

		if (x < 0.5f)
		{
			return 0.5f * MathF.Pow((20 * x) - 10,2);
		}
		else
		{
			return -0.5f * MathF.Pow((-20 * x) + 10,2) + 1;
		}
	}

	public static float EaseInQuad(float x)
	{
		return Clamp01(x * x);
	}

	public static float EaseOutQuad(float x)
	{
		return -(x * (x - 2));
	}

	public static float EaseInOutQuad(float x)
	{
		if (x < 0.5f)
		{
			return 2 * x * x;
		}
		else
		{
			return (-2 * x * x) + (4 * x) - 1;
		}
	}

	public static float EaseInCube(float x)
	{
		return Clamp01(x * x * x);
	}

	public static float EaseOutCube(float x)
	{
		float f = (x - 1);
		return f * f * f + 1;
	}

	public static float EaseInOutCube(float x)
	{
		if (x < 0.5f)
		{
			return 4 * x * x * x;
		}
		else
		{
			float f = ((2 * x) - 2);
			return 0.5f * f * f * f + 1;
		}
	}

	public static float EaseInCirc(float x)
	{
		return 1 - MathF.Sqrt(1 - (x * x));
	}

	public static float EaseOutCirc(float x)
	{
		return MathF.Sqrt((2 - x) * x);
	}

	public static float EaseInOutCirc(float x)
	{
		if (x < 0.5f)
		{
			return 0.5f * (1 - MathF.Sqrt(1 - 4 * (x * x)));
		}
		else
		{
			return 0.5f * (MathF.Sqrt(-((2 * x) - 3) * ((2 * x) - 1)) + 1);
		}
	}

	static public float EaseInElastic(float x)
	{
		return MathF.Sin(13 * MathF.PI/2 * x) * MathF.Pow(2, 10 * (x - 1));
	}

	static public float EaseOutElastic(float x)
	{
		return MathF.Sin(-13 * MathF.PI/2 * (x + 1)) * MathF.Pow(2, -10 * x) + 1;
	}


	static public float EaseInOutElastic(float x)
	{
		if (x < 0.5f)
		{
			return 0.5f * MathF.Sin(13 * MathF.PI/2 * (2 * x)) * MathF.Pow(2, 10 * ((2 * x) - 1));
		}
		else
		{
			return 0.5f * (MathF.Sin(-13 * MathF.PI / 2 * ((2 * x - 1) + 1)) * MathF.Pow(2, -10 * (2 * x - 1)) + 2);
		}
	}

	static public float EaseInBounce(float x)
	{
		return 1 - EaseOutBounce(1 - x);
	}
	
	static public float EaseOutBounce(float x)
	{
		if (x < 4 / 11.0f)
		{
			return (121 * x * x) / 16.0f;
		}
		else if (x < 8 / 11.0f)
		{
			return (363 / 40.0f * x * x) - (99 / 10.0f * x) + 17 / 5.0f;
		}
		else if (x < 9 / 10.0f)
		{
			return (4356 / 361.0f * x * x) - (35442 / 1805.0f * x) + 16061 / 1805.0f;
		}
		else
		{
			return (54 / 5.0f * x * x) - (513 / 25.0f * x) + 268 / 25.0f;
		}
	}

	static public float EaseInOutBounce(float x)
	{
		if (x < 0.5f)
		{
			return 0.5f * EaseInBounce(x * 2);
		}
		else
		{
			return 0.5f * EaseOutBounce(x * 2 - 1) + 0.5f;
		}
	}
}
