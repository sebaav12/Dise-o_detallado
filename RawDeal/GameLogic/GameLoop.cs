using RawDeal.Arguments;
using RawDealView;

namespace RawDeal
{
    public class GameLoop
    {
        private View _view;
        private string _gameStatus;
        private bool _availableNextGrappleIsPlus4D;
        private bool _availableNextGrapplesReversalIsPlus8F;

        public GameLoop(View view)
        {
            _view = view;
        }

        public void LoopOfGame(List<int> numSuperStars, Player playerOne, Player playerTwo)
        {
            int numSuper1 = numSuperStars[0];
            int numSuper2 = numSuperStars[1];

            Turn turn = new Turn(_view);

            LoopPlayerArgument loopPlayerArgument = new LoopPlayerArgument
            {
                PlayerOne = playerOne,
                PlayerTwo = playerTwo,
                Turn = turn
            };

            bool startWithPlayerOne = numSuper1 >= numSuper2;
            GeneralLoopPlayerStart(loopPlayerArgument, startWithPlayerOne);
        }

        private void GeneralLoopPlayerStart(LoopPlayerArgument argument, bool startWithPlayerOne)
        {
            Player firstPlayer = startWithPlayerOne ? argument.PlayerOne : argument.PlayerTwo;
            Player secondPlayer = startWithPlayerOne ? argument.PlayerTwo : argument.PlayerOne;

            firstPlayer.CreateInitialHand();
            secondPlayer.CreateInitialHand();

            _gameStatus = "";

            while (string.IsNullOrEmpty(_gameStatus))
            {
                string nextStep = HandleTurn(firstPlayer, secondPlayer, argument.Turn);
                
                var players = (activePlayer: secondPlayer, opponent: firstPlayer);
                
                HandleNextTurn(nextStep, players, argument.Turn);
            }
        }

        private string HandleTurn(Player activePlayer, Player opponent, Turn turn)
        {
            List<bool> loopAvailable = new List<bool>
            {
                _availableNextGrappleIsPlus4D, _availableNextGrapplesReversalIsPlus8F
            };
            _availableNextGrappleIsPlus4D = false;
            _availableNextGrapplesReversalIsPlus8F = false;
            return turn.StartTurn(activePlayer, opponent, loopAvailable);
        }

        private void HandleNextTurn(
            string nextStep,  (Player, Player) players, Turn turn)
        {
            Player activePlayer = players.Item1;
            Player opponent = players.Item2;
            
            if (IsGameOver(nextStep))
            {
                _gameStatus = "Fin";
                return;
            }
            UpdateAvailableFlags(nextStep);
            string nextTurn = HandleTurn(activePlayer, opponent, turn);
            UpdateGameState(nextTurn);
        }

        private bool IsGameOver(string step)
        {
            return step == "Fin del Juego";
        }

        private bool IsEndOfTurn(string step)
        {
            return step.Contains("Fin del Turno");
        }

        private void UpdateAvailableFlags(string nextStep)
        {
            _availableNextGrappleIsPlus4D = 
                nextStep.Contains("activateNextGrappleIsPlus4D");
            _availableNextGrapplesReversalIsPlus8F = 
                nextStep.Contains("activateNextGrapplesReversalIsPlus8F");
        }

        private void UpdateGameState(string nextTurn)
        {
            if (IsEndOfTurn(nextTurn))
            {
                UpdateAvailableFlags(nextTurn);
            }
            else if (IsGameOver(nextTurn))
            {
                _gameStatus = "Fin";
            }
        }
    }
}
