using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneVisualsController : MonoBehaviour
{
    public ObjectTools current;
    public static SceneVisualsController instance;

    public delegate void OnToolSelectionUpdate(int tool);
    public OnToolSelectionUpdate onToolSelectionUpdate;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    public void SetCurrentTool(int tool)
    {
        current.selectedTool = (ObjectTools.ToolSelection)tool;

        onToolSelectionUpdate?.Invoke(tool);
    }

    public void SetCurrentObject(ObjectTools tool)
    {
        current = tool;
    }

    public ObjectTools GetCurrentTool()
    {
        return current;
    }
}
