using System;
using UnityEngine;

namespace ECS.Components
{
	[Serializable]
	public struct DestroyedWallEffectComponent
	{
		public PoolObjectComponentProvider[] prefabs;
		public int                           count;
	}
}