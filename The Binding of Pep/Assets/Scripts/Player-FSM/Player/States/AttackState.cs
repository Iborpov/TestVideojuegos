using System.Collections;
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
        else
        {
            player.StartCoroutine(LaunchItem());
        }

        //Animación
        player.animaActive = true;
        player.animator.SetBool("IsAtacking", true);
    }

    public void Update()
    {
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

    private IEnumerator LaunchItem()
    {
        Vector3 startPoint = player.holdedItem.transform.position; //El punto de inicio
        Vector3 endPoint; //El punto final

        switch (player.playerDir)
        {
            case Player.PlayerDir.Down:
                endPoint = startPoint + Vector3.down * 10;
                break;
            case Player.PlayerDir.Left:
                endPoint = startPoint + Vector3.left * 10;
                break;
            case Player.PlayerDir.Right:
                endPoint = startPoint + Vector3.right * 10;
                break;
            case Player.PlayerDir.Up:
            default:
                endPoint = startPoint + Vector3.up * 10;
                break;
        }

        player.holdedItem.transform.parent = null;
        for (int i = 0; i < 20; i++)
        {
            //player.holdedItem.transform.position = Vector3.Lerp(startPoint, endPoint, i * .05f);
            player.hiRb.MovePosition(Vector3.Lerp(startPoint, endPoint, i * .05f));
            yield return new WaitForFixedUpdate();
        }
    }
}
