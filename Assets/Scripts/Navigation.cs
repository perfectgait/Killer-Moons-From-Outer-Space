using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Navigation : MonoBehaviour, ISelectHandler, ISubmitHandler
{
    bool isInitialSelection = true;
    
    GameObject menuCursor;
    Animator menuCursorAnimator;
    AudioManager audioManager;


    private void Start()
    {
        menuCursor = GameObject.Find("Menu Cursor");
        menuCursorAnimator = menuCursor.GetComponent<Animator>();
        audioManager = AudioManager.instance;
    }

    public void OnSelect(BaseEventData eventData)
    {
        menuCursor.transform.position = new Vector3(menuCursor.transform.position.x, transform.position.y);

        if (gameObject.name == "Start Button" && isInitialSelection)
        {
            isInitialSelection = false;
        }
        else
        {
            audioManager.PlaySoundEffect("Menu Move");
        }
    }

    public void OnSubmit(BaseEventData eventData)
    {
        StartCoroutine(HandleSubmit());
    }

    public IEnumerator HandleSubmit()
    {
        // TODO: Fix timing of animation and sound effect
        // TODO: Why is there sometimes a delay when triggering the animation?
        menuCursorAnimator.SetTrigger("Explode");
        audioManager.PlaySoundEffect("Menu Select");
        yield return new WaitForSeconds(menuCursorAnimator.GetCurrentAnimatorStateInfo(0).length + menuCursorAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime);
        Destroy(menuCursor);
        if (gameObject.name == "Start Button")
        {
            FindObjectOfType<LevelLoader>().LoadNextScene();
        }
        else
        {
            FindObjectOfType<LevelLoader>().QuitGame();
        }
    }
}
