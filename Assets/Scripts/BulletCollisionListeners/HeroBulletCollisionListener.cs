using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class HeroBulletCollisionListener : BulletCollisionListener
{
    [SerializeField] private Animator _bodyAnimator;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Collider2D _collider;
    [SerializeField] private Vector2 _flySpeedRange;
    [SerializeField] private Vector2 _flySpeedDecreaseRange;
    [SerializeField] private string _sortingLayerNameOnDead;
    [SerializeField] private HeroHealthController _healthController;
    [SerializeField] private HeroStateController _heroStateController;

    private float _deadFlySpeed;
    private float _flySpeedDecrease;
    private Transform _transform;
    private Vector2 _flyDirection;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _transform = GetComponent<Transform>();
    }

    public override void OnBeforeFrameCollision(BulletController bullet, RaycastHit2D raycastHit2D)
    {
        _flyDirection = bullet.transform.position.ToVector2() - raycastHit2D.point;
    }

    public override void OnCollision(BulletController bullet, RaycastHit2D raycastHit2D)
    {
        _healthController.Damage(bullet.Damage);

        SpawnBlood();
        
        if (_healthController.Health <= 0f)
        {
            _heroStateController.SetState(HeroStateController.State.Dead);
            _bodyAnimator.SetBool("Dead", true); 
            _collider.enabled = false;

            _deadFlySpeed = Random.Range(_flySpeedRange.x, _flySpeedRange.y);
            _flySpeedDecrease = Random.Range(_flySpeedDecreaseRange.x, _flySpeedDecreaseRange.y);

            _transform.right = -_flyDirection;

            enabled = false;
        }
    }

    private void SpawnBlood()
    {
    }

    private void FixedUpdate()
    {
        if (_deadFlySpeed <= 0.01f) return;

        _transform.position = _transform.position.ToVector2() + -_flyDirection * _deadFlySpeed * Time.deltaTime;
        _deadFlySpeed /= _flySpeedDecrease;

        if (_deadFlySpeed <= 0.01f)
        {
            _spriteRenderer.sortingLayerID = SortingLayer.NameToID(_sortingLayerNameOnDead);
        }
    }
}