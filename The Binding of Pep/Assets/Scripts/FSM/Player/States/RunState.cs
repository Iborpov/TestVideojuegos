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
