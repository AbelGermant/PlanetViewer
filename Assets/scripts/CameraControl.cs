using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraControl : MonoBehaviour
{
    public InputActionReference leftClick;    // Reference for the left-click action
    public InputActionReference RightClick;    // Reference for the right-click action
    public InputActionReference scrollWheel;  // Reference for the scroll action (optional if used for zooming)

    public Camera cam;

    private Vector3 lastMousePositionLeft;
    private Vector3 lastMousePositionRight;
    private bool isPanning = false;
    private bool isRotating = false;

    // Start is called before the first frame update
    void Start()
    {
        lastMousePositionLeft = Mouse.current.position.ReadValue();

        // Enable input actions
        leftClick.action.Enable();
        RightClick.action.Enable();
        scrollWheel.action.Enable();

        // Subscribe to the left-click started and canceled events
        leftClick.action.started += OnLeftClickStarted;
        leftClick.action.canceled += OnLeftClickCanceled;

        RightClick.action.started += OnRightClickStarted;
        RightClick.action.canceled += onRightClickCanceled;

        scrollWheel.action.performed += context => {
            // zoom
            Vector2 zoom = context.ReadValue<Vector2>();
            float zoomAmount = zoom.y*0.01f;
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

            // get camera direction
            Vector3 direction = cam.transform.forward;
            


            lastMousePositionLeft = currentMousePosition;
        }

        if (isRotating)
        {
            Vector3 currentMousePosition = Mouse.current.position.ReadValue();
            Vector3 mouseDelta = currentMousePosition - lastMousePositionRight;
            Vector3 rotate = new Vector3(-mouseDelta.y, mouseDelta.x, 0) * Time.deltaTime;
            cam.transform.Rotate(rotate, Space.World);
        }
    }

    // Triggered when the left-click starts (button pressed)
    private void OnLeftClickStarted(InputAction.CallbackContext context)
    {
        isPanning = true;
        lastMousePositionLeft = Mouse.current.position.ReadValue();  // Store initial mouse position
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
        scrollWheel.action.Disable();
    }
}

