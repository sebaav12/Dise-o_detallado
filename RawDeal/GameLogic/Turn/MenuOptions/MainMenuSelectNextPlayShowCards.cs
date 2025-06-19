using RawDealView;
using RawDealView.Formatters;
using RawDealView.Options;

namespace RawDeal.GameLogic.Turn;

public class MainMenuSelectNextPlayShowCards
{
    
    
    private View _view;
    private string _result;
     

    public MainMenuSelectNextPlayShowCards(View view)
    {
        _view = view;
    }
    
    public void ExecuteMainMenuSelectNextPlayShowCards(
        NextPlay mainMenu, Player playerOne, Player playerTwo)
    {
        if (mainMenu == NextPlay.ShowCards)
        {
            IfPlayerSelectShowCards(playerOne, playerTwo);
        }
    }
    
    private void IfPlayerSelectShowCards(Player playerOne, Player playerTwo)
    {
        CardSet menuShow = _view.AskUserWhatSetOfCardsHeWantsToSee();
        List<string> cardsinfo = null;

        Func<Player, List<string>> action = null;

        Dictionary<CardSet, Func<Player, List<string>>> actionMap = 
            new Dictionary<CardSet, Func<Player, List<string>>>
            {
                {CardSet.Hand, GetInfoCardsInHand},
                {CardSet.RingArea, GetInfoCardsInRingArea},
                {CardSet.RingsidePile, GetInfoCardsInRingSide},
                {CardSet.OpponentsRingArea, GetInfoCardsInRingArea},
                {CardSet.OpponentsRingsidePile, GetInfoCardsInRingSide},
            };

        if (actionMap.TryGetValue(menuShow, out action))
        {
            cardsinfo = menuShow == CardSet.OpponentsRingArea || menuShow == 
                CardSet.OpponentsRingsidePile ? action(playerTwo) : action(playerOne);
            _view.ShowCards(cardsinfo);
        }
    }

    private List<string> GetInfoCards(ListCard cards)
    {
        List<string> cardsInfo = new List<string>();
        foreach (ICard card in cards)
        {
            ViewableCardInfo cardInfo = new ViewableCardInfo(card);
            string cardStr = Formatter.CardToString(cardInfo);
            cardsInfo.Add(cardStr);
        }
        return cardsInfo;
    }
    
    private List<string> GetInfoCardsInHand(Player player)
    {
        return GetInfoCards(player.GetCardsHand());
    }

    private List<string> GetInfoCardsInRingArea(Player player)
    {
        return GetInfoCards(player.GetCardsRingArea());
    }

    private List<string> GetInfoCardsInRingSide(Player player)
    {
        return GetInfoCards(player.GetCardsRingSide());
    }
}