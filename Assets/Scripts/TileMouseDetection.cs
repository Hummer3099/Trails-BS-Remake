using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMouseDetection : MonoBehaviour
{
    private void OnMouseDown()
    {
        if(Input.GetMouseButtonDown(0) && UIController.getInstance().isMoving && !MovementController.GetInstance().isAnyPlayerMoving)
        {
            MovementController.GetInstance().CheckMove(transform.localPosition);
            UIController.getInstance().UnmarkTiles();
        }else if (Input.GetMouseButtonDown(0) && UIController.getInstance().isAttacking && !MovementController.GetInstance().isAnyPlayerMoving)
        {
            MovementController.GetInstance().CheckAttack(transform.localPosition);
            UIController.getInstance().UnmarkTiles();
        }
    }
}
