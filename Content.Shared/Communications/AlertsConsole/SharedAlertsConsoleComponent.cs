
using Robust.Shared.Serialization;

namespace Content.Shared.Communications.AlertsConsole;

[Serializable, NetSerializable]
public enum AlertsConsoleUiKey
{
    Key
}

public sealed partial class SharedAlertsConsoleComponent : Component
{
}

[Serializable, NetSerializable]
public sealed class AlertsConsoleInterfaceState : BoundUserInterfaceState
{
}