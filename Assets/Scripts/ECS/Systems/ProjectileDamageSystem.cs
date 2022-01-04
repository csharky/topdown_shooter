using System.Collections.Generic;
using System.Linq;
using ECS.Components;
using ECS.Events;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Systems
{
	public class ProjectileDamageSystem : IEcsRunSystem
	{
		private readonly EcsFilter<TransformComponent, ProjectileTag, HealthComponent, DamageEvent> _projectiles    = null;

		public void Run()
		{
			foreach (var projectileIdx in _projectiles)
			{
				ref var transform = ref _projectiles.Get1(projectileIdx).transform;
				
				if (!transform.gameObject.activeSelf) continue;

				ref var health = ref _projectiles.Get3(projectileIdx).Health;
				ref var damage = ref _projectiles.Get4(projectileIdx).Damage;
				health -= damage;

				if (health < 0)
				{
					ref var entity = ref _projectiles.GetEntity(projectileIdx);
					entity.Get<ProjectileDestroyEvent>();
				}
			}
		}
	}
	
}