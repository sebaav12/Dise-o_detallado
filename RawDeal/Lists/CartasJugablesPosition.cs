using System.Collections;

namespace RawDeal
{
    public class PositionInTheDeckOfPlayableCards
    {
        List<CardToPlayInfo> cartasJugablesPosition = new List<CardToPlayInfo>();
        
        public void Add(CardToPlayInfo cardInfo)
        {
            cartasJugablesPosition.Add(cardInfo);
        }
        
        public CardToPlayInfo GetCardInfoById(int index)
        {
            if (index < 0 || index >= cartasJugablesPosition.Count)
            {
                throw new IndexOutOfRangeException();
            }
            return cartasJugablesPosition[index];
        }
    }
}