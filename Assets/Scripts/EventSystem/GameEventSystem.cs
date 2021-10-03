using System;
using System.Collections.Generic;
using UnityEngine;

namespace EventSystem
{
    public class GameEventSystem : MonoBehaviour
    {
        public static GameEventSystem current
        {
            get
            {
                if (_current != null) return _current;
                
                var obj = new GameObject("GameEventSystem");
                var component = obj.AddComponent<GameEventSystem>();
                return component;
            }
        }

        private static GameEventSystem _current;

        private IDictionary<Type, ISet<IEventListener>> m_Listeners = new Dictionary<Type, ISet<IEventListener>>();

        private void Awake()
        {
            if (_current == null)
                _current = this;
            else
                Destroy(gameObject);
            
            DontDestroyOnLoad(this.gameObject);
        }
        
        public void Fire<T>(T eventData) where T : IEventData
        {
            var type = typeof(T);
            if (!m_Listeners.ContainsKey(type)) return;
            
            foreach (var listenerObj in m_Listeners[type])
            {
                if (listenerObj == null) continue;
                var listener = (IEventListener<T>) listenerObj;
                listener.Invoke(eventData);
            }
        }

        public void Register<T>(IEventListener<T> listener) where T : IEventData
        {
            var type = typeof(T);
            if (!m_Listeners.ContainsKey(type))
                m_Listeners.Add(type, new HashSet<IEventListener>());

            m_Listeners[type].Add(listener);
        }

        public void Unregister<T>(IEventListener<T> listener) where T : IEventData
        {
            var type = typeof(T);
            if (!m_Listeners.ContainsKey(type)) return;

            m_Listeners[type].Remove(listener);
        }
    }
}