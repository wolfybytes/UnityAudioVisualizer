using UnityEngine;
using UnityEngine.UI;

public class EnvironmentControlButtons : MonoBehaviour
{
    public Button[] buttons;

    private void Start()
    {
        buttons[0].onClick.AddListener(delegate {
            ToggleEnvironment();
        });
        buttons[1].onClick.AddListener(delegate {
            ToggleSunlight();
        });
    }

    public void ToggleEnvironment()
    {
        SceneVisualsController.instance.AdvanceCurrentEnvironment();
    }

    public void ToggleSunlight()
    {
        SunlightController.instance.AdvancePreset();
    }
}
