using UnityEngine;

public class IddleState : IState
{
    private Player player;

    public IddleState(Player player)
    {
        this.player = player;
    }

    public void Update()
    {
        player.animator.SetBool("IsRunning", false);
        player.animator.SetBool("IsAtacking", false);
        player.animator.SetFloat("Y", player.direction.y);
        player.animator.SetFloat("X", player.direction.x);
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
    }
}
