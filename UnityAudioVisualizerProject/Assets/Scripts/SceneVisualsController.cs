using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneVisualsController : MonoBehaviour
{
    private int modelIndex = 0;
    private int environmentIndex = 0;

    public ObjectTools current;
    public ObjectTools[] toolModels;
    public GameObject[] environments;
     
    public static SceneVisualsController instance;

    public delegate void OnIntUpdate(int tool);
    public OnIntUpdate onToolSelectionUpdate;
    public OnIntUpdate onModelChangeUpdate;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

        for (int i = 0; i < toolModels.Length; i++)
            toolModels[i].gameObject.SetActive(false);
        SetCurrentObject(0);

        for (int i = 0; i < environments.Length; i++)
            environments[i].SetActive(false);
        SetCurrentEnvironment(0);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
            AdvanceCurrentToolModel();
        if (Input.GetKeyDown(KeyCode.DownArrow))
            AdvanceCurrentEnvironment();
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

    public void SetCurrentEnvironment(int envIndex)
    {
        environments[environmentIndex].SetActive(false);

        environmentIndex = envIndex;
        environments[environmentIndex].SetActive(true);
    }

    public void AdvanceCurrentEnvironment()
    {
        int nextIndex = (environmentIndex + 1) % environments.Length;
        SetCurrentEnvironment(nextIndex);
    }

    public ObjectTools GetCurrentTool()
    {
        return current;
    }
}
