using ECS.Components;
using ECS.Components.Tags;
using ECS.Events;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Systems
{
	public class DisableComponentsOnBecomeDeadSystem : IEcsRunSystem
	{
		private readonly EcsFilter<ColliderComponent, MoveDirectionData, BecomeDeadEvent>
			_enemies = null;

		public void Run()
		{
			foreach (var enemyIdx in _enemies)
			{
				ref var moveDirection = ref _enemies.Get2(enemyIdx).direction;
				ref var collider = ref _enemies.Get1(enemyIdx).collider;

				moveDirection = Vector2.zero;
				collider.enabled = false;
			}
		}
	}
}