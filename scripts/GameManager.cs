using Godot;
using System;
using System.Collections.Generic;

public partial class GameManager : Node
{

    public static GameManager Instance { get; private set; }

    [ExportCategory("Game State")]

    [Export]
    public GameManagerState_GenerateDeck
        GenerateDeckState { get; private set; }

    [Export]
    public GameManagerState_SplitCard
        SplitCardState { get; private set; }

    [Export]
    public GameManagerState_CheckSpecial
        CheckSpecialState { get; private set; }

    [Export]
    public GameManagerState_Playing
        PlayingState { get; private set; }

    [Export]
    public GameManagerState_ShowScore
        ShowScoreState { get; private set; }

    private GameManagerState currentState;

    [ExportCategory("Settings")]
    [Export] public Player[] Players { get; set; }

    public List<Card> GameDeck { get; private set; }

    public event EventHandler<int> OnTurnChanged;

    public override void _EnterTree()
    {
        Instance = this;
        GameDeck = new();
    }

    public override void _Ready()
    {
        ChangeState(GenerateDeckState);
    }

    public override void _Input(InputEvent @event)
    {
        currentState?.OnInput();
    }

    public void ChangeState(GameManagerState newState)
    {
        currentState?.Exit();

        newState.SetGameManager(this);
        GD.Print("state now is: ", newState.Name);
        currentState = newState;

        newState.Start();

    }
    public void InvokeOnturnChange(int newTurn)
    {
        OnTurnChanged?.Invoke(this, newTurn);
    }

    public Card GetCardFromDeck(bool fromTop)
    {
        var gameDeck = GameDeck;
        if (gameDeck.Count <= 0)
        {
            // No more card
            throw new Exception("No more card to give");
        }

        Card gotCard;

        if (fromTop)
        {
            gotCard = gameDeck[0];
        }
        else
        {
            gotCard = gameDeck[^1];
        }

        gameDeck.Remove(gotCard);

        return gotCard;
    }


}
