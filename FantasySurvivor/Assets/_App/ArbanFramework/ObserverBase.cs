using System;
using System.Collections.Generic;
using UnityEngine;

namespace ArbanFramework.MVC
{
    public class EventListener
    {
        public EventTypeBase eventType { get; protected set; }
        public Action<EventBase> eventCallBack { get; protected set; }
        protected EventListener() { }

        public EventListener(EventTypeBase eventType, Action<EventBase> eventCallBack)
        {
            this.eventType = eventType;
            this.eventCallBack = eventCallBack;
        }
    }

    public class DataChangedListener: EventListener
    {
        public string dataName { get; private set; }
        public Action<DataChangedEvent> dataChangedEventCallBack { get; private set; }

        public DataChangedListener(EventTypeBase eventType, string dataName, Action<DataChangedEvent> dataChangedEventCallBack)
        {
            this.eventType = eventType;
            this.eventCallBack = OnEventCallBack;
            this.dataName = dataName;
            this.dataChangedEventCallBack = dataChangedEventCallBack;
        }

        private void OnEventCallBack(EventBase e)
        {
            var dataChangedEvent = e as DataChangedEvent;
            if (dataChangedEvent == null)
                return;
            if (dataChangedEvent.dataName != dataName)
                return;

            dataChangedEventCallBack?.Invoke(dataChangedEvent);
        }
    }

    public abstract class ObserverBase : MonoBehaviour
    {
        public AppBase app => GetApp();
        protected abstract AppBase GetApp();
        private List<EventListener> _eventListeners = new List<EventListener>();

        protected void ListenDataChanged(EventTypeBase eventType, string dataName, Action<DataChangedEvent> dataChangedEventCallBack)
        {
            var dataChangedListener = new DataChangedListener(eventType, dataName, dataChangedEventCallBack);
            _eventListeners.Add(dataChangedListener);
            app.eventManager.Listen(dataChangedListener.eventType, dataChangedListener.eventCallBack);
        }

        protected void Listen(EventTypeBase eventType, Action<EventBase> eventCallBack)
        {
            app.eventManager.Listen(eventType, eventCallBack);
            _eventListeners.Add(new EventListener(eventType, eventCallBack));
        }

        private void UnListen(EventTypeBase eventType, Action<EventBase> eventCallBack)
        {
            app.eventManager.UnListen(eventType, eventCallBack);
        }

        protected virtual void OnDestroy()
        {
            foreach(var listener in _eventListeners)
            {
                UnListen(listener.eventType, listener.eventCallBack);
            }

            _eventListeners.Clear();
        }
    }

    public class Controller<_App>: ObserverBase where _App: AppBase
    {
        new public _App app { get { return base.app as _App; } }

        protected override AppBase GetApp() => Singleton<_App>.instance;
    }

}