using System;
using Leopotam.Ecs;

namespace ECS.Components
{
	[Serializable]
	public struct GameMapData
	{
		public int       rounds;
		public EcsEntity currentRound;
	}
}