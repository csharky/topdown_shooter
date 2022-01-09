using ECS.Components;
using EventSystem;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Systems
{
	public class CameraFollowSystem : IEcsRunSystem
	{
		private readonly EcsFilter<CameraTag, TransformComponent, MoveSpeedData> _cameraFilter = null;
		private readonly EcsFilter<FollowByCameraComponent, TransformComponent> _followFilter = null;

		public void Run()
		{
			if (_cameraFilter.IsEmpty()) return;
			if (_followFilter.IsEmpty()) return;

			ref var cameraTransform = ref _cameraFilter.Get2(0).transform;
			ref var cameraMaxSpeed = ref _cameraFilter.Get3(0).speed;
			ref var followTargetTransform = ref _followFilter.Get2(0).transform;

			var camPosition = cameraTransform.position;
			var followTargetPosition = followTargetTransform.position;
			
			var camVelocity = new Vector3(1f, 1f, 0f);
			var targetPosition = new Vector3(followTargetPosition.x, followTargetPosition.y, camPosition.z);

			cameraTransform.position = Vector3.SmoothDamp(camPosition, targetPosition, ref camVelocity, Time.deltaTime,
				cameraMaxSpeed);
		}
	}
	public class AudioSoundSystem : IEcsRunSystem
	{
		private readonly EcsFilter<PlaySoundEvent> _events = null;

		public void Run()
		{
			foreach (var eventIdx in _events)
			{
				ref var type = ref _events.Get1(eventIdx).type;
				GameEventSystem.current.Fire(new PlayAudioEvent()
				{
					Type = type
				});
			}
		}
	}
}