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
    float targetOffsetValue;

    float rotationSpeed = 5;

    private void Awake()
    {
        transposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        targetOffsetValue = transposer.m_FollowOffset.y;
    }

    void Start() { }

    void Update()
    {
        Vector3 camChange = new Vector3(direction.x, 0, direction.y);
        cameraControler.transform.position += camChange * speed * Time.deltaTime;
    }

    private void ApplyRotation()
    {
        Vector3 rotationVector = Vector3.zero;
        transform.eulerAngles += rotationVector * rotationSpeed * Time.deltaTime;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Debug.Log(context);
        direction = context.ReadValue<Vector2>();
        if (direction != Vector2.zero)
        {
            direction.Normalize();
        }
    }

    public void OnRotate(InputAction.CallbackContext context)
    {
        Debug.Log(context);
    }

    public void OnZoom(InputAction.CallbackContext context)
    {
        Debug.Log(context);
    }
}
