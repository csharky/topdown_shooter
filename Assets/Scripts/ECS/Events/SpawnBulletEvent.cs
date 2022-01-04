using UnityEngine;

namespace ECS.Events
{
	internal struct SpawnBulletEvent
	{
		public Transform  RootTransform;
		public GameObject Prefab;
	}
}