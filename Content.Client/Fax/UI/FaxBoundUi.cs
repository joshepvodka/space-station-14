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

    public async Task PrintFile()
    {
        //Open file select dialog
        Stream? file;
        var filters = new FileDialogFilters(new FileDialogFilters.Group("txt"));
        try
        {
            file = await _fileDialogManager.OpenFile(filters);            
        }
        catch(IOException)
        {
            return;
        }

        //If UI gets closed of file is null return.
        if(_window == null)
            return;
        if (_window.Disposed)
            return;
        if(file == null)
            return;

        //Read the file contents and raise event.
        string content;
        try
        {
            StreamReader reader = new StreamReader(file);
            content = reader.ReadToEnd();
            reader.Close();
            file.Close();
        }
        catch(IOException)
        {
            return;
        }

        SendMessage(new FaxFileMessage(content.Substring(0, Math.Min(content.Length, 10000)), "printed paper", _window.OfficePaper));
    }

    private void OnFileButtonPressed()
    {
        PrintFile();
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
