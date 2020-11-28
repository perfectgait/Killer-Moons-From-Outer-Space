using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }
    }

    public void LoadPreviousScene()
    {
        AddCurrentSceneToHistory();
        SceneManager.LoadScene(SceneHistory.instance.GetPrevious());
    }

    public void LoadMainMenu()
    {
        SceneHistory.instance.Reset();
        SceneManager.LoadScene("Main Menu");
    }

    public void LoadNextLevel()
    {
        AddCurrentSceneToHistory();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadNextLevelWithTransition()
    {
        StartCoroutine(InternalLoadNextLevelWithTransition());
    }

    // TODO: Generalize this transition behavior so that it doesn't just load the next level
    private IEnumerator InternalLoadNextLevelWithTransition()
    {
        if (transition != null)
        {
            transition.SetTrigger("Start");
        }
        else
        {
            Debug.Log("Transition failed: No Animator set on Level Loader");
        }
        yield return new WaitForSeconds(transitionTime);
        LoadNextLevel();
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void LoadWinScreen()
    {
        AddCurrentSceneToHistory();
        SceneManager.LoadScene("Win Screen");
    }

    public void LoadFirstLevel()
    {
        AddCurrentSceneToHistory();
        SceneManager.LoadScene("Level 1");
    }

    public void LoadLoseScreen()
    {
        AddCurrentSceneToHistory();
        SceneManager.LoadScene("Lose Screen");
    }

    private void AddCurrentSceneToHistory()
    {
        SceneHistory.instance.Add(SceneManager.GetActiveScene().name);
    }
}
