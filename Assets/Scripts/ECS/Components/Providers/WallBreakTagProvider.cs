using Leopotam.Ecs;
using UnityEngine;
using Voody.UniLeo;

namespace ECS.Components
{
	public class WallBreakTagProvider : MonoBehaviour, IConvertToEntity
	{
		void IConvertToEntity.Convert(EcsEntity entity)
		{
			entity.Replace(new WallBreakTag(){ gameObject = gameObject });
		}
	}
}