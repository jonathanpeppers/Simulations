using Microsoft.Maui.Graphics;
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

	[Fact]
	public void Collision()
	{
		var ecosystem = new Ecosystem
		{
			Organisms =
			{
				new Organism
				{
					X = 1,
					Y = 1,
					VelocityX = 2,
					VelocityY = 0,
				},
				new Organism
				{
					X = 2,
					Y = 1,
					VelocityX = -2,
					VelocityY = 0,
				}
			}
		};

		ecosystem.Update();

		var organism = ecosystem.Organisms.Last();
		Assert.False(organism.IsAlive);
		Assert.Equal(Colors.Gray, organism.Color);
	}
}
