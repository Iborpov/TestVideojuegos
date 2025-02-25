using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraManager : MonoBehaviour
{
    [SerializeField]
    CinemachineVirtualCamera virtualCamera;

    [SerializeField]
    float speed = 5f;

    [SerializeField]
    GameObject cameraControler;

    CinemachineTransposer transposer;

    Vector2 direction;

    [SerializeField]
    float rotationSpeed = 40;
    float rotation = 0;

    [SerializeField]
    float zoomSpeed = 5;
    float zoom;

    float zoomVelocity;

    float targetOffsetValue;

    private void Awake()
    {
        transposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        targetOffsetValue = transposer.m_FollowOffset.y;
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
        transposer.m_FollowOffset.y = Mathf.SmoothDamp(
            transposer.m_FollowOffset.y,
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
            targetOffsetValue -= zoom / 90f;
            targetOffsetValue = Math.Clamp(targetOffsetValue, .5f, 12);
        }
    }
}
