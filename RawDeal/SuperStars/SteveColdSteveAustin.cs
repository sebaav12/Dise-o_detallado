using RawDealView;
namespace RawDeal.data;
using RawDealView.Formatters;

public class SteveColdSteveAustin : SuperStar
{
    public void Hability(Player playerOne, View view)
    {
        if (IsArsenalNonEmpty(playerOne))
        {
            PerformHabilityActions(playerOne, view);
        }
    }

    private bool IsArsenalNonEmpty(Player player)
    {
        return player.NumberOfCardInArsenal() >= 1;
    }

    private void PerformHabilityActions(Player playerOne, View view)
    {
        DrawOneCard(playerOne);
        AnnounceHabilityAndDraw(playerOne, view);
        List<string> handCardsInfo = PrepareHandCardsInfo(playerOne);
        int selectedCardId = AskPlayerToReturnCard(handCardsInfo, playerOne, view);
        ReturnSelectedCardToArsenal(playerOne, selectedCardId);
    }

    private void DrawOneCard(Player player)
    {
        player.takeOneCard();
    }

    private void AnnounceHabilityAndDraw(Player player, View view)
    {
        SuperStar superStar = player.CardSuperStar();
        string ability = superStar.SuperstarAbility;
        view.SayThatPlayerIsGoingToUseHisAbility(
            player.GetName(), ability);
        view.SayThatPlayerDrawCards(player.GetName(), 1);
    }

    private List<string> PrepareHandCardsInfo(Player player)
    {
        List<string> handCardsInfo = new List<string>();
        foreach (ICard card in player.GetCardsHand())
        {
            ViewableCardInfo cardInfo = CreateViewableCardInfo(card);
            string cardStr = Formatter.CardToString(cardInfo);
            handCardsInfo.Add(cardStr);
        }
        return handCardsInfo;
    }

    private ViewableCardInfo CreateViewableCardInfo(ICard card)
    {
        return new ViewableCardInfo(
            card
        );
    }

    private int AskPlayerToReturnCard(List<string> handCardsInfo, Player player, View view)
    {
        return view.AskPlayerToReturnOneCardFromHisHandToHisArsenal(
            player.GetName(), handCardsInfo);
    }

    private void ReturnSelectedCardToArsenal(Player player, int selectedCardId)
    {
        player.MoveSelectedCardOfHandToArsenal(selectedCardId);
    }

}