using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Navigation : MonoBehaviour, ISelectHandler, ISubmitHandler, IPointerEnterHandler, IPointerClickHandler
{
    const string START_BUTTON = "Start Button";
    const string CONTINUE_BUTTON = "Continue Button";
    const string RESTART_BUTTON = "Restart Button";
    const string MAIN_MENU_BUTTON = "Main Menu Button";
    const string QUIT_BUTTON = "Quit Button";

    bool isInitialSelection = true;
    
    GameObject menuCursor;
    Animator menuCursorAnimator;
    AudioManager audioManager;
    EventSystem eventSystem;
    LevelLoader levelLoader;

    private void Start()
    {
        menuCursor = GameObject.Find("Menu Cursor");
        menuCursorAnimator = menuCursor.GetComponent<Animator>();
        audioManager = AudioManager.instance;
        eventSystem = EventSystem.current;
        levelLoader = FindObjectOfType<LevelLoader>();
    }

    public void OnSelect(BaseEventData eventData)
    {
        menuCursor.transform.position = new Vector3(menuCursor.transform.position.x, transform.position.y);

        if (gameObject.name == eventSystem.firstSelectedGameObject.name && isInitialSelection)
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
        menuCursorAnimator.SetTrigger("Explode");
        audioManager.PlaySoundEffect("Menu Select");
        yield return new WaitForSeconds(.3f);
        menuCursor.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(.3f);

        switch (gameObject.name)
        {
            case START_BUTTON:
            case CONTINUE_BUTTON:
                GameScore.instance.Reset();
                levelLoader.LoadNextLevelWithTransition();
                break;
            case RESTART_BUTTON:
                GameScore.instance.Reset();
                levelLoader.LoadFirstLevel();
                break;
            case MAIN_MENU_BUTTON:
                GameScore.instance.Reset();
                levelLoader.LoadMainMenu();
                break;
            case QUIT_BUTTON:
                levelLoader.QuitGame();
                break;
            default:
                Debug.Log("Can't submit: Unknown button name");
                break;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!eventSystem.alreadySelecting)
        {
            eventSystem.SetSelectedGameObject(gameObject);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        StartCoroutine(HandleSubmit());
    }
}
