using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hearts : MonoBehaviour
{
    //[SerializeField]
    private List<Image> hearts;
    private int lives;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        lives = Lives.Instance.GetLives();
        LoadHearts();
    }

    void LoadHearts()
    {
        hearts = new List<Image>();
        for (int i = 0; i < 3; i++)
        {
            hearts.Add(transform.GetChild(i).GetComponent<Image>());
            Image image = hearts[i];
            if (i < lives)
            {
                // Si la vida actual cubre este corazón, mostrarlo lleno
                image.sprite = Lives.Instance.GetSprite(0); // Sprite lleno
            }
            else
            {
                Debug.Log("sin vida");
                // Si no, mostrar el corazón vacío
                image.sprite = Lives.Instance.GetSprite(2); // Sprite vacío
            }
        }
    }
}
