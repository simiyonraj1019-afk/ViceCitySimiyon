
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MobilePlayerController : MonoBehaviour
{
    public MobileJoystick joystick;
    public Transform cameraTransform;

    public float moveSpeed = 5f;
    public float gravity = -20f;
    public float rotationSpeed = 10f;

    private CharacterController controller;
    private Vector3 velocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (joystick == null) return;

        Vector2 input = joystick.InputValue;

        Vector3 direction = new Vector3(
            input.x, 0, input.y
        );

        if (cameraTransform != null)
        {
            Vector3 forward = cameraTransform.forward;
            Vector3 right = cameraTransform.right;

            forward.y = 0;
            right.y = 0;

            direction =
                forward.normalized * input.y +
                right.normalized * input.x;
        }

        direction = Vector3.ClampMagnitude(direction, 1f);

        controller.Move(
            direction * moveSpeed * Time.deltaTime
        );

        if (direction.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation =
                Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }

        if (controller.isGrounded && velocity.y < 0)
            velocity.y = -2f;

        velocity.y += gravity * Time.deltaTime;

        controller.Move(
            velocity * Time.deltaTime
        );
    }
}
