using RawDeal.data;

namespace RawDeal;

public class SuperStarValidator
{
    public List<object> ValidateDeckHaveSuperStar(
        List<string> listCardsPlayer, List<SuperStar> superStars)
    {
        try
        {
            return FindSuperStarInDeck(listCardsPlayer, superStars);
        }
        catch (UnexpectedBehaviorException ex)
        {
            return new List<object>();  
        }

    }

    private List<object> FindSuperStarInDeck(
        List<string> listCardsPlayer, List<SuperStar> superStars)
    {
        const int unnecessarySuffixLength = 17;
        string firstCardName = listCardsPlayer[0];
        string superStarName = firstCardName.Substring(
            0, firstCardName.Length - unnecessarySuffixLength);

        List<object> superStarInfo = TryGetSuperStarInfo(superStarName, superStars);

        if (superStarInfo == null)
        {
            throw new UnexpectedBehaviorException("Error en FindSuperStarInDeck");
        }

        return superStarInfo;
    }

    private List<object> TryGetSuperStarInfo(
        string superStarName, 
        List<SuperStar> superStars)
    {
        List<object> superStarInfo = null;

        for (int i = 0; i < superStars.Count; i++)
        {
            if (superStars[i].Name != superStarName) continue;

            superStarInfo = new List<object>
            {
                superStars[i].SuperstarValue,
                superStars[i].Name,
                superStars[i].HandSize
            };
            break;
        }

        return superStarInfo;
    }

}