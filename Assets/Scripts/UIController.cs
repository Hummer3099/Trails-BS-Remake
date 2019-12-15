using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Button moveButton;
    public Button attackButton;
    public Button defendButton;
    public bool isActive=true;
    public bool isMoving = false;
    public bool isAttacking = false;
    public bool isDefending = false;
    private static UIController instance;

    private void Awake()
    {
        instance = this;
    }
    public static UIController getInstance() {
        return instance;
    }

    public void doMove()
    {

        isMoving = !isMoving;
        isAttacking = false;
        isDefending = false;
    }
    public void doAttack()
    {

        isMoving = false;
        isAttacking = !isAttacking;
        isDefending = false;
    }
    public void doDefend()
    {

        isMoving = false;
        isAttacking = false;
        isDefending = !isDefending;
        MovementController.GetInstance().Defend();
    }
}
