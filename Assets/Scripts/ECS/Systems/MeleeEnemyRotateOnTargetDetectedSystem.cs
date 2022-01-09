using ECS.Components;
using ECS.Components.Tags;
using Leopotam.Ecs;

namespace ECS.Systems
{
	public class MeleeEnemyRotateOnTargetDetectedSystem : IEcsRunSystem
	{
		private readonly EcsFilter<EnemyTag, HasTarget, TargetOnViewSightEvent, TransformComponent, RotateDirectionData, MeleeTag>.Exclude<DeadComponent>
			_enemies = null;

		public void Run()
		{
			foreach (var enemyIdx in _enemies)
			{
				ref var target = ref _enemies.Get2(enemyIdx).target;
				ref var transform = ref _enemies.Get4(enemyIdx).transform;

				var currentPosition = transform.position.ToVector2();
				var playerPosition = target.Get<TransformComponent>().transform.position.ToVector2();

				ref var rotateDirection = ref _enemies.Get5(enemyIdx).direction;
				rotateDirection = playerPosition - currentPosition;
			}
		}
	}
}