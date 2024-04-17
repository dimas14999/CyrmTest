using Data;
using Model;
using UnityEngine;
using View;

namespace Logic
{
    public abstract class BaseBuilding : ClickZone
    {
        [SerializeField] private int _processSeconds;
        
        private WorldModel _worldModel;
        private BasePopupView _basePopupView;

        public void Init(BasePopupView basePopupView)
        {
            _basePopupView = basePopupView;
            _basePopupView.FillProcess.OnStartProcess += StartProcessHandler;
            _basePopupView.FillProcess.OnStopProcess += StopProcessHandler;
            _basePopupView.FillProcess.OnCompleteProcess += CompleteProcessHandler;
        }
        
        public void Construct(WorldModel worldModel)
        {
            _worldModel = worldModel;
        }
        
        private void OnDisable()
        {
            _basePopupView.FillProcess.OnStartProcess -= StartProcessHandler;
            _basePopupView.FillProcess.OnStopProcess -= StopProcessHandler;
            _basePopupView.FillProcess.OnCompleteProcess -= CompleteProcessHandler;
        }

        private void StartProcessHandler()
        {
            _basePopupView.FillProcess.StartOrResumeFill(_processSeconds);
        }

        private void StopProcessHandler()
        {
            _basePopupView.FillProcess.PauseFill();
        }
        
        private void CompleteProcessHandler(ItemData itemData)
        {
            _worldModel.LootModel.Collect(itemData, 1);
        }

        protected override void OnClick()
        {
            _basePopupView.Show();
        }
    }
}
