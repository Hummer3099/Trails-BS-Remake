using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnPanel : MonoBehaviour
{

    public GameObject turnPanel;
    public GameObject turnController;
    public GameObject rawImage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DestroyAndCreate();
    }
    public void DestroyAndCreate()
    {
        GameObject newRawImage = Instantiate(rawImage) as GameObject;
        newRawImage.transform.SetParent(turnPanel.transform, false);
        Destroy(rawImage.gameObject);
    }
}
