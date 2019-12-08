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
        unit = new Unit { attack = attack, defense = defense, attackRange = attackRange, moveRange = moveRange };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
