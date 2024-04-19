using Infrastructure;
using Infrastructure.Services;
using Logic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public abstract class BasePopupView : MonoBehaviour
    {
        [SerializeField] protected Button _startButton;
        [SerializeField] protected TMP_Text _buttonText;
        [SerializeField] protected Image _fillImage;
       // [SerializeField] protected FillProcess _fillProcess;
        [SerializeField] private Button _closeButton;

        public IFillProgressService FillProcess => _fillProgressService;

        protected IFillProgressService _fillProgressService;
        private ICoroutineRunner _coroutineRunner;
        public void Construct(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
            _fillProgressService = new FillProgressService(_coroutineRunner);
        }
        
        protected virtual void Start()
        {
            _fillProgressService.Init(_fillImage, _startButton, _buttonText);
        }
        
        protected virtual void OnEnable()
        {
            _startButton.onClick.AddListener(StartProcessHandler);
            _closeButton.onClick.AddListener(CloseHandler);
        }
        protected virtual void OnDisable()
        {
            _startButton.onClick.RemoveListener(StartProcessHandler);
            _closeButton.onClick.RemoveListener(CloseHandler);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }
        
        public void Hide()
        {
           // _fillProgressService.CancelFill();
            gameObject.SetActive(false);
        }

        private void CloseHandler()
        {
            Hide();
        }

        protected abstract void StartProcessHandler();
    }
}