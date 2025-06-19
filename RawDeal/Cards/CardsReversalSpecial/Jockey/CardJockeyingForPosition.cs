using RawDeal.Arguments;
using RawDealView;
using RawDealView.Options;

namespace RawDeal;

public class CardJockeyingForPosition : SpecialReversalCard
{
    public override bool CheckIsPossibleUseEffect(CheckReversalArgument argument)
    {
        ICard cardWhoTryPlayPlayerOne = argument.CardWhoTryPlayPlayerOne;
        string typeCardWhoTryPlayPlayerOne = argument.TypeCardWhoTryPlayPlayerOne;
        bool availableNextGrappleIsPlus4D = argument.AvailableNextGrappleIsPlus4D;
        return CheckForJockeyingForPosition(
            cardWhoTryPlayPlayerOne, 
            typeCardWhoTryPlayPlayerOne, 
            availableNextGrappleIsPlus4D);
    }
    
    private bool CheckForJockeyingForPosition(
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

    public override string ReverseEffectHand(
        ReverseEffectHandArgument argument)
    {
        try
        {
            return ReverseEffectHandJockeyingForPosition(argument);
        }
        catch (UnexpectedBehaviorException ex)
        {
            return "Error: " + ex.Message;
        }
    }

    private string ReverseEffectHandJockeyingForPosition(
        ReverseEffectHandArgument argument)
    {
        ICard instanceReversalCardWhoPlayerTwoTryPlay =
            argument.InstanceReversalCardWhoPlayerTwoTryPlay;
        Player playerTwo = argument.PlayerTwo;
        View _view = argument.View;
        
        string typeReversalWhoPlayerTwoTryPlay = instanceReversalCardWhoPlayerTwoTryPlay.Types[1];
            
        string value = ApplyDamageReversal(argument);
        
        RevertCard(argument);
            
        string cardToPlayString = 
            InfoCardInStringAlternativeFormat(
                instanceReversalCardWhoPlayerTwoTryPlay,typeReversalWhoPlayerTwoTryPlay);

        _view.SayThatPlayerReversedTheCard(playerTwo.GetName(), cardToPlayString);
            
        SelectedEffect selectedEffect = 
            _view.AskUserToSelectAnEffectForJockeyForPosition(playerTwo.GetName());
            
        MoveReversalCardToRingArea(argument);

        return ReturnReverseEffectHandJockeyingForPosition(selectedEffect, value);
    }

    private string ReturnReverseEffectHandJockeyingForPosition(
        SelectedEffect selectedEffect, string value)
    {
        if (selectedEffect == SelectedEffect.NextGrappleIsPlus4D)
        {
            return "activateNextGrappleIsPlus4D y cambio de jugador";
        }
        if (selectedEffect == SelectedEffect.NextGrapplesReversalIsPlus8F)
        {
            return "activateNextGrapplesReversalIsPlus8F y cambio de jugador";
        }

        if (value == "Gana Gana Player Two")
        {
            return "Gana Player Two";
        }

        throw new UnexpectedBehaviorException(
            "Comportamiento inesperado en ReturnReverseEffectHandJockeyingForPosition");
    }
}