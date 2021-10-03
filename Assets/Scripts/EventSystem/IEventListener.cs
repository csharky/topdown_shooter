using System;

namespace EventSystem
{
    public interface IEventListener : IDisposable
    {
    }
    public interface IEventListener<in T> : IEventListener where T : IEventData
    {
        void Invoke(T eventData);
    }
}