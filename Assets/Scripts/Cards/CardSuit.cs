public enum CardSuit
{
    DIAMOND, CLUB, HEART, SPADE
}

public static class CardSuitExtensions
{
    public static string ToUIString(this CardSuit cardSuit)
    {
        switch (cardSuit)
        {
            case CardSuit.DIAMOND:
                return "♦";
            case CardSuit.CLUB:
                return "♣";
            case CardSuit.HEART:
                return "♥";
            case CardSuit.SPADE:
                return "♠";
            default:
                throw new System.NotImplementedException("No UI string for Suit " + cardSuit);
        }
    }
    public static bool IsSameColor(this CardSuit thisSuit, CardSuit otherSuit)
    {
        // because suit colors are alternating, the sum of two suits being even means they're the same color
        return ((int)thisSuit + (int)otherSuit) % 2 == 0;
    }
}