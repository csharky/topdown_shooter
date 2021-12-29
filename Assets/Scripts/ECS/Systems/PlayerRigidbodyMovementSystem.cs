using ECS.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Systems
{
	public class PlayerRigidbodyMovementSystem : IEcsRunSystem
	{
		private readonly EcsFilter<PlayerTag, MoveDirectionData, RigidbodyComponent> _filter = null;

		public void Run()
		{
			foreach (var i in _filter)
			{
				ref var direction = ref _filter.Get2(i).direction;
				ref var rigidbody = ref _filter.Get3(i).rigidbody;
				
				var moveVector = direction.Clamp(1f);
        
				if (moveVector.sqrMagnitude > 0f)
					rigidbody.MovePosition(rigidbody.position + moveVector * 5 * Time.deltaTime);
			}
		}
	}
}