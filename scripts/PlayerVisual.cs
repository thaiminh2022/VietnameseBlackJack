using Godot;
using System;

public partial class PlayerVisual : CanvasLayer
{
    [Export] private Player player;


    [ExportCategory("Card displays")]

    [Export] private Control cardHolder;
    [Export] private PackedScene cardTemplate;

    [ExportCategory("Labels and Btns")]
    [Export] private Label scoreCountLabel;
    [Export] private Control restartBtn;
    [Export] private Control lockBtn;

    public override void _EnterTree()
    {
        player.OnCardAdded += Player_OnCardAdded;
        player.OnIsTurnChanged += Player_OnIsTurnChanged;
        player.OnWinningStateChanged += Player_OnWinningStateChanged;
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;

        restartBtn.Hide();
    }

    private void GameManager_OnStateChanged(object sender, GameManagerState e)
    {
        if (e is GameManagerState_WaitingToRestart)
        {
            restartBtn.Show();
            lockBtn.Hide();
        }else {
            restartBtn.Hide();
        }

    }

    private void Player_OnWinningStateChanged(object sender, EventArgs e)
    {
        ShowAllScore();

        var playerWinningState = player.GetWinningState();
        GD.Print(player.Name, " ", playerWinningState);
    }

    private void ShowAllScore()
    {
        scoreCountLabel.Show();
        foreach (CardSingleUI cardSingleUI in cardHolder.GetChildren())
        {
            cardSingleUI.ShowCard();
        }
    }

    private void Player_OnIsTurnChanged(object sender, EventArgs e)
    {

        // if the player alrady has a special, just show all of them
        if (player.HasSpeical())
        {
            foreach (CardSingleUI cardUI in cardHolder.GetChildren())
            {
                cardUI.ShowCard();
            }

            return;
        }

        // Change card visual base on turn
        foreach (CardSingleUI cardUI in cardHolder.GetChildren())
        {
            if (player.IsTurn())
            {
                cardUI.ShowCard();
                scoreCountLabel.Show();
            }
            else
            {
                cardUI.HideCard();
                scoreCountLabel.Hide();

            }

        }
    }

    private void Player_OnCardAdded(object sender, EventArgs e)
    {
        cardHolder.DestroyChildren();

        foreach (var card in player.GetHandDeck())
        {
            var newCardTemplate = cardTemplate.Instantiate<CardSingleUI>();

            newCardTemplate.SetCardSprite(card);
            cardHolder.AddChild(newCardTemplate);
        }

        scoreCountLabel.Text = player.GetScore().ToString();


    }


}
