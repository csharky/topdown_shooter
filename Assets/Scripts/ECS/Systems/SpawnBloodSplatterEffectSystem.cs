using ECS.Components;
using ECS.Events;
using ECS.Shared;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Systems
{
	public class SpawnBloodSplatterEffectSystem : IEcsRunSystem
	{
		private readonly EcsWorld                                 _world            = null;
		private readonly SharedBloodSplatterSettings              _sharedEffectData = null;
		private readonly EcsFilter<SpawnBloodSplatterEffectEvent> _filter           = null;
		
		public void Run()
		{
			foreach (var i in _filter)
			{
				ref var data = ref _filter.Get1(i).Data;
				ref var projectileTransform = ref _filter.Get1(i).ProjectileTransform;

				ref var prefabs = ref _sharedEffectData.prefabs;
				var count = Random.Range(_sharedEffectData.CountRange.x, _sharedEffectData.CountRange.y);

				for (int j = 0; j < count; j++)
				{
					var prefab = prefabs[Random.Range(0, prefabs.Length)];
					CreateBloodSplatterPart(prefab, j, data.Item2, projectileTransform);
				}
			}
		}

		private void CreateBloodSplatterPart(PoolObjectComponentProvider prefab, int i, RaycastHit2D raycastHit2D, Transform projectileTransform, bool inverted = false)
		{
			var direction = 0f;
			var position = 0f;
			var positionRight = 0f;
			var scale = Random.Range(1, 1.5f);
			if (i > 0)
			{
				direction = Mathf.Pow(-1, i % 5) * _sharedEffectData.RotationStep * Mathf.Ceil(i % 5 / 2f);
				position = Mathf.Pow(-1, i % 5) * Random.Range(_sharedEffectData.UpRange.x, _sharedEffectData.UpRange.y) * Mathf.Ceil(i % 5 / 2f);
				positionRight = Random.Range(-1, 1) * Random.Range(_sharedEffectData.RightRange.x, _sharedEffectData.RightRange.y) * Mathf.Ceil(i % 5 / 2f);
			}

			var bulletTransform = projectileTransform;
			var currentPos = bulletTransform.position;
			var nextPos = raycastHit2D.point 
			              + bulletTransform.up.ToVector2() * position
			              + bulletTransform.right.ToVector2() * positionRight;
			
			//spawnedDestroyedWall.SetActive(false);

			var rotation = Quaternion.Euler(bulletTransform.rotation.eulerAngles + new Vector3(0, 0, 180));
			
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