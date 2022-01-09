using System;
using Pathfinding;

namespace ECS.Components
{
	[Serializable]
	public struct PathComponent
	{
		public Path path;
		public int  waypoint;
	}
}