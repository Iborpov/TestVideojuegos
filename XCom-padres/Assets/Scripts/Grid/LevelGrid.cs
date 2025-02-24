using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrid : MonoBehaviour
{
    public static LevelGrid Instance { get; private set; }

    private GridSystem gridSystem;

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one LevelGrid Instance");
            return;
        }
        Instance = this;

        gridSystem = new GridSystem();
    }

    void Start() { }

    void Update() { }

    public Vector3 GetWorldPosition(GridPosition gridPosition)
    {
        return new Vector3(gridPosition.x, 0, gridPosition.z);
    }

    public GridPosition GetGridPosition(Vector3 worldPosition)
    {
        return new GridPosition((int)worldPosition.x, (int)worldPosition.z);
    }
}
