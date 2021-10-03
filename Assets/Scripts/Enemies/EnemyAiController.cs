using System;
using System.Linq;
using Pathfinding;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyAiController : MonoBehaviour
{
    [SerializeField] private Transform _bodyTransform;
    [FormerlySerializedAs("_heroStateController")] 
    [SerializeField] 
    protected HeroStateController heroStateController;
    [SerializeField] private Seeker _seeker;
    [SerializeField] private LayerMask _raycastLayerMask;
    [SerializeField] private LayerMask _playerLayerMask;
    [SerializeField] private float _findDestinationRate;
    [SerializeField] private float _speed;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _nextWaypointDistance;
    [FormerlySerializedAs("_reachDistance")] 
    [SerializeField] private float _playerReachDistance;
    [SerializeField] private float _lostDelayTime;
    
    [SerializeField] protected WeaponController weaponController;

    [SerializeField] private Transform _sightTransform;
    [SerializeField] private float _sightAngle;

    protected PlayerController playerTarget;
    protected Transform playerTargetTransform;
    protected bool weaponEquipped;

    protected bool playerOnViewSight
    {
        get => _playerOnViewViewSight;
        set
        {
            _playerOnViewViewSight = value;
            //Debug.Log($"player on sight: {value}");
        }
    }

    private Transform _selfTransform;

    private Path _path;
    private DateTime _lastSeenDate;

    private int _currentWaypoint;
    private bool _isLosted;
    private bool _playerOnViewViewSight;

    private void Awake()
    {
        _selfTransform = transform;
        playerTarget = FindObjectOfType<PlayerController>();
        playerTargetTransform = playerTarget.transform;
        weaponEquipped = weaponController != null;
        
        InvokeRepeating(nameof(FindDestination), 0, _findDestinationRate);
    }

    private void FixedUpdate()
    {
        if (heroStateController.CurrentState == HeroStateController.State.Dead) return;

        Move();
        TickLostTimer();
        OnFixedTick();
    }

    private void TickLostTimer()
    {
        if (!_isLosted) return;
        if (!CanFindAfterLosing())
            _isLosted = false;
    }

    private void Move()
    {
        if (_path == null || heroStateController.CurrentState.HasFlag(HeroStateController.State.DontMove)) return;
        
        var currentPosition = _selfTransform.position;
        var playerPosition = playerTargetTransform.position;
        
        if (Vector2.Distance(currentPosition, playerPosition) <= _playerReachDistance)
        {
            OnReachedPlayer();
            return;
        }
        
        while (_currentWaypoint < _path.vectorPath.Count && 
               Vector2.Distance(currentPosition, _path.vectorPath[_currentWaypoint]) <= _nextWaypointDistance)
        {
            _currentWaypoint++;
        }
        
        if (_currentWaypoint >= _path.vectorPath.Count)
        {
            heroStateController.SetState(heroStateController.CurrentState | HeroStateController.State.Stay);
            _path = null;
            return;
        }

        var direction = (_path.vectorPath[_currentWaypoint] - currentPosition).normalized;
        //var rawNextPosition = currentPosition + direction * Time.deltaTime * _speed
        var nextPosition = Vector2.Lerp(currentPosition, _path.vectorPath[_currentWaypoint], Time.fixedDeltaTime * _speed);

        Rotate(playerOnViewSight ? playerPosition - currentPosition : direction);

        OnCalculatedMoveToPlayer(nextPosition);

        if (heroStateController.CurrentState.HasFlag(HeroStateController.State.Stay)) return;
        _selfTransform.position = nextPosition;
    }

    private void Rotate(Vector3 direction)
    {
        Debug.DrawRay(_selfTransform.position, direction, Color.cyan, Time.deltaTime);
        
        var requiredAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90f;
        var currentAngle = _bodyTransform.rotation.eulerAngles.z;
        var smoothAngle = Mathf.LerpAngle(currentAngle, requiredAngle, Time.fixedDeltaTime * _rotateSpeed);
        _bodyTransform.rotation = Quaternion.Euler(0, 0, smoothAngle);
    }

    private void FindDestination()
    {
        if (heroStateController.CurrentState == HeroStateController.State.Dead) return;

        var currentPosition = _selfTransform.position;
        var playerPosition = playerTargetTransform.position;

        var distance = Vector2.Distance(currentPosition, playerPosition) + 2.5f;
        var raycast = Physics2D.Raycast(
            currentPosition,
            (playerPosition - currentPosition).normalized,
            distance,
            _raycastLayerMask);
        
        Debug.DrawLine(currentPosition, currentPosition + (playerPosition - currentPosition).normalized * distance, Color.red);

        var raycastedOnlyPlayer =
            raycast.collider != null && _playerLayerMask.Contains(raycast.collider.gameObject.layer);
        var justLost = _isLosted && CanFindAfterLosing();

        if (!raycastedOnlyPlayer && 
            DateTime.UtcNow - _lastSeenDate <= TimeSpan.FromSeconds(_lostDelayTime) && 
            !_isLosted)
        {
            _isLosted = true;
        }
        
        if (raycastedOnlyPlayer || justLost)
        {
            _seeker.StartPath(currentPosition, playerPosition, OnPath);

            playerOnViewSight = !justLost;

            if (justLost)
            {
                OnLoosingTarget();
            }
            else
            {
                var angle = Vector2.Angle(playerPosition - currentPosition, _sightTransform.position - currentPosition);

                if (angle < _sightAngle * 0.5f)
                    OnTargetOnSight();
                else
                    OnLoosingTargetFromSight();
            }
            
            //_heroStateController.SetState(HeroStateController.State.MoveToPlayer);
        }
        
    }

    private void OnPath(Path p)
    {
        if (!p.error)
        {
            _path = p;
            _currentWaypoint = 0;

            if (!_isLosted)
            {
                _lastSeenDate = DateTime.UtcNow;
            }
            return;
        }

        _path = null;
        _currentWaypoint = 0;
    }

    private bool CanFindAfterLosing()
    {
        return DateTime.UtcNow - _lastSeenDate <= TimeSpan.FromSeconds(_lostDelayTime);
    }
    
    protected virtual void OnFixedTick()
    {
    }
    
    protected virtual void OnCalculatedMoveToPlayer(Vector2 nextPosition)
    {
    }
    
    protected virtual void OnTargetOnSight()
    {
    }
    
    protected virtual void OnLoosingTargetFromSight()
    {
    }
    
    protected virtual void OnLoosingTarget()
    {
    }
    
    protected virtual void OnReachedPlayer()
    {
        _path = null;
        _currentWaypoint = 0;
    }
}