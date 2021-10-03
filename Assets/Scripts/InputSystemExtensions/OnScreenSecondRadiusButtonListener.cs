using UnityEngine;
using UnityEngine.InputSystem.OnScreen;

namespace InputSystemExtensions
{
    public class OnScreenSecondRadiusButtonListener : OnScreenControl
    {
        protected override string controlPathInternal { get; set; }

        protected override void OnEnable()
        {
            controlPathInternal = GetComponent<OnScreenStickWithSecondRadiusAsButton>()
                .ButtonControlPath;
            
            base.OnEnable();
        }

        public void CallControl(float value)
        {
            SendValueToControl(value);
        }
    }
}