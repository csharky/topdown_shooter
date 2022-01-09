using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Events
{
	internal struct SpawnBloodSplatterEffectEvent
	{
		public (EcsEntity, RaycastHit2D) Data;
		public Transform                 ProjectileTransform;
	}
}