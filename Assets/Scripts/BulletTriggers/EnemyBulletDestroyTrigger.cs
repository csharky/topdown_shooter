using System;
using UnityEngine;

public class EnemyBulletDestroyTrigger : BulletDestroyTrigger
{
    [SerializeField] private HeroStateController _heroStateController;

    private void Awake()
    {
        _heroStateController.OnStateChanged += StateChanged;
    }

    private void StateChanged(HeroStateController.StateChangedArgs args)
    {
        if (args.CurrentState != HeroStateController.State.Dead) return;
        
        enabled = false;
        damage = 0;
    }
}