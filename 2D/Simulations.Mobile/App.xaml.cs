namespace Simulations.Mobile;

public partial class App : Application
{
	/// <summary>
	/// 1 unit in Simulations.Logic is multiplied by this Scale for the screen coordinates
	/// </summary>
	public const int Scale = 46;

	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
	}

	protected override Window CreateWindow(IActivationState activationState)
	{
		var window = base.CreateWindow(activationState);
#if WINDOWS || MACCATALYST
		window.Width = 1150;
		window.Height = 1000;
#endif
		return window;
	}
}
