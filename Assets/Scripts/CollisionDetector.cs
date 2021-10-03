using System;
using System.Linq;
using UnityEngine;

public class CollisionDetector
{
    public Action<RaycastHit2D[]> OnCollision;
    public Action<RaycastHit2D[]> OnBefore;

    private bool _collideOnNextFrame;
    private RaycastHit2D[] _collideOnNextFrameRaycast;
    private bool _enabled = true;

    public void Check(Vector3 curPos, Vector3 raycastHeadPos, Vector3 direction, float speed)
    {
        if (!_enabled) return;
        
        if (_collideOnNextFrame)
        {
            OnCollision?.Invoke(_collideOnNextFrameRaycast);
        }
        
        _collideOnNextFrame = false;
        
        var diffBetweenRayAndCurPos = raycastHeadPos - curPos;
        var nextFramePos = curPos + direction * speed * Time.deltaTime;
        var distance = Vector2.Distance(raycastHeadPos, nextFramePos + diffBetweenRayAndCurPos);
        var results = new RaycastHit2D[5];
        
        Physics2D.RaycastNonAlloc(raycastHeadPos, direction, results, distance);
        
        //Debug.DrawLine(raycastHeadPos, raycastHeadPos + direction * distance, Color.red);
        results = results.Where(_ => _.collider != null).ToArray();
        _collideOnNextFrame = results.Any();

        if (_collideOnNextFrame)
        {
            _collideOnNextFrameRaycast = results;
            OnBefore?.Invoke(_collideOnNextFrameRaycast);
        }
    }

    public void Disable()
    {
        _enabled = false;
    }

    public void Enable()
    {
        _enabled = true;
    }
}