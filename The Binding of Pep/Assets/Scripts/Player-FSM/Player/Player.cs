using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public enum PlayerDir
    {
        Up,
        Down,
        Right,
        Left,
    }

    [SerializeField]
    public AudioClip sword;

    [SerializeField]
    public LayerMask pickableLayer;

    public PlayerDir playerDir = PlayerDir.Down;

    //Maquina de estados
    public PlayerFSM psm { get; private set; }

    //Components
    public Vector3 direction;
    public Rigidbody2D rb;
    public Animator animator;

    //Holded Item components
    public GameObject holdedItem;
    public Rigidbody2D hiRb;

    public BoxCollider2D bc;

    [SerializeField]
    public GameObject itemHolder;

    public float speed = 5;

    //Estados
    public bool attackPending = false;
    public bool pickUpPending = false;

    public bool pickUpActive = false;
    public bool animaActive = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        bc = GetComponent<BoxCollider2D>();
        itemHolder = transform.GetChild(1).gameObject;
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
            if (direction.x > 0)
                playerDir = PlayerDir.Right;
            if (direction.x < 0)
                playerDir = PlayerDir.Left;
            if (direction.y > 0)
                playerDir = PlayerDir.Up;
            if (direction.y < 0)
                playerDir = PlayerDir.Down;
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

    public void DisableAnimActive()
    {
        animaActive = false;
    }
}
