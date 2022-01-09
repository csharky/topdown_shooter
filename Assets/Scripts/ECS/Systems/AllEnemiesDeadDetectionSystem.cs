using ECS.Components;
using ECS.Components.Tags;
using Leopotam.Ecs;

namespace ECS.Systems
{
	public class AllEnemiesDeadDetectionSystem : IEcsRunSystem
	{
		private readonly EcsWorld                                   _world        = null;
		private readonly EcsFilter<EnemyTag>.Exclude<DeadComponent> _aliveEnemies = null;
		
		public void Run()
		{
			if (_aliveEnemies.IsEmpty())
			{
				_world.NewEntity().Get<AllEnemiesDeadEvent>();
			}
		}
	}
}