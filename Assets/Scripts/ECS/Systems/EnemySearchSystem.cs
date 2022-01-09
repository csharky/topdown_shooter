using System;
using ECS.Components;
using ECS.Components.Tags;
using ECS.Events;
using ECS.Shared;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Systems
{
	public class EnemySearchSystem : IEcsRunSystem
	{
		private readonly SharedEnemySettings _enemySettings = null;

		private readonly EcsFilter<EnemyTag, HasTarget, SearchTargetComponent, TransformComponent, RotateDirectionData>.Exclude<DeadComponent>
			_enemies = null;

		public void Run()
		{
			foreach (var enemyIdx in _enemies)
			{
				ref var entity = ref _enemies.GetEntity(enemyIdx);
				ref var target = ref _enemies.Get2(enemyIdx).target;
				ref var transform = ref _enemies.Get4(enemyIdx).transform;

				var layerMask = _enemySettings.SearchCollideLayerMask;
				var currentPosition = transform.position.ToVector2();
				var playerPosition = target.Get<TransformComponent>().transform.position.ToVector2();

				var raycastHits = new RaycastHit2D[1];
				Physics2D.LinecastNonAlloc(currentPosition, playerPosition, raycastHits, layerMask);

				ref var raycastHit = ref raycastHits[0];
				if (raycastHit.collider != null &&
				    _enemySettings.PlayerLayerMask.Contains(raycastHit.collider.gameObject.layer))
				{
					ref var rotateDirection = ref _enemies.Get5(enemyIdx).direction;
					rotateDirection = playerPosition - currentPosition;

					entity.Get<TargetOnViewSightEvent>();
					entity.Get<TargetDetectedEvent>() = new TargetDetectedEvent()
					{
						timer = 2f,
					};
				}

				Debug.DrawLine(currentPosition, playerPosition, Color.red);
			}
		}
	}
}