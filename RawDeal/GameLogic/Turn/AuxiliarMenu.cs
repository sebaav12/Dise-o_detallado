using RawDealView;
using RawDealView.Options;

namespace RawDeal;

public class AuxiliarMenu
{
    private View _view;
    
    public AuxiliarMenu(View view)
    {
        _view = view;
    }
    
    public NextPlay GenerateMainMenu(Player playerOne, bool _skillUsed)
    {
        List<string> playersWhitOptionalHabilities = 
            new List<string>() { "STONE COLD STEVE AUSTIN", "CHRIS JERICHO", "THE UNDERTAKER" };

        if (!playersWhitOptionalHabilities.Contains(playerOne.GetName()) || _skillUsed)
        {
            return _view.AskUserWhatToDoWhenHeCannotUseHisAbility();
        }

        if (playerOne.GetName() == "THE UNDERTAKER" && playerOne.NumberOfCardsInHand() < 2)
        {
            return _view.AskUserWhatToDoWhenHeCannotUseHisAbility();
        }

        if (playerOne.GetName() == "CHRIS JERICHO" && playerOne.NumberOfCardsInHand() < 1)
        {
            return _view.AskUserWhatToDoWhenHeCannotUseHisAbility();
        }

        return _view.AskUserWhatToDoWhenUsingHisAbilityIsPossible();
    }
    
    public void ShowBasicInfoOfGame(Player playerOne, Player playerTwo)
    {
        PlayerInfo PlayerOneInfo = 
            new PlayerInfo(playerOne.GetName(), playerOne.GetFortitude(),
                playerOne.NumberOfCardsInHand(), playerOne.NumberOfCardInArsenal());
        PlayerInfo PlayerTwoInfo = 
            new PlayerInfo(playerTwo.GetName(), playerTwo.GetFortitude(),
                playerTwo.NumberOfCardsInHand(), playerTwo.NumberOfCardInArsenal());
        _view.ShowGameInfo(PlayerOneInfo, PlayerTwoInfo);
    }
}