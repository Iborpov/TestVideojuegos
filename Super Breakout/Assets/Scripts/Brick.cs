using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField]
    int lives = 9;

    [SerializeField]
    List<Sprite> sprites;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }

    void OnCollisionEnter2D(Collision2D collision) { 
        Hit();
    }

    void Hit()
    {
        lives -= 1;
        GetComponent<SpriteRenderer>().sprite = sprites[lives];
    }
}
