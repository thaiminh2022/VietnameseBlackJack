using Godot;
public partial class CardDeck : TextureRect
{
    public override void _Ready()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
    }

    private void GameManager_OnStateChanged(object sender, GameManagerState state)
    {
        if (state is GameManagerState_ShowScore)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }
}
