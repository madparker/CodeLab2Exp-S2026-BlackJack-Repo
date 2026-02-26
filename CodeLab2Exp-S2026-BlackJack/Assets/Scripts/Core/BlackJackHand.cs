using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
	public class BlackJackHand : MonoBehaviour {

		public Text total;
		public float xOffset;
		public float yOffset;
		public GameObject handBase;
		public int handVals;
		//button controller
		public Button stayButton;

		protected DeckOfCards deck;
		protected List<DeckOfCards.Card> hand;
		bool stay = false;
		
		void Start () 
		{
			SetupHand();
		}

		//SETUP HAND: give player two cards by hitting twice.
		protected virtual void SetupHand()
		{
			deck = GameObject.Find("Deck").GetComponent<DeckOfCards>();
			hand = new List<DeckOfCards.Card>();
			HitMe();
			HitMe();

			//trigger black jack condition if hand totals target
			if (GetHandValue() == BlackJackManager.Target)
			{
				GameObject.Find("BlackJackManager").GetComponent<BlackJackManager>().BlackJack();
			}
		}

		//HIT ME: draw a card, show it and add value to the players hand
		public void HitMe()
		{
			if(!stay)
			{
				DeckOfCards.Card card = deck.DrawCard();

				GameObject cardObj = Instantiate(Resources.Load("prefab/Card")) as GameObject;

				ShowCard(card, cardObj, hand.Count);

				hand.Add(card);

				ShowValue();
			}
		}

		//SHOW CARD: apply the info of the card to the cardObj game object
		protected void ShowCard(DeckOfCards.Card card, GameObject cardObj, int pos)
		{
			cardObj.name = card.ToString();

			cardObj.transform.SetParent(handBase.transform);
			cardObj.GetComponent<RectTransform>().localScale = new Vector2(1, 1);
			cardObj.GetComponent<RectTransform>().anchoredPosition = 
				new Vector2(
					xOffset + pos * 110, 
					yOffset);

			cardObj.GetComponentInChildren<Text>().text = deck.GetNumberString(card);
			cardObj.GetComponentsInChildren<Image>()[1].sprite = deck.GetSuitSprite(card);
		}

		//SHOW VALUE: calculates and displays hand value on screen
		protected virtual void ShowValue()
		{
			handVals = GetHandValue();
			
			total.text = "Player: " + handVals;

			//disable stay button if less than 16
			// if(handVals <= 16)
			// {
			// 	stayButton.interactable = false;
			// }
			// else
			// {
			// 	stayButton.interactable = true;
			// }

			if(handVals > BlackJackManager.Target)
			{
				GameObject.Find("BlackJackManager").GetComponent<BlackJackManager>().PlayerBusted();
			}

		}

		//GET HAND VALUE: gets hand value via the BlackJackManager, see use in ShowValue()
		protected int GetHandValue()
		{
			BlackJackManager manager = GameObject.Find("BlackJackManager").GetComponent<BlackJackManager>();

			return manager.GetHandValue(hand);
		}
	}
}
