using ArbanFramework;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace ArbanFramework
{
    public enum TouchEventType
    {
        Begin,
        Drag,
        End,
        Unknown,
    }

    public struct TouchEventArgs
    {
        public object sender;
        public TouchEventType type;
        public Vector3 location;
    }

    [System.Serializable]
    public class TouchEvent : UnityEvent<TouchEventArgs> { }

    public class InputTouchView : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
    {
        public TouchEvent onTouch;
        public Camera cam { get; private set; }

        private void Awake()
        {
            Singleton<InputTouchView>.Set(this);
        }

        private void OnDestroy()
        {
            RemoveAllListeners();
            Singleton<InputTouchView>.Unset(this);
        }

        public void RemoveAllListeners()
        {
            onTouch.RemoveAllListeners();
        }    

        public void OnDrag(PointerEventData eventData)
        {
            ConvertToWolrd(TouchEventType.Drag, Input.mousePosition);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            ConvertToWolrd(TouchEventType.Begin, Input.mousePosition);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            ConvertToWolrd(TouchEventType.End, Input.mousePosition);
        }

        private void ConvertToWolrd(TouchEventType touchEventType, Vector3 input)
        {
            var camera = this.cam ? this.cam : Camera.main;

            Vector3 posResult = camera.ScreenToWorldPoint(input);
            posResult.z = input.z;
            onTouch?.Invoke(new TouchEventArgs()
            {
                sender = this,
                location = posResult,
                type = touchEventType
            });
        }
    }
}
