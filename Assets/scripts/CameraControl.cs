using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class CameraControl : MonoBehaviour
{

    public static CameraControl current;

    private void Awake()
    {
        if (current == null)
        {
            current = this;
        }
        else
        {
            Destroy(obj: this);
        }
    }


    public InputActionReference leftClick;    // Reference for the left-click action
    public InputActionReference RightClick;    // Reference for the right-click action
    public InputActionReference ScrollWheel;  // Reference for the scroll action (optional if used for zooming)

    public GameObject sun;

    public Camera cam;

    private Vector3 lastMousePositionLeft;
    private Vector3 lastMousePositionRight;
    private bool isPanning = false;
    private bool isRotating = false;

    private bool haveParent = false;

    // Start is called before the first frame update
    void Start()
    {
        lastMousePositionLeft = Mouse.current.position.ReadValue();

        // Enable input actions
        leftClick.action.Enable();
        RightClick.action.Enable();
        ScrollWheel.action.Enable();

        // Subscribe to the left-click started and canceled events
        leftClick.action.started += OnLeftClickStarted;
        leftClick.action.canceled += OnLeftClickCanceled;

        RightClick.action.started += OnRightClickStarted;
        RightClick.action.canceled += onRightClickCanceled;

        ScrollWheel.action.performed += context => {
            // zoom
            Vector2 zoom = context.ReadValue<Vector2>();
            // multiply by the distance between the camera and the sun
            float zoomAmount = zoom.y * (cam.transform.position - sun.transform.position).magnitude * 0.001f;
            // direction of the camera
            Vector3 direction = cam.transform.forward;
            // move the camera
            cam.transform.position += direction * zoomAmount;
        };
    }

    // Update is called once per frame
    void Update()
    {
        // Pan the camera when left-click is held and the mouse is moving
        if (isPanning)
        {
            Vector3 currentMousePosition = Mouse.current.position.ReadValue();
            Vector3 mouseDelta = currentMousePosition - lastMousePositionLeft;

            // Calculate the pan movement based on the camera's right and up directions
            Vector3 panMovement = cam.transform.right * mouseDelta.x * 0.05f + cam.transform.up * mouseDelta.y * 0.05f;
            // multiply by the distance between the camera and the sun
            panMovement *= (cam.transform.position - sun.transform.position).magnitude*0.03f;
            // Apply the pan movement
            cam.transform.position -= panMovement;

            lastMousePositionLeft = currentMousePosition;
        }

        // Rotate the camera when right-click is held and the mouse is moving
        if (isRotating)
        {
            Vector3 currentMousePosition = Mouse.current.position.ReadValue();
            Vector3 mouseDelta = currentMousePosition - lastMousePositionRight;

            // Calculate the rotation angles
            float rotationX = mouseDelta.y * 0.1f; // Vertical mouse movement rotates around the X axis
            float rotationY = mouseDelta.x * 0.1f; // Horizontal mouse movement rotates around the Y axis

            // Apply the rotations
            cam.transform.eulerAngles -= new Vector3(-rotationX, rotationY, 0);

            lastMousePositionRight = currentMousePosition;
        }
    }

    // Triggered when the left-click starts (button pressed)
    private void OnLeftClickStarted(InputAction.CallbackContext context)
    {
        // check if click is on planet
        if (Physics.Raycast(cam.ScreenPointToRay(Mouse.current.position.ReadValue()), out RaycastHit hit))
        {

            // check if object in SolarSystemController planetGameObjects
            if (SolarSystemController.current.planetGameObjects.ContainsValue(hit.collider.gameObject))
            {
                // Set the camera's to follow the planet
                cam.transform.position = hit.collider.transform.position + new Vector3(0, 0, -3);
                cam.transform.LookAt(hit.collider.transform);

                // attach the camera to the planet
                cam.transform.parent = hit.collider.transform;
                haveParent = true;

                // get the planet
                foreach (KeyValuePair<PlanetData.Planet, GameObject> planet in SolarSystemController.current.planetGameObjects)
                {
                    if (planet.Value == hit.collider.gameObject)
                    {
                        UIControls.current.ShowInfo(planet.Key);
                    }
                }
            }
        }
        // check if click in on UI
        else if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        else
        {
            if(haveParent)
            {
                cam.transform.parent = null;
                haveParent = false;
                UIControls.current.HideInfo();
            }
            isPanning = true;
            lastMousePositionLeft = Mouse.current.position.ReadValue();  // Store initial mouse position
        }





    }

    // Triggered when the left-click is canceled (button released)
    private void OnLeftClickCanceled(InputAction.CallbackContext context)
    {
        isPanning = false;
    }

    private void OnRightClickStarted(InputAction.CallbackContext context)
    {
        isRotating = true;
        lastMousePositionRight = Mouse.current.position.ReadValue();  // Store initial mouse position
    }

    private void onRightClickCanceled(InputAction.CallbackContext context)
    {
        isRotating = false;
    }

    private void OnDisable()
    {
        // Unsubscribe from input events and disable input actions when this script is disabled
        leftClick.action.started -= OnLeftClickStarted;
        leftClick.action.canceled -= OnLeftClickCanceled;

        RightClick.action.started -= OnRightClickStarted;
        RightClick.action.canceled -= onRightClickCanceled;

        leftClick.action.Disable();
        RightClick.action.Disable();
        ScrollWheel.action.Disable();
    }

    public void CenterCamera()
    {
        cam.transform.position = new Vector3(-10, 0, -30);
        cam.transform.eulerAngles = new Vector3(0, 0, 0);
        cam.transform.parent = null;
        haveParent = false;
        UIControls.current.HideInfo();
    }
}

