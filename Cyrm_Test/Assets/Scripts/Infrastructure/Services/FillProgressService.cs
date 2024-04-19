using System;
using System.Collections;
using Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Infrastructure.Services
{
    public class FillProgressService : IFillProgressService
    {
        public event Action OnStartProcess;
        public event Action OnStopProcess;
        public event Action<ItemData> OnCompleteProcess;

        private ItemData _currentItem;
        private bool _isProcessStarted;
        private Coroutine _fillCoroutine;
        private float _elapsedTime;
        private float _startFillAmount;
        private float _targetFillAmount;
        private Image _fillImage;
        private Button _startButton;
        private TMP_Text _startButtonText;
        private ICoroutineRunner _coroutineRunner;

        public FillProgressService(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }

        public void Init(Image fillImage, Button button, TMP_Text buttonText)
        {
            _fillImage = fillImage;
            _startButton = button;
            _startButtonText = buttonText;
            ResetFill();
            ChangeButton(Color.white, "Начать", _startButton, _startButtonText);
        }

        public void Process(ItemData itemData)
        {
            if (_isProcessStarted)
            {
                OnStopProcess?.Invoke();
                ChangeButton(Color.green, "Начать", _startButton, _startButtonText);
                _isProcessStarted = false;
                return;
            }
            
            if(itemData == null)
                return;

            _currentItem = itemData;
            OnStartProcess?.Invoke();
            ChangeButton(Color.red, "Стоп", _startButton, _startButtonText);
            _isProcessStarted = true;
        }
        
        public void StartOrResumeFill(float fillTime)
        {
            if (_fillCoroutine != null)
                _coroutineRunner.StopCoroutine(_fillCoroutine);
            
            if (_elapsedTime < fillTime)
            {
                _fillCoroutine = _coroutineRunner.StartCoroutine(FillImageOverTime(_elapsedTime, _startFillAmount, _targetFillAmount, fillTime));
            }
            else
            {
                ResetFill();
                _fillCoroutine = _coroutineRunner.StartCoroutine(FillImageOverTime(0f, 0f, 1f, fillTime));
            }
        }
        
        public void CancelFill()
        {
            PauseFill();
            ResetFill();
            ChangeButton(Color.white, "Начать", _startButton, _startButtonText);
            _isProcessStarted = false;
        }

        public void PauseFill()
        {
            if (_fillCoroutine != null)
                _coroutineRunner.StopCoroutine(_fillCoroutine);
        }
        
        private void ResetFill()
        {
            _fillImage.fillAmount = 0f;
            _elapsedTime = 0f;
            _startFillAmount = 0f;
            _targetFillAmount = 1f;
        }
        
        private void ChangeButton(Color color, string message, Button button, TMP_Text buttonText)
        {
            Image buttonImage = button.GetComponent<Image>();
            buttonImage.color = color;
            buttonText.text = message;
        }

        private IEnumerator FillImageOverTime(float startTime, float startFill, float targetFill, float fillTime)
        {
            _elapsedTime = startTime;
            _startFillAmount = startFill;
            _targetFillAmount = targetFill;

            while (_elapsedTime < fillTime)
            {
                _elapsedTime += Time.deltaTime;
                _fillImage.fillAmount = Mathf.Lerp(_startFillAmount, _targetFillAmount, _elapsedTime / fillTime);
                yield return null;
            }
            
            _fillImage.fillAmount = _targetFillAmount;
            
            ResetFill();
            _fillCoroutine = _coroutineRunner.StartCoroutine(FillImageOverTime(0f, 0f, 1f, fillTime));
            OnCompleteProcess?.Invoke(_currentItem);
        }

    }
}
