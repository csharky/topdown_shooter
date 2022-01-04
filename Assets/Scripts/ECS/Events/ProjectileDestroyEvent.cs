using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Events
{
	internal struct ProjectileDestroyEvent
	{
		public (EcsEntity, RaycastHit2D) Data;
	}
}