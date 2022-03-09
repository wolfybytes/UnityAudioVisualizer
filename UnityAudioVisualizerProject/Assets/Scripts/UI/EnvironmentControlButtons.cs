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
    }

    public void ToggleEnvironment()
    {
        SceneVisualsController.instance.AdvanceCurrentEnvironment();
    }
}
