using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class YourButton : MonoBehaviour, IPointerDownHandler,
IPointerUpHandler
{
    private bool _isPressed = false;
    [System.Serializable]
    public class ButtonReleasedEvent : UnityEvent { }

    // Event delegates triggered on mouse up.
    [FormerlySerializedAs("onMouseUp")]
    [SerializeField]
    private ButtonReleasedEvent _onReleased = new ButtonReleasedEvent();

    public void OnPointerDown(PointerEventData eventData)
    {
        _isPressed = true;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (_isPressed)
        {
            _isPressed = false;
            _onReleased.Invoke();
        }
    }
}
