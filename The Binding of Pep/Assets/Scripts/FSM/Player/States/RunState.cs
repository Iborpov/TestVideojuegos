using UnityEngine;

public class RunState : IState
{
    private Player player;

    public RunState(Player player)
    {
        this.player = player;
    }

    public void Update()
    {
        player.animator.SetBool("IsRunning", true);
    }

    public void FixedUpdate()
    {
        Move();
        if (player.direction == Vector3.zero)
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
        player.rb.velocity = player.direction * player.speed;
    }
}
