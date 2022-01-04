using System.Collections.Generic;
using System.Linq;
using ECS.Components;
using ECS.Events;
using Leopotam.Ecs;
using NUnit.Framework;
using UnityEngine;

namespace ECS.Systems
{
	public class ProjectileCollisionDetectSystem : IEcsRunSystem
	{
		private readonly EcsFilter<TransformComponent, ProjectileDetectionComponent, MoveSpeedData> _projectiles    = null;
		private readonly EcsFilter<ColliderComponent>                                               _colliders = null;

		public void Run()
		{
			foreach (var projectileIdx in _projectiles)
			{
				ref var projectileEntity = ref _projectiles.GetEntity(projectileIdx);
				ref var transform = ref _projectiles.Get1(projectileIdx).transform;
				
				if (!transform.gameObject.activeSelf) continue;

				ref var root = ref _projectiles.Get2(projectileIdx).detectionRoot;
				ref var mask = ref _projectiles.Get2(projectileIdx).layerMask;
				ref var speed = ref _projectiles.Get3(projectileIdx).speed;

				var raycastHeadPos = root.position;
				var direction = transform.right;
				var position = transform.position;
				
				var moveDelta = direction * speed * Time.deltaTime;
				var results = new RaycastHit2D[5];
        
				Physics2D.LinecastNonAlloc(raycastHeadPos, raycastHeadPos + moveDelta, results, mask);
				Debug.DrawLine(raycastHeadPos, raycastHeadPos + moveDelta, Color.red);

				var entities = new List<(EcsEntity, RaycastHit2D)>();

				for (int j = 0; j < results.Length; j++)
				{
					var raycast = results[j];
					if (raycast.collider == null) continue;
					
					var otherCollider = raycast.collider;
					
					foreach (var colliderIdx in _colliders)
					{
						ref var collider = ref _colliders.Get1(colliderIdx).Collider;
						if (otherCollider != collider) continue;
						
						ref var colliderEntity = ref _colliders.GetEntity(colliderIdx);
						
						entities.Add((colliderEntity, raycast));
						
						colliderEntity.Get<ProjectileCollidedEvent>() = new ProjectileCollidedEvent()
						{
							Entities = new List<(EcsEntity, RaycastHit2D)>(){ (projectileEntity, raycast) }
						};
					}
				}
				
				projectileEntity.Get<ProjectileCollidedEvent>() = new ProjectileCollidedEvent()
				{
					Entities = entities,
					ProjectileTransform = transform
				};
			}
		}
	}
	
}