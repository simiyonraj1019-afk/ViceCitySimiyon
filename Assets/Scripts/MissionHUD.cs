
using UnityEngine;
using UnityEngine.UI;

public class MissionHUD : MonoBehaviour
{
    public Text missionText;
    public Text helpText;

    private float messageTimer;

    void Start()
    {
        if (missionText != null)
        {
            missionText.text =
                "MISSION: Explore the City";
        }

        if (helpText != null)
        {
            helpText.text =
                "WASD: Move | SHIFT: Run | E: Car";
        }
    }

    void Update()
    {
        if (messageTimer > 0)
        {
            messageTimer -= Time.deltaTime;

            if (messageTimer <= 0 &&
                missionText != null)
            {
                missionText.text =
                    "MISSION: Explore the City";
            }
        }
    }

    public void ShowMessage(string message)
    {
        if (missionText != null)
            missionText.text = message;

        messageTimer = 5f;
    }
}
