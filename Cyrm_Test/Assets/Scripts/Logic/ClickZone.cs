using UnityEngine;
using UnityEngine.EventSystems;

namespace Logic
{
    public abstract class ClickZone : MonoBehaviour, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            OnClick();
        }

        protected abstract void OnClick();
    }
}
