using RawDealView;

namespace RawDeal.Arguments
{
    public class ShowCardsOverturnByTakingDamageArgument
    {
        public List<string> CardsInfo { get; set; }
        public int DamageNumber { get; set; }
        public View View { get; set; }
        public Player PlayerOne { get; set; }
    }
}