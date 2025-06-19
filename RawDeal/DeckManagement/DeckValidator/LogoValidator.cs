using RawDeal.data;

namespace RawDeal;

public class LogoValidator
{
    public bool DeckHaveOnlyCardsWhitOneLogo(
        List<string> listCardsPlayer, ListCard cards, List<SuperStar> superStars, 
        string superStarName)
    {
        List<string> logosInvalidos = FindInvalidLogos(superStars, superStarName);

        foreach (string cardTitle in listCardsPlayer)
        {
            var card = FindCardByTitle(cards, cardTitle);
            if (card != null && CardHasInvalidLogo(card, logosInvalidos))
            {
                return false;
            }
        }

        return true;
    }
    
    private List<string> FindInvalidLogos(
        List<SuperStar> superStars, string superStarName)
    {
        List<string> invalidLogos = new List<string>();

        foreach (var superStar in superStars)
        {
            if (superStar.Name != superStarName)
            {
                invalidLogos.Add(superStar.Logo);
            }
        }

        return invalidLogos;
    }
    
    private ICard FindCardByTitle(ListCard allCards, string cardTitle)
    {
        ICard foundCard = null;

        foreach (var card in allCards)
        {
            if (card.Title == cardTitle)
            {
                foundCard = card;
                break;
            }
        }

        return foundCard;
    }
    
    private bool CardHasInvalidLogo(ICard card, List<string> invalidLogos)
    {
        foreach (var subtype in card.Subtypes)
        {
            if (IsInvalidLogo(subtype, invalidLogos))
            {
                return true;
            }
        }
        return false;
    }

    private bool IsInvalidLogo(string subtype, List<string> invalidLogos)
    {
        foreach (var invalidLogo in invalidLogos)
        {
            if (StartsWithLogo(subtype, invalidLogo))
            {
                return true;
            }
        }
        return false;
    }
    
    private bool StartsWithLogo(string subtype, string logo)
    {
        int logoLength = logo.Length;
        bool isLongEnough = subtype.Length >= logoLength;

        if (!isLongEnough)
        {
            return false;
        }

        string startingSubstring = subtype.Substring(0, logoLength);
        return startingSubstring == logo;
    }
}