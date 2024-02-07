using Godot;

public partial class GameManagerState_Playing : GameManagerState {
    private Player[] players;

    private int turnIndex = -1;
    private int skipCounter;

    private bool playerJustDrew = false;

    private Player CurrentPlayer => players[turnIndex];

    public override void Start() {
        players = gameManager.Players;
        NextTurn();
    }
    public override void OnInput() {
        if (turnIndex == -1)
            return;

        if (Input.IsActionJustPressed(Constants.DRAW_CARD)) {
            PlayerDrawCard();
        }
        if (Input.IsActionJustPressed(Constants.SKIP_CARD)) {
            PlayerSkip();
        }
    }


    private void NextTurn() {

        if (IsLegalToEnd()) {
            gameManager.ChangeState(gameManager.ShowScoreState);
            return;
        }

        int secondLastPlayerIndex = players.Length - 1;
        if (turnIndex < secondLastPlayerIndex) {
            turnIndex++;

        } else {
            turnIndex = 0;
        }

        if (CurrentPlayer.HasDoubleAce() || CurrentPlayer.HasAceAndSpecial()) {
            NextTurn();
            return;
        }

        gameManager.InvokeOnturnChange(turnIndex);

        playerJustDrew = false;
        ChangePlayerIsTurn();
    }
    private void ChangePlayerIsTurn() {

        for (int i = 0; i < players.Length; i++) {
            players[i].SetTurn(i == turnIndex);
        }
    }

    private void PlayerDrawCard() {

        if (IsLegalToDrawCard() && !playerJustDrew) {

            // Get a card from the bottom of the deck
            var randomCard = gameManager.GetCardFromDeck(fromTop: false);
            CurrentPlayer.AddCard(randomCard);
            skipCounter = 0;
            playerJustDrew = true;
        } else {
            NextTurn();
        }
    }

    public void PlayerSkip() {
        if (IsLegalToEnd()) {
            gameManager.ChangeState(gameManager.ShowScoreState);
            return;
        }

        if (!playerJustDrew)
            skipCounter++;

        NextTurn();
    }


    private bool IsLegalToEnd() {

        bool allSkiped = skipCounter == GetSkipMax();
        if (allSkiped) {
            GD.Print("Ended by allSkiped");
            return allSkiped;
        }

        bool allMaxCard = true;
        foreach (var player in players) {
            if (player.GetHandDeck().Count != 5) {
                allMaxCard = false;
                break;
            }
        }
        if (allMaxCard) {
            GD.Print("Ended by allMax");
        }

        return allMaxCard;
    }

    private int GetSkipMax() {
        int skipMax = players.Length;
        foreach (var player in players) {
            if(player.HasDoubleAce() || player.HasAceAndSpecial()) {
                skipMax--;
            }
        }
        return skipMax;
    }

    private bool IsLegalToDrawCard() {
        var playerHandDeck = CurrentPlayer.GetHandDeck();

        return playerHandDeck.Count < 5;
    }
}
