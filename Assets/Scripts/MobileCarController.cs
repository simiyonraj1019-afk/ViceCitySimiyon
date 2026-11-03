
using UnityEngine;

public class MobileCarController : MonoBehaviour
{
    public ArcadeCarController car;

    public void Accelerate()
    {
        if (car != null)
            car.SetMobileInput(1f, 0f);
    }

    public void Reverse()
    {
        if (car != null)
            car.SetMobileInput(-1f, 0f);
    }

    public void TurnLeft()
    {
        if (car != null)
            car.SetMobileInput(0f, -1f);
    }

    public void TurnRight()
    {
        if (car != null)
            car.SetMobileInput(0f, 1f);
    }

    public void Stop()
    {
        if (car != null)
            car.SetMobileInput(0f, 0f);
    }
}
