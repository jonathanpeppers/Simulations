using Simulations.Logic;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Windows;
using System.Linq;
using System.Windows.Shapes;
using System.Windows.Media;
using Color = Simulations.Logic.Color;

namespace Simulations.WPF;

public partial class MainWindow : Window
{
	public MainWindow()
	{
		InitializeComponent();
		Loaded += OnLoaded;
	}

	async void OnLoaded(object sender, RoutedEventArgs e)
	{
		var generation = new Generation();

		var list = new List<Organism>();
		for (int i = 0; i < 5; i++)
			list.Add(new Organism { Color = Color.Blue, Diet = Color.Green });

		for (int i = 0; i < 5; i++)
			list.Add(new Organism { Color = Color.Red, Diet = Color.Blue });

		for (int i = 0; i < 5; i++)
			list.Add(new Organism { Color = Color.Green });

		foreach (var organism in list.OrderBy(_ => Random.Shared.Next()))
		{
			generation.Organisms.AddLast(organism);
		}

		Console.WriteLine("Starting simulation...");
		await Task.Delay(1000);

		for (int i = 0; i < 10; i++)
		{
			UpdateScreen(generation);

			Console.WriteLine();
			Console.WriteLine("New generation...");
			Console.WriteLine(generation.ToString());
			await Task.Delay(1000);
			generation = generation.NextGeneration();
		}

		Console.WriteLine("End of simulation...");
	}

	void UpdateScreen(Generation generation)
	{
		_panel.Children.Clear();

		foreach (var organism in generation.Organisms)
		{
			SolidColorBrush brush;
			switch (organism.Color)
			{
				case Color.Red:
					brush = Brushes.Red;
					break;
				case Color.Blue:
					brush = Brushes.Blue;
					break;
				case Color.Green:
					brush = Brushes.Green;
					break;
				default:
					throw new NotSupportedException("Unknown color: " + organism.Color);
			}
			_panel.Children.Add(new Ellipse { Width = 80, Height = 80, Fill = brush });
		}
	}
}
