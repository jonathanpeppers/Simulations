using Simulations.Logic;

namespace Simulations.Mobile
{
	public partial class MainPage : ContentPage
	{
		bool stop = false, pause = true;
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
			for (int i = 0; i <= Organism.MaxSize; i++)
			{
				grid.ColumnDefinitions.Add(new ColumnDefinition());
				grid.RowDefinitions.Add(new RowDefinition());
			}
			color.SelectedIndex = 0;

			BindableLayout.SetItemsSource(grid, ecosystem.Organisms);
			ecosystem.EatSound += (sender, e) => Sound.Play();
			Dispatcher.StartTimer(TimeSpan.FromSeconds(.333), Update);
			Unloaded += (sender, e) => stop = true;
		}

		bool Update()
		{
			if (!pause)
			{
				ecosystem.Update();
			}
			return !stop;
		}

		void OnPausePlay(object sender, EventArgs e)
		{
			pause = !pause;
			((Button)sender).Text = pause ? "Play" : "Pause";
		}

		void OnRemoveLast(object sender, EventArgs e)
		{
			if (ecosystem.Organisms.Count > 0)
			{
				ecosystem.Organisms.RemoveAt(ecosystem.Organisms.Count - 1);
			}
		}

		void OnSliderChanged(object sender, EventArgs e)
		{
			if (sender == x || sender == y)
			{
				positionLabel.Text = $"Position: {ToInt(x)}, {ToInt(y)}";
			}
			else if (sender == velocityX || sender == velocityY)
			{
				velocityLabel.Text = $"Velocity: {ToInt(velocityX)}, {ToInt(velocityY)}";
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
				1 => Colors.Green,
				2 => Colors.Purple,
				3 => Colors.Red,
				4 => Colors.Yellow,
				_ => throw new NotImplementedException(), // Shouldn't get here?
			};
		}
	}
}
