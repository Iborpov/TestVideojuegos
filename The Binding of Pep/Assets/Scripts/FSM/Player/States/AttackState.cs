using System.Net;
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
        if (!player.pickUpActive)
        {
            //Sonido espada
            SoundManager.Instance.PlayClip(player.sword);
        }

        //Animación
        player.animaActive = true;
        player.animator.SetBool("IsAtacking", true);
    }

    public void Update()
    {
        if (player.pickUpActive)
        {
            //LaunchItem();
        }

        if (!player.animaActive)
        {
            player.psm.TransitionTo(player.psm.iddleState);
        }
    }

    public void Exit()
    {
        player.attackPending = false;
        player.pickUpPending = false;

        //Si lleva un objeto al atacar lo ha lanzado y pasa a no tener objeto
        if (player.pickUpActive)
        {
            player.pickUpActive = false;
        }

        //Animación
        player.animator.SetBool("IsAtacking", true);
    }

    // private void LaunchItem()
    // {
    //     var startPoint = player.holdedItem.transform.position;
    //     var endPoint = player.transform.position;

    //     for (int i = 0; i < 20; i++)
    //     {
    //         player.hiRb.MovePosition(Vector3.Lerp(startPoint, endPoint, i * .05f));
    //         yield return new WaitForFixedUpdate();
    //     }
    // }
}
