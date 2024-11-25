using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    float speed = 5f;
    Rigidbody2D rbody;

    // Start is called before the first frame update
    void Start()
    {
        //Obtenemos el Rigibody de la bola
        rbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Si la bola tiene un objeto padre
        if (transform.parent != null)
        {
            //Modificamos su posicion para que sea la misma a la del padre
            transform.position = new Vector2(transform.parent.position.x, transform.position.y);
            //Si se pulsa el espacio se llama a la funcion Launch
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Launch();
            }
        }
    }

    // La funcion le quita el padre a la bola, y le aumenta su velocidad vertical para lanzarla hacia arriba
    public void Launch()
    {
        transform.parent = null;
        float x = UnityEngine.Random.Range(0, 2) == 0 ? -0.2f : 0.2f;
        GetComponent<Rigidbody2D>().velocity = new Vector2(x * speed, 1 * speed);
    }

    public void ChangeDirection(float direction)
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(direction, rbody.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            other.gameObject.GetComponent<AudioSource>().Play();
        }
    }
}
