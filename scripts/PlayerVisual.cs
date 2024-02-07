using Godot;
using System;

public partial class PlayerVisual : CanvasLayer {
    [Export] private Player player;

    [Export] private Control cardHolder;
    [Export] private PackedScene cardTemplate;

    [Export] private Label scoreCount;

    public override void _EnterTree() {
        player.OnCardAdded += Player_OnCardAdded;
        player.OnIsTurnChanged += Player_OnIsTurnChanged;
        GameManager.Instance.ShowScoreState.OnShowScore += ShowScoreState_OnShowScore;
    }

    private void ShowScoreState_OnShowScore(object sender, EventArgs e) {

        scoreCount.Show();
        foreach (CardSingleUI cardSingleUI in cardHolder.GetChildren()) {
            cardSingleUI.ShowCard();
        }
    }

    private void Player_OnIsTurnChanged(object sender, EventArgs e) {

        // if the player alrady has a special, just show all of them
        if(player.HasAceAndSpecial() || player.HasDoubleAce()) {
            foreach (CardSingleUI cardUI in cardHolder.GetChildren())
            {
                cardUI.ShowCard();
            }

            return;
        }

        // Change card visual base on turn
        foreach (CardSingleUI cardUI in cardHolder.GetChildren()) {
            if (player.IsTurn()) {
                cardUI.ShowCard();
                scoreCount.Show();
            } else {
                cardUI.HideCard();
                scoreCount.Hide();

            }

        }
    }

    private void Player_OnCardAdded(object sender, EventArgs e) {
        cardHolder.DestroyChildren();

        foreach (var card in player.GetHandDeck()) {
            var newCardTemplate = cardTemplate.Instantiate<CardSingleUI>();

            newCardTemplate.SetCardSprite(card);
            cardHolder.AddChild(newCardTemplate);
        }

        scoreCount.Text = player.GetScore().ToString();


    }


}
