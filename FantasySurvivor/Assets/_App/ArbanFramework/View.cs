using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
//using UnityEngine.UI;

namespace ArbanFramework.MVC
{
    public class DataChangedValue
    {
        public EventTypeBase eventType { get; protected set; }
        public string dataName { get; private set; }
        public object model { get; private set; }

        public DataChangedValue(EventTypeBase eventType, string dataName, object model = null)
        {
            this.eventType = eventType;
            this.dataName = dataName;
            this.model = model;
        }
    }

    public abstract class ViewBase : ObserverBase
    {
        public abstract class ComponentBase: IDisposable
        {
            public readonly string key;
            public readonly Component control;

            public ComponentBase(string key, Component control)
            {
                this.key = key;
                this.control = control;
            }

            public virtual void Dispose() { }

            public abstract void ReceiveEvent(EventBase eventBase);
        }

        public class DataBindingComponent<Control> : ComponentBase where Control: Component
        {
            public readonly Action<Control, EventBase> notifyCallback;

            public DataBindingComponent(string key, Component control, Action<Control, EventBase> notifyCallback) : base(key, control)
            {
                this.notifyCallback = notifyCallback;
            }

            public override void ReceiveEvent(EventBase eventBase)
            {
                if (control)
                    notifyCallback.Invoke(control as Control, eventBase);
            }
        }

        private bool _isInited = false;
        private Dictionary<EventTypeBase, List<ComponentBase>> _dataBindingComponentsDic = new Dictionary<EventTypeBase, List<ComponentBase>>();
        private Dictionary<string, ComponentBase> _componentDic = new Dictionary<string, ComponentBase>();
        private EventTypeBase _eventTypeNotify = new EventTypeBase("Notify");
        protected void ViewInit()
        {
            _dataBindingComponentsDic.Clear();
            _componentDic.Clear();
            OnViewInit();
            NotifyAllDataChanged();
            _isInited = true;
        }

        //protected void AddClick(Button button,UnityAction action)
        //{
        //    button.onClick.AddListener(action);
        //}

        protected ComponentBase AddDataBindingComponent<T>(string key, T component, Action<T,EventBase> notifyCallback, params EventTypeBase[] eventTypes) where T: Component
        {
            Debug.AssertFormat(component, "Control view key:{0}-{1} cannot null", key, GetType().Name);

            if(_componentDic.TryGetValue(key,out ComponentBase dataKeyBase))
                Debug.AssertFormat(false, "Control view key:{0}-{1} is dupplicate", key, GetType().Name);
            else
            {
                var dataBindingComponent = new DataBindingComponent<T>(key, component, notifyCallback);
                _componentDic.Add(key, dataBindingComponent);

                foreach(var eventType in eventTypes)
                {
                    RegisterEventListenner(eventType, dataBindingComponent);
                }
            }
            return dataKeyBase;
        }

        protected ComponentBase AddDataBinding<T>(string key, T component, Action<T, EventBase> notifyCallback, params DataChangedValue[] dataChangedValues) where T : Component
        {
            Debug.AssertFormat(component, "Control view key:{0}-{1} cannot null", key, GetType().Name);

            if (_componentDic.TryGetValue(key, out ComponentBase dataKeyBase))
                Debug.AssertFormat(false, "Control view key:{0}-{1} is dupplicate", key, GetType().Name);
            else
            {
                var dataBindingComponent = new DataBindingComponent<T>(key, component, notifyCallback);
                _componentDic.Add(key, dataBindingComponent);

                RegisterEventListenner(_eventTypeNotify, dataBindingComponent);

                foreach (var dataChangedValue in dataChangedValues)
                {
                    Action<DataChangedEvent> dataChangedEvent = (e) =>
                    {
                        if (dataChangedValue.model != null && e.sender != dataChangedValue.model)
                            return;

                        NotifyDataChanged(key);
                    };

                    ListenDataChanged(dataChangedValue.eventType, dataChangedValue.dataName, dataChangedEvent);
                }
            }

            return dataKeyBase;
        }

        protected void RegisterEventListenner(EventTypeBase eventType, ComponentBase componentBase)
        {
            if(!_dataBindingComponentsDic.TryGetValue(eventType,out List<ComponentBase> componentBases))
            {
                componentBases = new List<ComponentBase>();
                _dataBindingComponentsDic.Add(eventType, componentBases);
                Listen(eventType, ReceiveEvent);
            }

            componentBases.Add(componentBase);
        }

        private void ReceiveEvent(EventBase eventBase)
        {
            if (!_dataBindingComponentsDic.TryGetValue(eventBase.eventType, out List<ComponentBase> componentBases))
                return;

            foreach(var componentBase in componentBases)
            {
                componentBase.ReceiveEvent(eventBase);
            }
        }

        protected void NotifyDataChanged(string key)
        {
            if(!_componentDic.TryGetValue(key, out ComponentBase componentBase))
            {
                Debug.LogErrorFormat("Key notify {0} not found", key);
                return;
            }

            componentBase.ReceiveEvent(new EventBase(_eventTypeNotify, this));
        }

        protected void NotifyAllDataChanged()
        {
            foreach(var componentBase in _componentDic.Values)
            {
                componentBase.ReceiveEvent(new EventBase(_eventTypeNotify, this));
            }
        }

        protected virtual void OnViewInit() { }

        protected virtual void Start()
        {
            ViewInit();
        }

        protected virtual void OnEnable()
        {
            if(_isInited)
                NotifyAllDataChanged();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            if(_componentDic == null)
            {
                Debug.LogError("A");
            }    

            foreach(var componentBase in _componentDic.Values)
            {
                componentBase?.Dispose();
            }
            _componentDic.Clear();
            _dataBindingComponentsDic.Clear();
        }

        protected bool isInited => _isInited;
    }

    public class View<_App>: ViewBase where _App:AppBase
    {
        new public _App app { get { return base.app as _App; } }
        protected override AppBase GetApp() => Singleton<_App>.instance;
    }

}