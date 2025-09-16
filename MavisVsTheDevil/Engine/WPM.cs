using Raylib_cs;

namespace MavisVsTheDevil.Engine;

public static class WPM
{
	private static List<double> _lastTimeStamp = new List<double>(20);
	private static bool _isDirty = true;
	private static int _lastWPM;
	public static int GetWPM()
	{
		if (!_isDirty)
		{
			return _lastWPM;
		}
		
		//run from _current to x and add up the difference, then divide by x.
		int c = _lastTimeStamp.Count;
		if (c <= 1)
		{
			return -1;
		}

		double total = 0;
		for (var i = 1; i <c; i++)
		{
			double diff = _lastTimeStamp[i] - _lastTimeStamp[i - 1];
			total += diff;
		}

		_isDirty = false;
		_lastWPM =(int)((total / c)/60);
		return _lastWPM;
	}

	public static void TriggerWordComplete()
	{
		if (_lastTimeStamp.Count > 10)
		{
			_lastTimeStamp.RemoveAt(0);
		}

		_lastTimeStamp.Add(Raylib.GetTime());
		_isDirty = true;
	}
	public static void Reset()
	{
		_lastTimeStamp.Clear();
		_isDirty = true;
	}
}