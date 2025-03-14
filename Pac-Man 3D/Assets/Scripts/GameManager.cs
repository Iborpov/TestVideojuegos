using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    List<TextMeshProUGUI> UI;

    [SerializeField]
    List<GameObject> enemies;

    [SerializeField]
    GameObject chomp;
    GameObject player;
    GameObject chars;
    GameObject dots;
    GameObject powepellets;
    GameObject cherry;

    float timeStamp;

    void Awake()
    {
        player = GameObject.Find("Chomp");
        chars = GameObject.Find("Characters");
        dots = GameObject.Find("Dots");
        powepellets = GameObject.Find("PowerPellets");
        cherry = GameObject.Find("Cherry");
        UI[2].transform.parent.gameObject.SetActive(false);
        UI[3].transform.parent.gameObject.SetActive(false);
        cherry.SetActive(false);
        timeStamp = Time.time + 10f;
    }

    void Update()
    {
        //Update de la UI
        UI[0].text = "Lives " + Lives.Instance.GetLives();
        UI[1].text = "Score " + Score.Instance.GetScore();

        //Spawn de la cereza
        if (timeStamp <= Time.time && cherry != null)
        {
            cherry.SetActive(true);
        }

        if (player == null)
        {
            //El jugador uan tiene vidas
            if (Lives.Instance.GetLives() != 0)
            {
                player = Instantiate(chomp);
                player.transform.parent = chars.transform;
                GameObject[] ghosts = GameObject.FindGameObjectsWithTag("Enemy");
                foreach (var ghost in ghosts)
                {
                    ghost.GetComponent<GhostBT>().ResetPosition();
                }
            }
            else
            { //El jugador se queda sin vidas
                Destroy(chars);
                UI[2].transform.parent.gameObject.SetActive(true);
            }
        }

        if (dots != null && powepellets != null)
        { //Si no quedan puntos el jugador a ganado
            if (dots.transform.childCount == 0 && powepellets.transform.childCount == 0)
            {
                Destroy(chars);
                UI[3].transform.parent.gameObject.SetActive(true);
            }
        }
        else
        {
            Debug.LogError("No existe ningun punto");
        }

        if (Input.GetKey(KeyCode.R))
        {
            Restart();
        }
    }

    void Restart()
    {
        SceneManager.LoadScene("Level_01");
    }
}
