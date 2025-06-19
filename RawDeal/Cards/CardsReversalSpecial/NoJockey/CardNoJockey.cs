namespace RawDeal.NoJockey;
using RawDeal.Arguments;
using RawDealView;
using RawDealView.Options;

public class CardNoJockey : SpecialReversalCard
{
    public override bool CheckIsPossibleUseEffect(CheckReversalArgument argument)
    {
        return false;
    }
    
    public override string ReverseEffectHand(
        ReverseEffectHandArgument argument)
    {
        return ReverseEffectHandNOJockeyingForPosition(argument);
    }
    
    private string ReverseEffectHandNOJockeyingForPosition(
        ReverseEffectHandArgument argument)
        {
            Player playerOne = argument.PlayerOne;
            int positionInHandOfCardWhoPlayerOneTryPlay =
                argument.PositionInHandOfCardWhoPlayerOneTryPlay;
            ICard instanceReversalCardWhoPlayerTwoTryPlay =
                argument.InstanceReversalCardWhoPlayerTwoTryPlay;
            Player playerTwo = argument.PlayerTwo;
            View _view = argument.View;

            string typeReversalWhoPlayerTwoTryPlay = this.Types[0];
            
            string cardToPlayString = 
                InfoCardInStringAlternativeFormat(
                    instanceReversalCardWhoPlayerTwoTryPlay,typeReversalWhoPlayerTwoTryPlay);
            
            string nombre = playerTwo.GetName();
            
            
            _view.SayThatPlayerReversedTheCard(nombre, cardToPlayString);
            
            ListCard handOfPlayerOne = playerOne.GetCardsHand();
            ICard cardWhoTryPlayPlayerOne= 
                handOfPlayerOne.GetCardById(positionInHandOfCardWhoPlayerOneTryPlay);
            
            playerOne.AddCardToRingSide(cardWhoTryPlayPlayerOne);

            ReverseEffectHandManagerInterferes(playerTwo, _view);

            ReverseEffectHandChynaInterferes(playerTwo, _view);
            
            string value = ApplyDamageReversal(argument);
            
            playerOne.RemoveCardOfHand(positionInHandOfCardWhoPlayerOneTryPlay);
            
            MoveReversalCardToRingArea(argument);

            ReverseEffectHandCleanBreak(playerTwo, playerOne, _view);
            
            var returnReverseEffectHandArgument = new ReturnReverseEffectHandArgument
            {
                Value = value,
                PlayerTwo = playerTwo,
                CardToPlayString = cardToPlayString,
                View = _view
            };
            return ReturnReverseEffectHand(returnReverseEffectHandArgument);
        }

    private void ReverseEffectHandManagerInterferes(
        Player playerTwo, View _view)
    {
        if (this.Title == "Manager Interferes")
        {
            playerTwo.takeOneCard();
            
            _view.SayThatPlayerDrawCards(playerTwo.GetName(), 1);
        }
    }
    
    private void ReverseEffectHandChynaInterferes(
        Player playerTwo, 
        View _view)
    {
        if (this.Title == "Chyna Interferes")
        {
            playerTwo.takeOneCard();
            playerTwo.takeOneCard();
            _view.SayThatPlayerDrawCards(playerTwo.GetName(), 2);
        }
    }
    
    private void ReverseEffectHandCleanBreak(
        Player playerTwo, 
        Player playerOne, 
        View _view)
    {
        if (!IsCleanBreak()) return;

        int numberOfCardsToDiscard = DetermineNumberOfCardsToDiscard(playerOne);

        DiscardCardsFromPlayerHand(numberOfCardsToDiscard, playerOne, _view);

        AllowPlayerToDrawCards(playerTwo, _view);
    }

    private bool IsCleanBreak()
    {
        return this.Title == "Clean Break";
    }

    private int DetermineNumberOfCardsToDiscard(Player playerOne)
    {
        int defaultNumberOfCardsToDiscard = 4;
        int numberOfCardsInHand = playerOne.NumberOfCardsInHand();
        return Math.Min(defaultNumberOfCardsToDiscard, numberOfCardsInHand);
    }

    private void DiscardCardsFromPlayerHand(
        int numberOfCardsToDiscard, 
        Player playerOne, 
        View _view)
    {
        for (int i = 0; i < numberOfCardsToDiscard; i++)
        {
            List<string> cardsHandInfo = GetInfoCardsInHand(playerOne);
            int idCardToDiscard = _view.AskPlayerToSelectACardToDiscard(
                cardsHandInfo, playerOne.GetName(), 
                playerOne.GetName(), numberOfCardsToDiscard - i);

            playerOne.moveOneCardOfHandToRingSide(idCardToDiscard);

            if (playerOne.NumberOfCardsInHand() == 0) break;
        }
    }

    private void AllowPlayerToDrawCards(Player playerTwo, View _view)
    {
        _view.SayThatPlayerDrawCards(playerTwo.GetName(), 1);
        playerTwo.takeOneCard();
    }

    public override string Effect(Player player, int cardPosition, View view)
    {
        ICard playedCard = RemoveCardFromPlayerHand(player, cardPosition);

        view.SayThatPlayerSuccessfullyPlayedACard();

        SelectedEffect selectedEffect = 
            view.AskUserToSelectAnEffectForJockeyForPosition(player.GetName());
        
        try
        {
            return GetEffectResult(selectedEffect);
        }
        
        catch (UnexpectedBehaviorException ex)
        {
            return "Error: " + ex.Message;
        }
    }

    private ICard RemoveCardFromPlayerHand(Player player, int cardPosition)
    {
        ListCard playerHandCards = player.GetCardsHand();
        ICard cardToPlay = playerHandCards.GetCardById(cardPosition);

        player.RemoveCardOfHand(cardPosition);
        player.AddCardToRingArea(cardToPlay);

        return cardToPlay;
    }

    private string GetEffectResult(SelectedEffect selectedEffect)
    {
        if (selectedEffect == SelectedEffect.NextGrappleIsPlus4D)
        {
            return "activateNextGrappleIsPlus4D";
        }
        else if (selectedEffect == SelectedEffect.NextGrapplesReversalIsPlus8F)
        {
            return "activateNextGrapplesReversalIsPlus8F";
        }

        throw new UnexpectedBehaviorException(
            "Comportamiento inesperado en GetEffectResult");
    }
}