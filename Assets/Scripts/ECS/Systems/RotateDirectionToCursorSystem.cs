using ECS.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Systems
{
	public class RotateDirectionToCursorSystem : IEcsRunSystem
	{
		private readonly EcsFilter<RotateDirectionData, TransformComponent, PlayerTag> _rotateFilter = null;
		private readonly EcsFilter<CursorTag, TransformComponent>                      _cursor       = null;

		public void Run()
		{
			if (_cursor.IsEmpty()) return;

			foreach (var i in _rotateFilter)
			{
				ref var direction = ref _rotateFilter.Get1(i).direction;
				ref var rotateTransform = ref _rotateFilter.Get2(i).transform;
				ref var cursorTransform = ref _cursor.Get2(0).transform;

				var dir = rotateTransform.position.ToVector2() - cursorTransform.position.ToVector2();
				
				direction.x = dir.x;
				direction.y = dir.y;
			}
		}
	}
	
}