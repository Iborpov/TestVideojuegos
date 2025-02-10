using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    float startingHealth = 3f;

    public float currentHealth;

    public bool invulnerabilityOn;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startingHealth;
    }

    // Update is called once per frame
    void Update() { }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0f)
        {
            Debug.Log("Dead");
        }
    }
}
