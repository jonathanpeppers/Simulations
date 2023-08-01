using Simulations.Logic;

namespace Simulations.Tests;

public class OrganismTests
{
	[Fact]
	public void OrganismUpdate()
	{
		var organism = new Organism
		{
			X = 100,
			Y = 100,
			VelocityX = 5,
			VelocityY = 5,
		};

		organism.Update();

		Assert.Equal(105, organism.X);
        Assert.Equal(105, organism.Y);
    }
}