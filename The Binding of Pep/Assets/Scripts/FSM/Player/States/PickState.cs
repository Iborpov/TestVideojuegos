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
        player.animator.SetBool("IsPicking", true);
        player.pickUpActive = true;
    }

    public void Update()
    {
        player.psm.TransitionTo(player.psm.iddleState);
    }

    public void FixedUpdate()
    {
        player.pickUpPending = false;
        player.psm.TransitionTo(player.psm.iddleState);
    }

    public void Exit()
    {
        player.animator.SetBool("IsPicking", false);
    }
}
