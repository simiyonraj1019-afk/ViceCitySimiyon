
using UnityEngine;

public class SimpleTrafficCar : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 7f;

    private int currentWaypoint = 0;

    void Update()
    {
        if (waypoints == null ||
            waypoints.Length == 0)
            return;

        Transform target =
            waypoints[currentWaypoint];

        Vector3 direction =
            target.position - transform.position;

        direction.y = 0;

        if (direction.magnitude < 1.5f)
        {
            currentWaypoint++;

            if (currentWaypoint >= waypoints.Length)
                currentWaypoint = 0;

            return;
        }

        transform.position +=
            direction.normalized *
            speed * Time.deltaTime;

        transform.rotation =
            Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(direction),
                5f * Time.deltaTime
            );
    }
}
