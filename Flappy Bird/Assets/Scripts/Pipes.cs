using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipes : MonoBehaviour
{
    public GameObject pipes;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() {
        
            Move();
        
     }

    public void Move()
    {
        pipes.transform.position = new Vector2(
            pipes.transform.position.x - 0.01f,
            pipes.transform.position.y
        );
    }

    
}
