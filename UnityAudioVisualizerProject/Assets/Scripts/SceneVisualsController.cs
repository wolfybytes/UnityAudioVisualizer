using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneVisualsController : MonoBehaviour
{
    public ObjectTools current;

    public void SetCurrentTool(int tool)
    {
        current.selectedTool = (ObjectTools.ToolSelection)tool;
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
