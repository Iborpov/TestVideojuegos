using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PoweupsManager : Singleton<PoweupsManager>
{
    float bigBallTimer = 0f;
    float smallBallTimer = 0f;

    float bigBallCoolDownTime = 1f;
    float smallBallCoolDownTime = 1f;

    [SerializeField]
    private List<Sprite> sprites;

    [SerializeField]
    private GameObject cooldowUIPref;

    GameObject bigBallUi;
    GameObject smallBallUi;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        smallBallTimer += Time.deltaTime;
        bigBallTimer += Time.deltaTime;
        if (bigBallUi != null) //Big ball
        {
            string segTxt = (bigBallCoolDownTime - bigBallTimer).ToString("F1");
            bigBallUi.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = segTxt + "s";

            if (bigBallTimer > bigBallCoolDownTime)
            {
                Destroy(bigBallUi);
                ResetBall();
            }
        }
        else if (smallBallUi != null) //Small ball
        {
            string segTxt = (smallBallCoolDownTime - smallBallTimer).ToString("F1");
            smallBallUi.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = segTxt + "s";
            if (smallBallTimer > smallBallCoolDownTime)
            {
                Destroy(smallBallUi);

                ResetBall();
            }
        }
    }

    public void CooldownBigBall(float coolDownTime)
    {
        bigBallTimer = 0;
        if (bigBallUi == null)
        {
            if (smallBallUi != null)
            {
                Destroy(smallBallUi);
            }

            bigBallCoolDownTime = coolDownTime;
            bigBallUi = Instantiate(cooldowUIPref);
            Image icon = bigBallUi.transform.GetChild(0).GetComponent<Image>();
            TextMeshProUGUI seconds = bigBallUi
                .transform.GetChild(1)
                .GetComponent<TextMeshProUGUI>();
            icon.sprite = sprites[1];
            seconds.text = bigBallTimer + "s";
            bigBallUi.transform.parent = transform;
        }
        else
        {
            Debug.LogWarning("La bola tiene un efecto activo y no puede ser alterada");
        }
    }

    public void CooldownSmallBall(float coolDownTime)
    {
        smallBallTimer = 0;
        if (smallBallUi == null)
        {
            if (bigBallUi != null)
            {
                Destroy(bigBallUi);
            }

            smallBallCoolDownTime = coolDownTime;
            smallBallUi = Instantiate(cooldowUIPref);
            Image icon = smallBallUi.transform.GetChild(0).GetComponent<Image>();
            TextMeshProUGUI seconds = smallBallUi
                .transform.GetChild(1)
                .GetComponent<TextMeshProUGUI>();
            icon.sprite = sprites[2];
            seconds.text = smallBallTimer + "s";
            smallBallUi.transform.parent = transform;
        }
        else
        {
            Debug.LogWarning("La bola tiene un efecto activo y no puede ser alterada");
        }
    }

    public void ResetBall()
    {
        GameObject ball = FindFirstObjectByType<Ball>().gameObject;
        ball.transform.localScale = new Vector2(1, 1);
        ball.GetComponent<SpriteRenderer>().sprite = sprites[0];
    }
}
