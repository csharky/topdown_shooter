using ECS.Components;
using ECS.Components.Tags;
using Leopotam.Ecs;

namespace ECS.Systems
{
	public class EnemyInitSystem : IEcsInitSystem
	{
		private readonly EcsFilter<EnemyTag>  _enemies;
		private readonly EcsFilter<PlayerTag> _players;

		public void Init()
		{
			foreach (var enemyIdx in _enemies)
			{
				ref var entity = ref _enemies.GetEntity(enemyIdx);
				foreach (var playerIdx in _players)
				{
					ref var player = ref _players.GetEntity(playerIdx);
					entity.Get<HasTarget>() = new HasTarget()
					{
						target = player
					};
					entity.Get<SearchTargetComponent>();
					break;
				}
			}
		}
	}
}