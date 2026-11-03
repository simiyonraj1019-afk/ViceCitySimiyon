
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 4f;
    public float runSpeed = 7f;
    public float gravity = -20f;
    public Transform cameraTransform;
    public GameObject visualRoot;

    private CharacterController controller;
    private Vector3 velocity;
    private bool driving;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (driving) return;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(x, 0, z);

        if (cameraTransform != null)
        {
            Vector3 forward = cameraTransform.forward;
            Vector3 right = cameraTransform.right;
            forward.y = 0;
            right.y = 0;
            direction = forward.normalized * z + right.normalized * x;
        }

        direction = Vector3.ClampMagnitude(direction, 1f);

        float speed = Input.GetKey(KeyCode.LeftShift)
            ? runSpeed : walkSpeed;

        controller.Move(direction * speed * Time.deltaTime);

        if (direction.sqrMagnitude > 0.01f)
        {
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(direction),
                12f * Time.deltaTime
            );
        }

        if (controller.isGrounded && velocity.y < 0)
            velocity.y = -2f;

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    public void SetDriving(bool value)
    {
        driving = value;
        controller.enabled = !value;

        if (visualRoot != null)
            visualRoot.SetActive(!value);
    }
}
