using RawDealView;
namespace RawDeal.data;

public class Kane : SuperStar
{
    public void Hability(Player playerOne, Player playerTwo, View view)
    {
        if (IsPlayerArsenalEmpty(playerTwo))
        {
            AnnounceWinner(playerOne, view);
        }
        else
        {
            PerformHability(playerOne, playerTwo, view);
        }
    }

    private bool IsPlayerArsenalEmpty(Player player)
    {
        return player.NumberOfCardInArsenal() < 1;
    }

    private void AnnounceWinner(Player winner, View view)
    {
        view.CongratulateWinner(winner.GetName());
    }

    private void PerformHability(Player playerOne, Player playerTwo, View view)
    {
        List<string> affectedCardsInfo = ApplyDamageToPlayerTwo(playerOne, playerTwo);
        AnnounceHabilityUsage(playerOne, playerTwo, view);
        ShowAffectedCards(affectedCardsInfo, view);
        RemoveAffectedCardsFromPlayerTwoArsenal(playerTwo);
    }

    private List<string> ApplyDamageToPlayerTwo(Player playerOne, Player playerTwo)
    {
        int damageAmount = 1;
        List<string> affectedCardsInfo = new List<string>();

        ListCard cardsInArsenal = playerTwo.GetCardsArsenal();
        for (int i = cardsInArsenal.Count() - 1; i >= 0 && damageAmount > 0; i--, damageAmount--)
        {
            ICard card = cardsInArsenal.GetCardById(i);
            affectedCardsInfo.Add(InfoCardInString(card));
            playerTwo.AddCardToRingSide(card);
        }

        return affectedCardsInfo;
    }

    private void AnnounceHabilityUsage(Player playerOne, Player playerTwo, View view)
    {
        SuperStar superStar = playerOne.CardSuperStar();
        string ability = superStar.SuperstarAbility;
        view.SayThatPlayerIsGoingToUseHisAbility(
            playerOne.GetName(), ability);
        view.SayThatSuperstarWillTakeSomeDamage(playerTwo.GetName(), 1);
    }

    private void ShowAffectedCards(List<string> affectedCardsInfo, View view)
    {
        int currentDamage = 1;
        foreach (var card in affectedCardsInfo)
        {
            view.ShowCardOverturnByTakingDamage(card, currentDamage, affectedCardsInfo.Count());
            currentDamage++;
        }
    }

    private void RemoveAffectedCardsFromPlayerTwoArsenal(Player playerTwo)
    {
        playerTwo.RemoveManyCardsArsenal(1);  
    }

}