using ECS.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Systems
{
	public class MovementAnimatorSystem : IEcsRunSystem
	{
		private const string MoveXParameter = "SpeedX";
		private const string MoveYParameter = "SpeedY";

		private static readonly int MoveY = Animator.StringToHash(MoveYParameter);
		private static readonly int MoveX = Animator.StringToHash(MoveXParameter);

		private readonly EcsFilter<LegsAnimatorComponent, MoveDirectionData> _filter = null;

		public void Run()
		{
			foreach (var i in _filter)
			{
				ref var animator = ref _filter.Get1(i).animator;
				ref var direction = ref _filter.Get2(i).direction;
				
				animator.SetFloat(MoveX, direction.x);
				animator.SetFloat(MoveY, direction.y);
			}
		}
	}
}