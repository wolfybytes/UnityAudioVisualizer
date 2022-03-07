using UnityEngine;
using UnityEngine.UI;

public class ToolControlButtons : MonoBehaviour
{
    public SceneVisualsController controller;
    public Button[] buttons;

    private void Start()
    {
        buttons[0].onClick.AddListener(delegate {
            UseTransform();
        });
        buttons[1].onClick.AddListener(delegate {
            UseRotation();
        });
        buttons[2].onClick.AddListener(delegate {
            UseScale();
        });
        buttons[3].onClick.AddListener(delegate {
            ResetAll();
        });
    }

    public void UseTransform()
    {
        controller.SetCurrentTool(0);
    }

    public void UseRotation()
    {
        controller.SetCurrentTool(1);
    }

    public void UseScale()
    {
        controller.SetCurrentTool(2);
    }

    public void ResetAll()
    {
        controller.current.ResetModel();
    }
}
