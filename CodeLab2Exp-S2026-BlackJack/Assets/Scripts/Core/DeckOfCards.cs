using UnityEngine;
using UnityEngine.UI;

namespace Core
{
	public class DeckOfCards : MonoBehaviour {
	
		public Text cardNumUI;
		public Image cardImageUI;
		public Sprite[] cardSuits;

		//CARD: a card has a SUIT (Spades, Clubs, Diamonds, or Hearts) as well
		//as a TYPE (A, 1, 2, 3, etc.). Included in this class are constructors
		//for this class as well as some functions to parse this data into
		//game usable variables (i.e. TYPE -> VALUE)
		public class Card
		{
			public enum Suit {
				SPADES, 	//0
				CLUBS,		//1
				DIAMONDS,	//2
				HEARTS	 	//3
			};

			public enum Type {
				TWO		= 2,
				THREE	= 3,
				FOUR	= 4,
				FIVE	= 5,
				SIX		= 6,
				SEVEN	= 7,
				EIGHT	= 8,
				NINE	= 9,
				TEN		= 10,
				J		= 11,
				Q		= 12,
				K		= 13,
				A		= 14
			};

			public Type cardNum;
		
			public Suit suit;

			//CONSTRUCTOR: assigns a type and suit, see AddCardsToDeck()
			public Card(Type cardNum, Suit suit)
			{
				this.cardNum = cardNum;
				this.suit = suit;
			}

			//TO STRING: a function to parse the card data into a string.
			//This is never used.
			public override string ToString()
			{
				return "The " + cardNum + " of " + suit;
			}

			//GET HIGH VALUE: returns the high value of the card
			public int GetCardHighValue()
			{
				int val;

				switch(cardNum){
					case Type.A:
						val = 11;
						break;
					case Type.K:
					case Type.Q:
					case Type.J:
						val = 10;
						break;	
					default:
						val = (int)cardNum;
						break;
				}

				return val;
			}

			//GET LOW VALUE: returns the low value of the card
			public int GetCardLowValue()
			{
				int val;

				switch(cardNum){
					case Type.A:
						val = 1;
						break;
					case Type.K:
					case Type.Q:
					case Type.J:
						val = 10;
						break;	
					default:
						val = (int)cardNum;
						break;
				}

				return val;
			}
		}

		public static ShuffleBag<Card> deck;

		// Use this for initialization
		void Awake () 
		{
			if(!IsValidDeck())
			{
				deck = new ShuffleBag<Card>();

				AddCardsToDeck();
			}
		}

		protected virtual bool IsValidDeck()
		{
			return deck != null; 
		}

		protected virtual void AddCardsToDeck()
		{
			foreach (Card.Suit suit in Card.Suit.GetValues(typeof(Card.Suit)))
			{
				foreach (Card.Type type in Card.Type.GetValues(typeof(Card.Type)))
				{
					deck.Add(new Card(type, suit));
				}
			}
		}

		public virtual Card DrawCard()
		{
			Card nextCard = deck.Next();

			return nextCard;
		}


		public string GetNumberString(Card card)
		{
			if (card.cardNum.GetHashCode() <= 10)
			{
				return card.cardNum.GetHashCode() + "";
			}
			return card.cardNum + "";
		}
		
		public Sprite GetSuitSprite(Card card)
		{
			return cardSuits[card.suit.GetHashCode()];
		}
	}
}
