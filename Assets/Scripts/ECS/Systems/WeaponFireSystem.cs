using ECS.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Systems
{
	public class WeaponFireSystem : IEcsRunSystem
	{
		private readonly EcsFilter<FireInputComponent, WeaponComponent>.Exclude<WeaponFireBlock> _filter = null;

		public void Run()
		{
			foreach (var i in _filter)
			{
				ref var isFiring = ref _filter.Get1(i).isActive;
				if (!isFiring) continue;
				
				ref var weapon = ref _filter.Get2(i);

				ref var entity = ref _filter.GetEntity(i);
				
				var spawnedBullet = Pool.Current.Get(weapon.bulletPrefab);
				var spawnedBulletTransform = spawnedBullet.transform;
				ref var transform = ref weapon.firingPositionTransform;
				var rotation = transform.rotation;
				spawnedBulletTransform.position = transform.position;
				spawnedBulletTransform.rotation = Quaternion.Euler(
					rotation.eulerAngles.x,
					rotation.eulerAngles.y,
					rotation.eulerAngles.z + -90 + Random.Range(-1f, 1f));

				entity.Get<WeaponFireBlock>().Timer = weapon.shootDelay;
			}
		}
	}
}