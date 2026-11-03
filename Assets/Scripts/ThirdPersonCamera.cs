
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target;

    public Vector3 offset =
        new Vector3(0, 3.2f, -5.5f);

    public float mouseSensitivity = 3f;
    public float smoothSpeed = 12f;

    private float yaw;
    private float pitch = 12f;

    void LateUpdate()
    {
        if (target == null) return;

        if (Input.GetMouseButton(1))
        {
            yaw += Input.GetAxis("Mouse X")
                * mouseSensitivity;

            pitch -= Input.GetAxis("Mouse Y")
                * mouseSensitivity;

            pitch = Mathf.Clamp(pitch, -10f, 55f);
        }

        Quaternion rotation =
            Quaternion.Euler(pitch, yaw, 0);

        Vector3 desiredPosition =
            target.position + rotation * offset;

        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            smoothSpeed * Time.deltaTime
        );

        transform.LookAt(
            target.position + Vector3.up * 1.4f
        );
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
