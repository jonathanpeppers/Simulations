using System.Diagnostics;
using CommunityToolkit.Maui.Core.Primitives;
using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Controls.Shapes;
using Simulations.Logic;

namespace Simulations.Mobile
{
	public partial class MainPage : ContentPage
	{
		bool stop = false, pause = true;
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
			color.SelectedIndex = 0;

			ecosystem.EatSound += (sender, e) => element.Play();
			UpdateChildren();
			Dispatcher.StartTimer(TimeSpan.FromSeconds(.5), Update);
			Unloaded += (sender, e) => stop = true;
		}

		bool Update()
		{
			if (!pause)
			{
				ecosystem.Update();
				UpdateChildren();
			}
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
				AddOrganism(organism);
			}
		}

		void AddOrganism(Organism organism)
		{
			var image = new Image
			{
				BindingContext = organism,
				Source = ToImage(organism.Color),
			};
			AbsoluteLayout.SetLayoutBounds(image, new Rect(step * organism.X, step * organism.Y, step, step));
			layout.Children.Add(image);
		}

		void OnPausePlay(object sender, EventArgs e)
		{
			pause = !pause;
			((Button)sender).Text = pause ? "Play" : "Pause";
		}

		void OnMediaFailed(object sender, MediaFailedEventArgs e) => DisplayAlert("Oops!", e.ErrorMessage, "Ok");

		void OnMediaOpened(object sender, EventArgs e) => Debug.WriteLine("Media opened!");

		void OnRemoveLast(object sender, EventArgs e)
		{
			if (ecosystem.Organisms.Count > 0)
			{
				ecosystem.Organisms.RemoveAt(ecosystem.Organisms.Count - 1);
			}
			if (layout.Children.Count > 0)
			{
				layout.Children.RemoveAt(layout.Children.Count - 1);
			}
		}

		void OnAddNew(object sender, EventArgs e)
		{
			var organism = new Organism
			{
				X = ToInt(x),
				Y = ToInt(y),
				VelocityX = ToInt(velocityX),
				VelocityY = ToInt(velocityY),
				Color = GetColor(),
			};
			ecosystem.Organisms.Add(organism);
			AddOrganism(organism);
		}

		static int ToInt(Slider slider) => (int)Math.Round(slider.Value);

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
