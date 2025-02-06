using UnityEditor;
using UnityEngine;

public class AttackState : IState
{
    private Player player;

    public AttackState(Player player)
    {
        this.player = player;
    }

    public void Enter()
    {
        SoundManager.Instance.PlayClip(player.sword);
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

    public void Exit()
    {
        if (player.pickUpActive)
        {
            player.pickUpActive = false;
        }
    }
}
