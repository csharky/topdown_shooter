using ECS.Components;
using ECS.Events;
using ECS.Shared;
using Leopotam.Ecs;

namespace ECS.Systems
{
	public class BloodPartBecomeActiveSystem : IEcsRunSystem
	{
		private readonly EcsFilter<MoveSpeedData, SpeedDecreaseMultiplierComponent, BloodPartTag, BecomeActiveEvent>
			_bloodParts = null;

		private readonly SharedBloodSplatterSettings _sharedSettings = null;

		public void Run()
		{
			foreach (var idx in _bloodParts)
			{
				ref var bloodPartEntity = ref _bloodParts.GetEntity(idx);
				ref var gameObject = ref _bloodParts.Get3(idx).gameObject;

				ref var speed = ref _bloodParts.Get1(idx).speed;
				ref var multiplier = ref _bloodParts.Get2(idx).multiplier;

				speed = _sharedSettings.StartSpeed;
				multiplier = _sharedSettings.SpeedDecreaseMultiplier;
				
				gameObject.SetActive(true);

				bloodPartEntity.Del<BecomeActiveEvent>();
			}
		}
	}
}