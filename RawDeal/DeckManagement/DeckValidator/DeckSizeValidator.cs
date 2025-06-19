namespace RawDeal;

public class DeckSizeValidator
{
    public bool ValidateDeckHaveCorrectNumberOfCards(List<string> listCardsPlayer)
    {
        try
        {
            int sizeDeck1 = 0;
            int numberOfCardsInDeck = 61;
        
            foreach (string elemento in listCardsPlayer)
            {
                sizeDeck1++;
            }

            if (sizeDeck1 != numberOfCardsInDeck)
            {
                throw new InvalidDeckSizeException();
            }
            
            return true;
        }
        catch (InvalidDeckSizeException e)
        {
            return false;
        }
    }
    
    public class InvalidDeckSizeException : Exception
    {
        public InvalidDeckSizeException() : base(
            "The deck does not have the correct number of cards.") {}
    }
}