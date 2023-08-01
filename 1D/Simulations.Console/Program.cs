using Simulations.Logic;

var generation = new Generation();

var list = new List<Organism>();
for (int i = 0; i < 5; i++)
	list.Add(new Organism { Color = Color.Blue, Diet = Color.Green });

for (int i = 0; i < 5; i++)
	list.Add(new Organism { Color = Color.Red, Diet = Color.Blue });

for (int i = 0; i < 5; i++)
	list.Add(new Organism { Color = Color.Green });

foreach (var organism in list.OrderBy(_ => Random.Shared.Next()))
{
	generation.Organisms.AddLast(organism);
}

Console.WriteLine("Starting simulation...");
await Task.Delay(1000);

for (int i = 0; i < 10; i++)
{
	Console.WriteLine();
	Console.WriteLine("New generation...");
	Console.WriteLine(generation.ToString());
	await Task.Delay(1000);
	generation = generation.NextGeneration();
}

Console.WriteLine("End of simulation...");
Console.ReadLine();
