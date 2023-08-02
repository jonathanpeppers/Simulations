using Simulations.Logic;

namespace Simulations.Tests;

public class OrganismTests
{
	[Fact]
	public void OrganismUpdate()
	{
		var organism = new Organism
		{
			X = 2,
			Y = 3,
			VelocityX = 5,
			VelocityY = 5,
		};

		organism.Update();

		Assert.Equal(7, organism.X);
		Assert.Equal(8, organism.Y);
	}

	[Fact]
	public void OffscreenNegative()
	{
		var organism = new Organism
		{
			X = 5,
			Y = 5,
			VelocityX = -10,
			VelocityY = -10,
		};

		organism.Update();

		Assert.Equal(0, organism.X);
		Assert.Equal(0, organism.Y);
		Assert.Equal(10, organism.VelocityX);
		Assert.Equal(10, organism.VelocityY);
	}

	[Fact]
	public void OffscreenPositive()
	{
		var organism = new Organism
		{
			X = Organism.MaxSize - 5,
			Y = Organism.MaxSize - 5,
			VelocityX = 10,
			VelocityY = 10,
		};

		organism.Update();

		Assert.Equal(Organism.MaxSize, organism.X);
		Assert.Equal(Organism.MaxSize, organism.Y);
		Assert.Equal(-10, organism.VelocityX);
		Assert.Equal(-10, organism.VelocityY);
	}
}
