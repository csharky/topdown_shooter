using ECS.Components;
using ECS.Components.Tags;
using ECS.Events;
using Leopotam.Ecs;

namespace ECS.Systems
{
	public class ProjectileEnemyCollisionInteractSystem : IEcsRunSystem
	{
		private readonly EcsWorld                                          _world       = null;
		private readonly EcsFilter<ProjectileTag, ProjectileCollidedEvent> _projectiles = null;
		private readonly EcsFilter<EnemyTag, ProjectileCollidedEvent>      _enemies     = null;

		public void Run()
		{
			foreach (var projectileIdx in _projectiles)
			{
				ref var projectileCollidedEntities = ref _projectiles.Get2(projectileIdx).Entities;
				ref var projectileTransform = ref _projectiles.Get2(projectileIdx).ProjectileTransform;

				foreach (var projectileCollidedEntity in projectileCollidedEntities)
				{
					foreach (var enemyIdx in _enemies)
					{
						ref var enemyEntity = ref _enemies.GetEntity(enemyIdx);

						if (projectileCollidedEntity.Item1 != enemyEntity) continue;

						enemyEntity.Get<DeadComponent>();
						enemyEntity.Get<BecomeDeadEvent>();
						
						_world.NewEntity().Get<SpawnBloodSplatterEffectEvent>() = new SpawnBloodSplatterEffectEvent()
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