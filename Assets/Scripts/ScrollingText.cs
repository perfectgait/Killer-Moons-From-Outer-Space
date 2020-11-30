using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingText : MonoBehaviour
{
    [SerializeField] float startTime = 2f;
    [SerializeField] float scrollSpeed = 1f;
    [SerializeField] bool shouldHideNavigationUntilEnd = true;

    GameObject navigation;

    private float currentScrollSpeed;
    private float scrollSpeedIncrease = 5f;
    private bool shouldStopScrolling = false;

    private InputManager inputManager;

    private void Awake()
    {
        inputManager = new InputManager();
    }

    private void OnEnable()
    {
        inputManager.ScrollingText.Enable();
    }

    private void OnDisable()
    {
        inputManager.ScrollingText.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        navigation = GameObject.Find("Navigation");
        navigation.SetActive(!shouldHideNavigationUntilEnd);

        currentScrollSpeed = scrollSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsBeforeScrollStart())
        {
            return;
        }

        HandleScrollSpeed();
        HandleHiddenNavigation();
        ScrollText();
    }

    private bool IsBeforeScrollStart()
    {
        return Time.timeSinceLevelLoad <= startTime;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        shouldStopScrolling = true;
    }

    private void HandleScrollSpeed()
    {
        bool scrollFastIsPressedDown = Mathf.Abs(inputManager.ScrollingText.ScrollFast.ReadValue<float>()) > 0;

        if (scrollFastIsPressedDown)
        {
            currentScrollSpeed = scrollSpeed * scrollSpeedIncrease;
        }
        else
        {
            currentScrollSpeed = scrollSpeed;
        }
    }

    private void HandleHiddenNavigation()
    {
        if (shouldHideNavigationUntilEnd && shouldStopScrolling)
        {
            navigation.SetActive(true);
        }
    }

    private void ScrollText()
    {
        if (!shouldStopScrolling)
        {
            transform.position += Vector3.up * currentScrollSpeed * Time.deltaTime;
        }
    }
}
