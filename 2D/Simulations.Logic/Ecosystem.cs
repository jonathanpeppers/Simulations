namespace Simulations.Logic;

/// <summary>
/// A collection of Organisms
/// </summary>
public class Ecosystem
{
	public List<Organism> Organisms { get; set; } = new();

	/// <summary>
	/// Move forward time, one step
	/// </summary>
	public void Update()
	{
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

				if (Math.Abs(first.X -  second.X) <= first.Range &&
					Math.Abs(first.Y - second.Y) <= first.Range)
				{
					first.Collide(second);
				}
			}
		}

		// Update pass
		foreach (var organism in Organisms)
		{
			organism.Update();
		}
	}
}
