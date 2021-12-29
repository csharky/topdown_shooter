using System;
using ECS.Components;
using ECS.Events;
using ECS.Systems;
using Leopotam.Ecs;
using Leopotam.Ecs.UnityIntegration;
using UnityEngine;
using Voody.UniLeo;

namespace ECS
{
	public class EcsGameInstaller : MonoBehaviour
	{
		private EcsWorld   _world;
		private EcsSystems _systems;
		private EcsSystems _fixedUpdateSystems;

		private void Start()
		{
			_world = new EcsWorld();
			_systems = new EcsSystems(_world);
			_fixedUpdateSystems = new EcsSystems(_world, "FIXED UPDATE");
			
#if UNITY_EDITOR
			EcsWorldObserver.Create(_world);
#endif

			_systems
				.ConvertScene();

			_fixedUpdateSystems
				.ConvertScene();

			AddSystems();
			AddOneFrames();
			
			_systems.Init();
			_fixedUpdateSystems.Init();
			
#if UNITY_EDITOR
			EcsSystemsObserver.Create(_systems);
			EcsSystemsObserver.Create(_fixedUpdateSystems);
#endif
			
			DontDestroyOnLoad(gameObject);
		}

		private void Update()
		{
			_systems.Run();
		}
		
		private void FixedUpdate()
		{
			_fixedUpdateSystems.Run();
		}

		private void AddSystems()
		{
			_systems
				.Add(new WeaponInitSystem());
			
			_systems
				.Add(new WeaponFireBlockSystem())
				.Add(new PlayerMoveDirectionByInputSystem())
				.Add(new FireInputSystem())
				.Add(new PlayerTransformMovementSystem())
				.Add(new CameraFollowSystem())
				.Add(new RotateDirectionToCursorSystem())
				.Add(new WeaponFireSystem());

			_fixedUpdateSystems
				.Add(new ProjectileCollisionDetectSystem())
				.Add(new WallProjectileCollisionInteractSystem())
				.Add(new ProjectileWallCollisionInteractSystem())
				.Add(new ProjectileDestroySystem())
				.Add(new RemoveProjectileCollidedEventSystem())
				.Add(new TransformMovementForwardSystem())
				.Add(new PlayerRigidbodyMovementSystem())
				.Add(new TopdownRotateSystem());
		}

		private void AddOneFrames()
		{
			_systems.OneFrame<ProjectileDestroyEvent>();
		}
		
		private void OnDestroy () {
			if (_systems != null) {
				_systems.Destroy ();
				_systems = null;
				_fixedUpdateSystems.Destroy ();
				_fixedUpdateSystems = null;
				_world.Destroy ();
				_world = null;
			}
		}
	}
}