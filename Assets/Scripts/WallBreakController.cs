using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class WallBreakController : MonoBehaviour
{
    [SerializeField] private Vector2 _speedRange;
    [SerializeField] private Vector2 _speedDecreaseRange;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private float _grayscaleOnStopped;

    private float _speed;
    private float _startSpeed;
    private float _speedDecrease;
    private Transform _transform;

    public bool DestroyOnStop = false;

    private void OnEnable()
    {
        _transform = transform;

        _speed = Random.Range(_speedRange.x, _speedRange.y);
        _startSpeed = _speed;
        
        _speedDecrease = Random.Range(_speedDecreaseRange.x, _speedDecreaseRange.y);
    }

    private void FixedUpdate()
    {
        if (_speed <= 0.01f) return;

        _transform.position = _transform.position + -_transform.right * _speed * Time.deltaTime;
        _speed /= _speedDecrease;

        //var grayscaleOnStopped = _grayscaleOnStopped + _speed/_startSpeed * (1-_grayscaleOnStopped);

        if (_speed <= 0.01f)
        {
            _spriteRenderer.color = new Color(
                _grayscaleOnStopped,
                _grayscaleOnStopped,
                _grayscaleOnStopped,
                1);

            if (DestroyOnStop)
            {
                gameObject.SetActive(false);
            }
        }
    }
}