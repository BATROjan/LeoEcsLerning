using System;

namespace Client.Coins
{
    public struct CoinStorage
    {
        public Action<int> OnAddCoin;
        public int CoinCount
        {
            get
            {
                return coinCount;
            }
            set
            {
                coinCount = value;
                OnAddCoin?.Invoke(coinCount);
            }
        }
        private int coinCount;
    }
}