using Unity.VisualScripting;
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

    CameraManager CM;

    bool playerInside;

    // Start is called before the first frame update
    void Awake()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        bc = gameObject.GetComponent<BoxCollider2D>();
        CM = FindAnyObjectByType<CameraManager>();
        playerInside = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInside)
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
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CM.MoveToNewRoom(transform.parent.transform);
            playerInside = true;
        }

        //Codigo donde detecte si hay una sala adyacente o no, para crearla y mover la camara
    }
}
