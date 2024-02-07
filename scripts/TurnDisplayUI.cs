using Godot;
using System;

public partial class TurnDisplayUI : Control
{
    [Export] Label label;
    public override void _Ready() {
        GameManager.Instance.OnTurnChanged += Instance_OnTurnChanged;
    }

    private void Instance_OnTurnChanged(object sender, int turn) {
        int displayTurn = turn + 1;
        label.Text = displayTurn.ToString();
    }
}
