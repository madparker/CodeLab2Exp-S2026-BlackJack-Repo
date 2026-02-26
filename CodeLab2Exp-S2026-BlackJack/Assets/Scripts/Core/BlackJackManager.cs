using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Core
{
	public class BlackJackManager : MonoBehaviour 
	{
		public static int Target = 21;
		
		public string loadScene;

		[Header("UI Elements")] 
		public Text statusText;
		[SerializeField] private Button tryAgain;
		[SerializeField] private Button hitButton;
		[SerializeField] private Button stayButton;
		[SerializeField] private Button betButton;
		[SerializeField] private Slider betSlider;
		[SerializeField] private Text betText;
		[SerializeField] private Text cashText;
		[SerializeField] private GameObject handHider;
		
		
		#region Win/Lose Conditions

		//PLAYER BUST: player loses by exceeding target
		public void PlayerBusted()
		{
			HidePlay();
			GameOverText("YOU BUST", Color.red);
			
			BetManager.Instance.LoseBet();
			cashText.text = "CASH: $" + BetManager.Instance.cash;
		}

		//DEALER BUST: player wins by dealer exceeding target
		public void DealerBusted()
		{
			HidePlay();
			GameOverText("DEALER BUSTS!", Color.green);
			
			BetManager.Instance.WinBet();
			cashText.text = "CASH: $" + BetManager.Instance.cash;
		}
		
		//PLAYER WIN: player wins by having a higher value than dealer
		public void PlayerWin()
		{
			HidePlay();
			GameOverText("YOU WIN!", Color.green);
			
			BetManager.Instance.WinBet();
			cashText.text = "CASH: $" + BetManager.Instance.cash;
		}
		
		//PLAYER LOSE: player loses by having a lower value than dealer
		public void PlayerLose()
		{
			HidePlay();
			GameOverText("YOU LOSE.", Color.red);
			
			BetManager.Instance.LoseBet();
			cashText.text = "CASH: $" + BetManager.Instance.cash;
		}

		//PLAYER PUSH: player pushes by having the same value as dealer
		public void PlayerPush()
		{
			HidePlay();
			GameOverText("PUSH!", Color.yellow);
			
			BetManager.Instance.PushBet();
			cashText.text = "CASH: $" + BetManager.Instance.cash;
		}

		//BLACK JACK: player wins by hitting black jack (A + any 10)!
		public void BlackJack()
		{
			HidePlay();
			GameOverText("Black Jack!", Color.green);
			
			BetManager.Instance.BlackJack();
			cashText.text = "CASH: $" + BetManager.Instance.cash;
		}

		#endregion

		private void OnEnable()
		{
			Reset();
		}

		public void Reset()
		{
			handHider.SetActive(true);
			HideAllUI();
			ShowBet();
		}

		#region UI Functions

		private void GameOverText(string str, Color color){
			statusText.text = str;
			statusText.color = color;

			tryAgain.gameObject.SetActive(true);
		}

		private void HideAllUI()
		{
			hitButton.gameObject.SetActive(false);
			stayButton.gameObject.SetActive(false);
			betButton.gameObject.SetActive(false);
			betSlider.gameObject.SetActive(false);
			tryAgain.gameObject.SetActive(false);
			handHider.SetActive(true);

		}

		//SHOW BET: show bet button and slider and set max value on slider
		private void ShowBet()
		{
			betButton.gameObject.SetActive(true);
			betSlider.gameObject.SetActive(true);
			betSlider.maxValue = BetManager.Instance.cash;
			BetManager.Instance.currentBet = 1;
			betText.text = "BET: $" + BetManager.Instance.currentBet;
			cashText.text = "CASH: $" + BetManager.Instance.cash;
		}
		
		public void SwitchBetToPlay()
		{
			betButton.gameObject.SetActive(false);
			betSlider.gameObject.SetActive(false);
			
			hitButton.gameObject.SetActive(true);
			stayButton.gameObject.SetActive(true);
			handHider.SetActive(false);

		}
		
		public void HidePlay()
		{
			hitButton.gameObject.SetActive(false);
			stayButton.gameObject.SetActive(false);
		}

		#endregion
		
		public void TryAgain()
		{
			Reset();
			SceneManager.LoadScene(loadScene);
		}

		public void SetBetValue(Single value)
		{
			BetManager.Instance.currentBet = (int)value;
			betText.text = "BET: $" + BetManager.Instance.currentBet;
		}

		public virtual int GetHandValue(List<DeckOfCards.Card> hand)
		{
			//check the high values of each cards
			int handValue = 0;
			foreach(DeckOfCards.Card handCard in hand){
				handValue += handCard.GetCardHighValue();
			}

			//if high value exceeds target, check low values
			if (handValue > Target)
			{
				handValue = 0;
				foreach(DeckOfCards.Card handCard in hand){
					handValue += handCard.GetCardLowValue();
				}
			}
		
			return handValue;
		}
	}
}
