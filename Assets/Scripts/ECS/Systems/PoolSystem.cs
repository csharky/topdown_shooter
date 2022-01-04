using System;
using ECS.Components;
using ECS.Events;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Systems
{
	public class PoolSystem : IEcsRunSystem
	{
		private readonly EcsFilter<InstantiatePoolObjectEvent> _events = null;
		private readonly EcsFilter<PoolObjectComponent, TransformComponent> _pooledObjects = null;
		private readonly EcsFilter<PoolData> _pools = null;

		public void Run()
		{
			foreach (var i in _events)
			{
				ref var id = ref _events.Get1(i).id;
				ref var position = ref _events.Get1(i).position;
				ref var rotation = ref _events.Get1(i).rotation;
				ref var scale = ref _events.Get1(i).scale;

				foreach (var poolIdx in _pools)
				{
					ref var poolId = ref _pools.Get1(poolIdx).id;
					if (poolId != id) continue;
					
					ref var lastIdx = ref _pools.Get1(poolIdx).idx;
					ref var capacity = ref _pools.Get1(poolIdx).capacity;

					foreach (var objectIdx in _pooledObjects)
					{
						ref var pooledObjectId = ref _pooledObjects.Get1(objectIdx).id;
						ref var pooledObjectIdx = ref _pooledObjects.Get1(objectIdx).idx;

						if (pooledObjectId != id || lastIdx != pooledObjectIdx) continue;

						ref var transform = ref _pooledObjects.Get2(objectIdx).transform;
						transform.position = position;
						transform.rotation = rotation;
						transform.localScale = scale;

						ref var entity = ref _pooledObjects.GetEntity(objectIdx);
						entity.Get<BecomeActiveEvent>();
						 
						lastIdx++;
						lastIdx = (ushort) (lastIdx % capacity);
						break;
					}

					break;
				}

				ref var eventEntity = ref _events.GetEntity(i);
				eventEntity.Destroy();
			}
		}
	}
}