using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    List<TextMeshProUGUI> UI;

    [SerializeField]
    GameObject chomp;
    GameObject player;
    GameObject chars;
    GameObject dots;

    void Awake()
    {
        player = FindAnyObjectByType<Chomp>().gameObject;
        chars = GameObject.Find("Characters");
        dots = GameObject.Find("Dots_PowerPellets");
        UI[2].transform.parent.gameObject.SetActive(false);
        UI[3].transform.parent.gameObject.SetActive(false);
    }

    void Update()
    {
        UI[0].text = "Lives " + Lives.Instance.GetLives();
        UI[1].text = "Score " + Score.Instance.GetScore();

        if (player == null)
        {
            if (Lives.Instance.GetLives() != 0)
            {
                player = Instantiate(chomp);
                player.transform.parent = chars.transform;
                player.transform.position = new Vector3(0, 0, -5);
            }
            else
            {
                UI[2].transform.parent.gameObject.SetActive(true);
            }
        }

        if (dots != null)
        {
            if (dots.transform.childCount == 0)
            {
                UI[3].transform.parent.gameObject.SetActive(true);
            }
        }
    }
}
