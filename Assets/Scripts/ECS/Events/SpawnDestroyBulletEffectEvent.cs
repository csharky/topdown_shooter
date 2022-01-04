using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Events
{
	internal struct SpawnDestroyBulletEffectEvent
	{
		public (EcsEntity, RaycastHit2D) Data;
		public Transform                 ProjectileTransform;
	}
}