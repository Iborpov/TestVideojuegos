using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraManager : MonoBehaviour
{
    [SerializeField]
    CinemachineCamera virtualCamera;

    [SerializeField]
    float speed = 5f;

    [SerializeField]
    GameObject cameraControler;

    CinemachineFollow transposer;

    Vector2 direction;

    [SerializeField]
    float rotationSpeed = 40;
    float rotation = 0;

    float zoom;

    float zoomVelocity;

    float targetOffsetValue;

    private void Awake()
    {
        transposer = virtualCamera.GetComponent<CinemachineFollow>();
        targetOffsetValue = transposer.FollowOffset.y;
    }

    void Start() { }

    void Update()
    {
        ApplyMovement();
        ApplyRotation();
        ApplyZoom();
    }

    private void ApplyMovement()
    {
        Vector3 camChange =
            cameraControler.transform.forward * direction.y
            + cameraControler.transform.right * direction.x;
        cameraControler.transform.position += camChange * speed * Time.deltaTime;
    }

    private void ApplyRotation()
    {
        Vector3 rotationVector = new Vector3(0, rotation, 0);
        cameraControler.transform.eulerAngles += rotationVector * rotationSpeed * Time.deltaTime;
    }

    private void ApplyZoom()
    {
        transposer.FollowOffset.y = Mathf.SmoothDamp(
            transposer.FollowOffset.y,
            targetOffsetValue,
            ref zoomVelocity,
            .3f
        );
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();
        if (direction != Vector2.zero)
        {
            direction.Normalize();
        }
    }

    public void OnRotate(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            rotation = context.ReadValue<float>();
        }
        else if (context.canceled)
        {
            rotation = 0;
        }
    }

    public void OnZoom(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            zoom = context.ReadValue<float>();
            targetOffsetValue -= zoom;
            targetOffsetValue = Math.Clamp(targetOffsetValue, .5f, 12);
        }
    }
}
