using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnController : MonoBehaviour
{
    public List<UnitController> players;
    public List<UnitController> playersCounter;
    public List<UnitController> turns;

    private static TurnController instance;

    bool isCounting = false;
    bool isInitiated = false;
    bool isDoneSorting = false;

    void Start()
    {
        instance = this;
        turns = new List<UnitController>();
        playersCounter = new List<UnitController>();
    }

    // Update is called once per frame
    void Update()
    {
        players = UnitList.GetInstance().GetList();
        if (!isInitiated)
        {
            List<UnitController> sortedList = SortListBySpeed();

            playersCounter = sortedList.GetRange(0, sortedList.Count);
            CountTurns();
            ChangePlayer();
            isInitiated = true;
        }
        if (players.Count != playersCounter.Count)
        {
            playersCounter = players;
        }

        if (turns.Count < 6 && isDoneSorting)
        {
            CheckDeaths();
            CountTurns();
            ChangePlayer();
        }
        if (turns[0].unit.enemy && !MovementController.GetInstance().isAnyPlayerMoving)
        {
            EnemyTurn();
        }
    }

    public static TurnController GetInstance()
    {
        return instance;
    }

    public List<UnitController> GetTurnsList()
    {
        return turns;
    }

    void CountTurns()
    {
        isCounting = true;
        while (turns.Count < 6)
        {
            playersCounter[0].unit.addSpeed();
            if (playersCounter[0].unit.isSpeedAchieved() && !playersCounter[0].isDead)
            {
                turns.Add(playersCounter[0]);
                isCounting = false;
            }
            UnitController playerToRemove = playersCounter[0];
            playersCounter.RemoveAt(0);
            playersCounter.Add(playerToRemove);
        }
        isCounting = false;
        TurnPanel.GetInstance().CreateRawImage();
    }

    void CheckDeaths()
    {
        for(int i = 0; i < turns.Count; i++)
        {
            if(turns[i].unit.isDead)
            {
                turns.RemoveAt(i);
            }
        }
    }

    void ChangePlayer()
    {
        if (turns[0] != MovementController.GetInstance().GetCurrentPlayer())
        {
            MovementController.GetInstance().SetCurrentPlayer(turns[0]);
        }
    }

    void EnemyTurn()
    {
        UnitController Unit = null;
        float highestHP = 0;
        for (int i = 0; i < players.Count; i++)
        {
            if (Unit == null)
            {
                if (!players[i].unit.enemy)
                {
                    Unit = players[i];
                    highestHP = players[i].currentHP;
                }
            }
            else
            {
                if (highestHP < players[i].currentHP)
                {
                    if (!players[i].unit.enemy && !players[i].unit.isDead)
                    {
                        Unit = players[i];
                        highestHP = players[i].currentHP;
                    }
                }
            }
        }
        float offset = 4.6F;
        int x = (int)(Unit.Unit.transform.localPosition.x + offset);
        int y = (int)(Unit.Unit.transform.localPosition.z + offset);

        Tile[,] tiles = MapGenerator.GetInstance().GetTiles();
        Tile matchedTile = tiles[x, y];

        if (MovementController.GetInstance().isWithinAttackRange(matchedTile.floor.transform.localPosition))
        {
            MovementController.GetInstance().CheckAttack(matchedTile.floor.transform.localPosition);
        }
        else
        {
            MovementController.GetInstance().CheckMove(matchedTile.floor.transform.localPosition);
        }
    }

    List<UnitController> SortListBySpeed()
    {
        List<UnitController> sortedList = new List<UnitController>();
        List<UnitController> unsortedList = players.GetRange(0, players.Count);

        while (!isDoneSorting)
        {
            int indexSpeed = 0;
            UnitController highestSpeed = unsortedList[0];
            for (int i = 0; i < unsortedList.Count; i++)
            {
                if (unsortedList[i].speed > highestSpeed.speed)
                {
                    highestSpeed = unsortedList[i];
                    indexSpeed = i;
                }
            }
            if (unsortedList.Count == 1)
            {
                UnitController playerToRemove = unsortedList[0];
                unsortedList.RemoveAt(0);
                sortedList.Add(playerToRemove);
                //Debug.Log("Added " + playerToRemove.Unit.name);

                isDoneSorting = true;
            }
            else
            {
                UnitController playerToRemove = unsortedList[indexSpeed];
                unsortedList.RemoveAt(indexSpeed);
                sortedList.Add(playerToRemove);
                //Debug.Log("Added " + playerToRemove.Unit.name);
            }
        }
        playersCounter = sortedList;
        isDoneSorting = true;
        return sortedList;
    }

}
