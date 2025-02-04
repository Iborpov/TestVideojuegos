using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    float currentPosx;
    float currentPosy;

    Vector3 velocity;
    public float speed = 10f;

    // Start is called before the first frame update
    void Awake() { }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.SmoothDamp(
            transform.position,
            new Vector3(currentPosx, currentPosy, transform.position.z),
            ref velocity,
            speed
        );
    }

    public void MoveToNewRoom(Transform newRoom)
    {
        currentPosx = newRoom.position.x;
        currentPosy = newRoom.position.y;
    }
}
