using ECS.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Systems
{
	public class TransformMoveMinusRightSystem : IEcsRunSystem
	{
		private readonly EcsFilter<TransformComponent, MoveSpeedData, MoveMinusRightComponent>.Exclude<RigidbodyComponent> _filter = null;

		public void Run()
		{
			foreach (var i in _filter)
			{
				ref var transform = ref _filter.Get1(i).transform;
				ref var speed = ref _filter.Get2(i).speed;
				
				transform.position += -transform.right * speed * Time.deltaTime;
			}
		}
	}
}