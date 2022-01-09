using ECS.Components;
using ECS.Components.Tags;
using ECS.Shared;
using Leopotam.Ecs;
using Pathfinding;
using UnityEngine;

namespace ECS.Systems
{
	public class EnemySeekerStartPathSystem : IEcsRunSystem
	{
		private readonly EcsWorld _world = null;

		private readonly EcsFilter<EnemyTag, HasTarget, SeekerComponent, TransformComponent, TargetDetectedEvent>
			_enemies = null;

		public void Run()
		{
			foreach (var enemyIdx in _enemies)
			{
				ref var target = ref _enemies.Get2(enemyIdx).target;
				ref var transform = ref _enemies.Get4(enemyIdx).transform;
				var seeker = _enemies.Get3(enemyIdx).seeker;

				var currentPosition = transform.position.ToVector2();
				var playerPosition = target.Get<TransformComponent>().transform.position.ToVector2();

				var distance = Vector2.Distance(playerPosition, currentPosition);
				
				if (distance < 1f) continue;

				seeker.StartPath(currentPosition, playerPosition, (Path p) =>
				{
					var pathEvent = new PathCalculatedEvent
					{
						seeker = seeker,
						path = !p.error ? p : null
					};

					_world.NewEntity().Get<PathCalculatedEvent>() = pathEvent;
				});
			}
		}
	}
}