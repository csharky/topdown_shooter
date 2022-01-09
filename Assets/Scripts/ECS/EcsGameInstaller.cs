using System;
using System.Collections.Generic;
using ECS.Components;
using ECS.Events;
using ECS.Shared;
using ECS.Systems;
using Leopotam.Ecs;
using Leopotam.Ecs.UnityIntegration;
using UnityEngine;
using Voody.UniLeo;

namespace ECS
{
	public class EcsGameInstaller : MonoBehaviour
	{
		[SerializeField] private SharedEnemySettings         enemySettings;
		[SerializeField] private SharedBloodSplatterSettings bloodSplatterSettings;

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

			AddSystems();
			AddOneFrames();
			AddSharedData();
			
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
				.Add(new WeaponInitSystem())
				.Add(new EnemyInitSystem())
				;
			
			_systems
				.Add(new GameMapInitSystem())
				.Add(new WallBreakCreatePoolSystem())
				.Add(new BloodPartCreatePoolSystem())
				.Add(new PoolSystem())
				.Add(new EnemySearchSystem())
				.Add(new MeleeEnemyRotateOnTargetDetectedSystem())
				.Add(new EnemySeekerStartPathSystem())
				.Add(new EnemySeekerAssignPathSystem())
				.Add(new MeleeEnemyMoveByPathSystem())
				.Add(new TopdownRotateSystem())
				.Add(new WallBreakBecomeActiveSystem())
				.Add(new BloodPartBecomeActiveSystem())
				.Add(new WeaponFireBlockSystem())
				.Add(new TargetDetectedTimerSystem())
				.Add(new PlayerReloadByInputSystem())
				.Add(new TransformMovementSystem())
				.Add(new MovementAnimatorSystem())
				.Add(new DeadAnimatorSystem())
				.Add(new DisableComponentsOnBecomeDeadSystem())
				.Add(new AllEnemiesDeadDetectionSystem())
				.Add(new GameMapFinishAllEnemiesDeadRoundSystem())
				.Add(new CameraFollowSystem())
				.Add(new ProjectileTurnOffSystem())
				.Add(new RotateDirectionToCursorSystem())
				;

			_fixedUpdateSystems
				.Add(new PlayerMoveDirectionByInputSystem())
				.Add(new FireInputSystem())
				.Add(new WeaponFireSystem())
				.Add(new WeaponStartReloadSystem())
				.Add(new WeaponFinishReloadSystem())
				.Add(new SpawnBulletSystem())
				.Add(new ProjectileCollisionDetectSystem())
				.Add(new ProjectileEnemyCollisionInteractSystem())
				.Add(new ProjectileWallCollisionInteractSystem())
				.Add(new ProjectileThrowableWallCollisionInteractSystem())
				.Add(new ProjectileDamageSystem())
				.Add(new ProjectileDestroySystem())
				.Add(new TransformMovementForwardSystem())
				.Add(new TransformMoveRightSystem())
				.Add(new TransformMoveMinusRightSystem())
				.Add(new SpeedDecreaseByMultiplierSystem())
				.Add(new PlayerRigidbodyMovementSystem())
				.Add(new RemoveProjectileCollidedEventSystem())
				.Add(new RemoveCollideThrowableWallBlockSystem())
				.Add(new SpawnDestroyBulletEffectSystem())
				.Add(new SpawnDestroyWallEffectSystem())
				.Add(new SpawnBloodSplatterEffectSystem())
				;
		}

		private void AddOneFrames()
		{
			_systems.OneFrame<BecomeActiveEvent>();
			_systems.OneFrame<BecomeInactiveEvent>();
			_systems.OneFrame<PlaySoundEvent>();
			_systems.OneFrame<PathCalculatedEvent>();
			_systems.OneFrame<TargetOnViewSightEvent>();
			_systems.OneFrame<BecomeDeadEvent>();
			_systems.OneFrame<AllEnemiesDeadEvent>();
			
			_fixedUpdateSystems.OneFrame<ProjectileDestroyEvent>();
			_fixedUpdateSystems.OneFrame<DamageEvent>();
			_fixedUpdateSystems.OneFrame<SpawnBulletEvent>();
			_fixedUpdateSystems.OneFrame<SpawnDestroyBulletEffectEvent>();
			_fixedUpdateSystems.OneFrame<SpawnDestroyWallEffectEvent>();
			_fixedUpdateSystems.OneFrame<SpawnBloodSplatterEffectEvent>();
		}
		
		private void AddSharedData()
		{
			var list = new List<object>()
			{
				new SharedBulletData(),
				new SharedDestroyWallEffectData(),
				new SharedDestroyBulletEffectData(),
				bloodSplatterSettings,
				enemySettings
			};

			foreach (var obj in list)
			{
				_systems.Inject(obj, obj.GetType());
				_fixedUpdateSystems.Inject(obj, obj.GetType());
			}
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