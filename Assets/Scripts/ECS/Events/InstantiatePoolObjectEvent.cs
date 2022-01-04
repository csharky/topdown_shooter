using ECS.Components;
using UnityEngine;

namespace ECS.Events
{
	public struct InstantiatePoolObjectEvent
	{
		public PoolObjectId id;
		public Vector2      position;
		public Quaternion   rotation;
		public Vector3      scale;
	}
}