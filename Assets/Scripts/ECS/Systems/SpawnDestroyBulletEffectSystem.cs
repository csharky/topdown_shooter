using System.Collections;
using System.Collections.Generic;
using ECS.Components;
using ECS.Events;
using ECS.Shared;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Systems
{
	public class SpawnDestroyBulletEffectSystem : IEcsRunSystem, IEcsInitSystem
	{
		//private readonly SharedDestroyWallEffectData        sharedEffectData = null;
		private readonly SharedDestroyBulletEffectData             sharedEffectData = null;
		private readonly EcsFilter<SpawnDestroyBulletEffectEvent>  _filter          = null;
		private readonly EcsFilter<DestroyedWallEffectComponent>   _wallEffect      = null;
		private readonly EcsFilter<DestroyedBulletEffectComponent> _bulletEffect    = null;

		public void Init()
		{
			foreach (var i in _bulletEffect)
			{
				ref var effectPrefab = ref _bulletEffect.Get1(i).prefab;

				Pool.Current.AddToPool(effectPrefab);
			}
		}
		
		public void Run()
		{
			foreach (var i in _filter)
			{
				ref var data = ref _filter.Get1(i).Data;
				ref var projectileTransform = ref _filter.Get1(i).ProjectileTransform;
				ref var targetWallEntity = ref data.Item1;

				foreach (var wallIdx in _bulletEffect)
				{
					ref var wallEntity = ref _bulletEffect.GetEntity(wallIdx);
					if (wallEntity != targetWallEntity) continue;
					ref var prefab = ref _bulletEffect.Get1(wallIdx).prefab;
					ref var count = ref _bulletEffect.Get1(wallIdx).count;

					for (int j = 0; j < count; j++)
					{
						var item = CreateDestroyBulletEffect(prefab, j, data.Item2, projectileTransform);
						if (item != null)
						{
							item.transform.localScale = Random.Range(1.25f, 1.55f) * Vector3.one;
						}
					}
				}
			}
		}

		private GameObject CreateDestroyBulletEffect(GameObject prefab, int i, RaycastHit2D raycastHit2D, Transform projectileTransform)
		{
			var direction = 0f;
			var position = 0f;
			var positionRight = 0f;
			var scale = Random.Range(0.8f, 1.2f);
			if (i > 0)
			{
				direction = Random.Range(-1, 1) * 4 * Mathf.Ceil(i / 2f);
				position = Mathf.Pow(-1, i) * Random.Range(sharedEffectData.UpRange.x, sharedEffectData.UpRange.y) *
				           Mathf.Ceil(i / 2f);
				positionRight = Random.Range(-1, 1) *
				                Random.Range(sharedEffectData.RightRange.x, sharedEffectData.RightRange.y) *
				                Mathf.Ceil(i / 2f);
			}

			var bulletTransform = projectileTransform;
			var currentPos = bulletTransform.position;
			var nextPos = raycastHit2D.point
			              + bulletTransform.up.ToVector2() * position
			              + bulletTransform.right.ToVector2() * positionRight;

			var raycast = Physics2D.Raycast(nextPos, nextPos - currentPos.ToVector2(), sharedEffectData.RayWidth);


			if (raycast.collider == null) return null;

			nextPos = raycast.point;

			var spawnedDestroyedBullet = Pool.Current.Get(prefab);
			spawnedDestroyedBullet.gameObject.SetActive(false);

			var spawnedBulletTransform = spawnedDestroyedBullet.transform;
			var rotation = bulletTransform.rotation;

			spawnedBulletTransform.position = nextPos;
			spawnedBulletTransform.rotation = Quaternion.Euler(
				rotation.eulerAngles.x,
				rotation.eulerAngles.y,
				rotation.eulerAngles.z + direction);
			spawnedBulletTransform.localScale = Vector3.one * scale;
			
			spawnedDestroyedBullet.gameObject.SetActive(true);

			return spawnedDestroyedBullet;
		}


		private void CreateDestroyedWallEffect(GameObject prefab, int i, RaycastHit2D raycastHit2D, Transform projectileTransform, bool inverted = false)
		{
			var direction = 0f;
			var position = 0f;
			var positionRight = 0f;
			var scale = Random.Range(0.8f, 1.2f);
			if (i > 0)
			{
				direction = Mathf.Pow(-1, i) * sharedEffectData.RotationStep * Mathf.Ceil(i / 2f);
				position = Mathf.Pow(-1, i) * Random.Range(sharedEffectData.UpRange.x, sharedEffectData.UpRange.y) * Mathf.Ceil(i / 2f);
				positionRight = Random.Range(-1, 1) * Random.Range(sharedEffectData.RightRange.x, sharedEffectData.RightRange.y) * Mathf.Ceil(i / 2f);
			}

			var bulletTransform = projectileTransform;
			var currentPos = bulletTransform.position;
			var nextPos = raycastHit2D.point 
			              + bulletTransform.up.ToVector2() * position
			              + bulletTransform.right.ToVector2() * positionRight;
            
			var spawnedDestroyedWall = Pool.Current.Get(prefab);
			spawnedDestroyedWall.SetActive(false);
            
			var spawnedBulletTransform = spawnedDestroyedWall.transform;
			var rotation = bulletTransform.rotation;

			spawnedBulletTransform.position = nextPos;
			spawnedBulletTransform.rotation = Quaternion.Euler(
				rotation.eulerAngles.x,
				rotation.eulerAngles.y,
				rotation.eulerAngles.z + direction + (inverted ? 180f : 0f));
			spawnedBulletTransform.localScale = Vector3.one * scale;

			//_destroyObjects.Add(spawnedDestroyedWall.gameObject);

			spawnedDestroyedWall.SetActive(true);
		}
	}
}