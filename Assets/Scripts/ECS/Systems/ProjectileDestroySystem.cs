using System.Collections.Generic;
using System.Linq;
using ECS.Components;
using ECS.Events;
using ECS.Shared;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Systems
{
	public class ProjectileDestroySystem : IEcsRunSystem
	{
		private readonly SharedBulletData _sharedData = null;

		private readonly EcsFilter<TransformComponent, HealthComponent, ProjectileDestroyEvent, ProjectileDetectionComponent>
			_filter = null;

		public void Run()
		{
			foreach (var i in _filter)
			{
				ref var entity = ref _filter.GetEntity(i);
				ref var transform = ref _filter.Get1(i).transform;
				ref var health = ref _filter.Get2(i).Health;
				ref var data = ref _filter.Get3(i).Data;
				ref var raycastHead = ref _filter.Get4(i).detectionRoot;

				var diff = raycastHead.position - transform.position;
				transform.position = data.Item2.point - diff.ToVector2();
				//transform.gameObject.SetActive(false);
				health = _sharedData.Health;

				entity.Get<ProjectileTurnOffEvent>();
			}
		}
	}
}