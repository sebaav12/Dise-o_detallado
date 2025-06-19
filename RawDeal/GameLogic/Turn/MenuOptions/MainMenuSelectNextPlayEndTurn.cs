using RawDealView;
using RawDealView.Options;

namespace RawDeal;

public class MainMenuSelectNextPlayEndTurn
{
    private View _view;
    private string _result;
    
    public MainMenuSelectNextPlayEndTurn(View view, string result )
    {
        _view = view;
    }
    
    public string ExecuteMainMenuSelectNextPlayEndTurn(
        NextPlay mainMenu, Player playerOne, Player playerTwo)
    {
        if (mainMenu == NextPlay.EndTurn)
        {
            if (playerTwo.NumberOfCardInArsenal() == 0)
            {
                _view.CongratulateWinner(playerOne.GetName());
                _result = "Fin del Juego";
                return _result;
            }
            else if (playerOne.NumberOfCardInArsenal() == 0)
            {
                _view.CongratulateWinner(playerTwo.GetName());
                _result = "Fin del Juego";
                return _result;
            }
            else
            {
                _result = "Fin del Turno";
                return _result;
            }
        }
        return _result;
    }
}