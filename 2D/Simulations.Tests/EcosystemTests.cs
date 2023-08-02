using Simulations.Logic;

namespace Simulations.Tests;

public class EcosystemTests
{
	[Fact]
	public void EcosystemUpdate()
	{
		var ecosystem = new Ecosystem
		{
			Organisms =
			{
				new Organism
				{
					X = 2,
					Y = 3,
					VelocityX = 5,
					VelocityY = 5,
				}
			}
		};

		ecosystem.Update();

		var organism = ecosystem.Organisms.First();
		Assert.Equal(7, organism.X);
		Assert.Equal(8, organism.Y);
	}
}
