using System.Collections.ObjectModel;

namespace Simulations.Logic;

/// <summary>
/// A collection of Organisms
/// </summary>
public class Ecosystem
{
	public ObservableCollection<Organism> Organisms { get; set; } = new();

	/// <summary>
	/// Fired when we could play a sound effect
	/// </summary>
	public event EventHandler EatSound = delegate { };

	/// <summary>
	/// 50% chance this returns true, overridden by unit tests
	/// </summary>
	internal Func<bool> FiftyFifty { get; set; } = () => Random.Shared.Next(2) == 0;

	/// <summary>
	/// Move forward time, one step
	/// </summary>
	public void Update()
	{
		var born = new List<Organism>();

		// Collision pass
		foreach (var first in Organisms)
		{
			if (!first.IsAlive)
				continue;

			foreach (var second in Organisms)
			{
				if (first == second)
					continue;
				if (!second.IsAlive)
					continue;
				if (first.Color == second.Color)
					continue;

				if (Math.Abs(first.X -  second.X) <= first.Range &&
					Math.Abs(first.Y - second.Y) <= first.Range)
				{
					// 50% chance which one wins
					if (FiftyFifty())
					{
						first.Eat(second);
						born.Add(first.Duplicate());
					}
					else
					{
						second.Eat(first);
						born.Add(second.Duplicate());
					}

					EatSound(this, EventArgs.Empty);
				}
			}
		}

		// Born pass
		foreach (var organism in born)
		{
			Organisms.Add(organism);
		}

		// Update pass
		foreach (var organism in Organisms)
		{
			organism.Update();
		}

		// Remove pass, reverse for-loop
		for (int i = Organisms.Count - 1; i >= 0; i--)
		{
			if (Organisms[i].RemoveCounter > 10)
			{
				Organisms.RemoveAt(i);
			}
		}
	}
}
