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
		
		BloodPart1, BloodPart2, BloodPart3, BloodPart4, BloodPart5, BloodPart6, BloodPart7, BloodPart8, BloodPart9, BloodPart10,
		BloodPart11, BloodPart12, BloodPart13, BloodPart14, BloodPart15, BloodPart16, BloodPart17, BloodPart18, BloodPart19,
		BloodPart20, BloodPart21, BloodPart22, BloodPart23, BloodPart24, BloodPart25, BloodPart26, BloodPart27, BloodPart28, 
		BloodPart29
	}
}