using ECS.Components;
using Leopotam.Ecs;

namespace ECS.Systems
{
	public class GameMapInitSystem : IEcsInitSystem
	{
		private readonly EcsFilter<GameMapData> _gameMapData = null;
		private readonly EcsFilter<RoundData>   _rounds      = null;

		public void Init()
		{
			if (_gameMapData.IsEmpty()) return;

			ref var gameMapData = ref _gameMapData.Get1(0);
			if (gameMapData.rounds <= 0) return;

			foreach (var roundIdx in _rounds)
			{
				ref var id = ref _rounds.Get1(roundIdx).id;
				ref var activateOnBecome = ref _rounds.Get1(roundIdx).activateOnBecomeActive;

				if (id == 0)
					gameMapData.currentRound = _rounds.GetEntity(roundIdx);
				
				for (var i = 0; i < activateOnBecome.Length; i++)
					activateOnBecome[i].gameObject.SetActive(id == 0);
			}
		}
	}
}