using System.Text;

namespace Simulations.Logic;

public class Generation
{
	public TextWriter Logger { get; set; } = Console.Out;

	public LinkedList<Organism> Organisms { get; set; } = new();

	public Generation NextGeneration()
	{
		var nextGeneration = new Generation();
		var current = Organisms.First;
		var random = Random.Shared;
		while (current is not null)
		{
			// If forage is successful
			if (random.NextDouble() <= current.Value.ChanceToForage)
			{
				if (current.Value.Diet is null)
				{
					nextGeneration.Organisms.AddLast(current.Value);
				}
				else
				{
					bool ate = false;
					if (current.Previous is not null && current.Previous.Value.Color == current.Value.Diet)
					{
						Logger.WriteLine($"{current.Value.Color} ate previous {current.Previous.Value.Color}.");
						ate = true;
						nextGeneration.Organisms.Remove(current.Previous.Value);
						Organisms.Remove(current.Previous);
						nextGeneration.Organisms.AddLast(current.Value);
					}
					if (current.Next is not null && current.Next.Value.Color == current.Value.Diet)
					{
						ate = true;
						Logger.WriteLine($"{current.Value.Color} ate next {current.Next.Value.Color}.");
						nextGeneration.Organisms.Remove(current.Next.Value);
						Organisms.Remove(current.Next);
						nextGeneration.Organisms.AddLast(current.Value);
					}
					if (!ate)
					{
						Logger.WriteLine($"{current.Value.Color} died, no food.");
					}
				}
			}
			else if (current.Value.Diet is null)
			{
				Logger.WriteLine($"{current.Value.Color} survived, no diet.");
				nextGeneration.Organisms.AddLast(current.Value);
			}
			else
			{
				Logger.WriteLine($"{current.Value.Color} did not forage, died.");
			}

			current = current.Next;
		}
		
		return nextGeneration;
	}

	public override string ToString()
	{
		var builder = new StringBuilder("Surviving Organisms");
		builder.AppendLine();
		foreach (var grouping in Organisms.GroupBy(o => o.Color))
		{
			builder.AppendLine($"{grouping.Key.ToString()}: {grouping.Count()}");
		}
		foreach (var organism in Organisms)
		{
			builder.AppendLine(organism.Color.ToString());
		}
		return builder.ToString();
	}
}
