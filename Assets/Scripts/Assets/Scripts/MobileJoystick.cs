
using UnityEngine;
using UnityEngine.EventSystems;

public class MobileJoystick : MonoBehaviour,
    IPointerDownHandler,
    IPointerUpHandler,
    IDragHandler
{
    public RectTransform background;
    public RectTransform handle;

    public Vector2 InputValue { get; private set; }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 position;

        if (RectTransformUtility
            .ScreenPointToLocalPointInRectangle(
                background,
                eventData.position,
                eventData.pressEventCamera,
                out position))
        {
            Vector2 size = background.sizeDelta;

            position.x /= size.x;
            position.y /= size.y;

            InputValue = Vector2.ClampMagnitude(
                position * 2f, 1f
            );

            if (handle != null)
            {
                handle.anchoredPosition =
                    new Vector2(
                        InputValue.x * size.x / 2f,
                        InputValue.y * size.y / 2f
                    );
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        InputValue = Vector2.zero;

        if (handle != null)
            handle.anchoredPosition = Vector2.zero;
    }
}
