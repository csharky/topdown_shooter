using ECS.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Systems
{
	public class TargetDetectedTimerSystem : IEcsRunSystem
	{
		private readonly EcsFilter<TargetDetectedEvent> _filter = null;
		public void Run()
		{
			var deltaTime = Time.deltaTime;
			foreach (var i in _filter)
			{
				ref var entity = ref _filter.GetEntity(i);
				ref var block = ref _filter.Get1(i);
				block.timer -= deltaTime;
				
				if (block.timer <= 0f) entity.Del<TargetDetectedEvent>();
			}
		}
	}
}