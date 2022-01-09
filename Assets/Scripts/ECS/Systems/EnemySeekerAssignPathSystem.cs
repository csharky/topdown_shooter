using ECS.Components;
using Leopotam.Ecs;

namespace ECS.Systems
{
	public class EnemySeekerAssignPathSystem : IEcsRunSystem
	{
		private readonly EcsFilter<PathCalculatedEvent>
			_events = null;
		private readonly EcsFilter<SeekerComponent, PathComponent>
			_enemies = null;

		public void Run()
		{
			foreach (var eventIdx in _events)
			{
				ref var eventSeeker = ref _events.Get1(eventIdx).seeker;
				var eventPath = _events.Get1(eventIdx).path;
				
				foreach (var enemyIdx in _enemies)
				{
					ref var enemySeeker = ref _enemies.Get1(enemyIdx).seeker;
					
					if (eventSeeker != enemySeeker) continue;

					ref var enemyPath = ref _enemies.Get2(enemyIdx);

					enemyPath.path = eventPath;
					enemyPath.waypoint = 0;
					break;
				}	
			}
		}
	}
}