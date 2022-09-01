using Microsoft.Xna.Framework;

namespace HelloMonoWorld;

internal static class Time
{
	private static double _deltaTime;

	public static double DeltaTime
	{
		get { return _deltaTime * TimeScale; }
	}

	public static double TimeScale = 1d;

	public static void UpdateDeltaTime(GameTime gameTime)
	{
		_deltaTime = gameTime.ElapsedGameTime.TotalSeconds;
	}
}