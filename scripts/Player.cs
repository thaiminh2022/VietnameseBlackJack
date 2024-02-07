using Godot;
using System;
using System.Collections.Generic;

public partial class Player : Node2D, ISeatable
{

    public enum PlayerWinningState
    {
        Undefined,
        Won,
        NguLinh,
        XiBan,
        XiDach,
        Lost,
        QuacBai,
        ThuaNon,
    };

    [Export] private Control cardVisual;
    [Export] private Control animationVisual;


    private List<Card> handDeck = new List<Card>();
    private bool isTurn = false;
    private bool isLocked = false;

    // Win state
    private PlayerWinningState winningState = PlayerWinningState.Undefined;

    public event EventHandler OnCardAdded;
    public event EventHandler OnIsTurnChanged;
    public event EventHandler OnWinningStateChanged;


    public void CleanUp()
    {
        handDeck.Clear();
        isTurn = false;
        isLocked = false;
        winningState = PlayerWinningState.Undefined;
    }

    public void SetTurn(bool isTurn)
    {
        this.isTurn = isTurn;
        OnIsTurnChanged?.Invoke(this, EventArgs.Empty);
    }
    public bool IsTurn() => isTurn;

    public bool IsLocked() => isLocked;
    public void SetIsLocked(bool value) => isLocked = value;

    public List<Card> GetHandDeck() => handDeck;
    public void AddCard(Card card, bool force = false)
    {
        if (!isTurn && !force)
        {
            return;
        }
        handDeck.Add(card);
        SortCards();

        OnCardAdded.Invoke(this, EventArgs.Empty);

    }

    public bool HasSpeical()
    {
        return winningState switch
        {
            PlayerWinningState.XiBan => true,
            PlayerWinningState.XiDach => true,
            _ => false
        };
    }
    public void SetWinningState(PlayerWinningState winningState)
    {
        this.winningState = winningState;
        OnWinningStateChanged?.Invoke(this, EventArgs.Empty);
    }
    public PlayerWinningState GetWinningState() => winningState;

    public int GetScore()
    {
        int sum = 0;

        foreach (var card in handDeck)
        {

            // this guy need speical calculation
            if (card.Name == CardName.CA)
            {

                sum += handDeck.Count switch
                {
                    1 => 11,
                    2 => 10,
                    _ => 1,
                };

                continue;
            }

            bool isJQK = card.Name == CardName.CJ
                || card.Name == CardName.CQ
                || card.Name == CardName.CK;

            if (isJQK)
            {
                sum += 10;
                continue;
            }

            int cardScore = (int)card.Name + 1;
            sum += cardScore;
        }

        return sum;
    }

    private void SortCards()
    {
        handDeck.Sort((a, b) =>
        {
            int valueA = (int)a.Name;
            int valueB = (int)b.Name;

            if (valueA > valueB)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        });
    }

    public void RotateVisual(float rotation)
    {
        cardVisual.Rotation = Mathf.DegToRad(rotation);
        animationVisual.Rotation = Mathf.DegToRad(rotation);

        cardVisual.Position = GlobalPosition;
    }
}
