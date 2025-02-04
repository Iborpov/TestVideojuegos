using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField]
    public AudioClip sword;
    public PlayerFSM psm { get; private set; }
    public Vector3 direction;
    public Rigidbody2D rb;
    public Animator animator;

    public float speed = 5;
    public bool attackPending = false;
    bool pickUpPending = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void OnEnable()
    {
        psm = new PlayerFSM(this);
        psm.Initialize(psm.iddleState);
    }

    private void Update()
    {
        psm.Update();
    }

    private void FixedUpdate()
    {
        psm.FixedUpdate();
    }

    //Input Sistem ------------------------------------------------------------
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
