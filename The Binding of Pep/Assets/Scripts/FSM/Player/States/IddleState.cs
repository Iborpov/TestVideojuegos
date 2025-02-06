using UnityEngine;

public class IddleState : IState
{
    private Player player;

    private Vector3 lastDirection;

    public IddleState(Player player)
    {
        this.player = player;
    }

    public void Enter()
    {
        Debug.Log(player.pickUpActive);
        player.animator.SetBool("IsRunning", false);
        player.animator.SetBool("IsAtacking", false);
        lastDirection = player.direction;
        player.animator.SetLayerWeight(0, player.pickUpActive ? 1f : 0f);
    }

    public void Update()
    {
        if (lastDirection != player.direction)
        {
            player.animator.SetTrigger("NewDirection");
            player.animator.SetFloat("Y", player.direction.y);
            player.animator.SetFloat("X", player.direction.x);
            lastDirection = player.direction;
        }
    }

    public void FixedUpdate()
    {
        //Si el jugador le da a moverse se cambia de estado a RunState
        if (player.direction != Vector3.zero)
        {
            player.psm.TransitionTo(player.psm.runState);
        }

        //Si el jugador pulsa el boton de atacar cambia al estade de AttackState
        if (player.attackPending == true)
        {
            player.psm.TransitionTo(player.psm.attackState);
        }

        //Si el jugador pulsa el boton de recoger cambia al estade de PickState
        if (player.pickUpPending == true)
        {
            if (player.pickUpActive)
            {
                player.psm.TransitionTo(player.psm.attackState);
            }
            else
            {
                player.psm.TransitionTo(player.psm.pickState);
            }
        }
    }
}
