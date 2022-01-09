using System;
using UnityEngine;

namespace ECS.Components
{
	[Serializable]
	public struct RoundData
	{
		public int         id;
		public Transform[] activateOnBecomeActive;
		public RoundType   roundType;
	}
}