using System;
using System.Collections;
using Data;
using Logic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace View
{
    public class ResourceBuildingPopupView : BasePopupView
    {
        [SerializeField] private ChoiceItem _choiceItem;

        protected override void OnEnable()
        {
            base.OnEnable();
            _choiceItem.OnSwitch += _fillProcess.CancelFill;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _choiceItem.OnSwitch -= _fillProcess.CancelFill;
        }

        protected override void StartProcessHandler()
        {
            _fillProcess.Process(_choiceItem.CurrentItem);
        }
    }
}
