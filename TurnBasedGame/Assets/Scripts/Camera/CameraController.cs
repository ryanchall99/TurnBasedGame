using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    // CLAMP OFFSETS
    private const float MIN_FOLLOW_Y_OFFSET = 2f;
    private const float MAX_FOLLOW_Y_OFFSET = 12f;

    // CINEMACHINE CAMERA
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;

    private CinemachineTransposer cinemachineTransposer;
    private Vector3 targetFollowOffset;

    // CAMERA MOVE / ROTATION / ZOOM VALUES
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private float zoomSpeed = 5f;

    private void Start()
    {
        cinemachineTransposer = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>(); // Storing Transposer Component
        targetFollowOffset = cinemachineTransposer.m_FollowOffset; // Storing Initial Camera Offset On Start
    }

    private void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleZoom();
    }

    private void HandleMovement()
    {
        Vector3 inputMoveDir = new Vector3(0, 0, 0);

        if (Input.GetKey(KeyCode.W))
        {
            inputMoveDir.z += 1f; // Forward Key
        }

        if (Input.GetKey(KeyCode.A))
        {
            inputMoveDir.x += -1f; // Left Key
        }

        if (Input.GetKey(KeyCode.S))
        {
            inputMoveDir.z += -1f; // Backwards Key
        }

        if (Input.GetKey(KeyCode.D))
        {
            inputMoveDir.x += 1f; // Right Key
        }

        Vector3 moveVector = transform.forward * inputMoveDir.z + transform.right * inputMoveDir.x; // Movement based on rotation due to transform.right & transform.forward
        transform.position += moveVector * moveSpeed * Time.deltaTime; // Updating Cameras Transform.position based on rotation and keyboard inputs.
    }

    private void HandleRotation()
    {
        Vector3 rotationVector = new Vector3(0, 0, 0);

        if (Input.GetKey(KeyCode.Q))
        {
            rotationVector.y += 1f; // Left Rotation Key
        }

        if (Input.GetKey(KeyCode.E))
        {
            rotationVector.y += -1f; // Right Rotation Key
        }

        transform.eulerAngles += rotationVector * rotationSpeed * Time.deltaTime; // Updating Camera Rotation Based On Key Presses (Only Y Axis)
    }

    private  void HandleZoom()
    {
        float zoomAmount = 1f; // Value to increase each zoom level

        // PLAYER INPUT
        if (Input.mouseScrollDelta.y > 0f) // Zooming In
        {
            targetFollowOffset.y -= zoomAmount; // Update offset by negative zoom amount
        }

        if (Input.mouseScrollDelta.y < 0f) // Zooming Out
        {
            targetFollowOffset.y += zoomAmount; // Update offset by zoom amount
        }

        // CLAMPING OFFSET
        targetFollowOffset.y = Mathf.Clamp(targetFollowOffset.y, MIN_FOLLOW_Y_OFFSET, MAX_FOLLOW_Y_OFFSET); // Clamping Offset By Min / Max Value (Stops Over Zooming)

        // UPDATING OFFSET
        if (cinemachineTransposer.m_FollowOffset != targetFollowOffset) // Only update if values are not the same...
        {
            cinemachineTransposer.m_FollowOffset =
                 Vector3.Lerp(cinemachineTransposer.m_FollowOffset, targetFollowOffset, Time.deltaTime * zoomSpeed); // Updating new offset by linear interpolation for smooth zooming
        }
    }
}
