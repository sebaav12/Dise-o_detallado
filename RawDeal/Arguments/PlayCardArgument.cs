namespace RawDeal.Arguments;
 
 public struct PlayCardArgument
 {
     public Player PlayerOne { get; set; }
     public Player PlayerTwo { get; set; }
     public ICard CardIntance { get; set; }
     public CardToPlayInfo InfoCardWhoPlayerOneTryPlay { get; set; }
     public bool AvailableNextGrappleIsPlus4D { get; set; }
     public bool AvailableNextGrapplesReversalIsPlus8F { get; set; }
 }