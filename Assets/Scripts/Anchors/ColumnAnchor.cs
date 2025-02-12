﻿using UnityEngine;

/// <summary>
/// Behavior for card column. Cascades attached cards
/// </summary>
public class ColumnAnchor : CardAnchor
{
    [Tooltip("Vertical distance between two stacked cards. Measured as percent of screen height")]
    [SerializeField] private float CardStackingOffset = 0.05f;
    private float FixOffset = 0.7f;
    private float UnitOffset => (CardStackingOffset * Screen.height);

    public override bool CanAttachCard(PlayingCard card)
    {
        if (NumberOfHeldCards < 1)
        {
            return true;
        }

        PlayingCard topCard = TopCard;

        bool cardsAreDifferentColors = !topCard.Suit.IsSameColor(card.Suit);
        bool topCardIsOneRankHigher = (int)card.Rank == (int)topCard.Rank - 1;

        return cardsAreDifferentColors && topCardIsOneRankHigher;
    }

    public override void OnAttachCard(PlayingCard card)
    {
        base.OnAttachCard(card);
        foreach (Transform childCard in HeldCardsTransform)
        {
            childCard.GetComponent<PlayingCard>().MoveToAnchor();
        }
    }
    
    public override Vector3 GetAttachmentPosition(PlayingCard card)
    {
        int cardNumber = card.transform.GetSiblingIndex();
        return transform.position + (Vector3.down * UnitOffset * cardNumber * FixOffset);
    }
}
