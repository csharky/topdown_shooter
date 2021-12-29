using System;
using UnityEngine;

namespace ECS.Components
{
	[Serializable]
	public struct RigidbodyComponent
	{
		public Rigidbody2D rigidbody;
	}
}