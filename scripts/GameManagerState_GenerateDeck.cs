using System;

public partial class GameManagerState_GenerateDeck : GameManagerState
{
    public override void Start() {
        GenerateDeck();
        gameManager.ChangeState(gameManager.SplitCardState);
    }
    private void GenerateDeck() {
        var cardNames = Enum.GetValues(typeof(CardName));
        var cardTypes = Enum.GetValues(typeof(CardType));

        foreach (CardName name in cardNames) {
            foreach (CardType @type in cardTypes) {
                gameManager.GameDeck.Add(new Card(name, @type));
            }
        }

        gameManager.GameDeck.Shuffle();

    }
}
