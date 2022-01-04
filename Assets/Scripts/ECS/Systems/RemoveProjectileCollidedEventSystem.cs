using ECS.Events;
using Leopotam.Ecs;

namespace ECS.Systems
{
	internal class RemoveProjectileCollidedEventSystem : IEcsRunSystem
	{
		private readonly EcsFilter<ProjectileCollidedEvent> _events = null;

		public void Run()
		{
			foreach (var i in _events)
			{
				_events.GetEntity(i).Del<ProjectileCollidedEvent>();
			}
		}
	}
}