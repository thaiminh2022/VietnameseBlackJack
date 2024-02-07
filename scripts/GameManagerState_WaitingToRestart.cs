using Godot;

public partial class GameManagerState_WaitingToRestart :GameManagerState 
{
   public override void OnInput() {
       if(Input.IsActionJustPressed(Constants.RESTART)) {
           foreach(var player in gameManager.Players) {
               player.CleanUp();
           }
           gameManager.GameDeck.Clear();
           gameManager.ChangeState(gameManager.GenerateDeckState);
       }
    }
}
