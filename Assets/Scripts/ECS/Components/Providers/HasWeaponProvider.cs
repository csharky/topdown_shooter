using Leopotam.Ecs;
using UnityEngine;
using Voody.UniLeo;

namespace ECS.Components
{
	public class HasWeaponProvider : MonoBehaviour, IConvertToEntity
	{
		public WeaponComponentProvider weaponProvider;
		public void Convert(EcsEntity entity)
		{
			var weapon = entity.GetInternalWorld().NewEntity();
			var weaponTransform = weaponProvider.transform;
			foreach (var component in weaponProvider.GetComponents<Component>())
			{
				if (!(component is IConvertToEntity entityComponent)) continue;
				
				entityComponent.Convert(weapon);
				GameObject.Destroy(component);
			}
			
			entity.Replace(new HasWeapon()
			{
				weapon = weapon,
				weaponTransform = weaponTransform
			});
		}
	}
}