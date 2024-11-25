using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    //Stats
    [SerializeField]
    public int lives = 9;

    //Appereance
    [SerializeField]
    List<Sprite> sprites;

    //Prefabs
    [SerializeField]
    GameObject bricks;

    [SerializeField]
    GameObject powerupPref;

    //Sounds
    [SerializeField]
    AudioClip hit;

    [SerializeField]
    AudioClip destroy;

    //--------------------------------------------------------------------------------
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = sprites[lives - 1];
    }

    // Update is called once per frame
    void Update() { }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Hit();
    }

    void Hit()
    {
        lives -= 1;
        if (lives <= 0)
        {
            SoundManager.Instance.PlayClip(destroy);
            if (UnityEngine.Random.Range(0, 101) <= 50)
            {
                GameObject powerup = Instantiate(powerupPref);
                powerup.transform.position = new Vector2(
                    transform.position.x,
                    transform.position.y
                );
            }
            Destroy(bricks);
            Score.Instance.AddScore(100);
        }
        else
        {
            SoundManager.Instance.PlayClip(hit);
            GetComponent<SpriteRenderer>().sprite = sprites[lives - 1];
            Score.Instance.AddScore(50);
        }
    }


    public void SetLive(int newLives){
        lives = newLives;
    }
}
