using ECS.Events;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Systems
{
	public class SpawnBulletSystem : IEcsRunSystem
	{
		private readonly EcsFilter<SpawnBulletEvent> _filter = null;

		public void Run()
		{
			foreach (var i in _filter)
			{
				ref var rootTransform = ref _filter.Get1(i).RootTransform;
				ref var prefab = ref _filter.Get1(i).Prefab;

				if (rootTransform == null || prefab == null) continue;

				var spawnedBullet = Pool.Current.Get(prefab);
				var spawnedBulletTransform = spawnedBullet.transform;
				var rotation = rootTransform.rotation;
				spawnedBulletTransform.position = rootTransform.position;
				spawnedBulletTransform.rotation = Quaternion.Euler(
					rotation.eulerAngles.x,
					rotation.eulerAngles.y,
					rotation.eulerAngles.z + -90 + Random.Range(-1f, 1f));
			}
		}
	}
}