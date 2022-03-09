using UnityEngine;
using UnityEngine.UI;

public class CameraControlButtons : MonoBehaviour
{
    private CameraOrbit orbit;
    private Color selectedColor;

    public Button[] buttons;
    public Image[] selectionImages;

    private void Start()
    {
        orbit = Camera.main.GetComponent<CameraOrbit>();
        orbit.onModeSelectionUpdate += UpdateSelectionImages;

        buttons[0].onClick.AddListener(delegate {
            UseRotation();
        });
        buttons[1].onClick.AddListener(delegate {
            UseZoom();
        });
        buttons[2].onClick.AddListener(delegate {
            ResetAll();
        });

        orbit.SetCurrentMode(0);
    }

    public void UseRotation()
    {
        orbit.SetCurrentMode(0);
    }

    public void UseZoom()
    {
        if (orbit.controlType != CameraOrbit.ControlType.Zoom)
            orbit.SetCurrentMode(1);
        else
            orbit.SetCurrentMode(0);
    }

    public void ResetAll()
    {
        orbit.ResetCamera();
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
