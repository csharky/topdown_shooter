using Leopotam.Ecs;
using UnityEngine;
using Voody.UniLeo;

namespace ECS.Components
{
	public class BloodPartTagProvider : MonoBehaviour, IConvertToEntity
	{
		void IConvertToEntity.Convert(EcsEntity entity)
		{
			entity.Replace(new BloodPartTag(){ gameObject = gameObject });
		}
	}
}