
using UnityEngine;
using UnityEngine.EventSystems;

public class MobileCarController : MonoBehaviour,
    IPointerDownHandler, IPointerUpHandler
{
    public ArcadeCarController car;

    public enum CarButton
    {
        Accelerate,
        Reverse,
        Left,
        Right
    }

    public CarButton buttonType;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (car == null) return;

        switch (buttonType)
        {
            case CarButton.Accelerate:
                car.SetMobileInput(1f, 0f);
                break;

            case CarButton.Reverse:
                car.SetMobileInput(-1f, 0f);
                break;

            case CarButton.Left:
                car.SetMobileInput(0f, -1f);
                break;

            case CarButton.Right:
                car.SetMobileInput(0f, 1f);
                break;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (car != null)
            car.SetMobileInput(0f, 0f);
    }

    public void Stop()
    {
        if (car != null)
            car.SetMobileInput(0f, 0f);
    }
}