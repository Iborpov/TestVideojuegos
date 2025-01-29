using UnityEngine;

public class IddleState : IState
{
    private Player player;

    // pass in any parameters you need in the constructors
    public IddleState(Player player)
    {
        this.player = player;
    }

    public void FixedUpdate()
    {
        if (player.direction != Vector3.zero)
        {
            player.psm.TransitionTo(player.psm.runState);
        }

        if (player.attackPending == true)
        {
            player.psm.TransitionTo(player.psm.attackState);
        }
    }
}
