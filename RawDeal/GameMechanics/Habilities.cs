using RawDeal.data;
using RawDealView;

namespace RawDeal;

public class Habilities
{
    private View _view;
    
    public Habilities(View view)
    {
        _view = view;
    }
    
    public void ExecuteAutomaticHabilities(Player playerOne, Player playerTwo)
    {
        ExecuteIfPlayTheRock(playerOne);
        ExecuteIfPlayMankind(playerOne);
        ExecuteIfPlayKane(playerOne, playerTwo);
    }
    
    private void ExecuteIfPlayTheRock(Player player)
    {
        if (player.GetName() == "THE ROCK")
        {
            TheRock theRock = new TheRock();
            theRock.Hability(player, _view);
        }
    }
    
    private void ExecuteIfPlayMankind(Player player)
    {
        if (player.GetName() == "MANKIND")
        {
            Mankind mankind = new Mankind();
            mankind.Hability(player, _view);
        }
        else
        {
            player.takeOneCard();
        }
    }
    
    private void ExecuteIfPlayKane(Player playerOne, Player playerTwo)
    {
        if (playerOne.GetName() == "KANE")
        {
            Kane kane = new Kane();
            kane.Hability(playerOne, playerTwo, _view);
        }
    }

    public void ExecuteNoAutomaticHabilities(Player playerOne, Player playerTwo)
    {
        if (playerOne.GetName() == "THE UNDERTAKER")
        {
            ExecuteIfPlayTheUnderTaker(playerOne);
        }

        if (playerOne.GetName() == "CHRIS JERICHO")
        {
            ExecuteIfPlayChrisJericho(playerOne, playerTwo);
        }

        if (playerOne.GetName() == "STONE COLD STEVE AUSTIN")
        {
            ExecuteIfPlaySteveColdSteveAustin(playerOne);
        }
    }
    
    private void ExecuteIfPlayTheUnderTaker(Player player)
    {
        TheUnderTaker theUnderTaker = new TheUnderTaker();
        theUnderTaker.Hability(player, _view);
    }
    
    private void ExecuteIfPlayChrisJericho(Player playerOne, Player playerTwo)
    {
        ChrisJericho chrisJericho = new ChrisJericho();
        chrisJericho.Hability(playerOne, playerTwo, _view);
    }
    
    private void ExecuteIfPlaySteveColdSteveAustin(Player playerOne)
    {
        SteveColdSteveAustin steveColdSteveAustin = new SteveColdSteveAustin();
        steveColdSteveAustin.Hability(playerOne, _view);
    }
    
}