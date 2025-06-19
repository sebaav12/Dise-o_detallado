using RawDeal.Arguments;

namespace RawDeal;

public class CardKneeToTheGut : NoJockey.CardNoJockey
{
    public override bool CheckIsPossibleUseEffect(CheckReversalArgument argument)
    {
        ICard cardWhoTryPlayPlayerOne = argument.CardWhoTryPlayPlayerOne;
        string typeCardWhoTryPlayPlayerOne = argument.TypeCardWhoTryPlayPlayerOne;
        bool availableNextGrappleIsPlus4D = argument.AvailableNextGrappleIsPlus4D;
        
        return CheckForKneeToTheGut(
            cardWhoTryPlayPlayerOne, 
            typeCardWhoTryPlayPlayerOne, 
            availableNextGrappleIsPlus4D);
    }
    
    private bool CheckForKneeToTheGut(
        ICard cardWhoTryPlayPlayerOne, 
        string typeCardWhoTryPlayPlayerOne,
        bool availableNextGrappleIsPlus4D)
    {
        int damageOfCardWhoTryPlayPlayerOne = Int32.Parse(cardWhoTryPlayPlayerOne.Damage);

        return IsDamageWithinLimit(
            damageOfCardWhoTryPlayPlayerOne) && HasStrikeSubtype(cardWhoTryPlayPlayerOne);
    }

    private bool IsDamageWithinLimit(int damage)
    {
        return damage <= 7;
    }

    private bool HasStrikeSubtype(ICard card)
    {
        return card.Subtypes.Any(subtype => subtype == "Strike");
    }
    
}