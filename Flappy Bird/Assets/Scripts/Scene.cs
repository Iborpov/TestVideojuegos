using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene : MonoBehaviour
{

    public GameObject back1;
    public GameObject back2;
    public GameObject ground1;
    public GameObject ground2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Scroll()
    {
        //Background
        if (back1.transform.position.x <= -24.28f)
        {
            back1.transform.position = new Vector2(24.28f, back1.transform.position.y);
        }
        if (back2.transform.position.x <= -24.28f)
        {
            back2.transform.position = new Vector2(24.28f, back2.transform.position.y);
        }
        back1.transform.position = new Vector2(
            back1.transform.position.x - 0.005f,
            back1.transform.position.y
        );
        back2.transform.position = new Vector2(
            back2.transform.position.x - 0.005f,
            back2.transform.position.y
        );

        //Floor
        if (ground1.transform.position.x <= -21.93f)
        {
            ground1.transform.position = new Vector2(21.93f, ground1.transform.position.y);
        }
        if (ground2.transform.position.x <= -21.93f)
        {
            ground2.transform.position = new Vector2(21.93f, ground2.transform.position.y);
        }
        ground1.transform.position = new Vector2(
            ground1.transform.position.x - 0.01f,
            ground1.transform.position.y
        );
        ground2.transform.position = new Vector2(
            ground2.transform.position.x - 0.01f,
            ground2.transform.position.y
        );
    }
}
