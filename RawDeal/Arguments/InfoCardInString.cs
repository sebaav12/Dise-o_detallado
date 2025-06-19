using RawDealView.Formatters;

namespace RawDeal.Arguments;

public class CardInfoService
{
    public string InfoCardInString(ICard card)
    {
        ViewableCardInfo cardInfo = new ViewableCardInfo(card);
        string cardStr = Formatter.CardToString(cardInfo);
        return cardStr;
    }
}
