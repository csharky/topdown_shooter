using ECS.Components;
using UnityEngine;

namespace ECS.Shared
{
	[CreateAssetMenu]
	public class SharedBloodSplatterSettings : ScriptableObject
	{
		public Vector2 UpRange      => new Vector2(0.05f, 0.01f);
		public Vector2 RightRange   => new Vector2(0.001f, 0.005f);
		public float   RotationStep => 17;

		public float StartSpeed              => Random.Range(0f, 12f);
		public float SpeedDecreaseMultiplier => Random.Range(1.3f, 2f);

		public ushort     PooledAmountPerPrefab => 40;
		public Vector2Int CountRange            => new Vector2Int(10, 20);

		public PoolObjectComponentProvider[] prefabs;
	}
}