using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    public Unit unit;
    public GameObject Unit;

    public int attack;
    public int defense;
    public int attackRange;
    public int moveRange;
    public int hp;
    float moveSpeed = 0.3F;
    int currentX;
    int currentY;

    Vector3 currentTarget;
    bool isMoving = false;
    Compass compass = new Compass();

    void Start()
    {
        InitializeUnit();
        RegisterTile();
        UnitList.GetInstance().GetList().Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        CheckMovement();
    }

    void InitializeUnit()
    {
        unit = new Unit { attack = attack, defense = defense, attackRange = attackRange, moveRange = moveRange, hp = hp};
        if (Unit.gameObject.CompareTag("Party"))
        {
            unit.enemy = false;
            Debug.Log(Unit.name + " is a party member.");
        }
        else if (Unit.gameObject.CompareTag("Enemy"))
        {
            unit.enemy = true;
            Debug.Log(Unit.name + " is an enemy.");
        }
    }

    void RegisterTile()
    {
        float offset = 4.6F;
        int x = (int)(Unit.transform.localPosition.x + offset);
        int y = (int)(Unit.transform.localPosition.z + offset);
        Debug.Log(Unit.name + "  will be added at " + x + " " + y);
        MapGenerator.GetInstance().GetTiles()[x, y].unit = unit;
        Debug.Log(Unit.name + " added at " + x + " " + y);
        currentX = x;
        currentY = y;
    }

    void CheckMovement()
    {
        if(isMoving)
        {
            if(compass.left)
            {
                MoveLeft();
            }
            else if(compass.right)
            {
                MoveRight();
            }

            if(compass.up)
            {
                MoveUp();
            }
            else if(compass.down)
            {
                MoveDown();
            }
        }
    }

    public void Move(Vector3 target)
    {
        Tile[,] tiles = MapGenerator.GetInstance().GetTiles();

        float offset = 4.6F;
        int nextX = (int) (target.x + offset);
        int nexty = (int)(target.z + offset);

        Vector3 origin = Unit.transform.localPosition;

        if(origin.x > target.x)
        {
            compass.left = true;
        }
        else if(origin.x < target.x)
        {
            compass.right = true;
        }
        
        if(origin.z < target.z)
        {
            compass.up = true;
        }
        else if(origin.z > target.z)
        {
            compass.down = true;
        }

        isMoving = true;
        currentTarget = target;

        tiles[currentX, currentY].unit = null;
        tiles[nextX, nexty].unit = unit;
        Debug.Log("Assigned at " + nextX + " " + nexty);
    }

    void MoveLeft()
    {
        if(Unit.transform.localPosition.x > currentTarget.x)
        {
            Unit.transform.Translate(Unit.transform.right * -moveSpeed);
        }
        else
        {
            compass.left = false;
            CheckCompass();
        }
    }

    void MoveRight()
    {
        if (Unit.transform.localPosition.x < currentTarget.x)
        {
            Unit.transform.Translate(Unit.transform.right * moveSpeed);
        }
        else
        {
            compass.right = false;
            CheckCompass();
        }
    }

    void MoveUp()
    {
        if (Unit.transform.localPosition.z < currentTarget.z)
        {
            Unit.transform.Translate(Unit.transform.forward * moveSpeed);
        }
        else
        {
            compass.up = false;
            CheckCompass();
        }
    }

    void MoveDown()
    {
        if (Unit.transform.localPosition.z > currentTarget.z)
        {
            Unit.transform.Translate(Unit.transform.forward * -moveSpeed);
        }
        else
        {
            compass.down = false;
            CheckCompass();
        }
    }

    void CheckCompass()
    {
        if (compass.isDoneMoving())
        {
            isMoving = false;
        }
    }
}