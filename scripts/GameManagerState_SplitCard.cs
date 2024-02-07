public partial class GameManagerState_SplitCard : GameManagerState
{
    public override void Start() {
        SplitCardToPlayers();
        gameManager.ChangeState(gameManager.CheckSpecialState);
    }

    private void SplitCardToPlayers() {
        foreach (Player player in gameManager.Players) {
            // Give each player 2 cards
            for (int _i = 0; _i < 2; _i++) {
                player.AddCard(gameManager.GetCardFromDeck(fromTop: true), force: true);
            }
        }
    }
    
}
