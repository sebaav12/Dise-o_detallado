namespace RawDeal.Arguments;

public class CheckReversalArgument
{
    public Player PlayerTwo { get; set; }
    public ICard CardWhoTryPlayPlayerOne { get; set; }
    public string TypeCardWhoTryPlayPlayerOne { get; set; }
    public bool AvailableNextGrapplesReversalIsPlus8F { get; set; }
    public bool AvailableNextGrappleIsPlus4D { get; set; }
}