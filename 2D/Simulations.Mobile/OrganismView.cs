using Simulations.Logic;

namespace Simulations.Mobile;

public class OrganismView : Image
{
	protected override void OnBindingContextChanged()
	{
		base.OnBindingContextChanged();

		if (BindingContext is Organism organism)
		{
			AbsoluteLayout.SetLayoutBounds(this, new Rect(App.Scale * organism.X, App.Scale * organism.Y, App.Scale, App.Scale));
			Source = ToImage(organism.Color);
		}
		else
		{
			Source = null;
		}
	}

	static ImageSource ToImage(Color color)
	{
		return color.ToUint() switch
		{
			0xFF0000FF => ImageSource.FromFile("organism_blue.png"),
			0xFF008000 => ImageSource.FromFile("organism_green.png"),
			0xFF800080 => ImageSource.FromFile("organism_purple.png"),
			0xFFFF0000 => ImageSource.FromFile("organism_red.png"),
			0xFFFFFF00 => ImageSource.FromFile("organism_yellow.png"),
			_ => ImageSource.FromFile("organism_gray.png")
		};
	}
}
