using RawDealView;
namespace RawDeal.data;

public class TheUnderTaker : SuperStar
{
    public void Hability(Player player, View view)
    {
        if (IsHandNonEmpty(player, 2))
        {
            PerformHabilityActions(player, view);
        }
    }

    private bool IsHandNonEmpty(Player player, int minCardCount)
    {
        return player.NumberOfCardsInHand() >= minCardCount;
    }

    private void PerformHabilityActions(Player player, View view)
    {
        AnnounceHability(player, view);
        DiscardCardsFromHand(player, view, 2);
        DiscardCardsFromHand(player, view, 1);
        MoveCardFromRingSideToHand(player, view);
    }

    private void AnnounceHability(Player player, View view)
    {
        SuperStar superStar = player.CardSuperStar();
        string ability = superStar.SuperstarAbility;
        view.SayThatPlayerIsGoingToUseHisAbility(
            player.GetName(), ability);
    }

    private void DiscardCardsFromHand(Player player, View view, int totalCardsToDiscard)
    {
        List<string> handCardsInfo = GetInfoCardsInHand(player);
        int selectedCardId = view.AskPlayerToSelectACardToDiscard(
            handCardsInfo, player.GetName(), player.GetName(), totalCardsToDiscard);
        player.moveOneCardOfHandToRingSide(selectedCardId);
    }
    
    private void MoveCardFromRingSideToHand(Player player, View view)
    {
        List<string> ringSideCardsInfo = GetInfoCardsInRingSide(player);
        int selectedCardId = view.AskPlayerToSelectCardsToPutInHisHand(
            "THE UNDERTAKER", 1, ringSideCardsInfo);
        player.moveOneCardRingSideToHand(selectedCardId);
    }
}