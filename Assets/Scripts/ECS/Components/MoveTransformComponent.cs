using System;
using UnityEngine;

namespace ECS.Components
{
	[Serializable]
	public struct MoveTransformComponent
	{
		public Transform transform;
	}
}