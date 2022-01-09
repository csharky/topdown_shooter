using ECS.Components;
using Leopotam.Ecs;

namespace ECS.Systems
{
	public class GameMapFinishAllEnemiesDeadRoundSystem : IEcsRunSystem
	{
		private readonly EcsFilter<GameMapData>         _gameMapData = null;
		private readonly EcsFilter<RoundData>           _rounds      = null;
		private readonly EcsFilter<AllEnemiesDeadEvent> _event       = null;

		public void Run()
		{
			if (_gameMapData.IsEmpty() || _event.IsEmpty()) return;
			ref var roundsCount = ref _gameMapData.Get1(0).rounds;
			ref var currentRound = ref _gameMapData.Get1(0).currentRound.Get<RoundData>();
			ref var gameMapData = ref _gameMapData.Get1(0);

			if (roundsCount <= 0) return;
			if (currentRound.roundType != RoundType.EndByKillingAllEnemies) return;
			
			if (currentRound.id >= roundsCount - 1)
			{
				_gameMapData.GetEntity(0).Get<LevelClear>();
			}
			
			foreach (var roundIdx in _rounds)
			{
				ref var id = ref _rounds.Get1(roundIdx).id;
				ref var activateOnBecome = ref _rounds.Get1(roundIdx).activateOnBecomeActive;

				if (id != currentRound.id + 1) continue;
				
				gameMapData.currentRound = _rounds.GetEntity(roundIdx);
				
				for (var i = 0; i < activateOnBecome.Length; i++)
					activateOnBecome[i].gameObject.SetActive(true);

				break;
			}
		}
	}

}