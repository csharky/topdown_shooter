using ECS.Components;
using ECS.Events;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Systems
{
	public class ProjectileWallCollisionInteractSystem : IEcsRunSystem
	{
		private readonly EcsWorld                                          _world       = null;
		private readonly EcsFilter<ProjectileTag, ProjectileCollidedEvent> _projectiles = null;
		private readonly EcsFilter<WallTag, ProjectileCollidedEvent>       _walls       = null;

		public void Run()
		{
			foreach (var projectileIdx in _projectiles)
			{
				ref var projectileCollidedEntities = ref _projectiles.Get2(projectileIdx).Entities;
				ref var projectileTransform = ref _projectiles.Get2(projectileIdx).ProjectileTransform;

				foreach (var projectileCollidedEntity in projectileCollidedEntities)
				{
					foreach (var wallIdx in _walls)
					{
						ref var wallEntity = ref _walls.GetEntity(wallIdx);

						if (projectileCollidedEntity.Item1 != wallEntity) continue;
					
						_projectiles.GetEntity(projectileIdx).Get<ProjectileDestroyEvent>() = new ProjectileDestroyEvent()
						{
							Data = projectileCollidedEntity
						};

						_world.NewEntity().Get<SpawnDestroyBulletEffectEvent>() = new SpawnDestroyBulletEffectEvent()
						{
							Data = projectileCollidedEntity,
							ProjectileTransform = projectileTransform
						};
						
						_world.NewEntity().Get<SpawnDestroyWallEffectEvent>() = new SpawnDestroyWallEffectEvent()
						{
							Data = projectileCollidedEntity,
							ProjectileTransform = projectileTransform
						};
					
						break;
					}	
				}
			}
		}
	}
}