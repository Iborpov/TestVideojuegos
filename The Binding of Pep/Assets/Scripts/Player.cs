using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Vector3 direction;
    Rigidbody2D rb;

    public float speed = 5;
    bool attackPending = false;
    bool pickUpPending = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() { }

    void Move()
    {
        rb.velocity = direction * speed;
    }

    //Controlador de inputs de Movimiento
    public void OnMove(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();
        if (direction != Vector3.zero)
        {
            direction.Normalize();
        }
    }

    //Controlador de inputs de Ataque
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            attackPending = true;
        }
    }

    //Controlador de inputs de Recoger/Lanzar
    public void OnPickUp(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            pickUpPending = true;
        }
    }
}
