using RawDealView;
namespace RawDeal.data;

public class ChrisJericho : SuperStar
{
    public void Hability(Player playerOne, Player playerTwo, View _view)
    {
        int numCardsInHand = playerOne.NumberOfCardsInHand();
        if (numCardsInHand >= 1)
        {
            AnnouncePlayerAbility(playerOne, _view);
            HandleCardDiscardForPlayer(playerOne, _view);
            HandleCardDiscardForPlayer(playerTwo, _view);
        }
    }

    private void AnnouncePlayerAbility(Player player, View _view)
    {
        SuperStar superStar = player.CardSuperStar();
        string ability = superStar.SuperstarAbility;
        _view.SayThatPlayerIsGoingToUseHisAbility(player.GetName(), ability);
    }

    private void HandleCardDiscardForPlayer(Player player, View _view)
    {
        List<string> cardsHandInfo = GetInfoCardsInHand(player);
        int totalCardsToDiscard = 1;
        int idCartaDescartada = 
            _view.AskPlayerToSelectACardToDiscard(
                cardsHandInfo, player.GetName(), player.GetName(), totalCardsToDiscard);
        player.moveOneCardOfHandToRingSide(idCartaDescartada);
    }

}