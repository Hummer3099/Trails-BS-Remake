using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnButtons : MonoBehaviour
{
    public GameObject myButton;
    public GameObject turnPanel;
    public GameObject button;
    public static int i=2;

    // Start is called before the first frame update
    public void DestroyAndCreate()
    {
        
        GameObject newButton = Instantiate(button) as GameObject;
        newButton.name = "Button" + i;
        newButton.GetComponentInChildren<Text>().text = "Button" + i;
        i++;
        newButton.transform.SetParent(turnPanel.transform, false);
        Destroy(myButton.gameObject);
    }
}
