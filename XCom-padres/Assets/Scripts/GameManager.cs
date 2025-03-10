using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    Transform allies;

    [SerializeField]
    Transform enemies;

    [SerializeField]
    GameObject textPrefab;

    [SerializeField]
    GameObject canvas;


    GameObject turns;
    TextMeshProUGUI turnText;

    List<Transform> unitsList = new List<Transform>();
    int totalAP = 0;

    public bool turn; // True = Allies | False = Enemies
  
    void Awake() { 
        turns = canvas.transform.GetChild(0).gameObject;
        turnText = turns.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        turn = true;
        turnText.text = "Allies Turn";
        turnText.color = Color.green;
        
        UnitsList(allies);
        GetTurnUnits();
    }

    void Update() { 
        GetTurnUnits();
        for (int i = 0; i < unitsList.Count(); i++)
       {
         totalAP += unitsList[i].GetComponent<Unit>().GetActionPoints();
       }
        if (totalAP <= 0)
        {
            NextTurn();
        }
    }


    public void NextTurn(){
        if (turn)
        {
            turn = false;
            turnText.text = "Enemies Turn";
            turnText.color = Color.red;
            UnitsList(enemies);
            
            
        } else
        {
            turn = true;
            turnText.text = "Allies Turn";
            turnText.color = Color.green;
            UnitsList(allies);
        }
        
        foreach (Transform unit in unitsList)
        {
            unit.GetComponent<Unit>().ResetActionPoints();
        }
    }

    public void GetTurnUnits(){
       Transform panel = turns.transform.GetChild(1);
       foreach (Transform textPanel in panel)
        {
            Destroy(textPanel.gameObject);
        }
       for (int i = 0; i < unitsList.Count(); i++)
       {
        var unitComponent = unitsList[i].GetComponent<Unit>();
        GameObject text = Instantiate(textPrefab, panel);
        text.GetComponent<TextMeshProUGUI>().text = unitsList[i].name + " | "+unitComponent.GetActionPoints()+"AP";
       }
    }

    public void UnitsList(Transform units){
        if (unitsList != null){unitsList.Clear();}
        
        foreach (Transform unit in units)
        {
            unitsList.Add(unit);
        }

    }

    
}
