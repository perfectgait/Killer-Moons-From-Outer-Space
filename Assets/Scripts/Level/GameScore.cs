using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScore : MonoBehaviour
{
    [SerializeField] private int score;

    public static GameScore instance;

    private void Awake()
    {
        if (instance != null)
        {
            if (instance == this)
            {
                Destroy(this);
            }
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public int GetTotal()
    {
        return score;
    }

    public void IncrementBy(int score)
    {
        this.score += score;
    }

    // TODO: Reset is a built-in method. If this doesn't work, change to ResetTotal
    public void Reset()
    {
        score = 0;
    }
}
