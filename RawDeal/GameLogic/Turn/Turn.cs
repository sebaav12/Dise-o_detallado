using RawDeal.Arguments;
using RawDeal.GameLogic.Turn;
using RawDealView;
using RawDealView.Options;
namespace RawDeal;

public class Turn
{
    private View _view;
    private string? _result;
    private bool _skillUsed;
    private bool _availableNextGrappleIsPlus4D;
    private bool _availableNextGrapplesReversalIsPlus8F;
    private Dictionary<string, Func<string, string>> actionMap;
    
    public Turn(View view)
    {
        _view = view;
        _result = null;
        _skillUsed = false;
        actionMap = new Dictionary<string, Func<string, string>>
        {
            {"Gana Player One", SimpleReturn},
            {"Gana Player Two", SimpleReturn},
            {"Revertir desde el mazo", SimpleReturn},
            {"Revertir desde el mazo post daño completo", SimpleReturn},
            {"Revertir desde la mano", SimpleReturn},
            {"desactivateNextGrappleIsPlus4D", SimpleReturn},
            {"activateNextGrappleIsPlus4D", SimpleReturn},
            {"activateNextGrappleIsPlus4D y cambio de jugador", SimpleReturn},
            {"activateNextGrapplesReversalIsPlus8F y cambio de jugador", SimpleReturn},
            {"desactivateNextGrapplesReversalIsPlus8F", SimpleReturn},
            {"activateNextGrapplesReversalIsPlus8F", SimpleReturn}
        };
    }
    
    public string StartTurn(Player playerOne, Player playerTwo, List<bool> LoopAvailable)
    {
        bool LoopAvailableNextGrappleIsPlus4D = LoopAvailable[0];
        bool LoopAvailableNextGrapplesReversalIsPlus8F = LoopAvailable[1];
        _result = null;
        _skillUsed = false;
        _availableNextGrappleIsPlus4D = false;
        _availableNextGrapplesReversalIsPlus8F = false;

        if (LoopAvailableNextGrappleIsPlus4D)
        {
            _availableNextGrappleIsPlus4D = true;
        }
        if (LoopAvailableNextGrapplesReversalIsPlus8F)
        {
            _availableNextGrapplesReversalIsPlus8F = true;
        }
        if ( NumberOfCardsisZeroCondition(playerOne, playerTwo) == false)
        {
            _view.SayThatATurnBegins(Convert.ToString(playerOne.GetName()));
            Habilities habilities = new Habilities(_view);
            habilities.ExecuteAutomaticHabilities(playerOne, playerTwo);
            StoneColdSteveAustinCondition(playerOne);
            MainMenuLoop(playerOne, playerTwo, habilities);
        }
        return _result;
    }

    private bool NumberOfCardsisZeroCondition(Player playerOne, Player playerTwo)
    {
        if (playerOne.NumberOfCardInArsenal() == 0)
        {
            _view.CongratulateWinner(playerTwo.GetName());
            _result = "Fin del Juego";
            return true;
        }
        if (playerTwo.NumberOfCardInArsenal() == 0)
        {
            _view.CongratulateWinner(playerOne.GetName());
            _result = "Fin del Juego";
            return true;
        }
        return false;
    }

    private void StoneColdSteveAustinCondition(Player playerOne)
    {
        if (playerOne.GetName() == "STONE COLD STEVE AUSTIN")
        {
            if (playerOne.NumberOfCardInArsenal() == 0)
            {
                _skillUsed = true;
            }
        }
    }
    
    private void MainMenuLoop(Player playerOne, 
        Player playerTwo, Habilities habilities)
    {
        while (string.IsNullOrEmpty(_result))
        {
            AuxiliarMenu AuxiliarMenu = new AuxiliarMenu(_view);
            AuxiliarMenu.ShowBasicInfoOfGame(playerOne, playerTwo);
            NextPlay mainMenu = AuxiliarMenu.GenerateMainMenu(playerOne, _skillUsed);
            MainMenuSelectNextPlayShowCards mainMenuSelectNextPlayShowCards = 
                new MainMenuSelectNextPlayShowCards( _view);
            MainMenuSelectNextPlayEndTurn mainMenuSelectNextPlayEndTurn =
                new MainMenuSelectNextPlayEndTurn(_view, _result);
            mainMenuSelectNextPlayShowCards.
                ExecuteMainMenuSelectNextPlayShowCards(mainMenu, playerOne, playerTwo);
            MainMenuSelectNextPlayPlayCard( mainMenu,  playerOne,  playerTwo);
            if (mainMenu == NextPlay.EndTurn)
            {
                _result = mainMenuSelectNextPlayEndTurn.
                    ExecuteMainMenuSelectNextPlayEndTurn(mainMenu, playerOne, playerTwo);
            }
            MainMenuSelectNextPlayGiveUp(mainMenu, playerTwo);
            MainMenuSelectNextPlayUseAbility( mainMenu,  playerOne,  playerTwo, habilities);
        }
    }

    private void MainMenuSelectNextPlayPlayCard(
        NextPlay mainMenu, 
        Player playerOne, 
        Player playerTwo)
    {
        if (mainMenu != NextPlay.PlayCard) 
            return;
        string value = IfPlayerSelectPlayCard(playerOne, playerTwo);
        Dictionary<string, Action> actionMap = new Dictionary<string, Action>
        {
            {"Gana Player One", () => _result = "Fin del Juego"},
            {"Gana Player Two", () => _result = "Fin del Juego"},
            {"Revertir desde el mazo", () => _result = "Fin del Turno"},
            {"Revertir desde la mano", () => _result = "Fin del Turno"},
            {"activateNextGrappleIsPlus4D", () => _availableNextGrappleIsPlus4D = true},
            {"desactivateNextGrappleIsPlus4D", () => _availableNextGrappleIsPlus4D = false},
            {"activateNextGrappleIsPlus4D y cambio de jugador", () => {
                _availableNextGrappleIsPlus4D = true;
                _result = "Fin del Turno y activateNextGrappleIsPlus4D";
            }},
            {"activateNextGrapplesReversalIsPlus8F y cambio de jugador", () => {
                _availableNextGrapplesReversalIsPlus8F = true;
                _result = "Fin del Turno y activateNextGrapplesReversalIsPlus8F";
            }},
            {"activateNextGrapplesReversalIsPlus8F", () => 
                _availableNextGrapplesReversalIsPlus8F = true},
            {"desactivateNextGrapplesReversalIsPlus8F", () 
                => _availableNextGrapplesReversalIsPlus8F = false},
        };
    
        if (string.IsNullOrEmpty(value))
        {
            Console.WriteLine("Continue");
        }
        else if (actionMap.TryGetValue(value, out Action action))
        {
            action();
        }
        else
        {
            throw new InvalidOperationException($"Valor desconocido: {value}");
        }
    }
    
    private void MainMenuSelectNextPlayGiveUp(NextPlay mainMenu, Player playerTwo)
    {
        if (mainMenu == NextPlay.GiveUp)
        {
            _view.CongratulateWinner(playerTwo.GetName());
            _result = "Fin del Juego";
        }
    }
    
    private void MainMenuSelectNextPlayUseAbility(
        NextPlay mainMenu, Player playerOne, Player playerTwo, Habilities habilities)
    {
        if (mainMenu == NextPlay.UseAbility)
        {
            habilities.ExecuteNoAutomaticHabilities(playerOne, playerTwo);
            _skillUsed = true;
        }
    }
    
    private string IfPlayerSelectPlayCard(Player playerOne, Player playerTwo)
    {
        PositionInTheDeckOfPlayableCards cartasJugablesPosition = 
            new PositionInTheDeckOfPlayableCards();
        ListCard cartasJugablesInstancia = new ListCard();
        List<string> cartasJugables = new List<string>();
        JockeyingForPositionArgument gameState = new JockeyingForPositionArgument
        {
            CartasJugablesPosition = cartasJugablesPosition,
            CartasJugablesInstancia = cartasJugablesInstancia,
            CartasJugables = cartasJugables,
            PlayerOne = playerOne
        };
        JockeyingForPositionConditioner conditioner = 
            new JockeyingForPositionConditioner(gameState);
        conditioner.ApplyCondition();
        int numberOfCardSelectedForPLay = _view.AskUserToSelectAPlay(cartasJugables);
        var players = (PlayerOne: playerOne, PlayerTwo: playerTwo);
        return ManagementNumberOfCardSelectedForPLay(numberOfCardSelectedForPLay,
            gameState, players);
    }
    
    private string ManagementNumberOfCardSelectedForPLay(
        int numberOfCardSelectedForPLay,
        JockeyingForPositionArgument gameState,
        (Player PlayerOne, Player PlayerTwo) players)
    {
        PositionInTheDeckOfPlayableCards cartasJugablesPosition = 
            gameState.CartasJugablesPosition;
        ListCard cartasJugablesInstancia = gameState.CartasJugablesInstancia;
        Player playerOne = players.PlayerOne;
        Player playerTwo = players.PlayerTwo;
        
        if (numberOfCardSelectedForPLay != -1)
        {
            CardToPlayInfo cardInfo = 
                cartasJugablesPosition.GetCardInfoById(numberOfCardSelectedForPLay);
            int posicionhandJugable = cardInfo.GetInfoById(0).positionInHandJugable;
            ICard cartaElegida = cartasJugablesInstancia.GetCardById(posicionhandJugable);
            PlayCardArgument playCardArgument = new PlayCardArgument()
            {
                PlayerOne = playerOne,
                PlayerTwo = playerTwo,
                CardIntance = cartaElegida,
                InfoCardWhoPlayerOneTryPlay = cardInfo,
                AvailableNextGrappleIsPlus4D = _availableNextGrappleIsPlus4D,
                AvailableNextGrapplesReversalIsPlus8F = _availableNextGrapplesReversalIsPlus8F
            };
            PlayCard pLayCard = new PlayCard(_view);
            string play = pLayCard.ExecutePlayCard(playCardArgument);
            return ReturnIfPlayerSelectPlayCard(play);
        }
        return null;
    }
    
    private string ReturnIfPlayerSelectPlayCard(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return null;
        }
        if (actionMap.TryGetValue(value, out Func<string, string> action))
        {
            return action(value);
        }
        return null;
    }
    
    private string SimpleReturn(string value)
    {
        return value;
    }
}