using System;
using UnityEngine;

namespace ECS.Components
{
	[Serializable]
	public struct DestroyedBulletEffectComponent
	{
		public GameObject prefab;
		public int        count;
	}
}