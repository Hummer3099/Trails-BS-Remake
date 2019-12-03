using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile
{
    public GameObject floor { get; set; }
    public Unit unit { get; set; }
    public bool isOccupied()
    {
        return !(unit == null);
    }
    public bool isEnemy()
    {
        return unit.isEnemy();
    }
}