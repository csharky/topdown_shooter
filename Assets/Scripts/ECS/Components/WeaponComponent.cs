using System;
using UnityEngine;

namespace ECS.Components
{
	[Serializable]
	public struct WeaponComponent
	{
		public Transform  firingPositionTransform;
		public GameObject bulletPrefab;
		public int        shootsPerFire;
		public float      shootDelay;
		public int        ammo;
		public int        ammoCapacity;
		public float      reloadDelay;
	}
}