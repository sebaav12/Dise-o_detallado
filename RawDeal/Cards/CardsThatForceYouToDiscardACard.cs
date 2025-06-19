using RawDealView;

namespace RawDeal;

public class CardsThatForceYouToDiscardACard : ICard
{
    public override string Effect(
        Player player, int positionCardInHand, View _view)
    {
        ListCard cartasHand = player.GetCardsHand();
        ICard cartaToDiscard= cartasHand.GetCardById(positionCardInHand);
        string titleOfCard = cartaToDiscard.Title;
        
        player.RemoveCardOfHand(positionCardInHand);
        player.AddCardToRingSide(cartaToDiscard);
        player.takeOneCard();
        
        _view.SayThatPlayerSuccessfullyPlayedACard();
        _view.SayThatPlayerMustDiscardThisCard(player.GetName(), titleOfCard);
        _view.SayThatPlayerDrawCards(player.GetName(), 1);
        
        return " ";
    }
}