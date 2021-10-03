using UnityEngine;

namespace Enemies
{
    public class MeleeEnemyAiController : EnemyAiController
    {
        private bool _isAttacking = false;
        
        protected override void OnCalculatedMoveToPlayer(Vector2 nextPosition)
        {
            heroStateController.SetState(HeroStateController.State.Move);
        }

        protected override void OnReachedPlayer()
        {
            base.OnReachedPlayer();
            
            heroStateController.SetState(HeroStateController.State.Attack | HeroStateController.State.Move);
        }

        protected override void OnFixedTick()
        {
            if (heroStateController.CurrentState.HasFlag(HeroStateController.State.Attack))
            {
                Attack();
            }
        }

        private void Attack()
        {
        }
    }
}