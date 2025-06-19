namespace RawDeal;

public class CardTypeValidator
{
    
    public bool DeckHaveHeelAndFace(List<string> listCardsPlayer, ListCard cards)
    {
        return CheckCardPresenceInDeck(listCardsPlayer, cards, "Heel") &&
               CheckCardPresenceInDeck(listCardsPlayer, cards, "Face");
    }

    private bool CheckCardPresenceInDeck(
        List<string> listCardsPlayer, ListCard cards, string cardType)
    {
        bool cardFound = false;

        foreach (string element in listCardsPlayer)
        {
            if (IsCardInDeck(cards, element, cardType))
            {
                cardFound = true;
                break;
            }
        }

        return cardFound;
    }

    private bool IsCardInDeck(ListCard cards, string element, string cardType)
    {
        foreach (var card in cards)
        {
            if (element == card.Title && CardContainsSubtype(card, cardType))
            {
                return true;
            }
        }

        return false;
    }

    private bool CardContainsSubtype(ICard card, string cardType)
    {
        foreach (var sub in card.Subtypes)
        {
            if (sub == cardType)
            {
                return true;
            }
        }

        return false;
    }
}