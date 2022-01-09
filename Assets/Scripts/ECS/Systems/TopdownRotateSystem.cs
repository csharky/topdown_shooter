using ECS.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Systems
{
	public class TopdownRotateSystem : IEcsInitSystem, IEcsRunSystem
	{
		private readonly EcsFilter<RotateDirectionData, TransformComponent> _rotateFilter = null;
		
		public void Init()
		{
			foreach (var i in _rotateFilter)
			{
				ref var direction = ref _rotateFilter.Get1(i).direction;
				ref var rotateTransform = ref _rotateFilter.Get2(i).transform;

				direction = rotateTransform.rotation * Vector3.forward;
			}
		}

		public void Run()
		{
			foreach (var i in _rotateFilter)
			{
				ref var direction = ref _rotateFilter.Get1(i).direction;
				ref var rotateTransform = ref _rotateFilter.Get2(i).transform;

				var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
				rotateTransform.rotation = Quaternion.Euler(0, 0, angle);
			}
		}
	}
	
}