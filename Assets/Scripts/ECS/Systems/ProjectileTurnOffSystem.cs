using ECS.Components;
using ECS.Events;
using ECS.Shared;
using Leopotam.Ecs;

namespace ECS.Systems
{
	public class ProjectileTurnOffSystem : IEcsRunSystem
	{
		private readonly SharedBulletData _sharedData = null;

		private readonly EcsFilter<TransformComponent, ProjectileTurnOffEvent>
			_filter = null;

		public void Run()
		{
			foreach (var i in _filter)
			{
				ref var entity = ref _filter.GetEntity(i);
				ref var transform = ref _filter.Get1(i).transform;
				transform.gameObject.SetActive(false);

				entity.Del<ProjectileTurnOffEvent>();
			}
		}
	}
}