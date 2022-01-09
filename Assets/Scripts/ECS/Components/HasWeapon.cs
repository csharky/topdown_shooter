using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Components
{
	public struct HasWeapon
	{
		public EcsEntity weapon;
		public Transform weaponTransform;
	}
}