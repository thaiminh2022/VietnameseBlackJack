using Godot;

public abstract partial class GameManagerState : Node
{
    protected GameManager gameManager;
    
    public virtual void Start() { }
    public virtual void OnInput() { }
    public virtual void Update() {}
    public virtual void Exit() { }

    public void SetGameManager(GameManager gameManager) {
        this.gameManager = gameManager;
    }
}
