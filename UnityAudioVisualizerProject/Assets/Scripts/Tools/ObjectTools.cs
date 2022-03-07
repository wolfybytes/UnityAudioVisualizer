using UnityEngine;

public class ObjectTools : MonoBehaviour
{
    private Vector3 startingPosition;
    private Vector3 startingRotation;
    private Vector3 startingScale;

    private RaycastHit hit;
    private Ray ray;

    private Vector3 mouseOffset;
    private float mouseZCoord;
    private Vector3 mouseDownPosition;
    private float previousDragDistance;

    public float rotationStrengthMod = .02f;
    public float scaleStrengthMod = .001f;
    
    [HideInInspector] public bool isDragging = false;

    public enum ToolSelection {
        Transform,
        Rotate,
        Scale,
        None
    };
    public ToolSelection selectedTool = ToolSelection.Transform;

    private void Awake()
    {
        startingPosition = transform.position;
        startingRotation = transform.rotation.eulerAngles;
        startingScale = transform.localScale;
    }

    private void OnMouseDown()
    {
        mouseZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

        // Check to see if gameObject was clicked
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100f)) {
            // if it wasn't clicked, set current tool to None
            if (hit.transform == null)
                SceneVisualsController.instance.SetCurrentTool(3);
        }

        mouseOffset = gameObject.transform.position - GetMouseWorldPos();
        mouseDownPosition = Input.mousePosition;
    }

    private void OnMouseUp()
    {
        isDragging = false;
    }

    private void OnMouseDrag()
    {
        isDragging = true;

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

    private void OnEnable()
    {
        SceneVisualsController.instance.onToolSelectionUpdate += UpdateSelectedTool;
        ResetModel();
        SceneVisualsController.instance.SetCurrentTool(3);
    }

    private void OnDisable()
    {
        SceneVisualsController.instance.onToolSelectionUpdate -= UpdateSelectedTool;
    }

    public void UpdateSelectedTool(int selectedTool)
    {
        this.selectedTool = (ToolSelection)selectedTool;
    }
}
