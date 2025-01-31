using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    Sprite doorOpen;

    [SerializeField]
    Sprite doorClosed;

    [SerializeField]
    GameObject enemies;
    BoxCollider2D bc;
    SpriteRenderer sr;

    // Start is called before the first frame update
    void Awake()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        bc = gameObject.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemies.transform.childCount == 0)
        {
            sr.sprite = doorOpen;
            bc.isTrigger = true;
        }
        else
        {
            sr.sprite = doorClosed;
            bc.isTrigger = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Codigo donde detecte si hay una sala adyacente o no, para crearla y mover la camara
    }
}
