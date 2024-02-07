using Godot;
using System;

public partial class CardSingleUI : TextureRect
{
    private const string EMPTY_CARD_PATH = @"res://sprites/cards/card_back.png";

    private Card defaultCard;

    public void SetCardSprite(Card card) {
        defaultCard = card;
        var cardTexture = LoadTexture(card);
        Texture = cardTexture;
    }

    public void HideCard() {
        var tex = ResourceLoader.Load<Texture2D>(EMPTY_CARD_PATH);
        Texture = tex;
    }
    public void ShowCard() {
        var cardTexture = LoadTexture(defaultCard);
        Texture = cardTexture;
    }

    private Texture2D LoadTexture(Card card) {
        string cardName = GetCardName(card);
        string path = $"res://sprites/cards/{cardName}.png";

        return ResourceLoader.Load<Texture2D>(path);
    }


    private string GetCardName(Card card) {
        var cardNumber = card.Name.ToString()[1..];
        var cardType = card.Type.ToString().ToLower();

        return $"card_{cardType}_{cardNumber}";
    }
}
