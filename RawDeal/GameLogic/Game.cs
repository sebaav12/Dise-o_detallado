using RawDealView;
using RawDeal.data;
namespace RawDeal;

public class Game
{
    private View _view;
    private string _deckFolder;

    public Game(View view, string deckFolder)
    {
        _view = view;
        _deckFolder = deckFolder;
    }

    public void Play()
    {
        try{ 
            var (cards, superStars) = GenerateCardsAndSuperStars();

            var listsCardsOfPlayersAndSuperStarValues = PlayersSelectDeck(cards, superStars);

            var deckPlayerOneIsValid = listsCardsOfPlayersAndSuperStarValues.Item1.Item3;
            var deckPlayerTwoIsValid = listsCardsOfPlayersAndSuperStarValues.Item2.Item3;
             
            if (!deckPlayerTwoIsValid || !deckPlayerOneIsValid )
            {
                throw new InvalidDeckException();
            }
        
            var superStarValueOfPlayerOne = listsCardsOfPlayersAndSuperStarValues.Item1.Item2;
            var superStarValueOfPlayerTwo = listsCardsOfPlayersAndSuperStarValues.Item2.Item2;
        
            List<int> numSuperStars = new List<int>()
            {
                superStarValueOfPlayerOne, superStarValueOfPlayerTwo
            };
        
            (Player, Player) players = GiveCardsToPlayers(
                listsCardsOfPlayersAndSuperStarValues, cards, superStars);

            Player playerOne = players.Item1;
            Player playerTwo = players.Item2;

            GameLoop loop = new GameLoop(_view);
        
            loop.LoopOfGame(numSuperStars, playerOne, playerTwo);
        }
        catch (InvalidDeckException e)
        {
            _view.SayThatDeckIsInvalid();
        }
    }

    private (ListCard, List<SuperStar>) GenerateCardsAndSuperStars()
    {
        CardGenerator generator = new CardGenerator();
        var cards = generator.GenerateCardsInstances();
        var superStars = generator.GenerateSuperStarsInstances();
        return (cards, superStars);
    }

    private ((List<string>, int, bool), (List<string>, int, bool)) PlayersSelectDeck(
        ListCard cards,
        List<SuperStar> superStars)
    {
        (List<string>, int, bool) listCardsOfPlayerOneAndSuperStarValue =
            PlayerSelectDeck(cards, superStars);

        var deckPlayerOneIsValid = listCardsOfPlayerOneAndSuperStarValue.Item3;
        
        if (!deckPlayerOneIsValid )
        {
            return (listCardsOfPlayerOneAndSuperStarValue, listCardsOfPlayerOneAndSuperStarValue);
        }
        
        (List<string>, int, bool) listCardsOfPlayerTwoAndSuperStarValue =
            PlayerSelectDeck(cards, superStars);

        return (listCardsOfPlayerOneAndSuperStarValue, listCardsOfPlayerTwoAndSuperStarValue);
    }

    private (List<string>, int, bool) PlayerSelectDeck(
        ListCard cards, List<SuperStar> superStars)
    {
        DeckValidator checkDeckIsCorrect = new DeckValidator();

        List<string> listCardsOfPlayer = SelectDeck();
        List<object> returnOfValidateDeckPlayer =
            checkDeckIsCorrect.ValidateDeck(listCardsOfPlayer, cards, superStars);
        bool deckPlayerIsValid = Convert.ToBoolean(returnOfValidateDeckPlayer[0]);
        
        int superStarValue = Convert.ToInt32(returnOfValidateDeckPlayer[1]);
        
        return (listCardsOfPlayer, superStarValue, deckPlayerIsValid);
    }

    private (Player, Player) GiveCardsToPlayers(
        ((List<string>, int, bool), (List<string>, int, bool)) listCardsOfPlayer, 
        ListCard cards, List<SuperStar> superStars )
    {
        Player playerOne = new Player("Jugador 1");
        Player playerTwo = new Player("Jugador 2");
        
        var listCardsOfPlayerOne = listCardsOfPlayer.Item1.Item1;
        var listCardsOfPlayerTwo = listCardsOfPlayer.Item2.Item1;
        
        GiveSuperStarToPlayer(playerOne, superStars, listCardsOfPlayerOne);
        SaveCardsInArsenal(playerOne, listCardsOfPlayerOne, cards);
        
        GiveSuperStarToPlayer(playerTwo, superStars, listCardsOfPlayerTwo);
        SaveCardsInArsenal(playerTwo, listCardsOfPlayerTwo, cards);

        return (playerOne, playerTwo);
    }
    
    public class InvalidDeckException : Exception
    {
        public InvalidDeckException() : base(
            "The deck does not have the correct number of cards.") {}
    }
    
    private List<string> SelectDeck()
    {
        string addressPlayer1 = _view.AskUserToSelectDeck(_deckFolder);
        string[] lineas = File.ReadAllLines(addressPlayer1);
        List<string> listCardsPlayer1 = new List<string>(lineas);
        return listCardsPlayer1;
    }
    
    private void GiveSuperStarToPlayer(
        Player player, List<SuperStar> superStars, 
        List<string> listCardsPlayer)
    {
        string nameSuperStarStringOriginal = listCardsPlayer[0];
        string nameSuperStar = 
            nameSuperStarStringOriginal.Substring(0, nameSuperStarStringOriginal.Length - 17);

        foreach (var superStar in superStars)
        {
            if (superStar.Name == nameSuperStar)
            {
                player.GetSuperStar(superStar);
                listCardsPlayer.RemoveAt(0);
            }
        }
    }

    private void SaveCardsInArsenal(
        Player player, List<string> listCardsPlayer, ListCard cards)
    {
        foreach (var nameCard in listCardsPlayer)
        {
            AddCardToPlayerArsenal(nameCard, player, cards);
        }
    }

    private void AddCardToPlayerArsenal(string nameCard, Player player, ListCard cards)
    {
        foreach (var card in cards)
        {
            if (nameCard == card.Title)
            {
                player.AddCardToArsenal(card);
                return;
            }
        }
    }
}