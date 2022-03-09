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
        buttons[4].onClick.AddListener(delegate {
            ToggleCharacter();
        });
    }

    public void UseTransform()
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

    public void UseScale()
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

    public void ToggleCharacter()
    {
        SceneVisualsController.instance.AdvanceCurrentToolModel();
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
