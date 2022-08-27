using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance;

    private List<ScriptableUnit> _units;

    public BaseUnit SelectedUnit;

    void Awake()
    {
        Instance = this;

        _units = Resources.LoadAll<ScriptableUnit>("Units").ToList();
    }

    public void SpawnUnits()
    {
        foreach (var unit in _units)
        {
            var unitObject = Instantiate(unit.UnitPrefab);
            var spawnTile = GridManager.Instance.GetSpawnTile();
            spawnTile.SetUnit(unitObject);
        }

        GameManager.Instance.ChangeState(GameState.RollDice);
    }

    public void SetSelectedUnit(BaseUnit unit)
    {
        SelectedUnit = unit;
    }
}
