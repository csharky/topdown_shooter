using ECS.Components;

namespace ECS.Components
{
	public struct PoolData
	{
		public PoolObjectId id;
		public ushort       idx;
		public ushort       capacity;
	}
}