public partial class GameManagerState_CheckSpecial : GameManagerState {
    
    
    public override void Start() {
        CheckSpecial();
        gameManager.ChangeState(gameManager.PlayingState);
    }

    private void CheckSpecial() {
        var players = gameManager.Players;

        for (int i = 0; i < players.Length; i++) {
            int aceCount = 0;
            bool hasSpecial = false;

            var firstTwoCards = players[i].GetHandDeck();
            foreach (var card in firstTwoCards) {
                var isAce = card.Name == CardName.CA;

                if (isAce) {
                    aceCount++;
                    continue;
                }

                hasSpecial = card.Name == CardName.CJ
                            || card.Name == CardName.CQ
                            || card.Name == CardName.CK;

                // If we don't have a special or ace, break the loop
                if (!hasSpecial) {
                    break;
                }
            }

            if (aceCount == 2) {
                players[i].SetWinningState(Player.PlayerWinningState.XiBan);
            } else if (hasSpecial && aceCount == 1) {
                players[i].SetWinningState(Player.PlayerWinningState.XiDach);
            }
        }
    }
}
