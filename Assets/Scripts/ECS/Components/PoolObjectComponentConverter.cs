using Leopotam.Ecs;
using UnityEngine;
using Voody.UniLeo;

namespace ECS.Components
{
	public class PoolObjectComponentConverter : MonoBehaviour, IConvertToEntity
	{
		public void Convert(EcsEntity entity)
		{
			var provider = GetComponent<PoolObjectComponentProvider>();
			var id = provider.id;
			var idx = provider.idx;
			entity.Replace(new PoolObjectComponent()
			{
				id = id,
				idx = idx,
			});
		}
	}
}