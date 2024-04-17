using System;

namespace Model
{
    [Serializable]
    public class CoinModel
    {
        public int Collected;
        public event Action Changed;
        
        public void Add(int loot)
        {
            Collected += loot;
            Changed?.Invoke();
        }
    }
}