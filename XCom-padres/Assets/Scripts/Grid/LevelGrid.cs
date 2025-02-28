using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrid : MonoBehaviour
{
    [SerializeField]
    int width;

    [SerializeField]
    int height;

    [SerializeField]
    float cellSize;

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

        gridSystem = new GridSystem(width, height, cellSize);
    }

    void Start() { }

    void Update() { }

    public void AddUnitAtGridPosition(Unit unit) { }

    public void GetUnitListAtGridPosition(GridPosition gridPosition) { }

    public void RemoveUnitAtGridPosition(GridPosition gridPosition) { }

    public bool HasAnyUnitOnGridPosition(GridPosition gridPosition)
    {
        return false;
    }

    public bool IsValidGridPosition(GridPosition gridPosition)
    {
        return HasAnyUnitOnGridPosition(gridPosition);
    }

    public GridPosition GetGridPosition(Vector3 worldPosition)
    {
        return gridSystem.GetGridPosition(worldPosition);
    }

    public Vector3 GetWorldPosition(GridPosition gridPosition)
    {
        return gridSystem.GetWoldPosition(gridPosition);
    }

    public float GetWith()
    {
        return width;
    }

    public float GetHeight()
    {
        return height;
    }
}
