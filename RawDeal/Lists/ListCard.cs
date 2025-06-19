using System.Collections;

namespace RawDeal
{
    public class ListCard : IEnumerable<ICard>
    {
        private List<ICard> cardInstances = new List<ICard>();

        public void AddCard(ICard carta)
        {
            cardInstances.Add(carta);
        }

        public IEnumerator<ICard> GetEnumerator()
        {
            return cardInstances.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        
        public ICard GetCardById(int indice)
        {
            if (indice < 0 || indice >= cardInstances.Count)
            {
                throw new IndexOutOfRangeException();
            }
            return cardInstances[indice];
        }
        
        public void RemoveById(int indice)
        {
            if (indice < 0 || indice >= cardInstances.Count)
            {
                throw new IndexOutOfRangeException();
            }
            cardInstances.RemoveAt(indice);
        }
        
        public int NumberOfCards()
        {
            return cardInstances.Count;
        }
        
        public void Clear()
        {
            cardInstances.Clear();
        }
        
        public void RemoveRange(int startIndex, int count)
        {
            if (startIndex < 0 || startIndex + count > cardInstances.Count)
            {
                throw new ArgumentOutOfRangeException();
            }
            cardInstances.RemoveRange(startIndex, count);
        }
        
        public void Insert(int indice, ICard carta)
        {
            if (indice < 0 || indice > cardInstances.Count)
            {
                throw new IndexOutOfRangeException();
            }
            cardInstances.Insert(indice, carta);
        }
        
        public int IdOfCard(ICard carta)
        {
            return cardInstances.IndexOf(carta);
        }

        
      






    }
}