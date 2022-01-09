using ECS.Components;
using ECS.Events;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Systems
{
	public class DeadAnimatorSystem : IEcsRunSystem
	{
		private const string DeadParameter = "Dead";

		private static readonly int Dead = Animator.StringToHash(DeadParameter);

		private readonly EcsFilter<BodyAnimatorComponent, BecomeDeadEvent> _filter = null;

		public void Run()
		{
			foreach (var i in _filter)
			{
				ref var animator = ref _filter.Get1(i).animator;
				animator.SetBool(Dead, true);
			}
		}
	}
}