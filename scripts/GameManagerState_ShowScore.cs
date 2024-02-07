using Godot;
using System;
using System.Collections.Generic;

public partial class GameManagerState_ShowScore : GameManagerState {
    private const int MINIMUM_SCORE = 16;
    private const int MAXIMUM_SCORE = 21;

    [Export] private Timer waitToChangeStateTimer;

    public event EventHandler<float> OnWaitTimeChanged;


    public override void Start() {
        SetPlayerWiningState();

        waitToChangeStateTimer.Timeout += ChangeToWaitForRestart;
        waitToChangeStateTimer.Start();
    }

    private void ChangeToWaitForRestart() {
        gameManager.ChangeState(gameManager.WaitingToRestartState);
    }

    public override void Update() {
        if (waitToChangeStateTimer.TimeLeft >= 0) {
            OnWaitTimeChanged?.Invoke(this, (float)waitToChangeStateTimer.TimeLeft);

        }
    }

    private void SetPlayerWiningState() {

        List<Player> leftOver = new();
        int maxScore = -1;

        // Check for any losts
        foreach (var player in gameManager.Players) {
            int playerScore = player.GetScore();

            // Ngu linh!
            if(player.GetHandDeck().Count == 5) {
                if (playerScore >= 16 && playerScore <=21) {
                    player.SetWinningState(Player.PlayerWinningState.NguLinh);
                    continue;
                }

            }

            if (playerScore > MAXIMUM_SCORE) {
                player.SetWinningState(Player.PlayerWinningState.QuacBai);
            } else if (playerScore < MINIMUM_SCORE) {
                player.SetWinningState(Player.PlayerWinningState.ThuaNon);
            } else {
                leftOver.Add(player);
                if(playerScore > maxScore) {
                    maxScore = playerScore;
                }
            }
        }

        // Check for win
        foreach (var player in leftOver) {
            if(player.GetScore() < maxScore) {
                player.SetWinningState(Player.PlayerWinningState.Lost);
            }else {
                player.SetWinningState(Player.PlayerWinningState.Won);
            }
        }
    }
    public override void Exit() {
        waitToChangeStateTimer.Timeout -= ChangeToWaitForRestart;
    }

}
