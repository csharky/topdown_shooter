using System.Collections.Generic;
using System.Linq;
using ECS.Components;
using ECS.Events;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Systems
{
	public class ProjectileDestroySystem : IEcsRunSystem
	{
		private readonly EcsFilter<TransformComponent, ProjectileDestroyEvent> _filter = null;

		public void Run()
		{
			foreach (var i in _filter)
			{
				var gameObject = _filter.Get1(i).transform.gameObject;
				gameObject.SetActive(false);
			}
		}
	}
	
}