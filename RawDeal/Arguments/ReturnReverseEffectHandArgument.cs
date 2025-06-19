using RawDealView;

namespace RawDeal.Arguments
{
    public class ReturnReverseEffectHandArgument
    {
        public string Value { get; set; }
        public Player PlayerTwo { get; set; }
        public string CardToPlayString { get; set; }
        public View View { get; set; }
    }
}