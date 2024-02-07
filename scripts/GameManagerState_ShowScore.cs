using Godot;
using System;

public partial class GameManagerState_ShowScore : GameManagerState
{
    public event EventHandler OnShowScore;

    public override void Start() {
        OnShowScore?.Invoke(this, EventArgs.Empty);
    }
}
