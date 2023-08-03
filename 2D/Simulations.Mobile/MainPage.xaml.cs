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

		void OnRemoveLast(object sender, EventArgs e)
		{
			if (ecosystem.Organisms.Count > 0)
			{
				ecosystem.Organisms.RemoveAt(ecosystem.Organisms.Count - 1);
			}
		}

		void OnAddNew(object sender, EventArgs e)
		{
			ecosystem.Organisms.Add(new()
			{
				X = ToInt(x),
				Y = ToInt(y),
				VelocityX = ToInt(velocityX),
				VelocityY = ToInt(velocityY),
				Color = GetColor(),
			});
		}

		static int ToInt(Slider slider) => (int)Math.Round(slider.Value);

		Color GetColor()
		{
			return color.SelectedIndex switch
			{
				0 => Colors.Blue,
				1 => Colors.Purple,
				2 => Colors.Green,
				3 => Colors.Red,
				4 => Colors.Yellow,
				_ => throw new NotImplementedException(), // Shouldn't get here?
			};
		}
	}
}
