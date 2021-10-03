using UnityEngine;

namespace FootstepControllers
{
    public abstract class BaseFootstepController : MonoBehaviour
    {
        public virtual void OnFootstep(AnimatorFootstepListener.FootstepSide side)
        {
        }
    }
}