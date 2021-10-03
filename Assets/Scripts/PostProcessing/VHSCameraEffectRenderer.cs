using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace PostProcessing
{
    public sealed class VHSCameraEffectRenderer : PostProcessEffectRenderer<VHSCameraEffect>
    {
        public override void Render(PostProcessRenderContext context)
        {
            var sheet = context.propertySheets.Get(Shader.Find("Hidden/Custom/Grayscale"));
            sheet.properties.SetFloat("_VHSEffect", settings.power);
            context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
        }
    }
}