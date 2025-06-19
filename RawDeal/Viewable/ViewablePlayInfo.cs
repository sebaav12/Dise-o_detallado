using RawDealView.Formatters;
namespace RawDeal;

public class ViewablePlayInfo : IViewablePlayInfo
{
    public IViewableCardInfo CardInfo { get;}
    public String PlayedAs { get;}

    public ViewablePlayInfo(IViewableCardInfo cardInfo, string playedAs)
    {
        CardInfo = cardInfo;
        PlayedAs = playedAs;
    }
}