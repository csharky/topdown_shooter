using System;
using UnityEngine;

namespace ECS.Components
{
	[Serializable]
	public struct ColliderComponent
	{
		public Collider2D collider;
	}
}