namespace RawDeal;

public class ReversalInfo
{
    List<(bool, ICard)> reversalInfo = new List<(bool, ICard)>();
    
    public void Add(bool playReversal, ICard selectedCard)
    {
        reversalInfo.Add((playReversal, selectedCard));
    }
    
    public (bool playReversal, ICard cardReversalSelectedForPlayerTwo) GetInfoById(int index)
    {
        if (index < 0 || index >= reversalInfo.Count)
        {
            throw new IndexOutOfRangeException();
        }
        return reversalInfo[index];
    }
}