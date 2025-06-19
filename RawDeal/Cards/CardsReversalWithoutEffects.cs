using RawDeal.Arguments;
using RawDealView;

namespace RawDeal;

public class CardsReversalWithoutEffects : ICard
{
    public override string ReverseEffectHand(ReverseEffectHandArgument argument)
    {
        ICard instanceReversalCardWhoPlayerTwoTryPlay =
            argument.InstanceReversalCardWhoPlayerTwoTryPlay;
        Player playerTwo = argument.PlayerTwo;
        View _view = argument.View;
        
        RevertCard(argument);

        string value = ApplyDamageReversal(argument);
        
        string typeReversalWhoPlayerTwoTryPlay = 
            instanceReversalCardWhoPlayerTwoTryPlay.Types[0];
        
        if (instanceReversalCardWhoPlayerTwoTryPlay.Title == 
            "Jockeying for Position")
        {
            typeReversalWhoPlayerTwoTryPlay = 
                instanceReversalCardWhoPlayerTwoTryPlay.Types[1];
        }
        
        string cardToPlay = InfoPlayToString(
            instanceReversalCardWhoPlayerTwoTryPlay, typeReversalWhoPlayerTwoTryPlay);
        
        MoveReversalCardToRingArea(argument);
        
        var returnReverseEffectHandArgument = new ReturnReverseEffectHandArgument
        {
            Value = value,
            PlayerTwo = playerTwo,
            CardToPlayString = cardToPlay,
            View = _view
        };

        try
        {
            return ReturnReverseEffectHand(returnReverseEffectHandArgument);
        }
        catch(UnexpectedBehaviorException ex)
        {
            return "Error: " + ex.Message;
        }
    }
    
    public void RevertCard(ReverseEffectHandArgument argument)
    {
        
        Player playerOne = argument.PlayerOne;
        int positionInHandOfCardWhoPlayerOneTryPlay =
            argument.PositionInHandOfCardWhoPlayerOneTryPlay;
        
        ListCard handOfPlayerOne = playerOne.GetCardsHand();
        ICard cardWhoTryPlayPlayerOne= 
            handOfPlayerOne.GetCardById(positionInHandOfCardWhoPlayerOneTryPlay);
        
        playerOne.RemoveCardOfHand(positionInHandOfCardWhoPlayerOneTryPlay);
        playerOne.AddCardToRingSide(cardWhoTryPlayPlayerOne);
    }
    
    private string ApplyDamageReversal(ReverseEffectHandArgument argument)
    {
        Player playerOne = argument.PlayerOne;
        Player playerTwo = argument.PlayerTwo;
        View _view = argument.View;

        int damageNumber = GetDamageNumber(playerOne);
        int plusFortitude = damageNumber;

        if (playerOne.GetName() == "MANKIND" && damageNumber >= 1)
        {
            damageNumber--;
        }

        var showCardsOverturnByTakingDamageArgument = 
            new ShowCardsOverturnByTakingDamageArgument
        {
            CardsInfo = GetCardsInfoToMoveToRingSide(playerOne, damageNumber),
            DamageNumber = damageNumber,
            View = _view,
            PlayerOne = playerOne
        };
        ShowCardsOverturnByTakingDamage(showCardsOverturnByTakingDamageArgument);
        

        if (playerOne.NumberOfCardInArsenal() < damageNumber)
        {
            _view.CongratulateWinner(playerTwo.GetName());
            return "Gana Player Two";
        }

        playerOne.RemoveManyCardsArsenal(damageNumber);
        playerTwo.AddFortitude(plusFortitude);

        return "Revertir desde la mano";
    }

    private int GetDamageNumber(Player playerOne)
    {
        string damageDone = Damage;
        int damageNumber = int.Parse(damageDone);

        if (playerOne.GetName() == "MANKIND" && damageNumber >= 1)
        {
            damageNumber--;
        }

        return damageNumber;
    }

    private List<string> GetCardsInfoToMoveToRingSide(Player playerOne, int damageNumber)
    {
        List<string> cardsInfo = new List<string>();

        ListCard cardsArsenal = playerOne.GetCardsArsenal();
        int totalCards = cardsArsenal.Count();
        int startIndex = Math.Max(0, totalCards - damageNumber);

        for (int i = totalCards - 1; i >= startIndex; i--)
        {
            ICard card = cardsArsenal.GetCardById(i);

            if (cardsInfo.Count < damageNumber)
            {
                CardInfoService infoCardInString = new CardInfoService();
                string info = infoCardInString.InfoCardInString(card);
                
                cardsInfo.Add(info);
                playerOne.AddCardToRingSide(card);
            }
            else
            {
                break;
            }
        }

        return cardsInfo;
    }

    private void ShowCardsOverturnByTakingDamage(
        ShowCardsOverturnByTakingDamageArgument argument)
    {
        List<string> cardsInfo = argument.CardsInfo;
        int damageNumber = argument.DamageNumber;
        View _view = argument.View;
        Player playerOne = argument.PlayerOne;
        
        int currentDamage = 1;
        foreach (var card in cardsInfo)
        {
            _view.ShowCardOverturnByTakingDamage(card, currentDamage, damageNumber);
            currentDamage++;
        }

        if (damageNumber > 0)
        {
            _view.SayThatSuperstarWillTakeSomeDamage(playerOne.GetName(), damageNumber);
        }
    }
    
    public void MoveReversalCardToRingArea(ReverseEffectHandArgument argument)
    {
        ICard instanceReversalCardWhoPlayerTwoTryPlay =
            argument.InstanceReversalCardWhoPlayerTwoTryPlay;
        Player playerTwo = argument.PlayerTwo;

        int positionReversalInHand = 
            playerTwo.GetCardsHand().IdOfCard(instanceReversalCardWhoPlayerTwoTryPlay);
        
        playerTwo.RemoveCardOfHand(positionReversalInHand);
        playerTwo.AddCardToRingArea(instanceReversalCardWhoPlayerTwoTryPlay);
    }

    public override bool CheckIsPossibleUseEffect(CheckReversalArgument argument)
    {
        ICard cardWhoTryPlayPlayerOne = argument.CardWhoTryPlayPlayerOne;
        string typeCardWhoTryPlayPlayerOne = argument.TypeCardWhoTryPlayPlayerOne;
        
 
        if (this.Title == "Step Aside" && typeCardWhoTryPlayPlayerOne == "MANEUVER")
        {
            return cardWhoTryPlayPlayerOne.Subtypes.Contains("Strike");
        }

        if (this.Title == "Escape Move" && typeCardWhoTryPlayPlayerOne == "MANEUVER")
        {
            return cardWhoTryPlayPlayerOne.Subtypes.Contains("Grapple");
        }        
     
        if (this.Title == "Break the Hold" && typeCardWhoTryPlayPlayerOne == "MANEUVER")
        {
            return cardWhoTryPlayPlayerOne.Subtypes.Contains("Submission");
        }
    
        if (this.Title == "No Chance in Hell" && typeCardWhoTryPlayPlayerOne == "ACTION")
        {
            return true;
        }
    
        return false;
    }
    
    private string ReturnReverseEffectHand(ReturnReverseEffectHandArgument argument)
    {
        string value = argument.Value;
        Player playerTwo = argument.PlayerTwo;
        string cardToPlay = argument.CardToPlayString;
        View _view = argument.View;
            
        if (value == "Gana Player One")
        {
            return "Gana Player One";
        }
        
        if (value == "Gana Player Two")
        {
            return "Gana Player Two";
        }
        
        if (value == "Revertir desde la mano")
        {
            string nombre = playerTwo.GetName();
            _view.SayThatPlayerReversedTheCard(nombre, cardToPlay);
            return "Revertir desde la mano";
        }
        throw new UnexpectedBehaviorException(
            "Comportamiento inesperado en ReturnReverseEffectHand");
    } 
}