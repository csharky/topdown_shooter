using ECS.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Systems
{
	public class CameraFollowSystem : IEcsRunSystem
	{
		private readonly EcsFilter<CameraTag, TransformComponent> _cameraFilter = null;
		private readonly EcsFilter<FollowByCameraComponent, TransformComponent> _followFilter = null;

		public void Run()
		{

			if (_cameraFilter.IsEmpty()) return;
			if (_followFilter.IsEmpty()) return;

			ref var cameraTransform = ref _cameraFilter.GetEntity(0).Get<TransformComponent>().transform;
			ref var followTargetTransform = ref _followFilter.GetEntity(0).Get<TransformComponent>().transform;

			var followTargetPosition = followTargetTransform.position;
			cameraTransform.position = new Vector3(
				followTargetPosition.x,
				followTargetPosition.y,
				cameraTransform.position.z);
		}
	}
}