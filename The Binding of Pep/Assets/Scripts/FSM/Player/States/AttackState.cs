using UnityEngine;

public class AttackState : IState
{
    private Player player;

    // pass in any parameters you need in the constructors
    public AttackState(Player player)
    {
        this.player = player;
    }

    public void Update()
    {
        player.animator.SetBool("IsAtacking", true);
    }

    public void FixedUpdate()
    {
        player.attackPending = false;
        player.psm.TransitionTo(player.psm.iddleState);
    }
}
