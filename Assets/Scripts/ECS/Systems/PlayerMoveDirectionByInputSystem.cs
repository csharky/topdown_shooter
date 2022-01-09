using ECS.Components;
using Leopotam.Ecs;

namespace ECS.Systems
{
	public class PlayerMoveDirectionByInputSystem : IEcsRunSystem
	{
		private readonly EcsFilter<PlayerTag, MoveDirectionData> _filter = null;
		
		public void Run()
		{
			var moveVector = InputHelper.MoveVector();

			foreach (var i in _filter)
			{
				ref var moveDirectionData = ref _filter.Get2(i);
				ref var direction = ref moveDirectionData.direction;
				
				direction.x = moveVector.x;
				direction.y = moveVector.y;
			}
		}
	}
	
}