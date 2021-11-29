using EventSystem;
using EventSystem.Events;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed;

    [SerializeField] private Camera _camera;
    [SerializeField] private Animator _legsAnimator;
    [SerializeField] private Transform _bodyTransform;
    [SerializeField] private Transform _cursorTransform;
    [SerializeField] private UnityEngine.Rendering.Universal.Light2D _light;
    [SerializeField] private float _lightDelay;
    [SerializeField] private float _lightTurnOnIntensity;
    [SerializeField] private float _lightTurnOffIntensity;
    [SerializeField] private Color _lightTurnOnColor;
    [SerializeField] private Color _lightTurnOffColor;
    [SerializeField] private HeroStateController _heroStateController;
    
    private Rigidbody2D _rigidbody;
    private WeaponController _weaponController;
    private bool _weaponEquiped;
    private float _lightTurnOffDelay;

    private void Awake()
    {
        _camera = Camera.main;
        _rigidbody = GetComponent<Rigidbody2D>();
        _weaponController = GetComponentInChildren<WeaponController>();
        _weaponEquiped = _weaponController != null;
        
        _heroStateController.OnStateChanged += OnHeroStateChanged;
    }

    private void OnHeroStateChanged(HeroStateController.StateChangedArgs obj)
    {
        if (obj.CurrentState == HeroStateController.State.Dead)
        {
            _legsAnimator.SetFloat("SpeedX", 0);
            _legsAnimator.SetFloat("SpeedY", 0);
            
            GameEventSystem.current.Fire(new GameOver());
        }
    }

    private void Update()
    {
        if (_heroStateController.CurrentState == HeroStateController.State.Dead) return;
        
        Move();
        Fire();
        Rotate();
    }

    private void Fire()
    {
        var isFiring = InputHelper.IsFire() > 0.6f;
        if (!isFiring || !_weaponEquiped) return;
        var fired = _weaponController.Fire();

        if (fired)
        {
            _lightTurnOffDelay = _lightDelay;
            _light.color = _lightTurnOnColor;
        }
    }

    private void FixedUpdate()
    {
        if (_heroStateController.CurrentState == HeroStateController.State.Dead) return;
        
        CheckLight();
    }

    private void CheckLight()
    {
        _light.intensity = _lightTurnOffDelay > 0 ? _lightTurnOnIntensity : _lightTurnOffIntensity;
        
        if (_lightTurnOffDelay > 0)
            _lightTurnOffDelay -= Time.deltaTime;

        if (_lightTurnOffDelay <= 0)
        {
            _light.color = _lightTurnOffColor;
        }
    }

    private void Rotate()
    {
        var lookDir = CursorView.current.Transform.position - _bodyTransform.position;
        var angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg + 90f;
        _bodyTransform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void Move()
    {
        var moveVector = InputHelper.MoveVector();
        var moveRawX = moveVector.x;
        var moveRawY = moveVector.y;

        moveVector = moveVector.Clamp(1f);
        
        if (moveVector.sqrMagnitude > 0f)
            _rigidbody.MovePosition(_rigidbody.position + moveVector * moveSpeed * Time.deltaTime);

        _legsAnimator.SetFloat("SpeedX", Mathf.RoundToInt(moveRawX));
        _legsAnimator.SetFloat("SpeedY", Mathf.RoundToInt(moveRawY));
    }
}
