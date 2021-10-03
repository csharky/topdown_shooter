using System;
using EventSystem;
using EventSystem.EventListeners;
using EventSystem.Events;
using UnityEngine;
using UnityEngine.InputSystem;

public class VirtualSticksController : MonoBehaviour, IGameStartListener, IEventListener<GameOver>
{
    [SerializeField] private bool m_debug;
    private void Start()
    {
        GameEventSystem.current.Register<GameStarted>(this);
        GameEventSystem.current.Register<GameOver>(this);
    }

    private void OnDestroy()
    {
        Dispose();
    }

    public void Invoke(GameOver eventData)
    {
        gameObject.SetActive(false);
    }

    public void Dispose()
    {
        InputSystem.onDeviceChange -= OnDeviceChange;
        GameEventSystem.current.Unregister<GameStarted>(this);
        GameEventSystem.current.Unregister<GameOver>(this);
    }

    private void OnDeviceChange(InputDevice device, InputDeviceChange state)
    {
        if (!Application.isMobilePlatform && !m_debug) return;
        var onlyVirtualGamepad = InputHelper.IsOnlyVirtualGamepadConnected();
        if (gameObject.activeSelf && onlyVirtualGamepad) return;

        gameObject.SetActive(!InputHelper.IsGamepadConnected() || onlyVirtualGamepad);
    }

    public void Invoke(GameStarted eventData)
    {
        if (!Application.isMobilePlatform && !m_debug) {
            gameObject.SetActive(false);
            return;
        }
        InputSystem.onDeviceChange += OnDeviceChange;
        gameObject.SetActive(InputHelper.IsOnlyVirtualGamepadConnected());
    }
}