using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Hearts : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI extraHealth;

    [SerializeField]
    private List<Image> hearts;

    [SerializeField]
    private List<Sprite> sprites;
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
        extraHealth = transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        if (lives > 3)
        {
            extraHealth.text = "+" + (lives - 3);
        } else {
            extraHealth.text = "";
        }

        hearts = new List<Image>();
        for (int i = 0; i < 3; i++)
        {
            hearts.Add(transform.GetChild(i).GetComponent<Image>());
            Image image = hearts[i];
            if (i < lives)
            {
                // Si la vida actual cubre este corazón, mostrarlo lleno
                image.sprite = sprites[0]; // Sprite lleno
            }
            else
            {
                // Si no, mostrar el corazón vacío
                image.sprite = sprites[2]; // Sprite vacío
            }
        }
    }
}
