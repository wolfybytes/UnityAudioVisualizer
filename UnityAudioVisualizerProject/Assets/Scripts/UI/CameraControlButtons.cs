using UnityEngine;
using UnityEngine.UI;

public class CameraControlButtons : MonoBehaviour
{
    private Color selectedColor;

    public Button[] buttons;
    public Image[] selectionImages;

    private void Start()
    {
        SceneVisualsController.instance.onToolSelectionUpdate += UpdateSelectionImages;

        buttons[0].onClick.AddListener(delegate {
            UsePan();
        });
        buttons[1].onClick.AddListener(delegate {
            UseRotation();
        });
        buttons[2].onClick.AddListener(delegate {
            UseZoom();
        });
        buttons[3].onClick.AddListener(delegate {
            ResetAll();
        });
    }

    public void UsePan()
    {
        if (SceneVisualsController.instance.current.selectedTool != ObjectTools.ToolSelection.Transform)
            SceneVisualsController.instance.SetCurrentTool(0);
        else
            SceneVisualsController.instance.SetCurrentTool(3);
    }

    public void UseRotation()
    {
        if (SceneVisualsController.instance.current.selectedTool != ObjectTools.ToolSelection.Rotate)
            SceneVisualsController.instance.SetCurrentTool(1);
        else
            SceneVisualsController.instance.SetCurrentTool(3);
    }

    public void UseZoom()
    {
        if (SceneVisualsController.instance.current.selectedTool != ObjectTools.ToolSelection.Scale)
            SceneVisualsController.instance.SetCurrentTool(2);
        else
            SceneVisualsController.instance.SetCurrentTool(3);
    }

    public void ResetAll()
    {
        SceneVisualsController.instance.current.ResetModel();
        SceneVisualsController.instance.SetCurrentTool(3);
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
