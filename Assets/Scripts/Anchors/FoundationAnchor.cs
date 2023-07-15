using System.Drawing;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

/// <summary>
/// Behavior for the foundation card positions
/// </summary>
public class FoundationAnchor : CardAnchor
{
    [SerializeField] private CardSuit Suit = CardSuit.CLUB;

    private GameManager gameManager;

    public override void OnStart()
    {
        base.OnStart();

        UnityEngine.Color cardColor = UnityEngine.Color.black;
        if (Suit == CardSuit.DIAMOND || Suit == CardSuit.HEART)
        {
            cardColor = UnityEngine.Color.red;
        }

        GetComponentInChildren<Text>().text = Suit.ToUIString();
        GetComponentInChildren<Text>().fontSize = 45;
        GetComponentInChildren<Text>().color = cardColor;


        gameManager = FindObjectOfType<GameManager>();
        Assert.IsNotNull(gameManager, "FoundationAnchor requires a GameManager in the scene");
    }

    public override bool CanAttachCard(PlayingCard card)
    {
        bool matchesSuit = card.Suit == Suit;
        if (NumberOfHeldCards < 1)
        {
            return matchesSuit && card.Rank == CardRank.ACE;
        }
        else
        {
            PlayingCard topCard = TopCard;
            bool isOneRankHigherThanTopCard = (int)card.Rank == ((int)topCard.Rank + 1);
            return matchesSuit && isOneRankHigherThanTopCard;
        }
    }

    public override void OnAttachCard(PlayingCard card)
    {
        base.OnAttachCard(card);
        gameManager.OnFoundationChanged();
    }

    public bool IsComplete => NumberOfHeldCards == 13;
}
