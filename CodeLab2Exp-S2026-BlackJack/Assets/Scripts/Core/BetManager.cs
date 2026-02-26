using UnityEngine;

namespace Core
{
    public class BetManager : MonoBehaviour
    {
        [Header("Bet Settings")]
        public int cash = 100;
        public int currentBet = 0;
        
        #region Singleton

        public static BetManager Instance;
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        #endregion

        public void LoseBet()
        {
            cash -= currentBet*2;
        }

        public void WinBet()
        {
            cash += currentBet*2;
        }

        public void PushBet()
        {
            //do nothing, currently
        }

        public void BlackJack()
        {
            cash += Mathf.RoundToInt(currentBet*2.5f);
        }
    }
}
