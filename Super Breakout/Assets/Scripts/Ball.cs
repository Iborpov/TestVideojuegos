using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    float speed = 5;
    // Start is called before the first frame update
    void Start()
    {
        Launch();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Launch()
    {
        float x = UnityEngine.Random.Range(0, 2) == 0 ? -1 : 1;
        GetComponent<Rigidbody2D>().velocity = new Vector2(x*speed, 1*speed);
    }
}
