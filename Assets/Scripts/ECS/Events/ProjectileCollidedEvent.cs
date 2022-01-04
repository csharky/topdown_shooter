using System.Collections.Generic;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Events
{
	public struct ProjectileCollidedEvent
	{
		public List<(EcsEntity, RaycastHit2D)> Entities;
		public Transform                       ProjectileTransform;
	}
}