using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tile : MonoBehaviour
{
    public int PathIndex;
    public abstract bool IsPlayable { get; }
    public virtual void SetUnit(BaseUnit unit) {}
    public BaseUnit OccupiedUnit;

}
