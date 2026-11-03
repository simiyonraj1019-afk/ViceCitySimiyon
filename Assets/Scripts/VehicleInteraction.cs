
using UnityEngine;

public class VehicleInteraction : MonoBehaviour
{
    public PlayerController player;
    public ThirdPersonCamera gameCamera;

    public float enterDistance = 3f;

    private ArcadeCarController currentCar;

    void Update()
    {
        if (player == null || gameCamera == null)
            return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentCar == null)
                TryEnterCar();
            else
                ExitCar();
        }
    }

    void TryEnterCar()
    {
        ArcadeCarController[] cars =
            FindObjectsOfType<ArcadeCarController>();

        ArcadeCarController nearest = null;
        float best = enterDistance;

        foreach (ArcadeCarController car in cars)
        {
            float distance = Vector3.Distance(
                player.transform.position,
                car.transform.position
            );

            if (distance < best)
            {
                best = distance;
                nearest = car;
            }
        }

        if (nearest == null) return;

        currentCar = nearest;

        player.SetDriving(true);

        player.transform.SetParent(
            currentCar.transform
        );

        player.transform.position =
            currentCar.seatPoint != null
            ? currentCar.seatPoint.position
            : currentCar.transform.position;

        currentCar.SetPlayerDriving(true);

        gameCamera.SetTarget(
            currentCar.transform
        );
    }

    void ExitCar()
    {
        Vector3 exitPosition =
            currentCar.exitPoint != null
            ? currentCar.exitPoint.position
            : currentCar.transform.position
              + currentCar.transform.right * 2f;

        player.transform.SetParent(null);
        player.transform.position = exitPosition;

        player.SetDriving(false);
        currentCar.SetPlayerDriving(false);

        gameCamera.SetTarget(player.transform);

        currentCar = null;
    }
}
