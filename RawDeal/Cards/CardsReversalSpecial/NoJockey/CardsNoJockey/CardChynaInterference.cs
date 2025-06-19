using RawDeal.Arguments;

namespace RawDeal;

public class CardChynaInterference : NoJockey.CardNoJockey
{
    public override bool CheckIsPossibleUseEffect(CheckReversalArgument argument)
    {
        ICard cardWhoTryPlayPlayerOne = argument.CardWhoTryPlayPlayerOne;
        string typeCardWhoTryPlayPlayerOne = argument.TypeCardWhoTryPlayPlayerOne;
        bool availableNextGrappleIsPlus4D = argument.AvailableNextGrappleIsPlus4D;
        
        return CheckForChynaInterference(
            cardWhoTryPlayPlayerOne, 
            typeCardWhoTryPlayPlayerOne, 
            availableNextGrappleIsPlus4D);
    }
    
    private bool CheckForChynaInterference(            
        ICard cardWhoTryPlayPlayerOne, 
        string typeCardWhoTryPlayPlayerOne,
        bool availableNextGrappleIsPlus4D)
    {
        if (typeCardWhoTryPlayPlayerOne == "MANEUVER")
        {
            return true;
        }
        return false;  
    }
}