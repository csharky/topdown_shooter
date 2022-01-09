using Pathfinding;

namespace ECS.Components
{
	internal struct PathCalculatedEvent
	{
		public Seeker seeker;
		public Path   path;
		public int    waypoint;
	}
}