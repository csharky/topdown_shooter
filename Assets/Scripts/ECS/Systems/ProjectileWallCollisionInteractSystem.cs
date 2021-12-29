using ECS.Components;
using ECS.Events;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Systems
{
	public class ProjectileWallCollisionInteractSystem : IEcsRunSystem
	{
		private readonly EcsFilter<ProjectileTag, ProjectileCollidedEvent> _projectiles = null;

		public void Run()
		{
			foreach (var i in _projectiles)
			{
				Debug.Log("Projectile Collided with wall");
				_projectiles.GetEntity(i).Get<ProjectileDestroyEvent>();
				_projectiles.GetEntity(i).Del<ProjectileCollidedEvent>();
			}
		}
	}
}