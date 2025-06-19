using System.Text.Json;
using RawDeal.data;
using RawDeal.EffectPart1;

namespace RawDeal
{
    public class CardGenerator
    {
        private List<string> cardsWithoutEffects;
        private List<string> cardsThatForceYouToDiscardACard;
        private List<string> cardsReversalWithoutEffects;
        private List<string> cardsWithOtherEffects;
        
        public CardGenerator()
        {
            LoadCardNames();
        }
        
        private void LoadCardNames()
        {
            string filePath = Path.Combine("data", "cardNames.json");
            string json = File.ReadAllText(filePath);
            var cardNames = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(json);

            cardsWithoutEffects = cardNames["CardsWithoutEffects"];
            cardsThatForceYouToDiscardACard = cardNames["CardsThatForceYouToDiscardACard"];
            cardsReversalWithoutEffects = cardNames["CardsReversalWithoutEffects"];
            cardsWithOtherEffects = cardNames["CardsWithOtherEffects"];
        }

        public ListCard GenerateCardsInstances()
        {
            var cardsData = ReadCardsDataFromFile();
            return CreateCardInstances(cardsData);
        }

        private List<CardData> ReadCardsDataFromFile()
        {
            string cardsPath = Path.Combine("data", "cards.json");
            string jsonCards = File.ReadAllText(cardsPath);
            return JsonSerializer.Deserialize<List<CardData>>(jsonCards);
        }

        private ListCard CreateCardInstances(List<CardData> cardsData)
        {
            ListCard cardInstances = new ListCard();
      
            
            foreach (var cardData in cardsData)
            {
                ICard cardInstance = CreateIndividualCardInstance(
                    cardData );
                cardInstances.AddCard(cardInstance);
            }

            return cardInstances;
        }
        
        private ICard CreateIndividualCardInstance(
            CardData cardData 
            )
        {
            ICard cardInstance;
             if (cardsWithoutEffects.Contains(cardData.Title))
            { 
                    cardInstance = new CardsWithoutEffects
                 {
                     Title = cardData.Title,
                     Types = cardData.Types,
                     Subtypes = cardData.Subtypes,
                     Fortitude = cardData.Fortitude,
                     Damage = cardData.Damage,
                     StunValue = cardData.StunValue,
                     CardEffect = cardData.CardEffect,
                 };
            }
            else if (cardsThatForceYouToDiscardACard.Contains(cardData.Title))
            { 
                cardInstance = new CardsThatForceYouToDiscardACard
                 {
                     Title = cardData.Title,
                     Types = cardData.Types,
                     Subtypes = cardData.Subtypes,
                     Fortitude = cardData.Fortitude,
                     Damage = cardData.Damage,
                     StunValue = cardData.StunValue,
                     CardEffect = cardData.CardEffect,
                 };
            }
            else if (cardsReversalWithoutEffects.Contains(cardData.Title))
            {
                cardInstance = new CardsReversalWithoutEffects
                 {
                     Title = cardData.Title,
                     Types = cardData.Types,
                     Subtypes = cardData.Subtypes,
                     Fortitude = cardData.Fortitude,
                     Damage = cardData.Damage,
                     StunValue = cardData.StunValue,
                     CardEffect = cardData.CardEffect,
                 };
            }
             else if ( cardData.Title ==  "Rolling Takedown")
            {
                cardInstance = new CardRollingTakedown 
                 {
                     Title = cardData.Title,
                     Types = cardData.Types,
                     Subtypes = cardData.Subtypes,
                     Fortitude = cardData.Fortitude,
                     Damage = cardData.Damage,
                     StunValue = cardData.StunValue,
                     CardEffect = cardData.CardEffect,
                 };
            }
            else if ( cardData.Title ==  "Knee to the Gut")
            {
                cardInstance = new CardKneeToTheGut 
                {
                    Title = cardData.Title,
                    Types = cardData.Types,
                    Subtypes = cardData.Subtypes,
                    Fortitude = cardData.Fortitude,
                    Damage = cardData.Damage,
                    StunValue = cardData.StunValue,
                    CardEffect = cardData.CardEffect,
                };
            }
             else if ( cardData.Title ==  "Elbow to the Face")
            {
                cardInstance = new CardElbowToTheFace 
                {
                    Title = cardData.Title,
                    Types = cardData.Types,
                    Subtypes = cardData.Subtypes,
                    Fortitude = cardData.Fortitude,
                    Damage = cardData.Damage,
                    StunValue = cardData.StunValue,
                    CardEffect = cardData.CardEffect,
                };
            }
            else if ( cardData.Title ==  "Manager Interferes")
            {
                cardInstance = new CardManagerInterference 
                {
                    Title = cardData.Title,
                    Types = cardData.Types,
                    Subtypes = cardData.Subtypes,
                    Fortitude = cardData.Fortitude,
                    Damage = cardData.Damage,
                    StunValue = cardData.StunValue,
                    CardEffect = cardData.CardEffect,
                };
            }
            else if ( cardData.Title ==  "Chyna Interferes")
            {
                cardInstance = new CardChynaInterference 
                {
                    Title = cardData.Title,
                    Types = cardData.Types,
                    Subtypes = cardData.Subtypes,
                    Fortitude = cardData.Fortitude,
                    Damage = cardData.Damage,
                    StunValue = cardData.StunValue,
                    CardEffect = cardData.CardEffect,
                };
            }
            else if ( cardData.Title == "Clean Break")
            {
                cardInstance = new CardCleanBreak 
                {
                    Title = cardData.Title,
                    Types = cardData.Types,
                    Subtypes = cardData.Subtypes,
                    Fortitude = cardData.Fortitude,
                    Damage = cardData.Damage,
                    StunValue = cardData.StunValue,
                    CardEffect = cardData.CardEffect,
                };
            }
            else if ( cardData.Title ==  "Jockeying for Position")
            {
                cardInstance = new CardJockeyingForPosition 
                {
                    Title = cardData.Title,
                    Types = cardData.Types,
                    Subtypes = cardData.Subtypes,
                    Fortitude = cardData.Fortitude,
                    Damage = cardData.Damage,
                    StunValue = cardData.StunValue,
                    CardEffect = cardData.CardEffect,
                };
            }
             else if ( cardData.Title ==  "Head Butt")
             {
                 cardInstance = new CardHeadButt 
                 {
                     Title = cardData.Title,
                     Types = cardData.Types,
                     Subtypes = cardData.Subtypes,
                     Fortitude = cardData.Fortitude,
                     Damage = cardData.Damage,
                     StunValue = cardData.StunValue,
                     CardEffect = cardData.CardEffect,
                 };
             }
             else if ( cardData.Title ==  "Arm Bar")
             {
                 cardInstance = new CardArmBar 
                 {
                     Title = cardData.Title,
                     Types = cardData.Types,
                     Subtypes = cardData.Subtypes,
                     Fortitude = cardData.Fortitude,
                     Damage = cardData.Damage,
                     StunValue = cardData.StunValue,
                     CardEffect = cardData.CardEffect,
                 };
             }
            else
            {
                cardInstance = new CardsWithOtherEffects
                 {
                     Title = cardData.Title,
                     Types = cardData.Types,
                     Subtypes = cardData.Subtypes,
                     Fortitude = cardData.Fortitude,
                     Damage = cardData.Damage,
                     StunValue = cardData.StunValue,
                     CardEffect = cardData.CardEffect,
                 };
            }
            return cardInstance;
        }

        public List<SuperStar> GenerateSuperStarsInstances()
        {
            string rutaSuperStar = Path.Combine("data", "superstar.json");
            string jsonSuperStar = File.ReadAllText(rutaSuperStar);
            return JsonSerializer.Deserialize<List<SuperStar>>(jsonSuperStar);
        }
    }
}
