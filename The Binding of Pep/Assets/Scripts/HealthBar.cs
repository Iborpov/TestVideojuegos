using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    //Script de la vida del jugador
    [SerializeField]
    Health playerHealth;

    //Imagen de la vida total
    [SerializeField]
    Image totalHealthBar;

    //Imagen de la vida actual
    [SerializeField]
    Image currentHealthBar;

    void Start()
    {
        //Se inicializa la imagen de vida total
        totalHealthBar.fillAmount = playerHealth.currentHealth / 10f;
    }

    void Update()
    {
        //Se actualiza la imagen de vida actual
        currentHealthBar.fillAmount = playerHealth.currentHealth / 10f;
    }
}
