using ECS.Components;
using ECS.Shared;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Systems
{
	public class BloodPartCreatePoolSystem : IEcsInitSystem
	{
		private readonly EcsWorld                                _world                = null;
		private readonly SharedBloodSplatterSettings             _sharedEffectSettings = null;

		public void Init()
		{
			ref var effectPrefabs = ref _sharedEffectSettings.prefabs;

			foreach (var prefab in effectPrefabs)
			{
				var filter = _world.GetFilter(typeof(EcsFilter<PoolData>));
				var poolExists = false;
				foreach (var poolIdx in filter)
				{
					ref var poolDataId = ref filter.GetEntity(poolIdx).Get<PoolData>().id;
					if (poolDataId != prefab.id) continue;

					poolExists = true;
				}

				if (poolExists) continue;

				for (var j = 0; j < _sharedEffectSettings.PooledAmountPerPrefab; j++)
				{
					var obj = Object.Instantiate(prefab);
					obj.transform.position = Vector3.one * 2000;
					obj.idx = j;
				}

				var newPoolDataEntity = _world.NewEntity();
				var newPoolData = new PoolData()
				{
					id = prefab.id,
					idx = 0,
					capacity = _sharedEffectSettings.PooledAmountPerPrefab
				};

				newPoolDataEntity.Get<PoolData>() = newPoolData;
			}
		}
	}
}