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

				entity.Get<WeaponFireBlock>().Timer = weapon.shootDelay;
				
				_world.NewEntity().Get<SpawnBulletEvent>() = new SpawnBulletEvent()
				{
					RootTransform = transform,
					Prefab = weapon.bulletPrefab
				};
			}
		}
	}
}