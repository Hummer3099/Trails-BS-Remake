using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    public Button moveButton;
    public bool isActive=true;
    private static UIController instance;

    private void Awake()
    {
        instance = this;
    }
    public static UIController getInstance() {
        return instance;
    }

   
  
}
