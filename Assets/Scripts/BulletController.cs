using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private Transform _raycastHead;

    public float Health
    {
        get => _health;
        set => _health = value;
    }
    
    public float Damage
    {
        get => _damage;
        set => _damage = value;
    }
    
    public float Speed
    {
        get => _speed;
        set => _speed = value;
    }
    
    private CollisionDetector _collisionDetector;
    private Transform _selfTransform;
    
    private object _destroyedBullet;
    [SerializeField] private float _health;
    [SerializeField] private float _damage;
    private float _speed;

    private void Awake()
    {
        _selfTransform = transform;
        OnEnable();
    }

    private void OnEnable()
    {
        _collisionDetector = new CollisionDetector();
        _collisionDetector.OnCollision += OnCollisionDetected;
        _collisionDetector.OnBefore += OnBefore;
        _speed = Speed;
    }

    private void FixedUpdate()
    {
        CheckCollision();
        Move();
    }

    private void CheckCollision()
    {
        var curPos = _selfTransform.position;
        var raycastHeadPos = _raycastHead.position;
        var direction = _selfTransform.right;
        _collisionDetector.Check(curPos, raycastHeadPos, direction, Speed);
    }

    private void Move()
    {
        _selfTransform.position = _selfTransform.position + _selfTransform.right * _speed * Time.deltaTime;
    }

    private void OnBefore(RaycastHit2D[] raycastHits)
    {
        foreach (var raycastHit2D in raycastHits)
        {
            var collider = raycastHit2D.collider;
            var collisionListener = collider.GetComponent<BulletCollisionListener>();
            if (collisionListener != null)
            {
                collisionListener.OnBeforeFrameCollision(this, raycastHit2D);
            }
            
            var bulletDestroyTrigger = collider.GetComponent<BulletDestroyTrigger>();
            if (bulletDestroyTrigger != null)
            {
                _health -= bulletDestroyTrigger.Damage;

                if (_health <= 0)
                {
                    _speed = 0;
                    var diff = _raycastHead.position - _selfTransform.position;
                    _selfTransform.position = raycastHit2D.point - diff.ToVector2();
                }
            }
        }
    }
    
    private void OnCollisionDetected(RaycastHit2D[] raycastHits)
    {
        foreach (var raycastHit2D in raycastHits)
        {
            var otherCollider = raycastHit2D.collider;
            //Debug.Log($"{name} collided with {otherCollider.name}");

            var collisionListener = otherCollider.GetComponent<BulletCollisionListener>();
            if (collisionListener != null)
            {
                collisionListener.OnCollision(this, raycastHit2D);
            }
            
            if (_health <= 0)
            {
                _speed = 0;
                _collisionDetector.Disable();
                gameObject.SetActive(false);
            }
        }
    }
}