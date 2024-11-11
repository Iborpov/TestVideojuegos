using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField]
    int lives = 8;

    [SerializeField]
    List<Sprite> sprites;
    [SerializeField]
    GameObject bricks;

    // Start is called before the first frame update
    void Start() {
        GetComponent<SpriteRenderer>().sprite = sprites[lives-1];
     }

    // Update is called once per frame
    void Update() { }

    void OnCollisionEnter2D(Collision2D collision) { 
        Hit();
    }

    void Hit()
    {
        lives -= 1;
        if (lives<=0)
        {
            Destroy(bricks);
        } else
        {
            GetComponent<SpriteRenderer>().sprite = sprites[lives-1];    
        }
        
    }
}
