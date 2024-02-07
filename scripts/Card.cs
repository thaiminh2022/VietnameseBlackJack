public enum CardName {
    CA,
    C02,
    C03,
    C04,
    C05,
    C06,
    C07,
    C08,
    C09,
    C10,
    CJ,
    CQ,
    CK,
}

public enum CardType {
    Hearts,
    Diamonds,
    Spades,
    Clubs,
}

public readonly struct Card {
    public CardName Name { get; }
    public CardType Type { get; }

    public Card(CardName name, CardType type) {
        this.Name = name;
        this.Type = type;
    }
}
