using ECS.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Systems
{
	public class WeaponFinishReloadSystem : IEcsRunSystem
	{
		private readonly EcsWorld                                                              _world        = null;
		private readonly EcsFilter<ReloadBlock, WeaponComponent>                               _reloadBlocks = null;

		public void Run()
		{
			var deltaTime = Time.deltaTime;
			
			foreach (var i in _reloadBlocks)
			{
				ref var entity = ref _reloadBlocks.GetEntity(i);
				ref var block = ref _reloadBlocks.Get1(i);
				ref var weapon = ref _reloadBlocks.Get2(i);

				if (block.timer > 0f)
				{
					block.timer -= deltaTime;
					continue;
				}

				weapon.ammo = weapon.ammoCapacity;

				entity.Del<ReloadBlock>();
			}
		}
	}
}