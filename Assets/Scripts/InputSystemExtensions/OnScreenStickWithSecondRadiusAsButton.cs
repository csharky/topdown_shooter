using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.Serialization;

namespace InputSystemExtensions
{
    [RequireComponent(typeof(OnScreenSecondRadiusButtonListener))]
    public class OnScreenStickWithSecondRadiusAsButton : OnScreenControl, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData == null)
                throw new System.ArgumentNullException(nameof(eventData));

            RectTransformUtility.ScreenPointToLocalPointInRectangle(m_transform.parent.GetComponentInParent<RectTransform>(), eventData.position, eventData.pressEventCamera, out m_PointerDownPos);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (eventData == null)
                throw new System.ArgumentNullException(nameof(eventData));

            RectTransformUtility.ScreenPointToLocalPointInRectangle(m_transform.parent.GetComponentInParent<RectTransform>(), eventData.position, eventData.pressEventCamera, out var position);
            var delta = position - m_PointerDownPos;

            delta = Vector2.ClampMagnitude(delta, movementRange);
            var newPos = new Vector2(delta.x / movementRange, delta.y / movementRange) * 2f;
            SendValueToControl(newPos);

            var magnitude = newPos.magnitude;
            if (magnitude > 1f + m_SecondMovementRange)
            {
                if (m_buttonInputControl == null)
                {
                    Debug.LogError("Button input hasn't been found!");
                    return;
                }
                
                m_buttonListener.CallControl(1f);
                m_buttonTriggered = true;
            } else if (m_buttonTriggered)
            {
                m_buttonListener.CallControl(0f);
                m_buttonTriggered = false;
            }

            if (magnitude > 1f + m_SecondMovementRange || magnitude <= 1f) 
            {
                ((RectTransform)m_transform).anchoredPosition = m_StartPos + (Vector3)delta;
            } 
            else
            {
                delta = delta.Clamp(movementRange * 0.5f);
                ((RectTransform)m_transform).anchoredPosition = m_StartPos + (Vector3)delta;
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            ((RectTransform)m_transform).anchoredPosition = m_StartPos;
            SendValueToControl(Vector2.zero);
            m_buttonListener.CallControl(0f);
        }

        private void Start()
        {
            m_transform = transform;
            m_StartPos = ((RectTransform)m_transform).anchoredPosition;
            m_buttonInputControl = InputSystem.FindControl(m_ButtonControlPath);
            m_buttonListener = GetComponent<OnScreenSecondRadiusButtonListener>();
        }

        public float movementRange
        {
            get => m_MovementRange;
            set => m_MovementRange = value;
        }
        
        public float secondMovementRange
        {
            get => m_SecondMovementRange;
            set => m_SecondMovementRange = value;
        }

        [FormerlySerializedAs("movementRange")]
        [SerializeField]
        private float m_MovementRange = 50;
        
        [FormerlySerializedAs("movementRange")]
        [SerializeField]
        private float m_SecondMovementRange = 50;

        [InputControl(layout = "Vector2")]
        [SerializeField]
        private string m_ControlPath;
        
        [InputControl(layout = "Button")]
        [SerializeField]
        private string m_ButtonControlPath;
        
        private Vector3 m_StartPos;
        private Vector2 m_PointerDownPos;
        private InputControl m_buttonInputControl;
        private OnScreenSecondRadiusButtonListener m_buttonListener;
        private bool m_buttonTriggered;
        private Transform m_transform;

        protected override string controlPathInternal
        {
            get => m_ControlPath;
            set => m_ControlPath = value;
        }

        public string ButtonControlPath => m_ButtonControlPath;
    }
}