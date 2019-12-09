using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compass
{
    public bool right { get; set; }
    public bool left { get; set; }
    public bool up { get; set; }
    public bool down { get; set; }
    public bool isDoneMoving()
    {
        if(!right && !left && !up && !down)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
