namespace Simulations.Logic;

public class Organism
{
	/// <summary>
	/// The organism's color
	/// </summary>
	public Color Color { get; set; }

	/// <summary>
	/// The color organism that this eats
	/// </summary>
	public Color? Diet { get; set; }

	/// <summary>
	/// 0 to 1, chance of finding food
	/// </summary>
	public float ChanceToForage { get; set; } = 0.75f;
}
