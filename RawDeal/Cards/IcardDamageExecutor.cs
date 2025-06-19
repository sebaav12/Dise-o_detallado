using RawDeal.Arguments;
using RawDealView;
 
namespace RawDeal;

public class IcardDamageExecutor
{
    private View _view;
    
    public (string, int, bool) ExecuteDamage(
        ApplyDamageArgument applyDamageArgument, 
        (int damageNumber, bool isGrapple, int plusFotitude) args)
    {
        int damageNumber = args.damageNumber;
        bool isGrapple = args.isGrapple;
        int plusFotitude = args.plusFotitude;
        
        Player playerOne = applyDamageArgument.PlayerOne;
        Player playerTwo = applyDamageArgument.PlayerTwo;

        int positionInHandOfCardWhoPlayerOneTryPlay =
            applyDamageArgument.PositionInHandOfCardWhoPlayerOneTryPlay;
        string typeSelectedOfCardWhoPlayerOneTryPlay =
            applyDamageArgument.TypeSelectedOfCardWhoPlayerOneTryPlay;
        
        _view = applyDamageArgument.View;
        
        bool availableNextGrappleIsPlus4D = applyDamageArgument.AvailableNextGrappleIsPlus4D;
        bool availableNextGrapplesReversalIsPlus8F = 
            applyDamageArgument.AvailableNextGrapplesReversalIsPlus8F;
        
        ListCard cardsHandOfPlayerOne = playerOne.GetCardsHand();
        
        ICard cardWhoPlayerOneTryPlay = 
            cardsHandOfPlayerOne.GetCardById(positionInHandOfCardWhoPlayerOneTryPlay);
        
        List<string> listInfoCardInString = new List<string> { };
        int contador = 0;
        ListCard cardsArsenalOfPlayerTwo = playerTwo.GetCardsArsenal();
        int numberOfCardsInArsenalOfPlayerTwo = cardsArsenalOfPlayerTwo.Count();
        int startIndex = Math.Max(0, numberOfCardsInArsenalOfPlayerTwo - damageNumber);
        
        bool damageComplete = false;
        
        CardInfoService infoCardInString = new CardInfoService();
        
        for (int i = numberOfCardsInArsenalOfPlayerTwo - 1; i >= startIndex; i--)
        {
            ICard card = cardsArsenalOfPlayerTwo.GetCardById(i);
    
            if (contador <= damageNumber)
            {
                string info = infoCardInString.InfoCardInString(card);
                listInfoCardInString.Add(info);
                
                playerTwo.RemoveCardOfArsenal(i);
                
                CheckReversalCardArgument argument = new CheckReversalCardArgument()
                {
                    PlayerTwo = playerTwo,
                    CardOfPlayerTwo = card,
                    TypeSelectedOfCardWhoPlayerOneTryPlay = typeSelectedOfCardWhoPlayerOneTryPlay,
                    CardWhoTryPlayPlayerOne = cardWhoPlayerOneTryPlay,
                    AvailableNextGrapplesReversalIsPlus8F = availableNextGrapplesReversalIsPlus8F,
                    AvailableNextGrappleIsPlus4D = availableNextGrappleIsPlus4D
                };
    
                bool validReversal = CheckIfPlayerTwoCanUseThisCardHowReversal(argument);
    
                int fortitudeRating = playerTwo.GetFortitude();
                string fortitudeValueString = card.Fortitude;
                int fortitudeValue = Int32.Parse(fortitudeValueString);
                
                if (isGrapple == true && availableNextGrapplesReversalIsPlus8F == true)
                {
                    fortitudeValue = fortitudeValue + 8;
                }

                _view.ShowCardOverturnByTakingDamage(info, 
                    contador + 1, damageNumber);
    
                if (validReversal && fortitudeRating >= fortitudeValue)
                {
                    return IfIsValidReversal(
                        playerOne, playerTwo, 
                        (card, contador, damageNumber, plusFotitude, damageComplete));
                }

                playerTwo.AddCardToRingSide(card);
                
                contador++;
                
            }
        }

        return ("null", contador, damageComplete);
    }

    (string, int, bool) IfIsValidReversal(Player playerOne, Player playerTwo, 
        (ICard card, int contador, int damageNumber, int plusFotitude, bool damageComplete) args)
    {
        ICard card = args.card;
        int contador = args.contador;
        int damageNumber = args.damageNumber;
        int plusFotitude = args.plusFotitude;
        bool damageComplete = args.damageComplete;
        
        if (contador + 1 < damageNumber)
        {
            playerOne.AddFortitude(plusFotitude);
            _view.SayThatCardWasReversedByDeck(playerTwo.GetName());
            playerTwo.AddCardToRingSide(card);
                       
            return ("Revertir desde el mazo",  contador, damageComplete);
        }
        
        playerOne.AddFortitude(plusFotitude);
        _view.SayThatCardWasReversedByDeck(playerTwo.GetName());
        playerTwo.AddCardToRingSide(card);
        return (
            "Revertir desde el mazo post daño completo", 
             contador, damageComplete);
    } 
    
    private bool CheckIfPlayerTwoCanUseThisCardHowReversal(
        CheckReversalCardArgument argument)
    {
        Player playerTwo = argument.PlayerTwo;
        ICard cardOfPlayerTwo = argument.CardOfPlayerTwo;
        ICard cardWhoPlayerOneTryPlay = argument.CardWhoTryPlayPlayerOne;
        bool availableNextGrapplesReversalIsPlus8F = 
            argument.AvailableNextGrapplesReversalIsPlus8F;

        string typeSelectedOfCardWhoPlayerOneTryPlay =
            argument.TypeSelectedOfCardWhoPlayerOneTryPlay;
        bool availableNextGrappleIsPlus4D = argument.AvailableNextGrappleIsPlus4D;

        CheckReversalArgument checkIsPossibleUseEffectArgument = new CheckReversalArgument()
        {
            PlayerTwo = playerTwo,
            CardWhoTryPlayPlayerOne = cardWhoPlayerOneTryPlay,
            TypeCardWhoTryPlayPlayerOne = typeSelectedOfCardWhoPlayerOneTryPlay,
            AvailableNextGrapplesReversalIsPlus8F = availableNextGrapplesReversalIsPlus8F,
            AvailableNextGrappleIsPlus4D = availableNextGrappleIsPlus4D
        };
        
        string typeCardOfPlayerTwo = cardOfPlayerTwo.Types[0];

        if (typeCardOfPlayerTwo == "Reversal" && 
            cardOfPlayerTwo.CheckIsPossibleUseEffect(checkIsPossibleUseEffectArgument) &&
            playerTwo.CheckIfFortitudeIsBiggerThanReversalFortitude(
                cardOfPlayerTwo, cardWhoPlayerOneTryPlay, availableNextGrapplesReversalIsPlus8F))
        {
            return true;
        }
        
        return false;
    }
}