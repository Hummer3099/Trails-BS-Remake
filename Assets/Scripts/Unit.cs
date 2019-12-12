using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit
{
    public int hp { get; set; }
    public int attack { get; set; }
    public int defense { get; set; }
    public int speed { get; set; }
    public int counter = 0;
    public int remainder = 0;
    public int moveRange { get; set; }
    public int attackRange { get; set; }
    public float critical { get; set; }
    public float accuracy { get; set; }
    public float evasion { get; set; }
    public bool enemy { get; set; }

    public bool isEnemy()
    {
        return (enemy);
    }

    public void addSpeed()
    {
        counter += speed;
    }
    
    public bool isSpeedAchieved()
    {
        if(counter >= 10)
        {
            remainder = counter - 10;
            counter = remainder;
            remainder = 0;
            return true;
        }
        else
        {
            return false;
        }
    }
}
