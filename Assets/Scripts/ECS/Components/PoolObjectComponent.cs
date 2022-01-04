using System;

namespace ECS.Components
{
	[Serializable]
	public struct PoolObjectComponent
	{
		public PoolObjectId id;
		public int          idx;
	}

	public enum PoolObjectId
	{
		WallBreak1, WallBreak2, WallBreak3, WallBreak4, WallBreak5, WallBreak6, WallBreak7, WallBreak8, WallBreak9, WallBreak10,
		
	}
}