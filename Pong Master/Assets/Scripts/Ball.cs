using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed = 5;
    public Rigidbody2D rbody;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }

    public void Launch()
    {
        float x = UnityEngine.Random.Range(0, 2) == 0 ? -1 : 1;
        float y = UnityEngine.Random.Range(0, 2) == 0 ? -1 : 1;
        rbody.velocity = new Vector2(x * speed, y * speed);
    }

    public void AcelerateBall()
    {
        rbody.velocity = new Vector2(rbody.velocity.x * 1.1f, rbody.velocity.y * 1.1f);
    }
}
