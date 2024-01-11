using System.IO;
using System.Threading.Tasks;
using Content.Shared.Fax;
using JetBrains.Annotations;
using Robust.Client.GameObjects;
using Robust.Client.UserInterface;

namespace Content.Client.Fax.UI;

[UsedImplicitly]
public sealed class FaxBoundUi : BoundUserInterface
{
    
    [Dependency] private readonly IFileDialogManager _fileDialogManager = default!;

    [ViewVariables]
    private FaxWindow? _window;

    private bool _dialogIsOpen = false;

    public FaxBoundUi(EntityUid owner, Enum uiKey) : base(owner, uiKey)
    {
    }

    protected override void Open()
    {
        base.Open();

        _window = new FaxWindow();
        _window.OpenCentered();

        _window.OnClose += Close;
<<<<<<< HEAD
=======
        _window.FileButtonPressed += OnFileButtonPressed;
        _window.CopyButtonPressed += OnCopyButtonPressed;
>>>>>>> c91f6893fe (fixed what sloth wanted)
        _window.SendButtonPressed += OnSendButtonPressed;
        _window.RefreshButtonPressed += OnRefreshButtonPressed;
        _window.PeerSelected += OnPeerSelected;
    }

    private async void OnFileButtonPressed()
    {
        if(_dialogIsOpen)
            return;
        _dialogIsOpen = true;
        var filters = new FileDialogFilters(new FileDialogFilters.Group("txt"));
        await using var file = await _fileDialogManager.OpenFile(filters);

        if(_window == null)
        {
            _dialogIsOpen = false;
            return;
        }
        if(_window.Disposed)
        {
            _dialogIsOpen = false;
            return;
        }
        if(file == null)
        {
            _dialogIsOpen = false;
            return;
        }

        var reader = new StreamReader(file);
        var content = await reader.ReadToEndAsync();
        
        SendMessage(new FaxFileMessage(content.Substring(0, Math.Min(content.Length, 10000)), "printed paper", _window.OfficePaper));
        
        reader.Close();
        file.Close();
        
        _dialogIsOpen = false;
    }

    private void OnSendButtonPressed()
    {
        SendMessage(new FaxSendMessage());
    }

    private void OnRefreshButtonPressed()
    {
        SendMessage(new FaxRefreshMessage());
    }

    private void OnPeerSelected(string address)
    {
        SendMessage(new FaxDestinationMessage(address));
    }

    protected override void UpdateState(BoundUserInterfaceState state)
    {
        base.UpdateState(state);

        if (_window == null || state is not FaxUiState cast)
            return;

        _window.UpdateState(cast);
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        if (disposing)
            _window?.Dispose();
    }
}
