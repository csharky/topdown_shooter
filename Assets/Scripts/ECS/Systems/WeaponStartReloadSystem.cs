using ECS.Components;
using ECS.Events;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Systems
{
	public class WeaponStartReloadSystem : IEcsRunSystem
	{
		private readonly EcsWorld                                                              _world  = null;
		private readonly EcsFilter<ReloadInputComponent, WeaponComponent>.Exclude<ReloadBlock> _filter = null;

		public void Run()
		{
			foreach (var i in _filter)
			{
				ref var isReloading = ref _filter.Get1(i).isActive;
				if (!isReloading) continue;
				
				ref var entity = ref _filter.GetEntity(i);
				ref var weapon = ref _filter.Get2(i);

				entity.Get<ReloadBlock>().timer = weapon.reloadDelay;
				entity.Get<WeaponFireBlock>().timer = weapon.reloadDelay;
				
				var newEntity = _world.NewEntity();
				newEntity.Get<PlaySoundEvent>() = new PlaySoundEvent()
				{
					type = AudioController.SoundType.Shoot_Rifle
				};
			}
		}
	}
}