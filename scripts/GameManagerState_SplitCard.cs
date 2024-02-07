public partial class GameManagerState_SplitCard : GameManagerState
{
    public override void Start() {
        SplitCardToPlayers();
        gameManager.ChangeState(gameManager.CheckSpecialState);
    }

    private void SplitCardToPlayers() {
        for (int i = 0; i < gameManager.Players.Length; i++) {
            Player player = gameManager.Players[i];

            // Rig the game to test
            if(i == 1) {
                var aceCard = new Card(CardName.CA, CardType.Hearts);
                player.AddCard(aceCard, force: true);
                player.AddCard(aceCard, force: true);

                continue;
            }

            // Give each player 2 cards
            for (int _i = 0; _i < 2; _i++) {
                player.AddCard(gameManager.GetCardFromDeck(fromTop: true), force: true);
            }
        }
    }
    
}
