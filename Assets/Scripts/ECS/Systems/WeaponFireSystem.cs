using ECS.Components;
using ECS.Events;
using Leopotam.Ecs;

namespace ECS.Systems
{
	public class WeaponFireSystem : IEcsRunSystem
	{
		private readonly EcsWorld                                                                _world  = null;
		private readonly EcsFilter<FireInputComponent, WeaponComponent>.Exclude<WeaponFireBlock> _filter = null;

		public void Run()
		{
			foreach (var i in _filter)
			{
				ref var isFiring = ref _filter.Get1(i).isActive;
				if (!isFiring) continue;
				
				ref var weapon = ref _filter.Get2(i);
				ref var entity = ref _filter.GetEntity(i);
				
				ref var transform = ref weapon.firingPositionTransform;
				ref var ammo = ref weapon.ammo;
				
				if (ammo <= 0)
				{
					continue;
				}
				
				ammo--;

				entity.Get<WeaponFireBlock>().timer = weapon.shootDelay;
				
				var newEntity = _world.NewEntity();
				newEntity.Get<SpawnBulletEvent>() = new SpawnBulletEvent()
				{
					RootTransform = transform,
					Prefab = weapon.bulletPrefab
				};
				newEntity.Get<PlaySoundEvent>() = new PlaySoundEvent()
				{
					type = AudioController.SoundType.Shoot_Rifle
				};
			}
		}
	}
}