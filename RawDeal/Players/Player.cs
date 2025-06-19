namespace RawDeal;
using RawDeal.data;

public class Player
{
    private string name;
    private ListCard hand;
    private ListCard arsenal;
    private ListCard ringSide;
    private ListCard ringArea;
    private SuperStar superStar;
    private int fortitude;
    private int damage;
    
    public Player(string name)
    {
        this.name = name;
        this.hand = new ListCard();
        this.arsenal = new ListCard();
        this.ringSide = new ListCard();
        this.ringArea = new ListCard();
        this.fortitude = 0;
    }
    
    public string GetName()
    {
        return name;
    }
    
    public int GetFortitude()
    {
        return fortitude;
    }

    public void AddFortitude(int damage)
    {
        this.fortitude = this.fortitude + damage;
    }
    
    public void GetSuperStar(SuperStar card)
    {
        this.superStar = card;
        this.name = card.Name;
    }

    public SuperStar CardSuperStar()
    {
        return this.superStar;
    }
    
    public int NumberOfCardsInHand()
    {
        var numberCardHand = this.hand.NumberOfCards();
        return numberCardHand;
    }

    public ListCard GetCardsHand()
    {
        return this.hand;
    }
    
    public void RemoveCardOfHand(int position)
    {
        this.hand.RemoveById(position);
    }
    
    public ListCard GetCardsArsenal()
    {
        return this.arsenal;
    }
    
    public int NumberOfCardInArsenal()
    {
        var numberCardArsenal = this.arsenal.NumberOfCards();
        return numberCardArsenal;
    }
    
    public void AddCardToArsenal(ICard card)
    {
        this.arsenal.AddCard(card);
    }
    
    public void RemoveCardOfArsenal(int position)
    {
        this.arsenal.RemoveById(position);
    }
    
    public void RemoveManyCardsArsenal(int count)
    {

        if (NumberOfCardInArsenal() <= count)
        {
            this.arsenal.Clear();
        }
        else
        {
            int startIndex = this.arsenal.NumberOfCards() - count;
            this.arsenal.RemoveRange(startIndex, count);
        }
    }
    
    public ListCard GetCardsRingArea()
    {
        return this.ringArea;
    }
    
    public void AddCardToRingArea(ICard card)
    {
        this.ringArea.AddCard(card);
    }
    
    public ListCard GetCardsRingSide()
    {
        return this.ringSide;
    }

    public int NumberOfCardsInRingSide()
    {
        int num = this.ringSide.Count();
        return num;
    }
    
    public void AddCardToRingSide(ICard card)
    {
        this.ringSide.AddCard(card);
    }

    public void moveOneCardRingSideToArsenal(int position)
    {
        ICard carta = this.ringSide.GetCardById(position);
        this.ringSide.RemoveById(position);
        this.arsenal.Insert(0, carta);
    }
    
    public void moveOneCardRingSideToHand(int position)
    {
        ICard carta = this.ringSide.GetCardById(position);
        this.ringSide.RemoveById(position);
        this.hand.AddCard(carta);
    }
    
    public void CreateInitialHand()
    {
        int handSize = this.superStar.HandSize;
        for (int i = 0; i < handSize; i++)
        {
            int lastCardIndex = this.arsenal.NumberOfCards() - 1;
            ICard carta = this.arsenal.GetCardById(lastCardIndex);
            AddCardToHand(carta);
            this.arsenal.RemoveById(lastCardIndex);
        }
    }
    
    public void AddCardToHand(ICard card)
    {
        this.hand.AddCard(card);
    }
    
    public void takeOneCard()
    {
        int lastIndex = this.arsenal.NumberOfCards() - 1;
        ICard carta = this.arsenal.GetCardById(lastIndex);
        this.arsenal.RemoveById(lastIndex);
        AddCardToHand(carta);
    }
    
    public void moveOneCardOfHandToRingSide(int position)
    {
        ICard carta = this.hand.GetCardById(position);
        this.hand.RemoveById(position);
        this.ringSide.AddCard(carta);
    }

    public void MoveSelectedCardOfHandToArsenal(int position)
    {
        ICard carta = this.hand.GetCardById(position);
        this.hand.RemoveById(position);
        this.arsenal.Insert(0, carta);
    }

    public bool CheckPlayerHaveReversal()
    {
        foreach (var card in hand)
        {
            foreach (var type in card.Types)
            {
                if (type == "Reversal")
                {
                    return true;
                }
            }
        }
        return false;
    }

    public bool CheckIfFortitudeIsBiggerThanReversalFortitude(
        ICard reversalCard, ICard cardWhoTryPlayPlayerOne, 
        bool availableNextGrapplesReversalIsPlus8F)
    {
        int cardFortitude = int.Parse(reversalCard.Fortitude);

        bool isGrapple = false;
        
        foreach (var subtype in cardWhoTryPlayPlayerOne.Subtypes)
        {
            if (subtype == "Grapple")
            {
                isGrapple = true;
            }
        }

        if (availableNextGrapplesReversalIsPlus8F && isGrapple)
        {
            
            if (this.fortitude >= cardFortitude + 8)
            {
                return true;
            }
        }
        else
        {
            if (this.fortitude >= cardFortitude)
            {
                return true;
            }
        }

        return false;
    }

    public ListCard GetReversalCards()
    {
        ListCard reversalCards = new ListCard();
        
        foreach (var card in hand)
        {
             
            foreach (var type in card.Types)
            {
                if (type == "Reversal")
                {
                    reversalCards.AddCard(card);
                }
            }
        }
        
        return reversalCards;
    }
}

