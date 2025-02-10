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
        Debug.Log("Lanzando");
        var startPoint = player.holdedItem.transform.position; //El punto de inicio es la posicion del objeto que sujeta el lanzable
        Vector2 endPoint; //El punto final depende de la dirección en la que el jugador mira
        switch (player.playerDir)
        {
            case Player.PlayerDir.Down:
                endPoint = startPoint + Vector3.down;
                break;
            case Player.PlayerDir.Left:
                endPoint = Vector2.left;
                break;
            case Player.PlayerDir.Right:
                endPoint = Vector2.right;
                break;
            case Player.PlayerDir.Up:
            default:
                endPoint = Vector2.up;
                break;
        }

        for (int i = 0; i < 20; i++)
        {
            player.hiRb.MovePosition(Vector3.Lerp(startPoint, endPoint, i * .05f));
            yield return new WaitForFixedUpdate();
        }
    }
}
