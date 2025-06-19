using RawDealView;
namespace RawDeal.data;

public class TheRock : SuperStar
{
    public void Hability(Player player, View _view)
    {
        int numCartasRingSide = player.NumberOfCardsInRingSide();
        

        if (numCartasRingSide > 0)
        {
            bool useHability = 
                _view.DoesPlayerWantToUseHisAbility("THE ROCK");
            if (useHability == true)
            {
                SuperStar superStar = player.CardSuperStar();
                string ability = superStar.SuperstarAbility;
                _view.SayThatPlayerIsGoingToUseHisAbility(player.GetName(), 
                    ability);
                List<string> cardsinfo = GetInfoCardsInRingSide(player);
                int numCartas = 1;
                int cartaElegida = 
                    _view.AskPlayerToSelectCardsToRecover("THE ROCK", numCartas, cardsinfo);
                
                player.moveOneCardRingSideToArsenal(cartaElegida);
            }
        }
    }
}