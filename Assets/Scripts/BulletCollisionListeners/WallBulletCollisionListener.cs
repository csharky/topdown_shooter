using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class WallBulletCollisionListener : BulletCollisionListener
{
    [SerializeField] private DestroyedBulletEffect _destroyedBulletPrefab;
    [SerializeField] private WallBreakController[] _destroyedWallPrefabs;
    [SerializeField] private int _destroyedBulletCount;
    [SerializeField] private float _destroyedBulletRayWidth;
    [SerializeField] private int _destroyedWallCount;
    [SerializeField] private int _destroyedWallInvertedCount;
    [SerializeField] private int _destroyedWallRotationStep;
    [SerializeField] private int _destroyedWallEachDestroy;
    [SerializeField] private Vector2 _destroyedBulletUpRange;
    [SerializeField] private Vector2 _destroyedBulletRightRange;
    [SerializeField] private Vector2 _destroyedWallUpRange;
    [SerializeField] private Vector2 _destroyedWallRightRange;
    
    private readonly List<GameObject> _destroyObjects = new List<GameObject>();
    private readonly List<DebugLine> _debugLines = new List<DebugLine>();

    private void Awake()
    {
    }

    private void Start()
    {
        _destroyedWallPrefabs.ToList().ForEach(Pool.Current.AddToPool);
        Pool.Current.AddToPool(_destroyedBulletPrefab);
    }

    public override void OnBeforeFrameCollision(BulletController bullet, RaycastHit2D raycastHit2D)
    {
        for (int i = 0; i < _destroyedBulletCount; i++)
        {
            var item = CreateDestroyBullet(bullet, i, raycastHit2D);
            if (item != null)
            {
                item.transform.localScale = Random.Range(1f, 1.5f) * Vector3.one;
            }
        }
        
        for (var k = 0; k < _destroyedWallCount; k++)
            CreateDestroyedWallEffect(bullet, k, raycastHit2D);
                
        for (var k = 0; k < _destroyedWallInvertedCount; k++)
            CreateDestroyedWallEffect(bullet, k, raycastHit2D, true);
    }

    public override void OnCollision(BulletController bullet, RaycastHit2D raycastHit2D)
    {
    }

    private DestroyedBulletEffect CreateDestroyBullet(BulletController bullet, int i, RaycastHit2D raycastHit2D)
    {
        var direction = 0f;
        var position = 0f;
        var positionRight = 0f;
        var scale = Random.Range(0.8f, 1.2f);
        if (i > 0)
        {
            direction = Random.Range(-1, 1) * 4 * Mathf.Ceil(i / 2f);
            position = Mathf.Pow(-1, i) * Random.Range(_destroyedBulletUpRange.x, _destroyedBulletUpRange.y) * Mathf.Ceil(i / 2f);
            positionRight = Random.Range(-1, 1) * Random.Range(_destroyedBulletRightRange.x, _destroyedBulletRightRange.y) * Mathf.Ceil(i / 2f);
        }
            
        var bulletTransform = bullet.transform;
        var currentPos = bulletTransform.position;
        var nextPos = raycastHit2D.point 
                      + bulletTransform.up.ToVector2() * position
                      + bulletTransform.right.ToVector2() * positionRight;
            
        var raycast = Physics2D.Raycast(nextPos, nextPos - currentPos.ToVector2(), _destroyedBulletRayWidth);
        _debugLines.Add(
            new DebugLine()
            {
                Start = nextPos, End = nextPos + (nextPos - currentPos.ToVector2()) * _destroyedBulletRayWidth
            });
            

        if (raycast.collider == null) return null;
            
        nextPos = raycast.point;

        var spawnedDestroyedBullet = Pool.Current.Get(_destroyedBulletPrefab);
        spawnedDestroyedBullet.gameObject.SetActive(false);
            
        var spawnedBulletTransform = spawnedDestroyedBullet.transform;
        var rotation = bulletTransform.rotation;

        spawnedBulletTransform.position = nextPos;
        spawnedBulletTransform.rotation = Quaternion.Euler(
            rotation.eulerAngles.x,
            rotation.eulerAngles.y,
            rotation.eulerAngles.z + direction );
        spawnedBulletTransform.localScale = Vector3.one * scale;

        //_destroyObjects.Add(spawnedDestroyedBullet.gameObject);
        StartCoroutine(ShowWithDelay(spawnedDestroyedBullet.gameObject));

        return spawnedDestroyedBullet;
    }

    private void CreateDestroyedWallEffect(BulletController bullet, int i, RaycastHit2D raycastHit2D, bool inverted = false)
    {
        var direction = 0f;
        var position = 0f;
        var positionRight = 0f;
        var scale = Random.Range(0.8f, 1.2f);
        if (i > 0)
        {
            direction = Mathf.Pow(-1, i) * _destroyedWallRotationStep * Mathf.Ceil(i / 2f);
            position = Mathf.Pow(-1, i) * Random.Range(_destroyedWallUpRange.x, _destroyedWallUpRange.y) * Mathf.Ceil(i / 2f);
            positionRight = Random.Range(-1, 1) * Random.Range(_destroyedWallRightRange.x, _destroyedWallRightRange.y) * Mathf.Ceil(i / 2f);
        }

        var bulletTransform = bullet.transform;
        var currentPos = bulletTransform.position;
        var nextPos = raycastHit2D.point 
                      + bulletTransform.up.ToVector2() * position
                      + bulletTransform.right.ToVector2() * positionRight;
            
        var spawnedDestroyedWall = Pool.Current.Get(_destroyedWallPrefabs[Random.Range(0, _destroyedWallPrefabs.Length)]);
        spawnedDestroyedWall.gameObject.SetActive(false);
            
        var spawnedBulletTransform = spawnedDestroyedWall.transform;
        var rotation = bulletTransform.rotation;

        spawnedBulletTransform.position = nextPos;
        spawnedBulletTransform.rotation = Quaternion.Euler(
            rotation.eulerAngles.x,
            rotation.eulerAngles.y,
            rotation.eulerAngles.z + direction + (inverted ? 180f : 0f));
        spawnedBulletTransform.localScale = Vector3.one * scale;

        //_destroyObjects.Add(spawnedDestroyedWall.gameObject);

        spawnedDestroyedWall.DestroyOnStop = _destroyedWallEachDestroy > 0 && i % _destroyedWallEachDestroy == 0;
            

        StartCoroutine(ShowWithDelay(spawnedDestroyedWall.gameObject));
    }

    private IEnumerator ShowWithDelay(GameObject obj)
    {
        yield return new WaitForSeconds(Time.deltaTime);
        
        obj.SetActive(true);
    }

    private void LateUpdate()
    {
        foreach (var line in _debugLines)
        {
            Debug.DrawLine(line.Start, line.End, Color.red);
        }
    }

    internal class DebugLine
    {
        public Vector2 Start;
        public Vector2 End;
    }
}