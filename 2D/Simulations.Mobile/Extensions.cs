namespace Simulations.Mobile;

public static class Extensions
{
	public static Logic.Color ToLogicColor(this Color color)
	{
		return new Logic.Color
		{
			Red = color.Red,
			Green = color.Green,
			Blue = color.Blue,
		};
	}

	public static Color ToMauiColor(this Logic.Color color)
	{
		return new Color(color.Red, color.Green, color.Blue);
	}
}
