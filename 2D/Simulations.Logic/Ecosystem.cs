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
		foreach (var organism in Organisms)
		{
			organism.Update();
		}
	}
}
