using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMouseDetection : MonoBehaviour
{
    private void OnMouseDown()
    {
        if(Input.GetMouseButtonDown(0) && UIController.getInstance().isActive && !MovementController.GetInstance().isAnyPlayerMoving)
        {
            MovementController.GetInstance().CheckMove(transform.localPosition);
        }
    }
}
