using RawDealView;

namespace RawDeal.EffectPart1;

public class CardHeadButt : CardsWithOtherEffects
{
    public override string Effect(
        Player player, int positionCardInHand, View _view)
    {
        ListCard cartasHand = player.GetCardsHand();
        
        int totalCardsToDiscard = 1;
        
        player.RemoveCardOfHand(positionCardInHand);
         
        List<string> handCardsInfo = GetInfoCardInHand(player, positionCardInHand);
        
        _view.SayThatPlayerSuccessfullyPlayedACard();
        
        int selectedCardIddescartar = _view.AskPlayerToSelectACardToDiscard(
            handCardsInfo, player.GetName(), player.GetName(), totalCardsToDiscard);
        
        ICard cartaToDiscard = cartasHand.GetCardById(selectedCardIddescartar);
        
        player.RemoveCardOfHand(selectedCardIddescartar);
        
        player.AddCardToRingSide(cartaToDiscard); 
        
        return " ";
    }
}