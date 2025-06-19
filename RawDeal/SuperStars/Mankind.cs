using RawDealView;
namespace RawDeal.data;

public class Mankind : SuperStar
{
    public void Hability(Player player, View _view)
    {
        if (player.NumberOfCardInArsenal() >= 2)
        {
            player.takeOneCard();
            player.takeOneCard();
        }
        else
        {
            player.takeOneCard();
        }
    }
}