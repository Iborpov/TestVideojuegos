using System.Collections.Generic;
using UnityEngine;

public class Pots : MonoBehaviour
{
    [SerializeField]
    List<Sprite> aparences;

    [SerializeField]
    List<Sprite> brokenAparences;

    [SerializeField]
    AudioClip potBreack;

    SpriteRenderer sr;
    BoxCollider2D bc;
    int type;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        bc = GetComponent<BoxCollider2D>();
        type = Random.Range(0, 3);
    }

    private void Start()
    {
        sr.sprite = aparences[type];
    }

    void Update() { }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Sword"))
        {
            bc.isTrigger = true;
            sr.sprite = brokenAparences[type];
            SoundManager.Instance.PlayClip(potBreack);
        }
    }
}
