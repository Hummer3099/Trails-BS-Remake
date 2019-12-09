using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject floor;
    public GameObject level;
    public Tile[,] tiles = new Tile[10,10];

    private static MapGenerator instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        GenerateMap();
    }

    void Start()
    {
        
    }
    public static MapGenerator GetInstance()
    {
        return instance;
    }
    public Tile[,] GetTiles()
    {
        return tiles;
    }

    void GenerateMap()
    {
        float offset = -4.5F;
        for (int i = 0; i < 10; i++)
        {
            float offsetX = i + offset;
            for(int j = 0; j < 10; j++)
            {
                float offsetY = j + offset;
                GameObject floorToCreate = Instantiate(floor, new Vector3(offsetX, 0, offsetY), Quaternion.identity);
                floorToCreate.transform.SetParent(level.transform, false);
                Vector3 position = new Vector3(offsetX, 0, offsetY);
                floorToCreate.transform.localPosition = position;
                tiles[i, j] = (new Tile { floor = floorToCreate});
            }
        }
    }
}
