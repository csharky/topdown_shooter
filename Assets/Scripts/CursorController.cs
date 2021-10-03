using System.Linq;
using UnityEditor;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    [SerializeField] private float _joystickSense;
    [SerializeField] private float _mouseSense;
    [SerializeField] private Vector2 _maxDistanceRange;

    private Transform _transform;
    private Vector3 _screenMousePosition;

    private void Awake()
    {
        _transform = transform;
    }
    
    private void Start()
    {
        gameObject.SetActive(!Application.isMobilePlatform);

        if (gameObject.activeSelf)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void LateUpdate()
    {
        var localCenterToWorld = Vector2.zero;
        var currentPosition = _transform.localPosition;
        var direction = InputHelper.MouseDelta() * (InputHelper.IsGamepadConnected() ? _joystickSense : _mouseSense);
        var newPos = currentPosition + direction.ToVector3();

        var directionFromCenter = newPos.ToVector2() - localCenterToWorld;
        if (directionFromCenter.x > _maxDistanceRange.x) newPos = new Vector3(_maxDistanceRange.x, newPos.y, newPos.z);
        if (directionFromCenter.y > _maxDistanceRange.y) newPos = new Vector3(newPos.x, _maxDistanceRange.y, newPos.z);
        if (directionFromCenter.x < -_maxDistanceRange.x) newPos = new Vector3(-_maxDistanceRange.x, newPos.y, newPos.z);
        if (directionFromCenter.y < -_maxDistanceRange.y) newPos = new Vector3(newPos.x, -_maxDistanceRange.y, newPos.z);

        _transform.localPosition = newPos;
    }
}