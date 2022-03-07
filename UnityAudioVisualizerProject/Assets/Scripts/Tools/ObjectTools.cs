using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTools : MonoBehaviour
{
    private Vector3 mouseOffset;
    private float mouseZCoord;
    private Vector3 mouseDownPosition;
    private float previousDragDistance;

    public float rotationStrengthMod = .1f;

    public enum ToolSelection {
        Transform,
        Rotate,
        Scale
    };
    public ToolSelection selectedTool = ToolSelection.Transform;

    private void OnMouseDown()
    {
        mouseZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mouseOffset = gameObject.transform.position - GetMouseWorldPos();
        mouseDownPosition = Input.mousePosition;
    }

    private void OnMouseDrag()
    {
        if (selectedTool == ToolSelection.Transform) {
            transform.position = GetMouseWorldPos() + mouseOffset;
        } else if (selectedTool == ToolSelection.Rotate) {
            if (GetDistanceFromClickedPoint() != previousDragDistance)
            {
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + (GetDistanceFromClickedPoint() * rotationStrengthMod), transform.rotation.eulerAngles.z);
                previousDragDistance = GetDistanceFromClickedPoint();
            }
        }
    }

    private float GetDistanceFromClickedPoint()
    {
        return Vector3.Distance(mouseDownPosition, Input.mousePosition);
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mouseZCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}
