using Data;
using UnityEngine;
using View;

namespace Logic
{
    public class Market : ClickZone
    {
        private MarketPopupView _marketPopupView;
        
        public void Init(MarketPopupView marketPopupView)
        {
            _marketPopupView = marketPopupView;
        }

        protected override void OnClick()
        {
            _marketPopupView.Show();
        }
    }
}
