using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    List<UnitController> list;
    public UnitController currentPlayer;
    Tile[,] tiles;
    private static MovementController instance;

    public bool isAnyPlayerMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        tiles = MapGenerator.GetInstance().GetTiles();
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        list = UnitList.GetInstance().GetList();
        CheckDefend();
    }

    public static MovementController GetInstance()
    {
        return instance;
    }

    public UnitController GetCurrentPlayer()
    {
        return currentPlayer;
    }

    public void SetCurrentPlayer(UnitController newCurrentPlayer)
    {
        currentPlayer = newCurrentPlayer;
    }

    public void CheckMove(Vector3 clickedTile)
    {
        if(!currentPlayer.unit.enemy)
        {
            if (isWithinMovingRange(clickedTile))
            {
                currentPlayer.Move(clickedTile);
            }
        }
        else
        {
            Vector3 newVector = GetClosestPossibleTile(clickedTile);
            currentPlayer.Move(newVector);
        }
    }

    public bool isWithinMovingRange(Vector3 clickedTile)
    {
        int difference = CalcDiff(clickedTile);
        if (difference <= currentPlayer.moveRange)
        {
            float offset = 4.6F;
            int clickedX = (int)(clickedTile.x + offset);
            int clickedY = (int)(clickedTile.z + offset);

            if (clickedX >= 0 && clickedY >= 0 && clickedX < tiles.GetLength(0) && clickedY < tiles.GetLength(1))
            {
                if(!tiles[clickedX, clickedY].isOccupied())
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        return false;
    }
    public bool isWithinAttackRange(Vector3 clickedTile)
    {
        int difference = CalcDiff(clickedTile);
        if (difference <= currentPlayer.moveRange + currentPlayer.attackRange)
        {
            float offset = 4.6F;
            int clickedX = (int)(clickedTile.x + offset);
            int clickedY = (int)(clickedTile.z + offset);

            if (clickedX >= 0 && clickedY >= 0 && clickedX < tiles.GetLength(0) && clickedY < tiles.GetLength(1))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    public Vector3 GetClosestPossibleTile(Vector3 clickedTile)
    {
        Vector3 newVector = currentPlayer.Unit.transform.localPosition;

        float offset = 4.6F;
        int clickedX = (int)(clickedTile.x + offset);
        int clickedY = (int)(clickedTile.z + offset);

        int playerX = (int)(newVector.x + offset);
        int playerY = (int)(newVector.z + offset);

        bool increaseX = false;
        bool increaseY = false;

        bool sameX = false;
        bool sameY = false;

        if(playerX < clickedX)
        {
            increaseX = true;
        }
        else if(playerX == clickedX)
        {
            sameX = true;
        }

        if(playerY < clickedY)
        {
            increaseY = true;
        }
        else if(playerY == clickedY)
        {
            sameY = true;
        }

        if(sameX)
        {
            if(increaseY)
            {
                for (int i = 0; i < currentPlayer.moveRange; i++)
                {
                    newVector.z = newVector.z + 1F;
                }
            }
            else
            {
                for (int i = 0; i < currentPlayer.moveRange; i++)
                {
                    newVector.z = newVector.z - 1F;
                }
            }
        }

        else if (sameY)
        {
            if (increaseX)
            {
                for (int i = 0; i < currentPlayer.moveRange; i++)
                {
                    newVector.x = newVector.x + 1F;
                }
            }
            else
            {
                for (int i = 0; i < currentPlayer.moveRange; i++)
                {
                    newVector.x = newVector.x - 1F;
                }
            }
        }

        else
        {
            bool isYTurn = false;
            for(int i = 0; i < currentPlayer.moveRange; i++)
            {
                playerX = (int)(newVector.x + offset);
                playerY = (int)(newVector.z + offset);

                if (isYTurn && playerY != clickedY)
                {
                    if(increaseY)
                    {
                        newVector.z = newVector.z + 1F;
                    }
                    else
                    {
                        newVector.z = newVector.z - 1F;
                    }
                }
                else if(!isYTurn && playerX != clickedX)
                {
                    if(increaseX)
                    {
                        newVector.x = newVector.x + 1F;
                    }
                    else
                    {
                        newVector.x = newVector.x - 1F;
                    }
                }
                isYTurn = !isYTurn;
            }
        }
        return newVector;
    }

    public void CheckAttack(Vector3 clickedTile)
    {
        int difference = CalcDiff(clickedTile);
        Result currResult = new Result { distance = 9999 };
        if(difference <= currentPlayer.attackRange)
        {
            Attack(clickedTile);
            UIController.getInstance().isAttacking = false;
            TurnController.GetInstance().turns.RemoveAt(0);
        }

        else if (difference <= currentPlayer.moveRange + currentPlayer.attackRange && difference != currentPlayer.moveRange && difference != 1)
        {

            for(int i = 1; i < currentPlayer.attackRange + 1; i++)
            {
                Vector3 upCheck = new Vector3(clickedTile.x, clickedTile.y, clickedTile.z + i);
                Vector3 downCheck = new Vector3(clickedTile.x, clickedTile.y, clickedTile.z - i);
                Vector3 rightCheck = new Vector3(clickedTile.x + i, clickedTile.y, clickedTile.z);
                Vector3 leftCheck = new Vector3(clickedTile.x - i, clickedTile.y, clickedTile.z);

                if (currResult.distance > CheckDistance(upCheck).distance)
                {
                    currResult = CheckDistance(upCheck);
                    Debug.Log(currResult.distance);
                }
                if (currResult.distance > CheckDistance(downCheck).distance)
                {
                    currResult = CheckDistance(downCheck);
                    Debug.Log(currResult.distance);
                }
                if (currResult.distance > CheckDistance(rightCheck).distance)
                {
                    currResult = CheckDistance(rightCheck);
                    Debug.Log(currResult.distance);
                }
                if (currResult.distance > CheckDistance(leftCheck).distance)
                {
                    currResult = CheckDistance(leftCheck);
                    Debug.Log(currResult.distance);
                }
            }
            CheckMove(currResult.destination);
            Attack(clickedTile);
            Debug.Log("Attacked!");
            UIController.getInstance().isAttacking = false;
        }
    }

    void Attack(Vector3 clickedTile)
    {
        float offset = 4.6F;
        int clickedX = (int)(clickedTile.x + offset);
        int clickedY = (int)(clickedTile.z + offset);

        Unit enemy = tiles[clickedX, clickedY].unit;

        int enemyDefense = enemy.defense / 2;
        int resultHP = currentPlayer.attack - enemyDefense;
        if(enemy.hp - resultHP < 0)
        {
            enemy.hp = 0;
            tiles[clickedX, clickedY] = null;
            enemy.isDead = true;
        }
        else
        {
            enemy.hp -= resultHP;
        }
    }
    public void Defend()
    {
        currentPlayer.isDefendMode = true;
        currentPlayer.defense += 5;
        UIController.getInstance().isDefending = false;
        TurnController.GetInstance().turns.RemoveAt(0);
    }
    public void CheckDefend()
    {
        if (currentPlayer.isDefendMode) {
            currentPlayer.isDefendMode = false;
            currentPlayer.defense -= 5;
        }
    }

    int CalcDiff(Vector3 clickedTile)
    {
        float offset = 4.6F;
        int clickedX = (int)(clickedTile.x + offset);
        int clickedY = (int)(clickedTile.z + offset);

        Vector3 playerPosition = currentPlayer.Unit.transform.localPosition;

        int playerX = (int)(playerPosition.x + offset);
        int playerY = (int)(playerPosition.z + offset);

        int difference = Mathf.Abs(clickedX - playerX) + Mathf.Abs(clickedY - playerY);
        return difference;
    }

    Result CheckDistance(Vector3 clickedTile)
    {
        if(isWithinMovingRange(clickedTile))
        {

            float offset = 4.6F;
            int clickedX = (int)(clickedTile.x + offset);
            int clickedY = (int)(clickedTile.z + offset);

            float distance = Vector3.Distance(clickedTile, currentPlayer.Unit.transform.localPosition);

            return new Result {destination = clickedTile, distance = distance, x = clickedX, y = clickedY };
        }
        return new Result { distance = 99999999 };
    }

}