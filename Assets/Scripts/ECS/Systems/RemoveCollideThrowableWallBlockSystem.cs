using ECS.Components;
using Leopotam.Ecs;

namespace ECS.Systems
{
	internal class RemoveCollideThrowableWallBlockSystem : IEcsRunSystem
	{
		private readonly EcsFilter<CollideThrowableWallBlock> _events = null;

		public void Run()
		{
			foreach (var i in _events)
			{
				ref var framesCount = ref _events.Get1(i).framesCount;
				if (framesCount == 0)
				{
					_events.GetEntity(i).Del<CollideThrowableWallBlock>();
				}
				framesCount--;
			}
		}
	}
}