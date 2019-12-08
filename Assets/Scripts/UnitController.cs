using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    Unit unit;
    public GameObject Unit;

    public int attack;
    public int defense;
    public int attackRange;
    public int moveRange;
    void Start()
    {
        InitializeUnit();
        RegisterTile();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitializeUnit()
    {
        unit = new Unit { attack = attack, defense = defense, attackRange = attackRange, moveRange = moveRange };
        if (Unit.gameObject.CompareTag("Party"))
        {
            unit.enemy = false;
            Debug.Log(Unit.name + " is a party member.");
        }
        else if (Unit.gameObject.CompareTag("Enemy"))
        {
            unit.enemy = true;
            Debug.Log(Unit.name + " is an enemy.");
        }
    }

    void RegisterTile()
    {
        Tile [,] tiles = MapGenerator.GetInstance().GetTiles();
        float offset = 4.5F;
        int x = (int)(Unit.transform.localPosition.x + offset);
        int y = (int)(Unit.transform.localPosition.z + offset);
        tiles[x, y].unit = unit;
        Debug.Log(Unit.name + " added at " + x + " " + y);
    }
}
