using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitList : MonoBehaviour
{
    public List<UnitController> unitControllers;

    private static UnitList instance;

    private void Awake()
    {
        instance = this;
        unitControllers = new List<UnitController>();
    }
    public static UnitList GetInstance()
    {
        return instance;
    }
    public List<UnitController> GetList()
    {
        return unitControllers;
    }
}
