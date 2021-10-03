using System;
using TMPro;
using UnityEngine;

public class GameOverUiViewController : MonoBehaviour
{
    [SerializeField] private TextMeshProWithShadow _gamepadText;
    [SerializeField] private TextMeshProWithShadow _mobileText;
    [SerializeField] private TextMeshProWithShadow _keyboardText;

    private void Awake()
    {
        _mobileText.SetActive(Application.isMobilePlatform && InputHelper.IsOnlyVirtualGamepadConnected());
        _gamepadText.SetActive(!InputHelper.IsOnlyVirtualGamepadConnected());
        _keyboardText.SetActive(!Application.isMobilePlatform && InputHelper.IsOnlyVirtualGamepadConnected());
    }
}