using System.Linq;
using UnityEngine;

namespace FootstepControllers
{
    public class MainFootstepController : BaseFootstepController
    {
        [SerializeField] private AnimatorFootstepListener _footstepListener;
        private BaseFootstepController[] _childControllers;

        private void Awake()
        {
            if (_footstepListener != null)
                _footstepListener.OnFootstep += OnFootstep;

            _childControllers = GetComponentsInChildren<BaseFootstepController>().Where(_ => _ != this).ToArray();
        }

        public override void OnFootstep(AnimatorFootstepListener.FootstepSide side)
        {
            if (_childControllers == null) return;
        
            foreach (var footstepController in _childControllers)
            {
                footstepController.OnFootstep(side);
            }
        }
    }
}