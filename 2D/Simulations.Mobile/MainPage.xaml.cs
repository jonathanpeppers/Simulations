using Microsoft.Maui.Controls.Shapes;
using Simulations.Logic;

namespace Simulations.Mobile
{
	public partial class MainPage : ContentPage
	{
		const int step = 50;
		Ecosystem ecosystem = new()
		{
			Organisms =
			{
				new Organism
				{
					X = 1,
					Y = 2,
					Color = Colors.Blue.ToLogicColor(),
				},
				new Organism
				{
					X = 5,
					Y = 5,
					Color = Colors.Red.ToLogicColor(),
				},
				new Organism
				{
					X = 5,
					Y = 0,
					Color = Colors.Green.ToLogicColor(),
				},
			},
		};

		public MainPage()
		{
			InitializeComponent();

			foreach (var organism in ecosystem.Organisms)
			{
				var shape = new Ellipse
				{
					BindingContext = organism,
					BackgroundColor = organism.Color.ToMauiColor(),
				};
				AbsoluteLayout.SetLayoutBounds(shape, new Rect(step * organism.X, step * organism.Y, step, step));
				layout.Children.Add(shape);
			}

			Dispatcher.StartTimer(TimeSpan.FromSeconds(1), Update);
		}

		bool Update()
		{
			ecosystem.Update();
			foreach (var shape in layout.Children.Cast<Ellipse>())
			{
				var organism = (Organism)shape.BindingContext;
				shape.BackgroundColor = organism.Color.ToMauiColor();
				AbsoluteLayout.SetLayoutBounds(shape, new Rect(step * organism.X, step * organism.Y, step, step));
			}

			return true;
		}
	}
}
