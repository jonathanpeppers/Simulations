namespace Simulations.Logic;

public class Organism
{
	public Color Color { get; set; }

	public Color? Diet { get; set; }

	/// <summary>
	/// X position
	/// </summary>
	public int X { get; set; }

	/// <summary>
	/// Y position
	/// </summary>
	public int Y { get; set; }

	/// <summary>
	/// The organism's eyesight or range, defaults to 1
	/// </summary>
	public int Range { get; set; } = 1;

	/// <summary>
	/// The organism's X Velocity
	/// </summary>
	public int VelocityX { get; set; } = 1;

	/// <summary>
	/// The organism's Y Velocity
	/// </summary>
	public int VelocityY { get; set; } = 1;

	/// <summary>
	/// Updates time forward 1 step
	/// </summary>
	public void Update()
	{
		X += VelocityX;
		Y += VelocityY;
	}
}
