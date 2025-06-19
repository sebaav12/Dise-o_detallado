using RawDeal.Arguments;

namespace RawDeal;

public class CardRollingTakedown : NoJockey.CardNoJockey
{
    public override bool CheckIsPossibleUseEffect(CheckReversalArgument argument)
    {
        ICard cardWhoTryPlayPlayerOne = argument.CardWhoTryPlayPlayerOne;
        string typeCardWhoTryPlayPlayerOne = argument.TypeCardWhoTryPlayPlayerOne;
        bool availableNextGrappleIsPlus4D = argument.AvailableNextGrappleIsPlus4D;
        return CheckForRollingTakedown(
            cardWhoTryPlayPlayerOne, 
            typeCardWhoTryPlayPlayerOne, 
            availableNextGrappleIsPlus4D);
    }
    
    private bool CheckForRollingTakedown(
        ICard cardWhoTryPlayPlayerOne, 
        string typeCardWhoTryPlayPlayerOne,
        bool availableNextGrappleIsPlus4D)
    {
        if (IsActionCardForRollingTakedown(typeCardWhoTryPlayPlayerOne))
        {
            return false;
        }

        int damageOfCardWhoTryPlayPlayerOne = Int32.Parse(cardWhoTryPlayPlayerOne.Damage);
        int effectiveDamage = 
            availableNextGrappleIsPlus4D ? damageOfCardWhoTryPlayPlayerOne + 4 : 
                damageOfCardWhoTryPlayPlayerOne;

        if (effectiveDamage <= 7 && HasGrappleSubtype(cardWhoTryPlayPlayerOne))
        {
            return true;
        }
    
        return false;
    }

    private bool IsActionCardForRollingTakedown(string typeCardWhoTryPlayPlayerOne)
    {
        return this.Title == "Rolling Takedown" && typeCardWhoTryPlayPlayerOne == "ACTION";
    }

    private bool HasGrappleSubtype(ICard card)
    {
        return card.Subtypes.Any(subtype => subtype == "Grapple");
    }
    
}