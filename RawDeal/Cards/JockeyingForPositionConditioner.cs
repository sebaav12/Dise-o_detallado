using RawDeal.Arguments;
using RawDealView.Formatters;

namespace RawDeal;

public class JockeyingForPositionConditioner
{
    private readonly JockeyingForPositionArgument gameState;
    
    public JockeyingForPositionConditioner(JockeyingForPositionArgument gameState)
    {
        this.gameState = gameState;
    }

    public void ApplyCondition()
    {
        foreach (ICard card in gameState.PlayerOne.GetCardsHand())
        {
            if (IsCardPlayable(card))
            {
                ProcessCard(card);
            }
            gameState.PositionInHand++;
        }
    }

    private bool IsCardPlayable(ICard card)
    {
        return int.Parse(card.Fortitude) <= gameState.PlayerOne.GetFortitude() && 
               (!card.Types.Contains("Reversal") || card.Title == "Jockeying for Position");
    }

    private void ProcessCard(ICard card)
    {
        if (int.Parse(card.Fortitude) > gameState.PlayerOne.GetFortitude())
        {
            return;
        }

        if (card.Types.Contains("Reversal") && card.Title != "Jockeying for Position")
        {
            return;
        }

        string typeOfCardUpper = card.Types[0].ToUpper();
        AddCardToPlayableList(card, typeOfCardUpper);

        int numberOfTypes = card.Types.Count;
        if (numberOfTypes <= 1 || card.Title == "Jockeying for Position")
        {
            return;
        }

        string typeOfCardUpper2 = card.Types[1].ToUpper();
        AddCardToPlayableList(card, typeOfCardUpper2);
    }

    private void AddCardToPlayableList(ICard card, string typeOfCardUpper)
    {
        ViewablePlayInfo playInfo = 
            new ViewablePlayInfo(new ViewableCardInfo(card), typeOfCardUpper);
        string cardToPlay = Formatter.PlayToString(playInfo);
        CardToPlayInfo cardToPlayInfo = new CardToPlayInfo();

        cardToPlayInfo.Add(
            cardToPlay, gameState.PositionInHand, gameState.PositionInHandJugable,
            gameState.PositionInCartasJugablesList, typeOfCardUpper
        );

        gameState.CartasJugablesInstancia.AddCard(card);
        gameState.CartasJugables.Add(cardToPlay);
        gameState.CartasJugablesPosition.Add(cardToPlayInfo);

        gameState.PositionInCartasJugablesList++;
        gameState.PositionInHandJugable++;
    }

}
