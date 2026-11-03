
using UnityEngine;
using UnityEngine.EventSystems;

public class MobileCameraController : MonoBehaviour,
    IDragHandler
{
    public Transform target;

    public float distance = 6f;
    public float height = 3f;
    public float sensitivity = 0.3f;

    private float yaw = 0f;
    private float pitch = 15f;

    public void OnDrag(PointerEventData eventData)
    {
        yaw += eventData.delta.x * sensitivity;
        pitch -= eventData.delta.y * sensitivity;

        pitch = Mathf.Clamp(pitch, -10f, 60f);
    }

    void LateUpdate()
    {
        if (target == null) return;

        Quaternion rotation =
            Quaternion.Euler(pitch, yaw, 0);

        Vector3 offset = rotation *
            new Vector3(0, height, -distance);

        transform.position =
            target.position + offset;

        transform.LookAt(
            target.position + Vector3.up * 1.5f
        );
    }
}
