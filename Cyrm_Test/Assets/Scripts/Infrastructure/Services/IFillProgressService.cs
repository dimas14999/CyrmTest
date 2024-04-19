using System;
using Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Infrastructure.Services
{
    public interface IFillProgressService : IService
    {
        event Action OnStartProcess;
        event Action OnStopProcess;
        event Action<ItemData> OnCompleteProcess;

        void Init(Image fillImage, Button button, TMP_Text buttonText);
        void Process(ItemData itemData);
        void StartOrResumeFill(float fillTime);
        void CancelFill();
        void PauseFill();
    }
}
