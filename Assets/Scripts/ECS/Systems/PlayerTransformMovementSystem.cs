using ECS.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Systems
{
	public class PlayerTransformMovementSystem : IEcsRunSystem
	{
		private readonly EcsFilter<PlayerTag, MoveDirectionData, TransformComponent>.Exclude<RigidbodyComponent> _filter = null;

		public void Run()
		{
			foreach (var i in _filter)
			{
				ref var direction = ref _filter.Get2(i).direction;
				ref var transform = ref _filter.Get3(i).transform;
				
				var moveVector = direction.Clamp(1f);
        
				if (moveVector.sqrMagnitude > 0f)
					transform.position = transform.position.ToVector2() + (moveVector * 5 * Time.deltaTime);
			}
		}
	}
}