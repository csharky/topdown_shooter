using ECS.Components;
using ECS.Events;
using ECS.Shared;
using Leopotam.Ecs;

namespace ECS.Systems
{
	public class WallBreakBecomeActiveSystem : IEcsRunSystem
	{
		private readonly EcsFilter<MoveSpeedData, SpeedDecreaseMultiplierComponent, WallBreakTag, BecomeActiveEvent>
			_wallBreaks = null;

		private readonly SharedDestroyWallEffectData _sharedData = null;

		public void Run()
		{
			foreach (var wallBreakIdx in _wallBreaks)
			{
				ref var wallBreakEntity = ref _wallBreaks.GetEntity(wallBreakIdx);
				ref var gameObject = ref _wallBreaks.Get3(wallBreakIdx).gameObject;

				ref var speed = ref _wallBreaks.Get1(wallBreakIdx).speed;
				ref var multiplier = ref _wallBreaks.Get2(wallBreakIdx).multiplier;

				speed = _sharedData.StartSpeed;
				multiplier = _sharedData.SpeedDecreaseMultiplier;
				
				gameObject.SetActive(true);

				wallBreakEntity.Del<BecomeActiveEvent>();
			}
		}
	}
}