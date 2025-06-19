using RawDeal.Arguments;
using RawDealView;
using RawDealView.Formatters;
using RawDealView.Options;

namespace RawDeal;

public class CardData
{
    public string Title { get; set; }
    public List<string> Types { get; set; }
    public List<string> Subtypes { get; set; }
    public string Fortitude { get; set; }
    public string Damage { get; set; }
    public string StunValue { get; set; }
    public string CardEffect { get; set; }
}

public class ICard
{
    public string Title { get; set; }
    public List<string> Types { get; set; }
    public List<string> Subtypes { get; set; }
    public string Fortitude { get; set; }
    public string Damage { get; set; }
    public string StunValue { get; set; }
    public string CardEffect { get; set; }
    
    public string InfoPlayToString(
        ICard instanceReversalCardWhoPlayerTwoTryPlay, 
        string typeReversalWhoPlayerTwoTryPlay)
    {
        ViewableCardInfo cardInfo = new ViewableCardInfo(instanceReversalCardWhoPlayerTwoTryPlay);
        
        ViewablePlayInfo playInfo = 
            new ViewablePlayInfo(cardInfo, typeReversalWhoPlayerTwoTryPlay.ToUpper());
        string cardToPlay = Formatter.PlayToString(playInfo);

        return cardToPlay;
    }

    public string ApplyDamage(ApplyDamageArgument applyDamageArgument )
    {
        Player playerOne = applyDamageArgument.PlayerOne;
        Player playerTwo = applyDamageArgument.PlayerTwo;
        View _view = applyDamageArgument.View;
        bool availableNextGrappleIsPlus4D = applyDamageArgument.AvailableNextGrappleIsPlus4D;
        bool availableNextGrapplesReversalIsPlus8F = 
            applyDamageArgument.AvailableNextGrapplesReversalIsPlus8F;
        
        bool useNextGrappleIsPlus4D = false;
        bool useNextGrapplesReversalIsPlus8F = false;

        bool isGrapple = false;
        
        int damageNumber = 0;
        int plusFotitude = 0;
        
        isGrapple = CheckIsGrapple(isGrapple);
        
        if (isGrapple == true && availableNextGrappleIsPlus4D == true)
        {
            string damageDone = Damage;
            damageNumber = Int32.Parse(damageDone) + 4;
            plusFotitude = damageNumber - 4;
            useNextGrappleIsPlus4D = true;
        }
        else if (isGrapple == false || availableNextGrappleIsPlus4D == false)
        {
            string damageDone = Damage;
            damageNumber = Int32.Parse(damageDone);
            plusFotitude = damageNumber;
        }
        
        damageNumber = DamageNumberIfPLayerTwoIsMankind(playerTwo, damageNumber);
        
        SayIfPLayerTwoTakeSomeDamage(playerTwo, damageNumber, _view);
        
        IcardDamageExecutor executor = new IcardDamageExecutor();

        (string, int, bool) returnExecuteDamage = executor.ExecuteDamage(
            applyDamageArgument, (damageNumber, isGrapple, plusFotitude));
        
        if (returnExecuteDamage.Item1 != "null")
        {
            return returnExecuteDamage.Item1;
        }
        int contador = returnExecuteDamage.Item2;
        bool damageComplete = returnExecuteDamage.Item3;
        
        ListCard cardsArsenalOfPlayerTwo = playerTwo.GetCardsArsenal();
        
        playerOne.AddFortitude(plusFotitude);
        
        if (contador == damageNumber)
        {
            damageComplete = true;
        }

        if (damageComplete == false)
        {
            if(cardsArsenalOfPlayerTwo.Count() == 0)
            {
                _view.CongratulateWinner(playerOne.GetName());
                return "Gana Player One";
            }
        }
        
        string grappleResult = CheckAvailableNextGrapple(
            availableNextGrappleIsPlus4D, availableNextGrapplesReversalIsPlus8F);

        if (grappleResult == null)
        {
            return "Resultado no disponible";
        }
        return grappleResult;
    }

    private bool CheckIsGrapple(bool isGrapple)
    {
        foreach (var subtype in this.Subtypes)
        {
            if (subtype == "Grapple")
            {
                isGrapple = true;
            }
        }
        return isGrapple;
    }

    private int DamageNumberIfPLayerTwoIsMankind(Player playerTwo, int damageNumber)
    {
        if (playerTwo.GetName() == "MANKIND")
        {
            if (damageNumber >= 1)
            {
                damageNumber = damageNumber - 1;
            }
        }

        return damageNumber;
    }

    private void SayIfPLayerTwoTakeSomeDamage(Player playerTwo, int damageNumber, View _view)
    {
        if (damageNumber > 0)
        {
            _view.SayThatSuperstarWillTakeSomeDamage(playerTwo.GetName(), damageNumber);
        }
    }
    
    private string CheckAvailableNextGrapple(
        bool availableNextGrappleIsPlus4D, bool availableNextGrapplesReversalIsPlus8F)
    {
        if (availableNextGrappleIsPlus4D)
        {
            return "desactivateNextGrappleIsPlus4D";
        }

        if (availableNextGrapplesReversalIsPlus8F)
        {
            return "desactivateNextGrapplesReversalIsPlus8F";
        }
        
        return "No Action";
    }
    
    public List<string> GetInfoCardInHand(Player player, int positionCardInHand )
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

    public virtual string Effect(
        Player player, int positionCardInHand, View _view)
    {
        return "";
    }
    
    public virtual string ReverseEffectHand(
        ReverseEffectHandArgument argument)
    {
        return "";
    }
    
    public virtual bool CheckIsPossibleUseEffect(
        CheckReversalArgument argument)
    {
        return false;
    }
}

