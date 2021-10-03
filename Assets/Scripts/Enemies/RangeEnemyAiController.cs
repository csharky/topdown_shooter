using UnityEngine;

namespace Enemies
{
    public class RangeEnemyAiController : EnemyAiController
    {
        private bool _isAttacking = false;
        
        protected override void OnCalculatedMoveToPlayer(Vector2 nextPosition)
        {
        }

        protected override void OnReachedPlayer()
        {
            base.OnReachedPlayer();
        }

        protected override void OnTargetOnSight()
        {
            heroStateController.SetState(HeroStateController.State.Stay | HeroStateController.State.Attack);
        }

        protected override void OnLoosingTargetFromSight()
        {
            heroStateController.SetState(HeroStateController.State.Stay);
        }

        protected override void OnLoosingTarget()
        {
            heroStateController.SetState(HeroStateController.State.Move);
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
            if (!weaponEquipped) return;
            var fired = weaponController.Fire();
        }
    }
}