using UnityEngine;

public class AttackState : IState
{
    private Player player;

    // pass in any parameters you need in the constructors
    public AttackState(Player player)
    {
        this.player = player;
    }

    public void Update() { }

    public void FixedUpdate()
    {
        player.psm.TransitionTo(player.psm.iddleState);
    }
}
