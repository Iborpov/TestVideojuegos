using UnityEngine;

public class RunState : IState
{
    private Player player;
    Vector3 direction;

    // pass in any parameters you need in the constructors
    public RunState(Player player)
    {
        this.player = player;
        this.direction = player.direction;
    }

    public void FixedUpdate()
    {
        if (direction != Vector3.zero)
        {
            Move();
        }
        else
        {
            player.psm.TransitionTo(player.psm.iddleState);
        }

        if (player.attackPending == true)
        {
            player.psm.TransitionTo(player.psm.attackState);
        }
    }

    void Move()
    {
        player.rb.velocity = direction * player.speed;
    }
}
