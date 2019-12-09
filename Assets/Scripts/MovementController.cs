using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    List<UnitController> list;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        list = UnitList.GetInstance().GetList();
        list[0].Move(new Vector3(2.5F, 50, 0.5F));
    }

    void CheckMove()
    {

    }
}
