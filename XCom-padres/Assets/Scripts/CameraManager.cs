using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField]
    CinemachineVirtualCamera virtualCamera;

    CinemachineTransposer transposer;
    float targetOffsetValue;

    float rotationSpeed = 5;

    private void Awake()
    {
        transposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        targetOffsetValue = transposer.m_FollowOffset.y;
    }

    void Start() { }

    void Update() { }

    private void ApplyRotation()
    {
        Vector3 rotationVector = Vector3.zero;
        //rotationVector.y = rotation;

        transform.eulerAngles += rotationVector * rotationSpeed * Time.deltaTime;
    }
}
