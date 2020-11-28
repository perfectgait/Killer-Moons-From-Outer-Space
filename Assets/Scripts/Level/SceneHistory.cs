using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneHistory : MonoBehaviour
{
    public static SceneHistory instance;

    // TODO: Probably won't be an issue, but we may want to consider limiting the size of this list
    private List<string> history = new List<string>();

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

    public void Add(string sceneName)
    {
        history.Add(sceneName);
    }

    public string GetPrevious()
    {
        int count = history.Count;
        if (count > 0)
        {
            return history[count - 2];
        }
        else
        {
            Debug.Log("No previous scene found: Scene History is empty");
            return "";
        }
    }

    public void Reset()
    {
        history.Clear();
    }
}
