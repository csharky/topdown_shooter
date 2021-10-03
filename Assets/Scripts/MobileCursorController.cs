using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.Serialization;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class MobileCursorController : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField] private float _deadZone;

    private Transform _transform;
    private Vector3 _screenMousePosition;
    private Camera _cam;
    private bool _wasPressed;

    private void Awake()
    {
        _transform = transform;
        _cam = Camera.main;

        if (Application.isMobilePlatform)
        {
            EnhancedTouchSupport.Enable();
        }
    }

    private void Start()
    {
        gameObject.SetActive(Application.isMobilePlatform);
    }

    private void LateUpdate()
    {
        var direction = InputHelper.MouseDelta() * _radius;
        if (direction.magnitude < _deadZone) return;
        
        var newPos = direction.ToVector3();
        _transform.localPosition = newPos;
    }
}