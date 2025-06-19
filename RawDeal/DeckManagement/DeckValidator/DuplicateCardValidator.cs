namespace RawDeal;

public class DuplicateCardValidator
{
    public bool ValidateDeckDontHaveRepeatCards(List<string> listCardsPlayer, ListCard cards)
    {
        try
        {
            return CheckForDuplicateCards(listCardsPlayer, cards);
        }
        catch (DuplicateCardException e)
        {
            return true;
        }
    }
    
    private bool CheckForDuplicateCards(List<string> listCardsPlayer, ListCard cards)
    {
        Dictionary<string, int> cardCounts = GetCardCounts(listCardsPlayer);

        foreach (KeyValuePair<string, int> countEntry in cardCounts)
        {
            if (countEntry.Value <= 1) continue;

            ICard card = FindCardByTitleInList(cards, countEntry.Key);
            
            if (card == null) continue;

            if (CardIsUniqueOrOverLimit(card, countEntry.Value))
            {
                throw new DuplicateCardException(countEntry.Key);
            }
        }

        return false;
    }
    
    private Dictionary<string, int> GetCardCounts(List<string> listCardsPlayer)
    {
        var cardCounts = new Dictionary<string, int>();
        foreach (string cardTitle in listCardsPlayer)
        {
            if (!cardCounts.ContainsKey(cardTitle))
            {
                cardCounts[cardTitle] = 1;
            }
            else
            {
                cardCounts[cardTitle]++;
            }
        }
        return cardCounts;
    }

    private ICard FindCardByTitleInList(ListCard allCards, string cardTitle)
    {
        foreach (var card in allCards)
        {
            if (card.Title == cardTitle)
            {
                return card;
            }
        }
        
        throw new UnexpectedBehaviorException("Comportamiento inesperado en FindCardByTitleInList");

    }

    private bool CardIsUniqueOrOverLimit(ICard card, int count)
    {
        bool isUnique = SubtypeExists(card, "Unique");
        bool isSetUp = SubtypeExists(card, "SetUp");

        return isUnique || (!isSetUp && count > 3);
    }
    
    private bool SubtypeExists(ICard card, string subtype)
    {
        foreach (var sub in card.Subtypes)
        {
            if (sub == subtype)
            {
                return true;
            }
        }
        return false;
    }
    
    
    public class DuplicateCardException : Exception
    {
        public DuplicateCardException(string cardTitle) : base(
            $"Card {cardTitle} is duplicated.") {}
    }
}