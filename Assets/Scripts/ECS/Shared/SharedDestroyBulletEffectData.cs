using UnityEngine;

namespace ECS.Shared
{
	public class SharedDestroyBulletEffectData
	{
		public Vector2 UpRange      => new Vector2(0.05f, 0.01f);
		public Vector2 RightRange   => new Vector2(0.001f, 0.005f);
		public float   RotationStep => 17;
		public float   RayWidth     => 0.1f;
	}
}