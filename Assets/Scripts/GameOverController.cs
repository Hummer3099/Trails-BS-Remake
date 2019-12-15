using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    bool isGameOver = false;
    public int partyCounter;
    public int deadMembers;
    private static GameOverController instance;

    void Start()
    {
        instance = this;
        partyCounter = 0;
        deadMembers = 0;
    }

    void Update()
    {
        if (partyCounter == deadMembers && partyCounter != 0 && !MovementController.GetInstance().isAnyPlayerMoving)
        {
            SceneManager.LoadScene(1);
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
    public void IncreaseDead()
    {
        deadMembers++;
    }
}
