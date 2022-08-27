using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableTile : Tile
{
    [SerializeField] private GameObject _highlight;

    public override bool IsPlayable => true;

    public void OnMouseEnter()
    {
        _highlight.SetActive(true);
    }

    public void OnMouseExit()
    {
        _highlight.SetActive(false);
    }

    public void OnMouseDown()
    {
        Debug.Log("Tile Clicked: " + PathIndex);

        if (UnitManager.Instance.SelectedUnit != null)
        {
            Debug.Log("Set unit on tile");
            SetUnit(UnitManager.Instance.SelectedUnit);
            UnitManager.Instance.SetSelectedUnit(null);
        }
        else if (OccupiedUnit != null)
        {
            Debug.Log("Select unit on tile");
            UnitManager.Instance.SetSelectedUnit(OccupiedUnit);
        }
    }

    public override void SetUnit(BaseUnit unit)
    {
        if (unit.OccupiedTile != null) {
            unit.OccupiedTile.OccupiedUnit = null;
        }
        
        unit.transform.position = transform.position;
        unit.OccupiedTile = this;
        OccupiedUnit = unit;
    }
}
