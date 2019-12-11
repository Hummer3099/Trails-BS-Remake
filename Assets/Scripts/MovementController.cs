using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    List<UnitController> list;
    UnitController currentPlayer;
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
        currentPlayer = list[1];
        //CheckAttack(new Vector3(1.5F, 50, -2.5F));
        //CheckMove(new Vector3(-1.5F, 50, 0.5F));
    }

    public static MovementController GetInstance()
    {
        return instance;
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
            if(!isWithinMovingRange(clickedTile))
            {
                Vector3 newVector = GetClosestPossibleTile(clickedTile);
                currentPlayer.Move(newVector);
            }
            else
            {
                currentPlayer.Move(clickedTile);
            }
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

            if (!tiles[clickedX, clickedY].isOccupied())
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
        Vector3 newVector = clickedTile;
        float offset = 4.6F;
        int clickedX = (int)(newVector.x + offset);
        int clickedY = (int)(newVector.z + offset);

        Vector3 playerPosition = currentPlayer.Unit.transform.localPosition;

        int playerX = (int)(playerPosition.x + offset);
        int playerY = (int)(playerPosition.z + offset);

        bool decreaseX = false;
        bool decreaseY = false;

        bool sameX = false;
        bool sameY = false;

        if(playerX < clickedX)
        {
            decreaseX = true;
        }
        else if(playerX == clickedX)
        {
            sameX = true;
        }

        if(playerY < clickedY)
        {
            decreaseY = true;
        }
        else if(playerY == clickedY)
        {
            sameY = true;
        }

        int i = 2;
        int differece = CalcDiff(newVector);
        while (differece > currentPlayer.moveRange)
        {
            if(i%2 == 1)
            {
                if(!sameX)
                {
                    if (decreaseX)
                    {
                        newVector.x = newVector.x - 1;
                    }
                    else
                    {
                        newVector.x = newVector.x + 1;
                    }
                }
            }
            else
            {
                if(!sameY)
                {
                    if (decreaseY)
                    {
                        newVector.z = newVector.z - 1;

                        clickedY = (int)(newVector.z + offset);
                    }
                    else
                    {
                        newVector.z = newVector.z + 1;

                        clickedY = (int)(newVector.z + offset);
                    }
                }
            }
            i++;
            differece = CalcDiff(newVector);
        }
        return newVector;
    }

    public void CheckAttack(Vector3 clickedTile)
    {
        int difference = CalcDiff(clickedTile);
        if(difference <= currentPlayer.moveRange + currentPlayer.attackRange && difference != currentPlayer.moveRange && difference != 1)
        {
            Result currResult = new Result { distance = 9999 };

            Vector3 upCheck = new Vector3(clickedTile.x, clickedTile.y, clickedTile.z + 1);
            Vector3 downCheck = new Vector3(clickedTile.x, clickedTile.y, clickedTile.z - 1);
            Vector3 rightCheck = new Vector3(clickedTile.x + 1, clickedTile.y, clickedTile.z);
            Vector3 leftCheck = new Vector3(clickedTile.x - 1, clickedTile.y, clickedTile.z);

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

            CheckMove(currResult.destination);
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