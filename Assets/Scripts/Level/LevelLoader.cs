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

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void LoadNextLevel()
    {
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
        SceneManager.LoadScene("Win Screen");
    }

    public void LoadFirstLevel()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadLoseScreen()
    {
        SceneManager.LoadScene("Lose Screen");
    }

}
