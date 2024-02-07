using Godot;
using System;

public partial class PlayerAnimation : AnimationPlayer {
    const string DRAW_CARD_ANIMATION = "draw_card";
    [Export] Player player;

    public override void _Ready() {
        player.OnCardAdded += OnCardAdded;
    }

    public void OnCardAdded(object sender, EventArgs args) {
        Play(DRAW_CARD_ANIMATION);
    }
}
