using Leopotam.Ecs;
using UnityEngine;
using Voody.UniLeo;

namespace ECS.Components
{
	public abstract class MonoProviderExt <T> : BaseMonoProviderExt, IConvertToEntity where T : struct
	{
		[SerializeField]
		protected T value;

		void IConvertToEntity.Convert(EcsEntity entity)
		{
			entity.Replace(value);
		}
	}
}