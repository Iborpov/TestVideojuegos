using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    bool online = false; //¿Es una partida online?

    void Awake() { }

    void Update() { }

    public bool IsOnline
    {
        get { return online; }
    }
}
