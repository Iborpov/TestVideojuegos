using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PickState : IState
{
    private Player player;

    public PickState(Player player)
    {
        this.player = player;
    }

    public void Enter()
    {
        //Animación
        player.animaActive = true;
        player.animator.SetBool("IsPicking", true);

        //Si no tiene objeto lo coge
        if (!player.pickUpActive)
        {
            PickItem();
        }

        //Ahora lleva un objeto
        player.pickUpActive = true;
    }

    public void Update()
    {
        if (!player.animaActive)
        {
            player.psm.TransitionTo(player.psm.iddleState);
        }
    }

    public void Exit()
    {
        //control de imput
        player.pickUpPending = false;

        //Animación
        player.animator.SetBool("IsPicking", false);
    }

    private void PickItem()
    {
        Vector2 pickDirection;
        switch (player.playerDir)
        {
            case Player.PlayerDir.Down:
                pickDirection = Vector2.down;
                break;
            case Player.PlayerDir.Left:
                pickDirection = Vector2.left;
                break;
            case Player.PlayerDir.Right:
                pickDirection = Vector2.right;
                break;
            case Player.PlayerDir.Up:
            default:
                pickDirection = Vector2.up;
                break;
        }

        Debug.Log(player.direction);
        var hit = Physics2D.BoxCast(
            player.bc.bounds.center,
            player.bc.bounds.size,
            0,
            pickDirection,
            0.5f,
            player.pickableLayer
        );

        if (hit)
        {
            player.holdedItem = hit.collider.gameObject; //Establece el objeto colisionado a el objeto a cojer
            player.holdedItem.transform.parent = player.itemHolder.transform; //Pone el objeto cogido como hijo del holder
            player.holdedItem.transform.position = player.itemHolder.transform.position; //Le pone la posicon 0 para ser la del padre
            player.hiRb = player.holdedItem.GetComponent<Rigidbody2D>(); //Cogemos su rigibody
            player.hiRb.simulated = false; //Le quitamos la simulación
            player.holdedItem.GetComponent<Pots>().isPicked = true; //Indicamos al tarro que ha sido cogido
        }
    }
}
