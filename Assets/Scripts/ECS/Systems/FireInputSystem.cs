using ECS.Components;
using Leopotam.Ecs;

namespace ECS.Systems
{
	public class FireInputSystem : IEcsRunSystem
	{
		private readonly EcsFilter<FireInputComponent> _filter = null;
		
		public void Run()
		{
			var isFiring = InputHelper.IsFire() > 0.6f;

			foreach (var i in _filter)
			{
				ref var fireInput = ref _filter.Get1(i).isActive;
				
				fireInput = isFiring;
			}
		}
	}
	
}