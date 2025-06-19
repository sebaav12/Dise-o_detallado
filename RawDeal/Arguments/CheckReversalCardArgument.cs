namespace RawDeal.Arguments;

public class CheckReversalCardArgument : CheckReversalArgument
{
    public ICard CardOfPlayerTwo { get; set; }
    public string TypeSelectedOfCardWhoPlayerOneTryPlay { get; set; }
}