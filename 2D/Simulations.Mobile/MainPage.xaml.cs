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
					X = 0,
					Y = 0,
					Color = Colors.Blue.ToLogicColor(),
				},
				new Organism
				{
					X = Organism.MaxSize,
					Y = 0,
					Color = Colors.Green.ToLogicColor(),
				},
				new Organism
				{
					X = 0,
					Y = Organism.MaxSize,
					Color = Colors.Red.ToLogicColor(),
				},
				new Organism
				{
					X = Organism.MaxSize,
					Y = Organism.MaxSize,
					Color = Colors.Yellow.ToLogicColor(),
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
					Fill = new SolidColorBrush(organism.Color.ToMauiColor()),
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
				shape.Fill = new SolidColorBrush(organism.Color.ToMauiColor());
				AbsoluteLayout.SetLayoutBounds(shape, new Rect(step * organism.X, step * organism.Y, step, step));
			}

			return true;
		}
	}
}
