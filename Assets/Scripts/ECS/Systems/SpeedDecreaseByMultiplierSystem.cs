using ECS.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Systems
{
	public class SpeedDecreaseByMultiplierSystem : IEcsRunSystem
	{
		private readonly EcsFilter<MoveSpeedData, SpeedDecreaseMultiplierComponent> _filter = null;

		public void Run()
		{
			foreach (var i in _filter)
			{
				ref var speed = ref _filter.Get1(i).speed;
				if (speed < 0.01f)
				{
					speed = 0f;
					continue;
				}
				
				ref var multiplier = ref _filter.Get2(i).multiplier;

				speed /= multiplier;
			}
		}
	}
}