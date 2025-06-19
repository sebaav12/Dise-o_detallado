namespace RawDeal.data;
using System.Text.Json.Serialization;
using RawDealView.Formatters;

public class SuperStar
{
    public string Name { get; set; }
    public string Logo { get; set; }

    [JsonPropertyName("HandSize")]
    public int HandSize { get; set; }

    [JsonPropertyName("SuperstarValue")]
    public int SuperstarValue { get; set; }

    [JsonPropertyName("SuperstarAbility")]
    public string SuperstarAbility { get; set; }
    
    public List<string> GetInfoCardsInRingSide(Player player)
    {
        List<string> cardsinfo = new List<string> { };
                        
        foreach (ICard card in player.GetCardsRingSide())
        {
            ViewableCardInfo cardInfo = new ViewableCardInfo(card);
            string cardStr = Formatter.CardToString(cardInfo);
            cardsinfo.Add(cardStr);
        }

        return cardsinfo;
    }
    
    public List<string> GetInfoCardsInHand(Player player)
    {
        List<string> cardsinfo = new List<string> { };
        
        foreach (ICard card in player.GetCardsHand())
        {
            ViewableCardInfo cardInfo = new ViewableCardInfo(card);
            string cardStr = Formatter.CardToString(cardInfo);
            cardsinfo.Add(cardStr);
        }

        return cardsinfo;
    }
    
    public string InfoCardInString(ICard card)
    {
        ViewableCardInfo cardInfo = new ViewableCardInfo(card);
        string cardStr = Formatter.CardToString(cardInfo);
        return cardStr;
    }
}
