using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    List<UnitController> list;
    UnitController currentPlayer;
    Tile[,] tiles;

    // Start is called before the first frame update
    void Start()
    {
        tiles = MapGenerator.GetInstance().GetTiles();
    }

    // Update is called once per frame
    void Update()
    {
        list = UnitList.GetInstance().GetList();
        currentPlayer = list[0];
        CheckAttack(new Vector3(1.5F, 50, -2.5F));
    }

    public void CheckMove(Vector3 clickedTile)
    {
        if(isWithinMovingRange(clickedTile))
        {
            currentPlayer.Move(clickedTile);
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

    public void CheckAttack(Vector3 clickedTile)
    {
        int difference = CalcDiff(clickedTile);
        if(difference <= currentPlayer.moveRange + currentPlayer.attackRange)
        {
            float offset = 4.6F;
            int clickedX = (int)(clickedTile.x + offset);
            int clickedY = (int)(clickedTile.z + offset);
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
        return new Result { distance = 9999 };
    }

}