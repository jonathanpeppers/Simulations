namespace Simulations.Logic;

/// <summary>
/// A single lifeform, or organism
/// </summary>
public class Organism
{
	public const int MaxSize = 16;

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
	public int VelocityX { get; set; } = Random.Shared.Next(-3, 4);

	/// <summary>
	/// The organism's Y Velocity
	/// </summary>
	public int VelocityY { get; set; } = Random.Shared.Next(-3, 4);

	public bool IsAlive { get; set; } = true;

	/// <summary>
	/// Updates time forward 1 step
	/// </summary>
	public void Update()
	{
		X += VelocityX;
		Y += VelocityY;

		if (X < 0)
		{
			X = 0;
			VelocityX = -VelocityX;
		}
		else if (X > MaxSize)
		{
			X = MaxSize;
			VelocityX = -VelocityX;
		}

		if (Y < 0)
		{
			Y = 0;
			VelocityY = -VelocityY;
		}
		else if (Y > MaxSize)
		{
			Y = MaxSize;
			VelocityY = -VelocityY;
		}
	}

	public void Collide(Organism other) => other.Die();

	public void Die()
	{
		IsAlive = false;
		Color = new Color { Red = 0.5f, Green = 0.5f, Blue = 0.5f };
		VelocityX = 0;
		VelocityY = 0;
	}
}