
using UnityEngine;

public class MissionTrigger : MonoBehaviour
{
    public string missionTitle =
        "Reach the destination";

    public string completionMessage =
        "MISSION COMPLETE!";

    public GameObject markerVisual;

    private bool completed = false;

    void OnTriggerEnter(Collider other)
    {
        if (completed) return;

        bool isPlayer =
            other.CompareTag("Player");

        bool isCar =
            other.GetComponent<ArcadeCarController>()
            != null;

        if (!isPlayer && !isCar) return;

        completed = true;

        if (markerVisual != null)
            markerVisual.SetActive(false);

        MissionHUD hud =
            FindObjectOfType<MissionHUD>();

        if (hud != null)
            hud.ShowMessage(completionMessage);
    }
}
