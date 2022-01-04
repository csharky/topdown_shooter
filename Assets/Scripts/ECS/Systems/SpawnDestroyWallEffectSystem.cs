using System.Collections;
using System.Collections.Generic;
using ECS.Components;
using ECS.Events;
using ECS.Shared;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Systems
{
	public class SpawnDestroyWallEffectSystem : IEcsRunSystem
	{
		private readonly EcsWorld                                _world            = null;
		private readonly SharedDestroyWallEffectData             _sharedEffectData = null;
		private readonly EcsFilter<SpawnDestroyWallEffectEvent>  _filter          = null;
		private readonly EcsFilter<DestroyedWallEffectComponent> _wallEffect      = null;
		
		public void Run()
		{
			foreach (var i in _filter)
			{
				ref var data = ref _filter.Get1(i).Data;
				ref var projectileTransform = ref _filter.Get1(i).ProjectileTransform;
				ref var targetWallEntity = ref data.Item1;

				foreach (var wallIdx in _wallEffect)
				{
					ref var wallEntity = ref _wallEffect.GetEntity(wallIdx);
					if (wallEntity != targetWallEntity) continue;
					ref var prefabs = ref _wallEffect.Get1(wallIdx).prefabs;
					ref var count = ref _wallEffect.Get1(wallIdx).count;

					for (int j = 0; j < count; j++)
					{
						var prefab = prefabs[Random.Range(0, prefabs.Length)];
						CreateDestroyedWallEffect(prefab, j, data.Item2, projectileTransform);
					}
				}
			}
		}

		private void CreateDestroyedWallEffect(PoolObjectComponentProvider prefab, int i, RaycastHit2D raycastHit2D, Transform projectileTransform, bool inverted = false)
		{
			var direction = 0f;
			var position = 0f;
			var positionRight = 0f;
			var scale = Random.Range(0.8f, 1.2f);
			if (i > 0)
			{
				direction = Mathf.Pow(-1, i) * _sharedEffectData.RotationStep * Mathf.Ceil(i / 2f);
				position = Mathf.Pow(-1, i) * Random.Range(_sharedEffectData.UpRange.x, _sharedEffectData.UpRange.y) * Mathf.Ceil(i / 2f);
				positionRight = Random.Range(-1, 1) * Random.Range(_sharedEffectData.RightRange.x, _sharedEffectData.RightRange.y) * Mathf.Ceil(i / 2f);
			}

			var bulletTransform = projectileTransform;
			var currentPos = bulletTransform.position;
			var nextPos = raycastHit2D.point 
			              + bulletTransform.up.ToVector2() * position
			              + bulletTransform.right.ToVector2() * positionRight;
			
			//spawnedDestroyedWall.SetActive(false);

			var rotation = bulletTransform.rotation;
			
			_world.NewEntity().Get<InstantiatePoolObjectEvent>() = new InstantiatePoolObjectEvent()
			{
				id = prefab.id,
				position = nextPos,
				rotation = Quaternion.Euler(
					rotation.eulerAngles.x,
					rotation.eulerAngles.y,
					rotation.eulerAngles.z + direction + (inverted ? 180f : 0f)),
				scale = Vector3.one * scale
			};

			//_destroyObjects.Add(spawnedDestroyedWall.gameObject);

		}
	}
}