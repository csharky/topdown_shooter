using ECS.Components;
using Leopotam.Ecs;

namespace ECS.Systems
{
	public class WeaponInitSystem : IEcsInitSystem
	{
		private readonly EcsFilter<WeaponComponent> _filter = null;


		public void Init()
		{
			foreach (var i in _filter)
			{
				ref var bulletPrefab= ref _filter.Get1(i).bulletPrefab;

				if (Pool.Current != null)
					Pool.Current.AddToPool(bulletPrefab);
			}
		}
	}
}