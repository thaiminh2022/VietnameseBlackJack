using Godot;
using System;
using System.Collections.Generic;

public partial class Player : Node2D, ISeatable {

    [Export] private Control cardVisual;
    [Export] private Control animationVisual;


    private List<Card> handDeck = new List<Card>();
    private bool isTurn = false;
    private bool hasDoubleAce = false;
    private bool hasAceAndSpecial = false;

    public event EventHandler OnCardAdded;
    public event EventHandler OnIsTurnChanged;


    public void CleanUp() {
        handDeck.Clear();
        isTurn = false;
        hasDoubleAce = false;
        hasAceAndSpecial = false;
    }

    public void SetTurn(bool isTurn) {
        this.isTurn = isTurn;
        OnIsTurnChanged?.Invoke(this, EventArgs.Empty);
    }
    public bool IsTurn() => isTurn;

    public List<Card> GetHandDeck() => handDeck;
    public void AddCard(Card card, bool force = false) {
        if (!isTurn && !force) {
            return;
        }
        handDeck.Add(card);
        SortCards();

        OnCardAdded.Invoke(this, EventArgs.Empty);

    }

    public bool HasDoubleAce() => hasDoubleAce;
    public bool HasAceAndSpecial() => hasAceAndSpecial;

    public void SetHasDoubleAce(bool has) {
        GD.Print(Name, " has double ace");
        hasDoubleAce = has;
    }
    public void SetHasAceAndSpeical(bool has) {
        GD.Print(Name, " has ace and special");
        hasAceAndSpecial = has;
    }

    public int GetScore() {
        int sum = 0;
        int cACount = 0;


        foreach (var card in handDeck) {

            // Calculate later
            if (card.Name == CardName.CA) {
                cACount++;
                continue;
            }

            bool isJQK = card.Name == CardName.CJ
                || card.Name == CardName.CQ
                || card.Name == CardName.CK;

            if (isJQK) {
                sum += 10;
                continue;
            }

            int cardScore = (int)card.Name + 1;
            sum += cardScore;
        }

        // A can be either 1 or 10
        // 21 is max amount before lost
        for (int i = 0; i < cACount; i++) {
            if (sum + 10 > 21) {
                sum += 1;
            } else {
                sum += 10;
            }
        }

        return sum;
    }

    private void SortCards() {
        handDeck.Sort((a, b) => {
            int valueA = (int)a.Name;
            int valueB = (int)b.Name;

            if (valueA > valueB) {
                return 1;
            } else {
                return 0;
            }
        });
    }

    public void RotateVisual(float rotation) {
        cardVisual.Rotation = Mathf.DegToRad(rotation);
        animationVisual.Rotation = Mathf.DegToRad(rotation);

        cardVisual.Position = GlobalPosition;
    }
}
