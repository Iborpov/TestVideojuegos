using UnityEngine;

public class RunState : IState
{
    private Player player;
    private Vector3 lastDirection;

    public RunState(Player player)
    {
        this.player = player;
    }

    public void Enter()
    {
        //Animacion
        player.animator.SetBool("IsRunning", true);

        lastDirection = player.direction;
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
        Move();

        //Si el jugador le da a moverse se cambia de estado a RunState
        if (player.direction == Vector3.zero)
        {
            player.psm.TransitionTo(player.psm.iddleState);
        }

        //Si el jugador pulsa el boton de atacar cambia al estade de AttackState
        if (player.attackPending == true)
        {
            player.psm.TransitionTo(player.psm.attackState);
        }

        //Si el jugador pulsa el boton de recoger
        if (player.pickUpPending == true)
        {
            //Si lleva un objeto lo lanza, si no lo recoge
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

    public void Exit()
    {
        //Si sale del estado de correr esa quieto
        player.rb.velocity = new Vector3(0, 0);
    }

    void Move() //Mueve al personaje en la dirección indicada
    {
        player.rb.velocity = player.direction * player.speed;
    }
}
