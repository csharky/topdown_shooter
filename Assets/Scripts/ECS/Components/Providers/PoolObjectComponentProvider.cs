using UnityEngine;

namespace ECS.Components
{
	public class PoolObjectComponentProvider : MonoBehaviour
	{
		public                   PoolObjectId id;
		[HideInInspector] public int          idx;
	}
}