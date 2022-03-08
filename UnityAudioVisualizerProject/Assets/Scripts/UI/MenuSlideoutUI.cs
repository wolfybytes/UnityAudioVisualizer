using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSlideoutUI : MonoBehaviour
{
    private Vector3 targetPosition;
    private RectTransform rectTransform;
    private bool completedTransition = true;

    public bool isOpen = false;

    public Vector3 closedPostion;
    public Vector3 openPosition;
    public float transitionTime = .25f;

    private float timer = 9;
    private float percentage = 0;

    public void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        if (isOpen)
            rectTransform.anchoredPosition3D = openPosition;
        else
            rectTransform.anchoredPosition3D = closedPostion;
    }

    private void Update()
    {
        if (!completedTransition) {
            if (isOpen)
                rectTransform.anchoredPosition3D = Vector3.Lerp(closedPostion, openPosition, percentage);
            else
                rectTransform.anchoredPosition3D = Vector3.Lerp(openPosition, closedPostion, percentage);

            timer += Time.deltaTime;
        }
        if (percentage == 1)
            completedTransition = true;
        percentage = Mathf.Clamp01(timer / transitionTime);
        
    }

    public void ToggleMenu()
    {
        if (isOpen)
            isOpen = false;
        else
            isOpen = true;

        percentage = 0f;
        timer = 0f;
        completedTransition = false;
    }
}
