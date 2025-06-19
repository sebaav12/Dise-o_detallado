using RawDeal.Arguments;
using RawDealView;
using RawDealView.Formatters;

namespace RawDeal;

public class Reversal
{
    private View _view;
    
    public Reversal(View view)
    {
        _view = view;
    }
    
    public ReversalInfo CheckOponnetWantPlayReversal(CheckReversalArgument argument)
    {
        Player playerTwo = argument.PlayerTwo;
        ICard cardWhoTryPlayPlayerOne = argument.CardWhoTryPlayPlayerOne;
        string typeCardWhoTryPlayPlayerOne = argument.TypeCardWhoTryPlayPlayerOne;
        bool availableNextGrapplesReversalIsPlus8F = 
            argument.AvailableNextGrapplesReversalIsPlus8F;
        bool availableNextGrappleIsPlus4D = argument.AvailableNextGrappleIsPlus4D;
        
        ReversalInfo reversalInfo = new ReversalInfo();
        
        bool playReversal = false;
        
        CheckReversalArgument checkReversalArgument = new CheckReversalArgument()
        {
            PlayerTwo = playerTwo,
            CardWhoTryPlayPlayerOne = cardWhoTryPlayPlayerOne,
            TypeCardWhoTryPlayPlayerOne = typeCardWhoTryPlayPlayerOne,
            AvailableNextGrapplesReversalIsPlus8F = availableNextGrapplesReversalIsPlus8F,
            AvailableNextGrappleIsPlus4D = availableNextGrappleIsPlus4D
        };
        
        bool checkIfPlayerCanUseReversal = CheckIfPlayerCanUseReversal(checkReversalArgument);
        
        if (checkIfPlayerCanUseReversal)
        {
            
            ICard selectedCard = SelectOneReversalCard(checkReversalArgument);
    
            if (selectedCard != null)
            {
                playReversal = true;
                reversalInfo.Add(playReversal, selectedCard);
            }
            else
            {
                reversalInfo.Add(playReversal, null); 
            }
    
            return reversalInfo;
        }
        
        reversalInfo.Add(playReversal, null);
        return reversalInfo;
    }
    
    private bool CheckIfPlayerCanUseReversal(CheckReversalArgument argument)
    {
        Player playerTwo = argument.PlayerTwo;
        ICard cardWhoTryPlayPlayerOne = argument.CardWhoTryPlayPlayerOne;
        bool availableNextGrapplesReversalIsPlus8F = argument.AvailableNextGrapplesReversalIsPlus8F;

        if (playerTwo.CheckPlayerHaveReversal())
        {
            ListCard reversalCards = playerTwo.GetReversalCards();
            foreach (var card in reversalCards)
            {
                if (card.CheckIsPossibleUseEffect(argument))
                {
                    if (playerTwo.CheckIfFortitudeIsBiggerThanReversalFortitude(
                            card,cardWhoTryPlayPlayerOne, availableNextGrapplesReversalIsPlus8F))
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }
    
    private ICard SelectOneReversalCard(CheckReversalArgument argument)
    {
        Player playerTwo = argument.PlayerTwo;
        
        ListCard reversalCards = GetUsableReversalCards(argument);
            
        List<string> reversalCardsStringFormat = new List<string>();

        foreach (var card in reversalCards)
        {
            if (card.Title == "Jockeying for Position")
            {
                string tyOfCardUpper = card.Types[1].ToUpper();
                ViewablePlayInfo playInfo = 
                    new ViewablePlayInfo(InfoCardInViewableClass(card), tyOfCardUpper);
                string cardToPlay = Formatter.PlayToString(playInfo);
                reversalCardsStringFormat.Add(cardToPlay);
            }
            else
            {
                string tyOfCardUpper = card.Types[0].ToUpper();
                ViewablePlayInfo playInfo = 
                    new ViewablePlayInfo(InfoCardInViewableClass(card), tyOfCardUpper);
                string cardToPlay = Formatter.PlayToString(playInfo);
                reversalCardsStringFormat.Add(cardToPlay);
            }
        }
        
        int positionCardSelected = 
            _view.AskUserToSelectAReversal(playerTwo.GetName(), reversalCardsStringFormat);

        if (positionCardSelected != -1)
        {
            ICard cardSelected = reversalCards.GetCardById(positionCardSelected);

            return cardSelected;
        }

        return null;
    }
    
    private ListCard GetUsableReversalCards(CheckReversalArgument argument)
    {
        Player playerTwo = argument.PlayerTwo;
        ICard cardWhoTryPlayPlayerOne = argument.CardWhoTryPlayPlayerOne;
        bool availableNextGrapplesReversalIsPlus8F = argument.AvailableNextGrapplesReversalIsPlus8F;

        ListCard cards = new ListCard();
        if (playerTwo.CheckPlayerHaveReversal()) {
            ListCard reversalCards = playerTwo.GetReversalCards();
            foreach (var card in reversalCards) {
                bool canUseEffect = card.CheckIsPossibleUseEffect(argument);
                bool isFortitudeBigger = playerTwo.CheckIfFortitudeIsBiggerThanReversalFortitude(
                    card, cardWhoTryPlayPlayerOne, availableNextGrapplesReversalIsPlus8F);
                if (canUseEffect && isFortitudeBigger) {
                    cards.AddCard(card);
                }
            }
             
        }
        return cards;
    }

    
    private ViewableCardInfo InfoCardInViewableClass(ICard card)
    {
        ViewableCardInfo cardInfo = new ViewableCardInfo(card);
        return cardInfo;
    }
}