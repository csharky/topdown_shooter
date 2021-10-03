using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace PostProcessing
{
    [Serializable]
    [PostProcess(typeof(VHSCameraEffectRenderer), PostProcessEvent.AfterStack, "Custom/VHS Camera Effect")]
    public sealed class VHSCameraEffect : PostProcessEffectSettings
    {
        [Range(0f, 1f), Tooltip("VHS effect intensity.")]
        public FloatParameter power = new FloatParameter {value = 0.5f};
    }
}
