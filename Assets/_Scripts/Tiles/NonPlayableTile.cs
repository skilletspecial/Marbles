using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPlayableTile : Tile
{
    public override bool IsPlayable => false;
}
