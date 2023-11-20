
using Content.Client.UserInterface.Controls;
using Robust.Client.UserInterface.XAML;

namespace Content.Client.Communications.AlertsConsole.UI;

public sealed partial class AlertsConsoleMenu : FancyWindow
{

	private AlertsConsoleBoundUserInterface Owner;

	public AlertsConsoleMenu(AlertsConsoleBoundUserInterface owner)
	{
		Owner = owner;
	}
}
