using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneVisualsController : MonoBehaviour
{
    public ObjectTools current;
    public ObjectTools[] toolModels;
    public int modelIndex = 0; 
    public static SceneVisualsController instance;

    public delegate void OnToolSelectionUpdate(int tool);
    public OnToolSelectionUpdate onToolSelectionUpdate;
    public OnToolSelectionUpdate onModelChangeUpdate;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

        for (int i = 0; i < toolModels.Length; i++)
            toolModels[i].gameObject.SetActive(false);
        SetCurrentObject(0);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
            AdvanceCurrentToolModel();
    }

    public void SetCurrentTool(int tool)
    {
        current.selectedTool = (ObjectTools.ToolSelection)tool;

        onToolSelectionUpdate?.Invoke(tool);
    }

    public void SetCurrentObject(int toolIndex)
    {
        toolModels[modelIndex].gameObject.SetActive(false);

        modelIndex = toolIndex;
        current = toolModels[modelIndex];
        toolModels[modelIndex].gameObject.SetActive(true);

        onModelChangeUpdate?.Invoke(toolIndex);
    }

    public void AdvanceCurrentToolModel()
    {
        int nextIndex = (modelIndex + 1) % toolModels.Length;
        SetCurrentObject(nextIndex);
    }

    public ObjectTools GetCurrentTool()
    {
        return current;
    }
}
