using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField]
    bool powerUp = false;

    [SerializeField]
    int score = 0;

    [SerializeField]
    AudioClip audioClip;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SoundManager.Instance.PlayClip(audioClip);
            Destroy(this.gameObject);
        }
    }
}
