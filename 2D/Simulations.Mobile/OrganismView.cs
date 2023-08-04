#nullable enable
using Simulations.Logic;

namespace Simulations.Mobile;

public class OrganismView : Grid
{
	readonly Image image = new Image();
	Image? shadesImage;

	public OrganismView()
	{
		Children.Add(image);
	}

	protected override void OnBindingContextChanged()
	{
		base.OnBindingContextChanged();

		if (BindingContext is Organism organism)
		{
			// Performance hack, only update when BindingContext changes, or PropertyChanged w/ empty string
			organism.PropertyChanged += (sender, e) =>
			{
				if (sender is Organism organism && string.IsNullOrEmpty(e.PropertyName))
					Update(organism);
			};

			Update(organism);
		}
	}

	void Update(Organism organism)
	{
		AbsoluteLayout.SetLayoutBounds(this, new Rect(App.Scale * organism.X, App.Scale * organism.Y, App.Scale, App.Scale));
		image.Source = ToImage(organism.Color);

		if (organism.HasShades)
		{
			if (shadesImage is null)
			{
				shadesImage = new Image { Source = shades };
				Children.Add(shadesImage);
			}
			shadesImage.IsVisible = true;
		}
		else if (shadesImage != null)
		{
			shadesImage.IsVisible = false;
		}
	}

	static ImageSource ToImage(Color color)
	{
		return color.ToUint() switch
		{
			0xFF0000FF => blue,
			0xFF008000 => green,
			0xFF800080 => purple,
			0xFFFF0000 => red,
			0xFFFFFF00 => yellow,
			_ => gray,
		};
	}

	static ImageSource blue = ImageSource.FromFile("organism_blue.png");
	static ImageSource green = ImageSource.FromFile("organism_green.png");
	static ImageSource purple = ImageSource.FromFile("organism_purple.png");
	static ImageSource red = ImageSource.FromFile("organism_red.png");
	static ImageSource yellow = ImageSource.FromFile("organism_yellow.png");
	static ImageSource gray = ImageSource.FromFile("organism_gray.png");
	static ImageSource shades = ImageSource.FromFile("shades.png");
}
