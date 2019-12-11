using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateController : MonoBehaviour
{

    public Text stateText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateStateText();
    }

    void UpdateStateText()
    {
        if (UIController.getInstance().isActive)
        {
            stateText.text = "Choose you range tile";
        }else if (UIController.getInstance().isActive == false)
        {
            stateText.text = "Choose action";
        }

    }
}
