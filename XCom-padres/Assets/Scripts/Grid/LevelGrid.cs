using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrid : MonoBehaviour
{
    [SerializeField]
    float width;

    [SerializeField]
    float height;
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

        gridSystem = new GridSystem(width, height);
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
        return new GridPosition((int)worldPosition.x, (int)worldPosition.z);
    }

    public Vector3 GetWorldPosition(GridPosition gridPosition)
    {
        return new Vector3(gridPosition.x, 0, gridPosition.z);
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
