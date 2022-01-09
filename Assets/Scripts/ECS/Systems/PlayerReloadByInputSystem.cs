using ECS.Components;
using Leopotam.Ecs;

namespace ECS.Systems
{
	public class PlayerReloadByInputSystem : IEcsRunSystem
	{
		private readonly EcsFilter<ReloadInputComponent> _filter = null;
		
		public void Run()
		{
			var isReloading = InputHelper.IsReloading();

			foreach (var i in _filter)
			{
				ref var moveDirectionData = ref _filter.Get1(i);
				ref var active = ref moveDirectionData.isActive;

				active = isReloading > 0.5f;
			}
		}
	}
}