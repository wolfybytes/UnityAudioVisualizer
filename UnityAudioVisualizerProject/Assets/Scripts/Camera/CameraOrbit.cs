using UnityEngine;
using System.Collections;

public class CameraOrbit : MonoBehaviour 
{
    private Quaternion QT;

    private float startingDistance;
    private Vector2 startingRotation;

    protected Transform _XForm_Camera;
    protected Transform _XForm_Parent;

    protected Vector3 _LocalRotation;
    public float _CameraDistance = 10f;

    public float MouseSensitivity = 4f;
    public float ScrollSensitvity = 2f;
    public float OrbitDampening = 10f;
    public float ScrollDampening = 6f;
    public float zoomSensitivity = 2f;
    public float panSensitivity = 2f;

    public bool CameraDisabled = false;

    public delegate void OnModeSelectionUpdate(int mode);
    public OnModeSelectionUpdate onModeSelectionUpdate;

    public enum ControlType
    {
        Pan,
        Rotation,
        Zoom,
        None
    };
    public ControlType controlType = ControlType.Rotation;


    private void Start() {
        this._XForm_Camera = this.transform;
        this._XForm_Parent = this.transform.parent;

        _LocalRotation.x = this._XForm_Parent.eulerAngles.y;
        _LocalRotation.y = this._XForm_Parent.eulerAngles.x;

        startingDistance = _CameraDistance;
        startingRotation = new Vector2(_LocalRotation.x, _LocalRotation.y);
    }


    private void Update()
    {
        if (Input.GetMouseButton(0) && !SceneVisualsController.instance.current.isDragging) {
            CameraDisabled = false;
        } else {
            CameraDisabled = true;
        }
    }

    private void LateUpdate() {
        if (!CameraDisabled)
        {
            //Rotation of the Camera based on Mouse Coordinates
            if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
            {
                if (controlType == ControlType.Pan) {

                } else if (controlType == ControlType.Rotation) {
                    _LocalRotation.x += Input.GetAxis("Mouse X") * MouseSensitivity;
                    _LocalRotation.y -= Input.GetAxis("Mouse Y") * MouseSensitivity;

                    //Clamp the y Rotation to horizon and not flipping over at the top
                    if (_LocalRotation.y < 0f)
                        _LocalRotation.y = 0f;
                    else if (_LocalRotation.y > 90f)
                        _LocalRotation.y = 90f;
                } else if (controlType == ControlType.Zoom) {
                    float ScrollAmount = Input.GetAxis("Mouse Y") * zoomSensitivity;
                    ScrollAmount *= (this._CameraDistance * 0.3f);
                    this._CameraDistance += ScrollAmount * -1f;
                    this._CameraDistance = Mathf.Clamp(this._CameraDistance, 1.5f, 100f);
                }
            }
        }

        //Zooming Input from our Mouse Scroll Wheel
        if (Input.GetAxis("Mouse ScrollWheel") != 0f)
        {
            float ScrollAmount = Input.GetAxis("Mouse ScrollWheel") * ScrollSensitvity;

            ScrollAmount *= (this._CameraDistance * 0.3f);

            this._CameraDistance += ScrollAmount * -1f;

            this._CameraDistance = Mathf.Clamp(this._CameraDistance, 1.5f, 100f);
        }

        //Actual Camera Rig Transformations
        QT = Quaternion.Euler(_LocalRotation.y, _LocalRotation.x, 0);
        this._XForm_Parent.rotation = Quaternion.Lerp(this._XForm_Parent.rotation, QT, Time.deltaTime * OrbitDampening);

        if ( this._XForm_Camera.localPosition.z != this._CameraDistance * -1f )
        {
            this._XForm_Camera.localPosition = new Vector3(0f, 0f, Mathf.Lerp(this._XForm_Camera.localPosition.z, this._CameraDistance * -1f, Time.deltaTime * ScrollDampening));
        }
    }

    public void ResetCamera()
    {
        _LocalRotation.x = startingRotation.x;
        _LocalRotation.y = startingRotation.y;
        _CameraDistance = startingDistance;
    }

    public void SetCurrentMode(int mode)
    {
        controlType = (ControlType)mode;

        onModeSelectionUpdate?.Invoke(mode);
    }
}