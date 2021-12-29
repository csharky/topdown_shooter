using System;
using UnityEngine;

namespace ECS.Components
{
	[Serializable]
	public struct ProjectileDetectionComponent
	{
		public Transform detectionRoot;
		public LayerMask layerMask;
	}
}