using System.Diagnostics;
using CommunityToolkit.Maui.Core.Primitives;
using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Controls.Shapes;
using Simulations.Logic;

namespace Simulations.Mobile
{
	public partial class MainPage : ContentPage
	{
		bool stop = false;
		const int step = 50;
		Ecosystem ecosystem = new()
		{
			Organisms =
			{
				new Organism
				{
					X = 0,
					Y = 0,
					Color = Colors.Blue,
				},
				new Organism
				{
					X = Organism.MaxSize / 2,
					Y = Organism.MaxSize / 2,
					Color = Colors.Purple,
				},
				new Organism
				{
					X = Organism.MaxSize,
					Y = 0,
					Color = Colors.Green,
				},
				new Organism
				{
					X = 0,
					Y = Organism.MaxSize,
					Color = Colors.Red,
				},
				new Organism
				{
					X = Organism.MaxSize,
					Y = Organism.MaxSize,
					Color = Colors.Yellow,
				},
			},
		};

		public MainPage()
		{
			InitializeComponent();
			ecosystem.EatSound += (sender, e) => element.Play();
			UpdateChildren();
			Dispatcher.StartTimer(TimeSpan.FromSeconds(.5), Update);
			Unloaded += (sender, e) => stop = true;
		}

		bool Update()
		{
			ecosystem.Update();
			UpdateChildren();
			return !stop;
		}

		/// <summary>
		/// TODO: recreates every time, maybe could be better, or use BindableLayout
		/// </summary>
		void UpdateChildren()
		{
			layout.Children.Clear();
			foreach (var organism in ecosystem.Organisms)
			{
				var shape = new Ellipse
				{
					BindingContext = organism,
					Fill = new SolidColorBrush(organism.Color),
				};
				AbsoluteLayout.SetLayoutBounds(shape, new Rect(step * organism.X, step * organism.Y, step, step));
				layout.Children.Add(shape);
			}
		}

		void OnMediaFailed(object sender, MediaFailedEventArgs e) => DisplayAlert("Oops!", e.ErrorMessage, "Ok");

		void OnMediaOpened(object sender, EventArgs e) => Debug.WriteLine("Media opened!");
	}
}
