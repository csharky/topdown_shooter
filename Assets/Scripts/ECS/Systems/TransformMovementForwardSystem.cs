using ECS.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Systems
{
	public class TransformMovementForwardSystem : IEcsRunSystem
	{
		private readonly EcsFilter<TransformComponent, MoveSpeedData, MoveForwardComponent>.Exclude<RigidbodyComponent> _filter = null;

		public void Run()
		{
			foreach (var i in _filter)
			{
				ref var transform = ref _filter.Get1(i).transform;
				ref var speed = ref _filter.Get2(i).speed;
				
				transform.Translate(1 * speed * Time.deltaTime, 0, 0);
			}
		}
	}
}