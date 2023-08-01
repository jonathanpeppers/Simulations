using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using Simulations.Logic;
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
		var generation = new Generation
		{
			Logger = new DebugTextWriter(),
		};

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

		Debug.WriteLine("Starting simulation...");
		await Task.Delay(3000);

		for (int i = 0; i < 10; i++)
		{
			UpdateScreen(generation);

			Debug.WriteLine("");
			Debug.WriteLine("New generation...");
			Debug.WriteLine(generation.ToString());
			await Task.Delay(3000);
			generation = generation.NextGeneration();
		}

		Debug.WriteLine("End of simulation...");
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
