using RawDealView;
namespace RawDeal.Arguments;

public class ReverseEffectHandArgument
{
    public Player PlayerOne { get; set; }
    public int PositionInHandOfCardWhoPlayerOneTryPlay { get; set; }
    public string TypeSelectedOfCardWhoPlayerOneTryPlay { get; set; }
    public Player PlayerTwo { get; set; }
    public ICard InstanceReversalCardWhoPlayerTwoTryPlay { get; set; }
    public View View { get; set; }
    public bool AvailableNextGrappleIsPlus4D { get; set; }
    public bool AvailableNextGrapplesReversalIsPlus8F { get; set; }
}