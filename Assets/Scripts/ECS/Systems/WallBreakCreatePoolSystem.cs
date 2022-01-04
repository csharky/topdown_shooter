using ECS.Components;
using ECS.Shared;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Systems
{
	public class WallBreakCreatePoolSystem : IEcsInitSystem
	{
		private readonly EcsWorld                                _world            = null;
		private readonly SharedDestroyWallEffectData             _sharedEffectData = null;
		private readonly EcsFilter<DestroyedWallEffectComponent> _wallEffect       = null;

		public void Init()
		{
			foreach (var i in _wallEffect)
			{
				ref var effectPrefabs = ref _wallEffect.Get1(i).prefabs;

				foreach (var prefab in effectPrefabs)
				{
					var filter = _world.GetFilter(typeof(EcsFilter<PoolData>));
					var offset = 0;
					foreach (var poolIdx in filter)
					{
						ref var poolDataId = ref filter.GetEntity(poolIdx).Get<PoolData>().id;
						ref var poolDataCapacity = ref filter.GetEntity(poolIdx).Get<PoolData>().capacity;
						if (poolDataId != prefab.id) continue;
						
						poolDataCapacity += _sharedEffectData.PooledAmountPerPrefab;
						offset += _sharedEffectData.PooledAmountPerPrefab;
					}
					
					for (var j = 0 + offset; j < _sharedEffectData.PooledAmountPerPrefab + offset; j++)
					{
						var obj = Object.Instantiate(prefab);
						obj.transform.position = Vector3.one * 2000;
						obj.idx = j;
					}
					
					if (offset > 0) continue;

					var newPoolDataEntity = _world.NewEntity();
					var newPoolData = new PoolData()
					{
						id = prefab.id,
						idx = 0,
						capacity = _sharedEffectData.PooledAmountPerPrefab
					};

					newPoolDataEntity.Get<PoolData>() = newPoolData;
				}
			}
		}
	}
}