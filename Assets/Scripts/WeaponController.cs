using System;
using System.Linq;
using EventSystem;
using UnityEngine;
using Random = UnityEngine.Random;

public class WeaponController : MonoBehaviour
{
    public enum FiringType
    {
        Line, Range
    }

    public float Delay => _delay;
    public FiringType Type => _firingType;
    public int BulletsPerFire => _bulletsPerFire;
    public Vector2 BulletSpeedRange => _bulletSpeedRange;

    [SerializeField] private float _delay = 0.3f;
    [SerializeField] private FiringType _firingType = FiringType.Line;
    [SerializeField] private int _bulletsPerFire = 1;
    [SerializeField] private int _bulletsPoolSize;
    [SerializeField] private BulletController[] _bulletPrefabs;
    [SerializeField] private Vector2 _bulletSpeedRange;
    [SerializeField] private float _bulletsRangeBetween;
    [SerializeField] private float _bulletHealth;

    private Transform _transform;
    private float _fireDelay;

    private void Awake()
    {
        _transform = transform;
        
    }

    private void Start()
    {
        _bulletPrefabs.ToList().ForEach(_ => Pool.Current.AddToPool(_, _bulletsPoolSize));
    }

    public void FixedUpdate()
    {
        if (_fireDelay > 0)
        {
            _fireDelay -= Time.deltaTime;
        }
    }

    public bool Fire()
    {
        if (_fireDelay > 0) return false;

        _fireDelay = Delay;

        switch (Type)
        {
            case FiringType.Line:
            {
                var randomBullet = Random.Range(0, _bulletPrefabs.Length);

                var spawnedBullet = Pool.Current.Get(_bulletPrefabs[randomBullet]);
                var spawnedBulletTransform = spawnedBullet.transform;
                var rotation = _transform.rotation;
                spawnedBulletTransform.position = _transform.position;
                spawnedBulletTransform.rotation = Quaternion.Euler(
                    rotation.eulerAngles.x,
                    rotation.eulerAngles.y,
                    rotation.eulerAngles.z + -90 + Random.Range(-1f, 1f));
                
                spawnedBullet.Speed = Random.Range(BulletSpeedRange.x, BulletSpeedRange.y);
                spawnedBullet.Health = _bulletHealth;
                
                GameEventSystem.current.Fire(new PlayAudioEvent()
                {
                    Type = AudioController.SoundType.Shoot_Rifle
                });
                break;
            }
            case FiringType.Range:
            {
                for (int i = 0; i < BulletsPerFire; i++)
                {
                    var randomBullet = Random.Range(0, _bulletPrefabs.Length);

                    var spawnedBullet = Pool.Current.Get(_bulletPrefabs[randomBullet]);
                    var spawnedBulletTransform = spawnedBullet.transform;
                    var rotation = _transform.rotation;
                    var bulletDirection = 0f;
                    if (i > 0)
                        bulletDirection = Mathf.Pow(-1, i) * _bulletsRangeBetween * Mathf.Ceil(i / 2f);
                    
                    spawnedBulletTransform.position = _transform.position;
                    spawnedBulletTransform.rotation = Quaternion.Euler(
                        rotation.eulerAngles.x,
                        rotation.eulerAngles.y,
                        rotation.eulerAngles.z + -90 + bulletDirection );

                    spawnedBullet.Speed = Random.Range(BulletSpeedRange.x, BulletSpeedRange.y);
                    spawnedBullet.Health = _bulletHealth;
                }

                break;
            }
        }

        return true;
    }

}