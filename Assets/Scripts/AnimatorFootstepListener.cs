using System;
using UnityEngine;

public class AnimatorFootstepListener : MonoBehaviour
{
    public event Action<FootstepSide> OnFootstep;
    public void Footstep(FootstepSide side)
    {
        OnFootstep?.Invoke(side);
    }
    
    public enum FootstepSide { Left, Right }
}