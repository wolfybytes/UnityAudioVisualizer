using UnityEngine;
using UnityEngine.UI;

public class ToolControlButtons : MonoBehaviour
{
    private Color selectedColor;

    public Button[] buttons;
    public Image[] selectionImages;

    private void Start()
    {
        SceneVisualsController.instance.onToolSelectionUpdate += UpdateSelectionImages;

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
        SceneVisualsController.instance.SetCurrentTool(0);
    }

    public void UseRotation()
    {
        SceneVisualsController.instance.SetCurrentTool(1);
    }

    public void UseScale()
    {
        SceneVisualsController.instance.SetCurrentTool(2);
    }

    public void ResetAll()
    {
        SceneVisualsController.instance.current.ResetModel();
    }

    public void UpdateSelectionImages(int newSelection)
    {
        for (int i = 0; i < selectionImages.Length; i++)
        {
            selectedColor = selectionImages[i].color;
            if (i == newSelection)
                selectionImages[i].color = new Color(selectedColor.r, selectedColor.g, selectedColor.b, 1);
            else
                selectionImages[i].color = new Color(selectedColor.r, selectedColor.g, selectedColor.b, 0);
        }
    }
}
