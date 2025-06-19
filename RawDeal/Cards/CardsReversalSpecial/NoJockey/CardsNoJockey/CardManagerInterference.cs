using RawDeal.Arguments;
using RawDealView;

namespace RawDeal;

public class CardManagerInterference : NoJockey.CardNoJockey
{
    
    public override bool CheckIsPossibleUseEffect(CheckReversalArgument argument)
    {
        ICard cardWhoTryPlayPlayerOne = argument.CardWhoTryPlayPlayerOne;
        string typeCardWhoTryPlayPlayerOne = argument.TypeCardWhoTryPlayPlayerOne;
        bool availableNextGrappleIsPlus4D = argument.AvailableNextGrappleIsPlus4D;
        
        return CheckForManagerInterference(cardWhoTryPlayPlayerOne, typeCardWhoTryPlayPlayerOne, availableNextGrappleIsPlus4D);
    }
    
    private bool CheckForManagerInterference(
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