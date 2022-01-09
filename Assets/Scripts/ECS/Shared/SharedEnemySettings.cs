using UnityEngine;

namespace ECS.Shared
{
	[CreateAssetMenu(menuName = "Shared Enemy Settings")]
	public class SharedEnemySettings : ScriptableObject
	{
		[SerializeField] private LayerMask playerLayerMask;
		[SerializeField] private LayerMask searchCollideLayerMask;

		public LayerMask PlayerLayerMask => playerLayerMask;
		public LayerMask SearchCollideLayerMask => searchCollideLayerMask;
	}
}