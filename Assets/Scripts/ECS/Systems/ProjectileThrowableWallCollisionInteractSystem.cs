using ECS.Components;
using ECS.Events;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Systems
{
	public class ProjectileThrowableWallCollisionInteractSystem : IEcsRunSystem
	{
		private readonly EcsWorld                                                              _world       = null;
		private readonly EcsFilter<ProjectileTag, ProjectileCollidedEvent>.Exclude<CollideThrowableWallBlock>
			_projectiles = null;
		private readonly EcsFilter<ThrowableWallTag, ProjectileCollidedEvent, DamageComponent> _walls       = null;

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
						ref var projectileEntity = ref _projectiles.GetEntity(projectileIdx);

						if (projectileCollidedEntity.Item1 != wallEntity) continue;
						ref var wallDamage = ref _walls.Get3(wallIdx).Damage;

						projectileEntity.Get<CollideThrowableWallBlock>() = new CollideThrowableWallBlock()
						{
							framesCount = 1
						};
						projectileEntity.Get<DamageEvent>() = new DamageEvent()
						{
							Damage = wallDamage
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