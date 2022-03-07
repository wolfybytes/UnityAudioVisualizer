using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTools : MonoBehaviour
{
    private Vector3 startingPosition;
    private Vector3 startingRotation;
    private Vector3 startingScale;

    private Vector3 mouseOffset;
    private float mouseZCoord;
    private Vector3 mouseDownPosition;
    private float previousDragDistance;

    public float rotationStrengthMod = .02f;
    public float scaleStrengthMod = .001f;

    public enum ToolSelection {
        Transform,
        Rotate,
        Scale
    };
    public ToolSelection selectedTool = ToolSelection.Transform;

    private void Start()
    {
        startingPosition = transform.position;
        startingRotation = transform.rotation.eulerAngles;
        startingScale = transform.localScale;
    }

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
        } else if (selectedTool == ToolSelection.Scale) {
            if (GetDistanceFromClickedPoint() != previousDragDistance)
            {
                transform.localScale = new Vector3(transform.localScale.x + (GetDistanceFromClickedPoint() * scaleStrengthMod), transform.localScale.y + (GetDistanceFromClickedPoint() * scaleStrengthMod), transform.localScale.z + (GetDistanceFromClickedPoint() * scaleStrengthMod));
                previousDragDistance = GetDistanceFromClickedPoint();
            }
        }
    }

    private float GetDistanceFromClickedPoint()
    {
        float distance = Vector3.Distance(mouseDownPosition, Input.mousePosition);
        if (selectedTool == ToolSelection.Rotate) {
            if (mouseDownPosition.x < Input.mousePosition.x)
                distance = -distance;
        } else if (selectedTool == ToolSelection.Scale) {
            if (mouseDownPosition.x > Input.mousePosition.x)
                distance = -distance;
        }
        return distance;
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mouseZCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    public void ResetModel()
    {
        transform.position = startingPosition;
        transform.rotation = Quaternion.Euler(startingRotation);
        transform.localScale = startingScale;
    }
}
