using UnityEngine;

public class HeroHealthController : MonoBehaviour
{
    [SerializeField] private HeroStateController _heroStateController;
    [SerializeField] private float _startupHealth;

    private float _health;

    private void Awake()
    {
        _health = _startupHealth;
    }

    public float Health
    {
        get => _health;
    }

    public void Damage(float damage)
    {
        _health -= damage;

        if (_health <= 0f)
        {
            _heroStateController.SetState(HeroStateController.State.Dead);
        }
    }
}