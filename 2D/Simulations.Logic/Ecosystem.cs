namespace Simulations.Logic;

/// <summary>
/// A collection of Organisms
/// </summary>
public class Ecosystem
{
	public List<Organism> Organisms { get; set; } = new();

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
				}
			}
		}

		// Born pass
		Organisms.AddRange(born);

		// Update pass
		foreach (var organism in Organisms)
		{
			organism.Update();
		}
	}
}
