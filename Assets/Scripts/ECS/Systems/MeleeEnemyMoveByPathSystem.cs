using ECS.Components;
using ECS.Components.Tags;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Systems
{
	public class MeleeEnemyMoveByPathSystem : IEcsRunSystem
	{
		private readonly EcsFilter<MeleeTag, EnemyTag, PathComponent, MoveDirectionData, TransformComponent>.Exclude<DeadComponent>
			_enemies = null;

		public void Run()
		{
			foreach (var enemyIdx in _enemies)
			{
				ref var path = ref _enemies.Get3(enemyIdx);
				ref var moveDirection = ref _enemies.Get4(enemyIdx).direction;
				ref var transform = ref _enemies.Get5(enemyIdx).transform;

				if (path.path == null)
				{
					moveDirection = Vector2.zero;
					continue;
				}
				
				var position = transform.position;
				var distanceBetweenPoints = Vector2.Distance(position, path.path.vectorPath[path.waypoint]);
				while (path.waypoint < path.path.vectorPath.Count - 1 && distanceBetweenPoints <= 0.1f)
				{
					path.waypoint++;
				}

				var direction = path.path.vectorPath[path.waypoint] - position;
				moveDirection.x = direction.x;
				moveDirection.y = direction.y;
			}
		}
	}
}