using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    [SerializeField]
    private int type;

    [SerializeField]
    private GameObject ballpref;

    [SerializeField]
    private List<Sprite> poweupSprites;

    [SerializeField]
    private List<Sprite> extraSprites;

    [SerializeField]
    private AudioClip recover;

    // Start is called before the first frame update
    void Start()
    {
        type = UnityEngine.Random.Range(0, 4); //Se añade un tipo aleatorio
        GetComponent<SpriteRenderer>().sprite = poweupSprites[type]; //Se le aplica la aparencia segun el tipo elegido
    }

    // Update is called once per frame
    void Update() { }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.CompareTag("Player"))
        {
            switch (type)
            {
                case 0:
                    AddLive();
                    break;
                case 1:
                    ExtraBall();
                    break;
                case 2:
                    BigBall();
                    break;
                case 3:
                    SmallBall();
                    break;
                default:
                    break;
            }
            Destroy(gameObject);
        }
    }

    //PoweUps -----------------------------------------------------------------------

    //Añade una vida más
    void AddLive()
    {
        SoundManager.Instance.PlayClip(recover);
        Lives.Instance.AddLive();
    }

    //Añade una bola más
    void ExtraBall()
    {
        var padle = GameObject.Find("Padle");
        GameObject newball = Instantiate(ballpref);
        newball.gameObject.tag = "ExtraBall";
        newball.GetComponent<SpriteRenderer>().sprite = extraSprites[3];
        newball.transform.parent = padle.transform;
    }

    //Hace la bola más grande
    void BigBall()
    {
        var ball = FindFirstObjectByType<Ball>().gameObject;
        ball.transform.localScale = new Vector2(2, 2);
        ball.GetComponent<SpriteRenderer>().sprite = extraSprites[0];
        PoweupsManager.Instance.CooldownBigBall(5f);
    }

    //Hace la bola más pequeña
    void SmallBall()
    {
        var ball = FindFirstObjectByType<Ball>().gameObject;
        ball.transform.localScale = new Vector2(0.5f, 0.5f);
        ball.GetComponent<SpriteRenderer>().sprite = extraSprites[1];
        PoweupsManager.Instance.CooldownSmallBall(5);
    }

    //-----------------------------------------------------------------------

    public int GetTipe()
    {
        return this.type;
    }

    public void SetType(int newType)
    {
        this.type = newType;
    }

    public Sprite GetExtraSprite(int i){
        return extraSprites[i];
    }
}
