using RawDeal.Arguments;

namespace RawDeal;

public class CardElbowToTheFace: NoJockey.CardNoJockey
{
    public override bool CheckIsPossibleUseEffect(CheckReversalArgument argument)
    {
        ICard cardWhoTryPlayPlayerOne = argument.CardWhoTryPlayPlayerOne;
        string typeCardWhoTryPlayPlayerOne = argument.TypeCardWhoTryPlayPlayerOne;
        bool availableNextGrappleIsPlus4D = argument.AvailableNextGrappleIsPlus4D;
        
        return CheckForElbowToTheFace(
            cardWhoTryPlayPlayerOne, 
            typeCardWhoTryPlayPlayerOne, 
            availableNextGrappleIsPlus4D);
    }
    
    private bool CheckForElbowToTheFace(
        ICard cardWhoTryPlayPlayerOne, 
        string typeCardWhoTryPlayPlayerOne,
        bool availableNextGrappleIsPlus4D)
    {
        if (IsActionCardForElbowToTheFace(typeCardWhoTryPlayPlayerOne))
        {
            return false;
        }

        if (IsManeuverCard(typeCardWhoTryPlayPlayerOne))
        {
            int damageOfCardWhoTryPlayPlayerOne = 
                Int32.Parse(cardWhoTryPlayPlayerOne.Damage);
            return IsDamageWithinLimitForElbowToTheFace(
                damageOfCardWhoTryPlayPlayerOne, availableNextGrappleIsPlus4D);
        }
    
        return false;
    }

    private bool IsActionCardForElbowToTheFace(string typeCardWhoTryPlayPlayerOne)
    {
        return this.Title == "Elbow to the Face" && typeCardWhoTryPlayPlayerOne == "ACTION";
    }

    private bool IsManeuverCard(string typeCardWhoTryPlayPlayerOne)
    {
        return typeCardWhoTryPlayPlayerOne == "MANEUVER";
    }

    private bool IsDamageWithinLimitForElbowToTheFace(
        int damage, bool availableNextGrappleIsPlus4D)
    {
        int effectiveDamage = availableNextGrappleIsPlus4D ? damage + 4 : damage;
        return effectiveDamage <= 7;
    }
}