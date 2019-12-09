using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMouseDetection : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if(Input.GetMouseButtonDown(0))
        {
            MovementController.GetInstance().CheckMove(transform.localPosition);
        }
    }
}
