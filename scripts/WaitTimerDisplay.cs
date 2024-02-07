using Godot;

public partial class WaitTimerDisplay : Label {
    public override void _Ready() {
        GameManager.Instance.ShowScoreState.OnWaitTimeChanged += ShowScoreState_OnWaitTimeChanged;

        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged; ;

        Hide();
    }

    private void GameManager_OnStateChanged(object sender, GameManagerState e) {
        if (e is GameManagerState_ShowScore)
            Show();
        else
            Hide();
    }

    private void ShowScoreState_OnWaitTimeChanged(object sender, float time) {

        float displayTime = Mathf.Floor(time);
        Text = displayTime.ToString();

        if (time <= 0) {
            Hide();
        } else {
            Show();
        }
    }
}
