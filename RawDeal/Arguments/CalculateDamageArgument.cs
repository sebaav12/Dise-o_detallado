namespace RawDeal.Arguments
{
    public class CalculateDamageArgument
    {
        public Player PlayerOne { get; set; }
        public Player PlayerTwo { get; set; }
        public int PositionInHandOfCardWhoPlayerOneTryPlay { get; set; }
        public bool AvailableNextGrappleIsPlus4D { get; set; }
    }
}