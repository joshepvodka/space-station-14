
namespace Content.Client.Communications.AlertsConsole.UI;

public sealed class AlertsConsoleBoundUserInterface : BoundUserInterface
{

	private AlertsConsoleMenu _menu;

	[ViewVariables]
	public bool _canAnnounce;

	public AlertsConsoleBoundUserInterface(EntityUid owner, Enum uiKey) : base(owner, uiKey)
	{
	}

	protected override void Open()
    {
        base.Open();

        _menu = new CommunicationsConsoleMenu(this);
        _menu.OnClose += Close;
        _menu.OpenCentered();
    }



}

