
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ArcadeCarController : MonoBehaviour
{
    public float acceleration = 18f;
    public float reversePower = 10f;
    public float steeringSpeed = 85f;
    public float maxSpeed = 28f;

    public Transform seatPoint;
    public Transform exitPoint;

    private Rigidbody body;
    private bool playerDriving;

    private float throttle;
    private float steer;

    void Awake()
    {
        body = GetComponent<Rigidbody>();

        body.mass = 1200f;
        body.drag = 0.25f;
        body.angularDrag = 2f;

        body.interpolation =
            RigidbodyInterpolation.Interpolate;

        body.centerOfMass =
            new Vector3(0, -0.5f, 0);
    }

    void Update()
    {
        if (!playerDriving) return;

        throttle = Input.GetAxis("Vertical");
        steer = Input.GetAxis("Horizontal");
    }

    void FixedUpdate()
    {
        if (!playerDriving) return;

        float power = throttle >= 0
            ? acceleration : reversePower;

        if (body.velocity.magnitude < maxSpeed
            || throttle < 0)
        {
            body.AddForce(
                transform.forward * throttle * power,
                ForceMode.Acceleration
            );
        }

        float speedFactor =
            Mathf.Clamp01(body.velocity.magnitude / 3f);

        float direction =
            Vector3.Dot(body.velocity,
            transform.forward) >= 0 ? 1f : -1f;

        Quaternion turn = Quaternion.Euler(
            0,
            steer * steeringSpeed *
            speedFactor * direction *
            Time.fixedDeltaTime,
            0
        );

        body.MoveRotation(body.rotation * turn);
    }

    public void SetPlayerDriving(bool value)
    {
        playerDriving = value;
    }

    public bool IsPlayerDriving()
    {
        return playerDriving;
    }
}
