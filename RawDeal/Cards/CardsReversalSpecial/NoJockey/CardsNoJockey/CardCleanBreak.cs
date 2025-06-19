using RawDeal.Arguments;

namespace RawDeal;

public class CardCleanBreak: NoJockey.CardNoJockey
{
    public override bool CheckIsPossibleUseEffect(CheckReversalArgument argument)
    {
        ICard cardWhoTryPlayPlayerOne = argument.CardWhoTryPlayPlayerOne;
        string typeCardWhoTryPlayPlayerOne = argument.TypeCardWhoTryPlayPlayerOne;
        bool availableNextGrappleIsPlus4D = argument.AvailableNextGrappleIsPlus4D;
        
        return CheckForCleanBreak(
            cardWhoTryPlayPlayerOne, 
            typeCardWhoTryPlayPlayerOne, 
            availableNextGrappleIsPlus4D);
    }
    
    private bool CheckForCleanBreak(         
        ICard cardWhoTryPlayPlayerOne, 
        string typeCardWhoTryPlayPlayerOne,
        bool availableNextGrappleIsPlus4D)
    {
        if (cardWhoTryPlayPlayerOne.Title == "Jockeying for Position")
        {
            return true;
        }
        return false;  
    }
}