using RawDeal.Arguments;
using RawDealView;
using RawDealView.Formatters;
using RawDealView.Options;
namespace RawDeal;

public class SpecialReversalCard : CardsReversalWithoutEffects
{
    public override bool CheckIsPossibleUseEffect(CheckReversalArgument argument)
    {
        return false;
    }
    
    public string ApplyDamageReversal(ReverseEffectHandArgument argument)
    {
        Player playerOne = argument.PlayerOne;
        int positionInHandOfCardWhoPlayerOneTryPlay =
            argument.PositionInHandOfCardWhoPlayerOneTryPlay;
        Player playerTwo = argument.PlayerTwo;
        View _view = argument.View;
        bool availableNextGrappleIsPlus4D = argument.AvailableNextGrappleIsPlus4D;
        var calculateDamageArgument = new CalculateDamageArgument
        {
            PlayerOne = playerOne,
            PlayerTwo = playerTwo,
            PositionInHandOfCardWhoPlayerOneTryPlay = positionInHandOfCardWhoPlayerOneTryPlay,
            AvailableNextGrappleIsPlus4D = availableNextGrappleIsPlus4D
        };
        Tuple<int, int> result = CalculateDamage(calculateDamageArgument);
        int damageNumber = result.Item1;
        int plus = result.Item2;
        List<string> cardsinfo = ProcessCards(playerOne, damageNumber);
        if (damageNumber > 0)
        {
            _view.SayThatSuperstarWillTakeSomeDamage(playerOne.GetName(), damageNumber);
        }
        int currentDamage = 1;
        foreach (var card in cardsinfo)
        {
            var carta = new List<string>();
            carta.Add(card);
            _view.ShowCardOverturnByTakingDamage(card, currentDamage, damageNumber);
            currentDamage++;
        }
        if (playerOne.NumberOfCardInArsenal() < damageNumber)
        {
            _view.CongratulateWinner(playerTwo.GetName());
            return "Gana Player Two";
        }
        if (playerOne.NumberOfCardInArsenal() >= damageNumber)
        {
            playerOne.RemoveManyCardsArsenal(damageNumber);
            FortitudeInReversalSpecial(playerTwo, plus);
        }
        return "Revertir desde la mano";
    }
    
    Tuple<int, int> CalculateDamage(CalculateDamageArgument argument)
    {
        Player playerOne = argument.PlayerOne;
        Player playerTwo = argument.PlayerTwo;
        int positionInHandOfCardWhoPlayerOneTryPlay =
            argument.PositionInHandOfCardWhoPlayerOneTryPlay;
        bool availableNextGrappleIsPlus4D = argument.AvailableNextGrappleIsPlus4D;
        int damageNumber = GetDamage(playerOne, positionInHandOfCardWhoPlayerOneTryPlay);
        if (availableNextGrappleIsPlus4D)
        {
            if (this.Title != "Manager Interferes")
            {
                damageNumber = damageNumber + 4;
            }
        }
        string valor = "";
        int plus = 0;
        int plusFortitude = 0;

        if (this.Title == "Rolling Takedown")
        {
            plus = damageNumber;
            plusFortitude = damageNumber;
        }
        else if (this.Title == "Knee to the Gut")
        {
            plus = damageNumber;
            plusFortitude = damageNumber;
        }
        else
        {
            valor = this.Damage;
            plus = int.Parse(valor);
            plusFortitude = damageNumber;
        }
        if (playerTwo.GetName() == "MANKIND" && this.Title == "Rolling Takedown")
        {
            damageNumber = damageNumber - 1;
        }
        if (playerTwo.GetName() == "MANKIND" && this.Title == "Knee to the Gut")
        {
            damageNumber = damageNumber - 1;
        }
        if (playerOne.GetName() == "MANKIND" && this.Title == "Knee to the Gut")
        {
            damageNumber = damageNumber - 1;
        }
    
        if (playerOne.GetName() == "MANKIND" && this.Title == "Rolling Takedown")
        {
            damageNumber = damageNumber - 1;
        }
        return Tuple.Create(damageNumber, plus);
    }
    
    List<string> ProcessCards(Player playerOne, int damageNumber)
    {
        List<string> cardsinfo = new List<string>();
        int contador = 0;
        ListCard cardsArsenal = playerOne.GetCardsArsenal();
        int totalCards = cardsArsenal.Count();
        int startIndex = Math.Max(0, totalCards - damageNumber);
        for (int i = totalCards - 1; i >= startIndex; i--)
        {
            ICard card = cardsArsenal.GetCardById(i);

            if (contador < damageNumber)
            {
                CardInfoService infoCardInString = new CardInfoService();
                string info = infoCardInString.InfoCardInString(card);
                cardsinfo.Add(info);
                playerOne.AddCardToRingSide(card);
                contador++;
            }
            else
            { break; }
        }
        return cardsinfo;
    }
    
    private int GetDamage(
        Player playerOne, int positionInHandOfCardWhoPlayerOneTryPlay)
    {
        ListCard cardsHandOfPlayerOne = playerOne.GetCardsHand();
        ICard cardWhoPlayerOneTryPlay = 
            cardsHandOfPlayerOne.GetCardById(positionInHandOfCardWhoPlayerOneTryPlay);
 
        if (this.Title == "Rolling Takedown")
        {
            string damageDone = cardWhoPlayerOneTryPlay.Damage;
            int damageNumber = Int32.Parse(damageDone);
            return damageNumber;

        }
        else if (this.Title == "Knee to the Gut")
        {
            string damageDone = cardWhoPlayerOneTryPlay.Damage;
            int damageNumber = Int32.Parse(damageDone);
            return damageNumber;
        }
        else
        {
            string damageDone = Damage;
            int damageNumber = Int32.Parse(damageDone);

            if (playerOne.GetName() == "MANKIND")
            {
                if (damageNumber >= 1)
                {
                    damageNumber = damageNumber - 1;
                }
            }
            return damageNumber;
        }
    }
    
    private void FortitudeInReversalSpecial(Player playerTwo, int plusFortitude)
    {
        if (this.Title == "Rolling Takedown")
        {
            playerTwo.AddFortitude(0);
        }
        else if (this.Title == "Knee to the Gut")
        {
            playerTwo.AddFortitude(0);
        }
        else
        {
            playerTwo.AddFortitude(plusFortitude);
        }
    }
    
    public string InfoCardInStringAlternativeFormat(
        ICard instanceReversalCardWhoPlayerTwoTryPlay, 
        string typeReversalWhoPlayerTwoTryPlay)
    {
        ViewableCardInfo cardViewableCardInfo = new ViewableCardInfo(
                instanceReversalCardWhoPlayerTwoTryPlay);
        ViewablePlayInfo playViewablePlayInfo = 
            new ViewablePlayInfo(cardViewableCardInfo, 
                typeReversalWhoPlayerTwoTryPlay.ToUpper());
        string cardToPlayString = Formatter.PlayToString(playViewablePlayInfo);
        return cardToPlayString;
    }

    public string ReturnReverseEffectHand(ReturnReverseEffectHandArgument argument)
    {
        string value = argument.Value;
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
            return "Revertir desde la mano";
        }
        throw new UnexpectedBehaviorException(
            "Comportamiento inesperado en ReturnReverseEffectHand");
    }
    
    public List<string> GetInfoCardsInHand(Player player)
    {
        List<string> cardsinfo = new List<string> { };
        foreach (ICard card in player.GetCardsHand())
        {
            ViewableCardInfo cardInfo = new ViewableCardInfo(card);
            string cardStr = Formatter.CardToString(cardInfo);
            cardsinfo.Add(cardStr);
        }
        return cardsinfo;
    }
    
    public override string Effect(Player player, int cardPosition, View view)
    {
        ICard playedCard = RemoveCardFromPlayerHand(player, cardPosition);
        view.SayThatPlayerSuccessfullyPlayedACard();
        SelectedEffect selectedEffect = 
            view.AskUserToSelectAnEffectForJockeyForPosition(player.GetName());
        try
        {
            EffectResult result = GetEffectResult(selectedEffect);
             return result.Message;
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

    private EffectResult GetEffectResult(SelectedEffect selectedEffect)
    {
        if (selectedEffect == SelectedEffect.NextGrappleIsPlus4D)
        {
            return new EffectResult { Success = true, Message = "activateNextGrappleIsPlus4D" };
        }
        else if (selectedEffect == SelectedEffect.NextGrapplesReversalIsPlus8F)
        {
            return new EffectResult { Success = true, Message = 
                "activateNextGrapplesReversalIsPlus8F" };
        }
        throw new UnexpectedBehaviorException(
            "Comportamiento inesperado en ReturnReverseEffectHandJockeyingForPosition");
    }
}