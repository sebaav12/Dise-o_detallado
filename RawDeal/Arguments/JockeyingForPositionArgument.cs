namespace RawDeal.Arguments;

public class JockeyingForPositionArgument
{
    public PositionInTheDeckOfPlayableCards CartasJugablesPosition { get; set; }
    public ListCard CartasJugablesInstancia { get; set; }
    public List<string> CartasJugables { get; set; }
    public Player PlayerOne { get; set; }
    public int PositionInHand { get; set; }
    public int PositionInHandJugable { get; set; }
    public int PositionInCartasJugablesList { get; set; }
}