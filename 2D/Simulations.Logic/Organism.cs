using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Maui.Graphics;

namespace Simulations.Logic;

/// <summary>
/// A single lifeform, or organism
/// </summary>
public class Organism : ObservableObject
{
	public const int MaxSize = 16;

	public Color Color { get; set; } = Colors.White;

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

	public int RemoveCounter { get; set; }

	public int EatCounter { get; set; }

	/// <summary>
	/// The "top" organism wears shades, of course
	/// </summary>
	public bool HasShades { get; set; }

	/// <summary>
	/// Updates time forward 1 step
	/// </summary>
	public void Update()
	{
		try
		{
			if (!IsAlive)
			{
				RemoveCounter++;
				return;
			}

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
		finally
		{
			// Performance hack, just fire a single PropertyChanged for empty string
			OnPropertyChanged("");
		}
	}

	public void Eat(Organism other)
	{
		EatCounter++;
		other.Die();
	}

	public Organism Duplicate() => new()
	{
		Color = Color,
		X = X,
		Y = Y,
		Range = Range,
		IsAlive = IsAlive,
	};

	public void Die()
	{
		HasShades = false;
		IsAlive = false;
		Color = Colors.Gray;
		VelocityX = 0;
		VelocityY = 0;
	}
}
