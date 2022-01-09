using ECS.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Systems
{
	public class TransformMovementSystem : IEcsRunSystem
	{
		private readonly EcsFilter<MoveDirectionData, MoveTransformComponent, MoveSpeedData>.Exclude<RigidbodyComponent, DeadComponent> 
			_filter = null;

		public void Run()
		{
			foreach (var i in _filter)
			{
				ref var direction = ref _filter.Get1(i).direction;
				ref var transform = ref _filter.Get2(i).transform;
				ref var speed = ref _filter.Get3(i).speed;
				
				var moveVector = direction.Clamp(1f);
				var position = transform.position.ToVector2();
        
				if (moveVector.sqrMagnitude > 0f)
					transform.position = Vector2.Lerp(position, position + moveVector, Time.deltaTime * speed);
			}
		}
	}
}