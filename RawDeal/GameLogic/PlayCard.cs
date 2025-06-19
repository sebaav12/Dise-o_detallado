using RawDeal.Arguments;
using RawDealView;
using RawDealView.Formatters;

namespace RawDeal;

public class PlayCard
{
    private View _view;
    private Dictionary<string, Func<string, ICard, Player, View, string>> actionMap;
    public PlayCard(View view)
    {
        _view = view;
        actionMap = new Dictionary<string, Func<string, ICard, Player, View, string>>
        {
            {"Gana Player One", SimpleReturn},
            {"Gana Player Two", SimpleReturn},
            {"Revertir desde el mazo", HandleReverseFromDeck},
            {"Revertir desde el mazo post daño completo", ReturnForCaseCompleteDamage},
            {"Revertir desde la mano", SimpleReturn},
            {"desactivateNextGrappleIsPlus4D", SimpleReturn},
            {"activateNextGrappleIsPlus4D", SimpleReturn},
            {"activateNextGrappleIsPlus4D y cambio de jugador", SimpleReturn},
            {"activateNextGrapplesReversalIsPlus8F y cambio de jugador", SimpleReturn},
            {"desactivateNextGrapplesReversalIsPlus8F", SimpleReturn},
            {"activateNextGrapplesReversalIsPlus8F", SimpleReturn}
        };
    }

    public string ExecutePlayCard(PlayCardArgument argument)
    {
        ICard cardIntance = argument.CardIntance;
        CardToPlayInfo infoCardWhoPlayerOneTryPlay = argument.InfoCardWhoPlayerOneTryPlay;
        string superStarNamePlayerOne = argument.PlayerOne.GetName();
        ICard cardWhoTryPlayPlayerOne = cardIntance;
        var infoByIdForPlayerOne = infoCardWhoPlayerOneTryPlay.GetInfoById(0);
        string typeCardWhoTryPlayPlayerOne = infoByIdForPlayerOne.typeOfCardUpper;
        SayThatPlayerIsTryingToPlayThisCard(cardWhoTryPlayPlayerOne, 
            typeCardWhoTryPlayPlayerOne, superStarNamePlayerOne);
        int positionInHandOfCardWhoPlayerOneTryPlay = 
            infoCardWhoPlayerOneTryPlay.GetInfoById(0).positionInHand;
        string typeOfCardWhoPlayerOneTryPlay = 
            infoCardWhoPlayerOneTryPlay.GetInfoById(0).typeOfCardUpper;
        ReversalInfo infoCheckOponnetWantPlayReversal =
            GetInfoCheckOponnetWantPlayReversal(argument, typeCardWhoTryPlayPlayerOne);
        bool playerTwoWantPlayReversal = 
            infoCheckOponnetWantPlayReversal.GetInfoById(0).playReversal;
        ICard instanceReversalCardWhoPlayerTwoTryPlay = 
            infoCheckOponnetWantPlayReversal.GetInfoById(0).cardReversalSelectedForPlayerTwo;

        var applyDamageArgument = new ApplyDamageArgument
        {
            PlayerOne = argument.PlayerOne,
            PositionInHandOfCardWhoPlayerOneTryPlay = positionInHandOfCardWhoPlayerOneTryPlay,
            TypeSelectedOfCardWhoPlayerOneTryPlay = typeOfCardWhoPlayerOneTryPlay,
            PlayerTwo = argument.PlayerTwo,
            View = _view,
            AvailableNextGrappleIsPlus4D = argument.AvailableNextGrappleIsPlus4D,
            AvailableNextGrapplesReversalIsPlus8F = argument.AvailableNextGrapplesReversalIsPlus8F
        };
        
        var reverseEffectHandArgument = new ReverseEffectHandArgument
        {
            PlayerOne = argument.PlayerOne,
            PositionInHandOfCardWhoPlayerOneTryPlay = positionInHandOfCardWhoPlayerOneTryPlay,
            TypeSelectedOfCardWhoPlayerOneTryPlay = typeOfCardWhoPlayerOneTryPlay,
            PlayerTwo = argument.PlayerTwo,
            InstanceReversalCardWhoPlayerTwoTryPlay = instanceReversalCardWhoPlayerTwoTryPlay,
            View = _view,
            AvailableNextGrappleIsPlus4D = argument.AvailableNextGrappleIsPlus4D,
            AvailableNextGrapplesReversalIsPlus8F = argument.AvailableNextGrapplesReversalIsPlus8F
        };
        
        string value;
        if (playerTwoWantPlayReversal)
        {
            value = PlayerTwoWantPlayReversal(
                instanceReversalCardWhoPlayerTwoTryPlay,
                reverseEffectHandArgument);
        }
        
        else
        {
            value = PlayerTwoDoesNotWantPlayReversal(argument, applyDamageArgument);
        }
        return ReturnPlayCard(value, cardIntance, argument.PlayerOne);
    }
    
    private ReversalInfo GetInfoCheckOponnetWantPlayReversal(
        PlayCardArgument argument, string typeCardWhoTryPlayPlayerOne)
    {
        CheckReversalArgument checkReversalArgument = new CheckReversalArgument()
        {
            PlayerTwo = argument.PlayerTwo,
            CardWhoTryPlayPlayerOne = argument.CardIntance,
            TypeCardWhoTryPlayPlayerOne = typeCardWhoTryPlayPlayerOne,
            AvailableNextGrapplesReversalIsPlus8F = argument.AvailableNextGrapplesReversalIsPlus8F,
            AvailableNextGrappleIsPlus4D = argument.AvailableNextGrappleIsPlus4D
        };

        Reversal checkOponnetWantPlayReversal = new Reversal(_view);

        ReversalInfo infoCheckOponnetWantPlayReversal = 
            checkOponnetWantPlayReversal.CheckOponnetWantPlayReversal(checkReversalArgument);

        return infoCheckOponnetWantPlayReversal;
    }
    
    private void SayThatPlayerIsTryingToPlayThisCard(
        ICard cardWhoTryPlayPlayerOne, 
        string typeCardWhoTryPlayPlayerOne,
        string superStarNamePlayerOne)
    {
        ViewablePlayInfo playInfo = new ViewablePlayInfo(
                new ViewableCardInfo(cardWhoTryPlayPlayerOne), typeCardWhoTryPlayPlayerOne);
        
        string cardToPlay = Formatter.PlayToString(playInfo);
        _view.SayThatPlayerIsTryingToPlayThisCard(superStarNamePlayerOne, cardToPlay);
    }

    private string PlayerTwoWantPlayReversal(
        ICard instanceReversalCardWhoPlayerTwoTryPlay, 
        ReverseEffectHandArgument reverseEffectHandArgument )
    {
        string value = instanceReversalCardWhoPlayerTwoTryPlay.
                ReverseEffectHand(reverseEffectHandArgument);
        return value;
    }

    private string PlayerTwoDoesNotWantPlayReversal(
        PlayCardArgument argument, 
        ApplyDamageArgument applyDamageArgument)
    {
        CardToPlayInfo infoCardWhoPlayerOneTryPlay = argument.InfoCardWhoPlayerOneTryPlay;
        ICard cardWhoTryPlayPlayerOne = argument.CardIntance;
        string typeCardWhoTryPlayPlayerOne = 
            infoCardWhoPlayerOneTryPlay.GetInfoById(0).typeOfCardUpper;
        
        string value;
        Player playerOne = applyDamageArgument.PlayerOne;
        int positionInHandOfCardWhoPlayerOneTryPlay =
            applyDamageArgument.PositionInHandOfCardWhoPlayerOneTryPlay;
        
        if (typeCardWhoTryPlayPlayerOne == "MANEUVER")
        {
            if (cardWhoTryPlayPlayerOne.Title == "Head Butt")
            {
                Player currentPlayer = argument.PlayerOne;
                int positionInHand = infoCardWhoPlayerOneTryPlay.GetInfoById(0).positionInHand;
                View currentView = _view;

                cardWhoTryPlayPlayerOne.Effect(currentPlayer, positionInHand, currentView);
                value = cardWhoTryPlayPlayerOne.ApplyDamage(applyDamageArgument);
                
            }
            else if (cardWhoTryPlayPlayerOne.Title == "Arm Bar")
            {
                Player currentPlayer = argument.PlayerOne;
                int positionInHand = infoCardWhoPlayerOneTryPlay.GetInfoById(0).positionInHand;
                View currentView = _view;
                
                cardWhoTryPlayPlayerOne.Effect(currentPlayer, positionInHand, currentView);
                 
                value = cardWhoTryPlayPlayerOne.ApplyDamage(applyDamageArgument);
            }
            else
            {
                _view.SayThatPlayerSuccessfullyPlayedACard();
                value = cardWhoTryPlayPlayerOne.ApplyDamage(applyDamageArgument);
                ListCard cardsHandOfPlayerOne = playerOne.GetCardsHand();
                ICard cardWhoPlayerOneTryPlay = 
                    cardsHandOfPlayerOne.GetCardById(positionInHandOfCardWhoPlayerOneTryPlay);
                playerOne.RemoveCardOfHand(positionInHandOfCardWhoPlayerOneTryPlay);
                playerOne.AddCardToRingArea(cardWhoPlayerOneTryPlay);
            }
             
        }
        else if (typeCardWhoTryPlayPlayerOne == "ACTION")
        {
            Player currentPlayer = argument.PlayerOne;
            int positionInHand = infoCardWhoPlayerOneTryPlay.GetInfoById(0).positionInHand;
            View currentView = _view;
            value = cardWhoTryPlayPlayerOne.Effect(currentPlayer, positionInHand, currentView);
        }
        else
        {
            value = cardWhoTryPlayPlayerOne.ApplyDamage(applyDamageArgument);
        }
        return value;
    }
    
    private string ReturnPlayCard(string value, ICard cardInstance, Player playerOne)
    {
        if (string.IsNullOrEmpty(value))
        {
            return "Acción no definida o valor vacío.";
        }
        if (actionMap.TryGetValue(value, out Func<string, ICard, Player, View, string> action))
        {
            return action(value, cardInstance, playerOne, _view); }
        return "Acción no encontrada para el valor dado.";
    }
    
    private string SimpleReturn(string value, ICard cardInstance, Player player, View view)
    {
        return value;
    }

    private string ReturnForCaseCompleteDamage(
        string value, ICard cardInstance, Player player, View view)
    {
        return "Revertir desde el mazo";
    }
    
    private string HandleReverseFromDeck(
        string value, ICard cardInstance, Player player, View view)
    {
        string stunValueString = cardInstance.StunValue;
        int stunValue = int.Parse(stunValueString);
        if (stunValue > 0)
        {
            int numberCards = view.AskHowManyCardsToDrawBecauseOfStunValue(
                player.GetName(), stunValue);
            if (numberCards >= 0)
            {
                view.SayThatPlayerDrawCards(player.GetName(), numberCards);
            }
            for (int i = 0; i < numberCards; i++)
            {
                player.takeOneCard();
            }
        }
        return "Revertir desde el mazo";
    }
}