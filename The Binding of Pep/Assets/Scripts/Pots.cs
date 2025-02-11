using System.Collections.Generic;
using UnityEngine;

public class Pots : MonoBehaviour
{
    [SerializeField]
    List<Sprite> aparences;

    [SerializeField]
    List<Sprite> brokenAparences;

    [SerializeField]
    List<Sprite> noShadowAparences;

    [SerializeField]
    AudioClip potBreack;

    SpriteRenderer sr;
    BoxCollider2D bc;
    int type;

    public bool isPicked = false;

    void Awake()
    {
        //Inicializa los componentes
        sr = GetComponent<SpriteRenderer>();
        bc = GetComponent<BoxCollider2D>();

        //Seleción auna aparencia aleatoria
        type = Random.Range(0, 3);
    }

    private void Start()
    {
        //Aplica una aparencia de la lista
        sr.sprite = aparences[type];
    }

    void Update()
    {
        if (isPicked)
        {
            sr.sprite = noShadowAparences[type];
            sr.sortingOrder = 3;
        }
        else
        {
            sr.sprite = aparences[type];
            sr.sortingOrder = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Sword"))
        {
            if (bc.isTrigger == false)
            {
                SoundManager.Instance.PlayClip(potBreack);
            }
            bc.isTrigger = true;
            sr.sprite = brokenAparences[type];
        }
    }
}
