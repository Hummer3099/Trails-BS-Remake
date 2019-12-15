using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnPanel : MonoBehaviour
{

    public GameObject turnPanel;
    public GameObject turnController;
    public GameObject rawImage;
    private static TurnPanel instance;
    List<UnitController> list;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

    }

    // Update is called once per frame
    void Update()
    {
       
    }


    public static TurnPanel GetInstance()
    {
        return instance;
    }

    public void CreateRawImage()
    {
        if (list == null)
        {
            list = TurnController.GetInstance().GetTurnsList();
            foreach (UnitController unitCon in list)
            {
                GameObject newRawImage = Instantiate(unitCon.getRawImage()) as GameObject;
                newRawImage.transform.SetParent(turnPanel.transform, false);
            }
        }
        else {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
            foreach (UnitController unitCon in list)
            {
                GameObject newRawImage = Instantiate(unitCon.getRawImage()) as GameObject;
                newRawImage.transform.SetParent(turnPanel.transform, false);
            }
        }
    }

    private GameObject Instantiate(Func<RawImage> getRawImage)
    {
        throw new NotImplementedException();
    }
}
