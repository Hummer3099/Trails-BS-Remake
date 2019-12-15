using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    bool isGameOver = false;
    public int partyCounter;
    public int deadPartyCounter;
    public int enemyCounter;
    public int deadEnemyCounter;
    private static GameOverController instance;

    bool haveChecked = false;

    void Start()
    {
        instance = this;
        partyCounter = 0;
        deadPartyCounter = 0;
        enemyCounter = 0;
        deadEnemyCounter = 0;
    }

    void Update()
    {
        if(!haveChecked)
        {
            List<UnitController> list = UnitList.GetInstance().GetList();
            if (list != null)
            {
                for(int i = 0; i < list.Count; i++)
                {
                    if(list[i].unit.enemy)
                    {
                        enemyCounter++;
                    }
                    else
                    {
                        partyCounter++;
                    }
                }
                haveChecked = true;
            }
        }
        if (partyCounter == deadPartyCounter && partyCounter != 0 && !MovementController.GetInstance().isAnyPlayerMoving)
        {
            SceneManager.LoadScene(1);
        }
        else if(enemyCounter == deadEnemyCounter && enemyCounter != 0 && !MovementController.GetInstance().isAnyPlayerMoving)
        {
            SceneManager.LoadScene(2);
        }
    }

    public static GameOverController GetInstance()
    {
        return instance;
    }

    public void IncreaseParty()
    {
        partyCounter++;
    }
    public void IncreaseDeadParty()
    {
        deadPartyCounter++;
    }
    public void IncreaseEnemy()
    {
        enemyCounter++;
    }
    public void IncreaseDeadEnemy()
    {
        deadEnemyCounter++;
    }
}
