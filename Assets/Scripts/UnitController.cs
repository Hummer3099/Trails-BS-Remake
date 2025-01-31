﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitController : MonoBehaviour
{
    public Unit unit;
    public GameObject Unit;
    public GameObject portrait;

    public int attack;
    public int defense;
    public int speed;
    public int attackRange;
    public int moveRange;
    public int hp;
    public float currentHP;
    float moveSpeed = 0.72F;
    int currentX;
    int currentY;
    public Image HealthBar;
    public Text HPtext;
    public Text attStat;
    public Text moveStat;

    Vector3 currentTarget;
    bool isMoving = false;
    Compass compass = new Compass();

    public bool isDead = false;

    public bool isDefendMode;
    
    private static UnitController instance;
    public static UnitController GetInstance()
    {
        return instance;
    }

    void Start()
    {
        instance = this;
        InitializeUnit();
        RegisterTile();
        UnitList.GetInstance().GetList().Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        CheckMovement();
        UpdateHPBar();
        UpdateHPtext();
        UpdateStats();
        UpdateUnitHP();
        isUnitDead();
    }

    void UpdateUnitHP()
    {
        this.currentHP = unit.hp;
    }

    void UpdateUnitDefense()
    {
        this.defense = unit.defense;
    }

    void isUnitDead()
    {
        if(currentHP ==0 && !isDead)
        {
            if(!unit.enemy)
            {
                GameOverController.GetInstance().IncreaseDeadParty();
            }
            else
            {
                GameOverController.GetInstance().IncreaseDeadEnemy();
            }
            Destroy(Unit);
            UnitList.GetInstance().GetList().Remove(this);
            TurnController.GetInstance().GetTurnsList().Remove(this);
            isDead = true;
        }
    }

    void InitializeUnit()
    {
        unit = new Unit { attack = attack, defense = defense, attackRange = attackRange, moveRange = moveRange, hp = hp, speed=speed};
        if (Unit.gameObject.CompareTag("Party"))
        {
            unit.enemy = false;
        }
        else if (Unit.gameObject.CompareTag("Enemy"))
        {
            unit.enemy = true;
        }
    }

    void RegisterTile()
    {
        float offset = 4.6F;
        int x = (int)(Unit.transform.localPosition.x + offset);
        int y = (int)(Unit.transform.localPosition.z + offset);
        MapGenerator.GetInstance().GetTiles()[x, y].unit = unit;
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
        UpdateCoordinates();

        Tile[,] tiles = MapGenerator.GetInstance().GetTiles();

        float offset = 4.6F;
        int nextX = (int) (target.x + offset);
        int nextY = (int)(target.z + offset);

        Vector3 origin = Unit.transform.localPosition;
        if(nextX >= 0 && nextY >= 0 && nextX < tiles.GetLength(0) && nextY < tiles.GetLength(1))
        {
            if (!tiles[nextX, nextY].isOccupied())
            {
                if (origin.x > target.x)
                {
                    compass.left = true;
                }
                else if (origin.x < target.x)
                {
                    compass.right = true;
                }

                if (origin.z < target.z)
                {
                    compass.up = true;
                }
                else if (origin.z > target.z)
                {
                    compass.down = true;
                }

                isMoving = true;
                MovementController.GetInstance().isAnyPlayerMoving = true;
                currentTarget = target;

                tiles[currentX, currentY].unit = null;
                tiles[nextX, nextY].unit = unit;
                UpdateCoordinates();

                Debug.Log("Assigned at " + nextX + " " + nextY);
            }
        }
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

    void UpdateCoordinates()
    {
        float offset = 4.6F;
        int x = (int)(Unit.transform.localPosition.x + offset);
        int y = (int)(Unit.transform.localPosition.z + offset);
        currentX = x;
        currentY = y;
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
            MovementController.GetInstance().isAnyPlayerMoving = false;
            UIController.getInstance().isMoving = false;
            TurnController.GetInstance().turns.RemoveAt(0);
        }
    }
    void UpdateHPBar()
    {
        float calcHP = currentHP / hp;
        HealthBar.transform.localScale = new Vector3(Mathf.Clamp(calcHP, 0, 1), HealthBar.transform.transform.localScale.y, HealthBar.transform.transform.localScale.z);
        if(calcHP<0.66 && calcHP > 0.33)
        {
            HealthBar.GetComponent<Image>().color = new Color(255, 255, 0);
        }else if(calcHP < 0.33)
        {
            HealthBar.GetComponent<Image>().color = new Color(255,0, 0);
        }
        else
        {
            HealthBar.GetComponent<Image>().color = new Color(0, 255, 0);
        }
    }
    void UpdateHPtext()
    {
        HPtext.text = "HP: " + currentHP;
    }
    void UpdateStats()
    {
        attStat.text = "Range: " + attackRange + " " + "Move: " + moveRange;
    }
    public GameObject getRawImage()
    {
        return portrait;
    }

}