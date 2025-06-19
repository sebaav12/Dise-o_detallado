namespace RawDeal;

public class CardToPlayInfo
{
    List<(string, int, int, int,string)> cardToPlayInfo = 
        new List<(string, int, int, int, string)>();
    
    public void Add(string cardToPlay, int positionInHand, 
        int positionInHandJugable, int positionInCartasJugablesList, string tyOfCardUpper)
    {
        cardToPlayInfo.Add((cardToPlay, positionInHand, 
            positionInHandJugable, positionInCartasJugablesList, tyOfCardUpper));
    }
    
    public (string cardToPlay, int positionInHand, 
        int positionInHandJugable, int positionInCartasJugablesList, 
        string typeOfCardUpper) GetInfoById(int index)
    {
        if (index < 0 || index >= cardToPlayInfo.Count)
        {
            throw new IndexOutOfRangeException();
        }
        return cardToPlayInfo[index];
    }
}