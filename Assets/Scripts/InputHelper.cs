using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XInput;

public static class InputHelper
{
    public static float IsFire()
    {
        if (IsGamepadConnected())
        {
            return Gamepad.current.rightTrigger.ReadValue();
        }
        else
        {
            return Mouse.current.leftButton.ReadValue();
        }
    }
    
    public static float IsReloading()
    {
        if (IsGamepadConnected())
        {
            return Gamepad.current.triangleButton.ReadValue();
        }
        else
        {
            return Keyboard.current.rKey.ReadValue();
        }
    }

    public static Vector2 MoveVector()
    {
        if (IsGamepadConnected())
        {
            return Gamepad.current.leftStick.ReadValue();
        }
        else
        {
            return new Vector2(
                Keyboard.current.aKey.ReadValue() * -1 + Keyboard.current.dKey.ReadValue(),
                Keyboard.current.sKey.ReadValue() * -1 + Keyboard.current.wKey.ReadValue());
        }
    }
    
    public static Vector2 MouseDelta()
    {
        if (IsGamepadConnected())
        {
            return Gamepad.current.rightStick.ReadValue();
        }
        else
        {
            return Mouse.current.delta.ReadValue();
        }
    }

    public static bool IsGamepadConnected()
    {
        return Gamepad.current != null;
    }

    public static bool IsOnlyVirtualGamepadConnected()
    {
        return !Gamepad.all.Any(_ =>
        {
            var contains = _.usages.Any(__ => __.ToLower().ToString().Contains("onscreen"));
            Debug.Log($"{_.displayName} contains OnScreen: {contains}");
            return !contains;
        });
    }
    

    private static bool IsMacOS()
    {
        return Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.OSXPlayer;
    }
}