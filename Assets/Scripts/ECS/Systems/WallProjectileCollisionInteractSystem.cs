using ECS.Components;
using ECS.Events;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Systems
{
	public class WallProjectileCollisionInteractSystem : IEcsRunSystem
	{
		private readonly EcsFilter<WallTag, ProjectileCollidedEvent> _walls = null;

		public void Run()
		{
			foreach (var i in _walls)
			{
				Debug.Log("Wall Collided with Projectile");
				_walls.GetEntity(i).Del<ProjectileCollidedEvent>();
			}
		}
	}
}