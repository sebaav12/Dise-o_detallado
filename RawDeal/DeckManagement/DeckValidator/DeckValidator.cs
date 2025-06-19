using RawDeal.data;
namespace RawDeal;
using System.Collections.Generic;

public class DeckValidator
{
    public List<object> ValidateDeck(
        List<string> listCardsPlayer, ListCard cards, List<SuperStar> superStars)
    {
        DeckSizeValidator deckSizeValidator = new DeckSizeValidator();
        SuperStarValidator superStarValidator = new SuperStarValidator();
        DuplicateCardValidator duplicateCardValidator = new DuplicateCardValidator();
        LogoValidator logoValidator = new LogoValidator();
        CardTypeValidator cardTypeValidator = new CardTypeValidator();

        List<object> validationResult = SetupReturn();
        
        bool correctNumerOfCards = 
            deckSizeValidator.ValidateDeckHaveCorrectNumberOfCards(listCardsPlayer);
        if (correctNumerOfCards == false)
        {
            return validationResult;
        }
        
        List<object> deckHaveSuperStar = 
            superStarValidator.ValidateDeckHaveSuperStar(
                listCardsPlayer, superStars);
        if (deckHaveSuperStar != null)
        {
            validationResult[1] = deckHaveSuperStar[0];
            validationResult[2] = deckHaveSuperStar[1];
            validationResult[3] = deckHaveSuperStar[2];
        }
        else
        {
            return validationResult;
        }
        
        bool repeatCards = 
            duplicateCardValidator.ValidateDeckDontHaveRepeatCards(
                listCardsPlayer, cards);
        
        if (repeatCards)
        {
            return validationResult;
        }
        
        bool deckhaveHeelAndFace = 
            cardTypeValidator.DeckHaveHeelAndFace(
                listCardsPlayer, cards);
        if (deckhaveHeelAndFace)
        {
            return validationResult;
        }

        string superStarName = validationResult[2].ToString();
        bool deckHaveOnlyCardsWhitOneLogo = 
            logoValidator.DeckHaveOnlyCardsWhitOneLogo(listCardsPlayer, cards, 
                superStars, superStarName);
        if (deckHaveOnlyCardsWhitOneLogo == false)
        {
            return validationResult;
        }
        
        validationResult[0] = true;
        return validationResult;
    }
    
    private List<object> SetupReturn()
    {
        List<object> returnOfValidateDeck = new List<object>();
        bool deckIsValid = false;
        int superStarValue = -1;
        int handSize = -1;
        returnOfValidateDeck.Add(deckIsValid);     
        returnOfValidateDeck.Add(superStarValue);  
        returnOfValidateDeck.Add("Insert Name of Super Star");
        returnOfValidateDeck.Add(handSize);

        return returnOfValidateDeck;
    }
    
    public class SuperStarNotFoundException : Exception
    {
        public SuperStarNotFoundException() : base("Superstar not found in deck.") {}
    }
 
}
