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

    public Material red;
    public Material grass;

    private void Awake()
    {
        instance = this;
    }
    public static UIController getInstance() {
        return instance;
    }

    public void doMove()
    {
        UnmarkTiles();
        isMoving = !isMoving;
        isAttacking = false;
        isDefending = false;
        MarkMovementTiles();
    }
    public void doAttack()
    {
        UnmarkTiles();
        isMoving = false;
        isAttacking = !isAttacking;
        isDefending = false;
        MarkAttackTiles();
    }
    public void doDefend()
    {
        UnmarkTiles();
        isMoving = false;
        isAttacking = false;
        isDefending = !isDefending;
        MovementController.GetInstance().Defend();
    }

    void MarkMovementTiles()
    {
        Tile[,] tiles = MapGenerator.GetInstance().GetTiles();

        for(int i = 0; i < tiles.GetLength(0); i++)
        {
            for(int j = 0; j < tiles.GetLength(1); j++)
            {
                if(MovementController.GetInstance().isWithinMovingRange(tiles[i,j].floor.transform.localPosition))
                {
                    tiles[i, j].floor.GetComponent<Renderer>().material = red;
                }
            }
        }
    }
    void MarkAttackTiles()
    {
        Tile[,] tiles = MapGenerator.GetInstance().GetTiles();

        for (int i = 0; i < tiles.GetLength(0); i++)
        {
            for (int j = 0; j < tiles.GetLength(1); j++)
            {
                if (MovementController.GetInstance().isWithinAttackRange(tiles[i, j].floor.transform.localPosition))
                {
                    tiles[i, j].floor.GetComponent<Renderer>().material = red;
                }
            }
        }
    }

    public void UnmarkTiles()
    {
        Tile[,] tiles = MapGenerator.GetInstance().GetTiles();

        for (int i = 0; i < tiles.GetLength(0); i++)
        {
            for (int j = 0; j < tiles.GetLength(1); j++)
            {
                    tiles[i, j].floor.GetComponent<Renderer>().material = grass;
            }
        }
    }
}
